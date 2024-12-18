import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { CreateQuiz, Quiz } from '../../interfaces/Quiz';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  constructor(private apiService: ApiService) { }

  getQuizById(id: string) {
    return this.apiService.get(`quizzes/${id}`);
  }

  getQuizQuestions(id: string) {
    return this.apiService.get(`quizzes/${id}/questions`);
  }

  createQuiz(props: CreateQuiz) {
    return this.apiService.post('quizzes', props);
  }

  toggleQuiz(quizId: string) {
    return this.apiService.post('quizzes/toggle/'+quizId, {})
  }

  deleteQuiz(quizId: string) {
    return this.apiService.delete(`quizzes/${quizId}`);
  }

  generateQuiz(theme: string, difficulty: string, numberOfQuestions: number, expires: boolean, expiresInSeconds: number) {
    return this.apiService.post('quizzes/generate', { theme, difficulty, numberOfQuestions, expires, expiresInSeconds })
  }

  searchQuizzes(reference: string, pageSize: number, pageNumber: number) {
    return this.apiService.get(`quizzes/search?reference=${reference}&pageSize=${pageSize}&pageNumber=${pageNumber}`)
  }

  updateQuiz(title: string, description: string, expires: boolean, expiresInSeconds: number, difficulty: string, theme: string , quizId: string) 
  {
    return this.apiService.put(`quizzes/${quizId}`, { title, description, expires, expiresInSeconds, difficulty, theme })
  }
}
