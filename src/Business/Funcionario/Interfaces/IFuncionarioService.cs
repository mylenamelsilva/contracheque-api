﻿
using Business.Base;
using Business.Funcionario.Records;
using Business.Users.Records;

namespace Business.Users.Interfaces;

public interface IFuncionarioService
{
    public Resultado CriarFuncionario(UserRequestDto dto);
    public Resultado MostrarFuncionario(int id);
    public Resultado RemoverFuncionario(int id);
    public Resultado AtualizarFuncionario(int id, AtualizarInformacoesRequestDto dto);
    public FuncionarioInformacaoExtratoDto? RetornarInformacoesExtrato(int id);
}
