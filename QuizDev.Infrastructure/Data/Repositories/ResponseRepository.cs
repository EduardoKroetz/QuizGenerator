﻿using QuizDev.Core.Entities;
using QuizDev.Core.Repositories;

namespace QuizDev.Infrastructure.Data.Repositories;

public class ResponseRepository : IResponseRepository
{
    private readonly QuizDevDbContext _dbContext;

    public ResponseRepository(QuizDevDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Response matchResponse)
    {
        await _dbContext.Responses.AddAsync(matchResponse);
        await _dbContext.SaveChangesAsync();
    }
}
