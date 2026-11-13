using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HuissariatPro.Models.LEnum;
namespace HuissariatPro.Models
{
    public class Actualite
    {
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public string Categorie { get; set; } = "";
        public string Contenu { get; set; } = "";
        public string Icone { get; set; } = "📢";
        public DateTime DatePublication { get; set; } = DateTime.Today;
    }
}
