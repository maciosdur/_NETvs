using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; // Potrzebne do IFormFile

namespace lab_10.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole Nazwa jest wymagane.")]
        [StringLength(100)]
        [Display(Name = "Nazwa Towaru")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole Cena jest wymagane.")]
        [Range(0.01, 100000.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Data Ważności")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        // --- NOWE POLA DLA OBRAZKA ---

        [Display(Name = "Obrazek")]
        public string? ImagePath { get; set; } // Ścieżka do pliku (np. "upload/foto1.jpg") zapisana w bazie

        [NotMapped] // To pole NIE trafi do bazy danych
        [Display(Name = "Wgraj obrazek")]
        public IFormFile? FormFile { get; set; } // Ten obiekt służy do odebrania pliku z formularza

        // --- RELACJA Z KATEGORIĄ ---

        [Required(ErrorMessage = "Kategoria jest wymagana.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; } // Klucz obcy (Foreign Key)

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; } // Właściwość nawigacyjna
    }
}