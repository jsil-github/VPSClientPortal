using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Personal_Info : System.Web.UI.Page
{
    DataTable personalInfo = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        personalInfo = getpersonal_info();
        txt_name.Value = personalInfo.Rows[0][3].ToString();
        //txt_mobile.Value = personalInfo.Rows[0][0].ToString();
        txt_folio.Value = personalInfo.Rows[0][0].ToString();
        txt_bankname.Value = personalInfo.Rows[0][4].ToString();
        txt_accountno.Value = personalInfo.Rows[0][5].ToString();
        txt_bankaddress.Value = personalInfo.Rows[0][7].ToString();
       

    }

    public DataTable getpersonal_info()
    {
        DataTable tbl = new DataTable();
        DAL.clsOracleConnection cn = new DAL.clsOracleConnection();
        try
        {
            //making user
            string qry = @"select distinct u.folio_number,
                               us.user_id,
                               us.password,
                               u.title,
                               u.bank_name,
                               u.account_number,
                               u.bank_acc_title,
                               u.bank_address
                          from unit_account u
                         inner join unit_acc_client uac
                            on uac.folio_number = u.folio_number
                         inner join users us
                            on us.client_code = uac.client_code
                            inner join vps_account_scheme vv on vv.folio_number = u.folio_number
                             where us.user_id = 'SAMINAFAISAL'";

            tbl = DAL.OracleDataAccess.ExecuteDataset(cn.GetConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]), CommandType.Text, qry).Tables[0];
            if (tbl.Rows.Count > 0)
            {
                return tbl;
            }
            return tbl;
        }

        finally
        {
            cn.CloseConnection();
        }
    }

    protected void Edit_Click(object sender, EventArgs e)
    {
        txt_bankname.Attributes.Remove("readonly");
        txt_bankname.Attributes.Add("style", "border: 1px solid blue;");
        txt_bankaddress.Attributes.Remove("readonly");
        txt_bankaddress.Attributes.Add("style", "border: 1px solid blue;");
        txt_accountno.Attributes.Remove("readonly");
        txt_accountno.Attributes.Add("style", "border: 1px solid blue;");
    }
}