using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class BorrowRecord
    {
        [Key]
        public int BorrowRecordId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required(ErrorMessage = "PLease Enter Borrower Name  ")]
        public string? BorrowerName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Please enter a Email Address")]
        public string? BorrowerEmail { get; set; }
        [Required]
        [Phone(ErrorMessage = "Please enter a Valid Phone Number")]
        public string? Phone { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? BorrowDate { get; set; } = DateTime.UtcNow;
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }
        // Navigation Properties
        [BindNever]
        public Book?  Book { get; set; }
    }
}