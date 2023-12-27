using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class myFunctions
    {
        public static string _srv;
        public static string _us;
        public static string _pw;
        public static string _db;
        static SqlConnection conn = new SqlConnection();
        public static void taoketnoi()
        {
            conn.ConnectionString = @"Data Source = DESKTOP - RH49N5V\VIET; Intinial Catalog = HOTELS; Integrated Security=True";
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void dongketnoi()
        {
            conn.Close();
        }
        public static DataTable laydulieu(string qr)
        {
            taoketnoi();
            DataTable datatb = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(qr, conn);
            dap.Fill(datatb);
            dongketnoi();
            return datatb;
        }
        public static DateTime getFirstDayInMont(int year,int month)
        {
            return new DateTime(year, month, 1);
        }

    }
}
