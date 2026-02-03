using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace LMS.Models
{
    public class Book
    {
        
        public int BookId { get; set; }
        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "The Author field is required.")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
        public string? Author { get; set; }
        [Required(ErrorMessage = "The ISBN field is required.")]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "ISBN must be in the format XXX-XXXXXXXXXX.")]
        public string? ISBN { get; set; }
        [Required(ErrorMessage = "The Published Date field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        public DateTime PublishDate { get; set; }
        public string? CoverImagePath { get; set; }
        [NotMapped]
        public IFormFile? CoverImage { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [BindNever]
        [Display(Name = "Available")]
        public bool IsAvaiable { get; set; } = true;
        // Navigation Property
      
        public ICollection<BorrowRecord>? BorrowRecords { get; set; }
        public DateTime PublishedDate { get; internal set; }
        public bool IsAvailable { get; internal set; }
    }
}
