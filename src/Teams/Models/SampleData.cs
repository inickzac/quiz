using System;
using System.Linq;
using Teams.Data;
using Teams.Domain;

public static class SampleData
{
	public static void Initialize(ApplicationDbContext context)
	{
		if (!context.SingleSelectionQuestions.Any())
		{
			context.SingleSelectionQuestions.AddRange(
			new SingleSelectionQuestion("What is the most spoken language in the world?"),
			new SingleSelectionQuestion("What is the name of the most ancient continent?"),
			new SingleSelectionQuestion("Who invented the first telephone in 1876?"),
			new SingleSelectionQuestion("Who was Albert Einstein?")
			);

			context.MultipleAnswerQuestions.AddRange(
			new MultipleAnswerQuestion("Chinese"),
			new MultipleAnswerQuestion("Pangea"),
			new MultipleAnswerQuestion("Graham Bell"),
			new MultipleAnswerQuestion("physicist")

			);

			context.SaveChanges();
		}
	}
}
