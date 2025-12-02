using Application.DTOs;
using FluentValidation;

public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
{
    public UsuarioCreateDtoValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("O nome do Usuario é obrigatório")
            .MinimumLength(3)
            .WithMessage("O nome do Usuario não pode ter menos do que 3 caracteres")
            .MaximumLength(100)
            .WithMessage("O nome do Usuario não pode ter mais do que 100 caracteres");

        RuleFor(u => u.Senha)
            .MinimumLength(6)
            .WithMessage("A senha não pode ter menos de 6 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email do Usuario é obrigatório.")
            .EmailAddress()
            .WithMessage("O email informado é inválido");

        RuleFor(u => u.DataNascimento)  
            .NotEmpty()
            .WithMessage("Data de Nascimento do Usuario é obrigatório.");
            
        RuleFor(u => u.Telefone)
        .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
        .WithMessage("Telefone inválido. Use formato brasileiro, ex: (51) 99849-5783");
    }
}