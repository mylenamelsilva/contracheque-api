
using Business.Base;
using Business.Users.Records;

namespace Business.Users.Interfaces;

public interface IUserService
{
    public Resultado CriarFuncionario(UserRequestDto dto);
    public Resultado MostrarFuncionario(int id);
    public Resultado AtualizarFuncionario(int id, AtualizarInformacoesRequestDto dto);
}
