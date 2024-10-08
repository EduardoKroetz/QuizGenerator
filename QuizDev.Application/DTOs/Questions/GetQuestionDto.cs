﻿
using QuizDev.Application.DTOs.QuestionOptions;
using QuizDev.Core.Entities;

namespace QuizDev.Application.DTOs.Questions;

public class GetQuestionDto
{
    public GetQuestionDto(Guid id, string text, Guid quizId, List<QuestionOption> options)
    {
        Id = id;
        Text = text;
        QuizId = quizId;
        Options = options.Select(x => new GetQuestionOptionDto(x.Id, x.Response, x.IsCorrectOption, x.QuestionId)).ToList();
    }

    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid QuizId { get; set; }
    public List<GetQuestionOptionDto> Options { get; set; }
}