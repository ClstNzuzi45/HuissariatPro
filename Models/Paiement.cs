using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HuissariatPro.Models.LEnum;

namespace HuissariatPro.Models;

public class Paiement
{
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string Reference { get; set; } = ""; // PAY-2026-0001

    public DateTime DatePaiement { get; set; } = DateTime.Today;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Montant { get; set; }

    [StringLength(10)]
    public string Devise { get; set; } = "CDF"; // CDF / USD

    public ModePaiement Mode { get; set; } = ModePaiement.Especes;
    public StatutPaiement Statut { get; set; } = StatutPaiement.Paye;

    [StringLength(300)]
    public string? Libelle { get; set; }

    public int DossierId { get; set; }
    public Dossier? Dossier { get; set; }

    public int? ClientId { get; set; }
    public Client? Client { get; set; }

    public bool EstSupprime { get; set; }
}