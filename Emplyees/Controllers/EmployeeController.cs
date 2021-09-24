using Emplyees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emplyees.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext context;
        public EmployeeController(EmployeeContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(context.Employees.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employeeModel)
        {
            if (ModelState.IsValid)
            {
                context.Add(employeeModel);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = context.Employees.Find(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = context.Employees
                .FirstOrDefault(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        [HttpPost]
        public IActionResult Edit(Employee employeeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(employeeModel);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = context.Employees
                .FirstOrDefault(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            else
            {
                context.Employees.Remove(employeeModel);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
