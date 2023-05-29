using AutoMapper;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Services;
using ergo_web2_2023.Areas.Administrator.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Security.Cryptography;
using ergo_web2_2023.Services.Interfaces;
using ergo_web2_2023.Repositories.Interfaces;
using Newtonsoft.Json.Linq;

namespace ergo_web2_2023.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class FormController : Controller
    {

        private IBasicOperationService<Form> _formService;
        private IBasicOperationService<FormQuestion> _formQuestionBasicService;
        private IFormQuestionService<FormQuestion> _formQuestionService;
        private IBasicOperationService<Question> _questionService;
        private ISubquestionService<Subquestion> _subquestionService;
        private IBasicOperationService<FormGroup> _formGroupService;
        private readonly IMapper _mapper;

        public FormController(IMapper mapper, IBasicOperationService<Form> formService,
            IFormQuestionService<FormQuestion> formQuestionService,
            IBasicOperationService<FormQuestion> formQuestionBasicService,
            IBasicOperationService<Question> questionService,
            ISubquestionService<Subquestion> subquestionService,
            IBasicOperationService<FormGroup> formGroupService)
        {
            _mapper = mapper;
            _formService = formService;
            _formQuestionService = formQuestionService;
            _formQuestionBasicService = formQuestionBasicService;
            _questionService = questionService;
            _subquestionService = subquestionService;
            _formGroupService = formGroupService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListAllForms");
        }

        public async Task<int> GetNextFormId()
        {
            var forms = await _formService.GetAll();
            return forms.Count() + 1;
        }

        public async Task<IActionResult> ListAllForms()
        {
            var formsList = await _formService.GetAll();
            List<FormVM> formVM = _mapper.Map<List<FormVM>>(formsList);
            return View(formVM);
        }

        public async Task<IActionResult> CreateForm()
        {
            FormVM formVM = new FormVM();
            return View(formVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForm(FormVM entityVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var form = _mapper.Map<Form>(entityVm);
                    //form.FormId = await GetNextFormId();
                    await _formService.Add(form);
                    return RedirectToAction("ListAllForms");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(entityVm);
        }


        public async Task<IActionResult> EditForm(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Form? form = await _formService.FindById(Convert.ToInt16(id));
                var questionEdit = _mapper.Map<FormEditVM>(form);
                return View(questionEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditForm(FormEditVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var form = _mapper.Map<Form>(entityVM);
                    await _formService.Update(form);
                    return RedirectToAction("ListAllForms");
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
                Form? form = await _formService.FindById(Convert.ToInt16(id));
                var formVM = _mapper.Map<FormVM>(form);

                return View(formVM);
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
                Form form = await _formService.FindById(Convert.ToInt32(id));
                if (form == null)
                {
                    return NotFound();
                }
                await _formService.Delete(form);
                return RedirectToAction("ListAllForms");
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
            return RedirectToAction("ListAllForms");
        }

        public async Task<IActionResult> EditQuestions(int id)
        {


            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var list = await _formQuestionService.GetFormQuestionsAsync(id);
                List<FormQuestionVM> listVM = _mapper.Map<List<FormQuestionVM>>(list);

                //order list by QuestionOrder
                listVM = listVM.OrderBy(x => x.QuestionOrder).ToList();

                //extra info
                var title = _mapper.Map<Form>(await _formService.FindById(id));

                ViewBag.Questions = _mapper.Map<List<QuestionVM>>(await _questionService.GetAll());
                ViewBag.FormTitle = title.Title;
                ViewBag.UsedIDs = await _formQuestionService.GetFormQuestionIDsAsync(id);
                ViewBag.Id = id;
                ViewBag.Groups = _mapper.Map<List<FormGroupVM>>(await _formGroupService.GetAll());

                return View(listVM);
            }
        }

        public async Task InsertLast(int qid, int fid, int gid)
        {
            if (ModelState.IsValid)
            {
                var list = await _formQuestionService.GetFormQuestionsAsync(fid);
                var fqvm = new FormQuestionVM
                {
                    FormId = fid,
                    QuestionId = qid,
                    QuestionOrder = list.Count(),
                    GroupId = gid
                };
                var add = _mapper.Map<FormQuestion>(fqvm);
                await _formQuestionBasicService.Add(add);

                // subquestions van de gekozen question toevoegen
                ICollection<Subquestion> subquestions = await _subquestionService.GetSubquestionByQuestion(qid);
                IEnumerable<FormQuestion> questionsInForm = await _formQuestionService.GetFormQuestionsAsync(fid);
                LinkedList<int> questionsInFormIds = new LinkedList<int>();
                foreach (FormQuestion q in questionsInForm)
                {
                    questionsInFormIds.AddLast(q.QuestionId);
                }
                int subquestionsAdded = 0;
                foreach (Subquestion subquestion in subquestions)
                {
                    if (!questionsInFormIds.Contains(subquestion.SubQuestionId))
                    {
                        subquestionsAdded++;
                        var subFormQuestionVM = new FormQuestionVM
                        {
                            FormId = fid,
                            QuestionId = subquestion.SubQuestionId,
                            QuestionOrder = list.Count() + subquestionsAdded,
                            GroupId = gid
                        };
                        await _formQuestionBasicService.Add(_mapper.Map<FormQuestion>(subFormQuestionVM));
                    }
                }
            }
        }

        [HttpGet]
        public async Task SwitchQuestionUp(int? qidN, int? fidN)
        {
            int qid = (int)qidN;
            int fid = (int)fidN;
            try
            {
                FormQuestion formQuestion = await _formQuestionService.GetFormQuestionAsync(qid, fid);
                if (formQuestion.QuestionOrder > 0)
                {
                    FormQuestion formQuestionInfront = await _formQuestionService.GetFormQuestionByFormIdAndOrder(Convert.ToInt16(formQuestion.QuestionOrder - 1), fid);
                    formQuestion.QuestionOrder--;
                    formQuestionInfront.QuestionOrder++;
                    await _formQuestionBasicService.Update(formQuestion);
                    await _formQuestionBasicService.Update(formQuestionInfront);
                }               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task SwitchQuestionDown(int? qidN, int? fidN)
        {
            int qid = (int) qidN;
            int fid = (int) fidN;
            try
            {
                FormQuestion formQuestion = await _formQuestionService.GetFormQuestionAsync(qid, fid);
                IEnumerable<FormQuestion> formQuestions = await _formQuestionService.GetQuestionsOfForm(fid);
                if (formQuestion.QuestionOrder + 1 < formQuestions.Count())
                {
                    FormQuestion formQuestionBehind = await _formQuestionService.GetFormQuestionByFormIdAndOrder(Convert.ToInt16(formQuestion.QuestionOrder + 1), fid);
                    formQuestion.QuestionOrder++;
                    formQuestionBehind.QuestionOrder--;
                    await _formQuestionBasicService.Update(formQuestion);
                    await _formQuestionBasicService.Update(formQuestionBehind);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[HttpPost]
        public async Task/*<IActionResult>*/ DeleteQuestionFromForm(int? qid, int? fid)
        {
            if(qid == null && fid == null)
            {
                //return NotFound();
            }
            try
            {
                FormQuestion formQuestion = await _formQuestionService.GetFormQuestionAsync(Convert.ToInt16(qid), Convert.ToInt16(fid));
                if (formQuestion == null)
                {
                    //return NotFound(); // or any other error response
                }
                await _formQuestionBasicService.Delete(formQuestion);
                IEnumerable<FormQuestion> formQuestions = await _formQuestionService.GetQuestionsOfForm(Convert.ToInt16(fid));
                foreach (FormQuestion remainingFormQuestion in formQuestions)
                {
                    if (remainingFormQuestion.QuestionOrder > formQuestion.QuestionOrder)
                    {
                        remainingFormQuestion.QuestionOrder --;
                        await _formQuestionBasicService.Update(remainingFormQuestion);
                    }
                }
                //return RedirectToActionPermanent("EditQuestions", new {id = fid});
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
