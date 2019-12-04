using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Registration_Form
{
    public partial class form : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["xyz"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountry();
                BindQualification();
                BindBloodGroup();
                BindRegistration();
            }
        }

        public void Clear()
        {
            txtfname.Text = "";
            txtlname.Text = "";
            rblgen.ClearSelection();
            ddlcountry.SelectedValue = "0";
            ddlqual.SelectedValue = "0";
            ddlbldgp.SelectedValue = "0";
            BindRegistration();
        }

        public void BindCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_getcountrydata", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            ddlcountry.DataValueField = "Cid";
            ddlcountry.DataTextField = "Cname";
            ddlcountry.DataSource = dt;
            ddlcountry.DataBind();
            ddlcountry.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindQualification()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_QualBind", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            ddlqual.DataValueField = "Qid";
            ddlqual.DataTextField = "Qname";
            ddlqual.DataSource = dt;
            ddlqual.DataBind();
            ddlqual.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindBloodGroup()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_BldGrpBind", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            ddlbldgp.DataValueField = "Bid";
            ddlbldgp.DataTextField = "Bname";
            ddlbldgp.DataSource = dt;
            ddlbldgp.DataBind();
            ddlbldgp.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindRegistration()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("RegDisplayData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (btnsave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("RegDisplay_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fisrt_Name", txtfname.Text);
                cmd.Parameters.AddWithValue("@Last_Name", txtlname.Text);
                cmd.Parameters.AddWithValue("@Gender", rblgen.SelectedValue);
                cmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
                cmd.Parameters.AddWithValue("@Qualification", ddlqual.SelectedValue);
                cmd.Parameters.AddWithValue("@Blood_Group", ddlbldgp.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
                BindRegistration();
                Clear();

            }

            else if (btnsave.Text == "Update") 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ViewState["pp"]);
                cmd.Parameters.AddWithValue("@Fisrt_Name", txtfname.Text);
                cmd.Parameters.AddWithValue("@Last_Name", txtlname.Text);
                cmd.Parameters.AddWithValue("@Gender", rblgen.SelectedValue);
                cmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
                cmd.Parameters.AddWithValue("@Qualification", ddlqual.SelectedValue);
                cmd.Parameters.AddWithValue("@Blood_Group", ddlbldgp.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
                BindRegistration();
                Clear(); 
            }
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "A")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                BindRegistration();
            }
            else if (e.CommandName == "B")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", e.CommandArgument);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                txtfname.Text = dt.Rows[0]["Fisrt_Name"].ToString();
                txtlname.Text = dt.Rows[0]["Last_Name"].ToString();
                rblgen.SelectedValue = dt.Rows[0]["Gender"].ToString();
                ddlcountry.SelectedValue = dt.Rows[0]["Country"].ToString();
                ddlqual.SelectedValue = dt.Rows[0]["Qualification"].ToString();
                ddlbldgp.SelectedValue = dt.Rows[0]["Blood_Group"].ToString();
                btnsave.Text = "Update";
                ViewState["pp"] = e.CommandArgument;
            }
        }
    }
}