﻿namespace Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        long? Id { get; }
        string? Email { get; }
        string? Role { get; }
    }
}
