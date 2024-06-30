using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext applicationDbContext;

		public CategoryController(ApplicationDbContext applicationDbContext) 
		{ 
			this.applicationDbContext = applicationDbContext;
		}

		public IActionResult Index()
		{
			List<Category> categoryList = applicationDbContext.Categories.ToList();
			return View(categoryList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "The Category Name can not exactly match the Display Order");
			}

			if (ModelState.IsValid)
			{
				applicationDbContext.Categories.Add(category);
				applicationDbContext.SaveChanges();
				TempData["success"] = "Create category successful";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			// 3 ways to fetch data
			// 1 only use with id
			Category? category = applicationDbContext.Categories.Find(id);

			// 2 can use id and other properties such as name, display order, ...
			//Category? category1 = applicationDbContext.Categories.FirstOrDefault(u => u.Id == id);

			// 3 can use id and other properties such as name, display order, ... suitable when calculation or filtering data then get it
			//Category? category2 = applicationDbContext.Categories.Where(u => u.Id == id).FirstOrDefault();

			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				applicationDbContext.Categories.Update(category);
				applicationDbContext.SaveChanges();
				TempData["success"] = "Update category successful";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			// 3 ways to fetch data
			// 1 only use with id
			Category? category = applicationDbContext.Categories.Find(id);

			// 2 can use id and other properties such as name, display order, ...
			//Category? category1 = applicationDbContext.Categories.FirstOrDefault(u => u.Id == id);

			// 3 can use id and other properties such as name, display order, ... suitable when calculation or filtering data then get it
			//Category? category2 = applicationDbContext.Categories.Where(u => u.Id == id).FirstOrDefault();

			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category? category = applicationDbContext.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			applicationDbContext.Categories.Remove(category);
			applicationDbContext.SaveChanges();
			TempData["success"] = "Delete category successful";
			return RedirectToAction("Index");
		}
	}
}
