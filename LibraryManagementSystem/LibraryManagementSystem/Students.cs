using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace LibraryManagementSystem
{
    public class Students : IStudents
    {
        public  int Add_Student_Details()
        {
            string query = "select * from Student_details";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();
            row["name"] = AnsiConsole.Ask<string>("[red]Enter the Student_Name:[/]");
            row["email"] = AnsiConsole.Ask<string>("[red]Enter the Email:[/]");
            row["Address"] = AnsiConsole.Ask<string>("[red]Enter the Address:[/]");
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
           int count= adp.Update(ds);
          AnsiConsole.MarkupLine("[Blue] Inserted Student Details Successfully[/]");
            return count;
        }


        public  int Edit_Student_Details()
        {
            int id = AnsiConsole.Ask<int>("[red]Enter the Student Reg_Id:[/]");
            string query = $"select * from Student_details where reg_id={id}";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                row["reg_id"] = AnsiConsole.Ask<int>("[red]Enter Updated Student Reg_Id:[/]");
                row["name"] = AnsiConsole.Ask<string>("[red]Enter  Updated Student Name:[/]");
                row["email"] = AnsiConsole.Ask<string>("[red]Enter Updated email:[/]");
                row["Address"] = AnsiConsole.Ask<string>("[red]Enter Updated Address :[/]");
                SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
                adp.Update(ds);
                AnsiConsole.MarkupLine("[blue]Updated Student Details successfullly[/] ");
            }
            else
            {
                AnsiConsole.MarkupLine("Enter the Valid Id To Update Student Details");
            }
            return ds.Tables[0].Rows.Count;
        }

        public  int Delete_Student_Details()
        {
            int id = AnsiConsole.Ask<int>("[red]Enter the Student Reg_id :[/]");
            string query = $"select * from Student_details where reg_id={id}";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Rows[0].Delete();
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                AnsiConsole.MarkupLine("[blue] Deleted Book Details According to Given ID[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[blue]Enter the Valid Id To Delete Student Details[/]");
            }
            return ds.Tables[0].Rows.Count;
        }


        public  void Search_Student_Based_on_Reg_id()
        {
            int reg_id = AnsiConsole.Ask<int>("[red]Enter the Student Reg_id :[/]");
            //string query = $"select Student_details.reg_id,Student_details.name,Student_details.email,Student_details.Address,Issue_book.book_id,Issue_book.IssueDate from Student_details inner join Issue_book on Student_details.reg_id = {reg_id}";
            string query = $"select * from student_details where reg_id={reg_id}";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Table table;
            if (ds.Tables[0].Rows.Count > 0)
            {
                table = new Table();
                table.AddColumn("reg_Id");
                table.AddColumn("Student_Name");
                table.AddColumn("Student_Mail");
                table.AddColumn("Address");
              //  table.AddColumn("Book_Id");
              //  table.AddColumn("Issued_Date");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());
                }
                AnsiConsole.Write(table);
            }
            else
            {

                AnsiConsole.MarkupLine("[red] Enter the valid Id for Searching the Students[/]");
            }

        }
        public void Count_The_Students_Who_Have_Books()
        {
            string query = $"select * from Issue_book ";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Table table;
            if (ds.Tables[0].Rows.Count > 0)
            {
                table = new Table();
                table.AddColumn("Book_Id");
                table.AddColumn("Reg_Id");
                table.AddColumn("Issue_Date");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                }
                AnsiConsole.Write(table);
                Console.WriteLine("");
                AnsiConsole.MarkupLine($"[blue] Students Who Have Book = {ds.Tables[0].Rows.Count} [/] ");
            }
            else
            {
                AnsiConsole.MarkupLine("[blue] Table Contain Zero Data[/]");
            }
        }
    }
}
  
