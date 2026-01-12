using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{

    internal class Customer
    {

        #region Public Instance Members (Properties)
        [Key]
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        //Navigation Properties (for Relationships(One to Many))
        public virtual List<Complaint> Complaints { get; set; }

        #endregion

    }
}




