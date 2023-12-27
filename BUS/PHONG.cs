using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class PHONG
    {
        Entities db;
        public PHONG()
        {
            db = Entities.CreateEntities();
        }
        public tb_Phong getItem(int idphong)
        {
            return db.tb_Phong.FirstOrDefault(x => x.IDPHONG == idphong);
        }
        public List<tb_Phong> getAll()
        {
            return db.tb_Phong.ToList();
        }
        public List<tb_Phong>getByTang(string idTang)
        {
            return db.tb_Phong.Where(x => x.IDTANG.ToString() == idTang).ToList();
        }
        public List<tb_Phong> getByLoaiPhong(string idLoaiPhong)
        {
            return db.tb_Phong.Where(x => x.IDLOAIPHONG.ToString() == idLoaiPhong).ToList();
        }
        public List<ObjPhong> getList()
        {
            var lstPhong = db.tb_Phong.ToList();
            List<ObjPhong> lstP = new List<ObjPhong>();
            ObjPhong objP;
            foreach(var item in lstPhong)
            {
                objP = new ObjPhong();
                objP.IDPHONG = item.IDPHONG;
                objP.TENPHONG = item.TENPHONG;
                objP.TRANGTHAI = item.TRANGTHAI;
                objP.DISABLED = item.DISABLED;
                objP.IDTANG = item.IDTANG;
                var t = db.tb_Tang.FirstOrDefault(x => x.IDTANG == item.IDTANG);
                objP.TENTANG = t.TENTANG;
                objP.IDLOAIPHONG = item.IDLOAIPHONG;
                var lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == item.IDLOAIPHONG);
                objP.TENLOAIPHONG = lp.TENLOAIPHONG;
                objP.DONGIA = lp.DONGIA;
                lstP.Add(objP);
            }
            return lstP;
        }
        public List<ObjPhong> getTT()
        {
            var lstPhong = db.tb_Phong.ToList();
            List<ObjPhong> lstP = new List<ObjPhong>();
            ObjPhong objP;
            foreach (var item in lstPhong)
            {
                objP = new ObjPhong();
                objP.IDPHONG = item.IDPHONG;
                objP.TENPHONG = item.TENPHONG;
                objP.TRANGTHAI = item.TRANGTHAI=false;
                objP.DISABLED = item.DISABLED;
                objP.IDTANG = item.IDTANG;
                var t = db.tb_Tang.FirstOrDefault(x => x.IDTANG == item.IDTANG);
                objP.TENTANG = t.TENTANG;
                objP.IDLOAIPHONG = item.IDLOAIPHONG;
                var lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == item.IDLOAIPHONG);
                objP.TENLOAIPHONG = lp.TENLOAIPHONG;
                objP.DONGIA = lp.DONGIA;
                lstP.Add(objP);
            }
            return lstP;
        }
        public List<tb_Phong> getAll(int idphong)
        {
            return db.tb_Phong.Where(x => x.IDPHONG == idphong).ToList();
        }
        public void add(tb_Phong p)
        {
            try
            {
                db.tb_Phong.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trogn quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void update(tb_Phong p)
        {
            tb_Phong _p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == p.IDPHONG);
            _p.IDPHONG = p.IDPHONG;
            _p.TENPHONG = p.TENPHONG;
            _p.TRANGTHAI = p.TRANGTHAI;
            _p.IDTANG = p.IDTANG;
            _p.IDLOAIPHONG = p.IDLOAIPHONG;
            _p.DISABLED = p.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(int idphong)
        {
            tb_Phong _p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == idphong);
            _p.DISABLED = true;
            try
            {
                db.tb_Phong.Remove(_p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    }
}
