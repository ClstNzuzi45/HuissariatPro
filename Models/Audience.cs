
using System.ComponentModel.DataAnnotations;

namespace HuissariatPro.Models;

public class Audience
{
    public int Id { get; set; }

    public DateTime DateAudience { get; set; } = DateTime.Today.AddDays(7);

    public TimeSpan Heure { get; set; } = new(9, 0, 0);

    public LEnum.TypeAudience Type { get; set; } = LEnum.TypeAudience.Audience;

    [Required, StringLength(100)]
    public string Tribunal { get; set; } = "";

    [StringLength(50)]
    public string? Salle { get; set; }

    [StringLength(500)]
    public string? OrdreDuJour { get; set; }

    [StringLength(1000)]
    public string? Resultat { get; set; }

    public bool EstReporte { get; set; }

    public int DossierId { get; set; }
    public Dossier? Dossier { get; set; }

    public int HuissierId { get; set; }
    public Huissier? Huissier { get; set; }

    public bool EstSupprime { get; set; }
}