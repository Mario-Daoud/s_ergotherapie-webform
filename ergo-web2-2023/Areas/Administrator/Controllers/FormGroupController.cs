using AutoMapper;
using ergo_web2_2023.Areas.Administrator.ViewModels;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories.Interfaces;
using ergo_web2_2023.Services;
using ergo_web2_2023.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ergo_web2_2023.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class FormGroupController : Controller
    {

        private IBasicOperationService<FormGroup> _formGroupService;
        private IFormQuestionService<FormQuestion> _formQuestionService;
        private IBasicOperationService<FormQuestion> _formQuestionBasicService;
        private readonly IMapper _mapper;

        public FormGroupController(
            IMapper mapper,
            IBasicOperationService<FormGroup> formGroupService,
            IFormQuestionService<FormQuestion> formQuestionService,
            IBasicOperationService<FormQuestion> formQuestionBasicService)
        {
            _mapper = mapper;
            _formGroupService = formGroupService;
            _formQuestionService = formQuestionService;
            _formQuestionBasicService = formQuestionBasicService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CreateFormGroup()
        {
            FormGroupVM formGroupVM = new FormGroupVM();
            return View(formGroupVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFormGroup(FormGroupVM entityVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var formGroup = _mapper.Map<FormGroup>(entityVm);
                    await _formGroupService.Add(formGroup);
                    return RedirectToAction("ListAllGroups");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("ListAllGroups");
        }

        public async Task<IActionResult> ListAllGroups()
        {
            var list = await _formGroupService.GetAll();
            List<FormGroupVM> listVM = _mapper.Map<List<FormGroupVM>>(list);
            return View(listVM);
        }

        public async Task<IActionResult> EditGroup(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                FormGroup? group = await _formGroupService.FindById(Convert.ToInt16(id));
                var formGroup = _mapper.Map<FormGroupVM>(group);
                return View(formGroup);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(FormGroupVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var group = _mapper.Map<FormGroup>(entityVM);
                    await _formGroupService.Update(group);
                    return RedirectToAction("ListAllGroups");
                }
            }
            catch (DataException ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError(ex.Message, "call system administrator.");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return View(entityVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return Index();
            }
            else
            {
                FormGroup? formGroup = await _formGroupService.FindById(Convert.ToInt16(id));
                var formGroupVM = _mapper.Map<FormGroupVM>(formGroup);

                return View(formGroupVM);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                FormGroup formGroup = await _formGroupService.FindById(Convert.ToInt32(id));
                var formQuestions = await _formQuestionService.GetFormQuestionsOfGroup(Convert.ToInt32(id));
                if (formQuestions != null)
                {
                    foreach (FormQuestion formQuestion in formQuestions)
                    {
                        await _formQuestionBasicService.Delete(formQuestion);
                    }
                }
                if (formGroup == null)
                {
                    return NotFound();
                }
                await _formGroupService.Delete(formGroup);
                return RedirectToAction("ListAllGroups");
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to delete data.");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Call the administrator");
            }
            return RedirectToAction("ListAllGroups");
        }
    }
}
