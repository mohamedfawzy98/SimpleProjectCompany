using AutoMapper;
using BLL.interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Project_PL_.Models;
using Microsoft.EntityFrameworkCore;
using Project_PL_.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Project_PL_.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeRepository employeRepository
                                  , IMapper mapper
                                  , IDepartmentRepository departmentRepository)
        {
            _employeRepository = employeRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index(string InputSearch)
        {
            var emp = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(InputSearch))
            {
                emp = await _employeRepository.GetAllAsync();
            }
            else
            {
                emp = await _employeRepository.GetNameAsync(InputSearch);
            }
            var Result = _mapper.Map<IEnumerable<EmployeeViewModel>>(emp);

            return View(Result);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["department"] = await _departmentRepository.GetAllAsync();
            return View();
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            model.ImageName = DocumentSetting.UploadFile(model.Image, "Image");

            var result = _mapper.Map<Employee>(model);
            if (ModelState.IsValid)
            {

                var count = await _employeRepository.AddAsync(result);
                if (count > 0)
                {
                    TempData["create"] = "Create is Succsefully";

                }
                else
                {
                    TempData["create"] = "Create is Not Succsefully";

                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid");
            return View(model);
        }
        public async Task<IActionResult> Details([FromRoute] int? id, string viewName)
        {
            if (id is null) return BadRequest();

            var emp = await _employeRepository.GetIdAsync(id);

            if (emp is null) return NotFound();
            var result = _mapper.Map<EmployeeViewModel>(emp);

            return View(viewName, result);
        }
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            ViewData["department"] = await _departmentRepository.GetAllAsync();
            return await Details(id, "Edit");
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(EmployeeViewModel model)
        {
            if (model.ImageName is not null)
            {
                DocumentSetting.DeleteFile(model.ImageName, "Image");
            }
            if (model.Image is not null)
            {
                model.ImageName = DocumentSetting.UploadFile(model.Image, "Image");

            }
            var result = _mapper.Map<Employee>(model);
            if (ModelState.IsValid)
            {
                var count = _employeRepository.Update(result);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Invaild");
            return View(model);
        }
        public Task<IActionResult> Delete([FromRoute] int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(EmployeeViewModel model)
        {
            var result = _mapper.Map<Employee>(model);
            if (ModelState.IsValid)
            {
                var count = _employeRepository.Delete(result);
                if (count > 0)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "Image");
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Invaild");
            return View(model);
        }
    }
}
