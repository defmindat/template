﻿namespace EatWise.Common.Application.Authorization;

public sealed record PermissionResponse(Guid UserId, HashSet<string> Permissions);
