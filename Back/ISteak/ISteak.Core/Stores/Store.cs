using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.Stores
{
    public class Store
    {
        public Guid? Id { get; set; }
        public int? Code { get; set; }
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Identity { get; set; }
    }
}
