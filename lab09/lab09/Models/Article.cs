using System.ComponentModel.DataAnnotations;
namespace lab09.Models
{
    public class Article
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Pole Nazwa jest wymagane.")]
        [StringLength(100, ErrorMessage = "Nazwa musi mieć mniej niż 100 znaków.")]
        [Display(Name = "Nazwa Towaru")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole Cena jest wymagane.")]
        [Range(0.01, 100000.00, ErrorMessage = "Cena musi być większa od zera.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        [Display(Name = "Data Ważności")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "Wybór Kategorii jest wymagany.")]
        [Display(Name = "Kategoria")]
        public ArticleCategory Category { get; set; }
    }
}
