using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
namespace LMS.ViewModels
{
    public class ReturnViewModel
    {
        [Required]
        public int BorrowRecordId { get; set; }

     
        public string? BookTitle { get; set; }
        
        public string? BorrowerName { get; set; }
     
        public DateTime? BorrowDate { get; set; }
    }

}

