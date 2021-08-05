using System;
using System.IO;
using System.Collections;
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
using System.Data.OleDb;
using System.Text.RegularExpressions;



namespace Sharon
{
    public partial class Results : System.Web.UI.Page
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
            string query = "SELECT CONCAT(name, ' (', type, ')') FROM `remedycache_asset` where name like @SearchText" + "'%'";
            string myConnectionString = "server=itgsydsrv521;uid=vdash_im_ro; pwd=NrHYkxa2tG;database=vdash;";
            try
            {
                using (MySqlConnection con = new MySqlConnection(myConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {

                        cmd.CommandText = "SELECT CONCAT(name, ' (', type, ')') FROM `remedycache_asset` where (name like @SearchText)";
                        cmd.Parameters.AddWithValue("@SearchText", prefix + "%");
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string entry = string.Format("{0}", reader["CONCAT(name, ' (', type, ')')"]);
                                entry = entry.Replace("- None -", "Application");
                                entry = entry.Replace("Windows Server Infrastructure", "Windows");
                                entry = entry.Replace("UNIX Infrastructure", "UNIX");
                                assets.Add(entry);
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
            List<string> uniqueAssets = assets.Distinct().ToList();
            return uniqueAssets.ToArray();
        }
        
        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }

        public DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
        {
            try
            {
                ArrayList UniqueRecords = new ArrayList();
                ArrayList DuplicateRecords = new ArrayList();

                // Check if records is already added to UniqueRecords otherwise,
                // Add the records to DuplicateRecords
                foreach (DataRow dRow in table.Rows)
                {
                    if (UniqueRecords.Contains(dRow[DistinctColumn]))
                        DuplicateRecords.Add(dRow);
                    else
                        UniqueRecords.Add(dRow[DistinctColumn]);
                }

                // Remove duplicate rows from DataTable added to DuplicateRecords
                foreach (DataRow dRow in DuplicateRecords)
                {
                    table.Rows.Remove(dRow);
                }

                // Return the clean DataTable which contains unique records.
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        protected void Get_AssetData(string asset, string type)
        {
            assetType.ImageUrl = "~/img/Asset.png";
            tab_recent.Visible = false;

            string serverIp = "itgsydsrv521";
            string username = "vdash_im_ro";
            string password = "NrHYkxa2tG";
            string databaseName = "vdash";

            string dbConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", serverIp, username, password, databaseName);
            string query = "SELECT Name,ci,serial,status,role,supported,impact,urgency,region,site,building,cabinet,company,organisation,department,cost_centre,type,category,item,product_name,primary_capability,ci_description,additional_details,mbl_description FROM `remedycache_asset` where name = '" + asset + "' and type = '" + type + "'";
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


            StringBuilder emailList = new StringBuilder();
            emailList.Append("mailto:");
            query = "SELECT contact_type,contact_name  FROM `remedycache_people` where name = '" + asset + "' ORDER BY contact_type";
            //string[] ADGroups = File.ReadAllLines("C:\\temp\\remedygroups.txt");
            string[] ADGroups = File.ReadAllLines(@"\\alice\\applications\\sharon\\remedygroups.txt");
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

                                    DataTable uniqueDT = RemoveDuplicateRows(dt,"contact_name");

                                    foreach (DataRow row in uniqueDT.Rows)
                                    {
                                        string entry = row["contact_name"].ToString();
                                        string email = string.Empty;
                                        string remedyDepartment = string.Empty;
                                        string remedyGroup = string.Empty;
                                        if (entry.Contains("Macquarie Group->"))
                                        {
                                            entry = entry.Replace(' ', '_');
                                            //remedyDepartment = entry.Split('>')[1];
                                            remedyGroup = entry.Split('>').Last();
                                            //email = "DG-SGP_AllApprovers-" + remedyDepartment + remedyGroup;
                                            foreach (string ADGroup in ADGroups.Where(str => str.Contains(remedyGroup)))
                                            {
                                                email = ADGroup;
                                                email = email + "@macquarie.com;";
                                            }
                                        }
                                        else
                                        {
                                            email = entry.Replace(' ', '.');
                                            email = email + "@macquarie.com;";
                                        }
                                       
                                        emailList.Append(email);
                                    }
                                    emailList.Append("?subject=" + asset.ToUpper());
                                    peopleEmail.NavigateUrl = emailList.ToString();
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

            query = "SELECT DISTINCT parent,child  FROM `remedycache_deps` where parent = '" + asset + "'" + " OR child = '" + asset + "'";

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
                                        string subject = row[dt.Columns[1].ColumnName].ToString();
                                        if (subject != asset)
                                        {
                                            html.Append("<td>");
                                            html.Append("Child");
                                            html.Append("</td>");
                                            html.Append("<td>");
                                            string link = "http://alice/sharon/Results.aspx?asset=" + row[dt.Columns[1].ColumnName] + "&type=data";
                                            html.Append("<a href='" + link + "' target='_blank' />" + row[dt.Columns[1].ColumnName]);
                                            html.Append("</td>");
                                        }
                                        else
                                        {
                                            html.Append("<td>");
                                            html.Append("Parent");
                                            html.Append("</td>");
                                            html.Append("<td>");
                                            string link = "http://alice/sharon/Results.aspx?asset=" + row[dt.Columns[0].ColumnName] + "&type=data";
                                            html.Append("<a href='" + link + "' target='_blank' />" + row[dt.Columns[0].ColumnName]);
                                            html.Append("</td>");
                                        }


                                        html.Append("</tr>");
                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_relatedassets.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_relatedassets.Visible = false;
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

            query = "SELECT description,duration_type,recurrence_type,start_time,end_time  FROM `remedycache_blackout` where name = '" + asset + "'";

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
                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "description":
                                                    html.Append("Description");
                                                    break;
                                                case "duration_type":
                                                    html.Append("Duration");
                                                    break;
                                                case "recurrence_type":
                                                    html.Append("Recurrence");
                                                    break;
                                                case "start_time":
                                                    html.Append("Start Time");
                                                    break;
                                                case "end_time":
                                                    html.Append("End Time");
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("<td>");
                                            html.Append(row[column.ColumnName]);
                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_blackoutinfo.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_blackoutinfo.Visible = false;

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


            query = "SELECT bcp_id,bcp_critical,global_recovery_principle,business_desired_rto_recovery,agreed_rto_recovery_time_obje,mqg_architecture,notesbcp  FROM `remedycache_bcp` where ci = '" + assetRecord.CI + "' LIMIT 0,1";

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
                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "bcp_id":
                                                    html.Append("BCP ID");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "bcp_critical":
                                                    html.Append("BCP Critical");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    if (row[column.ColumnName].ToString() == "1")
                                                    {
                                                        html.Append("Yes");
                                                    }
                                                    else
                                                    {
                                                        html.Append("No");
                                                    }

                                                    break;
                                                case "global_recovery_principle":
                                                    html.Append("Global Recovery Principle");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "business_desired_rto_recovery":
                                                    html.Append("Business Desired RTO (mins)");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "agreed_rto_recovery_time_obje":
                                                    html.Append("Agreed RTO (mins)");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "mqg_architecture":
                                                    html.Append("Architecture");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "notesbcp":
                                                    html.Append("Historic BCP Notes");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_bcp.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_bcp.Visible = false;

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


            query = "SELECT vblock_remedycrq.CRQID,Summary1,Status,impact,from_unixtime(scheduled_start),from_unixtime(scheduled_end),chgimp,chgcoordchg FROM `vblock_remedycrq` LEFT JOIN `vblock_remedycrqrelated` on vblock_remedycrq.crqid = vblock_remedycrqrelated.crqid WHERE vblock_remedycrqrelated.related_asset = '" + asset + "' ORDER BY from_unixtime(scheduled_start) DESC LIMIT 0,5";

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
                                    tab_recent.Visible = true;

                                    //Building an HTML string.
                                    StringBuilder html = new StringBuilder();

                                    //Table start.
                                    html.Append("<table class='zebra'>");


                                    //Building the Data rows.
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "CRQID":
                                                    html.Append("CRQ ID");
                                                    break;
                                                case "Status":
                                                    html.Append("Status");
                                                    break;
                                                case "from_unixtime(scheduled_start)":
                                                    html.Append("Scheduled Start Date");
                                                    break;
                                                case "from_unixtime(scheduled_end)":
                                                    html.Append("Scheduled End Date");
                                                    break;
                                                case "impact":
                                                    html.Append("Impact");
                                                    break;
                                                case "Summary1":
                                                    html.Append("Summary");
                                                    break;
                                                case "chgimp":
                                                    html.Append("Change Implementer");
                                                    break;
                                                case "chgcoordchg":
                                                    html.Append("Change Coordinator");
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("<td>");
                                            if (column.ColumnName == "CRQID")
                                            {
                                                string link = "http://roogle/search/?search=" + row[column.ColumnName];
                                                html.Append("<a href='" + link + "' target='_blank' />" + row[column.ColumnName]);
                                            }
                                            else
                                            {
                                                html.Append(row[column.ColumnName]);
                                            }
                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_recentchanges.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_recentchanges.Visible = false;

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

            query = "SELECT vblock_remedyinc.INCID,description,Status,priority,from_unixtime(reported_date),assigned_group,assignee FROM `vblock_remedyinc` LEFT JOIN `vblock_remedyincrelated` on vblock_remedyinc.incid = vblock_remedyincrelated.incid WHERE vblock_remedyincrelated.related_asset = '" + asset + "' ORDER BY from_unixtime(reported_date) DESC LIMIT 0,5";

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
                                    tab_recent.Visible = true;
                                    //Building an HTML string.
                                    StringBuilder html = new StringBuilder();

                                    //Table start.
                                    html.Append("<table class='zebra'>");


                                    //Building the Data rows.
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "INCID":
                                                    html.Append("INC ID");
                                                    break;
                                                case "description":
                                                    html.Append("Description");
                                                    break;
                                                case "Status":
                                                    html.Append("Status");
                                                    break;
                                                case "priority":
                                                    html.Append("Priority");
                                                    break;
                                                case "from_unixtime(reported_date)":
                                                    html.Append("Reported Date");
                                                    break;
                                                case "assigned_group":
                                                    html.Append("Assigned Group");
                                                    break;
                                                case "assignee":
                                                    html.Append("Assignee");
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("<td>");
                                            if (column.ColumnName == "INCID")
                                            {
                                                string link = "http://roogle/search/?search=" + row[column.ColumnName];
                                                html.Append("<a href='" + link + "' target='_blank' />" + row[column.ColumnName]);
                                            }
                                            else
                                            {
                                                html.Append(row[column.ColumnName]);
                                            }
                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_recentincidents.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_recentincidents.Visible = false;
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

        protected void Get_AssetDataWild(string asset)
        {
            assetType.ImageUrl = "~/img/Asset.png";
            tab_recent.Visible = false;

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
            string[] ADGroups = File.ReadAllLines(@"\\alice\\applications\\sharon\\remedygroups.txt");
            query = "SELECT contact_type,contact_name  FROM `remedycache_people` where name = '" + asset + "' ORDER BY contact_type";

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
                                    StringBuilder emailList = new StringBuilder();
                                    emailList.Append("mailto:");
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

                                    DataTable uniqueDT = RemoveDuplicateRows(dt, "contact_name");

                                    foreach (DataRow row in uniqueDT.Rows)
                                    {
                                        string entry = row["contact_name"].ToString();
                                        string email = string.Empty;
                                        string remedyDepartment = string.Empty;
                                        string remedyGroup = string.Empty;
                                        if (entry.Contains("Macquarie Group->"))
                                        {
                                            entry = entry.Replace(' ', '_');
                                            //remedyDepartment = entry.Split('>')[1];
                                            remedyGroup = entry.Split('>').Last();
                                            //email = "DG-SGP_AllApprovers-" + remedyDepartment + remedyGroup;
                                            foreach (string ADGroup in ADGroups.Where(str => str.Contains(remedyGroup)))
                                            {
                                                email = ADGroup;
                                                email = email + "@macquarie.com;";
                                            }
                                        }
                                        else
                                        {
                                            email = entry.Replace(' ', '.');
                                            email = email + "@macquarie.com;";
                                        }

                                        emailList.Append(email);
                                    }
                                    emailList.Append("?subject=" + asset.ToUpper());
                                    peopleEmail.NavigateUrl = emailList.ToString();
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

            query = "SELECT DISTINCT parent,child  FROM `remedycache_deps` where parent = '" + asset + "'" + " OR child = '" + asset + "'";

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
                                        string subject = row[dt.Columns[1].ColumnName].ToString();
                                        if (subject != asset)
                                        {
                                            html.Append("<td>");
                                            html.Append("Child");
                                            html.Append("</td>");
                                            html.Append("<td>");
                                            string link = "http://alice/sharon/Results.aspx?asset=" + row[dt.Columns[1].ColumnName] + "&type=data";
                                            html.Append("<a href='" + link + "' target='_blank' />" + row[dt.Columns[1].ColumnName]);
                                            html.Append("</td>");
                                        }
                                        else
                                        {
                                            html.Append("<td>");
                                            html.Append("Parent");
                                            html.Append("</td>");
                                            html.Append("<td>");
                                            string link = "http://alice/sharon/Results.aspx?asset=" + row[dt.Columns[0].ColumnName] + "&type=data";
                                            html.Append("<a href='" + link + "' target='_blank' />" + row[dt.Columns[0].ColumnName]);
                                            html.Append("</td>");
                                        }


                                        html.Append("</tr>");
                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_relatedassets.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_relatedassets.Visible = false;
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

            query = "SELECT description,duration_type,recurrence_type,start_time,end_time  FROM `remedycache_blackout` where name = '" + asset + "'";

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
                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "description":
                                                    html.Append("Description");
                                                    break;
                                                case "duration_type":
                                                    html.Append("Duration");
                                                    break;
                                                case "recurrence_type":
                                                    html.Append("Recurrence");
                                                    break;
                                                case "start_time":
                                                    html.Append("Start Time");
                                                    break;
                                                case "end_time":
                                                    html.Append("End Time");
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("<td>");
                                            html.Append(row[column.ColumnName]);
                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_blackoutinfo.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_blackoutinfo.Visible = false;

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


            query = "SELECT bcp_id,bcp_critical,global_recovery_principle,business_desired_rto_recovery,agreed_rto_recovery_time_obje,mqg_architecture,notesbcp  FROM `remedycache_bcp` where ci = '" + assetRecord.CI + "' LIMIT 0,1";

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
                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "bcp_id":
                                                    html.Append("BCP ID");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "bcp_critical":
                                                    html.Append("BCP Critical");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    if (row[column.ColumnName].ToString() == "1")
                                                    {
                                                        html.Append("Yes");
                                                    }
                                                    else
                                                    {
                                                        html.Append("No");
                                                    }

                                                    break;
                                                case "global_recovery_principle":
                                                    html.Append("Global Recovery Principle");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "business_desired_rto_recovery":
                                                    html.Append("Business Desired RTO (mins)");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "agreed_rto_recovery_time_obje":
                                                    html.Append("Agreed RTO (mins)");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "mqg_architecture":
                                                    html.Append("Architecture");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                case "notesbcp":
                                                    html.Append("Historic BCP Notes");
                                                    html.Append("</td>");
                                                    html.Append("<td>");
                                                    html.Append(row[column.ColumnName]);
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_bcp.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_bcp.Visible = false;

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


            query = "SELECT vblock_remedycrq.CRQID,Summary1,Status,impact,from_unixtime(scheduled_start),from_unixtime(scheduled_end),chgimp,chgcoordchg FROM `vblock_remedycrq` LEFT JOIN `vblock_remedycrqrelated` on vblock_remedycrq.crqid = vblock_remedycrqrelated.crqid WHERE vblock_remedycrqrelated.related_asset = '" + asset + "' ORDER BY from_unixtime(scheduled_start) DESC LIMIT 0,5";

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
                                    tab_recent.Visible = true;

                                    //Building an HTML string.
                                    StringBuilder html = new StringBuilder();

                                    //Table start.
                                    html.Append("<table class='zebra'>");


                                    //Building the Data rows.
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "CRQID":
                                                    html.Append("CRQ ID");
                                                    break;
                                                case "Status":
                                                    html.Append("Status");
                                                    break;
                                                case "from_unixtime(scheduled_start)":
                                                    html.Append("Scheduled Start Date");
                                                    break;
                                                case "from_unixtime(scheduled_end)":
                                                    html.Append("Scheduled End Date");
                                                    break;
                                                case "impact":
                                                    html.Append("Impact");
                                                    break;
                                                case "Summary1":
                                                    html.Append("Summary");
                                                    break;
                                                case "chgimp":
                                                    html.Append("Change Implementer");
                                                    break;
                                                case "chgcoordchg":
                                                    html.Append("Change Coordinator");
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("<td>");
                                            if (column.ColumnName == "CRQID")
                                            {
                                                string link = "http://roogle/search/?search=" + row[column.ColumnName];
                                                html.Append("<a href='" + link + "' target='_blank' />" + row[column.ColumnName]);
                                            }
                                            else
                                            {
                                                html.Append(row[column.ColumnName]);
                                            }
                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_recentchanges.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_recentchanges.Visible = false;

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

            query = "SELECT vblock_remedyinc.INCID,description,Status,priority,from_unixtime(reported_date),assigned_group,assignee FROM `vblock_remedyinc` LEFT JOIN `vblock_remedyincrelated` on vblock_remedyinc.incid = vblock_remedyincrelated.incid WHERE vblock_remedyincrelated.related_asset = '" + asset + "' ORDER BY from_unixtime(reported_date) DESC LIMIT 0,5";

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
                                    tab_recent.Visible = true;
                                    //Building an HTML string.
                                    StringBuilder html = new StringBuilder();

                                    //Table start.
                                    html.Append("<table class='zebra'>");


                                    //Building the Data rows.
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        foreach (DataColumn column in dt.Columns)
                                        {

                                            html.Append("<tr>");
                                            html.Append("<td>");
                                            switch (column.ColumnName)
                                            {
                                                case "INCID":
                                                    html.Append("INC ID");
                                                    break;
                                                case "description":
                                                    html.Append("Description");
                                                    break;
                                                case "Status":
                                                    html.Append("Status");
                                                    break;
                                                case "priority":
                                                    html.Append("Priority");
                                                    break;
                                                case "from_unixtime(reported_date)":
                                                    html.Append("Reported Date");
                                                    break;
                                                case "assigned_group":
                                                    html.Append("Assigned Group");
                                                    break;
                                                case "assignee":
                                                    html.Append("Assignee");
                                                    break;
                                                default:
                                                    break;
                                            }

                                            html.Append("</td>");
                                            html.Append("<td>");
                                            if (column.ColumnName == "INCID")
                                            {
                                                string link = "http://roogle/search/?search=" + row[column.ColumnName];
                                                html.Append("<a href='" + link + "' target='_blank' />" + row[column.ColumnName]);
                                            }
                                            else
                                            {
                                                html.Append(row[column.ColumnName]);
                                            }
                                            html.Append("</td>");
                                            html.Append("</tr>");

                                        }
                                        html.Append("<tr>");
                                        html.Append("<td colspan='2'><hr class='divider'/></td>");
                                        html.Append("</tr>");

                                    }

                                    //Table end.
                                    html.Append("</table>");

                                    //Append the HTML string to Placeholder.
                                    table_recentincidents.Controls.Add(new Literal { Text = html.ToString() });
                                }
                                else
                                {
                                    tab_recentincidents.Visible = false;
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

                string input = Request.QueryString["asset"];
                string search = Request.QueryString["type"];
                if (input != null)
                {

                    if ((input.Contains(")")) && ((input.IndexOf("(")) == (input.LastIndexOf("("))) && (search != "data"))
                    {
                        string asset = ReplaceLastOccurrence(input, " (", "+").Split('+')[0];
                        string type = input.Replace(" (", "(").Split('(').Last().TrimEnd(')');

                        type = type.Replace("Application", "- None -");
                        type = type.Replace("Windows", "Windows Server Infrastructure");
                        type = type.Replace("UNIX", "UNIX Infrastructure");
                        Get_AssetData(asset, type);
                    }
                    else
                    {
                        if ((input.IndexOf("(")) != (input.LastIndexOf("(")))
                        { 
                            string asset = ReplaceLastOccurrence(input, " (", "+").Split('+')[0];

                            Get_AssetDataWild(asset);
                        }
                        else
                        {
                            Get_AssetDataWild(input);
                        }
                    }
                    this.Title = input;
                    searchQuery.Value = input;
                }


            }



        }


        protected void searchButton_OnClick(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
            string asset = Request.Form["searchQuery"];
            Response.Redirect("Results.aspx?asset=" + Server.UrlEncode(asset));


        }
    }
}