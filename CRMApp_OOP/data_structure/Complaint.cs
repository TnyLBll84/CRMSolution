using System;
using System.Text;
using System.Threading;

namespace CRMApp_OOP
{
    internal enum Status { Opened, Closed, Resolved }

    internal delegate void Notify(string message);

    internal abstract class Complaint

    {
        #region Events
        public event Notify ComplainStatusChanged;
        #endregion

        private static int nextComplaintID = 0;

        protected Complaint(string type)
        {
            Id = Interlocked.Increment(ref nextComplaintID);
            Status = Status.Opened;
            ComplaintType = type;
        }

        protected Complaint(int customerID, string description, ILogger logger, string type) : this(type)
        {
            CustomerId = customerID;
            Description = description;
            Logger = logger;
        }

        protected int Id { get; set; }

        public int CustomerId { get; protected set; }

        protected Status Status { get; set; }

        protected string Description { get; set; } = string.Empty;

        protected ILogger Logger { get; set; }

        public string ComplaintType { get; protected set; }
        #region Methods (Behavior)
        public abstract void UpdateComplaintStatus(Status status);

        protected void OnComplaintStatusChanged(string msg)
        {
            ComplainStatusChanged.Invoke(msg);
        }

        public virtual void GetComplaintDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n*************************************");
            sb.AppendLine($"Complaint ID: {Id}");
            sb.AppendLine($"Customer ID: {CustomerId}");
            sb.AppendLine($"Complaint Type: {ComplaintType}");
            sb.AppendLine($"Status : {Status}");
            sb.AppendLine($"Description : {Description}");
            sb.AppendLine("*************************************\n");

            Console.WriteLine(sb.ToString());
        }
        #endregion
    }

    internal class ProductComplaint : Complaint
    {
        public int ProductId { get; private set; }

        public ProductComplaint(int customerID, string description, int productId, ILogger logger)
            : base(customerID, description, logger, "Product Complaint")
        {
            ProductId = productId;
        }

        public override void UpdateComplaintStatus(Status status)
        {
            Status = status;
            Logger.LogInfo($"Product Complaint #{Id} status updated to {Status}");

            OnComplaintStatusChanged($"Complaint# {this.Id} status has been updated to {this.Status}");
        }

        public override void GetComplaintDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n*************************************");
            sb.AppendLine($"Complaint ID: {Id}");
            sb.AppendLine($"Customer ID: {CustomerId}");
            sb.AppendLine($"Complaint Type: {ComplaintType}");
            sb.AppendLine($"Product ID: {ProductId}");
            sb.AppendLine($"Status : {Status}");
            sb.AppendLine($"Description : {Description}");
            sb.AppendLine("*************************************\n");

            Console.WriteLine(sb.ToString());
        }
    }

    internal class ServiceComplaint : Complaint
    {
        public ServiceComplaint(int customerID, string description, ILogger logger)
            : base(customerID, description, logger, "Service Complaint")
        {
        }

        public override void UpdateComplaintStatus(Status status)
        {
            Status = status;
            Logger.LogError($"Service Complaint #{Id} status updated to {Status}");
        }

        public override void GetComplaintDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n*************************************");
            sb.AppendLine($"Complaint ID: {Id}");
            sb.AppendLine($"Customer ID: {CustomerId}");
            sb.AppendLine($"Complaint Type: {ComplaintType}");
            sb.AppendLine($"Status : {Status}");
            sb.AppendLine($"Description : {Description}");
            sb.AppendLine("*************************************\n");

            Console.WriteLine(sb.ToString());
        }
    }
}
