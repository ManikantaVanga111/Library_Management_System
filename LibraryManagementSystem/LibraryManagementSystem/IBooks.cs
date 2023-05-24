using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
  public interface IBooks
    {
        public int Add_Book_Details();
        public int Edit_Book_Details();
        public int Delete_Book_Details();
    }
}
