using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace GUI
{
    public partial class frmDatPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmDatPhong()
        {
            InitializeComponent();
            //DataTable tb = myFunctions.laydulieu("SELECT A.IDPHONG,A.TENPHONG,C.DONGIA,A.IDTANG,B.TENTANG FROM tb_Phong A,tb_Tang B,tb_LoaiPhong C WHERE A.IDTANG=B.IDTANG AND A.TRANGTHAI=0 AND A.IDLOAIPHONG=C.IDLOAIPHONG");
            //gcPhong.DataSource = tb;
            //gcDatPhong.DataSource = tb.Clone();
        }
        bool _them;
        int _idPhong;
        string _tenPhong;
        List<objDatPhong_SP> lstDPSP;
        List<ObjPhong> lstDPCT;

        KHACHHANG _khachhang;
        SANPHAM _sanpham;
        PHONG _phong;
        //GridHitInfo downHitInfor = null;

        private void frmDatPhong_Load(object sender, EventArgs e)
        {
            _khachhang = new KHACHHANG();
            _sanpham = new SANPHAM();
            _phong = new PHONG();
            lstDPSP = new List<objDatPhong_SP>();
            lstDPCT = new List<ObjPhong>();

            dtTuNgay.Value = myFunctions.getFirstDayInMont(DateTime.Now.Year,DateTime.Now.Month);
            dtDenNgay.Value = DateTime.Now;

            loadKH();
            loadSP();
            loadPhong();
            //loadTT();

            showHideControl(true);
            _enabled(false);
            gvPhong.ExpandAllGroups();
            tabDanhSach.SelectedTabPage = pageDanhSach;
            
        }
        void loadKH()
        {
            cboKhachHang.DataSource = _khachhang.getAll();
            cboKhachHang.DisplayMember = "HOTEN";
            cboKhachHang.ValueMember = "IDKH";
        }
        void loadSP()
        {
            gcSanPham.DataSource = _sanpham.getAll();
            gvSanPham.OptionsBehavior.Editable = false;
        }
        void loadPhong()
        {
            gcPhong.DataSource = _phong.getTT();
            gvPhong.OptionsBehavior.Editable = false;
        }
        //void loadTT()
        //{
        //    cboTrangThai.DataSource = TRANGTHAI.getList();
        //    cboTrangThai.ValueMember = "_value";
        //    cboTrangThai.DisplayMember = "_display";
        //}

        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
            btnIn.Visible = t;
        }
        void _enabled(bool t)
        {
            cboKhachHang.Enabled = t;
            btnAddNew.Enabled = t;
            dtNgayDat.Enabled = t;
            dtNgayTra.Enabled = t;
            cboTrangThai.Enabled = t;
            chkDoan.Enabled = t;
            txtSoNguoi.Enabled = t;
            txtGhiChu.Enabled = t;
        }
        void _reset()
        {
            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);
            txtSoNguoi.Text = "1";
            chkDoan.Checked = true;
            cboTrangThai.SelectedValue = false;
            txtGhiChu.Text = "";
            txtThanhTien.Text = "0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            tabDanhSach.SelectedTabPage = xtraTabPage2;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
            tabDanhSach.SelectedTabPage = xtraTabPage2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(false);
            showHideControl(true);
            
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            tabDanhSach.SelectedTabPage = pageDanhSach;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void gvDatPhong_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (gvDatPhong.GetFocusedRowCellValue("IDPHONG") != null)
        //    {
        //        _idPhong = int.Parse(gvDatPhong.GetFocusedRowCellValue("IDPHONG").ToString());
        //        _tenPhong = gvDatPhong.GetFocusedRowCellValue("TENPHONG").ToString();
        //    }
        //    GridView view = sender as GridView;
        //    downHitInfor = null;
        //    GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
        //    if (Control.ModifierKeys != Keys.None) return;
        //    if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
        //    {
        //        downHitInfor = hitInfo;
        //    }
        //}

        //private void gvDatPhong_MouseMove(object sender, MouseEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (e.Button == MouseButtons.Left && downHitInfor != null)
        //    {
        //        Size dragSize = SystemInformation.DragSize;
        //        Rectangle dragReact = new Rectangle(new Point(downHitInfor.HitPoint.X - dragSize.Width / 2, downHitInfor.HitPoint.Y - dragSize.Height / 2), dragSize);
        //        if (!dragReact.Contains(new Point(e.X, e.Y)))
        //        {
        //            DataRow row = view.GetDataRow(downHitInfor.RowHandle);
        //            view.GridControl.DoDragDrop(row, DragDropEffects.Move);
        //            downHitInfor = null;
        //            DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        //        }
        //    }
        //}

        //private void gvPhong_MouseDown(object sender, MouseEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    downHitInfor = null;
        //    GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
        //    if (Control.ModifierKeys != Keys.None) return;
        //    if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
        //    {
        //        downHitInfor = hitInfo;
        //    }
        //}

        //private void gvPhong_MouseMove(object sender, MouseEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (e.Button == MouseButtons.Left && downHitInfor != null)
        //    {
        //        Size dragSize = SystemInformation.DragSize;
        //        Rectangle dragReact = new Rectangle(new Point(downHitInfor.HitPoint.X - dragSize.Width / 2, downHitInfor.HitPoint.Y - dragSize.Height / 2), dragSize);
        //        if (!dragReact.Contains(new Point(e.X, e.Y)))
        //        {
        //            DataRow row = view.GetDataRow(downHitInfor.RowHandle);
        //            view.GridControl.DoDragDrop(row, DragDropEffects.Move);
        //            downHitInfor = null;
        //            DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        //        }
        //    }
        //}

        //private void gcPhong_DragDrop(object sender, DragEventArgs e)
        //{
        //    GridControl grid = sender as GridControl;
        //    DataTable tb = grid.DataSource as DataTable;
        //    DataRow row = e.Data.GetData(typeof(DataRow)) as DataRow;
        //    if (row != null && tb != null && row.Table != tb)
        //    {
        //        tb.ImportRow(row);
        //        row.Delete();
        //    }
        //}

        //private void gcPhong_DragOver(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(typeof(DataRow)))
        //    {
        //        e.Effect = DragDropEffects.Move;
        //    }
        //    else
        //    {
        //        e.Effect = DragDropEffects.None;
        //    }
        //}
        bool cal(Int32 _width,GridView _view)
        {
            _view.IndicatorWidth = _view.IndicatorWidth < _width ? _width : _view.IndicatorWidth;
            return true;
        }

        private void gvPhong_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvPhong.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_width, gvPhong); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_width, gvPhong); }));
            }
        }

        private void gcSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                objDatPhong_SP sp = new objDatPhong_SP();
                sp.IDSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                sp.TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString();
                sp.IDPHONG = _idPhong;
                sp.TENPHONG = _tenPhong;
                sp.DONGIA = float.Parse(gvSanPham.GetFocusedRowCellValue("DONGIA").ToString());
                sp.SOLUONG = 1;
                sp.THANHTIEN = sp.DONGIA * sp.SOLUONG;
                foreach(var item in lstDPSP)
                {
                    if (item.IDSP == sp.IDSP && item.IDPHONG == sp.IDPHONG)
                    {
                        item.SOLUONG = item.SOLUONG + 1;
                        item.THANHTIEN = item.SOLUONG * item.DONGIA;
                        loadDPSP();
                        return;
                    }
                    
                }
                lstDPSP.Add(sp);
            }
            loadDPSP();
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + double.Parse(gvDatPhong.Columns["DONGIA"].SummaryItem.SummaryValue.ToString())).ToString("N0");
        }
        
        void loadDPSP()
        {
            List<objDatPhong_SP> lstDP = new List<objDatPhong_SP>();
            foreach(var item in lstDPSP)
            {
                lstDP.Add(item);
            }
            gcSPDV.DataSource = lstDP;
        }

        private void gcPhong_DoubleClick(object sender, EventArgs e)
        {
            if (gvPhong.GetFocusedRowCellValue("IDPHONG") != null)
            {   ObjPhong phong = new ObjPhong();
                phong.IDPHONG = int.Parse(gvPhong.GetFocusedRowCellValue("IDPHONG").ToString());
                phong.TENPHONG = gvPhong.GetFocusedRowCellValue("TENPHONG").ToString();
                phong.DONGIA = double.Parse(gvPhong.GetFocusedRowCellValue("DONGIA").ToString());
                lstDPCT.Add(phong);
            }
            loadPhongDat();
            txtThanhTien.Text = (double.Parse(gvDatPhong.Columns["DONGIA"].SummaryItem.SummaryValue.ToString())).ToString("N0");
        }
        void loadPhongDat()
        {
            gcDatPhong.DataSource = lstDPCT;
            if (gvDatPhong.GetFocusedRowCellValue("IDPHONG") != null)
            {
                _idPhong = int.Parse(gvDatPhong.GetFocusedRowCellValue("IDPHONG").ToString());
                _tenPhong = gvDatPhong.GetFocusedRowCellValue("TENPHONG").ToString();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmKhachHang FRM = new frmKhachHang();
            FRM.ShowDialog();
        }
    }
}