using AutoMapper;
using BLL.interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Project_PL_.Models;

namespace Project_PL_.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentRepository departmentRepository , IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var dept = await _departmentRepository.GetAllAsync();
            var result = _mapper.Map < IEnumerable<DepartmentViewModel>>(dept);

            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            var dept = _mapper.Map<Department>(model);

            if (ModelState.IsValid)
            {
                var count = await _departmentRepository.AddAsync(dept);
                if(count > 0)
                {
                    TempData["Create"] = "Create Succssefully";
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty,"InVailid");
            return View(model);
        }
        public async Task<IActionResult> Details([FromRoute]int ? id , string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var dept = await _departmentRepository.GetIdAsync(id);
            var result = _mapper.Map<DepartmentViewModel>(dept);
            if (dept is null)
                return NotFound();
            return View(viewName, result);
        }
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(DepartmentViewModel model)
        {
            var dept =  _mapper.Map<Department>(model);

            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Update(dept);
                if(Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "InValid");
            return View(model);
        }

        public Task<IActionResult> Delete([FromRoute] int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(DepartmentViewModel model)
        {
            var dept = _mapper.Map<Department>(model);

            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Delete(dept);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "InValid");
            return View(model);
        }
    }
}
