
using Business.Base;
using Business.Users.Records;

namespace Business.Users.Interfaces;

public interface IUserService
{
    public Resultado CriarFuncionario(UserRequestDto dto);
    public Resultado MostrarFuncionario(int id);
}
