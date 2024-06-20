using DotNetTaskAPI.Models.EnumType;

namespace DotNetTaskAPI.Models.Domain
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<string> Options { get; set; }
    }
}
