using AutoMapper;
using Demo.BL.Interfaces;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {


        // Loosely Coupling
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        // Tightly Coupling
        //DepartmentRep department;

        public DepartmentController(IDepartmentRep department, IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await department.GetAsync();
            var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {

            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM model)
        {

            try
            {


                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Department>(model);
                    await department.CreateAsync(result);
                    return RedirectToAction("Index");
                }


                return View(model);

                //ModelState.Clear();
                //return View();

            }
            catch (Exception ex)
            {

                // Handle Exception
                TempData["error"] = ex.Message;

                // Log Exception
                using (var stream = new StreamWriter(@"D:\Rounds\Senior Steps\Elasher G1\Back End\.Net Core 6\Session_ 4\Demo\Demo\wwwroot\Logs\" + Guid.NewGuid() + ".txt"))
                {
                    await stream.WriteLineAsync(ex.Message);
                }
            }

            return View();
            //return View(model);
        }


        public async Task<IActionResult> Update(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM model)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var result = mapper.Map<Department>(model);
                    await department.UpdateAsync(result);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {

                // Handle Exception
                TempData["error"] = ex.Message;

                // Log Exception
                using (var stream = new StreamWriter(@"D:\Rounds\Senior Steps\Elasher G1\Back End\.Net Core 6\Session_ 4\Demo\Demo\wwwroot\Logs\" + Guid.NewGuid() + ".txt"))
                {
                    await stream.WriteLineAsync(ex.Message);
                }
            }

            return View();
            //return View(model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentVM model)
        {

            try
            {
                var result = mapper.Map<Department>(model);
                await department.DeleteAsync(result);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                // Handle Exception
                TempData["error"] = ex.Message;

                // Log Exception
                using (var stream = new StreamWriter(@"D:\Rounds\Senior Steps\Elasher G1\Back End\.Net Core 6\Session_ 4\Demo\Demo\wwwroot\Logs\" + Guid.NewGuid() + ".txt"))
                {
                    await stream.WriteLineAsync(ex.Message);
                }
            }

            return View();
            //return View(model);
        }

    }
}
