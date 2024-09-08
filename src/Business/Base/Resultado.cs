using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public class Resultado
    {
        public bool Erro { get; }
        public string? Mensagem { get; }
        public object? Data {  get; }
        
        public Resultado(bool erro, string? mensagem, object? data)
        {
            Erro = erro;
            Mensagem = mensagem;
            Data = data;
        }

        public static Resultado Sucesso(string? mensagem = "", object? data = null) => new Resultado(false, mensagem, data);
        public static Resultado Falha(string? mensagem = "", object? data = null) => new Resultado(true, mensagem, data);
    }
}
