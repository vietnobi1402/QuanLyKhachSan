using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BUS;
using DevExpress.XtraNavBar;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;

namespace GUI
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        FUNC _func;
        TANG _tang;
        PHONG _phong;
        GalleryItem item=null;
        private void Form1_Load(object sender, EventArgs e)
        {
            _func = new FUNC();
            _phong = new PHONG();
            _tang = new TANG();

            leftMenu();
            showRoom();

        }
        void leftMenu()
        {
            int i = 0;
            var _lsParent = _func.getParent();
            foreach(var _pr in _lsParent)
            {
                NavBarGroup navGroup = new NavBarGroup(_pr.DESCRIPTION);
                navGroup.Tag = _pr.FUNC_CODE;
                navGroup.Name = _pr.FUNC_CODE;
                navGroup.ImageOptions.LargeImageIndex = i;
                i++;
                navMain.Groups.Add(navGroup);

                var _lschild = _func.getChild(_pr.FUNC_CODE);
                foreach(var _ch in _lschild)
                {
                    NavBarItem navItem = new NavBarItem(_ch.DESCRIPTION);
                    navItem.Tag = _ch.FUNC_CODE;
                    navItem.Name = _ch.FUNC_CODE;
                    navItem.ImageOptions.SmallImageIndex = 0;
                    navGroup.ItemLinks.Add(navItem);
                }
                navMain.Groups[navGroup.Name].Expanded = true;
            }
            
        }
        void showRoom()
        {
            var lsTang = _tang.getAll();
            gControl.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            gControl.Gallery.ImageSize = new Size(64, 64);
            gControl.Gallery.ShowItemText = true;
            gControl.Gallery.ShowGroupCaption = true;
            foreach(var t in lsTang)
            {
                var galleryItem = new GalleryItemGroup();
                galleryItem.Caption = t.TENTANG;
                galleryItem.CaptionAlignment = GalleryItemGroupCaptionAlignment.Stretch;
                var lsPhong = _phong.getByTang(t.IDTANG.ToString());
                foreach(var p in lsPhong)
                {
                    var gc_item = new GalleryItem();
                    gc_item.Caption = p.TENPHONG;
                    gc_item.Value = p.IDPHONG;
                    if (p.TRANGTHAI == true)
                    {
                        gc_item.ImageOptions.Image = imageList3.Images[0];
                    }
                    else
                    {
                        gc_item.ImageOptions.Image = imageList3.Images[1];
                    }
                    
                    galleryItem.Items.Add(gc_item);
                }
                gControl.Gallery.Groups.Add(galleryItem);
            }
            
        }

        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string func_code = e.Link.Item.Tag.ToString();
            switch (func_code)
            {
                case "CONGTY":
                    {
                        frmCongTy FRM = new frmCongTy();
                        FRM.ShowDialog();
                        break;
                    }
                case "DONVI":
                    {
                        frmDonVi FRM = new frmDonVi();
                        FRM.ShowDialog();
                        break;
                    }
                case "KHACHHANG":
                    {
                        frmKhachHang FRM = new frmKhachHang();
                        FRM.ShowDialog();
                        break;
                    }
                case "TANG":
                    {
                        frmTang FRM = new frmTang();
                        FRM.ShowDialog();
                        break;
                    }
                case "LOAIPHONG":
                    {
                        frmLoaiPhong FRM = new frmLoaiPhong();
                        FRM.ShowDialog();
                        break;
                    }
                case "SANPHAM":
                    {
                        frmSanPham FRM = new frmSanPham();
                        FRM.ShowDialog();
                        break;
                    }
                case "THIETBI":
                    {
                        frmThietBi FRM = new frmThietBi();
                        FRM.ShowDialog();
                        break;
                    }
                case "PHONG":
                    {
                        frmPhong FRM = new frmPhong();
                        FRM.ShowDialog();
                        break;
                    }
                case "PHONG_THIETBI":
                    {
                        frmPhong_ThietBi FRM = new frmPhong_ThietBi();
                        FRM.ShowDialog();
                        break;
                    }
                case "DPTD":
                    {
                        frmDatPhong FRM = new frmDatPhong();
                        FRM.ShowDialog();
                        break;
                    }
            }
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void popupMenu1_Popup(object sender, EventArgs e)
        {
            Point point = gControl.PointToClient(Control.MousePosition);
            RibbonHitInfo hitInfo = gControl.CalcHitInfo(point);
            if (hitInfo.InGalleryItem || hitInfo.HitTest == RibbonHitTest.GalleryImage)
                item = hitInfo.GalleryItem;
        }
        private void btnDatPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var gc_item = new GalleryItem();
            string id = item.Value.ToString();
            MessageBox.Show(id);
        }
        private void btnSPDV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }



        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

    }
}
