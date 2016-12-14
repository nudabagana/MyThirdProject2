using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyThirdProject2
{
    class DataController
    {
        MyThirdDatabase2ModelContainer context = new MyThirdDatabase2ModelContainer();

        SqlConnection connection = new SqlConnection();
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet dataset;

        public void Init()
        {
            connection.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\See Sharp\MyThirdProject2\MyThirdProject2\ProjectThirdDatabase2.mdf'; Integrated Security = True";
            connection.Open();
        }

        public void WhenDone()
        {
            connection.Close();
            dataAdapter.Dispose();
            context.Dispose();
        }

        public DataSet SelectGamesData()
        {
            dataset = new DataSet();
            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Name, Publisher, Release_Year FROM Games", connection);
            dataAdapter.Fill(dataset, "Games");
            return dataset;
        }

        public DataSet SelectPlatformsData()
        {
            dataset = new DataSet();
            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Device, Os FROM Platforms", connection);
            dataAdapter.Fill(dataset, "Platforms");
            return dataset;
        }

        public void InsertGamesData(string first, string second, int third)
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
            row["Name"] = first;
            row["Publisher"] = second;
            row["Release_Year"] = third;
            dataset.Tables[0].Rows.Add(row);
            dataAdapter.Update(dataset.Tables[0]);
        }

        public void InsertPlatformsData(string first, string second)
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

            row["Device"] = first;
            row["Os"] = second;
            dataset.Tables[0].Rows.Add(row);
            dataAdapter.Update(dataset.Tables[0]);
        }

        public void DeleteGamesData(int id)
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

            dataset.Tables[0].Rows.Find(id).Delete();

            dataAdapter.Update(dataset.Tables[0]);
        }

        public void DeletePlatformsData(int id)
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

            dataset.Tables[0].Rows.Find(id).Delete();

            dataAdapter.Update(dataset.Tables[0]);
        }

        public void UpdateGamesData(int id, string first, string second, int third)
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

            dataset.Tables[0].Rows.Find(id)["Name"] = first;
            dataset.Tables[0].Rows.Find(id)["Publisher"] = second;
            dataset.Tables[0].Rows.Find(id)["Release_Year"] = third;

            dataAdapter.Update(dataset.Tables[0]);
        }

        public void UpdatePlatformsData(int id, string first, string second)
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


            dataset.Tables[0].Rows.Find(id)["Device"] = first;
            dataset.Tables[0].Rows.Find(id)["Os"] = second;

            dataAdapter.Update(dataset.Tables[0]);
        }

        public DbSet<Player> SelectPlayersEntity()
        {
            return context.Players;
        }

        public DbSet<Score> SelectScoreEntity()
        {
            return context.Scores;
        }

        public void InsertPlayersEntity(string first)
        {
            Player player = new Player();
            player.Nickname = first;

            context.Players.Add(player);
            context.SaveChanges();
        }

        public void InsertScoreEntity(int first, int second, int third, int fourth)
        {
            Score score = new Score();
            score.Count = first;
            score.Time = second;
            score.GameId = third;
            score.PlayerId = fourth;

            context.Scores.Add(score);
            context.SaveChanges();

        }

        public void DeletePlayersEntity(int id)
        {
            context.Players.Remove(context.Players.FirstOrDefault(x => x.Id == id));

            context.SaveChanges();
        }

        public void DeleteScoreEntity(int id)
        {
            context.Scores.Remove(context.Scores.FirstOrDefault(x => x.Id == id));

            context.SaveChanges();

        }

        public void UpdatePlayersEntity(int id, string first)
        {
            context.Players.FirstOrDefault(x => x.Id == id).Nickname = first;

            context.SaveChanges();

            //Console.WriteLine("Data updated!");
        }

        public void UpdateScoreEntity(int id, int first, int second)
        {
            context.Scores.FirstOrDefault(x => x.Id == id).Count = first;
            context.Scores.FirstOrDefault(x => x.Id == id).Time = second;

            context.SaveChanges();

        }

        public List<Score> ReturnScoreList()
        {
            return context.Scores.ToList();
        }

        public List<string> ReturnHowManyScores()
        {
            var everything = from scor in context.Scores
                             join play in context.Players on scor.PlayerId equals play.Id
                             select new { Name = play.Nickname, Score = scor.Count };
            var playerScores = from scores in everything
                               group scores.Score by scores.Name into allscores
                               select new { Name = allscores.Key, Count = allscores.ToList().Count };
            List<string> list = new List<string>();
            foreach (var player in playerScores)
            {
                list.Add($"{player.Name} | {player.Count} ");
            }

            return list;
        }

        public List<string> ReturnPlayersAndScores()
        {
            var everything = from scor in context.Scores
                             join play in context.Players on scor.PlayerId equals play.Id
                             select new { Name = play.Nickname, Score = scor.Count };

            List<string> list = new List<string>();

            foreach (var e in everything)
            {
                list.Add($"{e.Name} | {e.Score} ");
            }
            return list;
        }

    }
}
