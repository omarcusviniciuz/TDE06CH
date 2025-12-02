using Application.DTOs;

namespace Application.Services;

public static class MappingExtensions
{
    public static UsuarioReadDto? ToReadDto(this Usuario u)
    {
   
        if (u == null) return null;    
    
            return new UsuarioReadDto(
                Id: u.Id,                
                Nome: u.Nome,  
                Email: u.Email,         
                Telefone: u.Telefone,   
                DataNascimento: u.DataNascimento,        
                Ativo: u.Ativo,       
                DataCriacao: u.DataCriacao
              
            );
    }
}
