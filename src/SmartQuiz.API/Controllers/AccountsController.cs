﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartQuiz.API.Extensions;
using SmartQuiz.Application.UseCases.Users;
using SmartQuiz.Core.DTOs.Users;

namespace SmartQuiz.API.Controllers;

[Route("api/[controller]")]
public class AccountsController : ControllerBase
{

    /// <summary>
    /// Registrar novo usuário
    /// </summary>
    /// <param name="createUserDto"></param>
    /// <returns>Token de autenticação e Id do usuário criado</returns>
    /// <response code="200">Retorna o token de autenticação e o Id do usuário criado</response>
    /// <response code="400">Se os parâmetros são inválidos</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto createUserDto, [FromServices] CreateUserUseCase useCase)
    {
        var result = await useCase.Execute(createUserDto);
        return Ok(result); //Trocar para GetById quando tiver o método
    }

    /// <summary>
    /// Autenticar usuário com e-mail e senha
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Token de autenticação</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto dto, [FromServices] LoginUserUseCase useCase)
    {
        var result = await useCase.Execute(dto);
        return Ok(result);
    }

    /// <summary>
    /// Buscar usuário pelo Id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailsAsync([FromRoute] Guid userId, [FromServices] GetUserUseCase useCase)
    {
        var result = await useCase.Execute(userId);
        return Ok(result);
    }

    /// <summary>
    /// Obter informações do usuário autenticado
    /// </summary>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpGet, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailsAsync([FromServices] GetUserUseCase useCase)
    {
        var userId = User.GetUserId();
        var result = await useCase.Execute(userId);
        return Ok(result);
    }

    /// <summary>
    /// Buscar partidas de um usuário
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="useCase"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet("{userId:guid}/matches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserMatchesAsync([FromRoute] Guid userId, [FromServices] GetUserMatchesUseCase useCase, [FromQuery] int pageSize = 15, [FromQuery] int pageNumber = 1)
    {
        var result = await useCase.Execute(userId, pageSize, pageNumber);
        return Ok(result);
    }

    /// <summary>
    /// Buscar quizzes de um usuário
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="useCase"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet("{userId:guid}/quizzes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserQuizzesAsync([FromRoute] Guid userId, [FromServices] GetUserQuizzesUseCase useCase, [FromQuery] int pageSize = 15, [FromQuery] int pageNumber = 1)
    {
        var result = await useCase.Execute(userId, pageSize, pageNumber);
        return Ok(result);
    }

    /// <summary>
    /// Criar código de verificação de e-mail e enviar pro e-mail do usuário autenticado
    /// </summary>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpPost("verify-email"), Authorize]
    public async Task<IActionResult> VerifyEmailAsync([FromServices] VerifyEmailUseCase useCase)
    {
        var email = User.GetUserEmail();
        var result = await useCase.Execute(email);
        return Ok(result);

    }

    /// <summary>
    /// Verificar se o código de verificação de e-mail é válido e validar e-mail
    /// </summary>
    /// <param name="code"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpPost("verify-email-code/{code}"), Authorize]
    public async Task<IActionResult> VerifyEmailCodeAsync([FromRoute] string code, [FromServices] VerifyEmailCodeUseCase useCase)
    {
        var email = User.GetUserEmail();
        var result = await useCase.Execute(code, email);
        return Ok(result);

    }
}
