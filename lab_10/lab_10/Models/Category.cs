using System.ComponentModel.DataAnnotations;

namespace lab_10.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana.")]
        [StringLength(50)]
        [Display(Name = "Nazwa Kategorii")]
        public string Name { get; set; }

        public virtual ICollection<Article>? Articles { get; set; }
    }
}