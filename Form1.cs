using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace XMLimport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtSourcePath.Text = OFD.FileName;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //XmlDocument xml = new XmlDocument();

            string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {

                XmlTextReader xmlreader = new XmlTextReader(txtSourcePath.Text);
                //DataSet ds = new DataSet();
                //ds.ReadXml(xmlreader);
                //xmlreader.Close();
                //if (ds.Tables.Count != 0)
                //{

                //        con.Open();
                //        SqlCommand cmd = new SqlCommand("XMLAccountInfo", con);
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = ds.GetXml();
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataSet ds1 = new DataSet();
                //        da.Fill(ds1);
                //        //gvDetails.DataSource = ds1;
                //        //gvDetails.DataBind();
                //        con.Close();
                //    }



                //try
                //{
                //    string fileName, filepath, contentXml;
                //    fileName = txtSourcePath.Text;
                //    filepath = Server.MapPath("~/upload/") + fileName;
                //    FileUpload1.SaveAs(filepath);
                //    string xmlfile = File.ReadAllText(filepath);
                //    SqlCommand cmd = new SqlCommand("InsertXMLData", con);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.AddWithValue("@xmlfile", xmlfile);
                //    con.Open();
                //    cmd.ExecuteNonQuery();
                //    con.Close();
                //    Response.Write("<script type=\"text/javascript\">alert('file uploaded successfully');</script>");
                //}
                //catch (Exception ex) { }

                DataSet ds = new DataSet();

                //xml.loadxml(txtsourcepath.text);

                ds.ReadXml(txtSourcePath.Text);
                DataTable dtaccount = ds.Tables["accountinfo"];
                DataTable dtsnippet = ds.Tables["snippetinfo"];
                DataTable dtnote = ds.Tables["noteinfo"];
                DataTable dtadditional = ds.Tables["additionalinfo"];
                DataTable dtxmldocument = ds.Tables["xmldocumentinfo"];
                //datatable dtemp = ds.tables["employee"];


                con.Open();

                //using (SqlBulkCopy sb = new SqlBulkCopy(con))
                //{

                //    sb.DestinationTableName = "accountinfo";
                //    sb.ColumnMappings.Add("accountnumber", "accountnumber");
                //    sb.ColumnMappings.Add("accounttype", "accounttype");
                //    sb.WriteToServer(dtaccount);
                //}

                //using (SqlBulkCopy sb = new SqlBulkCopy(con))
                //{
                //    sb.DestinationTableName = "snippetinfo";
                //    //sb.columnmappings.add("documentnumber", 1);
                //    sb.ColumnMappings.Add("snippetfiletype", "snippetfiletype");
                //    sb.ColumnMappings.Add("snippetfilename", "snippetfilename");
                //    sb.WriteToServer(dtsnippet);
                //}

                //using (SqlBulkCopy sb = new SqlBulkCopy(con))
                //{
                //    sb.DestinationTableName = "noteinfo";
                //    // sb.columnmappings.add("documentnumber", "1");
                //    sb.ColumnMappings.Add("notedate", "notedate");
                //    sb.ColumnMappings.Add("notetext", "notetext");
                //    sb.ColumnMappings.Add("operator", "operator");
                //    sb.ColumnMappings.Add("pagenumber", "pagenumber");
                //    sb.ColumnMappings.Add("priority", "priority");
                //    sb.WriteToServer(dtnote);
                //}
                //using (SqlBulkCopy sb = new SqlBulkCopy(con))
                //{
                //    sb.DestinationTableName = "additionalinfo";
                //    // sb.columnmappings.add("documentnumber", "1");
                //    sb.ColumnMappings.Add("fieldname", "fieldname");
                //    sb.ColumnMappings.Add("fieldvalue", "fieldvalue");
                //    sb.WriteToServer(dtadditional);
                //}
                using (SqlBulkCopy sb = new SqlBulkCopy(con))
                {
                    sb.DestinationTableName = "xmldocumentinfo";
                    sb.ColumnMappings.Add(1, "DocumentNumber");
                    sb.ColumnMappings.Add("Documenttype", "Documenttype");
                    sb.ColumnMappings.Add("Date", "Date");
                    sb.ColumnMappings.Add("Description", "Description");
                    sb.ColumnMappings.Add("ImageFileType", "DocumentFileType");
                    sb.ColumnMappings.Add("ImageFileName", "DocumentFileName");

                    sb.WriteToServer(dtxmldocument);
                }
                con.Close();

            }
        }
    }
}

