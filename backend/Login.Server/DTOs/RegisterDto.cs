﻿namespace Login.Server.DTOs;

public sealed record RegisterDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password);
