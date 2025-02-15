﻿namespace RapidPay.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task RegisterUserAsync(string username, string password);
    }
}
