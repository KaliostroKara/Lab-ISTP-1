using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_ISTP_1.Models;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace Lab_ISTP_1.Controllers
{

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

        
        public UsersController(ApplicationDbContext context, [NotNull] IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}
        //public async Task<IActionResult> Index()
        //{
           // var viewModel = new ViewModel()
          //  {
             //   Users = _context.Users.ToList(),
            //    Categories = _context.Categories.ToList(),
           //     Products = _context.Products.ToList(),
           //     Orders = _context.Orders.ToList()
          //  };

          //  return View(viewModel);
       // }



        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult ListOfUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Email,Password")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Email,Password")] Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserId))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }


        public ActionResult Login(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == login && u.Password == password);
            if (user != null)
            {
                // Проверяем значение столбца Roles
                if (user.Roles == 1)
                {
                    // Если пользователь найден и его роль равна 1, то авторизуем его и перенаправляем на главную страницу
                    //FormsAuthentication.SetAuthCookie(users.Name, false);
                    return RedirectToAction("MainPage", "Home");
                }
                else if (user.Roles == 2)
                {
                    // Если пользователь найден и его роль равна 2, то перенаправляем на страницу "Stranica", "Predsta"
                    return RedirectToAction("Index", "Products");
                }
            }

            // Если пользователь не найден или его роль не соответствует ожидаемой, то выводим сообщение об ошибке
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View("Index");
        }

        [HttpPost]
        public ActionResult Register(string login, string email, string password)
        {
            // проверяем, что такой пользователь еще не зарегистрирован
            if (_context.Users.Any(u => u.Name == login || u.Email == email))
            {
                ModelState.AddModelError("", "User with such login or email already exists.");
                return View("Index");
            }

            // создаем нового пользователя и добавляем его в базу данных
            var user = new Users { Name = login, Email = email, Password = password, Roles = 1 };
            _context.Users.Add(user);
            _context.SaveChanges();

            // перенаправляем пользователя на страницу авторизации после регистрации
            return RedirectToAction("Login", "Users");
        }



    }
}
