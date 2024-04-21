//using Faker;
using Bogus;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Task5.Enums;
using Task5.Interfaces;
using Task5.Models;
using Task5.ViewModels;

namespace Task5.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _dataService;
        public HomeController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetData(DataViewModel dataViewModel)
        {
            var data = _dataService.GenerateUsersInformation(
                    10,
                    dataViewModel.ErrorValue,
                    dataViewModel.Locale,
                    dataViewModel.Seed + dataViewModel.Page);

            foreach (var item in data)
            {
                item.Number += 20 + 10 * (dataViewModel.Page - 1);
            }

            return Json(data);
        }

        [HttpGet]
        public IActionResult GetCsv(DataViewModel dataViewModel)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentUICulture))
            {
                csv.WriteRecords(_dataService.GenerateUsersInformationByPage(
                        dataViewModel.Page,
                        dataViewModel.ErrorValue,
                        dataViewModel.Locale,
                        dataViewModel.Seed
                    ));

                return File(mem.ToArray(), "text/csv", "file.csv");
            }
        }

        [HttpGet]
        public IActionResult Index(DataViewModel dataViewModel)
        {
            int locale;

            if (dataViewModel.Locale == "en")
            {
                locale = 1;
            }
            else if (dataViewModel.Locale == "pl")
            {
                locale = 2;
            }
            else
            {
                locale = 0;
            }


            dataViewModel.Page++;
            Console.WriteLine(dataViewModel.Page);

            return View(new UserInformationViewModel
            {
                Page = dataViewModel.Page,
                Locale = locale,
                Seed = dataViewModel.Seed,
                ErrorValue = (int)(dataViewModel.ErrorValue * 100),
                UsersInformation = _dataService.GenerateUsersInformation(
                    20 + 10 * (dataViewModel.Page - 1), 
                    dataViewModel.ErrorValue, 
                    dataViewModel.Locale,
                    dataViewModel.Seed),
            });
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}