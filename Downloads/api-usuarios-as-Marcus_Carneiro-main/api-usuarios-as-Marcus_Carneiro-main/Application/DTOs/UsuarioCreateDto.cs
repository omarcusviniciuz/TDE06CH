namespace Application.DTOs;
public record UsuarioCreateDto(
    int Id,
    string Nome,
    string Email,
    string Senha,
    DateTime DataNascimento,
    string?Telefone
);