using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Interfaces;

public interface IInterviewService
{
    Task<InterviewQuestion> CreateAsync(InterviewQuestion interviewQuestion, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InterviewQuestion>> ListAsync(CancellationToken cancellationToken = default);
    Task<InterviewQuestion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(InterviewQuestion interviewQuestion, CancellationToken cancellationToken = default);
    Task DeleteAsync(InterviewQuestion interviewQuestion, CancellationToken cancellationToken = default);
}
