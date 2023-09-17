using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Customer
{
    public class Customer
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public int PersonTypeCode { get; set; }
        public string? PersonTypeName { get; set; }
        public string? Identity { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreationUserId { get; set; }
        public string? CreationUserName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public Guid ChangeUserId { get; set; }
        public string? ChangeUserName { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public Guid ExclusionUserId { get; set; }
        public string? ExclusionUserName { get; set; }
        public int RecordStatusCode { get; set; }
        public string? RecordStatusName { get; set; }

        public Customer()
        {
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return this.Name;
        }


    }
}
