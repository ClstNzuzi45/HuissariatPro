using System.ComponentModel.DataAnnotations;

namespace HuissariatPro.Models;

public class Etude
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Nom { get; set; } = ""; // Ex : Etude Maître Kabila

    [Required, StringLength(100)]
    public string Ville { get; set; } = "";

    [Required, StringLength(100)]
    public string Province { get; set; } = "";

    [StringLength(200)]
    public string? Adresse { get; set; }

    [Phone, StringLength(30)]
    public string? Telephone { get; set; }

    [EmailAddress, StringLength(120)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? NumeroAgrement { get; set; }

    public bool EstActive { get; set; } = true;

    public ICollection<Huissier> Huissiers { get; set; } = new List<Huissier>();

    public bool EstSupprime { get; set; }
}