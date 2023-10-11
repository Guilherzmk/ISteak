using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Products
{
    public class Product
    {
        public Guid? Id { get; set; }
        public int? Code { get; set; }
        public string? Name { get; set; }
        public string? Note {  get; set; }
        public double? Price { get; set; }
        public int? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public int? Quantity { get; set; }
        public byte? Image {  get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? CreationUserId { get; set; }
        public string? CreationUserName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public Guid? ChangeUserId { get; set; }
        public string? ChangeUserName { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public Guid? ExclusionUserId { get; set; }
        public string? ExclusionUserName { get; set;}
        public int? RecordStatus { get; set; }
        public string? RecordStatusName { get; set; }
    }
}

