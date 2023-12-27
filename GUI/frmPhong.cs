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
using DAL;
using BUS;

namespace GUI
{
    public partial class frmPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmPhong()
        {
            InitializeComponent();
        }
        PHONG _phong;
        TANG _tang;
        LOAIPHONG _lp;
        bool _them;
        int _idphong;
        private void frmPhong_Load(object sender, EventArgs e)
        {
            _phong = new PHONG();
            _tang = new TANG();
            _lp = new LOAIPHONG();
            loadTang();
            loadLoaiPhong();
            showHideControl(true);
            _enabled(false);
            loadData();
        }


        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }
        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            cboLoaiphong.Enabled = t;
            cboTang.Enabled = t;
            chkChothue.Enabled = t;
            chkDisable.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            cboTang.SelectedText ="";
            cboLoaiphong.SelectedText ="" ;
            chkChothue.Checked = false;
            chkDisable.Checked = false;
        }
        void loadTang()
        {
            cboTang.DataSource = _tang.getAll();
            cboTang.DisplayMember = "TENTANG";
            cboTang.ValueMember = "IDTANG";
        }
        void loadLoaiPhong()
        {
            cboLoaiphong.DataSource = _lp.getAll();
            cboLoaiphong.DisplayMember = "TENLOAIPHONG";
            cboLoaiphong.ValueMember = "IDLOAIPHONG";
        }
        //void loadPhongByTang()
        //{
        //    gcDanhSach.DataSource = _phong.getByTang(cboTang.SelectedValue.ToString());
        //    gvDanhSach.OptionsBehavior.Editable = false;
        //}
        //void loadPhongByLoaiPhong()
        //{
        //    gcDanhSach.DataSource = _phong.getByLoaiPhong(cboLoaiphong.SelectedIndex.ToString());
        //    gvDanhSach.OptionsBehavior.Editable = false;
        //}
        void loadData()
        {
            gcDanhSach.DataSource = _phong.getList();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private Boolean checkLoiNhapLieu()
        {
            Boolean kq = true;
            string strErrors = "";

            if (txtTen.Text.Trim() == "")
            {
                strErrors += "Chưa nhập tên phòng; ";
                kq = false;
                txtTen.Focus();
            }
            //if (cboTang.SelectedIndex < 0)
            //{
                
            //    strErrors += "Chưa chọn tầng; ";
            //    kq = false;
            //    cboTang.Focus();
            //}
            //if (cboLoaiphong.SelectedIndex < 0)
            //{

            //    strErrors += "Chưa chọn loại phòng; ";
            //    kq = false;
            //    cboLoaiphong.Focus();
            //}
            if (!kq) MessageBox.Show(strErrors);
            return kq;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _phong.delete(_idphong);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                if (checkLoiNhapLieu())
                {   
                    tb_Phong phong = new tb_Phong();
                    phong.TENPHONG = txtTen.Text;
                    phong.IDTANG = int.Parse(cboTang.SelectedValue.ToString());
                    phong.IDLOAIPHONG =int.Parse( cboLoaiphong.SelectedValue.ToString());
                    phong.TRANGTHAI = chkChothue.Checked;
                    phong.DISABLED = chkDisable.Checked;
                    _phong.add(phong);
                    _them = false;
                    loadData();
                    _enabled(false);
                    showHideControl(true);

                }
                
            }
            else
            {
                tb_Phong phong = _phong.getItem(_idphong);
                phong.TENPHONG = txtTen.Text;
                phong.IDTANG = int.Parse(cboTang.SelectedValue.ToString());
                phong.IDLOAIPHONG = int.Parse(cboLoaiphong.SelectedValue.ToString());
                phong.TRANGTHAI = chkChothue.Checked;
                phong.DISABLED = chkDisable.Checked;
                _phong.update(phong);
                _them = false;
                loadData();
                _enabled(false);
                showHideControl(true);
            }
            
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idphong =int.Parse( gvDanhSach.GetFocusedRowCellValue("IDPHONG").ToString());
                cboTang.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDTANG");
                cboLoaiphong.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG");
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENPHONG").ToString();
                chkChothue.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("TRANGTHAI").ToString());
                chkDisable.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources._9004715_cross_delete_remove_cancel_icon;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
            else if (e.Column.Name == "TRẠNG THÁI" && bool.Parse(e.CellValue.ToString()) == true){
                Image img = Properties.Resources._1303888_accept_check_complete_correct_ok_icon;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }
}