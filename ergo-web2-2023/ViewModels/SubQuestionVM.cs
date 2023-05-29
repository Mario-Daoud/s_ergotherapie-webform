namespace ergo_web2_2023.ViewModels
{
    public class SubQuestionVM
    {
        public int Id { get; set; }
        public int SubQuestionId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public QuestionsVM Question { get; set; }
    }
}
