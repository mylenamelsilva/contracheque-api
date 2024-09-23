using Business.Base;
using Business.Funcionario.Records;
using Business.Users.Interfaces;
using Business.Users.Model;
using Business.Users.Records;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Users
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _userRepository;

        public FuncionarioService(IFuncionarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Resultado CriarFuncionario(UserRequestDto dto)
        {
            var temPropriedadeNula = VerificarSeTemPropriedadeNula(dto);

            if (temPropriedadeNula.Any())
            {
                return Resultado.Falha("Preencha todos os campos corretamente.", temPropriedadeNula);
            }

            if (VerificarSeJaExisteDocumento(dto.Documento))
            {
                return Resultado.Falha("Já existe um funcionário com esse documento", dto);
            }

            string[] fmts = new string[] { "dd/MM/yyyy" };
            var dataAdmissao = DateOnly.ParseExact(dto.DataAdmissao, fmts, CultureInfo.InvariantCulture);

            Usuario usuario = new(dto.Nome, 
                                  dto.Sobrenome,
                                  dto.Documento,
                                  dto.Setor, 
                                  dto.SalarioBruto, 
                                  dataAdmissao, 
                                  dto.DescontoPlanoSaude, 
                                  dto.DescontoPlanoDental, 
                                  dto.DescontoValeTransporte);

            var resultadoInsert = _userRepository.CriarFuncionario(usuario);

            return resultadoInsert == 1 ? Resultado.Sucesso("Funcionário criado.", resultadoInsert) : Resultado.Falha("Funcionário não criado.", resultadoInsert);
        }

        public Resultado MostrarFuncionario(int id)
        {
            if (id <= 0)
            {
                return Resultado.Falha("Id inválido.");
            }

            var usuario = _userRepository.RecuperarFuncionario(id);
            
            if (usuario == null)
            {
                return Resultado.Sucesso(null, null);
            }


            var dataAdmissao = usuario.DataAdmissao.ToString();
            var salarioBrutoBr = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", usuario.SalarioBruto);

            UserResponseDto usuarioDto = new(usuario.Id,
                                             usuario.Nome,
                                             usuario.Sobrenome,
                                             usuario.Documento,
                                             usuario.Setor,
                                             salarioBrutoBr,
                                             dataAdmissao,
                                             usuario.DescontoPlanoSaude,                                
                                             usuario.DescontoPlanoDental,                                
                                             usuario.DescontoValeTransporte);

            return Resultado.Sucesso(null, usuarioDto);
        }

        public Resultado AtualizarFuncionario(int id, AtualizarInformacoesRequestDto dto)
        {
            var temPropriedadeNula = VerificarSeTemPropriedadeNula(dto);

            if (temPropriedadeNula.Any())
            {
                return Resultado.Falha("Preencha todos os campos corretamente.", temPropriedadeNula);
            }

            var existeFuncionario = _userRepository.RecuperarFuncionario(id);

            if (existeFuncionario == null)
            {
                return Resultado.Falha("Não existe funcionário com esse id.", dto);
            }

            Usuario usuario = new(dto.Nome,
                                              dto.Sobrenome,
                                              existeFuncionario.Documento,
                                              dto.Setor,
                                              dto.SalarioBruto,
                                              existeFuncionario.DataAdmissao,
                                              dto.DescontoPlanoSaude,
                                              dto.DescontoPlanoDental,
                                              dto.DescontoValeTransporte);

            var resultadoUpdate = _userRepository.AtualizarFuncionario(id, usuario);

            return resultadoUpdate == 1 ? Resultado.Sucesso("Funcionário atualizado.", resultadoUpdate) : Resultado.Falha("Funcionário não atualizado.", resultadoUpdate);

        }

        public Resultado RemoverFuncionario(int id)
        {

            var existeFuncionario = _userRepository.RecuperarFuncionario(id);

            if (existeFuncionario == null)
            {
                return Resultado.Falha("Não existe funcionário com esse id.", id);
            }

            var resultadoDelete = _userRepository.RemoverFuncionario(id);

            return resultadoDelete == 1 ? Resultado.Sucesso("Funcionário removido.", resultadoDelete) : Resultado.Falha("Funcionário não removido.", resultadoDelete);
        }

        public FuncionarioInformacaoExtratoDto? RetornarInformacoesExtrato(int id)
        {
            var funcionario = _userRepository.RecuperarFuncionario(id);

            if (funcionario == null)
            {
                return null;
            }

            return new FuncionarioInformacaoExtratoDto(funcionario.SalarioBruto,
                                                       funcionario.DescontoPlanoSaude,
                                                       funcionario.DescontoPlanoDental,
                                                       funcionario.DescontoValeTransporte);
        }

        #region métodos privados

        private List<string> VerificarSeTemPropriedadeNula(object obj)
        {
            var propriedades = obj.GetType().GetProperties();
            List<string> propriedadesNulas = new();

            foreach (var propriedade in propriedades)
            {
                var valor = propriedade.GetValue(obj);

                var valorNuloString = valor is string str && string.IsNullOrEmpty(str);
                var valorNuloNumero = valor is decimal dec && dec == 0.0m;

                if (!valorNuloString && !valorNuloNumero) { continue; }

                propriedadesNulas.Add(propriedade.Name.ToString());
            }

            return propriedadesNulas;
        }

        private bool VerificarSeJaExisteDocumento(string documento)
        {
            return _userRepository.ExisteFuncionario(documento);
        }
        #endregion
    }
}
