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
                Console.WriteLine("1. Overview...");
                Console.WriteLine("2. Add new...");
                Console.WriteLine("3. Remove existing...");
                Console.WriteLine("4. Change...");
                Console.WriteLine("5. Custom functions.");
                Console.WriteLine("6. Exit.");
                control = Convert.ToInt32(Console.ReadLine());
                switch (control)
                {
                    case 1:
                        Console.WriteLine("1. All Players");
                        Console.WriteLine("2. All Platforms");
                        Console.WriteLine("3. All Players");
                        Console.WriteLine("4. All Scores");
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
                        Console.WriteLine("1. Game");
                        Console.WriteLine("2. Platform");
                        Console.WriteLine("3. Player");
                        Console.WriteLine("4. Score");
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
                            Console.WriteLine("Choose Name:");
                            row["Name"] = Console.ReadLine();
                            Console.WriteLine("Choose Publisher:");
                            row["Publisher"] = Console.ReadLine();
                            Console.WriteLine("Choose Release_Year:");
                            row["Release_Year"] = Convert.ToInt32(Console.ReadLine());
                            dataset.Tables[0].Rows.Add(row);
                            dataAdapter.Update(dataset.Tables[0]);

                            //Console.WriteLine("Data inserted!");
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
                            Console.WriteLine("Choose Device:");
                            row["Device"] = Console.ReadLine();
                            Console.WriteLine("Choose Os:");
                            row["Os"] = Console.ReadLine();
                            dataset.Tables[0].Rows.Add(row);
                            dataAdapter.Update(dataset.Tables[0]);

                            //Console.WriteLine("Data inserted!");
                        }
                        else if (control == 3)
                        {
                            Player player = new Player();
                            Console.WriteLine("Choose Nickname:");
                            player.Nickname = Console.ReadLine();

                            context.Players.Add(player);
                            context.SaveChanges();

                            //Console.WriteLine("Data inserted!");
                        }
                        else {
                            Score score = new Score();
                            Console.WriteLine("Choose Count:");
                            score.Count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Choose Time:");
                            score.Time = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Choose Game Id:");
                            score.GameId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Choose Player Id:");
                            score.PlayerId = Convert.ToInt32(Console.ReadLine());

                            context.Scores.Add(score);
                            context.SaveChanges();

                           // Console.WriteLine("Data inserted!");
                        }
                        break;
                    case 3:
                        Console.WriteLine("1. Game");
                        Console.WriteLine("2. Platform");
                        Console.WriteLine("3. Player");
                        Console.WriteLine("4. Score");
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

                            Console.WriteLine("Choose Id to Remove:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            dataset.Tables[0].Rows.Find(temp).Delete();

                            dataAdapter.Update(dataset.Tables[0]);
                            //Console.WriteLine("Data deleted!");
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

                            Console.WriteLine("Choose Id to Remove:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            dataset.Tables[0].Rows.Find(temp).Delete();

                            dataAdapter.Update(dataset.Tables[0]);
                            //Console.WriteLine("Data deleted!");
                        }
                        else if (control == 3)
                        {
                            Console.WriteLine("Choose Id to Remove:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            context.Players.Remove(context.Players.FirstOrDefault(x => x.Id == temp));

                            context.SaveChanges();

                            //Console.WriteLine("Data deleted!");
                        }
                        else {
                            Console.WriteLine("Choose Id to Remove:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            context.Scores.Remove(context.Scores.FirstOrDefault(x => x.Id == temp));

                            context.SaveChanges();

                            //Console.WriteLine("Data deleted!");
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

                            Console.WriteLine("Id to Change:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("New Name:");
                            dataset.Tables[0].Rows.Find(temp)["Name"] = Console.ReadLine();
                            Console.WriteLine("New Publisher:");
                            dataset.Tables[0].Rows.Find(temp)["Publisher"] = Console.ReadLine();
                            Console.WriteLine("New Release_Year:");
                            dataset.Tables[0].Rows.Find(temp)["Release_Year"] = Convert.ToInt32(Console.ReadLine());
                        
                            dataAdapter.Update(dataset.Tables[0]);
                            //Console.WriteLine("Data updated!");
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

                            Console.WriteLine("Id to Change:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("New Device:");
                            dataset.Tables[0].Rows.Find(temp)["Device"] = Console.ReadLine();
                            Console.WriteLine("New Os:");
                            dataset.Tables[0].Rows.Find(temp)["Os"] = Console.ReadLine();

                            dataAdapter.Update(dataset.Tables[0]);
                            //Console.WriteLine("Data updated!");
                        }
                        else if (control == 3)
                        {
                            Console.WriteLine("Id to Change:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update Nickname:");
                            context.Players.FirstOrDefault(x => x.Id == temp).Nickname = Console.ReadLine();

                            context.SaveChanges();

                            //Console.WriteLine("Data updated!");
                        }
                        else {
                            Console.WriteLine("Id to Change:");
                            int temp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("New Score Count:");
                            context.Scores.FirstOrDefault(x => x.Id == temp).Count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("New Time:");
                            context.Scores.FirstOrDefault(x => x.Id == temp).Time = Convert.ToInt32(Console.ReadLine());

                            context.SaveChanges();

                            //Console.WriteLine("Data updated!");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Choose function:");
                        Console.WriteLine("1. See 2nd and 3rd highest scores");
                        Console.WriteLine("2. See how many scores player has");
                        Console.WriteLine("3. See player name and Score");
                        control = Convert.ToInt32(Console.ReadLine());
                        if (control == 1) {
                            var winners = context.Scores.ToList();
                            winners.Sort((x, y) => y.Count.CompareTo(x.Count));
                            foreach (var winner in winners.Skip(1).Take(2))
                            {
                                Console.WriteLine($"{winner.Count} ");
                            }
                        }
                        else if (control == 2)
                        {
                            Console.WriteLine("Select player nickname:");
                            string temp = Console.ReadLine();
                            var everything = from scor in context.Scores
                                               join play in context.Players on scor.PlayerId equals play.Id
                                               select new { Name = play.Nickname, Score = scor.Count };
                            var playerScores = from scores in everything
                                                group scores.Score by scores.Name into allscores
                                                select new { Name = allscores.Key, Count = allscores.ToList().Count};

                            foreach (var player in playerScores)
                            {
                                Console.WriteLine($"{player.Name} | {player.Count} ");
                            }
                        }
                        else
                        {
                            var everything = from scor in context.Scores
                                            join play in context.Players on scor.PlayerId equals play.Id
                                            select new { Name = play.Nickname, Score = scor.Count };
                            Console.WriteLine("Player Name  |  Score");
                            foreach ( var e in everything)
                            {
                                Console.WriteLine($"{e.Name} | {e.Score} ");
                            }
                        }
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
