using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace WpfApp2.Entity
{
    public class Department
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public override string ToString()
        {
            return Id.ToString()[..5] + "... " + Name;
        }
    }
}
