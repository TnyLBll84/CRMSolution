using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class CSVLogRecord
    {
        public DateTime DateTime { get; set; }
        public string LogType { get; set; }
        public string Message { get; set; }
    }
    
}
