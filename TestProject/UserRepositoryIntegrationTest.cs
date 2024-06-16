using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetMarketContext _dbContext;
        private readonly UserRepository _UserRepository;
        public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _UserRepository = new UserRepository(_dbContext); 
        }
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            //Arrange
            var email = "test@example.com";
            var password = "password";
            var user = new User { Email = email, Password = password, FirstName = "dvori", LastName = "rottman" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            //Act
            var result = await _UserRepository.Login(user);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            //Arrange
            var user = new User { Email = "updateuser@example.com", Password = "password", FirstName = "Eve", LastName = "Johnson" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var updatedUser = new User { Email = "updateduser@example.com", Password = "newpassword", FirstName = "UpdatedName", LastName = "UpdatedLastName" };

            // Attach the existing user to the context before updating
            _dbContext.Entry(user).State = EntityState.Detached;
            updatedUser.UserId = user.UserId; // Ensure the IDs match

            //Act
            var result = await _UserRepository.UpdateUser(user.UserId, updatedUser);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("updateduser@example.com", result.Email);
        }


        [Fact]
        public async Task RegisterUser_ValidUser_AddsUser()
        {
            //Arrange
            var user = new User { Email = "newuser@example.com", Password = "securepassword", FirstName = "John", LastName = "Doe" };

            //Act
            var result = await _UserRepository.Register(user);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("newuser@example.com", result.Email);
        }

        [Fact]
        public async Task GetUserById_ValidId_ReturnsUser()
        {
            //Arrange
            var user = new User { Email = "userbyid@example.com", Password = "password", FirstName = "Alice", LastName = "Smith" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _UserRepository.GetById(user.UserId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("userbyid@example.com", result.Email);
        }
    }
}



