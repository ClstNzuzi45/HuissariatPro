using System.ComponentModel.DataAnnotations;
using static HuissariatPro.Models.LEnum;

namespace HuissariatPro.Models;

public class Huissier
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    [Display(Name = "N° Matricule")]
    public string Matricule { get; set; } = ""; // Ex : HJ-2026-0001

    [Required, StringLength(80)]
    public string Nom { get; set; } = "";

    [Required, StringLength(80)]
    public string Prenom { get; set; } = "";

    public Sexe Sexe { get; set; }

    public DateTime DateNaissance { get; set; }

    [StringLength(100)]
    public string? LieuNaissance { get; set; }

    [Required, StringLength(100)]
    public string Province { get; set; } = ""; // Kinshasa, Kongo-Central...

    [Required, StringLength(100)]
    public string Ville { get; set; } = "";

    [EmailAddress, StringLength(120)]
    public string? Email { get; set; }

    [Phone, StringLength(30)]
    public string? Telephone { get; set; }

    [StringLength(200)]
    public string? Adresse { get; set; }

    public DateTime DateAssermentation { get; set; } = DateTime.Today;

    public DateTime DateInscription { get; set; } = DateTime.Today;

    public StatutHuissier Statut { get; set; } = StatutHuissier.Actif;

    // Relations
    public int EtudeId { get; set; }
    public Etude? Etude { get; set; }

    public ICollection<Dossier> Dossiers { get; set; } = new List<Dossier>();
    public ICollection<Acte> Actes { get; set; } = new List<Acte>();

    [Display(Name = "Nom complet")]
    public string NomComplet => $"{Prenom} {Nom}";

    public string? UserCreation_Id { get; set; }
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public string? UserModification_Id { get; set; }
    public DateTime? DateModification { get; set; }
    public bool EstSupprime { get; set; }
}