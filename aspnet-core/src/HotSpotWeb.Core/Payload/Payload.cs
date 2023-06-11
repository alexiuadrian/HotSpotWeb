using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.Payload
{
    public class Payload
    {
        public string name { get; set; }
        public bool is_available { get; set; }
        public bool requires_admin { get; set; }
        public string[] flags { get; set; }

        public Payload(string _name, bool _is_available, bool _requires_admin, string[] _flags)
        {
            name = _name;
            is_available = _is_available;
            requires_admin = _requires_admin;
            flags = _flags;
        }

        public Payload()
        {
        }
    }
}
