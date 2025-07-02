using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

namespace PTSCOATING.pts.ACID
{
    public partial class TakePicture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null || Session["UserId"] == "")
                {
                    Response.Redirect("../../login.aspx");
                }
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Id", typeof(int)));
                dt.Columns.Add(new DataColumn("Codetype", typeof(string)));
                dt.Columns.Add(new DataColumn("Code", typeof(string)));
                dr = dt.NewRow();
                dt.Columns["Id"].AutoIncrement = true;
                dt.Columns["Id"].AutoIncrementSeed = 1;
                dt.Columns["Id"].AutoIncrementStep = 1;
                dr["Codetype"] = string.Empty;
                dr["Code"] = string.Empty;
                Session["NewACIDBatch"] = dt;
            }
        }
        [WebMethod]
        public static string Upload(string image)
        {




            try
            {
                byte[] bitmapArrayOfBytes = Convert.FromBase64String(image);
                //byte[] imageBytes = Convert.FromBase64String(image.Split(',')[1]);
                string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
                string filePath = HttpContext.Current.Server.MapPath(string.Format("~/pts/ACID/Uploads/{0}.jpg", fileName));
                System.IO.File.WriteAllBytes(filePath, bitmapArrayOfBytes);

                HttpContext.Current.Session["fileName"] = fileName + ".jpg";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient("https://ocr-extract-text.p.rapidapi.com/ocr");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("X-RapidAPI-Host", "ocr-extract-text.p.rapidapi.com");
                request.AddHeader("X-RapidAPI-Key", ConfigurationManager.AppSettings["rapidapiKey"]);
                request.AddFile("image", filePath);
                IRestResponse response = client.Execute(request);
                string status = "";
                string display = "";
                if (Convert.ToString(response.StatusCode) == "OK")
                {
                    display = response.Content.ToString();
                    dynamic d = JObject.Parse(display);
                    status = d["text"];
                }
                else
                {
                    FileInfo F1 = new FileInfo(HttpContext.Current.Request.ServerVariables["Appl_Physical_Path"] + "pts\\ACID\\Uploads\\" + Convert.ToString(HttpContext.Current.Session["fileName"]));
                    if (F1.Exists)
                    {
                        F1.Delete();
                        HttpContext.Current.Session["fileName"] = "";
                    }
                    status = "Error";
                }
                return status;
                //return "121";
            }
            catch (Exception ex)
            {
                // return the exception instead
                return (ex.Message + "\r\n" + ex.StackTrace);
            }


        }
        [WebMethod]
        public static void SaveUser(string customers)
        {
            DataTable dt = (DataTable)(HttpContext.Current.Session["NewACIDBatch"]);
            DataRow dr = null;
            dr = dt.NewRow();
            dr["Codetype"] = "";
            dr["Code"] = customers.ToUpper().Trim();
            dt.Rows.Add(dr);
            HttpContext.Current.Session["NewACIDBatch"] = dt;
        }
        public class Customer
        {
            public string counts { get; set; }
            public string types { get; set; }
            public string code { get; set; }
        }
        public class Users
        {
            public string UserName { get; set; }
            public string batch { get; set; }

        }
    }
}