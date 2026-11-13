using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HuissariatPro.Models.LEnum;

namespace HuissariatPro.Models;

public class Acte
{
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string NumeroActe { get; set; } = ""; // ACT-2026-0001

    public TypeActe Type { get; set; }
    public StatutActe Statut { get; set; } = StatutActe.Redige;

    public DateTime DateActe { get; set; } = DateTime.Today;
    public DateTime? DateSignification { get; set; }

    [Required, StringLength(200)]
    public string Destinataire { get; set; } = "";

    [StringLength(300)]
    public string? LieuSignification { get; set; }

    [StringLength(2000)]
    public string? Contenu { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Cout { get; set; }

    public int DossierId { get; set; }
    public Dossier? Dossier { get; set; }

    public int HuissierId { get; set; }
    public Huissier? Huissier { get; set; }

    public bool EstSupprime { get; set; }
}