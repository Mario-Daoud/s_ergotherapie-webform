using AutoMapper;
using ergo_web2_2023.Areas.Administrator.ViewModels;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Models.Enums;
using ergo_web2_2023.Repositories.Interfaces;
using ergo_web2_2023.Services;
using ergo_web2_2023.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace ergo_web2_2023.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class QuestionController : Controller
    {

        private IBasicOperationService<Question> _questionService;
        private IQuestionOptionService<QuestionOption> _questionOptionService;
        private readonly IMapper _mapper;

        public QuestionController(IMapper mapper, IBasicOperationService<Question> questionService, IQuestionOptionService<QuestionOption> questionOptionService)
        {
            _mapper = mapper;
            _questionService = questionService;
            _questionOptionService = questionOptionService;

        }

        public IActionResult Index()
        {
            return RedirectToAction("ListAllQuestions");
        }

        public async Task<int> GetNextQuestionId()
        {
            var questions = await _questionService.GetAll();
            return questions.Count() + 1;
        }

        public async Task<IActionResult> ListAllQuestions()
        {
            var list = await _questionService.GetAll();
            List<QuestionVM> listVM = _mapper.Map<List<QuestionVM>>(list);
            return View(listVM);
        }

        public async Task<IActionResult> CreateQuestion()
        {
            QuestionVM questionVM = new QuestionVM();
            return View(questionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(QuestionVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var question = _mapper.Map<Question>(entityVM);
                    //question.QuestionId = await GetNextQuestionId();
                    await _questionService.Add(question);
                    return RedirectToAction("ListAllQuestions");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(entityVM);
        }

        public async Task<IActionResult> EditQuestion(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Question? question = await _questionService.FindById(Convert.ToInt16(id));
                var questionEdit = _mapper.Map<QuestionEditVM>(question);
                return View(questionEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(QuestionEditVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var question = _mapper.Map<Question>(entityVM);
                    await _questionService.Update(question);
                    return RedirectToAction("ListAllQuestions");
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

        public async Task<IActionResult> AddOptions(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Question? question = await _questionService.FindById(Convert.ToInt16(id));
                OptionsVM options = new OptionsVM();
                options.Title = question.Title;
                options.Options = question.QuestionOptions;
                options.QuestionId = id;
                return View(options);
            }
        }

        public async Task<IActionResult> InsertNewOption(int questionId, string optionText)
        {
            try
            {
                QuestionOption option = new QuestionOption();
                option.Optiontext = optionText;
                option.QuestionId = questionId;
                IEnumerable<QuestionOption> options = await _questionOptionService.GetOptionsOfQuestion(questionId);
                option.Question = await _questionService.FindById(questionId);
                _questionOptionService.CreateOption(option);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction("AddOptions", new { id = questionId });
        }

        public async Task<IActionResult> DeleteOption(int optionId, int questionId)
        {
            try
            {
                QuestionOption option = await _questionOptionService.FindById(optionId);
                _questionOptionService.Delete(option);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction("AddOptions", new { id = questionId });
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

                Question? question = await _questionService.FindById(Convert.ToInt16(id));
                var questionVM = _mapper.Map<QuestionVM>(question);

                return View(questionVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                Question question = await _questionService.FindById(Convert.ToInt32(id));
                if (question == null)
                {
                    return NotFound();
                }
                await _questionService.Delete(question);
            }
            catch (DataException ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to delete data.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete this question as it's being used in a form.");
            }
            return RedirectToAction("ListAllQuestions");
        }
    }
}