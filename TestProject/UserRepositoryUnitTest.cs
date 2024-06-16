using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace TestProject
{
    public class UserRepositoryUnitTest: IClassFixture<DatabaseFixture>
    {
        
        [Fact]
        public async Task GetById_ValidId_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var user = new User { UserId = userId, FirstName = "dvori", LastName = "rottman", Email = "dvori@gmail.com", Password = "password" };
            var users = new List<User> { user }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            // Mock FindAsync to return the user based on UserId
            mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((object[] ids) => users.FirstOrDefault(u => u.UserId == (int)ids[0]));

            var mockContext = new Mock<AdoNetMarketContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.GetById(userId);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Register_ValidUser_AddsUser()
        {
            // Arrange
            var user = new User { FirstName = "dvori", LastName = "rottman", Email = "dvori@gmail.com", Password = "password" };
            var mockSet = new Mock<DbSet<User>>();

            var mockContext = new Mock<AdoNetMarketContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.Register(user);

            // Assert
          
            Assert.Equal(user, result);
        }

        [Fact]
        public async  Task GetUser_validCardentials_ReturnsUser()
        {
            var user = new User { FirstName = "dvori", LastName = "rottman", Email = "dvori@gmail.com", Password = "password" };

            var mockContext = new Mock<AdoNetMarketContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.Login(user);

            Assert.Equal(user, result);
        }
        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            // Arrange
            var user = new User { FirstName = "dvori", LastName = "rottman", Email = "dvori@gmail.com", Password = "password" };
            var updatedUser = new User { FirstName = "updated", LastName = "user", Email = "updated@gmail.com", Password = "newpassword" };

            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<AdoNetMarketContext>();

            mockSet.Setup(m => m.Update(It.IsAny<User>()));
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.UpdateUser(user.UserId, updatedUser);

            // Assert
            Assert.Equal("updated@gmail.com", result.Email);
        }
    }
}