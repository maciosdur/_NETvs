using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace lab11.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana.")]
        [StringLength(50)]
        [Display(Name = "Nazwa Kategorii")]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Article>? Articles { get; set; }
    }
}