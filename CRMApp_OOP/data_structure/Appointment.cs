using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_OOP
{
    internal class Appointment
    {
        #region pulbic Properties (Data Members)
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime AppointmentTime { get; set; }

        #endregion

        public Appointment(int customerID, DateTime appointmentTime)
        {
            this.Id = new Random().Next();
            this.CustomerId = customerID;
            this.AppointmentTime = appointmentTime;
        }
    }
}