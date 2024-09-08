using Business.Users.Model;
using Business.Users.Records;

namespace Business.Users.Interfaces;

public interface IUserRepository
{
    public int CriarFuncionario(Usuario req);
    public Usuario? RecuperarFuncionario(int id);
    public bool ExisteFuncionario(string documento);
    public int AtualizarFuncionario(int id, Usuario req);
    public int RemoverFuncionario(int id);
}
