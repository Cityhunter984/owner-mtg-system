using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

using MODEL;
using MyBetProject.Models.Form;

namespace MyBetProject.Models.Management
{
    public class BetManagement
    {
        public static int addBetData(BetForm data)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_AddBetData", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@League", data.League);
                cmd.Parameters.AddWithValue("@Team", data.Team);
                cmd.Parameters.AddWithValue("@BetType", data.BetType);
                cmd.Parameters.AddWithValue("@BetDate", data.BetDate);
                cmd.Parameters.AddWithValue("@BetTime", data.BetTime);
                cmd.Parameters.AddWithValue("@BetID", data.BetID);
                cmd.Parameters.AddWithValue("@BetPlace", data.BetPlace);
                cmd.Parameters.AddWithValue("@CreateDate", data.CreateDate);

                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = cmd;
                da.InsertCommand.ExecuteNonQuery();

                return 1;
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

        public static List<BetForm> getBetDataByDate(DateTime date)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetBetDataByDate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Date", date);

                SqlDataReader dr = cmd.ExecuteReader();
                List<BetForm> lst = new List<BetForm>();
                while (dr.Read())
                {
                    BetForm b = new BetForm();
                    b.ID = Convert.ToInt32(dr["ID"]);
                    b.League = dr["League"].ToString();
                    b.Team = dr["Team"].ToString();
                    b.BetType = dr["BetType"].ToString();
                    b.BetDate = Convert.ToDateTime(dr["BetDate"]);
                    b.BetTime = Convert.ToDateTime(dr["BetTime"]);
                    b.BetID = dr["BetID"].ToString();
                    b.BetPlace = dr["BetPlace"].ToString();
                    b.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                    b.BetDateString = b.BetDate.ToString("dd/MM/yyyy HH:mm");

                    lst.Add(b);
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
    }
}