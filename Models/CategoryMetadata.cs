namespace AgendaContatos.Models;

public sealed record CategoryMetadata(
    ContactCategory Category,
    string Name,
    string Slug,
    string Icon,
    string Description,
    string HelperText);
