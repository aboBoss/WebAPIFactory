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
    public class StocksController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"
                    select Id, Diameter, Sum from dbo.Stock";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Stock st)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                    insert into dbo.Stock 
                    (Diameter,Sum)
                    values 
                    (
                    '" + st.Diameter + @"'
                    ,'" + st.Sum + @"'
                    )
                    ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch (Exception)
            {

                return "Failed to Add";
            }
        }


        public string Put(Stock st)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                    update dbo.Stock set 
                    Diameter = '" + st.Diameter + @"'
                    ,Sum = '" + st.Sum + @"'
                    where Id = " + st.Id + @" 
                    ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully";
            }
            catch (Exception)
            {

                return "Failed to Update";
            }
        }


        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                    delete from dbo.Stock
                    where Id = " + id + @" 
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