using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BUS
{
    public class PHONG_THIETBI
    {
        Entities db;
        public PHONG_THIETBI()
        {
            db = Entities.CreateEntities();
        }
        public tb_Phong_ThietBi getItem(int idphong)
        {
            return db.tb_Phong_ThietBi.FirstOrDefault(x => x.IDPHONG == idphong);
        }
        public List<tb_Phong_ThietBi> getAll()
        {
            return db.tb_Phong_ThietBi.ToList();
        }
        public List<objPhong_Thietbi> getList()
        {
            var lstPhongtb = db.tb_Phong_ThietBi.ToList();
            List<objPhong_Thietbi> lstPtb = new List<objPhong_Thietbi>();
            objPhong_Thietbi objPTB;
            foreach (var item in lstPhongtb)
            {
                objPTB = new objPhong_Thietbi();
                objPTB.IDPHONG = item.IDPHONG;
                var p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == item.IDPHONG);
                objPTB.TENPHONG = p.TENPHONG;

                objPTB.IDTB = item.IDTB;
                var tb = db.tb_ThietBi.FirstOrDefault(x => x.IDTB == item.IDTB);
                objPTB.TENTB = tb.TENTB;

                objPTB.SOLUONG = item.SOLUONG;
                lstPtb.Add(objPTB);
            }
            return lstPtb;
        }
        public List<tb_Phong_ThietBi> getAll(int idphong)
        {
            return db.tb_Phong_ThietBi.Where(x => x.IDPHONG == idphong).ToList();
        }
        public void add(tb_Phong_ThietBi ptb)
        {
            try
            {
                db.tb_Phong_ThietBi.Add(ptb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trogn quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        //public void update(tb_Phong_ThietBi ptb)
        //{
        //    tb_Phong_ThietBi _ptb = db.tb_Phong_ThietBi.FirstOrDefault(x => x.IDPHONG == ptb.IDPHONG);
        //    _ptb.IDTB = ptb.IDTB;
        //    _ptb.SOLUONG = ptb.SOLUONG;
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
        //    }
        //}
        public void delete(int idphong)
        {
            tb_Phong_ThietBi _ptb = db.tb_Phong_ThietBi.FirstOrDefault(x => x.IDPHONG == idphong);
            try
            {
                db.tb_Phong_ThietBi.Remove(_ptb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    }
}
