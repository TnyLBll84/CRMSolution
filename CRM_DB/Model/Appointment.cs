using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class Appointment
    {
        #region pulbic Properties (Data Members)
        [Key]
        public int AppointmentId { get; set; }

        public int CustomerId { get; set; }

        public DateTime AppointmentTime { get; set; }

        #endregion
    }
}
