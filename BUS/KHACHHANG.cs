using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BUS
{
    public class KHACHHANG
    {
        Entities db;
        public KHACHHANG()
        {
            db = Entities.CreateEntities();
        }
        public tb_KhachHang getItem(int idkh)
        {
            return db.tb_KhachHang.FirstOrDefault(x => x.IDKH == idkh);
        }
        public List<tb_KhachHang> getAll()
        {
            return db.tb_KhachHang.ToList();
        }
        public void add(tb_KhachHang kh)
        {
            try
            {
                db.tb_KhachHang.Add(kh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }

        }
        public void update(tb_KhachHang kh)
        {
            tb_KhachHang _kh = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == kh.IDKH);
            _kh.HOTEN = kh.HOTEN;
            _kh.DIENTHOAI = kh.DIENTHOAI;
            _kh.CCCD = kh.CCCD;
            _kh.EMAIL = kh.EMAIL;
            _kh.DIACHI = kh.DIACHI;
            _kh.DISABLED = kh.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
        public void delete(int idkh)
        {
            tb_KhachHang kh = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == idkh);
            kh.DISABLED = true;
            try
            {
                db.tb_KhachHang.Remove(kh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
    }
}
