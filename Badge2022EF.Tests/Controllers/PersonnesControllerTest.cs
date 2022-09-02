using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL;
using Badge2022EF.DAL.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Badge2022EF.WebApi.JWT_Authentication.JWTWebAuthentication.Repository;
using Microsoft.AspNetCore.Mvc;
using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.Controllers;
using FluentAssertions;



// https://youtu.be/3BsESpxSzzw
namespace Badge2022EF.Tests.Controllers
{
    public class PersonnesControllerTest
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly PersonneRepository _personneRepository;
        private readonly SignInManager<PersonneEntity> _signInManager;
        private readonly UserManager<PersonneEntity> _userManager;
        private readonly Badge2022Context _context;
        public PersonnesControllerTest()
        {
            _jWTManager = A.Fake<IJWTManagerRepository>();
            _personneRepository = A.Fake<PersonneRepository>();
            _signInManager = A.Fake<SignInManager<PersonneEntity>>();
            _userManager = A.Fake<UserManager<PersonneEntity>>();
            _context = A.Fake<Badge2022Context>();
        }
        [Fact]
        public void PersonnesController_GetAll_ReturnOk()
        {
            //Arrange
            var personnes = A.Fake<Personnes>();
            var listPersonnes = A.Fake<IEnumerable<Personnes>>();

            //Isolate.WhenCalled(() => controller.SignInManager.PasswordSignInAsync(null, null, true, true)).WillReturn(Task.FromResult(SignInStatus.Failure));
            //var transBuilder = Isolate.Fake.Instance<ITransactionFactory>();

            // A.CallTo(() => _context.Users.ToList()).Returns(listPersonnes);
            //A.CallTo(() => _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonList);

            A.CallTo(() => _personneRepository.GetAll()).Returns(listPersonnes);
            var controller = new PersonnesController(_jWTManager, _personneRepository, _signInManager, _userManager, _context);

            //Act
            var result = controller.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
