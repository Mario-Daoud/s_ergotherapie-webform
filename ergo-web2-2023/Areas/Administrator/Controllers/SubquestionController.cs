using AutoMapper;
using ergo_web2_2023.Areas.Administrator.ViewModels;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories.Interfaces;
using ergo_web2_2023.Services;
using ergo_web2_2023.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Data;

namespace ergo_web2_2023.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class SubquestionController : Controller
    {

        private IBasicOperationService<Subquestion> _subquestionService;
        private IBasicOperationService<Question> _questionService;
        private IQuestionOptionService<QuestionOption> _optionService;
        private readonly IMapper _mapper;

        public SubquestionController(
            IMapper mapper,
            IBasicOperationService<Subquestion> subquestionService,
            IBasicOperationService<Question> questionService,
            IQuestionOptionService<QuestionOption> optionService)
        {
            _mapper = mapper;
            _subquestionService = subquestionService;
            _questionService = questionService;
            _optionService = optionService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListAllSubquestions");
        }

        public async Task<int> GetNextSubquestionId()
        {
            var subquestions = await _subquestionService.GetAll();
            return subquestions.Count() + 1;
        }

        public async Task<IActionResult> ListAllSubquestions()
        {
            var list = await _subquestionService.GetAll();
            List<SubquestionVM> listVM = _mapper.Map<List<SubquestionVM>>(list);
            return View(listVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetOptions(int questionId)
        {
            var options = await _optionService.GetOptionsOfQuestion(questionId);
            return Json(options);
        }

        public async Task<IActionResult> CreateSubquestion()
        {
            var subquestionVM = new SubquestionAddVM()
            {
                Vragen = new SelectList(await _questionService.GetAll(), "QuestionId", "Title"),
                Subvragen = new SelectList(await _questionService.GetAll(), "QuestionId", "Title"),
                Options = new SelectList(new List<SelectListItem>())
            };
            return View(subquestionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubquestion(SubquestionAddVM entityVM)
        {
            if(entityVM.SubQuestionId == entityVM.QuestionId)
            {
                ModelState.AddModelError("Vragen", "Question en subquestion mogen niet dezelfde waarde hebben.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var subquestion = _mapper.Map<Subquestion>(entityVM);
                    // subquestion.SubQuestionId = await GetNextSubquestionId();
                    await _subquestionService.Add(subquestion);
                    return RedirectToAction("ListAllSubquestions");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return await CreateSubquestion();
        }

        public async Task<IActionResult> EditSubquestion(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Subquestion? subquestion = await _subquestionService.FindById(Convert.ToInt16(id));
                var subquestionEdit = _mapper.Map<SubquestionEditVM>(subquestion);
                subquestionEdit.Vragen = new SelectList(await _questionService.GetAll(), "QuestionId", "Title", subquestion.QuestionId);
                subquestionEdit.Subvragen = new SelectList(await _questionService.GetAll(), "QuestionId", "Title", subquestion.SubQuestionId);
                subquestionEdit.Options = new SelectList(new List<SelectListItem>());

                return View(subquestionEdit);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubquestion(SubquestionEditVM entityVM)
        {
            if (entityVM.SubQuestionId == entityVM.QuestionId)
            {
                ModelState.AddModelError("Vragen", "Question en subquestion mogen niet dezelfde waarde hebben.");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var subquestion = _mapper.Map<Subquestion>(entityVM);
                    await _subquestionService.Update(subquestion);
                    return RedirectToAction("ListAllSubquestions");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "call system administrator.");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return await EditSubquestion(entityVM.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return Index();
            }
            else
            {
                Subquestion ? subquestion = await _subquestionService.FindById(Convert.ToInt16(id));

                var subquestionVM = _mapper.Map<SubquestionVM>(subquestion);

                return View(subquestionVM);
            }
        }

        public async Task<IActionResult> DeleteSubquestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                Subquestion subquestion = await _subquestionService.FindById(Convert.ToInt32(id));
                if (subquestion == null)
                {
                    return NotFound();
                }
                await _subquestionService.Delete(subquestion);
                return RedirectToAction("ListAllSubquestions");
            }
            catch (DataException ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to delete data.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Call the administrator");
            }
            return RedirectToAction("ListAllSubquestions");
        }
    }
}
