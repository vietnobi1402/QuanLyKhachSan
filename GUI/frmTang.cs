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
    public partial class frmTang : DevExpress.XtraEditors.XtraForm
    {
        public frmTang()
        {
            InitializeComponent();
        }
        TANG _t;
        bool _them;
        int _idtang;
        private void frmTang_Load(object sender, EventArgs e)
        {
            _t = new TANG();
            loadData();
            showHideControl(true);
            _enabled(false);
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
            chkDisable.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            chkDisable.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _t.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;

        }
        private Boolean checkLoiNhapLieu()
        {
            Boolean kq = true;
            string strErrors = "";
            if (txtTen.Text.Trim() == "")
            {
                strErrors += "Chưa nhập tên tầng; ";
                kq = false;
                txtTen.Focus();
            }
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
                _t.delete(_idtang);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                if (checkLoiNhapLieu())
                {
                    tb_Tang t = new tb_Tang();
                    t.TENTANG = txtTen.Text;
                    t.DISABLED = chkDisable.Checked;
                    _t.add(t);
                    _them = false;
                    loadData();
                    _enabled(false);
                    showHideControl(true);
                }
                
            }
            else
            {
                tb_Tang t = _t.getItem(_idtang);
                t.TENTANG = txtTen.Text;
                t.DISABLED = chkDisable.Checked;
                _t.update(t);
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
                _idtang = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDTANG").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENTANG").ToString();
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
        }
    }
}