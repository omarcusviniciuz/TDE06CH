namespace Application.Services;

public static class UsuarioFactory
{
    public static Usuario Criar(string nome, string email, string Telefone, DateTime dataNascimento)
    {
          if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome é inválido...", nameof(nome));
        }
        
        if (string.IsNullOrWhiteSpace(email))
        {
               throw new ArgumentException("O Email é inválido...", nameof(email));
        }

        if (Telefone.Length < 9)
        {
             throw new ArgumentException("O Telefone é inválido...", nameof(Telefone));
        }
            if (dataNascimento > DateTime.Now.AddYears(-18))
        {
            throw new ArgumentException("Data de nascimento inválida.", nameof(dataNascimento));
        }

        Console.WriteLine("Usuario validado com sucesso! Criando...");
        
        Usuario UsuarioValido = new Usuario();
        UsuarioValido.Nome = nome;
        UsuarioValido.Telefone = Telefone;
        UsuarioValido.Email = email;
        UsuarioValido.DataNascimento = DateTime.Now;
        UsuarioValido.DataCriacao = DateTime.Now;
        return UsuarioValido;
    }
}
