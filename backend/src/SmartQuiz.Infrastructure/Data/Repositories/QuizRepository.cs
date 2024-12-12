﻿using Microsoft.EntityFrameworkCore;
using SmartQuiz.Core.Entities;
using SmartQuiz.Core.Repositories;
using SmartQuiz.Infrastructure.Data.Repositories.Base;

namespace SmartQuiz.Infrastructure.Data.Repositories;

public class QuizRepository : Repository<Quiz>, IQuizRepository
{
    public QuizRepository(SmartQuizDbContext dbContext) : base(dbContext)
    {
    }

    public new async Task<Quiz?> GetByIdAsync(Guid id)
    {
        return await context.Quizzes
            .Include(x => x.Questions.OrderBy(q => q.Order))
            .ThenInclude(x => x.AnswerOptions)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<List<Quiz>> SearchQuiz(string[]? keyWords, int skip, int take)
    {
        var query = context.Quizzes.AsQueryable();

        if (keyWords != null)
            query = query.Where(x => keyWords.Any(k =>
                x.Title.ToLower().Contains(k) ||
                x.Description.ToLower().Contains(k)
            ));

        return await query
            .Include(x => x.Questions)
            .Where(x => x.IsActive == true)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<bool> HasMatchesRelated(Guid quizId)
    {
        return await context.Matches.AnyAsync(x => x.QuizId == quizId);
    }

    public async Task<List<Quiz>> GetUserQuizzesAsync(Guid userId, int skip, int take)
    {
        return await context.Quizzes
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.Questions)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}