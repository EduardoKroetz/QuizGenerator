﻿using Microsoft.EntityFrameworkCore;
using SmartQuiz.Core.DTOs.Matches;
using SmartQuiz.Core.DTOs.Quizzes;
using SmartQuiz.Core.DTOs.Users;
using SmartQuiz.Core.Entities;
using SmartQuiz.Core.Repositories;

namespace SmartQuiz.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SmartQuizDbContext _dbContext;

    public UserRepository(SmartQuizDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<GetUserDto?> GetDetailsAsync(Guid userId)
    {
        return await _dbContext.Users
            .Select(x => new GetUserDto
            {
                Id = x.Id,
                Email = x.Email,
                Username = x.Username,
                EmailIsVerified = x.EmailIsVerified
            })
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<List<GetMatchDto>> GetUserMatchesAsync(Guid userId, int skip, int take)
    {
        return await _dbContext.Matches
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Skip(skip)
            .Take(take)
            .Include(x => x.Quiz)
            .Select(x => new GetMatchDto
            {
                Id = x.Id,
                Score = x.Score,
                CreatedAt = x.CreatedAt,
                ExpiresIn = x.ExpiresIn,
                Status = x.Status.ToString(),
                QuizId = x.QuizId,
                UserId = x.UserId,
                Reviewed = x.Reviewed,
                ReviewId = x.ReviewId,
                Quiz = new GetQuizDto
                (
                    x.Quiz.Id,
                    x.Quiz.Title,
                    x.Quiz.Description,
                    x.Quiz.Expires,
                    x.Quiz.ExpiresInSeconds,
                    x.Quiz.IsActive,
                    x.Quiz.UserId
                )
            })
            .ToListAsync();
    }

    public async Task<List<GetQuizDto>> GetUserQuizzesAsync(Guid userId, int skip, int take)
    {
        return await _dbContext.Quizzes
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Skip(skip)
            .Take(take)
            .Select(x => new GetQuizDto(x.Id, x.Title, x.Description, x.Expires, x.ExpiresInSeconds, x.IsActive, x.UserId))
            .ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
