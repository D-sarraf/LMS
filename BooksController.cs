using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace LMS.Controllers
{
   
    public class BooksController : Controller
    {
        
        private readonly LDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        // Injecting the LibraryContext and Logger to interact with the database and log events.        
        public BooksController(LDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //public IActionResult Create() => View(new Book());
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(
                _context.Categories,
                "CategoryId",
                "Name"
            );

            return View(new Book());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book mybook, IFormFile? CoverImagePath)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                 _context.Categories,
                 "CategoryId",
                    "Name",
                   mybook.CategoryId
                );
                return View(mybook);
            }
            if (mybook.CoverImage != null)
            {
                // 2. Create a unique filename to prevent overwriting files with the same name
                string fileName = Guid.NewGuid().ToString()
                    + Path.GetExtension(mybook.CoverImage.FileName);

                // 3. Define the path to the wwwroot/images folder
                //string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Covers", fileName);
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,
                    "Covers", fileName);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string filepath = Path.Combine(uploadPath, fileName);
                // 4. Save the file to the physical folder
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await mybook.CoverImage.CopyToAsync(stream);
                }

                // 5. Store the relative path string in the database column
                mybook.CoverImagePath = "/Covers/" + fileName;
            }

           
            if (!await IsUniqueBook(mybook))

            {
                ViewBag.Categories = new SelectList(
                    _context.Categories,
                    "CategoryId",
                    "Name",
                    mybook.CategoryId
                );
                return View(mybook);
            }

            mybook.IsAvailable = true;

            _context.Books.Add(mybook);

            if (!await saveChangesAsync())
            {
                ViewBag.Categories = new SelectList(
                    _context.Categories,
                    "CategoryId",
                    "Name",
                    mybook.CategoryId
                );
                return View(mybook);
            }

            TempData["SuccessMessage"] = "Book added successfully!";
            return RedirectToAction(nameof(ViewBooks));
        }
       
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Book mybook)
        //{
        //    if (!ModelState.IsValid)
        //        return View(mybook);
        //    // Duplicate checks
        //    if (await _context.Books.AnyAsync(b => b.ISBN == mybook.ISBN))
        //    {
        //        ModelState.AddModelError("ISBN", "ISBN already exists");
        //        return View(mybook);
        //    }
        //    if (await _context.Books.AnyAsync(b => b.Title == mybook.Title && b.Author == mybook.Author))
        //    {
        //        ModelState.AddModelError(string.Empty, "A book with the same title and author already exists");
        //        return View(mybook);
        //    }
        //    var book = new Book
        //    {
        //        Title = mybook.Title,
        //        Author = mybook.Author,
        //        ISBN = mybook.ISBN,
        //        PublishDate = mybook.PublishDate,
        //        IsAvailable = true
        //    };
        //    _context.Books.Add(book);
        //    await _context.SaveChangesAsync();
        //    TempData["SuccessMessage"] = "Book added successfully!";
        //    return RedirectToAction(nameof(Create));
        //}
        //public async Task<IActionResult> Details()
        //    {
        //    var books = await _context.Books
        //        .AsNoTracking()
        //        .ToListAsync();
        //    return View(books);
        //}
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }


            return View(book);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Details(string searchString)
        //{
        //    var books = from b in _context.Books
        //                select b;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString) || b.ISBN.Contains(searchString));
        //    }
        //    return View(books.ToList());
        //}


        //public async Task<IActionResult> ViewBooks()
        //{
        //    var books = await _context.Books
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return View(books);
        //}
        public async Task<IActionResult> ViewBooks(string searchString)
        {
            var books = _context.Books.Include (b=> b.Category).Include(b => b.BorrowRecords).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                books = books.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.Author.Contains(searchString) ||
                    b.ISBN.Contains(searchString));
            }

            return View(await books.AsNoTracking().ToListAsync());
        }
        

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();
            ViewBag.Categories = new SelectList(
               _context.Categories,
               "CategoryId",
               "Name",
                book.CategoryId  
            );

            return View("Create", book); // same view
            //return View( book);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Book mybook)
        //{
        //    if (id != mybook.BookId)
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //        return View("Create", mybook);

        //    // Duplicate ISBN check (ignore current book)
        //    if (await _context.Books.AnyAsync(b => b.ISBN == mybook.ISBN && b.BookId != mybook.BookId))
        //    {
        //        ModelState.AddModelError("ISBN", "ISBN already exists");
        //        return View("Create", mybook);
        //    }

        //    _context.Update(mybook);
        //    await _context.SaveChangesAsync();
        //    TempData["SuccessMessage"] = "Book updated successfully!";
        //    return RedirectToAction(nameof(ViewBooks));
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book mybook)
        {
            if (id != mybook.BookId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                _context.Categories,
                "CategoryId",
                   "Name",
                  mybook.CategoryId
               );
                return View("Create", mybook);
            }

            if (!await IsUniqueBook(mybook))
            {
                ViewBag.Categories = new SelectList(
                    _context.Categories,
                    "CategoryId",
                    "Name",
                    mybook.CategoryId
                );
                return View("Create", mybook);
            }
            var bookfromDb = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId == id);
            if (bookfromDb == null)
                return NotFound();
            if (mybook.CoverImage != null)
            {
                // 2. Create a unique filename to prevent overwriting files with the same name
                string fileName = Guid.NewGuid().ToString()
                    + Path.GetExtension(mybook.CoverImage.FileName);

                // 3. Define the path to the wwwroot/images folder
                //string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Covers", fileName);
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,
                    "Covers", fileName);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string filepath = Path.Combine(uploadPath, fileName);
                // 4. Save the file
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await mybook.CoverImage.CopyToAsync(stream);
                }

                // 5. Store the relative path string in the database column
                mybook.CoverImagePath = "/Covers/" + fileName;
            }
            else
            {
                mybook.CoverImagePath = bookfromDb.CoverImagePath;
            }

                _context.Update(mybook);

            if (!await saveChangesAsync())
            {
                ViewBag.Categories = new SelectList(
                    _context.Categories,
                    "CategoryId",
                    "Name",
                    mybook.CategoryId
                );
                return View("Create", mybook);
            }

            TempData["SuccessMessage"] = "Book updated successfully!";
            return RedirectToAction(nameof(ViewBooks));
        }

        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return NotFound();
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
               
            }

            TempData["SuccessMessage"] = "Book deleted successfully!";
            return RedirectToAction(nameof(ViewBooks));
        }
       
        private async Task<bool> IsISBNDuplicate(string isbn, int? bookId = null)
        {
            return await _context.Books
                .AnyAsync(b => b.ISBN == isbn && (bookId == null || b.BookId != bookId));
        }
        private async Task<bool> saveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Concurrency error occurred while saving data.");
                return false;
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while saving data.");
                return false;
            }
        }
    
        private async Task<bool> IsUniqueBook(Book myBook)
        {
            bool isbnExists = await _context.Books.AnyAsync(b =>
                b.ISBN == myBook.ISBN &&
                b.BookId != myBook.BookId);

            if (isbnExists)
            {
                ModelState.AddModelError("ISBN", "ISBN already exists");
                return false;
            }

            bool titleAuthorExists = await _context.Books.AnyAsync(b =>
                b.Title == myBook.Title &&
                b.Author == myBook.Author &&
                b.BookId != myBook.BookId);

            if (titleAuthorExists)
            {
                ModelState.AddModelError("", "A book with the same title and author already exists");
                return false;
            }

            return true;
        }

    }
}
