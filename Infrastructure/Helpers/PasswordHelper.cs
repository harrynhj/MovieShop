﻿namespace Infrastructure.Helpers;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PasswordHelper
{
    public static string GenerateSalt()
    {
        byte[] salt = new byte[128 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    public static string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hashed;
    }
}