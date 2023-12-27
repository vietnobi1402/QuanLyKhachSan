using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BUS
{
    public class DATPHONG_CHITIET
    {
        Entities db;
        public DATPHONG_CHITIET()
        {
            db = Entities.CreateEntities();
        }
        public tb_DatPhong_CT getItem(int iddpct)
        {
            return db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == iddpct);
        }
        public List<tb_DatPhong_CT> getAll()
        {
            return db.tb_DatPhong_CT.ToList();
        }
        public void add(tb_DatPhong_CT dpct)
        {
            try
            {
                db.tb_DatPhong_CT.Add(dpct);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }

        }
        public void update(tb_DatPhong_CT dpct)
        {
            tb_DatPhong_CT _dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == dpct.IDDPCT);
            _dpct.IDDP = dpct.IDDP;
            _dpct.IDPHONG = dpct.IDPHONG;
            _dpct.NGAY = dpct.NGAY;
            _dpct.DONGIA = dpct.DONGIA;
            _dpct.SONGAYO = dpct.SONGAYO;
            _dpct.THANHTIEN = dpct.THANHTIEN;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
        public void delete(int iddpct)
        {
            tb_DatPhong_CT dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == iddpct);
            try
            {
                db.tb_DatPhong_CT.Remove(dpct);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
    }
}
