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

                //XmlTextReader xmlreader = new XmlTextReader(txtSourcePath.Text);


                DataSet ds = new DataSet();

                //xml.loadxml(txtsourcepath.text);

                ds.ReadXml(txtSourcePath.Text);
                if (rdBulk.Checked)
                {

                    DataTable dtaccount = ds.Tables["Accountinfo"];
                    DataTable dtsnippet = ds.Tables["Snippetinfo"];
                    DataTable dtnote = ds.Tables["Noteinfo"];
                    DataTable dtadditional = ds.Tables["Additionalinfo"];
                    DataTable dtxmldocument = ds.Tables["DocumentInfo"];
                    //datatable dtemp = ds.tables["employee"];


                    con.Open();
                    using (SqlBulkCopy sb = new SqlBulkCopy(con))
                    {
                        sb.DestinationTableName = "DocumentInfo";
                        //sb.ColumnMappings.Add(1, "DocumentNumber");
                        sb.ColumnMappings.Add("DocumentType", "DocumentType");
                        sb.ColumnMappings.Add("Date", "Date");
                        sb.ColumnMappings.Add("Description", "Description");
                        sb.ColumnMappings.Add("ImageFileType", "DocumentFileType");
                        sb.ColumnMappings.Add("ImageFileName", "DocumentFileName");

                        sb.WriteToServer(dtxmldocument);
                    }

                    using (SqlBulkCopy sb = new SqlBulkCopy(con))
                    {

                        sb.DestinationTableName = "AccountInfo";
                        sb.ColumnMappings.Add("AccountNumber", "AccountNumber");
                        sb.ColumnMappings.Add("AccountType", "AccountType");
                        sb.WriteToServer(dtaccount);
                    }

                    using (SqlBulkCopy sb = new SqlBulkCopy(con))
                    {
                        sb.DestinationTableName = "SnippetInfo";
                        //sb.columnmappings.add("documentnumber", 1);
                        sb.ColumnMappings.Add("SnippetFileType", "SnippetFileType");
                        sb.ColumnMappings.Add("SnippetFileName", "SnippetFileName");
                        sb.WriteToServer(dtsnippet);
                    }

                    using (SqlBulkCopy sb = new SqlBulkCopy(con))
                    {
                        sb.DestinationTableName = "NoteInfo";
                        // sb.columnmappings.add("documentnumber", "1");
                        sb.ColumnMappings.Add("NoteDate", "NoteDate");
                        sb.ColumnMappings.Add("NoteText", "NoteText");
                        sb.ColumnMappings.Add("Operator", "Operator");
                        sb.ColumnMappings.Add("PageNumber", "PageNumber");
                        sb.ColumnMappings.Add("Priority", "Priority");
                        sb.WriteToServer(dtnote);
                    }
                    using (SqlBulkCopy sb = new SqlBulkCopy(con))
                    {
                        sb.DestinationTableName = "AdditionalInfo";
                        // sb.columnmappings.add("documentnumber", "1");
                        sb.ColumnMappings.Add("FieldName", "FieldName");
                        sb.ColumnMappings.Add("FieldValue", "FieldValue");
                        sb.WriteToServer(dtadditional);
                    }

                    con.Close();
                    MessageBox.Show("Data Imported Successfully");
                }
                else if (rdLine.Checked)
                {
                    using (XmlTextReader reader = new XmlTextReader(txtSourcePath.Text))
                    {
                        while (reader.Read())
                        {
                            String AccountNumber        = " ";
                            String AccountType          = " ";
                            String SnippetFileType      = " ";
                            String SnippetFileName      = " ";
                            String NoteDate             = " ";
                            String NoteText             = " ";
                            String Operator             = " ";
                            String PageNumber           = " ";
                            String Priority             = " ";
                            String FieldName            = " ";
                            String FieldValue           = " ";
                            String DocumentType         = " ";
                            String Date                 = " ";
                            String Description          = " ";
                            String DocumentFileType     = " ";
                            String DocumentFileName     = " ";
                            con.Open();
                            //SqlCommand insertCommand = new SqlCommand("XMLDocumentInfo", con);
                            SqlCommand insertCommand1 = new SqlCommand("XMLAccountInfo", con);
                            SqlCommand insertCommand2 = new SqlCommand("XMLSnippetInfo", con);
                            SqlCommand insertCommand3 = new SqlCommand("XMLNoteInfo", con);
                            SqlCommand insertCommand4 = new SqlCommand("XMLAdditional", con);

                            //if (reader.IsStartElement("DocumentInfo"))
                            //{
                                
                            //    while (reader.Read() && reader.IsStartElement())
                            //    {
                            //        switch (reader.Name)
                            //        {
                            //            case "DocumentType":
                            //                DocumentType = reader.ReadString();
                            //                break;
                            //            case "Date":
                            //                Date = reader.ReadString();
                            //                break;
                            //            case "Description":
                            //                Description = reader.ReadString();
                            //                break;
                            //            case "DocumentFileType":
                            //                DocumentFileType = reader.ReadString();
                            //                break;
                            //            case "DocumentFileName":
                            //                DocumentFileName = reader.ReadString();
                            //                break;
                            //            default:
                            //                throw new InvalidExpressionException("Unexpected tag");
                            //        }
                            //        reader.ReadEndElement();
                            //    }
                            //}
                            if (reader.IsStartElement("AccountInfo"))
                            {
                               
                                while (reader.Read() && reader.IsStartElement())
                                {
                                    switch (reader.Name)
                                    {
                                        case "AccountNumber":
                                            AccountNumber = reader.ReadString();
                                            break;
                                        case "AccountType":
                                            AccountType = reader.ReadString();
                                            break;
                                        default:
                                            throw new InvalidExpressionException("Unexpected tag");
                                    }
                                    reader.ReadEndElement();
                                }
                            }
                            
                            else if (reader.IsStartElement("AdditionalInfo"))
                            {



                                while (reader.Read() && reader.IsStartElement())
                                {
                                    switch (reader.Name)
                                    {
                                        case "FieldName":
                                            FieldName = reader.ReadString();
                                            break;
                                        case "FieldValue":
                                            FieldValue = reader.ReadString();
                                            break;
                                        default:
                                            throw new InvalidExpressionException("Unexpected tag");
                                    }
                                    reader.ReadEndElement();
                                }
                            }
                            else if (reader.IsStartElement("NoteInfo"))
                            {
                                while (reader.Read() && reader.IsStartElement())
                                {
                                    switch (reader.Name)
                                    {
                                        case "NoteDate":
                                            NoteDate = reader.ReadString();
                                            break;
                                        case "NoteText":
                                            NoteText = reader.ReadString();
                                            break;
                                        case "Operator":
                                            Operator = reader.ReadString();
                                            break;
                                        case "PageNumber":
                                            PageNumber = reader.ReadString();
                                            break;
                                        case "Priority":
                                            Priority = reader.ReadString();
                                            break;
                                        default:
                                            throw new InvalidExpressionException("Unexpected tag");
                                    }
                                    reader.ReadEndElement();
                                }
                            }
                            else if (reader.IsStartElement("SnippetInfo"))
                            {
                                while (reader.Read() && reader.IsStartElement())
                                {
                                    switch (reader.Name)
                                    {
                                        case "SnippetFileType":
                                            SnippetFileType = reader.ReadString();
                                            break;
                                        case "SnippetFileName":
                                            SnippetFileName = reader.ReadString();
                                            break;
                                        default:
                                            throw new InvalidExpressionException("Unexpected tag");
                                    }
                                    reader.ReadEndElement();
                                }
                            }


                                //insertCommand.CommandType = CommandType.StoredProcedure;
                                insertCommand1.CommandType = CommandType.StoredProcedure;
                                insertCommand2.CommandType = CommandType.StoredProcedure;
                                insertCommand3.CommandType = CommandType.StoredProcedure;
                                insertCommand4.CommandType = CommandType.StoredProcedure;
                                //insertCommand.Parameters.AddWithValue("DocumentType", DocumentType);
                                //insertCommand.Parameters.AddWithValue("Date", Date);
                                //insertCommand.Parameters.AddWithValue("Description", Description);
                                //insertCommand.Parameters.AddWithValue("DocumentFileType", DocumentFileType);
                                //insertCommand.Parameters.AddWithValue("DocumentFileName", DocumentFileName);
                                insertCommand1.Parameters.AddWithValue("AccountNumber", AccountNumber);
                                insertCommand1.Parameters.AddWithValue("AccountType", AccountType);
                                insertCommand2.Parameters.AddWithValue("SnippetFileType", SnippetFileType);
                                insertCommand2.Parameters.AddWithValue("SnippetFileName", SnippetFileName);
                                insertCommand3.Parameters.AddWithValue("NoteDate", NoteDate);
                                insertCommand3.Parameters.AddWithValue("NoteText", NoteText);
                                insertCommand3.Parameters.AddWithValue("Operator", Operator);
                                insertCommand3.Parameters.AddWithValue("PageNumber", PageNumber);
                                insertCommand3.Parameters.AddWithValue("Priority", Priority);
                                insertCommand4.Parameters.AddWithValue("FieldName", FieldName);
                                insertCommand4.Parameters.AddWithValue("FieldValue", FieldValue);


                            if (!((DocumentType == " " & Date == " " & Description == " " & DocumentFileType == " " & DocumentFileName == " "  & AccountNumber == " " & AccountType == " "
                                & SnippetFileType == " " & SnippetFileType == " " & NoteDate == " " & NoteText == " " & Operator == " " & PageNumber == " "
                                & Priority == " " & FieldName == " " & FieldValue == " ")))
                                {
                                    insertCommand1.ExecuteNonQuery();
                                    insertCommand2.ExecuteNonQuery();
                                    insertCommand3.ExecuteNonQuery();
                                    insertCommand4.ExecuteNonQuery();
                            }
                            
                            
                            con.Close();
                            
                        }

                        MessageBox.Show("Data Imported Successfully");


                    }

                }




            }
        }
    }
}


