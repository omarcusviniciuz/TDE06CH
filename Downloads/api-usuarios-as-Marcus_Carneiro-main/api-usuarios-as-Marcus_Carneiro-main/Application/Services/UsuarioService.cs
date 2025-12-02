using Application.Interfaces;
using Application.DTOs;

namespace Application.Services;

public class UsuarioService : IUsuarioService
{
    public IUsuarioRepository _repo;
    
    public UsuarioService(IUsuarioRepository repository)
    {
        _repo = repository;
    }
    
    public async Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct)
    {
        var usuarios = await _repo.GetAllAsync(ct);
        return usuarios.Select(MappingExtensions.ToReadDto);
    }

    public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct)
    {
        var usuario = await _repo.GetByIdAsync(id, ct);
        return usuario == null ? null : MappingExtensions.ToReadDto(usuario);
    }

    public async Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct)
    {
        var usuario = UsuarioFactory.Criar(dto.Nome, dto.Email, dto.Telefone, dto.DataNascimento);

        await _repo.AddAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);

        return MappingExtensions.ToReadDto(usuario);
    }

    public async Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
    {
        var usuario = await _repo.GetByIdAsync(id, ct);
        if (usuario == null)
            throw new Exception("Usuário não encontrado");

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Telefone = dto.Telefone;
        usuario.DataNascimento = dto.DataNascimento;

        await _repo.UpdateAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);

        return MappingExtensions.ToReadDto(usuario);
    }

       public async Task<bool> RemoverAsync(int id, CancellationToken ct = default)
    {
        var usuario = await _repo.GetByIdAsync(id, ct);

        if (usuario == null)
        {
            return false;
        }

        await _repo.RemoveAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct)
    {
        return await _repo.EmailExistsAsync(email, ct);
    }
}