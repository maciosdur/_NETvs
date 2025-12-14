using System.ComponentModel.DataAnnotations;
namespace lab09.Models
{
    public class Article
    {
        // Unikalny identyfikator towaru (wymagany dla operacji CRUD)
        public int Id { get; set; }

        // Nazwa towaru
        [Required(ErrorMessage = "Pole Nazwa jest wymagane.")]
        [StringLength(100, ErrorMessage = "Nazwa musi mieć mniej niż 100 znaków.")]
        [Display(Name = "Nazwa Towaru")]
        public string Name { get; set; }

        // Cena towaru
        [Required(ErrorMessage = "Pole Cena jest wymagane.")]
        [Range(0.01, 100000.00, ErrorMessage = "Cena musi być większa od zera.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        // Data ważności (opcjonalna, jeśli towar ma termin ważności)
        // Używamy typu 'DateTime?' aby móc ją oznaczyć jako opcjonalną.
        [Display(Name = "Data Ważności")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        // Kategoria towaru (używamy stworzonego enuma)
        [Required(ErrorMessage = "Wybór Kategorii jest wymagany.")]
        [Display(Name = "Kategoria")]
        public ArticleCategory Category { get; set; }
    }
}
