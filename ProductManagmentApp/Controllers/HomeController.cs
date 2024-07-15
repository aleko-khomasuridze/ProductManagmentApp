using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagmentApp.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagmentApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ProductContext _productContext;

		public HomeController(ProductContext productContext)
		{
			_productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
		}

		public IActionResult Index(string id)
		{
			var filters = new Filters(id);
			ViewBag.Filters = filters;

			ViewBag.Categories = _productContext.Categories.ToList() ?? new List<Category>();
			ViewBag.ExpirationDateFilters = Filters.ExpirationDateValues;

			IQueryable<Product> productsQuery = _productContext.Products.Include(p => p.Category);

			// Apply filters based on expiration date
			if (filters.HasExpirationDate)
			{
				productsQuery = productsQuery.Where(p => p.ExpirationDate < DateTime.Today);
			}

			// Apply filters based on category
			if (filters.HasCategory)
			{
				productsQuery = productsQuery.Where(p => p.CategoryId == filters.CategoryId);
			}

			// Apply filters based on expired status
			if (filters.HasExpirationDate)
			{
				productsQuery = productsQuery.Where(p => p.IsExpired);
			}

			var products = productsQuery.OrderBy(p => p.Price).ToList();

			return View(products);
		}

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Categories = _productContext.Categories.ToList() ?? new List<Category>();
			var product = new Product { CategoryId = "de"};
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(Product product)
		{
			if (ModelState.IsValid)
			{
				_productContext.Products.Add(product);
				_productContext.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				// Re-populate ViewBag.Categories in case of an invalid model state
				var categories = _productContext.Categories.ToList() ?? new List<Category>();
				ViewBag.Categories = new SelectList(categories, "Id", "Name");
				return View(product);
			}
		}

		[HttpPost]
		public IActionResult Filter(string[] filter)
		{
			string id = string.Join("-", filter);
			return RedirectToAction("Index", new { Id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productContext.Products.FirstOrDefaultAsync(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = await _productContext.Products.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			_productContext.Products.Remove(product);
			await _productContext.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
