using Dapper;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Repository.Execute
{
    public class EstadoRepository : IEstadoRepository
    {
        private string _conn { get; set; }

        public EstadoRepository()
        {
            _conn = $"Server=DESKTOP-1GGA1R2\\SQLEXPRESS;Database=SGAGNT;User Id=anderson;Password=123456;";
        }

        public async Task<List<Estado>> Listar()
        {

            using (var db = new SqlConnection(_conn))
            {
                await db.OpenAsync();
                var query = @"SELECT EST_ID
                              ,EST_SIGLA
                              ,EST_NOME
                              ,EST_ID_REGIAO,
							  ROW_NUMBER() OVER(
							  ORDER BY EST_ID ASC) AS ROW_ID
                          FROM ESTADO";
                //var query = "Select Top 10 ClienteId,Nome,Idade,Pais From Clientes";
                var retorno = await db.QueryAsync<Estado>(query);

                return retorno.ToList();

            }
        }
    }
}
