using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDbContext _context; // the db context will be injecsted through constructor

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense()
        {
            var allExpenses = _context.Expenseses.ToList();
            var totalAmt = allExpenses.Sum(x => x.Value);
            ViewBag.Expenseses = totalAmt; // viewbag allows the data transfer from the controller to the view
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null) {
                var expenseInDb = _context.Expenseses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);
            }
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenseses.SingleOrDefault(expense => expense.Id == id);
            _context.Expenseses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expense");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                // create
                _context.Expenseses.Add(model);
            }
            else {
                //edit
                _context.Expenseses.Update(model);
            }
            _context.SaveChanges();
            return RedirectToAction("Expense");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
