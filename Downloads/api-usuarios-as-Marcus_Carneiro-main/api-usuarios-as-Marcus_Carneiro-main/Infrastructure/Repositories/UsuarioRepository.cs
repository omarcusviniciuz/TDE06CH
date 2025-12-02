using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken ct = default)
    {
        // TODO: retornar lista com AsNoTracking.
        
        return await _context.Usuarios.AsNoTracking().ToListAsync(ct);
            

    }

    public async Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        // TODO: usar FindAsync.
    
        return await _context.Usuarios.FindAsync(new object[] { id }, ct);
    }

    public async Task AddAsync(Usuario Usuario, CancellationToken ct = default)
    {
        // TODO: AddAsync(Usuario, ct)
        
        await _context.Usuarios.AddAsync(Usuario, ct);
    }

    public Task RemoveAsync(Usuario Usuario, CancellationToken ct = default)
    {
        // TODO: _context.Remove(Usuario)
        
        _context.Usuarios.Remove(Usuario);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        // TODO: _context.SaveChangesAsync(ct)

        await _context.SaveChangesAsync(ct);
    }

    public  Task UpdateAsync(Usuario Usuario, CancellationToken ct = default)
    {
        _context.Usuarios.Update(Usuario);
        return Task.CompletedTask;
    }

    public Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<int> IUsuarioRepository.SaveChangesAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
