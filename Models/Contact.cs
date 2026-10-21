using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Models;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do contato.")]
    [StringLength(120, ErrorMessage = "O nome deve ter no máximo 120 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o telefone do contato.")]
    [StringLength(30, ErrorMessage = "O telefone deve ter no máximo 30 caracteres.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail do contato.")]
    [ValidEmail(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(160, ErrorMessage = "O e-mail deve ter no máximo 160 caracteres.")]
    public string Email { get; set; } = string.Empty;
    
    public string Notes { get; set; } = string.Empty;

    [Required]
    public ContactCategory Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
