using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BUS
{
    public class DATPHONG
    {
        Entities db;
        public DATPHONG()
        {
            db = Entities.CreateEntities();
        }
        public tb_DatPhong getItem(int iddp)
        {
            return db.tb_DatPhong.FirstOrDefault(x => x.IDDP == iddp);
        }
        public List<tb_DatPhong> getAll()
        {
            return db.tb_DatPhong.ToList();
        }
        public void add(tb_DatPhong dp)
        {
            try
            {
                db.tb_DatPhong.Add(dp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }

        }
        public void update(tb_DatPhong dp)
        {
            tb_DatPhong _dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == dp.IDDP);
            _dp.IDDP = dp.IDDP;
            _dp.MACTY = dp.MACTY;
            _dp.MADVI = dp.MADVI;
            _dp.NGAYDATPHONG = dp.NGAYDATPHONG;
            _dp.NGAYTRAPHONG = dp.NGAYTRAPHONG;
            _dp.SONGUOIO = dp.SONGUOIO;
            _dp.SOTIEN = dp.SOTIEN;
            _dp.IDUSER = dp.IDUSER;
            _dp.DISABLED = dp.DISABLED;
            _dp.THEODOAN = dp.THEODOAN;
            _dp.GHICHU = dp.GHICHU;
            _dp.CREATE_DATE = dp.CREATE_DATE;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
        public void delete(int iddp)
        {
            tb_DatPhong dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == iddp);
            dp.DISABLED = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
    }
}
