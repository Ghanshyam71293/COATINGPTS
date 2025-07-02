using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Configuration;
//using System.Web.UI.MasterPage;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Web.Configuration;
namespace PTSCOATING
{
    public class helperclass
    {
        public static string strconnect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        public void GridviewData_Parameter(GridView gridview, string stored_text, Hashtable parameters)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(strconnect);
            SqlCommand cmd = new SqlCommand(stored_text, connection);
            try
            {
                DictionaryEntry Item = default(DictionaryEntry);
                cmd.CommandType = CommandType.Text;
                foreach (DictionaryEntry Item_loopVariable in parameters)
                {
                    Item = Item_loopVariable;
                    cmd.Parameters.AddWithValue(Item.Key.ToString(), Item.Value.ToString());
                }
                cmd.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                cmd.Connection.Close();
                gridview.DataSource = dataset.Tables[0].DefaultView;
                gridview.DataBind();

            }
            catch (Exception err)
            {
                cmd.Connection.Close();
                throw (err);
            }
        }

        public DataTable sendDataTable(string strsql)
        {
            DataTable Dt = null;

            try
            {
                SqlConnection connection = new SqlConnection(strconnect);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(strsql, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(Dt);

                adapter.Dispose();
                connection.Close();

            }
            catch (Exception err)
            {
                throw (err);
            }
            return Dt;
        }

        public DataTable sendDatatable(string strsql, bool Addnewrecord)
        {
            DataTable functionReturnValue = null;

            try
            {
                SqlConnection connection = new SqlConnection(strconnect);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(strsql, connection);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                if (Addnewrecord == true)
                {
                    DataRow myNewRow = null;
                    object[] rowVals = new object[datatable.Rows.Count];
                    rowVals[0] = 1;
                    myNewRow = datatable.Rows.Add(rowVals);
                }

                functionReturnValue = datatable;
                adapter.Dispose();
                connection.Close();

            }
            catch (Exception err)
            {
                throw (err);
            }
            return functionReturnValue;
        }

        public DataSet sendDataset(string strsql, bool Addnewrecord)
        {
            DataSet functionReturnValue = null;
            try
            {
                SqlConnection connection = new SqlConnection(strconnect);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(strsql, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                if (Addnewrecord == true)
                {
                    DataRow myNewRow = null;
                    object[] rowVals = new object[dataset.Tables[0].Columns.Count];
                    rowVals[0] = 1;
                    myNewRow = dataset.Tables[0].Rows.Add(rowVals);
                }
                functionReturnValue = dataset;
                adapter.Dispose();
                connection.Close();

            }
            catch (Exception err)
            {
                throw (err);
            }
            return functionReturnValue;
        }
        public void ExecuteQry(string myExecuteQuery)
        {
            SqlConnection cn = new SqlConnection(strconnect);
            SqlCommand myCommand = new SqlCommand(myExecuteQuery, cn);
            myCommand.Connection.Open();
            myCommand.ExecuteNonQuery();
            cn.Close();
        }

        #region "ExecuteQuerySP.."
        public void ExecuteQry_SP(string stored_proc, Hashtable parameters)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(strconnect);
            SqlCommand cmd = new SqlCommand(stored_proc, connection);
            try
            {
                DictionaryEntry Item = default(DictionaryEntry);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry Item_loopVariable in parameters)
                {
                    Item = Item_loopVariable;
                    //cmd.Parameters.AddWithValue(Item.Key, Item.Value);
                    cmd.Parameters.AddWithValue(Item.Key.ToString(), Item.Value);
                }
                if (cmd.Connection.State == ConnectionState.Open || cmd.Connection.State == ConnectionState.Broken)
                {
                    cmd.Connection.Close();
                }
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ERR)
            {
                throw (ERR);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion

        public void ExecuteQry_Parameter(string stored_text, Hashtable parameters)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(strconnect);
            SqlCommand cmd = new SqlCommand(stored_text, connection);
            try
            {
                DictionaryEntry Item = default(DictionaryEntry);
                cmd.CommandType = CommandType.Text;
                foreach (DictionaryEntry Item_loopVariable in parameters)
                {
                    Item = Item_loopVariable;
                    cmd.Parameters.AddWithValue(Item.Key.ToString(), Item.Value.ToString());
                }
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception err)
            {
                cmd.Connection.Close();
                throw (err);
            }
        }
        public DataSet senddataset_Parameter(string stored_text, Hashtable parameters)
        {
            DataSet functionReturnValue = null;
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(strconnect);
            SqlCommand cmd = new SqlCommand(stored_text, connection);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataset = new DataSet();
                DictionaryEntry Item = default(DictionaryEntry);
                cmd.CommandType = CommandType.Text;
                foreach (DictionaryEntry Item_loopVariable in parameters)
                {
                    Item = Item_loopVariable;
                    cmd.Parameters.AddWithValue(Item.Key.ToString(), Item.Value.ToString());
                }
                cmd.Connection.Open();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataset);
                functionReturnValue = dataset;
                cmd.Connection.Close();
            }
            catch (Exception err)
            {
                cmd.Connection.Close();
                throw (err);
            }
            return functionReturnValue;
        }

        public object SendValue_Parameter(string stored_text, Hashtable parameters)
        {
            object functionReturnValue = null;
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(strconnect);
            SqlCommand cmd = new SqlCommand(stored_text, connection);
            try
            {
                DictionaryEntry Item = default(DictionaryEntry);
                cmd.CommandType = CommandType.Text;
                foreach (DictionaryEntry Item_loopVariable in parameters)
                {
                    Item = Item_loopVariable;
                    cmd.Parameters.AddWithValue(Item.Key.ToString(), Item.Value.ToString());

                }
                cmd.Connection.Open();
                functionReturnValue = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception err)
            {
                cmd.Connection.Close();
                throw (err);
            }
            return functionReturnValue;
        }
        public object SendValue_SP(string stored_proc, Hashtable parameters)
        {
            object functionReturnValue = null;
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(strconnect);
            SqlCommand cmd = new SqlCommand(stored_proc, connection);
            try
            {
                DictionaryEntry Item = default(DictionaryEntry);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry Item_loopVariable in parameters)
                {
                    Item = Item_loopVariable;
                    cmd.Parameters.AddWithValue(Item.Key.ToString(), Item.Value.ToString());
                }
                cmd.Connection.Open();
                functionReturnValue = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception err)
            {
                cmd.Connection.Close();
                throw (err);
            }
            return functionReturnValue;
        }
        public void Fillcombo(String strsql, DropDownList droplist)
        {
            try
            {
                // strconnect = page.Session["strconnect1"].ToString();
                System.Data.SqlClient.SqlDataAdapter adapter = null;
                adapter = new System.Data.SqlClient.SqlDataAdapter(strsql, strconnect);
                System.Data.DataSet selecttable = new System.Data.DataSet();
                adapter.Fill(selecttable);
                droplist.Items.Clear();
                if (selecttable.Tables[0].Rows.Count > 0)
                {
                    droplist.DataSource = selecttable.Tables[0];
                    droplist.DataTextField = selecttable.Tables[0].Columns[0].ColumnName;
                    droplist.DataValueField = selecttable.Tables[0].Columns[1].ColumnName;
                    droplist.DataBind();
                }
                droplist.Items.Insert(0, "--Choose One--");
                droplist.Items[0].Value = "0";
                droplist.SelectedIndex = droplist.Items.IndexOf(droplist.Items.FindByText("Select"));
                adapter.Dispose();
                selecttable.Dispose();
            }
            catch (Exception ERR)
            {
                throw (ERR);
            }
        }

        public void Fillcombo(String strsql, DropDownList droplist, string dispitem)
        {
            try
            {
                // strconnect = page.Session["strconnect1"].ToString();
                System.Data.SqlClient.SqlDataAdapter adapter = null;
                adapter = new System.Data.SqlClient.SqlDataAdapter(strsql, strconnect);
                System.Data.DataSet selecttable = new System.Data.DataSet();
                adapter.Fill(selecttable);
                droplist.Items.Clear();
                if (selecttable.Tables[0].Rows.Count > 0)
                {
                    droplist.DataSource = selecttable.Tables[0];
                    droplist.DataTextField = selecttable.Tables[0].Columns[0].ColumnName;
                    droplist.DataValueField = selecttable.Tables[0].Columns[1].ColumnName;
                    droplist.DataBind();
                }
                droplist.Items.Insert(0, dispitem);
                droplist.Items[0].Value = "0";
                droplist.SelectedIndex = droplist.Items.IndexOf(droplist.Items.FindByText("Select"));
                adapter.Dispose();
                selecttable.Dispose();
            }
            catch (Exception ERR)
            {
                throw (ERR);
            }
        }
        public void Fillcombo_Parameter(string stored_text, Hashtable parameters, DropDownList droplist)
        {
            try
            {
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(strconnect);
                SqlCommand cmd = new SqlCommand(stored_text, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataset = new DataSet();
                DictionaryEntry Item = default(DictionaryEntry);
                cmd.CommandType = CommandType.Text;
                foreach (DictionaryEntry Item_loopVariable in parameters)
                {
                    Item = Item_loopVariable;
                    cmd.Parameters.AddWithValue(Item.Key.ToString(), Item.Value.ToString());
                }
                adapter.SelectCommand = cmd;
                adapter.Fill(dataset);
                droplist.Items.Clear();
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    droplist.DataSource = dataset.Tables[0];
                    droplist.DataTextField = dataset.Tables[0].Columns[0].ColumnName;
                    droplist.DataValueField = dataset.Tables[0].Columns[1].ColumnName;
                    droplist.DataBind();
                }
                droplist.Items.Insert(0, "- -Select- -");
                droplist.Items[0].Value = "0";
                droplist.SelectedIndex = droplist.Items.IndexOf(droplist.Items.FindByText("- -Select- -"));
                adapter.Dispose();
                dataset.Dispose();
            }
            catch (Exception ERR)
            {
                throw (ERR);
            }
        }
        public void FillListBox(System.String strsql, ListBox listbox)
        {
            try
            {
                System.Data.SqlClient.SqlDataAdapter adapter = null;
                adapter = new System.Data.SqlClient.SqlDataAdapter(strsql, strconnect);
                System.Data.DataSet selecttable = new System.Data.DataSet();
                adapter.Fill(selecttable);
                listbox.Items.Clear();
                if (selecttable.Tables[0].Rows.Count > 0)
                {
                    listbox.DataSource = selecttable.Tables[0];
                    listbox.DataTextField = selecttable.Tables[0].Columns[0].ColumnName;
                    listbox.DataValueField = selecttable.Tables[0].Columns[1].ColumnName;
                    listbox.DataBind();
                }
                adapter.Dispose();
                selecttable.Dispose();
            }
            catch (Exception ERR)
            {
                throw (ERR);
            }
        }

    }
}