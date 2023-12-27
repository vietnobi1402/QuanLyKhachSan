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
    public partial class frmPhong_ThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frmPhong_ThietBi()
        {
            InitializeComponent();
        }
        PHONG_THIETBI _ptb;
        PHONG _phong;
        THIETBI _tb;
        bool _them;
        int _idphong;
        private void frmPhong_ThietBi_Load(object sender, EventArgs e)
        {
            _ptb = new PHONG_THIETBI();
            _phong = new PHONG();
            _tb = new THIETBI();
            loadPhong();
            loadThietBi();
            showHideControl(true);
            _enabled(false);
            loadData();
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }
        void _enabled(bool t)
        {
            cboPhong.Enabled = t;
            cboThietBi.Enabled = t;
            txtSoLuong.Enabled = t;
        }
        void _reset()
        {
            cboPhong.SelectedText = "";
            cboThietBi.SelectedText = "";
            txtSoLuong.Text = "";
        }
        void loadPhong()
        {
            cboPhong.DataSource = _phong.getAll();
            cboPhong.DisplayMember = "TENPHONG";
            cboPhong.ValueMember = "IDPHONG";
        }
        void loadThietBi()
        {
            cboThietBi.DataSource = _tb.getAll();
            cboThietBi.DisplayMember = "TENTB";
            cboThietBi.ValueMember = "IDTB";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _ptb.getList();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
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
                tb_Phong_ThietBi ptb = new tb_Phong_ThietBi();
                ptb.IDPHONG = int.Parse(cboPhong.SelectedValue.ToString());
                ptb.IDTB = int.Parse(cboThietBi.SelectedValue.ToString());
                ptb.SOLUONG =int.Parse( txtSoLuong.Text);
                _ptb.add(ptb);
            }
            else
            {
                //tb_Phong_ThietBi ptb = _ptb.getItem(_idphong);
                //ptb.IDPHONG = int.Parse(cboPhong.SelectedValue.ToString());
                //ptb.IDTB = int.Parse(cboThietBi.SelectedValue.ToString());
                //ptb.SOLUONG = int.Parse(txtSoLuong.Text);
                //_ptb.update(ptb);
            }
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
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
                _idphong = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDPHONG").ToString());
                cboPhong.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDPHONG");
                cboThietBi.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDTB");
                txtSoLuong.Text = gvDanhSach.GetFocusedRowCellValue("SOLUONG").ToString();
            }
        }
    }
}