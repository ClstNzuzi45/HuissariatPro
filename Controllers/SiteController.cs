using HuissariatPro.Data;
using HuissariatPro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HuissariatPro.Controllers;

public class SiteController : Controller
{
    private readonly ApplicationDbContext _context;
    public SiteController(ApplicationDbContext context) => _context = context;

    // ============ ACCUEIL ============
    public async Task<IActionResult> Index()
    {
        var model = new SiteHomeViewModel
        {
            TotalHuissiers = await _context.Huissiers.CountAsync(),
            TotalEtudes = await _context.Etudes.CountAsync(),
            TotalActes = await _context.Actes.CountAsync(),
            TotalDossiers = await _context.Dossiers.CountAsync(),
            DerniersHuissiers = await _context.Huissiers
                .Include(h => h.Etude)
                .OrderByDescending(h => h.DateInscription)
                .Take(6).ToListAsync(),
            DernieresActualites = GetActualites().Take(3).ToList()
        };
        return View(model);
    }

    // ============ ANNUAIRE ============
    public async Task<IActionResult> Huissiers(string? recherche, string? province)
    {
        var q = _context.Huissiers.Include(h => h.Etude)
            .Where(h => h.Statut == Models.LEnum.StatutHuissier.Actif);

        if (!string.IsNullOrWhiteSpace(recherche))
            q = q.Where(h => h.Nom.Contains(recherche) || h.Prenom.Contains(recherche)
                || h.Matricule.Contains(recherche) || h.Ville.Contains(recherche));

        if (!string.IsNullOrWhiteSpace(province))
            q = q.Where(h => h.Province == province);

        var model = new SiteHuissiersViewModel
        {
            Recherche = recherche,
            Province = province,
            Provinces = await _context.Huissiers.Select(h => h.Province).Distinct().OrderBy(p => p).ToListAsync(),
            Huissiers = await q.OrderBy(h => h.Nom).Take(60).ToListAsync(),
            Total = await q.CountAsync()
        };
        return View(model);
    }

    // ============ SERVICES ============
    public IActionResult Services() => View();

    // ============ ACTUALITÉS ============
    public IActionResult Actualites() => View(GetActualites());

    public IActionResult ActualiteDetails(int id)
    {
        var a = GetActualites().FirstOrDefault(x => x.Id == id);
        return a is null ? NotFound() : View(a);
    }

    // ============ CONTACT ============
    [HttpGet]
    public IActionResult Contact() => View(new ContactViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Contact(ContactViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        // TODO : enregistrer en base (table DemandeContact) ou envoyer email
        TempData["Success"] = $"Merci {model.Nom} ! Votre demande « {model.Service} » a été reçue. Un huissier vous rappelle sous 24h au {model.Telephone}.";
        return RedirectToAction(nameof(Contact));
    }

    // ============ VÉRIFIER UN HUISSIER / ACTE ============
    [HttpGet]
    public async Task<IActionResult> Verifier(string? type, string? numero)
    {
        if (string.IsNullOrWhiteSpace(numero)) return View(null);

        if (type == "acte")
        {
            var acte = await _context.Actes
                .Include(a => a.Huissier).Include(a => a.Dossier)
                .FirstOrDefaultAsync(a => a.NumeroActe == numero.Trim().ToUpper());
            ViewBag.Type = "acte";
            ViewBag.Numero = numero;
            ViewBag.Trouve = acte != null;
            return View(acte);
        }

        var huissier = await _context.Huissiers.Include(h => h.Etude)
            .FirstOrDefaultAsync(h => h.Matricule == numero.Trim().ToUpper()
                || h.Nom.Contains(numero) || h.Prenom.Contains(numero));
        ViewBag.Type = "huissier";
        ViewBag.Numero = numero;
        ViewBag.Trouve = huissier != null;
        return View(huissier);
    }

    public IActionResult Error() => View();

    // --- Actualités en dur (à remplacer par une table Actualite plus tard) ---
    private static List<Actualite> GetActualites() => new()
    {
        new() { Id = 1, Titre = "Nouveau tableau national 2026 publié", Categorie = "Communiqué", Icone = "📢", DatePublication = new DateTime(2026,11,12), Contenu = "La liste officielle des huissiers inscrits est disponible. Vérifiez votre inscription dans l'annuaire." },
        new() { Id = 2, Titre = "Formation : Saisie immobilière", Categorie = "Formation", Icone = "⚖️", DatePublication = new DateTime(2026,11,5), Contenu = "Session de formation continue pour tous les huissiers de Kinshasa et des provinces. Inscription à la Représentation." },
        new() { Id = 3, Titre = "Attention aux faux huissiers", Categorie = "Alerte", Icone = "🛡️", DatePublication = new DateTime(2026,10,28), Contenu = "Ne payez jamais sans vérifier le matricule sur ce site. Signalez tout faux acte au +243 900 000 000." },
    };
}