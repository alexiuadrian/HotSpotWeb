using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.Payload
{
    public class Payload
    {
        public string Name { get; set; }
        public bool Is_available { get; set; }
        public bool Requires_admin { get; set; }
        public string[] Flags { get; set; }

        public Payload(string name, bool is_available, bool requires_admin, string[] flags)
        {
            Name = name;
            Is_available = is_available;
            Requires_admin = requires_admin;
            Flags = flags;
        }

        public Payload()
        {
        }

        public override string ToString()
        {
            return $"Name: {Name}, Is_available: {Is_available}, Requires_admin: {Requires_admin}, Flags: {Flags}";
        }

        // function for converting into json (the names must be lowercase)
        public string ToJson()
        {
            return $"{{\"name\": \"{Name}\", \"is_available\": {Is_available.ToString().ToLower()}, \"requires_admin\": {Requires_admin.ToString().ToLower()}, \"flags\": {Flags}}}";
        }
    }
}
