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

        [Fact]
        public async Task GetGroupAsync_ReturnsGroup_WhenGroupExists()
        {
            // Arrange
            var userAuthId = "authUserId";
            var groupName = "existingGroup";

            var groupEntity = new GroupEntity { GroupName = groupName };

            var userEntity = new UserEntity
            {
                Username = "authUser",
                AuthProvider = new AuthProviderEntity
                {
                    AuthProviderUserId = userAuthId,
                    Name = "github",
                },
                Groups = new List<GroupEntity> { groupEntity },
            };

            mockUserRepository
                .Setup(repo => repo.GetUserIncludingGroupsAndPlayersAndExperienceAsync(userAuthId))
                .ReturnsAsync(userEntity);

            // Act
            var result = await userService.GetGroupAsync(userAuthId, groupName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(groupEntity.GroupName, result.Value.GroupName);
        }

        [Fact]
        public async Task GetGroupAsync_ReturnsNotFound_WhenGroupDoesntExists()
        {
            // Arrange
            var userAuthId = "authUserId";
            var groupName = "nonExistingGroup";

            var userEntity = new UserEntity
            {
                Username = "authUser",
                AuthProvider = new AuthProviderEntity
                {
                    AuthProviderUserId = userAuthId,
                    Name = "github",
                },
                Groups = [],
            };

            mockUserRepository
                .Setup(repo => repo.GetUserIncludingGroupsAndPlayersAndExperienceAsync(userAuthId))
                .ReturnsAsync(userEntity);

            // Act
            var result = await userService.GetGroupAsync(userAuthId, groupName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorType.NotFound, result.Error!.ErrorType);
        }

        [Fact]
        public async Task CreateGroupOnUserAsync_ReturnsSuccess_WhenGroupIsCreated()
        {
            // Arrange
            var userAuthId = "authUserId";
            var groupName = "newGroup";

            var userEntity = new UserEntity
            {
                Username = "authUser",
                AuthProvider = new AuthProviderEntity
                {
                    AuthProviderUserId = userAuthId,
                    Name = "github",
                },
                Groups = new List<GroupEntity>(),
            };

            mockUserRepository
                .Setup(repo => repo.GetUserIncludingGroupsAsync(userAuthId))
                .ReturnsAsync(userEntity);

            // Act
            var result = await userService.CreateGroupOnUserAsync(userAuthId, groupName);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddPlayerOnGroupAsync_ReturnsSuccess_WhenPlayerIsAdded()
        {
            // Arrange
            var userAuthId = "authUserId";
            var groupName = "newGroup";
            var playerName = "newPlayer";

            var userEntity = new UserEntity
            {
                Username = "authUser",
                AuthProvider = new AuthProviderEntity
                {
                    AuthProviderUserId = userAuthId,
                    Name = "github",
                },
                Groups = [new GroupEntity { GroupName = groupName, Players = [] }],
            };

            mockUserRepository
                .Setup(repo => repo.GetUserIncludingGroupsAndPlayersAsync(userAuthId))
                .ReturnsAsync(userEntity);

            mockPlayerService
                .Setup(s => s.GetOrCreatePlayerAsync(playerName))
                .ReturnsAsync(
                    new PlayerEntity
                    {
                        PlayerName = playerName,
                        TotalExperience = 1,
                        TotalLevel = 1,
                    }
                );

            // Act
            var result = await userService.AddPlayerOnGroupAsync(userAuthId, groupName, playerName);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task RemovePlayerFromGroup_ReturnsSuccess_WhenPlayerIsRemoved()
        {
            // Arrange
            var userAuthId = "authUserId";
            var groupName = "newGroup";
            var playerName = "newPlayer";

            var playerEntity = new PlayerEntity
            {
                PlayerName = playerName,
                TotalExperience = 1,
                TotalLevel = 1,
            };

            var userEntity = new UserEntity
            {
                Username = "authUser",
                AuthProvider = new AuthProviderEntity
                {
                    AuthProviderUserId = userAuthId,
                    Name = "github",
                },
                Groups = [new GroupEntity { GroupName = groupName, Players = [playerEntity] }],
            };

            mockUserRepository
                .Setup(repo => repo.GetUserIncludingGroupsAndPlayersAsync(userAuthId))
                .ReturnsAsync(userEntity);

            mockGroupRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<GroupEntity>()))
                .Verifiable();

            // Act
            var result = await userService.RemovePlayerFromGroup(userAuthId, groupName, playerName);

            // Assert
            Assert.True(result.IsSuccess);

            // Assert that the group was updated
            mockGroupRepository.Verify();
        }
    }
}
