using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace lab11.Models
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


        [Display(Name = "Obrazek")]
        public string? ImagePath { get; set; }

        [NotMapped] 
        [Display(Name = "Wgraj obrazek")]
        [JsonIgnore]
        public IFormFile? FormFile { get; set; } 

        // --- RELACJA Z KATEGORIĄ ---

        [Required(ErrorMessage = "Kategoria jest wymagana.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; } 

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; } 
    }
}