using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class TANG
    {
        Entities db;
        public TANG()
        {
            db = Entities.CreateEntities();
        }
        public tb_Tang getItem(int idtang)
        {
            return db.tb_Tang.FirstOrDefault(x => x.IDTANG == idtang);
        }
        public List<tb_Tang> getAll()
        {
            return db.tb_Tang.ToList();
        }
        public void add(tb_Tang t)
        {
            try
            {
                db.tb_Tang.Add(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }

        }
        public void update(tb_Tang t)
        {
            tb_Tang _t = db.tb_Tang.FirstOrDefault(x => x.IDTANG == t.IDTANG);
            _t.TENTANG = t.TENTANG;
            _t.DISABLED = t.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
        public void delete(int idtang)
        {
            tb_Tang t = db.tb_Tang.FirstOrDefault(x => x.IDTANG == idtang);
            t.DISABLED = true;
            try
            {
                db.tb_Tang.Remove(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy thẻ trong quá trình xử lý dữ liệu " + ex.Message);
            }
        }
    }
}
