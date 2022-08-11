using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class DBServerices
    {
        private static DBServerices DbInstance = null;
        private SqlConnection connection;

        private DBServerices(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }
        public DBServerices()
        {

        }
        public static DBServerices GetDbServices()
        {
            if (DbInstance is null)
            {
                DbInstance = new DBServerices(@"Data Source=SANADCOMP\SQLEXPRESS;Initial Catalog=FinalFactory;Integrated Security=True");
            }

            return DbInstance;
        }

        //create sql command
        public SqlCommand CreateCommand(string CommandSTR)
        {
            SqlCommand cmd = new SqlCommand(CommandSTR, connection);
            cmd.CommandTimeout = 10;
            cmd.CommandType = System.Data.CommandType.Text;

            return cmd;
        }

        //exec sql command
        public int ExecuteAndClose(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                cmd.Connection.Close();
            }
        }

        //get rows from DB
        public DataTable Select(SqlCommand cmd)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Table");
                DataTable dt = ds.Tables["Table"];

                return dt;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                cmd.Connection.Close();
            }
        }

        //create a list of objects from a DataTable
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        //check for each column in each row 
        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}