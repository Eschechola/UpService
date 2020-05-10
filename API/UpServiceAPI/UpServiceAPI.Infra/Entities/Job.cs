using FluentValidation;
using System;

namespace UpServiceAPI.Infra.Entities
{
    public class Job : AbstractValidator<Job>
    {

        #region Properties

        public int Id { get; set; }
        public int FkIdClientJobRequester { get; set; }
        public int? FkIdClientJobProvider { get; set; }
        public string Hash { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime? ConclusionDate { get; set; }
        public double JobMaxValue { get; set; }
        public string State { get; set; }

        #endregion


        #region Model Validate

        public Job()
        {
            RuleFor(v => v)
                .NotNull()
                .WithMessage("Os dados não podem ser vazios.");




            RuleFor(v => v.FkIdClientJobRequester)
                .NotEmpty()
                .WithMessage("O id do cliente quem solicitou o serviço não pode ser nulo ou vazio.");




            RuleFor(v => v.Title)
                .NotEmpty()
                .WithMessage("O titulo do trabalho não pode ser nulo ou vazio.")

                .MinimumLength(10)
                .WithMessage("O título do trabalho deve ter no mínimo 10 caracteres.")

                .MaximumLength(90)
                .WithMessage("O título do trabalho deve ter no máximo 90 caracteres.");




            RuleFor(v => v.Description)
                .NotEmpty()
                .WithMessage("A descrição do trabalho não pode ser nulo ou vazio.")

                .MinimumLength(45)
                .WithMessage("A descrição do trabalho deve ter no mínimo 45 caracteres.")

                .MaximumLength(3000)
                .WithMessage("A descrição do trabalho deve ter no máximo 3000 caracteres.");




            RuleFor(v => v.JobMaxValue)
                .NotEmpty()
                .WithMessage("O valor máximo deve ser informado.")

                .Must(v =>
                {
                    if (v > 30 || v < 12000)
                        return true;
                    else
                        return false;
                })
                .WithMessage("O valor do serviço deve ser entre R$30,00 e R$12.000,00.");
        }

        #endregion
    }
}
