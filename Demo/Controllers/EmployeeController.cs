using AutoMapper;
using Demo.APIs.Controllers;
using Demo.BL.DTOs;
using Demo.BL.Interfaces;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Formatting;

namespace Demo.PL.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {

        #region Fields

        private readonly IDepartmentRep department;
        private readonly IMapper mapper;
        private readonly ICountry country;
        private readonly ICity city;
        private readonly IDistrict district;
        private readonly IEmployeeRep employee;

        #endregion


        #region Ctor

        public EmployeeController(IEmployeeRep employee, IDepartmentRep department, IMapper mapper,
            ICountry country, ICity city, IDistrict district)
        {
            this.employee = employee;
            this.department = department;
            this.mapper = mapper;
            this.country = country;
            this.city = city;
            this.district = district;
        }

        #endregion


        #region Actions

        public async Task<IActionResult> Index(EmployeeFilter filter)
        {


            //var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:7299");
            //var album = new Album() { ID = "Test001", Name = "DC" };
            //HttpResponseMessage response = client.PostAsync("/api/TestTwo", album, new XmlMediaTypeFormatter()).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var products = response.Content.ReadAsStringAsync().Result;
            //}
            //else
            //{
            //    throw new Exception("Exception");
            //}



            IEnumerable<EmployeeVM> result;

            if (filter.DateFrom == null && filter.DateTo == null)
            {
                var data = await employee.GetAsync(emp => (emp.IsActive == true) && (emp.IsDeleted == false));
                result = mapper.Map<IEnumerable<EmployeeVM>>(data);
            }
            else
            {
                var data = await employee.GetAsync(emp =>
                                               (emp.IsActive == true)
                                            && (emp.IsDeleted == false)
                                            && (emp.CreatedOn.Value.Date >= filter.DateFrom)
                                            && (emp.CreatedOn.Value.Date <= filter.DateTo));

                result = mapper.Map<IEnumerable<EmployeeVM>>(data);
            }

            return View(result);

        }

        public async Task<IActionResult> Details(int Id, bool IsActive)
        {
            EmployeeVM result;
            Employee data;

            if (IsActive == true)
            {
                data = await employee.GetByIdAsync(emp =>
                                           emp.IsActive == true
                                        && emp.IsDeleted == false
                                        && emp.Id == Id);
            }
            else
            {
                data = await employee.GetByIdAsync(emp =>
                           emp.IsActive == false
                        && emp.IsDeleted == true
                        && emp.Id == Id);
            }


            result = mapper.Map<EmployeeVM>(data);

            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", result.DepartmentId);

            return View(result);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");
            ViewBag.CountryList = new SelectList(await country.GetAsync(a => a.Id != null), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(model);
                    await employee.CreateAsync(result);
                    return RedirectToAction("Index");
                }


                // Log Exception
                using (var stream = new StreamWriter(@"D:\Rounds\Senior Steps\Elasher G1\Back End\.Net Core 6\Session_ 4\Demo\Demo\wwwroot\Logs\" + Guid.NewGuid() + ".txt"))
                {
                    await stream.WriteLineAsync("Validation Error");
                }

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


            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");
            return View(model);
        }


        public async Task<IActionResult> Update(int id)
        {
            var data = await employee.GetByIdAsync(emp =>
                                                       emp.IsActive == true
                                                    && emp.IsDeleted == false
                                                    && emp.Id == id);

            var result = mapper.Map<EmployeeVM>(data);

            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", result.DepartmentId);


            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(model);
                    await employee.UpdateAsync(result);
                    return RedirectToAction("Index");
                }

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


            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", model.DepartmentId);
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var data = await employee.GetByIdAsync(emp =>
                                                       emp.IsActive == true
                                                    && emp.IsDeleted == false
                                                    && emp.Id == id);

            var result = mapper.Map<EmployeeVM>(data);

            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", result.DepartmentId);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM model)
        {
            try
            {
                var result = mapper.Map<Employee>(model);
                await employee.DeleteAsync(result);
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

            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", model.DepartmentId);
            return View(model);
        }


        public async Task<IActionResult> Restore()
        {
            var data = await employee.GetAsync(emp => (emp.IsActive == false) && (emp.IsDeleted == true));
            var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            try
            {
                if (id == null)
                    throw new Exception("Data null here");

                await employee.ReActivate(id);
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

            return LocalRedirect("/Employee/Restore");
        }

        #endregion


        #region Ajax Call


        // Get All Cities Based On Country Id

        [HttpPost]
        public async Task<JsonResult> GetCitiesByCountryId(int CntryId)
        {
            var data = await city.GetAsync(x => x.CountryId == CntryId);
            return Json(data);
        }


        // Get All Districts Based On City Id

        [HttpPost]
        public async Task<JsonResult> GetDistrictsByCityId(int CtyId)
        {
            var data = await district.GetAsync(x => x.CityId == CtyId);
            return Json(data);
        }


        #endregion



    }
}
