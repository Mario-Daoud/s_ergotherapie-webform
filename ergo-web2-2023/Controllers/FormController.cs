using AutoMapper;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using ergo_web2_2023.Services.Interfaces;
using ergo_web2_2023.Repositories.Interfaces;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.Licensing;
using ergo_web2_2023.Models.Enums;
using System.Text;
using System.Drawing;

namespace ergo_web2_2023.Controllers
{
    public class FormController : Controller
    {
        private readonly IMapper _mapper;
        private IBasicOperationService<Question> _questionService;
        private IBasicOperationService<Form> _formService;
        private IFormQuestionService<FormQuestion> _formQuestionService;
        private IQuestionOptionService<QuestionOption> _questionOptionService;
        private IBasicOperationsDAO<Subquestion> _subquestionService;
        private IBasicOperationsDAO<FormGroup> _formGroupService;

        public FormController(IMapper mapper,
            IBasicOperationService<Question> questionService,
            IBasicOperationService<Form> formService,
            IFormQuestionService<FormQuestion> formQuestionService,
            IQuestionOptionService<QuestionOption> questionOptionService,
            IBasicOperationsDAO<Subquestion> subquestionService,
            IBasicOperationsDAO<FormGroup> formGroupService)
        {
            _mapper = mapper;
            _questionService = questionService;
            _formService = formService;
            _formQuestionService = formQuestionService;
            _questionOptionService = questionOptionService;
            _subquestionService = subquestionService;
            _formGroupService = formGroupService;
        }
        public async Task<IActionResult> Index()
        {

            var questionList = await _questionService.GetAll();
            List<QuestionsVM> listVM = _mapper.Map<List<QuestionsVM>>(questionList);

            return View(listVM);

        }
        public async Task<IActionResult> FillInForm(int id)
        {

            char subQuestionGroupCharacter = 'a';
            var subquestions = await _subquestionService.GetAll();
            var formQuestions = await _formQuestionService.GetQuestionsOfForm(id);
            var formGroups = await _formGroupService.GetAll();
      


            List<QuestionsVM> questionsList = new List<QuestionsVM>();
            List<FormGroupVM> formGroupList = new List<FormGroupVM>();

            foreach (var formQuestion in formQuestions)
            {
                QuestionsVM questionVM = new QuestionsVM();
                questionVM = _mapper.Map<QuestionsVM>(formQuestion.Question);
                questionVM.IsSubquestion = false;
                questionVM.GroupId = formQuestion.GroupId;
                foreach (var subquestion in subquestions)
                {
                    if (formQuestion.QuestionId == subquestion.SubQuestionId)
                    {
                        questionVM.IsSubquestion = true;
                        questionVM.SuperQuestionId = subquestion.QuestionId;
                        questionVM.OptionId = subquestion.OptionId;
                       // questionVM.subQuestionCharacter = subQuestionCharacter;
                       // subQuestionCharacter++;
                    }
                    
                }
                questionsList.Add(questionVM);

                
                FormGroupVM formGroupVM = new FormGroupVM();


                formGroupVM = _mapper.Map<FormGroupVM>(formQuestion.Group);
                Boolean containsGroup = false;
                foreach (var formGroup in formGroupList)
                {
                    if(questionVM.IsSubquestion)
                    {
                        if (formGroupVM.GroupId == formGroup.GroupId && formGroup.OptionId == questionVM.OptionId && formGroup.SuperQuestionId == questionVM.SuperQuestionId)
                        {
                            containsGroup = true;
                        }
                        formGroupVM.subquestionGroupChar = subQuestionGroupCharacter;
                        subQuestionGroupCharacter++;
                    }
                    else
                    {
                        if (formGroupVM.GroupId == formGroup.GroupId)
                        {
                            containsGroup = true;
                        }
                    }
                    
                }

                if (!containsGroup)
                {
                    formGroupVM.OptionId = questionVM.OptionId;
                    formGroupVM.SuperQuestionId = questionVM.SuperQuestionId;
                    
                    formGroupList.Add(formGroupVM);
                }
            }



            for (int i = 0; i < formGroupList.Count; i++)
            {
                List<QuestionsVM> questions = new List<QuestionsVM>();
                foreach(var question in questionsList)
                {
                    if (formGroupList[i].GroupId == question.GroupId && formGroupList[i].OptionId == question.OptionId && formGroupList[i].SuperQuestionId == question.SuperQuestionId) 
                    {
                       questions.Add(question);
                    }
                }
                formGroupList[i].Questions = questions;
            }

            ViewBag.Id = id;
            return View(formGroupList);
        }


