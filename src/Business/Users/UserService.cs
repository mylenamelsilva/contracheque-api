﻿using Business.Base;
using Business.Users.Interfaces;
using Business.Users.Model;
using Business.Users.Records;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Resultado CriarFuncionario(UserRequestDto dto)
        {
            var temPropriedadeNula = VerificarSeTemPropriedadeNula(dto);

            if (temPropriedadeNula.Any())
            {
                // retornar erro com uma lista das propriedades vazias
                return Resultado.Falha("Preencha todos os campos corretamente.", temPropriedadeNula);
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

            UserResponseDto usuarioDto = new(usuario.Id,
                                             usuario.Nome,
                                             usuario.Sobrenome,
                                             usuario.Documento,
                                             usuario.Setor,
                                             usuario.SalarioBruto,
                                             dataAdmissao,
                                             usuario.DescontoPlanoSaude,                                
                                             usuario.DescontoPlanoDental,                                
                                             usuario.DescontoValeTransporte);

            return Resultado.Sucesso(null, usuarioDto);
        }

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
    }
}
