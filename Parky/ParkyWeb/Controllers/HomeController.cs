using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModel;
using ParkyWeb.Repository;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository _npRepo;
        private readonly IAccountRepository _accRepo;
        private readonly ITrailRepository _trailRepo;

        public HomeController(ILogger<HomeController> logger, INationalParkRepository npRepo, ITrailRepository trailRepo,
            IAccountRepository accRepo)
        {
            _trailRepo = trailRepo;
            _npRepo = npRepo;
            _logger = logger;
            _accRepo = accRepo;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM listOfParksAndTrails = new IndexVM()
            {
                NationalParkList = (IEnumerable<NationalPark>)await _npRepo.GetAllAsync(SD.NationalParkAPIPath, HttpContext.Session.GetString("JWToken")),
                TrailList = (IEnumerable<Trail>)await _trailRepo.GetAllAsync(SD.TrailAPIPath, HttpContext.Session.GetString("JWToken"))
            };
            return View(listOfParksAndTrails);
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

        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            User objUser = await _accRepo.LoginAsync(SD.AccountAPIPath + "authenticate/", obj);
            if (objUser.Token == null)
            {
                //Login not sucessfull
                return View();
            }

            //To use cookie authentication we have to add Principle Claim and Claim
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));

            //Add identity to new claim principle
            var principal = new ClaimsPrincipal(identity);

            //Automatically sign the user in
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            //Save token in a session for API calls to access
            HttpContext.Session.SetString("JWToken", objUser.Token);

            //Populate alert
            TempData["alert"] = "Welcome " + objUser.Username;

            //Redirect to Home/Index Action
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            User obj = new User();
            return View(obj);//Passing empty model to view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            bool result = await _accRepo.RegisterAsync(SD.AccountAPIPath + "register/", obj);
            if (result == false)
            {
                //Reg not successfull
                return View();
            }

            //Populate alert
            TempData["alert"] = "Registration Succesful ";

            //Redirect to Login Action
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            //Call this to signout our user in web app
            await HttpContext.SignOutAsync();

            //Save token in a session
            HttpContext.Session.SetString("JWToken", "");//Pass empty object to clear session

            //Redirect to Login Action
            return RedirectToAction("Index");
        }
    }
}
