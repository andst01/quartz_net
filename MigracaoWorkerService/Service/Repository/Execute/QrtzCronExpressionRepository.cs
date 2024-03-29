using Dapper;
using Microsoft.Extensions.Configuration;
using MigracaoWorkerService.Models;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using System.Data.SqlClient;

namespace MigracaoWorkerService.Service.Repository.Execute
{
    public class QrtzCronExpressionRepository : IQrtzCronExpressionRepository
    {
        private IConfiguration _configuration;

        private string _conn { get; set; }

        public QrtzCronExpressionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conn = $"Server=DESKTOP-1GGA1R2\\SQLEXPRESS;Database=QUARTZ;User Id=anderson;Password=123456;";
           // _conn = "Data Source=DESKTOP-1GGA1R2\\SQLEXPRESS;Initial Catalog=QUARTZ;Integrated Security=SSPI;Trusted_Connection=True;MultipleActiveResultSets=true";
                //_configuration.GetConnectionString("Quartz");
        }

        public async Task<int> Inserir(JobMetadata jobMetadata)
        {
            using (var db = new SqlConnection(_conn))
            {
                await db.OpenAsync();

                var query = @"INSERT INTO QRTZ_CRON_TRIGGERS
                              (JOB_NAME,JOB_CRON_EXPRESSION)
                              VALUES
                              (@JOB_NAME, @JOB_CRON_EXPRESSION)";

                var parametros = new DynamicParameters();
                parametros.Add("@JOB_NAME", jobMetadata.JobName, System.Data.DbType.String);
                parametros.Add("@JOB_CRON_EXPRESSION", jobMetadata.JobCronExpression, System.Data.DbType.String);

                var retorno = db.Execute(query, parametros);

                return retorno;
            }

        }

        public async Task<List<JobMetadata>> Listar()
        {
            using (var db = new SqlConnection(_conn))
            {
                await db.OpenAsync();

                //var parametros = new { JOB_NAME = nomeJob };

                var query = @"SELECT JOB_ID as JobId, 
                                     JOB_NAME as JobName, 
                                     JOB_CRON_EXPRESSION as JobCronExpression, 
                                     JOB_DATE as JobDate,
                                     JOB_ACTIVE as JobAtivo
                                     FROM QRTZ_CRON_TRIGGERS
                                     WHERE JOB_ACTIVE = 1";

                var retorno = await db.QueryAsync<JobMetadata>(query);

                return retorno.ToList();

            }
        }
            
    }
}
