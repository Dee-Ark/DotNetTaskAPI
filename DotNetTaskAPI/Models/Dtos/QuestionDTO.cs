using DotNetTaskAPI.Models.EnumType;

namespace DotNetTaskAPI.Models.Dtos
{
    public class QuestionDTO
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<string> Options { get; set; }
    }
}
