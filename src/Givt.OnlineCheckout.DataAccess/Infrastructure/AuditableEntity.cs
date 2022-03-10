using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Givt.OnlineCheckout.DataAccess.Infrastructure
{
    public abstract class AuditableEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
