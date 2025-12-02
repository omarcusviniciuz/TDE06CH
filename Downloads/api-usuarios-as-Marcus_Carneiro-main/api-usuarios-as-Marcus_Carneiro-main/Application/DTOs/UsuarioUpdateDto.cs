namespace Application.DTOs;
public  record  UsuarioUpdateDto(
    int Id,
    string Nome,
    string Email,
    DateTime DataNascimento,
    string?Telefone,
    bool Ativo
);