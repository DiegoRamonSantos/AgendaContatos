using System.Diagnostics.CodeAnalysis;

namespace AgendaContatos.Models;

public static class ContactCategoryCatalog
{
    public static IReadOnlyList<CategoryMetadata> All { get; } =
    [
        new(ContactCategory.Family, "Família", "familia", "👨‍👩‍👧", "Registre parentes, responsáveis e vínculos importantes do dia a dia.", "Para manter os contatos mais próximos sempre por perto."),
        new(ContactCategory.Work, "Trabalho", "trabalho", "💼", "Centralize colegas, clientes e parceiros profissionais em um só lugar.", "Ideal para networking, fornecedores e contatos corporativos."),
        new(ContactCategory.Friends, "Amigos", "amigos", "🎉", "Organize amizades, grupos e pessoas com quem você compartilha momentos especiais.", "Perfeito para recados rápidos, convites e reencontros."),
        new(ContactCategory.Others, "Outros", "outros", "📂", "Guarde contatos diversos que não se encaixam nas categorias principais.", "Use para serviços, vizinhos, suporte e referências gerais.")
    ];

    public static CategoryMetadata GetMetadata(ContactCategory category)
    {
        return All.First(item => item.Category == category);
    }

    public static IReadOnlyList<string> GetDetailOptions(ContactCategory category)
    {
        return category switch
        {
            ContactCategory.Family =>
            [
                "Pai",
                "Mãe",
                "Irmão",
                "Irmã",
                "Avô",
                "Avó",
                "Tio",
                "Tia",
                "Primo",
                "Prima",
                "Cônjuge"
            ],
            ContactCategory.Work =>
            [
                "Colega",
                "Cliente",
                "Fornecedor",
                "Parceiro",
                "Gestor",
                "Equipe",
                "RH",
                "Suporte"
            ],
            ContactCategory.Friends =>
            [
                "Melhor amigo",
                "Amigo próximo",
                "Faculdade",
                "Escola",
                "Grupo social",
                "Vizinho",
                "Conhecido"
            ],
            _ =>
            [
                "Serviço",
                "Emergência",
                "Saúde",
                "Comércio",
                "Suporte",
                "Vizinho",
                "Referência"
            ]
        };
    }

    public static bool TryGetBySlug(string? slug, [NotNullWhen(true)] out CategoryMetadata? metadata)
    {
        metadata = All.FirstOrDefault(item => item.Slug.Equals(slug ?? string.Empty, StringComparison.OrdinalIgnoreCase));

        return metadata is not null;
    }
}
