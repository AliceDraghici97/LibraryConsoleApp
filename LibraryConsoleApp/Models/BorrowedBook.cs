using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsoleApp.Models
{
    public class BorrowedBook
    {
        public string ISBN { get; set; }
        public DateTime LendingDateTime { get; set; }
        public int Quantity { get; set; }
    }
}
