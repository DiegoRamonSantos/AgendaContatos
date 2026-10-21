using AgendaContatos.Data;
using AgendaContatos.Models;
using AgendaContatos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Controllers;

public class ContactsController(AppDbContext dbContext, ILogger<ContactsController> logger) : Controller
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<ContactsController> _logger = logger;

    [HttpGet("/contatos/{categorySlug}")]
    public async Task<IActionResult> Category(string categorySlug, int? editId, CancellationToken cancellationToken)
    {
        if (!ContactCategoryCatalog.TryGetBySlug(categorySlug, out var currentCategory))
        {
            _logger.LogWarning("Categoria com slug {CategorySlug} nao encontrada.", categorySlug);
            return NotFound();
        }

        var formModel = new ContactFormViewModel
        {
            CategorySlug = currentCategory.Slug
        };

        if (editId.HasValue)
        {
            var contact = await _dbContext.Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    item => item.Id == editId.Value && item.Category == currentCategory.Category,
                    cancellationToken);

            if (contact is null)
            {
                _logger.LogWarning(
                    "Contato {ContactId} nao encontrado para edicao na categoria {CategorySlug}.",
                    editId.Value,
                    currentCategory.Slug);
                TempData["FeedbackError"] = "O contato solicitado não foi encontrado nesta categoria.";
                return RedirectToAction(nameof(Category), new { categorySlug = currentCategory.Slug });
            }

            formModel = MapToForm(contact, currentCategory.Slug);
        }

        return View(await BuildCategoryPageAsync(currentCategory, formModel, cancellationToken));
    }

    [HttpPost("/contatos/salvar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(ContactFormViewModel formModel, CancellationToken cancellationToken)
    {
        if (!ContactCategoryCatalog.TryGetBySlug(formModel.CategorySlug, out var currentCategory))
        {
            _logger.LogWarning("Categoria com slug {CategorySlug} nao encontrada ao salvar contato.", formModel.CategorySlug);
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return View("Category", await BuildCategoryPageAsync(currentCategory, formModel, cancellationToken));
        }

        var name = formModel.Name?.Trim() ?? string.Empty;
        var phone = formModel.Phone?.Trim() ?? string.Empty;
        var email = formModel.Email?.Trim() ?? string.Empty;

        if (formModel.Id.HasValue)
        {
            var contact = await _dbContext.Contacts
                .FirstOrDefaultAsync(
                    item => item.Id == formModel.Id.Value && item.Category == currentCategory.Category,
                    cancellationToken);

            if (contact is null)
            {
                _logger.LogWarning(
                    "Contato {ContactId} nao encontrado para atualizacao na categoria {CategorySlug}.",
                    formModel.Id.Value,
                    currentCategory.Slug);
                TempData["FeedbackError"] = "Não foi possível localizar o contato para edição.";
                return RedirectToAction(nameof(Category), new { categorySlug = currentCategory.Slug });
            }

            contact.Name = name;
            contact.Phone = phone;
            contact.Email = email;
            contact.Notes = formModel.Notes?.Trim() ?? string.Empty;
            contact.UpdatedAt = DateTime.UtcNow;

            TempData["FeedbackSuccess"] = "Contato atualizado com sucesso.";
        }
        else
        {
            _dbContext.Contacts.Add(new Contact
            {
                Name = name,
                Phone = phone,
                Email = email,
                Notes = formModel.Notes?.Trim() ?? string.Empty,
                Category = currentCategory.Category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            TempData["FeedbackSuccess"] = "Contato cadastrado com sucesso.";
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Category), new { categorySlug = currentCategory.Slug });
    }

    [HttpPost("/contatos/excluir/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, string categorySlug, CancellationToken cancellationToken)
    {
        if (!ContactCategoryCatalog.TryGetBySlug(categorySlug, out var currentCategory))
        {
            _logger.LogWarning("Categoria com slug {CategorySlug} nao encontrada ao excluir contato.", categorySlug);
            return NotFound();
        }

        var contact = await _dbContext.Contacts
            .FirstOrDefaultAsync(
                item => item.Id == id && item.Category == currentCategory.Category,
                cancellationToken);

        if (contact is null)
        {
            _logger.LogWarning(
                "Contato {ContactId} nao encontrado para exclusao na categoria {CategorySlug}.",
                id,
                currentCategory.Slug);
            TempData["FeedbackError"] = "O contato que você tentou excluir não foi encontrado.";
            return RedirectToAction(nameof(Category), new { categorySlug = currentCategory.Slug });
        }

        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["FeedbackSuccess"] = "Contato excluído com sucesso.";

        return RedirectToAction(nameof(Category), new { categorySlug = currentCategory.Slug });
    }

    private async Task<ContactCategoryPageViewModel> BuildCategoryPageAsync(
        CategoryMetadata currentCategory,
        ContactFormViewModel? formModel = null,
        CancellationToken cancellationToken = default)
    {
        var contacts = await _dbContext.Contacts
            .AsNoTracking()
            .Where(item => item.Category == currentCategory.Category)
            .OrderBy(item => item.Name)
            .ThenBy(item => item.Email)
            .ToListAsync(cancellationToken);

        return new ContactCategoryPageViewModel
        {
            CurrentCategory = currentCategory,
            Categories = ContactCategoryCatalog.All,
            Contacts = contacts,
            FormModel = formModel ?? new ContactFormViewModel { CategorySlug = currentCategory.Slug }
        };
    }

    private static ContactFormViewModel MapToForm(Contact contact, string categorySlug)
    {
        return new ContactFormViewModel
        {
            Id = contact.Id,
            Name = contact.Name,
            Phone = contact.Phone,
            Email = contact.Email,
            Notes = contact.Notes,
            CategorySlug = categorySlug
        };
    }
}