using Business.Users.Model;

namespace Business.Users.Interfaces;

public interface IUserRepository
{
    public int CriarFuncionario(Usuario req);
    public Usuario? RecuperarFuncionario(int id);
}
