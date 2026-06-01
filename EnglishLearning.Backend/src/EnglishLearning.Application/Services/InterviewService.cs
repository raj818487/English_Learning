using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Services;

public sealed class InterviewService(IRepository<InterviewQuestion> interviewRepository, IUnitOfWork unitOfWork) : IInterviewService
{
	public async Task<InterviewQuestion> CreateAsync(InterviewQuestion interviewQuestion, CancellationToken cancellationToken = default)
	{
		var created = await interviewRepository.AddAsync(interviewQuestion, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);
		return created;
	}

	public Task<IReadOnlyList<InterviewQuestion>> ListAsync(CancellationToken cancellationToken = default) =>
		interviewRepository.ListAsync(cancellationToken);

	public Task<InterviewQuestion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
		interviewRepository.GetByIdAsync(id, cancellationToken);

	public async Task UpdateAsync(InterviewQuestion interviewQuestion, CancellationToken cancellationToken = default)
	{
		await interviewRepository.UpdateAsync(interviewQuestion, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(InterviewQuestion interviewQuestion, CancellationToken cancellationToken = default)
	{
		await interviewRepository.DeleteAsync(interviewQuestion, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
