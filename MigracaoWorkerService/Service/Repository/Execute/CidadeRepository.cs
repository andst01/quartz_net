using Amazon.Runtime.Internal.Transform;
using Dapper;
using Microsoft.Extensions.Configuration;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using System.Data.SqlClient;

namespace MigracaoWorkerService.Service.Repository.Execute
{
    public class CidadeRepository : ICidadeRepository
    {

        private IConfiguration _configuration;

        private string _conn { get; set; }

        public CidadeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conn = $"Server=DESKTOP-1GGA1R2\\SQLEXPRESS;Database=SGAGNT;User Id=anderson;Password=123456;";
            //_conn = "Data Source=DESKTOP-1GGA1R2\\SQLEXPRESS;Initial Catalog=SGAGNT;Integrated Security=SSPI;Trusted_Connection=True;MultipleActiveResultSets=true";
                //_configuration.GetConnectionString("DefaultConnection");
        }
        // string _conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public async Task<List<Cidade>> Listar()
        {

            using (var db = new SqlConnection(_conn))
            {
                await db.OpenAsync();
                var query = @"SELECT CIDA_ID
                              ,CIDA_NOME
                              ,CIDA_ID_MICROREGIAO
                              ,CIDA_ID_ESTADO
                          FROM CIDADE";
                //var query = "Select Top 10 ClienteId,Nome,Idade,Pais From Clientes";
                var cidades = await db.QueryAsync<Cidade>(query);

                return cidades.ToList();
                
            }
        }

        public async Task<List<Cidade>> ListarComParametros(int index, int limit)
        {

            using (var db = new SqlConnection(_conn))
            {
                await db.OpenAsync();
                var query = @"SELECT ROW_ID,
                               CIDA_ID,
                               CIDA_NOME,
                               CIDA_ID_ESTADO,
                               CIDA_ID_MICROREGIAO,
                               QTD
                        FROM
                          (SELECT CIDA_ID,
                                  CIDA_NOME,
                                  CIDA_ID_ESTADO,
                                  CIDA_ID_MICROREGIAO,
                                  ROW_NUMBER() OVER(
                                                    ORDER BY CIDA_ID ASC) AS ROW_ID,

                             (SELECT COUNT(*)
                              FROM CIDADE) AS QTD
                           FROM CIDADE
                           GROUP BY CIDA_ID,
                                    CIDA_NOME,
                                    CIDA_ID_ESTADO,
                                    CIDA_ID_MICROREGIAO) CIDADE
                        WHERE ROW_ID BETWEEN @index AND @limit";
                //var query = "Select Top 10 ClienteId,Nome,Idade,Pais From Clientes";
                var dictionary = new Dictionary<string, object>() { { "@index", index }, { "@limit", limit } };
                var parameters = new DynamicParameters(dictionary);

                var cidades = await db.QueryAsync<Cidade>(query, parameters);

                return cidades.ToList();

            }
        }
    }
}
