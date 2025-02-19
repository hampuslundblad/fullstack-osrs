using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Errors;
using DotnetComp.Models.Entities;
using DotnetComp.Repositories;
using DotnetComp.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DotnetComp.Tests
{
    public class UserServiceTest
    {
        private readonly Mock<ILogger<UserService>> mockLogger;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<IGroupRepository> mockGroupRepository;
        private readonly Mock<IPlayerService> mockPlayerService;
        private readonly UserService userService;

        public UserServiceTest()
        {
            mockLogger = new Mock<ILogger<UserService>>();
            mockUserRepository = new Mock<IUserRepository>();
            mockGroupRepository = new Mock<IGroupRepository>();
            mockPlayerService = new Mock<IPlayerService>();
            userService = new UserService(
                mockLogger.Object,
                mockUserRepository.Object,
                mockGroupRepository.Object,
                mockPlayerService.Object
            );
        }

        [Fact]
        public async Task FindOrCreateUserAsync_ReturnsExistingUser_WhenUserExists()
        {
            // Arrange
            var userAuthId = "existingUser";
            var userEntity = new UserEntity
            {
                Username = "existingUser",
                AuthProvider = new AuthProviderEntity
                {
                    Name = "github",
                    AuthProviderUserId = userAuthId,
                },
            };
            mockUserRepository
                .Setup(repo => repo.GetUserIncludingGroupsAndPlayersAsync(userAuthId))
                .ReturnsAsync(userEntity);

            // Act
            var result = await userService.FindOrCreateUserAsync(userAuthId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(userEntity.Username, result.Value.Username);
        }

        [Fact]
        public async Task FindOrCreateUserAsync_CreatesNewUser_WhenUserDoesNotExist()
        {
            // Arrange
            var userAuthId = "newUser";
            mockUserRepository
                .Setup(repo => repo.GetUserIncludingGroupsAndPlayersAsync(userAuthId))
                .ReturnsAsync((UserEntity?)null);
            mockUserRepository
                .Setup(repo => repo.CreateUserAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync((UserEntity userEntity) => userEntity);

            // Act
            var result = await userService.FindOrCreateUserAsync(userAuthId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("basic user", result.Value.Username);
        }
    }
}
