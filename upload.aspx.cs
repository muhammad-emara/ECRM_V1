using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class upload : System.Web.UI.Page
{
    public string UploadMesage = null;


      string connectionString = @"Data Source=SQL.esupport.net;Initial Catalog=ECRMIslam;User ID=sa;Password=1234;Connect Timeout =600";


      void InsertIntoDB(string FilePath)
      {
          string insertSQL;

          insertSQL = "INSERT INTO Invoices ([Number], [Class], [Currency], [Original], [FunctionalAmount], [InvoicesRemainingDues], [FunctionalRemainingDue], [PositiveValue], [MonthC], [YearC], [InvoiceDate], [CustomerNumber], [CustomerName], [CustomerTypeName], [CustomerCategoryName], [MarketSegmentName], [BusLineName], [PartnerName], [PartnerName2], [CustAdminStatusName], [WarrantyDays], [ACRT] ) ";
          insertSQL += "SELECT A.[Number], A.[Class], A.[Currency], A.[Original], A.[FunctionalAmount], A.[InvoicesRemainingDues], A.[FunctionalRemainingDue], A.[PositiveValue], A.[MonthC], A.[YearC], A.[InvoiceDate], A.[CustomerNumber], A.[CustomerName], A.[CustomerTypeName], A.[CustomerCategoryName], A.[MarketSegmentName], A.[BusLineName], A.[PartnerName], A.[PartnerName2], A.[CustAdminStatusName], A.[WarrantyDays], A.[ACRT] ";
          insertSQL += "FROM OPENROWSET ";
          //insertSQL += @"('Microsoft.ACE.OLEDB.12.0','Excel 12.0 XML;Connect Timeout =600;Database=C:\Users\islam.abdelrazik\Desktop\SampleExcelImport.xlsx;HDR=YES', 'select * from [Sheet1$]') AS A;";
          insertSQL += @"('Microsoft.ACE.OLEDB.12.0','Excel 12.0 XML;Connect Timeout =600;Database=" + FilePath + ";HDR=YES', 'select * from [Sheet1$]') AS A;";

          // Path to file on server = C:\Users\islam.abdelrazik\Desktop\SampleExcelImport.xls


          SqlConnection con = new SqlConnection(connectionString);
          SqlCommand cmd = new SqlCommand(insertSQL, con);
          cmd.CommandTimeout = 600;
          int added = 0;
          try
          {
              con.Open();
              added = cmd.ExecuteNonQuery();
              UploadMesage = "Upload File Successufel and Inserted Into DB ";

          }

          catch (Exception err)
          {
              UploadMesage = err.Message;
              //Response.Write(err.Message + "  Code.287");
          }
          finally
          {
              con.Close();
          }


          /////////////// DELETE Empty Rows /////////////////////////
          insertSQL = "DELETE FROM Invoices WHERE [Number] IS NUll; ";

          // Path to file on server = C:\Users\islam.abdelrazik\Desktop\SampleExcelImport.xls

          con = new SqlConnection(connectionString);
          cmd = new SqlCommand(insertSQL, con);
          cmd.CommandTimeout = 600;
          added = 0;
          try
          {
              con.Open();
              added = cmd.ExecuteNonQuery();

          }

          catch (Exception err)
          {
              Response.Write(err.Message + "  Code.287");
          }
          finally
          {
              con.Close();
          }

      }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        /*
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string FilePath = Server.MapPath(FolderPath + FileName);
            FileUpload1.SaveAs(FilePath);
            //Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        }
        */



        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0)
        {

            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string folder = Server.MapPath("~/files/");
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FilePath = Server.MapPath(FolderPath + "/files/" + fileName);
            bool error = false;
            switch (Extension)
            {
                case ".xls": //Excel 97-03

                case ".xlsx": //Excel 07
                    
                    break;
                default: error = true; break;
            }
            
            DateTime SelectedDate = Calendar1.SelectedDate;
            //Response.Write(SelectedDate.Date.ToString());
            if (SelectedDate == null)
            {
                UploadMesage = "Error : Please select date of sheet";
                error = true;
            }

            if(error == false)
            {
                    Directory.CreateDirectory(folder);
                    FileUpload1.PostedFile.SaveAs(Path.Combine(folder, fileName));
                    try
                    {
                        InsertIntoDB(FilePath);

                    }
                    catch
                    {
                        UploadMesage = "Operation Failed!!!";
                        //Response.Write(UploadMesage);
                    }

            }
            else
            {
                if (SelectedDate == null)
                {
                    UploadMesage = "Error : Please select date of sheet";
                    error = true;
                }
                else
                {
                    UploadMesage = "Error File Type ! Only " + "( .xls ) Or " + "(.xlsx) Supported";
                }
                
            }


        }








    }
    bool CalenderSelected = false;

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        CalenderSelected = true;
    }
}