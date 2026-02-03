//using LMS.Data;
//using LMS.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LMS.Controllers
//{
//    public class BorrowController : Controller
//    {
//        private readonly LDbContext _context;

//        public BorrowController(LDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Create(int bookId)
//        {
//            var book = await _context.Books.FindAsync(bookId);

//            if (book == null || !book.IsAvailable)
//                return NotFound();

//            var borrow = new BorrowRecord
//            {
//                BookId = bookId
//            };

//            return View(borrow);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(BorrowRecord record)
//        {
//            if (!ModelState.IsValid)
//                return View(record);

//            var book = await _context.Books.FindAsync(record.BookId);
//            if (book == null)
//                return NotFound();

//            book.IsAvailable = false;
//            record.BorrowDate = DateTime.UtcNow;

//            _context.BorrowRecords.Add(record);
//            _context.Books.Update(book);

//            await _context.SaveChangesAsync();

//            TempData["SuccessMessage"] = "Book borrowed successfully!";
//            return RedirectToAction("ViewBooks", "Books");
//        }


//        public async Task<IActionResult> Return(int id)
//        {
//            var record = await _context.BorrowRecords
//                .Include(b => b.Book)
//                .FirstOrDefaultAsync(b => b.BorrowRecordId == id);

//            if (record == null || record.ReturnDate != null)
//                return NotFound();

//            return View(record);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [ActionName("Return")]
//        public async Task<IActionResult> ReturnPost(int id)
//        {
//            var record = await _context.BorrowRecords
//                .Include(b => b.Book)
//                .FirstOrDefaultAsync(b => b.BorrowRecordId == id);

//            if (record == null)
//                return NotFound();

//            record.ReturnDate = DateTime.UtcNow;
//            record.Book.IsAvailable = true;

//            await _context.SaveChangesAsync();

//            TempData["SuccessMessage"] = "Book returned successfully!";
//            return RedirectToAction("ViewBooks", "Books");
//        }
//    }
//}
using LMS.Data;
using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class BorrowController : Controller
    {
        private readonly LDbContext _context;

        public BorrowController(LDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Borrowit(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book == null || !book.IsAvailable)
                return NotFound();

            var vm = new BorrowViewModel
            {
                BookId = book.BookId,
                BookTitle = book.Title
            };

            return View(vm); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrowit(BorrowViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var book = await _context.Books.FindAsync(vm.BookId);
            if (book == null || !book.IsAvailable)
                return NotFound();

            var record = new BorrowRecord
            {
                BookId = vm.BookId,
                BorrowerName = vm.BorrowerName,
                BorrowerEmail = vm.BorrowerEmail,
                Phone = vm.Phone,
                BorrowDate = DateTime.UtcNow
            };

            book.IsAvailable = false;

            _context.BorrowRecords.Add(record);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Book borrowed successfully!";
            return RedirectToAction("ViewBooks", "Books");
        }

        public async Task<IActionResult> Return(int id)
        {
            var record = await _context.BorrowRecords
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.BorrowRecordId == id && r.ReturnDate == null);

            if (record == null)
                return NotFound();

            var vm = new ReturnViewModel
            {
                BorrowRecordId = record.BorrowRecordId,
                BookTitle = record.Book.Title,
                BorrowerName = record.BorrowerName,
                BorrowDate = record.BorrowDate
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(ReturnViewModel vm)
        {
            var record = await _context.BorrowRecords
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.BorrowRecordId == vm.BorrowRecordId);

            if (record == null)
                return NotFound();

            record.ReturnDate = DateTime.UtcNow;
            record.Book.IsAvailable = true;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Book returned successfully!";
            return RedirectToAction("ViewBooks", "Books");
       
        }
    }
}
