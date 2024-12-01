﻿
using System.Security.Claims;

namespace SmartQuiz.API.Extensions;

public static class ClaimsPrincipalExtension
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdStr = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Não foi possível obter o id do usuário");
        return new Guid(userIdStr);
    }

    public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? throw new Exception("Não foi possível obter o email do usuário");
    }
}
