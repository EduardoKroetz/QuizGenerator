﻿using QuizDev.Core.Entities;

namespace QuizDev.Core.Repositories;

public interface IResponseRepository 
{
    Task CreateAsync(Response matchResponse);
}