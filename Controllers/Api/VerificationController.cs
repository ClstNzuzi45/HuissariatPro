using HuissariatPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HuissariatPro.Controllers.Api;

[Route("api/verification")]
[ApiController]
public class VerificationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public VerificationController(ApplicationDbContext context) => _context = context;

    [HttpGet("huissier/{numero}")]
    public async Task<IActionResult> Huissier(string numero)
    {
        var h = await _context.Huissiers.Include(x => x.Etude)
            .FirstOrDefaultAsync(x => x.Matricule == numero.ToUpper());
        if (h is null) return NotFound(new { message = "Aucun huissier trouvé." });
        return Ok(new { h.NomComplet, h.Matricule, h.Province, h.Ville, h.Statut, Etude = h.Etude?.Nom, h.Telephone });
    }

    [HttpGet("acte/{numero}")]
    public async Task<IActionResult> Acte(string numero)
    {
        var a = await _context.Actes.Include(x => x.Huissier).Include(x => x.Dossier)
            .FirstOrDefaultAsync(x => x.NumeroActe == numero.ToUpper());
        if (a is null) return NotFound(new { message = "Aucun acte trouvé." });
        return Ok(new { a.NumeroActe, a.Type, a.Statut, a.DateActe, a.DateSignification, a.Destinataire, Huissier = a.Huissier!.NomComplet });
    }
}