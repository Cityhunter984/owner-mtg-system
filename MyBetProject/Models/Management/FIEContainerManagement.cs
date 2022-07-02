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
    public class FIEContainerManagement
    {
        public static void addFIEContainerInfo(FIEContainerForm frm)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_AddFIEContInfo", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ArrivePlace", frm.ArrivePlace);
                cmd.Parameters.AddWithValue("@ArriveDate", frm.ArriveDate);
                cmd.Parameters.AddWithValue("@VNTruck", frm.VNTruck);
                cmd.Parameters.AddWithValue("@ContNo", frm.ContNo);
                cmd.Parameters.AddWithValue("@ContSize", frm.ContSize);
                cmd.Parameters.AddWithValue("@PKL", frm.PKLStr == "0" ? false : true);
                cmd.Parameters.AddWithValue("@ContOwner", frm.ContOwner);
                cmd.Parameters.AddWithValue("@GoodsType", frm.GoodsType);
                cmd.Parameters.AddWithValue("@DeliveryDate", frm.DeliveryDate.Year == 1 ? (Object)DBNull.Value : frm.DeliveryDate);
                cmd.Parameters.AddWithValue("@DeliveryPlace", frm.DeliveryPlace == null || frm.DeliveryPlace == "" ? (Object)DBNull.Value : frm.DeliveryPlace);
                cmd.Parameters.AddWithValue("@DeliveryStatus", frm.DeliveryStatus);
                cmd.Parameters.AddWithValue("@ThridParty", frm.ThridParty);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);

                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = cmd;
                da.InsertCommand.ExecuteNonQuery();
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

        public static List<FIEContainerForm> getFIEContainerInfoByCreateDate(DateTime createDate)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetFIEContainerInfoByCreateDate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CreateDate", createDate);
                List<FIEContainerForm> lst = new List<FIEContainerForm>();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    FIEContainerForm f = new FIEContainerForm();
                    f.ID = Convert.ToInt32(dr["ID"]);
                    f.ArrivePlace = dr["ArrivePlace"].ToString();
                    f.ArriveDate = Convert.ToDateTime(dr["ArriveDate"]);
                    f.ArriveDateStr = f.ArriveDate.ToShortDateString();
                    f.VNTruck = dr["VNTruck"].ToString();
                    f.ContNo = dr["ContNo"].ToString();
                    f.ContSize = Convert.ToInt32(dr["ContSize"]);
                    f.PKL = (bool)dr["PKL"];
                    f.PKLStr = f.PKL == true ? "Yes" : "No";
                    f.ContOwner = dr["ContOwner"].ToString();
                    f.GoodsType = dr["GoodsType"].ToString();
                    f.DeliveryDateStr = (dr["DeliveryDate"] is DBNull) ? null : Convert.ToDateTime(dr["DeliveryDate"]).ToShortDateString();
                    f.DeliveryPlace = dr["DeliveryPlace"] == null ? null : dr["DeliveryPlace"].ToString();
                    f.DeliveryStatus = Convert.ToInt32(dr["DeliveryStatus"]);
                    f.DeliveryStatusName = dr["Name"].ToString();
                    f.color = dr["Color"].ToString();
                    f.ThridParty = dr["ThridParty"].ToString();


                    lst.Add(f);
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

        public static List<FIEContainerForm> getSearchDataByDate(FIEContainerSearchForm frm)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                DateTime thisWeekStart = frm.searchDate.AddDays(-(int)frm.searchDate.DayOfWeek);
                DateTime thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

                SqlCommand cmd = new SqlCommand("sp_GetFIEContainerDataByDate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DateType", frm.dateType);
                cmd.Parameters.AddWithValue("@SearchDate", frm.searchDate);
                cmd.Parameters.AddWithValue("@FirstDayOfWeek", thisWeekStart);
                cmd.Parameters.AddWithValue("@LastDayOfWeek", thisWeekEnd);
                cmd.Parameters.AddWithValue("@SearchType", frm.searchType);
                cmd.Parameters.AddWithValue("@SearchData", frm.searchData == null ? "" : frm.searchData);

                List<FIEContainerForm> lst = new List<FIEContainerForm>();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    FIEContainerForm f = new FIEContainerForm();
                    f.ID = Convert.ToInt32(dr["ID"]);
                    f.ArrivePlace = dr["ArrivePlace"].ToString();
                    f.ArriveDate = Convert.ToDateTime(dr["ArriveDate"]);
                    f.ArriveDateStr = f.ArriveDate.ToShortDateString();
                    f.VNTruck = dr["VNTruck"].ToString();
                    f.ContNo = dr["ContNo"].ToString();
                    f.ContSize = Convert.ToInt32(dr["ContSize"]);
                    f.PKL = (bool)dr["PKL"];
                    f.PKLStr = f.PKL == true ? "Yes" : "No";
                    f.ContOwner = dr["ContOwner"].ToString();
                    f.GoodsType = dr["GoodsType"].ToString();
                    f.DeliveryDateStr = (dr["DeliveryDate"] is DBNull) ? null : Convert.ToDateTime(dr["DeliveryDate"]).ToShortDateString();
                    f.DeliveryPlace = dr["DeliveryPlace"] == null ? null : dr["DeliveryPlace"].ToString();
                    f.DeliveryStatus = Convert.ToInt32(dr["DeliveryStatus"]);
                    f.color = dr["Color"].ToString();
                    f.DeliveryStatusName = dr["Name"].ToString();
                    f.ThridParty = dr["ThridParty"].ToString();


                    lst.Add(f);
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

        public static FIEContainerForm getContInfoByID(int id)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand("sp_GetContInfoByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                FIEContainerForm f = new FIEContainerForm();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    f.ID = Convert.ToInt32(dr["ID"]);
                    f.ArrivePlace = dr["ArrivePlace"].ToString();
                    f.ArriveDate = Convert.ToDateTime(dr["ArriveDate"]);
                    f.ArriveDateStr = f.ArriveDate.ToString("yyyy-MM-dd");
                    f.VNTruck = dr["VNTruck"].ToString();
                    f.ContNo = dr["ContNo"].ToString();
                    f.ContSize = Convert.ToInt32(dr["ContSize"]);
                    f.PKL = (bool)dr["PKL"];
                    f.PKLStr = f.PKL == true ? "1" : "0";
                    f.ContOwner = dr["ContOwner"].ToString();
                    f.GoodsType = dr["GoodsType"].ToString();
                    f.DeliveryDateStr = (dr["DeliveryDate"] is DBNull) ? "" : Convert.ToDateTime(dr["DeliveryDate"]).ToString("yyyy-MM-dd");
                    f.DeliveryPlace = dr["DeliveryPlace"] == null ? null : dr["DeliveryPlace"].ToString();
                    f.DeliveryStatus = Convert.ToInt32(dr["DeliveryStatus"]);
                    f.color = dr["Color"].ToString();
                    f.DeliveryStatusName = dr["Name"].ToString();
                    f.ThridParty = dr["ThridParty"].ToString();
                }
                dr.Close();

                return f;
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

        public static void getUpdateContInfo(FIEContainerForm frm)
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateContInfo", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", frm.ID);
                cmd.Parameters.AddWithValue("@ArrivePlace", frm.ArrivePlace);
                cmd.Parameters.AddWithValue("@ArriveDate", frm.ArriveDate);
                cmd.Parameters.AddWithValue("@VNTruck", frm.VNTruck);
                cmd.Parameters.AddWithValue("@ContNo", frm.ContNo);
                cmd.Parameters.AddWithValue("@ContSize", frm.ContSize);
                cmd.Parameters.AddWithValue("@PKL", frm.PKLStr == "0" ? false : true);
                cmd.Parameters.AddWithValue("@ContOwner", frm.ContOwner);
                cmd.Parameters.AddWithValue("@GoodsType", frm.GoodsType);
                cmd.Parameters.AddWithValue("@DeliveryDate", frm.DeliveryDate.Year == 1 ? (Object)DBNull.Value : frm.DeliveryDate);
                cmd.Parameters.AddWithValue("@DeliveryPlace", frm.DeliveryPlace == null || frm.DeliveryPlace == "" ? (Object)DBNull.Value : frm.DeliveryPlace);
                cmd.Parameters.AddWithValue("@DeliveryStatus", frm.DeliveryStatus);
                cmd.Parameters.AddWithValue("@ThridParty", frm.ThridParty);

                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = cmd;
                da.InsertCommand.ExecuteNonQuery();
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

        public static List<DeliveryStatusForm> getAllDeliveryStatus()
        {
            SqlConnection cn = ConnectionString.getConnectionString();

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllDeliveryStatus", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                List<DeliveryStatusForm> lst = new List<DeliveryStatusForm>();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DeliveryStatusForm d = new DeliveryStatusForm();
                    d.ID = Convert.ToInt32(dr["ID"]);
                    d.Name = dr["Name"].ToString();
                    d.Color = dr["Color"].ToString();

                    lst.Add(d);
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