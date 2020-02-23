using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ECRM : System.Web.UI.Page
{
    public SqlDataSource sql1 = null;


    public void FillListF(string CoulmnName , ListBox List)
    {
        try
        {
            SqlConnection sqlConnection = new SqlConnection(strSQLconnection);
            SqlDataReader reader = null;
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT(" + CoulmnName + ") FROM [ECRMIslam].[dbo].[Invoices] ORDER BY " + CoulmnName , sqlConnection);
            reader = sqlCommand.ExecuteReader();
            List.Items.Clear();
            while (reader.Read())
            {

                List.Items.Add(reader[CoulmnName].ToString());

            }
            reader.Close();
            sqlConnection.Close();


        }
        catch (Exception ex)
        {

        }
    }


    public void Class12(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(lstItemsWithChosen, this.GetType(), "showalert", "selectorstyle();", true);
        Timer1.Enabled = true;

        
    }

    string strSQLconnection = ConfigurationManager.ConnectionStrings["SqlConString"].ConnectionString;

    void CreateView(string ViewName, string ViewSelect)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(strSQLconnection);
            conn.Open();
            string strSQLCommand = "CREATE VIEW " + ViewName + " AS " + ViewSelect;
            SqlCommand command = new SqlCommand(strSQLCommand, conn);
            string returnvalue = (string)command.ExecuteScalar();
            conn.Close();
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);

        }

    }

    void DropView(string ViewName)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(strSQLconnection);
            conn.Open();
            string strSQLCommand = "DROP VIEW " + ViewName;
            SqlCommand command = new SqlCommand(strSQLCommand, conn);
            string returnvalue = (string)command.ExecuteScalar();
            conn.Close();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }

    }
    
    public string KAM_View = null;
    public string GDS_View = null;
    public string LCP_View = null;
    public string Terminated_View = null;
    public string DC_View = null;

    string Filters(string ClassFilter, string CustomerFilter, string TeamFilter, string AdminFilter, string YearFilter, string MonthFilter, string PartnerFilter, string ParnetFilter, string MarketFilter)
    {
        string AllFilters = null;

        if (ClassFilter != "")
        {
            AllFilters = " WHERE " + ClassFilter + " ";
        }
        //////////////////
        if (AllFilters != null && CustomerFilter != "")
        {
            AllFilters = AllFilters + " AND " + CustomerFilter + " ";
        }
        else if (AllFilters == null && CustomerFilter != "")
        {
            AllFilters = " WHERE " + CustomerFilter + " ";
        }
        //////////////////////
        if (AllFilters != null && TeamFilter != "")
        {
            AllFilters = AllFilters + " AND " + TeamFilter + " ";
        }
        else if (AllFilters == null && TeamFilter != "")
        {
            AllFilters = " WHERE " + TeamFilter + " ";
        }
        /////////////////////
        if (AllFilters != null && AdminFilter != "")
        {
            AllFilters = AllFilters + " AND " + AdminFilter + " ";
        }
        else if (AllFilters == null && AdminFilter != "")
        {
            AllFilters = " WHERE " + AdminFilter + " ";
        }
        /////////////////////
        if (AllFilters != null && YearFilter != "")
        {
            AllFilters = AllFilters + " AND " + YearFilter + " ";
        }
        else if (AllFilters == null && YearFilter != "")
        {
            AllFilters = " WHERE " + YearFilter + " ";
        }
        /////////////////////
        if (AllFilters != null && MonthFilter != "")
        {
            AllFilters = AllFilters + " AND " + MonthFilter + " ";
        }
        else if (AllFilters == null && MonthFilter != "")
        {
            AllFilters = " WHERE " + MonthFilter + " ";
        }
        /////////////////////
        if (AllFilters != null && PartnerFilter != "")
        {
            AllFilters = AllFilters + " AND " + PartnerFilter + " ";
        }
        else if (AllFilters == null && PartnerFilter != "")
        {
            AllFilters = " WHERE " + PartnerFilter + " ";
        }
        /////////////////////
        if (AllFilters != null && ParnetFilter != "")
        {
            AllFilters = AllFilters + " AND " + ParnetFilter + " ";
        }
        else if (AllFilters == null && ParnetFilter != "")
        {
            AllFilters = " WHERE " + ParnetFilter + " ";
        }
        /////////////////////
        if (AllFilters != null && MarketFilter != "")
        {
            AllFilters = AllFilters + " AND " + MarketFilter + " ";
        }
        else if (AllFilters == null && MarketFilter != "")
        {
            AllFilters = " WHERE " + MarketFilter + " ";
        }

        return AllFilters;
    }

    public string AllFilters = null;


    void TeamSummary()
    {
        
        ////////////////// Team Summary Data /////////////////

     
        string KAM_View = "SELECT * FROM  dbo.Invoices WHERE  (CustomerTypeName = 'Enterprise KAM') AND (CustAdminStatusName IS NULL OR";
        KAM_View += " CustAdminStatusName NOT IN ('Enterprise Terminated', 'Enterprise Cancelled', 'Enterprise Terminated & LA failed', 'Enterprise Transferred to LA', ";
        KAM_View += "'Enterprise Terminated & Excluded from LA'))";

        string GDS_View = "SELECT * FROM dbo.Invoices";
        GDS_View += " WHERE(MarketSegmentName = 'Global Partner')";

        string LCP_View = "SELECT * FROM dbo.Invoices WHERE (MarketSegmentName = 'Premium Partner') OR";
        LCP_View += " (MarketSegmentName = 'Financial') AND (PartnerName IN ('MIST', 'MICC.', 'IM solution', 'ITSC sky no limits', 'MACHO Systems.'))";


        string Terminated_View = "SELECT  * FROM [ECRMIslam].[dbo].[Invoices] WHERE dbo.Invoices.ID NOT IN (SELECT ID FROM KAM)";
        Terminated_View += " AND dbo.Invoices.ID NOT IN (SELECT ID FROM GDS) AND dbo.Invoices.ID NOT IN (SELECT ID FROM LCP) ";
        Terminated_View += " AND CustAdminStatusName IN ('Enterprise Terminated','Enterprise Cancelled','Enterprise Terminated & LA failed','Enterprise Transferred to LA',	'Enterprise Terminated & Excluded from LA')";

        string DC_View = "SELECT  * FROM [ECRMIslam].[dbo].[Invoices] ";
        DC_View += " WHERE [CustomerName] IN (SELECT [CustomerName] FROM [ECRMIslam].[dbo].[CasesCustomers])";



        //DropView("KAM");
        //DropView("GDS");
        //DropView("LCP");
        //DropView("DC");

        //CreateView("KAM", KAM_View);
        //CreateView("GDS", GDS_View);
        //CreateView("LCP", LCP_View);
        //CreateView("Terminated_View", Terminated_View);
        //CreateView("DC", DC_View);

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("All");
        DataRow dr;
        dt.Columns.Add(new DataColumn("Handling_Team", typeof(string)));
        dt.Columns.Add(new DataColumn("Class", typeof(string)));
        dt.Columns.Add(new DataColumn("Positive_Value", typeof(double)));
        dt.Columns.Add(new DataColumn("#_of_Invoices", typeof(Int32)));
        dt.Columns.Add(new DataColumn("#_of_Customers", typeof(Int32)));
        dt.Columns.Add(new DataColumn("#_of_Partners", typeof(Int32)));



        SqlConnection sqlConnection = new SqlConnection(strSQLconnection);
        SqlDataReader reader = null;
        sqlConnection.Open();

        //1.	KAM
        SqlCommand sqlCommand = new SqlCommand("SELECT CustomerTypeName AS Handling_Team , Class AS Class, SUM(PositiveValue) AS Positive_Value , COUNT(Number) As #_of_Invoices ,Count(DISTINCT(CustomerNumber)) AS #_of_Customers , Count(DISTINCT(PartnerName)) AS #_of_Partners FROM [ECRMIslam].[dbo].[KAM]" + AllFilters + " GROUP BY CustomerTypeName , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "KAM";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["#_of_Invoices"] = Convert.ToInt32(reader["#_of_Invoices"].ToString());
            dr["#_of_Customers"] = Convert.ToInt32(reader["#_of_Customers"].ToString());
            dr["#_of_Partners"] = Convert.ToInt32(reader["#_of_Partners"].ToString());
            dt.Rows.Add(dr);
        }
        reader.Close();

        //2.	GDS
        sqlCommand = new SqlCommand("SELECT MarketSegmentName AS Handling_Team , Class AS Class, SUM(PositiveValue) AS Positive_Value , COUNT(Number) As #_of_Invoices ,Count(DISTINCT(CustomerNumber)) AS #_of_Customers , Count(DISTINCT(PartnerName)) AS #_of_Partners FROM [ECRMIslam].[dbo].[GDS] " + AllFilters + " GROUP BY MarketSegmentName , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "GDS";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["#_of_Invoices"] = Convert.ToInt32(reader["#_of_Invoices"].ToString());
            dr["#_of_Customers"] = Convert.ToInt32(reader["#_of_Customers"].ToString());
            dr["#_of_Partners"] = Convert.ToInt32(reader["#_of_Partners"].ToString());
            dt.Rows.Add(dr);
        }
        reader.Close();
        //3.	Local Partner
        sqlCommand = new SqlCommand("SELECT  Class AS Class, SUM(PositiveValue) AS Positive_Value , COUNT(Number) As #_of_Invoices ,Count(DISTINCT(CustomerNumber)) AS #_of_Customers , Count(DISTINCT(PartnerName)) AS #_of_Partners FROM [ECRMIslam].[dbo].[LCP] " + AllFilters + " GROUP BY  Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "Local Partner";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["#_of_Invoices"] = Convert.ToInt32(reader["#_of_Invoices"].ToString());
            dr["#_of_Customers"] = Convert.ToInt32(reader["#_of_Customers"].ToString());
            dr["#_of_Partners"] = Convert.ToInt32(reader["#_of_Partners"].ToString());
            dt.Rows.Add(dr);
        }
        reader.Close();
        //4.	Terminated

        sqlCommand = new SqlCommand("SELECT  Class AS Class, SUM(PositiveValue) AS Positive_Value , COUNT(Number) As #_of_Invoices ,Count(DISTINCT(CustomerNumber)) AS #_of_Customers , Count(DISTINCT(PartnerName)) AS #_of_Partners FROM [ECRMIslam].[dbo].[Terminated_View] " + AllFilters + " GROUP BY  Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);

        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "Terminated";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["#_of_Invoices"] = Convert.ToInt32(reader["#_of_Invoices"].ToString());
            dr["#_of_Customers"] = Convert.ToInt32(reader["#_of_Customers"].ToString());
            dr["#_of_Partners"] = Convert.ToInt32(reader["#_of_Partners"].ToString());
            dt.Rows.Add(dr);
        }

        reader.Close();
        //4.	Direct-Case

        sqlCommand = new SqlCommand("SELECT  Class AS Class, SUM(PositiveValue) AS Positive_Value , COUNT(Number) As #_of_Invoices ,Count(DISTINCT(CustomerNumber)) AS #_of_Customers , Count(DISTINCT(PartnerName)) AS #_of_Partners FROM [ECRMIslam].[dbo].[DC] " + AllFilters + " GROUP BY  Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "Direct-Case";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["#_of_Invoices"] = Convert.ToInt32(reader["#_of_Invoices"].ToString());
            dr["#_of_Customers"] = Convert.ToInt32(reader["#_of_Customers"].ToString());
            dr["#_of_Partners"] = Convert.ToInt32(reader["#_of_Partners"].ToString());
            dt.Rows.Add(dr);
        }



        ds.Tables.Add(dt);
        GridView1.DataSource = ds;
        //GridView1.DataSource = reader;
        GridView1.DataBind();
        //sqlConnection.Close();
        //reader.Close();
        ////////////Team Summary End///////////////////////////

    }


    void CustomerSummary()
    {
        ////////////////// Team Summary Data /////////////////



        DataSet ds = new DataSet();
        DataTable dt = new DataTable("All");
        DataRow dr;
        dt.Columns.Add(new DataColumn("Handling_Team", typeof(string)));
        dt.Columns.Add(new DataColumn("Class", typeof(string)));
        dt.Columns.Add(new DataColumn("Positive_Value", typeof(double)));
        dt.Columns.Add(new DataColumn("CustomerNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("CustomerName", typeof(string)));


        SqlConnection sqlConnection = new SqlConnection(strSQLconnection);
        SqlDataReader reader = null;
        sqlConnection.Open();

        //1.	KAM
        SqlCommand sqlCommand = new SqlCommand("SELECT CustomerTypeName AS Handling_Team , Class AS Class, SUM(PositiveValue) AS Positive_Value , CustomerName , CustomerNumber FROM [ECRMIslam].[dbo].[KAM] " + AllFilters + " GROUP BY CustomerTypeName , CustomerName , CustomerNumber , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "KAM";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["CustomerNumber"] = Convert.ToString(reader["CustomerNumber"].ToString());
            dr["CustomerName"] = Convert.ToString(reader["CustomerName"].ToString());
            dt.Rows.Add(dr);
        }
        reader.Close();


        
        //2.	GDS
        sqlCommand = new SqlCommand("SELECT MarketSegmentName AS Handling_Team , Class AS Class, SUM(PositiveValue) AS Positive_Value ,  CustomerName , CustomerNumber FROM [ECRMIslam].[dbo].[GDS] " + AllFilters + " GROUP BY MarketSegmentName , CustomerName , CustomerNumber, Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "GDS";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["CustomerNumber"] = Convert.ToString(reader["CustomerNumber"].ToString());
            dr["CustomerName"] = Convert.ToString(reader["CustomerName"].ToString());
            dt.Rows.Add(dr);
        }
        reader.Close();

        
        //3.	Local Partner
        sqlCommand = new SqlCommand("SELECT  Class AS Class, SUM(PositiveValue) AS Positive_Value ,  CustomerName , CustomerNumber FROM [ECRMIslam].[dbo].[LCP] " + AllFilters + " GROUP BY   CustomerName , CustomerNumber , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "Local Partner";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["CustomerNumber"] = Convert.ToString(reader["CustomerNumber"].ToString());
            dr["CustomerName"] = Convert.ToString(reader["CustomerName"].ToString());
            dt.Rows.Add(dr);
        }
        reader.Close();

        
        //4.	Terminated

        sqlCommand = new SqlCommand("SELECT  Class AS Class, SUM(PositiveValue) AS Positive_Value ,  CustomerName , CustomerNumber FROM [ECRMIslam].[dbo].[Terminated_View] " + AllFilters + " GROUP BY   CustomerName , CustomerNumber , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);

        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "Terminated";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["CustomerNumber"] = Convert.ToString(reader["CustomerNumber"].ToString());
            dr["CustomerName"] = Convert.ToString(reader["CustomerName"].ToString());
            dt.Rows.Add(dr);
        }

        reader.Close();

        /*
        //4.	Direct-Case

        sqlCommand = new SqlCommand(DC_View, sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Handling_Team"] = "Direct-Case";
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
            dr["CustomerNumber"] = Convert.ToInt32(reader["CustomerNumber"].ToString());
            dr["CustomerName"] = Convert.ToString(reader["CustomerName"].ToString());
            dt.Rows.Add(dr);
        }

        */

        ds.Tables.Add(dt);

        DataView dv = dt.DefaultView;
        dv.Sort = "Positive_Value desc";
        DataTable sortedDT = dv.ToTable();



        GridView2.DataSource = sortedDT;
        //GridView1.DataSource = reader;
        GridView2.DataBind();
        //sqlConnection.Close();
        //reader.Close();
        ////////////Team Summary End///////////////////////////

    }


    void Partner()
    {
        

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("All");
        DataRow dr;
        dt.Columns.Add(new DataColumn("Class", typeof(string)));
        dt.Columns.Add(new DataColumn("PartnerName", typeof(string)));
        dt.Columns.Add(new DataColumn("Positive_Value", typeof(double)));


        SqlConnection sqlConnection = new SqlConnection(strSQLconnection);
        SqlDataReader reader = null;
        sqlConnection.Open();

        //1.	KAM
        SqlCommand sqlCommand = new SqlCommand("SELECT PartnerName , Class AS Class, SUM(PositiveValue) AS Positive_Value  FROM [ECRMIslam].[dbo].[Invoices] " + AllFilters + " GROUP BY PartnerName  , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["PartnerName"] = Convert.ToString(reader["PartnerName"].ToString()); ;
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());
           
            dt.Rows.Add(dr);
        }
        reader.Close();



        ds.Tables.Add(dt);

        DataView dv = dt.DefaultView;
        dv.Sort = "Positive_Value desc";
        DataTable sortedDT = dv.ToTable();



        GridView3.DataSource = sortedDT;
        //GridView1.DataSource = reader;
        GridView3.DataBind();
        //sqlConnection.Close();
        //reader.Close();

    }



    void ParentName()
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("All");
        DataRow dr;
        dt.Columns.Add(new DataColumn("Class", typeof(string)));
        dt.Columns.Add(new DataColumn("ParentName", typeof(string)));
        dt.Columns.Add(new DataColumn("Positive_Value", typeof(double)));


        SqlConnection sqlConnection = new SqlConnection(strSQLconnection);
        SqlDataReader reader = null;
        sqlConnection.Open();

        SqlCommand sqlCommand = new SqlCommand("SELECT ParentName , Class AS Class, SUM(PositiveValue) AS Positive_Value  FROM [ECRMIslam].[dbo].[Invoices] " + AllFilters +" GROUP BY ParentName  , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["ParentName"] = Convert.ToString(reader["ParentName"].ToString()); ;
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());

            dt.Rows.Add(dr);
        }
        reader.Close();

        ds.Tables.Add(dt);

        DataView dv = dt.DefaultView;
        dv.Sort = "Positive_Value desc";
        DataTable sortedDT = dv.ToTable();

        GridView4.DataSource = sortedDT;
        //GridView1.DataSource = reader;
        GridView4.DataBind();
        //sqlConnection.Close();
        //reader.Close();
        ////////////Team Summary End///////////////////////////

    }


    void MarketSegment()
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("All");
        DataRow dr;
        dt.Columns.Add(new DataColumn("Class", typeof(string)));
        dt.Columns.Add(new DataColumn("MarketSegmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("Positive_Value", typeof(double)));


        SqlConnection sqlConnection = new SqlConnection(strSQLconnection);
        SqlDataReader reader = null;
        sqlConnection.Open();

        SqlCommand sqlCommand = new SqlCommand("SELECT MarketSegmentName , Class AS Class, SUM(PositiveValue) AS Positive_Value  FROM [ECRMIslam].[dbo].[Invoices] " + AllFilters + " GROUP BY MarketSegmentName  , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["MarketSegmentName"] = Convert.ToString(reader["MarketSegmentName"].ToString()); ;
            dr["Class"] = reader["Class"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());

            dt.Rows.Add(dr);
        }
        reader.Close();

        ds.Tables.Add(dt);

        DataView dv = dt.DefaultView;
        dv.Sort = "Positive_Value desc";
        DataTable sortedDT = dv.ToTable();

        GridView6.DataSource = sortedDT;
        //GridView1.DataSource = reader;
        GridView6.DataBind();
        //sqlConnection.Close();
        //reader.Close();
        ////////////Team Summary End///////////////////////////

    }



    void YearC()
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("All");
        DataRow dr;
        dt.Columns.Add(new DataColumn("Year", typeof(string)));
        dt.Columns.Add(new DataColumn("#_of_Invoices", typeof(string)));
        dt.Columns.Add(new DataColumn("Positive_Value", typeof(double)));


        SqlConnection sqlConnection = new SqlConnection(strSQLconnection);
        SqlDataReader reader = null;
        sqlConnection.Open();

        SqlCommand sqlCommand = new SqlCommand("SELECT YearC , COUNT(Number) As #_of_Invoices  , SUM(PositiveValue) AS Positive_Value  FROM [ECRMIslam].[dbo].[Invoices] " + AllFilters + " GROUP BY YearC  , Class ORDER BY SUM(PositiveValue) DESC", sqlConnection);
        reader = sqlCommand.ExecuteReader();
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Year"] = Convert.ToString(reader["YearC"].ToString()); ;
            dr["#_of_Invoices"] = reader["#_of_Invoices"].ToString();
            dr["Positive_Value"] = Convert.ToDouble(reader["Positive_Value"].ToString());

            dt.Rows.Add(dr);
        }
        reader.Close();

        ds.Tables.Add(dt);

        DataView dv = dt.DefaultView;
        dv.Sort = "Positive_Value desc";
        DataTable sortedDT = dv.ToTable();

        GridView5.DataSource = sortedDT;
        //GridView1.DataSource = reader;
        GridView5.DataBind();
        //sqlConnection.Close();
        //reader.Close();
        ////////////Team Summary End///////////////////////////

    }

    protected void Page_Load(object sender, EventArgs e)
    {
            if (!IsPostBack)
            {



                /* ,[Number]
              ,[Class]
              ,[Currency]
              ,[Original]
              ,[FunctionalAmount]
              ,[InvoicesRemainingDues]
              ,[FunctionalRemainingDue]
              ,[PositiveValue]
              ,[MonthC]
              ,[YearC]
              ,[InvoiceDate]
              ,[CustomerNumber]
              ,[CustomerName]
              ,[CustomerTypeName]
              ,[CustomerCategoryName]
              ,[MarketSegmentName]
              ,[BusLineName]
              ,[PartnerName]
              ,[ParentName]
              ,[CustAdminStatusName]
              ,[WarrantyDays]
              ,[ACRT]*/

                FillListF("CustomerName", ListBox1);
                FillListF("PartnerName", ListBox3);
                FillListF("CustAdminStatusName", ListBox4);
                FillListF("MonthC", ListBox5);
                FillListF("YearC", ListBox6);
                FillListF("MarketSegmentName", ListBox7);
                FillListF("ParentName", ListBox8);

                TeamSummary();
                CustomerSummary();
                Partner();
                ParentName();
                YearC();
                MarketSegment();

         }

    }
   protected void lstItemsWithChosen_TextChanged(object sender, EventArgs e)
    {
        //Response.Write("cccccccccc");
    }
    protected void GridView1_Unload(object sender, EventArgs e)
    {

      

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }


    string GetSlectedItemsInList(ListBox List , string ColumnName)
    {
        int[] ClassSelected =List.GetSelectedIndices();
        string[] SelectedItems = new string[20000];
        string ClassFilter = "" + ColumnName + " IN ('";
        bool ItemSelected = false;

        for (int Index = 0; Index < ClassSelected.Length; Index++ )
        {
            SelectedItems[Index] = List.Items[ClassSelected[Index]].Text;

            if (ClassSelected.Length == 1)
            {
                ItemSelected = true;
                ClassFilter = ClassFilter + SelectedItems[Index].ToString() + "'";
                break;
            }
            else if (ClassSelected.Length > 1 && Index < ClassSelected.Length - 1)
            {
                ItemSelected = true;
                ClassFilter = ClassFilter + SelectedItems[Index].ToString() + "','";
            }
            else if (ClassSelected.Length > 1 && Index == ClassSelected.Length - 1)
            {
                ItemSelected = true;
                ClassFilter = ClassFilter + SelectedItems[Index].ToString() + "'";
            }

        }

        ClassFilter = ClassFilter + ")";

        if (ItemSelected == false)
        {
            ClassFilter = "";
        }
        else
        {
            ClassFilter = " " + ClassFilter;
        }

        return ClassFilter;

    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        /* ,[Number]
              ,[Class]
              ,[Currency]
              ,[Original]
              ,[FunctionalAmount]
              ,[InvoicesRemainingDues]
              ,[FunctionalRemainingDue]
              ,[PositiveValue]
              ,[MonthC]
              ,[YearC]
              ,[InvoiceDate]
              ,[CustomerNumber]
              ,[CustomerName]
              ,[CustomerTypeName]
              ,[CustomerCategoryName]
              ,[MarketSegmentName]
              ,[BusLineName]
              ,[PartnerName]
              ,[ParentName]
              ,[CustAdminStatusName]
              ,[WarrantyDays]
              ,[ACRT]*/

        Timer1.Enabled = false;


        ScriptManager.RegisterStartupScript(lstItemsWithChosen, this.GetType(), "showalert", "grids();", true);
        ScriptManager.RegisterStartupScript(lstItemsWithChosen, this.GetType(), "showalert", "grids2();", true);


        string ClassFilter = GetSlectedItemsInList(lstItemsWithChosen,"Class");
        string CustomerFilter = GetSlectedItemsInList(ListBox1,"CustomerName");
        string PartnerFilter = GetSlectedItemsInList(ListBox3, "PartnerName");
        string CustAdminStatusFilter = GetSlectedItemsInList(ListBox4, "CustAdminStatusName");
        string MonthCFilter = GetSlectedItemsInList(ListBox5, "MonthC");
        string YearCFilter = GetSlectedItemsInList(ListBox6, "YearC");
        string MarketSegmentFilter = GetSlectedItemsInList(ListBox7, "MarketSegmentName");
        string ParentFilter = GetSlectedItemsInList(ListBox8, "ParentName");

        AllFilters = Filters(ClassFilter, CustomerFilter, "", CustAdminStatusFilter, YearCFilter, MonthCFilter, PartnerFilter, ParentFilter, MarketSegmentFilter);

        TeamSummary();
        CustomerSummary();
        Partner();
        ParentName();
        YearC();
        MarketSegment();
       

        

    }
}