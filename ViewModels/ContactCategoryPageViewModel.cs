using AgendaContatos.Models;

namespace AgendaContatos.ViewModels;

public class ContactCategoryPageViewModel
{
    public required CategoryMetadata CurrentCategory { get; init; }

    public required IReadOnlyList<CategoryMetadata> Categories { get; init; }

    public required IReadOnlyList<Contact> Contacts { get; init; }

    public required ContactFormViewModel FormModel { get; init; }

    public bool IsEditing => FormModel.Id.HasValue;
}