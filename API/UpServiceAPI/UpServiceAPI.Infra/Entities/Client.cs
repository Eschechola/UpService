using FluentValidation;

namespace UpServiceAPI.Infra.Entities
{
    public class Client : AbstractValidator<Client>
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Telephone { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public int MountNotes { get; set; }
        public double SumNotes  { get; set; }
        public double Ranking { get; set; }

        #endregion


        #region Model Validate

        public Client()
        {
            RuleFor(v => v)
                .NotNull()
                .WithMessage("Os dados não podem ser vazios.");



            RuleFor(v => v.Type)
                .NotEmpty()
                .WithMessage("O tipo de cliente deve ser inserido")

                .Must(v => {
                    if (v == "SS" || v == "PS")
                        return true;
                    else
                        return false;
                })
                .WithMessage(v => $"O tipo {v.Type} não é valido.");




            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("O nome deve ser informado.")

                .MinimumLength(3)
                .WithMessage("O nome deve conter no mínimo 3 caracteres.")

                .MaximumLength(80)
                .WithMessage("O nome deve conter no máximo 80 caracteres.")


                .Matches("^[A-Za-z ]+$")
                .WithMessage("O nome deve conter somente letras e espaços.");




            RuleFor(v => v.Password)
                .NotEmpty()
                .WithMessage("A senha deve ser informada.")

                .MinimumLength(6)
                .WithMessage("A senha deve conter no mínimo 6 caracteres.")

                .MaximumLength(10)
                .WithMessage("A senha deve conter no máximo 10 caracteres.");




            RuleFor(v => v.Email)
                .NotEmpty()
                .WithMessage("O email deve ser informado.")

                .MinimumLength(10)
                .WithMessage("O email deve conter no mínimo 10 caracteres.")

                .MaximumLength(120)
                .WithMessage("O email deve conter no máximo 120 caracteres.")

                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("O endereço de email é inválido.");



            RuleFor(v => v.CPF)
                .NotEmpty()
                .WithMessage("O CPF deve ser informado.")

                .MinimumLength(11)
                .WithMessage("O CPF deve conter no mínimo 11 caracteres.")

                .MaximumLength(14)
                .WithMessage("O email deve conter no máximo 14 caracteres.");




            RuleFor(v => v.Telephone)
                .NotEmpty()
                .WithMessage("O Telefone deve ser informado.")

                .MinimumLength(8)
                .WithMessage("O CPF deve conter no mínimo 8 caracteres.")

                .MaximumLength(15)
                .WithMessage("O telefone deve conter no máximo 15 caracteres.");




            RuleFor(v => v)
               .NotNull()
               .WithMessage("Os dados não podem ser vazios.");




            RuleFor(v => v.Country)
                .NotEmpty()
                .WithMessage("O País deve ser informado.")

                .MinimumLength(5)
                .WithMessage("O País deve ter no mínimo 5 caracteres.")

                .MaximumLength(60)
                .WithMessage("O País deve ter no máximo 60 caracteres.");




            RuleFor(v => v.State)
                .NotEmpty()
                .WithMessage("O Estado deve ser informado.")

                .MinimumLength(5)
                .WithMessage("O Estado deve ter no mínimo 5 caracteres.")

                .MaximumLength(90)
                .WithMessage("O Estado deve ter no máximo 90 caracteres.");




            RuleFor(v => v.City)
                .NotEmpty()
                .WithMessage("A Cidade deve ser informado.")

                .MinimumLength(3)
                .WithMessage("A Cidade deve ter no mínimo 3 caracteres.")

                .MaximumLength(90)
                .WithMessage("A Cidade deve ter no máximo 90 caracteres.");




            RuleFor(v => v.ZipCode)
                .NotEmpty()
                .WithMessage("O CEP deve ser informado.")

                .MinimumLength(8)
                .WithMessage("O CEP deve ter no mínimo 8 caracteres.")

                .MaximumLength(90)
                .WithMessage("O CEP deve ter no máximo 15 caracteres.");




            RuleFor(v => v.Street)
                .NotEmpty()
                .WithMessage("A Rua deve ser informada.")

                .MinimumLength(3)
                .WithMessage("A Rua deve ter no mínimo 3 caracteres.")

                .MaximumLength(150)
                .WithMessage("A rua deve ter no máximo 150 caracteres.");




            RuleFor(v => v.HomeNumber)
                .NotEmpty()
                .WithMessage("O Número deve ser informado.");
        }

        #endregion
    }
}
