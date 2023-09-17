using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.User
{
    public class User
    {
        public Guid? Id { get; set; }
        public int? Code { get; set; }
        public string? Name { get; set; }
        public string? AccessKey { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? ProfileId { get; set; }
        public string? ProfileName { get; set; }
        public int? AccessCount { get; set; }
        public string? Note { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? CreationUserId { get; set; }
        public string? CreationUserName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public Guid? ChangeUserId { get; set; }
        public string? ChangeUserName { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public Guid? ExclusionUserId { get; set; }
        public string? ExclusionUserName { get; set; }
        public int? RecordStatus { get; set; }
        public string? RecordStatusName { get; set; }
        public string? Token { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
