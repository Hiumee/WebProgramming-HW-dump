using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using AspCoreMVCEF.Models;
using AspCoreMVCEF.Data;
using AspCoreMVCEF.DataAbstractionLayer;
using Microsoft.AspNetCore.Http;

namespace AspCoreMVCEF.Controllers
{
    public class MainController : Controller
    {
        private readonly DBWpContext _context;

        public MainController(DBWpContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return View("Login");
            }
            return View("FilterStudents");
        }

        [HttpGet]
        public IActionResult UpdateView(int id)
        {
            DAL dal = new DAL();
            string uname = HttpContext.Session.GetString("username");
            int author_id = dal.GetAuthorByName(uname);

            if (uname == null)
            {
                return View("Error", "Need to be logged in for this");
            }
            else
            {
                Article article = dal.GetArticleById(id, author_id);
                if (article == null)
                {
                    return View("Error", "Article does not exist");
                }

                return View("UpdateView", article);
            }
        }

        [HttpPost]
        public string GetArticles(string filter = null, string category = null, string startDate = null, string endDate = null)
        {
            if (filter == null)
            {
                return "No filter used";
            }

            if (filter == "category")
            {
                return GetArticlesByCategory(category);
            }
            else if (filter == "date")
            {
                return GetArticlesByDate(startDate, endDate);
            }
            else
            {
                return "Invalid filter";
            }
        }

        public string GetArticlesByCategory(string category = "")
        {
            DAL dal = new DAL();
            List<Article> alist = dal.GetArticlesFromCategory(category);

            return JsonSerializer.Serialize(alist);
        }

        public string GetArticlesByDate(string date1 = "", string date2 = "")
        {
            DAL dal = new DAL();
            
            List<Article> alist = dal.GetArticlesByDate(DateTime.Parse(date1), DateTime.Parse(date2));

            return JsonSerializer.Serialize(alist);
        }

        [HttpGet]
        public string GetCategories()
        {
            DAL dal = new DAL();
            List<string> categories = dal.GetCategories();

            return JsonSerializer.Serialize(categories);
        }

        [HttpPost]
        public string AddArticle(string title, string category, string content, string date)
        {
            DAL dal = new DAL();
            int author = 0;
            string uname = HttpContext.Session.GetString("username");

            if (uname != null)
            {
                author = dal.GetAuthorByName(uname);
                dal.AddArticle(title, category, author, content, DateTime.Parse(date));
                return "Success";
            }

            return "Login required";
        }

        [HttpPost]
        public string UpdateArticle(string title, string category, string content, int id)
        {
            DAL dal = new DAL();
            string uname = HttpContext.Session.GetString("username");
            
            if(uname == null)
            {
                return "Login required";
            }

            dal.UpdateArticle(id, title, category, content);

            return "Success";
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            DAL dal = new DAL();
            string uname = dal.CheckCredentials(username, password);


            if (uname != "")
            {
                HttpContext.Session.SetString("username", uname);
                return View("FilterStudents");
            }

            return View("Error", "Wrong credentials");
        }

        public string Session()
        {
            return HttpContext.Session.GetString("username");
        }
    }
}
