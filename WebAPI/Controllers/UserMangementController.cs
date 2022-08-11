using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/UserMangement")]
    public class UserMangementController : ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;


        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"
                          SELECT ID, NAME, Password, PhoneNo, IsActive
                                FROM dbo.EmpMangement
                          ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [HttpPost]
        [Route("Registration")]
        public string Registration(Mangement mangement)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("usp_Registration", conn);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", mangement.Name);
                cmd.Parameters.AddWithValue("@Password", mangement.Password);
                cmd.Parameters.AddWithValue("@PhoneNo", mangement.PhoneNo);
                cmd.Parameters.AddWithValue("@IsActive", mangement.IsActive);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    msg = "Data inserted";
                }
                else
                {
                    msg = "Error.";

                }

            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
         
            return msg;

        }


        [HttpPost]
        [Route("Login")]
        public string Login(Mangement mangement)
        {
            string msg = string.Empty;
            try
            {
                da = new SqlDataAdapter("usp_Login", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Name",mangement.Name);
                da.SelectCommand.Parameters.AddWithValue("@Password", mangement.Password);
                DataTable dt= new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    msg = "User is Valid";
                }
                else
                {
                    msg = "User is Invalid";
                    
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;

        }


        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                    delete from dbo.EmpMangement
                    where ID = " + id + @" 
                    ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully";
            }
            catch (Exception)
            {

                return "Failed to delete";
            }
        }

    }
}
