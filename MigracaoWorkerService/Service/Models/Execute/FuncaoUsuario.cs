using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class FuncaoUsuario : IdentityUserRole<int>
    {
        
        public override int UserId { get; set; }
    
        public override int RoleId { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

    }
}
