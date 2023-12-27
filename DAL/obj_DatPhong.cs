using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class obj_DatPhong
    {
        public int IDPHONG { get; set; }
        public string TENPHONG { get; set; }
        public bool? TRANGTHAI { get; set; }
        public int IDTANG { get; set; }
        public string TENTANG { get; set; }
        public int IDLOAIPHONG { get; set; }
        public string TENLOAIPHONG { get; set; }
        public double? DONGIA { get; set; }
        public bool? DISABLED { get; set; }
    }
}
