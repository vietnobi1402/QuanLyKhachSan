using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BUS
{
    public class SANPHAM
    {
        Entities db;
        public SANPHAM()
        {
            db = Entities.CreateEntities();
        }
        public tb_SanPham getItem(int idsp)
        {
            return db.tb_SanPham.FirstOrDefault(x => x.IDSP == idsp);
        }
        public List<tb_SanPham> getAll()
        {
            return db.tb_SanPham.ToList();
        }
        public void add(tb_SanPham sp)
        {
            try
            {
                db.tb_SanPham.Add(sp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }

        }
        public void update(tb_SanPham sp)
        {
            tb_SanPham _sp = db.tb_SanPham.FirstOrDefault(x => x.IDSP == sp.IDSP);
            _sp.TENSP = sp.TENSP;
            _sp.DONGIA = sp.DONGIA;
            _sp.DISABLED = sp.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
        public void delete(int idsp)
        {
            tb_SanPham sp = db.tb_SanPham.FirstOrDefault(x => x.IDSP == idsp);
            sp.DISABLED = true;
            try
            {
                db.tb_SanPham.Remove(sp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
    }
}
