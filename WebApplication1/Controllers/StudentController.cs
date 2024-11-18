using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbcontext dbcontext;
        public StudentController(AppDbcontext dbcontext) { 
            this.dbcontext=dbcontext;
        }  
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Add(AddViewModel viewModel)
        {
            var student = new Student() { 
                Name = viewModel.Name,
                email= viewModel.email,
                phone_number= viewModel.phone_number
            };
            await dbcontext.Students.AddAsync(student);
            await dbcontext.SaveChangesAsync();

            return View("Add");
        }
        [HttpGet]

        public async Task<IActionResult> List()
        {
            var students = await dbcontext.Students.ToListAsync(); 
          //  Console.WriteLine(students.Count+" count");
          //  Console.WriteLine(students[0]);
            return View(students);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(Guid Id)
        {
            var student = await dbcontext.Students.FindAsync(Id);

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student ViewModel)
        {
            Console.WriteLine(ViewModel.Id);
            var student= await dbcontext.Students.FindAsync(ViewModel.Id);
            Console.WriteLine(student.Id);
            if(student is not null)
            {
                student.Name = ViewModel.Name;
                student.email = ViewModel.email;
                student.phone_number = ViewModel.phone_number;
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("List","Student");
            }
            else
            {
                return View("Edit");
               
            }
        }
        [HttpPost]

        public async Task<IActionResult> Delete(Student ViewModel)
        {
            var student = await dbcontext.Students.FindAsync(ViewModel.Id);
            if(student is not null)
            {
                 dbcontext.Students.Remove(student);
                await dbcontext.SaveChangesAsync();
               // return View("Edit");
            }
            return RedirectToAction("List", "Student");
        }

    }
}
