using Application.DTOs;
using Application.Interfaces;
using FluentValidation;

public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
{
    private readonly IUsuarioRepository _repo;

    public UsuarioUpdateDtoValidator(IUsuarioRepository repo)
    {
        _repo = repo;

        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("O nome do Usuario é obrigatório")
            .MinimumLength(3).WithMessage("O nome não pode ter menos de 3 caracteres")
            .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email do Usuario é obrigatório.")
            .EmailAddress().WithMessage("O email informado é inválido")
            .MustAsync(EmailUnico).WithMessage("Este email já está cadastrado por outro usuário.");

        RuleFor(u => u.DataNascimento)
            .NotEmpty().WithMessage("Data de Nascimento é obrigatória.");

        RuleFor(u => u.Telefone)
            .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("Telefone inválido. Ex: (51) 99849-5783");
    }

    private async Task<bool> EmailUnico(UsuarioUpdateDto dto, string email, CancellationToken ct)
    {
        var usuarioExistente = await _repo.GetByEmailAsync(email, ct);

        if (usuarioExistente == null)
            return true;

        return usuarioExistente.Id == dto.Id;
    }
}