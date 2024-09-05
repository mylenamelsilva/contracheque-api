using Business.Users.Interfaces;
using Business.Users.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Users.Repository;

public class UserRepository : IUserRepository
{
    private string _connection;

    public UserRepository(DatabaseContext context)
    {
        _connection = context.Database.GetConnectionString();
    }

    public int CriarFuncionario(Usuario req)
    {
        string query = $@"
                                INSERT INTO [dbo].[Usuarios]
                                    ([Nome]
                                    ,[Sobrenome]
                                    ,[Documento]
                                    ,[Setor]
                                    ,[SalarioBruto]
                                    ,[DataAdmissao]
                                    ,[DescontoPlanoSaude]
                                    ,[DescontoPlanoDental]
                                    ,[DescontoValeTransporte])
                                VALUES
                                    (@Nome
                                    ,@Sobrenome
                                    ,@Documento
                                    ,@Setor
                                    ,@SalarioBruto
                                    ,@DataAdmissao
                                    ,@DescontoPlanoSaude
                                    ,@DescontoPlanoDental
                                    ,@DescontoValeTransporte)
         ";


        using (SqlConnection conn = new(_connection))
        {
            conn.Open();

            SqlTransaction transaction = conn.BeginTransaction();
            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = query,
                CommandType = System.Data.CommandType.Text,
                Transaction = transaction
            };

            command.Parameters.AddWithValue("@Nome", req.Nome);
            command.Parameters.AddWithValue("@Sobrenome", req.Sobrenome);
            command.Parameters.AddWithValue("@Documento", req.Documento);
            command.Parameters.AddWithValue("@Setor", req.Setor);
            command.Parameters.AddWithValue("@SalarioBruto", req.SalarioBruto);
            command.Parameters.AddWithValue("@DataAdmissao", req.DataAdmissao);
            command.Parameters.AddWithValue("@DescontoPlanoSaude", req.DescontoPlanoSaude);
            command.Parameters.AddWithValue("@DescontoPlanoDental", req.DescontoPlanoDental);
            command.Parameters.AddWithValue("@DescontoValeTransporte", req.DescontoValeTransporte);

            var linhaAfetada = command.ExecuteNonQuery();

            if (linhaAfetada != 1) { 
                transaction.Rollback();
                return linhaAfetada;
            }

            transaction.Commit();
            return linhaAfetada;
        }
    }

    public Usuario? RecuperarFuncionario(int id)
    {
        string query = $@"
                        SELECT    
                             [Id] 
                            ,[Nome]
                            ,[Sobrenome]
                            ,[Documento]
                            ,[Setor]
                            ,[SalarioBruto]
                            ,[DataAdmissao]
                            ,[DescontoPlanoSaude]
                            ,[DescontoPlanoDental]
                            ,[DescontoValeTransporte]
                       FROM  [dbo].[Usuarios]
                       WHERE Id = @ID
         ";

        Usuario usuario = new();

        using (SqlConnection conn = new(_connection))
        {
            conn.Open();

            SqlCommand command = new()
            {
                Connection = conn,
                CommandText = query,
                CommandType = System.Data.CommandType.Text
            };

            command.Parameters.AddWithValue("@ID", id);

            var resultado = command.ExecuteReader();

            if (!resultado.HasRows)
            {
                return null;
            }

            while (resultado.Read())
            {
                usuario.Id = (int)resultado["Id"];
                usuario.Nome = (string)resultado["Nome"];
                usuario.Sobrenome = (string)resultado["Sobrenome"];
                usuario.Documento = (string)resultado["Documento"];
                usuario.Setor = (string)resultado["Setor"];
                usuario.SalarioBruto = (decimal)resultado["SalarioBruto"];
                usuario.DataAdmissao = DateOnly.FromDateTime((DateTime)resultado["DataAdmissao"]);
                usuario.DescontoPlanoSaude = (bool)resultado["DescontoPlanoSaude"];
                usuario.DescontoPlanoDental = (bool)resultado["DescontoPlanoDental"];
                usuario.DescontoValeTransporte = (bool)resultado["DescontoValeTransporte"];
            }

            return usuario;
        }
    }
}
