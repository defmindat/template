﻿using EatWise.Common.Application.Messaging;

namespace EatWise.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;

