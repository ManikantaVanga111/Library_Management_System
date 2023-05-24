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
    public class Books : IBooks
    {
        public  int Add_Book_Details()
        {
            string query = ($"select * from Book_details");
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();
            row["book_name"] = AnsiConsole.Ask<string>("[red]Enter the BOOK_Name:[/]");
            row["author"] = AnsiConsole.Ask<string>("[red]Enter the BOOK_Author:[/]");
            row["Quantity"] = AnsiConsole.Ask<int>("[red]Enter the BOOK_Quantity:[/]");
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
           int count= adp.Update(ds);
            AnsiConsole.MarkupLine("[blue]Inserted Book Details Successfullly[/] ");
            return count;
        }
        public int Edit_Book_Details()
        {
            int id = AnsiConsole.Ask<int>("[red]Enter the BOOK_ID:[/]");
            string query = $"select * from  Book_details where book_id={id}";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                row["book_id"] = AnsiConsole.Ask<int>("[red]Enter the Updated BOOK_ID:[/]");
                row["book_name"] = AnsiConsole.Ask<string>("[red]Enter the Updated BOOK_Name:[/]");
                row["author"] = AnsiConsole.Ask<string>("[red]Enter the Updated BOOK_Author:[/]");
                row["Quantity"] = AnsiConsole.Ask<int>("[red]Enter the BOOK_Quantity:[/]");
                SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
                adp.Update(ds);
                AnsiConsole.MarkupLine("[blue]Updated Book Details successfullly[/] ");
            }
            else
            {
                AnsiConsole.MarkupLine("[blue]Enter the Valid Id To Update Book Details [/] ");
            }
            return ds.Tables[0].Rows.Count; 
        }
        public int  Delete_Book_Details()
        {
            int id = AnsiConsole.Ask<int>("[red]Enter the  Book_Id:[/]");
            string query = $"select * from  Book_details where book_id={id}";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Rows[0].Delete();
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                AnsiConsole.MarkupLine("[blue] Deleted Books According to Given ID[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[blue]Enter the Valid Id To Delete Book Details[/]");
            }
            return ds.Tables[0].Rows.Count;
        }
        public  void Search_Book_Based_on_Author()
        {

            String AN = AnsiConsole.Ask<string>("[red]Enter the  BOOK_Author:[/]");
            string query = $"select * from Book_details where author='{AN}'";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Table table;
            if (ds.Tables[0].Rows.Count > 0)
            {
                table = new Table();
                table.AddColumn("Book_Id");
                table.AddColumn("Book_Name");
                table.AddColumn("Author");
                table.AddColumn("Quantity");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());
                }
                AnsiConsole.Write(table);

            }
            else
            {
                AnsiConsole.MarkupLine("[red]Enter the valid Author Name To Search the Books[/]");
            }

        }
        public void Issue_Book()
        {
            int book_id = AnsiConsole.Ask<int>("[red]Enter the Book_Id :[/]");
            int reg_id = AnsiConsole.Ask<int>("[red]Enter the Student Reg_id :[/]");
            string query = $"select * from  Book_details where book_id={book_id}";
            string query4= $"select * from Student_details where reg_id={reg_id}";
            SqlDataAdapter adp4 = Connectionclass.GetAdapter(query4);
           DataSet ds9=new DataSet();
            adp4.Fill(ds9);
            if (ds9.Tables[0].Rows.Count>0)
            {
              SqlDataAdapter adp = Connectionclass.GetAdapter(query);
              DataSet ds = new DataSet();
              adp.Fill(ds);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    DataRow quantityRow = ds.Tables[0].Rows[0];
                    int quantity = Convert.ToInt32(quantityRow["Quantity"]);
                    if (quantity != 0)
                    {
                        SqlDataAdapter adp1 = Connectionclass.GetAdapter($"select * from Issue_book");
                        DataSet ds1 = new DataSet();
                        adp1.Fill(ds1);
                        var row = ds1.Tables[0].NewRow();
                        row["book_id"] = book_id;
                        row["reg_id"] = reg_id;
                        row["IssueDate"] = DateTime.Now;
                        ds1.Tables[0].Rows.Add(row);
                        SqlCommandBuilder cmd = new SqlCommandBuilder(adp1);
                        adp1.Update(ds1);
                        AnsiConsole.MarkupLine("[blue]successfully Issued book for student[/]");
                        SqlDataAdapter adp2 = Connectionclass.GetAdapter($"select * from  Book_details where book_id={book_id}");
                        DataSet ds2 = new DataSet();
                        adp2.Fill(ds2);
                        var row1 = ds2.Tables[0].Rows[0];
                        row1["Quantity"] = quantity - 1;
                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adp2);
                        adp2.Update(ds2);
                        AnsiConsole.MarkupLine("[blue] Successfully Quantity Updated[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[blue] Quantity Is Zero[/]");
                    }

                }
                else
                {
                    AnsiConsole.MarkupLine("[red] Enter the valid Id To Issue the Book For Student[/]");

                }
            }else
            {
                AnsiConsole.MarkupLine("[red] Enter the valid Id To Issue the Book For Student[/]");
            }
        }
        public void Return_Book()
        {
            int reg_id = AnsiConsole.Ask<int>("[red]Enter the Student Reg_id :[/]");
            int book_id = AnsiConsole.Ask<int>("[red]Enter the Book_Id :[/]");
            string query = $"select * from  Issue_book where reg_id ={reg_id} and book_id={book_id} ";
            SqlDataAdapter adp = Connectionclass.GetAdapter(query);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SqlDataAdapter adp1 = Connectionclass.GetAdapter($"select * from Issue_book where reg_id ={reg_id}");
                DataSet ds1 = new DataSet();
                adp1.Fill(ds1);
                ds1.Tables[0].Rows[0].Delete();
                SqlCommandBuilder cmdBuilder1 = new SqlCommandBuilder(adp1);
                adp1.Update(ds1);
                AnsiConsole.MarkupLine("[red] Successfully Deleted Issue Details[/]");
                SqlDataAdapter adp2 = Connectionclass.GetAdapter($"select * from Book_details where book_id={book_id}");
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2);
                DataRow quantityRow = ds2.Tables[0].Rows[0];
                int quantity = Convert.ToInt32(quantityRow["Quantity"]);
                quantityRow["Quantity"] = quantity + 1;
                SqlCommandBuilder cmdBuilder2 = new SqlCommandBuilder(adp2);
                adp2.Update(ds2);
                AnsiConsole.MarkupLine("[blue] Successfully Quantity Updated[/]");
            }
            else
            {

                AnsiConsole.MarkupLine("[red] Enter the valid Id To Return the Book From Student[/]");
            }
        }
    }
}
