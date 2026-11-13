using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HuissariatPro.Models.LEnum;

namespace HuissariatPro.Models;

public class Dossier
{
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string NumeroDossier { get; set; } = ""; // DOS-2026-0001

    [Required, StringLength(200)]
    public string Objet { get; set; } = "";

    public TypeDossier Type { get; set; }
    public StatutDossier Statut { get; set; } = StatutDossier.EnCours;

    public DateTime DateOuverture { get; set; } = DateTime.Today;
    public DateTime? DateCloture { get; set; }

    [StringLength(100)]
    public string? Tribunal { get; set; }

    [StringLength(50)]
    public string? NumeroRole { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Honoraires { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Frais { get; set; }

    [StringLength(1000)]
    public string? Observations { get; set; }

    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public int HuissierId { get; set; }
    public Huissier? Huissier { get; set; }

    public ICollection<Acte> Actes { get; set; } = new List<Acte>();
    public ICollection<Audience> Audiences { get; set; } = new List<Audience>();
    public ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();

    // Calculés — ne pas mapper en base
    [NotMapped] public decimal TotalDu => Honoraires + Frais;
    [NotMapped] public decimal TotalPaye => Paiements.Where(p => p.Statut != StatutPaiement.Annule).Sum(p => p.Montant);
    [NotMapped] public decimal ResteAPayer => TotalDu - TotalPaye;

    public bool EstSupprime { get; set; }
}