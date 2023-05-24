using Spectre.Console;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Library Management System").Centered().Color(Color.White));
            Books sc1 = new Books();
            Students sc2 = new Students();
            string username = AnsiConsole.Ask<string>("[yellow]Enter Username:[/]");
            string password = AnsiConsole.Ask<string>("[yellow]Enter Password:[/]");
            try
            {
                string query = ($"select * from User_details where username='{username}' and UserPassword ='{password}'");
                DataSet ds = new DataSet();
                SqlDataAdapter adp = Connectionclass.GetAdapter(query);
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (true)
                    {
                        var ch = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                 .Title("[red] Select your choice : [/]")
                                 .AddChoices(new[]
                                 {
                        "Add_Book_Details",
                        "Edit_Book_Details",
                        "Delete_Book_Details",
                        "Add_Student _Details",
                        "Edit_Student_Details",
                        "Delete_Student_Details",
                        "Issue_Book",
                        "Return_Book",
                        "Search_Book_Based_on_Author",
                        "Search_Student_Based_on_Reg_id",
                        "Count_The_Students_Who_Have_Books"

                                 }));
                        switch (ch)
                        {
                            case "Add_Book_Details":
                                {
                                    sc1.Add_Book_Details();
                                    break;

                                }
                            case "Edit_Book_Details":
                                {
                                    sc1.Edit_Book_Details();
                                    break;
                                }
                            case "Delete_Book_Details":
                                {
                                    sc1.Delete_Book_Details();
                                    break;
                                }
                            case "Add_Student _Details":
                                {
                                    sc2.Add_Student_Details();
                                    break;
                                }
                            case "Edit_Student_Details":
                                {
                                    sc2.Edit_Student_Details();
                                    break;
                                }
                            case "Delete_Student_Details":
                                {
                                    sc2.Delete_Student_Details();
                                    break;
                                }
                            case "Issue_Book":
                                {
                                    sc1.Issue_Book();
                                    break;
                                }
                            case "Return_Book":
                                {
                                    sc1.Return_Book();
                                    break;
                                }
                            case "Search_Book_Based_on_Author":
                                {
                                    sc1.Search_Book_Based_on_Author();
                                    break;
                                }
                            case "Search_Student_Based_on_Reg_id":
                                {
                                    sc2.Search_Student_Based_on_Reg_id();
                                    break;
                                }
                            case "Count_The_Students_Who_Have_Books":
                                {
                                    sc2.Count_The_Students_Who_Have_Books();
                                    break;
                                }

                        }
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red] Invalid Username and Password[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red] {ex.Message} [/]");
            } 
        
        }
    }
}
               

    
