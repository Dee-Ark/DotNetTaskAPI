using DotNetTaskAPI.Models.Domain;
using DotNetTaskAPI.Models.Dtos;
using DotNetTaskAPI.Models.EnumType;
using DotNetTaskAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DotNetTaskAPI.Controllers.XUnitTestController
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsControllerTests : ControllerBase
    {
        private readonly Mock<ICosmosDbService> _mockCosmosDbService;
        private readonly QuestionController _controller;
        public QuestionsControllerTests()
        {
            _mockCosmosDbService = new Mock<ICosmosDbService>();
            //_controller = new QuestionController(_mockCosmosDbService.Object);
        }


        [Fact]
        [HttpPost]
        public async Task PostQuestion_ShouldReturnCreatedAtAction()
        {
            var questionDto = new Question { Type = QuestionType.Paragraph, Text = "What is your name?", Options = null };

            var result = await _controller.CreateQuestion(questionDto);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        [HttpGet]
        public async Task GetQuestion_ShouldReturnQuestion()
        {
            var question = new Question { QuestionId = 1, Type = QuestionType.Paragraph, Text = "What is your name?", Options = null };
            _mockCosmosDbService.Setup(service => service.GetQuestionAsync("1")).ReturnsAsync(question);

            var result = await _controller.GetQuestion(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Question>(okResult.Value);
            Assert.Equal("What is your name?", returnValue.Text);
        }
    }
}
