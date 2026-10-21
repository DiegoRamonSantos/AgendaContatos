namespace AgendaContatos.ViewModels;

public class CategoryStatsViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HelperText { get; set; } = string.Empty;
    public string ColorClass { get; set; } = string.Empty;
    public int ContactCount { get; set; }
}

public class HomePageViewModel
{
    public IReadOnlyList<CategoryStatsViewModel> Categories { get; set; } = [];
    public int TotalContacts { get; set; }
}
