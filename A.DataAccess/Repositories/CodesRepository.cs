using A.DomainModel;
using A.DomainModel.Entitis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace A.DataAccess.Repositories
{
    public class CodesRepository
    {
        static SqlConnection con = new SqlConnection(utils.ConnectionString);
        static SqlCommand cmd;
        static SqlDataReader SQlDatard;

        public static List<Codes> Gets(int codeasli)
        {
            var data = new List<Codes>();
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                var FillAllDrpListReg = "RF_FillAllDrpListReg_withParameter";

                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = FillAllDrpListReg;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StateReg", codeasli);

                con.Open();
                SQlDatard = cmd.ExecuteReader();
                if (SQlDatard.HasRows)
                {
                    while (SQlDatard.Read())
                    {
                        data.Add(new Codes()
                        {
                            Code_Asli = Convert.ToInt32(SQlDatard["Code_Asli"]),
                            Code_Farei = Convert.ToInt32(SQlDatard["Code_Farei"]),
                            Sharh = SQlDatard["Sharh"].ToString(),
                            Show = Convert.ToInt32(SQlDatard["Show"]),
                            UseFull_Col = SQlDatard["UseFull_Col"].ToString(),
                            As_Col = SQlDatard["As_Col"].ToString(),

                            Fmajmoeh_Codem = SQlDatard["Fmajmoeh_Codem"].ToString(),
                            Bomi_ReshtehEmtehanee = Convert.ToInt32(SQlDatard["Bomi_ReshtehEmtehanee"]),
                        }
                        );
                    }
                    cmd.Parameters.Clear();
                }
                SQlDatard.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            return data;
        }

        public static List<Codes> Gets()//برای  لود کل جدول نیازی به متغییر نیست 
        {
            var data = new List<Codes>();
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                var FillAllDrpListReg = "RF_FillAllDrpListReg_Allca";

                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = FillAllDrpListReg;
                cmd.Parameters.Clear();

                con.Open();
                SQlDatard = cmd.ExecuteReader();
                if (SQlDatard.HasRows)
                {
                    while (SQlDatard.Read())
                    {
                        data.Add(new Codes()
                        {
                            Code_Asli = Convert.ToInt32(SQlDatard["Code_Asli"]),
                            Code_Farei = Convert.ToInt32(SQlDatard["Code_Farei"]),
                            Sharh = SQlDatard["Sharh"].ToString(),
                            Show = Convert.ToInt32(SQlDatard["Show"]),
                            UseFull_Col = SQlDatard["UseFull_Col"].ToString(),
                            As_Col = SQlDatard["As_Col"].ToString(),

                            Fmajmoeh_Codem = SQlDatard["Fmajmoeh_Codem"].ToString(),
                            Bomi_ReshtehEmtehanee = Convert.ToInt32(SQlDatard["Bomi_ReshtehEmtehanee"]),
                        }
                        );
                    }
                    cmd.Parameters.Clear();
                }
                SQlDatard.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            return data;
        }

        public static List<Codes> Gets_Cache(int codeasli)
        {
            var cacheKey = "BaseData_codeasli_" + codeasli;
            var cacheData = Cache.CacheApi.SCache.GetDataCache(cacheKey);
            if (cacheData != null)
            {
                return (List<Codes>)cacheData;
            }
            var list = Gets(codeasli);
            Cache.CacheApi.SCache.SetDataCache(cacheKey, list);
            return list;
        }

        public static List<Codes> Gets_Cache()//برای  لود کل جدول نیازی به متغییر نیست 
        {
            var cacheKey = "Codes";
            var cacheData = Cache.CacheApi.SCache.GetDataCache(cacheKey);
            if (cacheData != null)
            {
                return (List<Codes>)cacheData;
            }
            var list = Gets();
            Cache.CacheApi.SCache.SetDataCache(cacheKey, list);
            return list;
        }


        public static void Add(Codes item)
        {
            Cache.CacheApi.SCache.ClearPreKey("Codes");
            //
            //فرآیند ثبت اطلاعات
        }

        public static void Update(Codes item)
        {
            Cache.CacheApi.SCache.ClearPreKey("Codes");
            //
            //فرآیند .یرایش اطلاعات
        }

        public static void Delete(int key)
        {
            Cache.CacheApi.SCache.ClearPreKey("Codes");
            //
            //فرآیند حذف اطلاعات
        }
    }
}
