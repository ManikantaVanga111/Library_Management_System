using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
   public class StudentService 
    {
       private IStudents _students;
       public StudentService(IStudents students)
       {
        _students = students;
        }
        public int Data_inserted()
        {
          return  _students.Add_Student_Details();
        }
        public int  Data_Deleted()
        {
           return  _students.Delete_Student_Details();
        }
        public int Data_Updated()
        {
            return _students.Edit_Student_Details();
        }
    }
}
