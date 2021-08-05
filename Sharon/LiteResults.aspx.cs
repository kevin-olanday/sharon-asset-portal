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
    public partial class LiteResults : System.Web.UI.Page
    {
        public class Asset
        {
            public string Name { get; set; }
            public string CI { get; set; }
            public string Serial { get; set; }
            public string Status { get; set; }
            public string Role { get; set; }
            public string Supported { get; set; }
            public string Impact { get; set; }
            public string Urgency { get; set; }
            public string Region { get; set; }
            public string Site { get; set; }
            public string Building { get; set; }
            public string Cabinet { get; set; }
            public string Company { get; set; }
            public string Organisation { get; set; }
            public string Department { get; set; }
            public string CostCentre { get; set; }
            public string Type { get; set; }
            public string Category { get; set; }
            public string Item { get; set; }
            public string Class { get; set; }
            public string CIDescription { get; set; }
            public string AdditionalInformation { get; set; }
            public string BusinessDescription { get; set; }
            public string OwnedBy { get; set; }
            public string ApprovedBy { get; set; }
            public string ManagedBy { get; set; }
            public string SupportedBy { get; set; }
            public string UsedBy { get; set; }

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
            string query = "SELECT Name FROM `remedycache_asset` where name like @SearchText" + "'%'";
            string myConnectionString = "server=itgsydsrv521;uid=vdash_im_ro; pwd=NrHYkxa2tG;database=vdash;";
            try
            {
                using (MySqlConnection con = new MySqlConnection(myConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {

                        cmd.CommandText = "SELECT Name FROM `remedycache_asset` where (name like @SearchText)";
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

        protected void Get_AssetData(string asset)
        {
            assetType.ImageUrl = "~/img/Asset.png";

            string serverIp = "itgsydsrv521";
            string username = "vdash_im_ro";
            string password = "NrHYkxa2tG";
            string databaseName = "vdash";

            string dbConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", serverIp, username, password, databaseName);
            string query = "SELECT Name,ci,serial,status,role,supported,impact,urgency,region,site,building,cabinet,company,organisation,department,cost_centre,type,category,item,product_name,primary_capability,ci_description,additional_details,mbl_description FROM `remedycache_asset` where name = '" + asset + "'";
            string myConnectionString = "server=itgsydsrv521;uid=vdash_im_ro; pwd=NrHYkxa2tG;database=vdash;";

            Asset assetRecord = new Asset();

            try
            {
                using (MySqlConnection con = new MySqlConnection(myConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                adapter.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    assetRecord.Name = dt.Rows[0]["name"].ToString();
                                    assetRecord.CI = dt.Rows[0]["ci"].ToString();
                                    assetRecord.Item = dt.Rows[0]["product_name"].ToString();
                                    assetRecord.Serial = dt.Rows[0]["serial"].ToString();
                                    assetRecord.Status = dt.Rows[0]["status"].ToString();
                                    assetRecord.Role = dt.Rows[0]["role"].ToString();
                                    assetRecord.Supported = dt.Rows[0]["supported"].ToString();
                                    assetRecord.Impact = dt.Rows[0]["impact"].ToString();
                                    assetRecord.Urgency = dt.Rows[0]["urgency"].ToString();
                                    assetRecord.Region = dt.Rows[0]["region"].ToString();
                                    assetRecord.Site = dt.Rows[0]["site"].ToString();
                                    assetRecord.Building = dt.Rows[0]["building"].ToString();
                                    assetRecord.Cabinet = dt.Rows[0]["cabinet"].ToString();
                                    assetRecord.Company = dt.Rows[0]["company"].ToString();
                                    assetRecord.Organisation = dt.Rows[0]["organisation"].ToString();
                                    assetRecord.Department = dt.Rows[0]["department"].ToString();
                                    assetRecord.CostCentre = dt.Rows[0]["cost_centre"].ToString();
                                    assetRecord.Type = dt.Rows[0]["type"].ToString();
                                    assetRecord.Category = dt.Rows[0]["category"].ToString();
                                    assetRecord.CIDescription = dt.Rows[0]["ci_description"].ToString();
                                    assetRecord.AdditionalInformation = dt.Rows[0]["additional_details"].ToString();
                                    assetRecord.BusinessDescription = dt.Rows[0]["mbl_description"].ToString();



                                    switch (assetRecord.Type)
                                    {
                                        case "Windows Server Infrastructure":
                                            assetType.ImageUrl = "~/img/Windows.png";
                                            break;
                                        case "UNIX Infrastructure":
                                            assetType.ImageUrl = "~/img/Unix.png";
                                            break;
                                        case "Database":
                                            assetType.ImageUrl = "~/img/Database.png";
                                            break;
                                        default:
                                            break;
                                    }


                                    label_assetName.Text = assetRecord.Name;
                                    label_assetID.Text = assetRecord.CI;
                                    label_item.Text = assetRecord.Item;
                                    label_serial.Text = assetRecord.Serial;
                                    label_status.Text = assetRecord.Status;
                                    label_role.Text = assetRecord.Role;
                                    label_supported.Text = assetRecord.Supported;
                                    label_impact.Text = assetRecord.Impact;
                                    label_urgency.Text = assetRecord.Urgency;
                                    label_region.Text = assetRecord.Region;
                                    label_site.Text = assetRecord.Site;
                                    label_address.Text = assetRecord.Building;
                                    label_cabinetNumber.Text = assetRecord.Cabinet;
                                    label_company.Text = assetRecord.Company;
                                    label_organisation.Text = assetRecord.Organisation;
                                    label_department.Text = assetRecord.Department;
                                    label_costcentre.Text = assetRecord.CostCentre;
                                    label_category.Text = assetRecord.Category;
                                    label_type.Text = assetRecord.Type;
                                    label_ciDescription.Text = assetRecord.CIDescription;
                                    label_additionalInformation.Text = assetRecord.AdditionalInformation;
                                    label_businessDescription.Text = assetRecord.BusinessDescription;

                                    remedyLink.HRef = "https://remedyprd.pc.internal.macquarie.com/arsys/forms/remedyappprd/AST%3AComputerSystem/Management/?qual=%27CI+ID%2B%27%3D%22" + assetRecord.CI + "%22&cacheid=288a09cf";
                                }
                                else
                                {
                                    tab_overview.Visible = false;
                                    resultsPanel.Visible = false;
                                    noresultsPanel.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {

            }

            query = "SELECT contact_type,contact_name  FROM `remedycache_people` where name = '" + asset + "'" + " ORDER BY contact_type";

            try
            {
                using (MySqlConnection con = new MySqlConnection(myConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                adapter.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {

                                    //Building an HTML string.
                                    StringBuilder html = new StringBuilder();

                                    //Table start.
                                    html.Append("<table class='zebra'>");


                                    //Building the Data rows.
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        html.Append("<tr>");
                                        foreach (DataColumn column in dt.Columns)
                                        {
                                            html.Append("<td>");
                                            html.Append(row[column.ColumnName]);
                                            html.Append("</td>");
                                        }
                                        html.Append("</tr>");
                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_people.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_people.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {

            }

       
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string asset = Request.QueryString["asset"];
                this.Title = asset;
                searchQuery.Value = asset;
                Get_AssetData(asset);
            }



        }

        protected void searchButton_OnClick(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
            string asset = Request.Form["searchQuery"];
            Response.Redirect("LiteResults.aspx?asset=" + Server.UrlEncode(asset));


        }
    }
}