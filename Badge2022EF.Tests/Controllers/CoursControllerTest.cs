
using Badge2022EF.DAL.Repositories;
using FakeItEasy;
using Badge2022EF.DAL;
using Badge2022EF.DAL.Repositories.Interface;
using Moq;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;
using Badge2022EF.DAL.Entities;
using Badge2022EF.WebApi.Controllers;

// https://youtu.be/3BsESpxSzzw
namespace Badge2022EF.Tests.Controllers
{
    public class CoursControllerTest
    {

        [Fact]
        public void CoursController_GetAll_ReturnOk()
        {
            var CoursList = A.Fake<List<Cours>>();
            //- var mockContext = new Mock<Badge2022Context>();
            var mockSet = new Mock<DbSet<CoursEntity>>();

            var mockContext = new Mock<Badge2022Context>();
            mockContext.Setup(m => m.Cours).Returns(mockSet.Object);

            //Arrange
            var mockRepo = new Mock<ICoursRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(CoursList);

            // var repo = new CoursRepository( mockContext.Object );                                // Context ne passe pas !!!!!!!!!!!!!!!!
            var controller = new CoursController( (CoursRepository)mockRepo.Object, mockContext.Object );         // Context ne passe pas !!!!!!!!!!!!!!!!

            //Act
            //- var result = controller.GetAll();

            //Assert
            //- result.Should().NotBeNull();
        }
    }
}
