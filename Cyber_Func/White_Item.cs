using System;
using System.Collections.Generic;
using System.Text;

namespace Cyber_Func
{
    class White_Item
    {
        string keyHash;
        string name;
        string version;

        internal White_Item(string keyHash, string name, string version)
        {
            this.keyHash = keyHash;
            this.name = name;
            this.version = version;
        }

        internal string GetKeyHash()
        {
            return this.keyHash;
        }
        internal string GetName()
        {
            return this.name;
        }
        
        internal string GetVersion()
        {
            return this.version;
        }
        internal List<string> GetItemArray()
        {
            List<string> tmp = new List<string>() { this.keyHash, this.name, this.version };
            return tmp;
        }

    }
}
