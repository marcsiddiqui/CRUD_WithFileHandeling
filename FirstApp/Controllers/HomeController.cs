using FirstApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

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

        #region Saving In File With custom working

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
                            s_model.PhoneNumber = lineData[6];

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

                var studentline = newLineNumber + "," + model.FullName + "," + model.FatherName + "," + model.Class + "," + model.Section + "," + model.Email + "," + model.PhoneNumber;

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
                        s_model.PhoneNumber = lineData[6];
                        break;
                    }

                }
            }


            return View(s_model);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentModel model)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData.txt";

            if (!System.IO.File.Exists(filePath))
                return RedirectToAction("StudentList");

            var finalData = "";
            if (System.IO.File.Exists(filePath))
            {
                var allLines = System.IO.File.ReadAllLines(filePath).ToList();
                if (allLines != null && allLines.Any())
                {
                    foreach (var line in allLines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var lineData = line.Split(",");

                            var lineNumber = Convert.ToInt32(lineData[0]);
                            if (lineNumber == model.Id)
                            {
                                var studentline = lineNumber + "," + model.FullName + "," + model.FatherName + "," + model.Class + "," + model.Section + "," + model.Email + "," + model.PhoneNumber;
                                finalData = finalData + "\n" + studentline;
                            }
                            else
                            {
                                finalData = finalData + "\n" + line;
                            }
                        }
                    }

                    System.IO.File.WriteAllText(filePath, finalData);
                    return RedirectToAction("StudentList");
                }
            }

            return View(model);
        }

        public IActionResult DeleteStudent(int id)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData.txt";

            if (!System.IO.File.Exists(filePath))
                return RedirectToAction("StudentList");

            var finalData = "";
            if (System.IO.File.Exists(filePath))
            {
                var allLines = System.IO.File.ReadAllLines(filePath).ToList();
                if (allLines != null && allLines.Any())
                {
                    foreach (var line in allLines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var lineData = line.Split(",");

                            var lineNumber = Convert.ToInt32(lineData[0]);
                            if (lineNumber == id)
                            {
                                continue;
                            }
                            else
                            {
                                finalData = finalData + "\n" + line;
                            }
                        }
                    }

                    System.IO.File.WriteAllText(filePath, finalData);
                    return RedirectToAction("StudentList");
                }
            }

            return RedirectToAction("StudentList");
        }

        #endregion

        #region Saving In File With Newtonsoft.Json package

        public IActionResult StudentList2()
        {
            var model = new StudentListModel();

            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData2.txt";

            if (System.IO.File.Exists(filePath))
            {
                var content = System.IO.File.ReadAllText(filePath);

                var list = !string.IsNullOrWhiteSpace(content) ? JsonConvert.DeserializeObject<List<StudentModel>>(content) : new List<StudentModel>();

                if (list != null && list.Any())
                    model.Students.AddRange(list);
            }

            return View(model);
        }

        public IActionResult CreateStudent2()
        {




            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent2(StudentModel model)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData2.txt";

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath);
            }

            if (System.IO.File.Exists(filePath))
            {
                var content = System.IO.File.ReadAllText(filePath);

                var list = !string.IsNullOrWhiteSpace(content) ? JsonConvert.DeserializeObject<List<StudentModel>>(content) : new List<StudentModel>();

                if (list == null)
                {
                    list = new List<StudentModel>();
                }

                model.Id = list.OrderByDescending(x => x.Id).FirstOrDefault()?.Id + 1 ?? 1;

                list.Add(model);

                if (list != null && list.Any())
                {
                    var newContent = JsonConvert.SerializeObject(list);
                    if (!string.IsNullOrWhiteSpace(newContent))
                    {
                        System.IO.File.WriteAllText(filePath, newContent);

                        return RedirectToAction("StudentList2");
                    }
                }
            }

            return View();
        }

        public IActionResult EditStudent2(int id)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData2.txt";
            var content = System.IO.File.ReadAllText(filePath);

            var list = !string.IsNullOrWhiteSpace(content) ? JsonConvert.DeserializeObject<List<StudentModel>>(content) : new List<StudentModel>();

            if (list != null && list.Any())
            {
                var s_model = list.FirstOrDefault(x => x.Id == id);

                if (s_model != null)
                    return View(s_model);
            }

            return RedirectToAction("StudentList2");
           
        }

        [HttpPost]
        public IActionResult EditStudent2(StudentModel model)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData2.txt";

            if (!System.IO.File.Exists(filePath))
                return RedirectToAction("StudentList");

            if (System.IO.File.Exists(filePath))
            {
                var content = System.IO.File.ReadAllText(filePath);

                var list = !string.IsNullOrWhiteSpace(content) ? JsonConvert.DeserializeObject<List<StudentModel>>(content) : new List<StudentModel>();

                if (list != null && list.Any())
                {
                    list = list.Where(x => x.Id != model.Id).ToList();

                    list.Add(model);

                    list = list.OrderBy(x => x.Id).ToList();
                }

                if (list != null && list.Any())
                {
                    var newContent = JsonConvert.SerializeObject(list);
                    if (!string.IsNullOrWhiteSpace(newContent))
                    {
                        System.IO.File.WriteAllText(filePath, newContent);

                        return RedirectToAction("StudentList2");
                    }
                }
            }

            return View(model);
        }

        public IActionResult DeleteStudent2(int id)
        {
            var filePath = "C:\\Users\\marsalan\\Desktop\\StudentData2.txt";

            if (!System.IO.File.Exists(filePath))
                return RedirectToAction("StudentList2");

            if (System.IO.File.Exists(filePath))
            {
                var content = System.IO.File.ReadAllText(filePath);

                var list = !string.IsNullOrWhiteSpace(content) ? JsonConvert.DeserializeObject<List<StudentModel>>(content) : new List<StudentModel>();

                if (list != null && list.Any())
                {
                    list = list.Where(x => x.Id != id).ToList();
                }

                if (list != null)
                {
                    var newContent = JsonConvert.SerializeObject(list);
                    if (!string.IsNullOrWhiteSpace(newContent))
                    {
                        System.IO.File.WriteAllText(filePath, newContent);

                        return RedirectToAction("StudentList2");
                    }
                }
            }

            return RedirectToAction("StudentList2");
        }

        #endregion
    }
}