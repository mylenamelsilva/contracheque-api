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
}
