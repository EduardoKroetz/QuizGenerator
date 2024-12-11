﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartQuiz.API.Extensions;
using SmartQuiz.Application.UseCases.AnswerOptions;
using SmartQuiz.Application.UseCases.Questions;
using SmartQuiz.Application.DTOs.Questions;

namespace SmartQuiz.API.Controllers;

[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    /// <summary>
    /// Criar uma nova questão para um Quiz
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateQuestionDto dto, [FromServices] CreateQuestionUseCase useCase)
    {
        var userId = User.GetUserId();
        var result = await useCase.Execute(dto, userId);
        return Ok(result);
    }

    /// <summary>
    /// Buscar questão pelo Id
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpGet("{questionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQuestionDetailsAsync([FromRoute] Guid questionId, [FromServices] GetQuestionDetailsUseCase useCase)
    {
        var result = await useCase.Execute(questionId);
        return Ok(result);
    }

    /// <summary>
    /// Atualizar a pergunta (text) da Questão
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="dto"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpPatch("{questionId:guid}/text"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid questionId, [FromBody] UpdateQuestionDto dto, [FromServices] UpdateQuestionUseCase useCase)
    {
        var userId = User.GetUserId();
        var result = await useCase.Execute(questionId, dto, userId);
        return Ok(result);

    }

    /// <summary>
    /// Deletar questão pelo Id
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpDelete("{questionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid questionId, [FromServices] DeleteQuestionUseCase useCase)
    {
        var userId = User.GetUserId();
        var result = await useCase.Execute(questionId, userId);
        return Ok(result);
    }


    /// <summary>
    /// Alterar opção de resposta correta da questão
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpPatch("{questionId:guid}/correct-option/{answerOptionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid questionId, [FromRoute] Guid answerOptionId, [FromServices] UpdateCorrectOptionUseCase useCase)
    {
        var userId = User.GetUserId();
        var result = await useCase.Execute(questionId, answerOptionId, userId);
        return Ok(result);
    }


    /// <summary>
    /// Buscar opções de resposta de uma Questão
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="useCase"></param>
    /// <returns></returns>
    [HttpGet("{questionId:guid}/answer-options")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAnswerOptionsFromQuestion([FromRoute] Guid questionId, [FromServices] GetAnswerOptionsByQuestionUseCase useCase)
    {
        var result = await useCase.Execute(questionId);
        return Ok(result);
    }
}
