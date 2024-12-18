﻿using AutoMapper;
using SmartQuiz.Application.DTOs.Questions;
using SmartQuiz.Application.DTOs.Responses;
using SmartQuiz.Application.Services.Interfaces;

namespace SmartQuiz.Application.UseCases.Quizzes;

public class GetQuestionsByQuizUseCase
{
    private readonly IQuestionService _questionService;
    private readonly IMapper _mapper;

    public GetQuestionsByQuizUseCase(IQuestionService questionService, IMapper mapper)
    {
        _questionService = questionService;
        _mapper = mapper;
    }

    public async Task<ResultDto> Execute(Guid quizId)
    {
        var questions = await _questionService.GetQuestionsByQuizId(quizId);

        var questionsDto = _mapper.Map<IEnumerable<GetQuestionWithCorrectOptionDto>>(questions);     
        
        return new ResultDto(questionsDto);
    }
}