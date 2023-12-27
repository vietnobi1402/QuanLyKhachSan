using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BUS
{
    public class THIETBI
    {
        Entities db;
        public THIETBI()
        {
            db = Entities.CreateEntities();
        }
        public tb_ThietBi getItem(int idtb)
        {
            return db.tb_ThietBi.FirstOrDefault(x => x.IDTB == idtb);
        }
        public List<tb_ThietBi> getAll()
        {
            return db.tb_ThietBi.ToList();
        }
        public void add(tb_ThietBi tb)
        {
            try
            {
                db.tb_ThietBi.Add(tb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }

        }
        public void update(tb_ThietBi tb)
        {
            tb_ThietBi _tb = db.tb_ThietBi.FirstOrDefault(x => x.IDTB == tb.IDTB);
            _tb.TENTB = tb.TENTB;
            _tb.DONGIA = tb.DONGIA;
            _tb.DISABLED = tb.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
        public void delete(int idtb)
        {
            tb_ThietBi kh = db.tb_ThietBi.FirstOrDefault(x => x.IDTB == idtb);
            kh.DISABLED = true;
            try
            {
                db.tb_ThietBi.Remove(kh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
    }
}
