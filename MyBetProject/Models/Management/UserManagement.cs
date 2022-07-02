using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using MyBetProject.Models.Form;
using MODEL;


namespace MyBetProject.Models.Management
{
    public class UserManagement
    {
        public static List<UserForm> getLogin(UserForm frm)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetLogin", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserName", frm.UserName);
                cmd.Parameters.AddWithValue("@Pass", ContentHelper.MD5Hash(frm.Pass));

                SqlDataReader dr = cmd.ExecuteReader();
                List<UserForm> lst = new List<UserForm>();

                while (dr.Read())
                {
                    UserForm u = new UserForm();
                    u.UserID = dr["UserID"].ToString();
                    u.UserName = dr["UserName"].ToString();
                    u.Pass = dr["Pass"].ToString();
                    u.PositionID = Convert.ToInt32(dr["Position"]);
                    u.ID = Convert.ToInt32(dr["ID"]);
                    u.Position = dr["Pos"].ToString();

                    lst.Add(u);
                }
                dr.Close();

                return lst;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static List<UserPositionForm> getAllPosition()
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllPosition", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                List<UserPositionForm> lst = new List<UserPositionForm>();
                while (dr.Read())
                {
                    UserPositionForm u = new UserPositionForm();
                    u.ID = Convert.ToInt32(dr["ID"]);
                    u.Pos = dr["Pos"].ToString();

                    lst.Add(u);
                }
                dr.Close();

                return lst;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static List<UserForm> getAllUser()
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                List<UserForm> lst = new List<UserForm>();

                while (dr.Read())
                {
                    UserForm u = new UserForm();
                    u.UserID = dr["UserID"].ToString();
                    u.UserName = dr["UserName"].ToString();
                    u.PositionID = Convert.ToInt32(dr["Position"]);
                    u.ID = Convert.ToInt32(dr["ID"]);
                    u.Position = dr["Pos"].ToString();

                    lst.Add(u);
                }
                dr.Close();

                return lst;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static UserForm getUserByID(int id)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetUserByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataReader dr = cmd.ExecuteReader();
                UserForm u = new UserForm();
                while (dr.Read())
                {
                    u.UserID = dr["UserID"].ToString();
                    u.UserName = dr["UserName"].ToString();
                    u.PositionID = Convert.ToInt32(dr["Position"]);
                    u.ID = Convert.ToInt32(dr["ID"]);
                }
                dr.Close();

                return u;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static bool getDeleteUserByID(int id)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteUserByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = cmd;
                da.InsertCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public static bool getAddNewUser(UserForm frm)
        {
            SqlConnection cn = ConnectionString.getConnectionString();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_AddNewUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", frm.UserID);
                cmd.Parameters.AddWithValue("@UserName", frm.UserName);
                cmd.Parameters.AddWithValue("@Pass", ContentHelper.MD5Hash(frm.Pass));
                cmd.Parameters.AddWithValue("@Position", frm.PositionID);

                SqlDataReader dr = cmd.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    count = Convert.ToInt32(dr["UserCount"]);
                }
                dr.Close();

                return (count == 0 ? true : false);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}