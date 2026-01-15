using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class Login
    {
        #region Public Instance Members (Properties)

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // Navigational Property
        public virtual Customer Customer { get; set; }

        #endregion
    }
}
