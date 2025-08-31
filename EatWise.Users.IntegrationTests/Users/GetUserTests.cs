﻿using EatWise.Common.Domain;
using EatWise.Users.Application.Users.GetUser;
using EatWise.Users.Application.Users.RegisterUser;
using EatWise.Users.Domain.Users;
using EatWise.Users.IntegrationTests.Abstractions;
using FluentAssertions;

namespace EatWise.Users.IntegrationTests.Users;

public class GetUserTests : BaseIntegrationTest
{
    public GetUserTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        
        // Act
        Result<UserResponse> userResult = await Sender.Send(new GetUserQuery(userId));
        
        // Assert
        userResult.Error.Should().Be(UserErrors.NotFound(userId));  
    }

    [Fact]
    public async Task Should_ReturnUser_WhenUserExists()
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(),
            Faker.Name.FirstName(),
            Faker.Name.LastName()));
        
        Guid userId = result.Value;
        
        // Act 
        Result<UserResponse> userResult = await Sender.Send(new GetUserQuery(userId));
        
        // Assert
        userResult.IsSuccess.Should().BeTrue();
        userResult.Value.Should().NotBeNull();
    }
}
