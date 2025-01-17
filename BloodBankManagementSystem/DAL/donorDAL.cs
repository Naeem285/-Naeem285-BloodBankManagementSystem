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
    class donorDAL
    {
        // Updated Connection String
        static string myconnstrng = "Server=AKRAM\\SQLEXPRESS;Database=BloodBankDB;Trusted_Connection=True;";

        #region SELECT to display data in DataGridView from database
        public DataTable Select()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "SELECT * FROM tbl_donors";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region INSERT data to database
        public bool Insert(donorBLL d)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "INSERT INTO tbl_donors (first_name, last_name, email, contact, gender, address, blood_group, added_date, image_name, added_by) VALUES (@first_name, @last_name, @email, @contact, @gender, @address, @blood_group, @added_date, @image_name, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@added_date", d.added_date);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region UPDATE donors in Database
        public bool Update(donorBLL d)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_donors SET first_name=@first_name, last_name=@last_name, email=@email, contact=@contact, gender=@gender, address=@address, blood_group=@blood_group, image_name=@image_name, added_by=@added_by WHERE donor_id=@donor_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);
                cmd.Parameters.AddWithValue("@donor_id", d.donor_id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region DELETE donors from Database
        public bool Delete(donorBLL d)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_donors WHERE donor_id=@donor_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@donor_id", d.donor_id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Count Donors for Specific Blood Group
        public string countDonors(string blood_group)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            string donors = "0";

            try
            {
                string sql = "SELECT * FROM tbl_donors WHERE blood_group = '" + blood_group + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                donors = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return donors;
        }
        #endregion

        #region Method to Search Donors
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_donors WHERE donor_id LIKE '%" + keywords + "%' OR first_name LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%' OR email LIKE '%" + keywords + "%' OR blood_group LIKE '%" + keywords + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
    }
}
