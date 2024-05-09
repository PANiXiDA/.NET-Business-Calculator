using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.Extensions.Hosting;
using Dal.DbModels;

namespace UI.Areas.Public.Controllers
{
	[Area("Public")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Calculate(int investment, int projectDuration, int monthlyIncome, int monthlyIncomeGrowth, 
			int equipmentCosts, int materiaCosts, int otherOneTimeCosts, int utilityCosts, int rent, int averageSalary, 
			int countEmployees, int costPatentPerYear, string taxSystem)
		{
			int financialResultWithoutIncomeTaxes = investment + (int)(monthlyIncome * Math.Pow((1 + monthlyIncomeGrowth), projectDuration)) -
				(equipmentCosts + materiaCosts + otherOneTimeCosts) - (utilityCosts + rent) * projectDuration - averageSalary * countEmployees * 12;
			int financialResultWithIncomeTaxes = 0;
			if (taxSystem == "income")
			{
				// Ставка налога = 6%
				financialResultWithIncomeTaxes = (int)(financialResultWithoutIncomeTaxes * (1 - 0.06));
			}
			else if (taxSystem == "incomeMinusExpenses")
			{
				// Ставка налога = 15%
				financialResultWithIncomeTaxes = (int)(financialResultWithoutIncomeTaxes * (1 - 0.15));
			}
			else
			{
				// Налог выплачивается патентом одной суммой
				financialResultWithIncomeTaxes = financialResultWithoutIncomeTaxes - costPatentPerYear;
			}
			TempData["FinancialResultWithoutIncomeTaxes"] = financialResultWithoutIncomeTaxes;
			TempData["FinancialResultWithIncomeTaxes"] = financialResultWithIncomeTaxes;
			return RedirectToAction("Index", null, new { area = "Public" });
		}

		[Route("robots.txt")]
		public IActionResult Robots()
		{
			string filename;
			if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
			{
				filename = "robotsDevelopment.txt";
			}
			else
			{
				filename = "robotsProduction.txt";
			}
			
			string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);
			byte[] filedata = System.IO.File.ReadAllBytes(filepath);
			string contentType = "text/plain";

			return File(filedata, contentType);
		}
	}
}