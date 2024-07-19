using BusinessLogicLayer;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace TestProject
{
    public class AccountServiceTests
    {
        private readonly Mock<IUserManager> _userManagerMock;
        private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _userManagerMock = new Mock<IUserManager>();
            _tokenGeneratorMock = new Mock<ITokenGenerator>();
            _accountService = new AccountService(_userManagerMock.Object, _tokenGeneratorMock.Object);

        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenUserIsLoggedIn()
        {
            var user = new IdentityUser { UserName = "testuser" };
            var loginDto = new Login ("testuser","Password123" );
            var roles = new List<string> { "User" };
            var expectedToken = "generatedToken";

            _userManagerMock.Setup(um => um.FindByNameAsync(loginDto.Username))
                            .ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginDto.Password))
                            .ReturnsAsync(true);
            _userManagerMock.Setup(um => um.GetRolesAsync(user))
                            .ReturnsAsync(roles);
            _tokenGeneratorMock.Setup(tg => tg.Generate(user, roles))
                               .Returns(expectedToken);

            var result = await _accountService.LoginAsync(loginDto);

            result.Should().Be(expectedToken);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnTrue_WhenUserCreated()
        {

            var registerDto = new Register("testuser", "test@example.com", "Password123", "Driver");

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                           .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                           .ReturnsAsync(IdentityResult.Success);

            var result = await _accountService.RegisterAsync(registerDto);

            result.Should().BeTrue();  
        }

        [Fact]
        public async Task AssignRoleAsync_ShouldReturnTrue_WhenRoleAssigned()
        {
            var user = new IdentityUser { UserName = "testuser" };
            var userRoleDto = new UserRole("testuser", "Driver");

            _userManagerMock.Setup(um => um.FindByNameAsync(userRoleDto.Username))
                            .ReturnsAsync(user);
            _userManagerMock.Setup(um => um.AddToRoleAsync(user, userRoleDto.Role))
                            .ReturnsAsync(IdentityResult.Success);

            var result = await _accountService.AssignRoleAsync(userRoleDto);

            result.Should().BeTrue();  
        }
    }
}