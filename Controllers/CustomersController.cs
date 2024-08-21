using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Data;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(LibraryDbContext context, ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchName, string searchEmail, string searchPhone)
        {
            var filterModel = new CustomerFilterViewModel
            {
                SearchName = searchName,
                SearchEmail = searchEmail,
                SearchPhone = searchPhone
            };

            var customers = from c in _context.Customers
                            select c;

            if (!string.IsNullOrEmpty(searchName))
            {
                customers = customers.Where(c => c.Name.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchEmail))
            {
                customers = customers.Where(c => c.Email.Contains(searchEmail));
            }

            if (!string.IsNullOrEmpty(searchPhone))
            {
                customers = customers.Where(c => c.PhoneNumber.Contains(searchPhone));
            }

            filterModel.Customers = await customers.ToListAsync();

            return View(filterModel);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers
                .Include(c => c.BookLoans)
                .ThenInclude(bl => bl.Book)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null) return NotFound();

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,DateOfBirth")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Redirect to Index page after creation
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the customer.");
                    ModelState.AddModelError("", "An error occurred while creating the customer. Please try again.");
                }
            }
            else
            {
                // Log or debug validation errors if the model state is not valid
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        _logger.LogError(error.ErrorMessage); // Use logger to log errors
                    }
                }
            }

            return View(customer); // Return to the Create view if model state is invalid or an error occurred
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber,DateOfBirth")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        public async Task<IActionResult> BorrowBook(int customerId, int bookId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            var book = await _context.Books.FindAsync(bookId);

            if (customer == null || book == null) return NotFound();

            var bookLoan = new BookLoan
            {
                CustomerId = customerId,
                BookId = bookId,
                LoanDate = DateTime.Now
            };

            _context.BookLoans.Add(bookLoan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = customerId });
        }
    }
}
