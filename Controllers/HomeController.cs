using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AgendaContatos.Data;
using AgendaContatos.Models;
using AgendaContatos.ViewModels;

namespace AgendaContatos.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = ContactCategoryCatalog.All.Select(cat => new CategoryStatsViewModel
        {
            Name = cat.Name,
            Slug = cat.Slug,
            Icon = cat.Icon,
            Description = cat.Description,
            HelperText = cat.HelperText,
            ColorClass = GetColorClassForCategory(cat.Category),
            ContactCount = _context.Contacts.Count(c => c.Category == cat.Category)
        }).ToList();

        var viewModel = new HomePageViewModel
        {
            Categories = categories,
            TotalContacts = _context.Contacts.Count()
        };

        return View(viewModel);
    }

    private static string GetColorClassForCategory(ContactCategory category)
    {
        return category switch
        {
            ContactCategory.Family => "category-family",
            ContactCategory.Work => "category-work",
            ContactCategory.Friends => "category-friends",
            ContactCategory.Others => "category-others",
            _ => "category-default"
        };
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}