using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DALSqlServer
/// </summary>
public class DALSqlServer
{
    public DALSqlServer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static SqlConnection cn;
    public static SqlDataAdapter da;
    public static DataSet ds = new DataSet();
    public static string strConnection = "Data Source=.\\sqlexpress;Initial Catalog=vps;User ID=sa;Password=jsil@123++";

    private static void cnClose()
    {
        try
        {
            cn.Close();


        }
        catch (Exception ex)
        {
            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
            return;
        }
    }
    public static void DBConnect()
    {
        try
        {

            cn = new SqlConnection(strConnection);
            if (cn.State == ConnectionState.Open)
            {
                cnClose();
            }
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
        }

        catch (Exception ex)
        {
            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
        }
        finally
        {

        }
    }
    public static DataTable ExecuteTable(string SSql)
    {
        SqlCommand cmd;
        DataTable tbl = new DataTable();
        try
        {
            DBConnect();
            cmd = new SqlCommand(SSql, cn);
            //cmd.Parameters.AddRange(p);
            tbl.Load(cmd.ExecuteReader());
            cn.Close();

        }
        catch (Exception ex)
        {
            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
            cn.Close();

        }
        return tbl;
    }
    public static DataTable ExecuteTable(string SSql, SqlParameter[] p)
    {
        SqlCommand cmd;
        DataTable tbl = new DataTable();
        try
        {
            DBConnect();
            cmd = new SqlCommand(SSql, cn);
            cmd.Parameters.AddRange(p);
            tbl.Load(cmd.ExecuteReader());
            cn.Close();

        }
        catch (Exception ex)
        {
            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
            cn.Close();

        }
        return tbl;
    }
    public static Boolean Execute(string SSql)
    {
        SqlCommand cmd;
        try
        {
            DBConnect();
            cmd = new SqlCommand(SSql, cn);

            cmd.ExecuteNonQuery();
            cn.Close();
            return true;
        }
        catch (Exception ex)
        {
            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
            cn.Close();
            return false;
        }
    }
    public static Boolean Execute(string SSql, SqlParameter[] p)
    {
        SqlCommand cmd;
        try
        {
            DBConnect();
            cmd = new SqlCommand(SSql, cn);
            cmd.Parameters.AddRange(p);
            cmd.ExecuteNonQuery();
            cn.Close();
            return true;
        }
        catch (Exception ex)
        {
            JSFunctions.ErrorLog.LogError("DFT SMS - dftl_incoming_sms.aspx --> incoming sms api ", ex.Message);
            cn.Close();
            return false;
        }
    }

    public static object ExecuteScaler(string SSql, SqlParameter[] p)
    {
        object val = null;
        SqlCommand cmd;
        try
        {
            DBConnect();
            cmd = new SqlCommand(SSql, cn);
            cmd.Parameters.AddRange(p);
            val = cmd.ExecuteScalar();
            cn.Close();

        }
        catch (Exception ex)
        {
            JSFunctions.ErrorLog.LogError("VPS Execute ", ex.Message);
            cn.Close();

        }

        return val;
    }
}