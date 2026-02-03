using System.ComponentModel.DataAnnotations;
namespace LMS.Models
{
    public class Category
    {
       
        public int? CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
