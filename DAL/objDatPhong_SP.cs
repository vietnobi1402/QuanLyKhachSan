using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class objDatPhong_SP
    {
        public int IDSP { get; set; }
        public string TENSP { get; set; }
        public int SOLUONG { get; set; }
        public double DONGIA { get; set; }
        public double THANHTIEN { get; set; }
        public int IDPHONG { get; set; }
        public string TENPHONG { set; get; }
        public int IDDP { get; set; }
    }
}
