using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class Complaint
    {
        #region public Properties (Data Members)
        [Key]
        public int ComplaintId { get; set; }

        public int CustomerId { get; set; }

        public string Description { get; set; }

        public string ComplaintType { get; set; }
        #endregion
    }
}
