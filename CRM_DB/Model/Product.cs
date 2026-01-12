using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class Product
    {
        #region public Properties (Data Members)
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public virtual List<Complaint> Complaints { get; set; }
        #endregion
    }
}