        public async Task<ActionResult> CreateDocumentAsync(int id)
        {
            SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqVk1mQ1NDaV1CX2BZf1J8RGdTe1dgBShNYlxTR3ZaQFViSH9XckxrXX9e;Mgo+DSMBPh8sVXJ1S0R+X1pCaV1FQmFJfFBmRGJTflp6cFxWESFaRnZdQV1mS3ZSd0RnXXZWdXBV;ORg4AjUWIQA/Gnt2VFhiQlJPcEBAWnxLflF1VWJYdV90flFPcC0sT3RfQF5jTHxbd0RiXXtXeHJdRg==;MjIyNzQxOEAzMjMxMmUzMDJlMzBKcFUzWUFXN0FLa3ZLbE5kYUZrRm5WM1NBeDM5SGdoNGZLblJoSEZEeTFrPQ==;MjIyNzQxOUAzMjMxMmUzMDJlMzBFblFTMGtyZEdlS21waUpOOVRSZkZpODZKemhueGZSRithUTNlOWY3VFpJPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVldX2RWfFN0RnNYflRxcF9CaUwgOX1dQl9gSXtRfkRiWHtaeX1WRGA=;MjIyNzQyMUAzMjMxMmUzMDJlMzBKTUNQTDVMb0pFK0NuVGRoeHJpZE1xcnNTSGhtN2N6MG5KYThVVlNOdndNPQ==;MjIyNzQyMkAzMjMxMmUzMDJlMzBvbnAvRURyNzB4Y1lUT1phY0pPeVdiZE5kVFhOVHdHdWRsdTBWUGNyRnVVPQ==;Mgo+DSMBMAY9C3t2VFhiQlJPcEBAWnxLflF1VWJYdV90flFPcC0sT3RfQF5jTHxbd0RiXXtXeX1URg==;MjIyNzQyNEAzMjMxMmUzMDJlMzBPZUFwUFd6K1dLVjdVRmhPQ25QbWRKdEVQM2Q2Smg4TlVNQS9DVC9uNUdjPQ==;MjIyNzQyNUAzMjMxMmUzMDJlMzBwQzQvTG13cnc3eVBUZitmT2lNMHEzQjlqTE5DR2ZUaFdFWUpiMlFtQmJnPQ==;MjIyNzQyNkAzMjMxMmUzMDJlMzBKTUNQTDVMb0pFK0NuVGRoeHJpZE1xcnNTSGhtN2N6MG5KYThVVlNOdndNPQ==");
            var form = await _formService.FindById(id);
            var subquestions = await _subquestionService.GetAll();
            var formQuestions = await _formQuestionService.GetQuestionsOfForm(id);
            var formGroups = await _formGroupService.GetAll();
            FormGroupVM formGroupVM = new FormGroupVM();
            FormQuestionVM formQuestionVM = new FormQuestionVM();
            using (WordDocument document = new WordDocument())
            {
                WSection section = document.AddSection() as WSection;
                document.EnsureMinimal();
                WTableStyle tableStyle = document.AddTableStyle("CustomTableStyle");
                tableStyle.TableProperties.Borders.BorderType = BorderStyle.Single;
                tableStyle.TableProperties.Borders.LineWidth = 2;
                tableStyle.TableProperties.Borders.Color = Color.Black;
                WTable Titletable = (WTable)section.AddTable();
                Titletable.ApplyStyle(tableStyle.Name);
                WTableRow headerRow = Titletable.AddRow();
                headerRow.Cells.Add(new WTableCell(document));
                headerRow.Cells[0].AddParagraph().AppendText(form.Title).CharacterFormat.Bold = true;
                headerRow.Cells[0].CellFormat.BackColor = Color.Blue;
                WTableRow DescriptionRow = Titletable.AddRow();
                DescriptionRow.Cells.Add(new WTableCell(document));
                DescriptionRow.Cells[0].AddParagraph().AppendText(form.Description);
                DescriptionRow.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
                DescriptionRow.Cells[0].CellFormat.BackColor = Color.White;
                for(int i = 1; i < DescriptionRow.Cells.Count; i++)
                {
                    DescriptionRow.Cells[i].CellFormat.HorizontalMerge = CellMerge.Continue;
                }
                foreach (var formgroup in formGroups)
                {
                    formGroupVM = _mapper.Map<FormGroupVM>(formgroup);
                    WTable FormGrouptable = (WTable)section.AddTable();
                    FormGrouptable.ApplyStyle(tableStyle.Name);
                    WTableRow FormTitleRow = FormGrouptable.AddRow();
                    FormTitleRow.Cells.Add(new WTableCell(document));
                    FormTitleRow.Cells[0].AddParagraph().AppendText(formGroupVM.Title).CharacterFormat.TextColor = Color.White;
                    FormTitleRow.Cells[0].CellFormat.BackColor = Color.Blue;
                    var QuestionsForm = await _formQuestionService.GetFormQuestionsOfGroup(formGroupVM.GroupId);
                    foreach(var formQuestion in QuestionsForm)
                    {
                        WTable table = (WTable)section.AddTable();
                        table.ApplyStyle(tableStyle.Name);
                        formQuestionVM = _mapper.Map<FormQuestionVM>(formQuestion);
                        if (formQuestionVM.Question.Type == QuestionType.multiple_choice_question)
                        {
                            WTableRow QuestionRow = table.AddRow();
                            QuestionRow.Height = 150;
                            QuestionRow.Cells.Add(new WTableCell(document));
                            QuestionRow.Cells.Add(new WTableCell(document));
                            QuestionRow.Cells[0].AddParagraph().AppendText(formQuestionVM.Question.Title);
                            StringBuilder optionsText = new StringBuilder();
                            foreach (var option in formQuestionVM.Question.QuestionOptions)
                            {
                               optionsText.AppendLine(option.OptionText);
                            }
                            WTableRow optionRow = table.AddRow();
                            optionRow.Cells.Add(new WTableCell(document));
                            optionRow.Cells[0].AddParagraph().AppendText(optionsText.ToString());
                            optionRow.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;

                            for(int i = 1; i < optionRow.Cells.Count; i++)
                            {
                                optionRow.Cells[i].CellFormat.HorizontalMerge = CellMerge.Continue;
                            }
                        }
                        else
                        {
                            WTableRow QuestionRow = table.AddRow();
                            QuestionRow.Cells.Add(new WTableCell(document));
                            QuestionRow.Cells.Add(new WTableCell(document));
                            QuestionRow.Cells[0].AddParagraph().AppendText(formQuestionVM.Question.Title);
                        }
                        section.AddParagraph();
                    }
                }
                MemoryStream stream = new MemoryStream();
                document.Save(stream, FormatType.Docx);
                stream.Position = 0;
                return File(stream, "application/msword", "Form.docx");
            }            
        }
    }   
}
