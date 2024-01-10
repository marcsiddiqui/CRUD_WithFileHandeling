using FirstApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Linq;

namespace FirstApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<string> list = new List<string>();

            list.Add("Samiya");
            list.Add("Sana");
            list.Add("Maryam");
            list.Add("Erum");
            list.Add("Ali Siddiqui");
            list.Add("Summiyal");
            list.Add("Mesum");
            list.Add("Kamil");
            list.Add("Saad");
            list.Add("Usman");
            list.Add("Fakhir");
            list.Add("Anus");
            list.Add("Ali Alam");

            ViewBag.FullName = "Muhammad Arsalan (Sr. .Net Dev.) + Trainer";

            ViewBag.Students = list;

            ViewData.Add("Qualification", "MCS");  // key-value pair

            TempData["Contact"] = "03152258884";

            return View();
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

        public IActionResult StudentList()
        {
            var model = new StudentListModel();

            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData.txt";

            if (System.IO.File.Exists(filePath))
            {
                var allLines = System.IO.File.ReadAllLines(filePath).ToList();
                if (allLines != null && allLines.Any())
                {
                    foreach (var line in allLines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        var lineData = line.Split(",");
                        if (lineData != null && lineData.Count() > 0)
                        {
                            var s_model = new StudentModel();
                            s_model.Id = Convert.ToInt32(lineData[0]);
                            s_model.FullName = lineData[1];
                            s_model.FatherName = lineData[2];
                            s_model.Class = lineData[3];
                            s_model.Section = lineData[4];
                            s_model.Email = lineData[5];

                            model.Students.Add(s_model);
                        }
                    }
                }
            }

            return View(model);
        }

        public IActionResult CreateStudent()
        {




            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentModel model)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData.txt";

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath);
            }

            if (System.IO.File.Exists(filePath))
            {
                var newLineNumber = 1;

                var data2 = System.IO.File.ReadAllLines(filePath);
                if (data2 != null && data2.Count() > 0)
                {
                    var lastline = data2.LastOrDefault();
                    if (lastline != null)
                    {
                        var lastLineData = lastline.Split(",");
                        var lastLineNumber = Convert.ToInt32(lastLineData[0]);
                        newLineNumber = lastLineNumber + 1;
                    }
                }

                var content = System.IO.File.ReadAllText(filePath);

                var studentline = newLineNumber + "," + model.FullName + "," + model.FatherName + "," + model.Class + "," + model.Section + "," + model.Email;

                content = content + "\n" + studentline;

                System.IO.File.WriteAllText(filePath, content);

                return RedirectToAction("StudentList");
            }

            return View();
        }

        public IActionResult EditStudent(int id)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData.txt";
            var allLines = System.IO.File.ReadAllLines(filePath).ToList();

            var s_model = new StudentModel();

            foreach (var n in allLines)
            {
                if (!string.IsNullOrWhiteSpace(n))
                {
                    var lineData = n.Split(",");

                    var lineNumber = Convert.ToInt32(lineData[0]);

                    if (lineNumber == id)
                    {

                        s_model.Id = Convert.ToInt32(lineData[0]);
                        s_model.FullName = lineData[1];
                        s_model.FatherName = lineData[2];
                        s_model.Class = lineData[3];
                        s_model.Section = lineData[4];
                        s_model.Email = lineData[5];
                        break;
                    }

                }
            }


            return View(s_model);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentModel model)
        {
            

            return View(model);
        }
    }
}