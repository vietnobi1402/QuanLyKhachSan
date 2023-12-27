using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class LOAIPHONG
    {
        Entities db;
        public LOAIPHONG()
        {
            db = Entities.CreateEntities();
        }
        public tb_LoaiPhong getItem(int idlp)
        {
            return db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == idlp);
        }
        public List<tb_LoaiPhong> getAll()
        {
            return db.tb_LoaiPhong.ToList();
        }
        public void add(tb_LoaiPhong lp)
        {
            try
            {
                db.tb_LoaiPhong.Add(lp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }

        }
        public void update(tb_LoaiPhong lp)
        {
            tb_LoaiPhong _lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == lp.IDLOAIPHONG);
            _lp.TENLOAIPHONG = lp.TENLOAIPHONG;
            _lp.DONGIA = lp.DONGIA;
            _lp.SONGUOI = lp.SONGUOI;
            _lp.SOGIUONG = lp.SOGIUONG;
            _lp.DISABLED = lp.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
        public void delete(int idlp)
        {
            tb_LoaiPhong lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == idlp);
            lp.DISABLED = true;
            try
            {
                db.tb_LoaiPhong.Remove(lp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
    }
}
