using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//(localdb)\MSSQLLocalDB
namespace MyThirdProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyThirdDatabase2ModelContainer context = new MyThirdDatabase2ModelContainer();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\See Sharp\MyThirdProject2\MyThirdProject2\ProjectThirdDatabase2.mdf'; Integrated Security = True";
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            DataSet dataset;

            int control;
            while (true)
            {
                control = 0;
                Console.WriteLine("Hello, this is Score DB manager. Please choose an action:");
                Console.WriteLine("1. View table contents.");
                Console.WriteLine("2. Insert into table.");
                Console.WriteLine("3. Delete from table.");
                Console.WriteLine("4. Update in table.");
                Console.WriteLine("5. Custom functions.");
                Console.WriteLine("6. Exit.");
                control = Convert.ToInt32(Console.ReadLine());
                switch (control)
                {
                    case 1:
                        Console.WriteLine("Choose table:");
                        Console.WriteLine("1. Games");
                        Console.WriteLine("2. Platforms");
                        Console.WriteLine("3. Players");
                        Console.WriteLine("4. Scores");
                        control = Convert.ToInt32(Console.ReadLine());
                        if (control == 1)
                        {
                            dataset = new DataSet();
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Name, Publisher, Release_Year FROM Games", connection);
                            dataAdapter.Fill(dataset, "Games");

                            Console.WriteLine("GAMES");
                            Console.WriteLine("Id  |  Name  |  Publisher  |  Release_Year");
                            foreach (DataRow info in dataset.Tables[0].Rows)
                            {
                                Console.WriteLine(info["Id"] + " | " + info["Name"] + " | " + info["Publisher"] + " | " + info["Release_Year"]);
                            }
                        }
                        else if (control == 2)
                        {
                            dataset = new DataSet();
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Device, Os FROM Platforms", connection);
                            dataAdapter.Fill(dataset, "Platforms");

                            Console.WriteLine("PLATFORMS");
                            Console.WriteLine("Id  |  Device  |  Os");
                            foreach (DataRow info in dataset.Tables[0].Rows)
                            {
                                Console.WriteLine(info["Id"] + " | " + info["Device"] + " | " + info["Os"]);
                            }
                        }
                        else if (control == 3) {
                            Console.WriteLine("PLAYERS");
                            Console.WriteLine("Id  |  Nickname");
                            foreach(var player in context.Players)
                            {
                                Console.WriteLine($"{player.Id} | {player.Nickname}");
                            }
                        }
                        else {
                            Console.WriteLine("SCORES");
                            Console.WriteLine("Id  |  Score Count  |  Time");
                            foreach (var score in context.Scores)
                            {
                                Console.WriteLine($"{score.Id} | {score.Count} | {score.Time}");
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("Choose table:");
                        Console.WriteLine("1. Games");
                        Console.WriteLine("2. Platforms");
                        Console.WriteLine("3. Players");
                        Console.WriteLine("4. Scores");
                        control = Convert.ToInt32(Console.ReadLine());
                        if (control == 1)
                        {
                            SqlCommand insert = new SqlCommand();
                            insert.Connection = connection;
                            insert.CommandType = CommandType.Text;
                            insert.CommandText = "INSERT INTO Games(Name, Publisher, Release_Year) VALUES (@One,@Two,@Three)";
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Name, Publisher, Release_Year FROM Games", connection);
                            insert.Parameters.Add(new SqlParameter("@One", SqlDbType.NVarChar, 50, "Name"));
                            insert.Parameters.Add(new SqlParameter("@Two", SqlDbType.NVarChar, 50, "Publisher"));
                            insert.Parameters.Add(new SqlParameter("@Three", SqlDbType.Int, 50, "Release_Year"));

                            dataAdapter.InsertCommand = insert;

                            dataset = new DataSet();
                            dataAdapter.Fill(dataset, "Games");

                            DataRow row = dataset.Tables[0].NewRow();
                            Console.WriteLine("Insert Name:");
                            row["Name"] = Console.ReadLine();
                            Console.WriteLine("Insert Publisher:");
                            row["Publisher"] = Console.ReadLine();
                            Console.WriteLine("Insert Release_Year:");
                            row["Release_Year"] = Convert.ToInt32(Console.ReadLine());
                            dataset.Tables[0].Rows.Add(row);
                            dataAdapter.Update(dataset.Tables[0]);

                            Console.WriteLine("Data inserted!");
                        }
                        else if (control == 2)
                        {
                            SqlCommand insert = new SqlCommand();
                            insert.Connection = connection;
                            insert.CommandType = CommandType.Text;
                            insert.CommandText = "INSERT INTO Platforms(Device, Os) VALUES (@One,@Two)";
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Device, Os FROM Platforms", connection);
                            insert.Parameters.Add(new SqlParameter("@One", SqlDbType.NVarChar, 50, "Device"));
                            insert.Parameters.Add(new SqlParameter("@Two", SqlDbType.NVarChar, 50, "Os"));

                            dataAdapter.InsertCommand = insert;

                            dataset = new DataSet();
                            dataAdapter.Fill(dataset, "Platforms");

                            DataRow row = dataset.Tables[0].NewRow();
                            Console.WriteLine("Insert Device:");
                            row["Device"] = Console.ReadLine();
                            Console.WriteLine("Insert Os:");
                            row["Os"] = Console.ReadLine();
                            dataset.Tables[0].Rows.Add(row);
                            dataAdapter.Update(dataset.Tables[0]);

                            Console.WriteLine("Data inserted!");
                        }
                        else if (control == 3)
                        {
                            Player player = new Player();
                            Console.WriteLine("Insert Nickname:");
                            player.Nickname = Console.ReadLine();

                            context.Players.Add(player);
                            context.SaveChanges();

                            Console.WriteLine("Data inserted!");
                        }
                        else {
                            Score score = new Score();
                            Console.WriteLine("Insert Count:");
                            score.Count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Insert Time:");
                            score.Time = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Insert Game Id:");
                            score.GameId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Insert Player Id:");
                            score.PlayerId = Convert.ToInt32(Console.ReadLine());

                            context.Scores.Add(score);
                            context.SaveChanges();

                            Console.WriteLine("Data inserted!");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Choose table:");
                        Console.WriteLine("1. Games");
                        Console.WriteLine("2. Platforms");
                        Console.WriteLine("3. Players");
                        Console.WriteLine("4. Scores");
                        control = Convert.ToInt32(Console.ReadLine());
                        if (control == 1)
                        {
                            SqlCommand insert = new SqlCommand();
                            insert.Connection = connection;
                            insert.CommandType = CommandType.Text;
                            insert.CommandText = "DELETE FROM Games WHERE Id = @Four";
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Name, Publisher, Release_Year FROM Games", connection);

                            insert.Parameters.Add(new SqlParameter("@Four", SqlDbType.Int, 50, "Id"));

                            dataAdapter.DeleteCommand = insert;

                            dataset = new DataSet();
                            dataAdapter.Fill(dataset, "Games");
                            dataset.Tables[0].PrimaryKey = new[] { dataset.Tables[0].Columns["Id"] };

                            Console.WriteLine("Id to Delete:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            dataset.Tables[0].Rows.Find(temp).Delete();

                            dataAdapter.Update(dataset.Tables[0]);
                            Console.WriteLine("Data deleted!");
                        }
                        else if (control == 2)
                        {
                            SqlCommand insert = new SqlCommand();
                            insert.Connection = connection;
                            insert.CommandType = CommandType.Text;
                            insert.CommandText = "DELETE FROM Platforms WHERE Id = @Four";
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Device, Os FROM Platforms", connection);

                            insert.Parameters.Add(new SqlParameter("@Four", SqlDbType.Int, 50, "Id"));

                            dataAdapter.DeleteCommand = insert;

                            dataset = new DataSet();
                            dataAdapter.Fill(dataset, "Platforms");
                            dataset.Tables[0].PrimaryKey = new[] { dataset.Tables[0].Columns["Id"] };

                            Console.WriteLine("Id to Delete:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            dataset.Tables[0].Rows.Find(temp).Delete();

                            dataAdapter.Update(dataset.Tables[0]);
                            Console.WriteLine("Data deleted!");
                        }
                        else if (control == 3)
                        {
                            Console.WriteLine("Id to Delete:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            context.Players.Remove(context.Players.FirstOrDefault(x => x.Id == temp));

                            context.SaveChanges();

                            Console.WriteLine("Data deleted!");
                        }
                        else {
                            Console.WriteLine("Id to Delete:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            context.Scores.Remove(context.Scores.FirstOrDefault(x => x.Id == temp));

                            context.SaveChanges();

                            Console.WriteLine("Data deleted!");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Choose table:");
                        Console.WriteLine("1. Games");
                        Console.WriteLine("2. Platforms");
                        Console.WriteLine("3. Players");
                        Console.WriteLine("4. Scores");
                        control = Convert.ToInt32(Console.ReadLine());
                        if (control == 1)
                        {
                            SqlCommand insert = new SqlCommand();
                            insert.Connection = connection;
                            insert.CommandType = CommandType.Text;
                            insert.CommandText = "UPDATE Games SET Name = @One, Publisher = @Two, Release_Year = @Three WHERE Id = @Four";
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Name, Publisher, Release_Year FROM Games", connection);

                            insert.Parameters.Add(new SqlParameter("@One", SqlDbType.NVarChar, 50, "Name"));
                            insert.Parameters.Add(new SqlParameter("@Two", SqlDbType.NVarChar, 50, "Publisher"));
                            insert.Parameters.Add(new SqlParameter("@Three", SqlDbType.Int, 50, "Release_Year"));
                            insert.Parameters.Add(new SqlParameter("@Four", SqlDbType.Int, 50, "Id"));

                            dataAdapter.UpdateCommand = insert;

                            dataset = new DataSet();
                            dataAdapter.Fill(dataset, "Games");
                            dataset.Tables[0].PrimaryKey = new[] { dataset.Tables[0].Columns["Id"] };

                            Console.WriteLine("Id to Update:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update Name:");
                            dataset.Tables[0].Rows.Find(temp)["Name"] = Console.ReadLine();
                            Console.WriteLine("Update Publisher:");
                            dataset.Tables[0].Rows.Find(temp)["Publisher"] = Console.ReadLine();
                            Console.WriteLine("Update Release_Year:");
                            dataset.Tables[0].Rows.Find(temp)["Release_Year"] = Convert.ToInt32(Console.ReadLine());
                        
                            dataAdapter.Update(dataset.Tables[0]);
                            Console.WriteLine("Data updated!");
                        }
                        else if (control == 2)
                        {
                            SqlCommand insert = new SqlCommand();
                            insert.Connection = connection;
                            insert.CommandType = CommandType.Text;
                            insert.CommandText = "UPDATE Platforms SET Device = @One, Os = @Two WHERE Id = @Four";
                            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Device, Os FROM Platforms", connection);

                            insert.Parameters.Add(new SqlParameter("@One", SqlDbType.NVarChar, 50, "Device"));
                            insert.Parameters.Add(new SqlParameter("@Two", SqlDbType.NVarChar, 50, "Os"));
                            insert.Parameters.Add(new SqlParameter("@Four", SqlDbType.Int, 50, "Id"));

                            dataAdapter.UpdateCommand = insert;

                            dataset = new DataSet();
                            dataAdapter.Fill(dataset, "Platforms");
                            dataset.Tables[0].PrimaryKey = new[] { dataset.Tables[0].Columns["Id"] };

                            Console.WriteLine("Id to Update:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update Device:");
                            dataset.Tables[0].Rows.Find(temp)["Device"] = Console.ReadLine();
                            Console.WriteLine("Update Os:");
                            dataset.Tables[0].Rows.Find(temp)["Os"] = Console.ReadLine();

                            dataAdapter.Update(dataset.Tables[0]);
                            Console.WriteLine("Data updated!");
                        }
                        else if (control == 3)
                        {
                            Console.WriteLine("Id to Update:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update Nickname:");
                            context.Players.FirstOrDefault(x => x.Id == temp).Nickname = Console.ReadLine();

                            context.SaveChanges();

                            Console.WriteLine("Data updated!");
                        }
                        else {
                            Console.WriteLine("Id to update:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update Score Count:");
                            context.Scores.FirstOrDefault(x => x.Id == temp).Count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update Time:");
                            context.Scores.FirstOrDefault(x => x.Id == temp).Time = Convert.ToInt32(Console.ReadLine());

                            context.SaveChanges();

                            Console.WriteLine("Data updated!");
                        }
                        break;
                    case 5:
                        break;
                    case 6:
                        connection.Close();
                        dataAdapter.Dispose();
                        context.Dispose();
                        System.Environment.Exit(1);
                        break;
                }
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Any key to continue");
                System.Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
