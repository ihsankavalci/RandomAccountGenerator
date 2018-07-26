using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            //RandomGitHubAccountWriter();
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            List<GitHubAcc> gitHubAccounts = GetGitHubAccounts(conn);
            foreach (GitHubAcc account in gitHubAccounts)
            {
                MailAddress from = new MailAddress("ihsan@yemeksepeti.com");
                MailAddress to = new MailAddress(account.Email);
                MailMessage message = new MailMessage(from, to);
                message.Body = "selam github hesap bilgilerin asagidadir\n";

                string json = JsonConvert.SerializeObject(account, Formatting.Indented);
                message.Body += json;

            }
            Console.ReadLine();
        }
        public static List<GitHubAcc> GetGitHubAccounts(SqlConnection conn)
        {
            string sql = "Select * from GithubAccounts";

            // Create command.
            SqlCommand cmd = new SqlCommand();

            // Set connection for Command.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            List<GitHubAcc> gitHubAccountList = new List<GitHubAcc>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        int uidIndex = reader.GetOrdinal("userid");
                        int uid = reader.GetInt32(uidIndex);

                        int unameIndex = reader.GetOrdinal("username");
                        String uname = reader.GetString(unameIndex);

                        int emailIndex = reader.GetOrdinal("email");
                        String email = reader.GetString(emailIndex);

                        int urlIndex = reader.GetOrdinal("githuburl");
                        String url = reader.GetString(urlIndex);

                        var gitHubAcc = new GitHubAcc();
                        gitHubAcc.Id = uid;
                        gitHubAcc.Username = uname;
                        gitHubAcc.Email = email;
                        gitHubAcc.GitHubUrl = url;

                        gitHubAccountList.Add(gitHubAcc);

                        Console.WriteLine("username:" + uname);

                    }
                }
            }

            return gitHubAccountList;
        }
        private static void InsertGitHubAccount(SqlConnection conn, GitHubAcc gitHubAcc)
        {
            string sql = "Insert into GithubAccounts (userid, username, email, githuburl) "
                                                 + " values (@userid, @username, @email, @githuburl) ";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;


            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = gitHubAcc.Id;
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = gitHubAcc.Username;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = gitHubAcc.Email;
            cmd.Parameters.Add("@githuburl", SqlDbType.VarChar).Value = gitHubAcc.GitHubUrl;

            int rowCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Row Count affected = " + rowCount);
        }
        public static void SendMail(MailMessage Message)
        {

            SmtpClient client = new SmtpClient();
            client.Host = "192.168.0.215";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential("", "");
            client.Send(Message);
        }
        private static void RandomGitHubAccountWriter()
        {
            var randomHelper = new RandomHelper();
            List<GitHubAcc> gitHubAccountList = new List<GitHubAcc>();
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            for (int i = 0; i < 10; i++)
            {
                String uname = randomHelper.RandomStr();
                var gitHubAccountFactory = new GitHubAccountFactory();
                var gitHubAccount = gitHubAccountFactory.CreateRandomGithubAccount();
                gitHubAccountList.Add(gitHubAccount);
                InsertGitHubAccount(conn, gitHubAccount);
            }

            string json = JsonConvert.SerializeObject(gitHubAccountList, Formatting.Indented);
            FileStream fs = File.Create("result.txt");

            StreamWriter sw = new StreamWriter(fs);
            sw.Write(json);
            sw.Flush();

            foreach (GitHubAcc account in gitHubAccountList)
            {
                // Console.WriteLine(account.Username);
            }
        }
    }
}
