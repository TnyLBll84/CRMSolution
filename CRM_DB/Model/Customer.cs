using CRM_DB.Model;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB.Model
{

    internal class Customer
    {

        #region Public Instance Members (Properties)
        [Key]
        public int CustId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        #endregion

    }
}




