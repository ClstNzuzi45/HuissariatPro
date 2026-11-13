using HuissariatPro.Models;

namespace HuissariatPro.ViewModels;

public class SiteHomeViewModel
{
    public int TotalHuissiers { get; set; }
    public int TotalEtudes { get; set; }
    public int TotalActes { get; set; }
    public int TotalDossiers { get; set; }
    public List<Huissier> DerniersHuissiers { get; set; } = new();
    public List<Actualite> DernieresActualites { get; set; } = new();
}

public class SiteHuissiersViewModel
{
    public string? Recherche { get; set; }
    public string? Province { get; set; }
    public List<string> Provinces { get; set; } = new();
    public List<Huissier> Huissiers { get; set; } = new();
    public int Total { get; set; }
}

public class ContactViewModel
{
    public string Nom { get; set; } = "";
    public string Telephone { get; set; } = "";
    public string? Email { get; set; }
    public string Service { get; set; } = "Signification";
    public string Message { get; set; } = "";
    public string? Province { get; set; }
}

public class Actualite
{
    public int Id { get; set; }
    public string Titre { get; set; } = "";
    public string Categorie { get; set; } = "";
    public string Contenu { get; set; } = "";
    public string Icone { get; set; } = "📢";
    public DateTime DatePublication { get; set; } = DateTime.Today;
}