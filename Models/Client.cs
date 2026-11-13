using System.ComponentModel.DataAnnotations;
using static HuissariatPro.Models.LEnum;

namespace HuissariatPro.Models;

public class Client
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nom { get; set; } = "";

    [StringLength(100)]
    public string? Prenom { get; set; }

    public TypeClient Type { get; set; } = TypeClient.Particulier;

    [EmailAddress, StringLength(120)]
    public string? Email { get; set; }

    [Phone, StringLength(30)]
    public string? Telephone { get; set; }

    [StringLength(200)]
    public string? Adresse { get; set; }

    [StringLength(50)]
    public string? NumeroPiece { get; set; } // CNI / RCCM

    public bool EstActif { get; set; } = true;

    public ICollection<Dossier> Dossiers { get; set; } = new List<Dossier>();

    public bool EstSupprime { get; set; }

    public string NomComplet => $"{Prenom} {Nom}".Trim();
}