using System;
using System.Collections.Generic;

namespace Business.Users.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Sobrenome { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string Setor { get; set; } = null!;

    public decimal SalarioBruto { get; set; }

    public DateOnly DataAdmissao { get; set; }

    public bool DescontoPlanoSaude { get; set; }

    public bool DescontoPlanoDental { get; set; }

    public bool DescontoValeTransporte { get; set; }

    public Usuario() { }

    public Usuario(string nome, string sobrenome, string documento, string setor, decimal salarioBruto, DateOnly dataAdmissao, bool descontoPlanoSaude, bool descontoPlanoDental, bool descontoValeTransporte)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Documento = documento;
        Setor = setor;
        SalarioBruto = salarioBruto;
        DataAdmissao = dataAdmissao;
        DescontoPlanoSaude = descontoPlanoSaude;
        DescontoPlanoDental = descontoPlanoDental;
        DescontoValeTransporte = descontoValeTransporte;
    }
}
