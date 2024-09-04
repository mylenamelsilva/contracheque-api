
using Business.Users.Records;

namespace Business.Users.Interfaces;

public interface IUserService
{
    public int CriarFuncionario(UserRequestDto dto);
}
