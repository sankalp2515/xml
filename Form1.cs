using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xmlImport
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
                    string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cs))
            
                    {
                            DataSet ds = new DataSet();

                            //xml.loadxml(txtsourcepath.text);

                            ds.ReadXml(txtSourcePath.Text);
                            if (rdBulk.Checked)
                            {

                            }
                            else if (rdLine.Checked)
                            {

                                using (XmlTextReader reader = new XmlTextReader(txtSourcePath.Text))
                                {
                                    String AccountNumber = " ";
                                    String AccountType = " ";
                                    String SnippetFileType = " ";
                                    String SnippetFileName = " ";
                                    String NoteDate = " ";
                                    String NoteText = " ";
                                    String Operator = " ";
                                    String PageNumber = " ";
                                    String Priority = " ";
                                    String FieldName = " ";
                                    String FieldValue = " ";
                                    String DocumentType = " ";
                                    String Date = " ";
                                    String Description = " ";
                                    String ImageFileType = " ";
                                    String ImageFileName = " ";
                                     int count = 1;
                        
                        
                                    while (reader.Read())
                                    {
                           
                            
                                        con.Open();

                                          if (reader.IsStartElement("DocumentInfo"))
                                        {
                                            SqlCommand insertCommand1 = new SqlCommand("XMLDocumentInfo", con);
                                            while (reader.Read() && reader.IsStartElement())
                                            {
                                                   
                                                        switch (reader.Name)
                                                    {
                                                        case "DocumentType":
                                                            DocumentType = reader.ReadString();
                                                            break;
                                                        case "Date":
                                                            Date = reader.ReadString();
                                                            break;
                                                        case "Description":
                                                            Description = reader.ReadString();
                                                            break;
                                                        case "ImageFileType":
                                                            ImageFileType = reader.ReadString();
                                                            break;
                                                        case "ImageFileName":
                                                            ImageFileName = reader.ReadString();
                                                            break;
                                                            //default:
                                                            //    throw new InvalidExpressionException("Unexpected tag");
                                                    }
                                    
                                                  if (reader.IsStartElement("AccountInfo"))
                                                {
                                                    SqlCommand insertCommand = new SqlCommand("XMLAccountInfo", con);
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
                                                        
                                                        SqlCommand c = new SqlCommand();
                                                        
                                                        
                                                        c.Connection = con;

                                                        
                                                        
                                                        c.CommandType = CommandType.Text;
                                                        c.CommandType = CommandType.Text;
                                                        c.CommandText = "UPDATE AccountInfo SET DocumentNumber = @count Where DocumentNumber = 'NULL'";
                                                        SqlDataReader d = c.ExecuteReader();







                                        }
                                                    insertCommand.CommandType = CommandType.StoredProcedure;
                                                    insertCommand.Parameters.AddWithValue("AccountNumber", AccountNumber);
                                                    insertCommand.Parameters.AddWithValue("AccountType", AccountType);
                                                    if (!((AccountNumber == " " & AccountType == " ")))
                                                    {
                                                        insertCommand.ExecuteNonQuery();
                                                    }
                                                }
                                                else if (reader.IsStartElement("SnippetInfo"))
                                                {
                                                    SqlCommand insertCommand = new SqlCommand("XMLSnippetInfo", con);
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
                                                    insertCommand.CommandType = CommandType.StoredProcedure;
                                                    insertCommand.Parameters.AddWithValue("SnippetFileType", SnippetFileType);
                                                    insertCommand.Parameters.AddWithValue("SnippetFileName", SnippetFileName);
                                                    if (!((SnippetFileType == " " & SnippetFileName == " ")))
                                                    {
                                                        insertCommand.ExecuteNonQuery();
                                                    }
                                                }
                                                else if (reader.IsStartElement("NoteInfo"))
                                                {
                                                    SqlCommand insertCommand = new SqlCommand("XMLNoteInfo", con);
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
                                                    insertCommand.CommandType = CommandType.StoredProcedure;
                                                    insertCommand.Parameters.AddWithValue("NoteDate", NoteDate);
                                                    insertCommand.Parameters.AddWithValue("NoteText", NoteText);
                                                    insertCommand.Parameters.AddWithValue("Operator", Operator);
                                                    insertCommand.Parameters.AddWithValue("PageNumber", PageNumber);
                                                    insertCommand.Parameters.AddWithValue("Priority", Priority);

                                                    if (!((NoteDate == " " & NoteText == " " & Operator == " " & PageNumber == " " & Priority == " ")))
                                                    {
                                                        insertCommand.ExecuteNonQuery();
                                                    }
                                                }
                                                 if (reader.IsStartElement("AdditionalInfo"))
                                                {
                                                    SqlCommand insertCommand = new SqlCommand("XMLAdditionalInfo", con);
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
                                                    insertCommand.CommandType = CommandType.StoredProcedure;
                                                    insertCommand.Parameters.AddWithValue("FieldName", FieldName);
                                                    insertCommand.Parameters.AddWithValue("FieldValue", FieldValue);
                                                    if (!((FieldName == " " & FieldValue == " ")))
                                                    {
                                                        insertCommand.ExecuteNonQuery();
                                                    }
                                                }
                                                reader.ReadEndElement();
                                            }
                                                SqlCommand cmd = new SqlCommand();

                                                cmd.CommandText = "select DocumentNumber From  DocumentInfo";
                                                cmd.Connection = con;

                                                SqlDataReader dr = cmd.ExecuteReader();
                                                dr.Read();
                                                count = dr.GetInt32(0);
                                            insertCommand1.CommandType = CommandType.StoredProcedure;
                                            insertCommand1.Parameters.AddWithValue("DocumentType", DocumentType);
                                            insertCommand1.Parameters.AddWithValue("Date", Date);
                                            insertCommand1.Parameters.AddWithValue("Description", Description);
                                            insertCommand1.Parameters.AddWithValue("ImageFileType", ImageFileType);
                                            insertCommand1.Parameters.AddWithValue("ImageFileName", ImageFileName);
                                            if (!((DocumentType == " " & Date == " " & Description == " " & ImageFileType == " " & ImageFileName == " ")))
                                            {
                                                insertCommand1.ExecuteNonQuery();
                                            }
                                                    
                                                    
                                                    
                                        }
                                        //  else  if (reader.IsStartElement("AccountInfo"))
                                        //{
                                        //    SqlCommand insertCommand = new SqlCommand("XMLAccountInfo", con);
                                        //    while (reader.Read() && reader.IsStartElement())
                                        //    {
                                        //        switch (reader.Name)
                                        //        {
                                        //            case "AccountNumber":
                                        //                AccountNumber = reader.ReadString();
                                        //                break;
                                        //            case "AccountType":
                                        //                AccountType = reader.ReadString();
                                        //                break;
                                        //            default:
                                        //                throw new InvalidExpressionException("Unexpected tag");
                                        //        }
                                        //        reader.ReadEndElement();
                                        //    }
                                        //    insertCommand.CommandType = CommandType.StoredProcedure;
                                        //    insertCommand.Parameters.AddWithValue("AccountNumber", AccountNumber);
                                        //    insertCommand.Parameters.AddWithValue("AccountType", AccountType);
                                        //    if (!((AccountNumber == " " & AccountType == " ")))
                                        //    {
                                        //        insertCommand.ExecuteNonQuery();
                                        //    }
                                        //}
                                        //else if (reader.IsStartElement("SnippetInfo"))
                                        //{
                                        //    SqlCommand insertCommand = new SqlCommand("XMLSnippetInfo", con);
                                        //    while (reader.Read() && reader.IsStartElement())
                                        //    {
                                        //        switch (reader.Name)
                                        //        {
                                        //            case "SnippetFileType":
                                        //                SnippetFileType = reader.ReadString();
                                        //                break;
                                        //            case "SnippetFileName":
                                        //                SnippetFileName = reader.ReadString();
                                        //                break;
                                        //            default:
                                        //                throw new InvalidExpressionException("Unexpected tag");
                                        //        }
                                        //        reader.ReadEndElement();
                                        //    }
                                        //    insertCommand.CommandType = CommandType.StoredProcedure;
                                        //    insertCommand.Parameters.AddWithValue("SnippetFileType", SnippetFileType);
                                        //    insertCommand.Parameters.AddWithValue("SnippetFileName", SnippetFileName);
                                        //    if (!((SnippetFileType == " " & SnippetFileName == " ")))
                                        //    {
                                        //        insertCommand.ExecuteNonQuery();
                                        //    }
                                        //}
                                        //else if (reader.IsStartElement("NoteInfo"))
                                        //{
                                        //    SqlCommand insertCommand = new SqlCommand("XMLNoteInfo", con);
                                        //    while (reader.Read() && reader.IsStartElement())
                                        //    {
                                        //        switch (reader.Name)
                                        //        {
                                        //            case "NoteDate":
                                        //                NoteDate = reader.ReadString();
                                        //                break;
                                        //            case "NoteText":
                                        //                NoteText = reader.ReadString();
                                        //                break;
                                        //            case "Operator":
                                        //                Operator = reader.ReadString();
                                        //                break;
                                        //            case "PageNumber":
                                        //                PageNumber = reader.ReadString();
                                        //                break;
                                        //            case "Priority":
                                        //                Priority = reader.ReadString();
                                        //                break;
                                        
                                        //            default:
                                        //                throw new InvalidExpressionException("Unexpected tag");
                                        //        }
                                        //        reader.ReadEndElement();
                                        //    }
                                        //    insertCommand.CommandType = CommandType.StoredProcedure;
                                        //    insertCommand.Parameters.AddWithValue("NoteDate", NoteDate);
                                        //    insertCommand.Parameters.AddWithValue("NoteText", NoteText);
                                        //    insertCommand.Parameters.AddWithValue("Operator", Operator);
                                        //    insertCommand.Parameters.AddWithValue("PageNumber", PageNumber);
                                        //    insertCommand.Parameters.AddWithValue("Priority", Priority);
                                
                                        //    if (!((NoteDate == " " & NoteText == " "& Operator == " " & PageNumber == " "& Priority == " " )))
                                        //    {
                                        //        insertCommand.ExecuteNonQuery();
                                        //    }
                                        //}
                                        //else if (reader.IsStartElement("AdditionalInfo"))
                                        //{
                                        //    SqlCommand insertCommand = new SqlCommand("XMLAdditionalInfo", con);
                                        //    while (reader.Read() && reader.IsStartElement())
                                        //    {
                                        //        switch (reader.Name)
                                        //        {
                                        //            case "FieldName":
                                        //                FieldName = reader.ReadString();
                                        //                break;
                                        //            case "FieldValue":
                                        //                FieldValue = reader.ReadString();
                                        //                break;
                                        //            default:
                                        //                throw new InvalidExpressionException("Unexpected tag");
                                        //        }
                                        //        reader.ReadEndElement();
                                        //    }
                                        //    insertCommand.CommandType = CommandType.StoredProcedure;
                                        //    insertCommand.Parameters.AddWithValue("FieldName", FieldName);
                                        //    insertCommand.Parameters.AddWithValue("FieldValue", FieldValue);
                                        //    if (!((FieldName == " " & FieldValue == " ")))
                                        //    {
                                        //        insertCommand.ExecuteNonQuery();
                                        //    }
                                        //}                     

                                        con.Close();
                                    }


                                }


                            }

                    }
                }
        }
    }

