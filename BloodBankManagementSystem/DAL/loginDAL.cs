using BloodBankManagementSystem.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.DAL
{
    class loginDAL
    {
        // Updated Connection String
        static string myconnstrng = "Server=AKRAM\\SQLEXPRESS;Database=BloodBankDB;Trusted_Connection=True;";

        public bool loginCheck(loginBLL l)
        {
            // Create a Boolean Variable and set its default value to false
            bool isSuccess = false;

            // Connecting Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // SQL Query to Check Login Based on Username and Password
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password";

                // Create SQL Command to Pass the value to SQL Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Pass the value to SQL Query Using Parameters
                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);

                // SQL Data Adapter to Get the Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // DataTable to Hold the Data from Database Temporarily
                DataTable dt = new DataTable();

                // Fill the data from adapter to dt
                adapter.Fill(dt);

                // Check whether user exists or not
                if (dt.Rows.Count > 0)
                {
                    // User Exists and Login Successful
                    isSuccess = true;
                }
                else
                {
                    // Login Failed
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                // Display Error Message if there's any Exception Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close Database Connection
                conn.Close();
            }

            return isSuccess;
        }
    }
}
