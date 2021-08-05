using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text;
using System.Configuration;
using System.Web.Services;

namespace Sharon
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
    
        }

        [WebMethod]
        public static string[] GetAssets(string prefix)
        {
            List<string> assets = new List<string>();
            string serverIp = "itgsydsrv521";
            string username = "vdash_im_ro";
            string password = "NrHYkxa2tG";
            string databaseName = "vdash";

            string dbConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", serverIp, username, password, databaseName);
            string query = "SELECT  concat(name," + " " + ", type) FROM `remedycache_asset` where name like @SearchText" + "'%'";
            string myConnectionString = "server=itgsydsrv521;uid=vdash_im_ro; pwd=NrHYkxa2tG;database=vdash;";
            try
            {
                using (MySqlConnection con = new MySqlConnection(myConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {

                        cmd.CommandText = "SELECT concat(name," + " " + ", type) FROM `remedycache_asset` where (name like @SearchText)";
                        cmd.Parameters.AddWithValue("@SearchText", prefix + "%");
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                assets.Add(string.Format("{0}", reader["Name"]));
                            }
                            reader.Close();
                        }
                        con.Close();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return assets.ToArray();
        }

        protected void searchButton_OnClick(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
            string asset = Request.Form["searchQuery"];
            if (asset != null)
            { 
                Response.Redirect("Results.aspx?asset=" + Server.UrlEncode(asset));
            }


        }
    }
}