using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebBanSachCLKBooks.Models;

namespace WebBanSachCLKBooks.Controllers
{
    public class GioHangController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        public static List<GioHang> lstGioHang = new List<GioHang>();

        #region Thông Báo Ngoại Lệ Thời Gian Ngày Giao
        private void SetAlert(String alert, String type)
        {
            TempData["AlertMessage_DatHang"] = alert;
            if (type == "success")
            {
                TempData["AlertType_DatHang"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType_DatHang"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType_DatHang"] = "alert-danger";
            }
        }
        #endregion

        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        #region Cập Nhật Giỏ Hàng
        public ActionResult ThemGioHang(int MaSach)
        {                 
            GioHang SanPham = lstGioHang.Find(n => n.MaSach == MaSach);
            if(SanPham == null)
            {
                SanPham = new GioHang(MaSach);
                lstGioHang.Add(SanPham);               
                return RedirectToAction("GioHang","GioHang");
            }
            else
            {
                lstGioHang.Find(n => n.MaSach == MaSach).SoLuong++;
                return RedirectToAction("GioHang", "GioHang");
            }
        }

        public ActionResult XoaGioHang(int MaSach)
        {

            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("GioHang", "GioHang");
            }
            else
            {
                lstGioHang.RemoveAll(n => n.MaSach == MaSach);
                return RedirectToAction("GioHang", "GioHang");
            }
        }

        public ActionResult XoaTatCaGioHang()
        {
            lstGioHang.Clear();
            return RedirectToAction("GioHang","GioHang");
        }

        public ActionResult CapNhatGioHang(int MaSach , FormCollection collection)
        {
            lstGioHang.Find(n => n.MaSach == MaSach).SoLuong = int.Parse(collection["txtSoLuong"].ToString());
            return RedirectToAction("GioHang","GioHang");
        }
        #endregion


        #region View Giỏ Hàng
        // Tính tổng số lượng các sản phẩm trong giỏ hàng
        public int TongSoLuong()
        {
            int TongSoLuong = 0;
            if(lstGioHang != null)
            {
                TongSoLuong = lstGioHang.Sum(n => n.SoLuong);
            }
            return TongSoLuong;
        }

        // Tính tổng tiền phải trả trong giỏ hàng
        public double TongTien()
        {
            double TongTien = 0;
            if( lstGioHang != null)
            {
                TongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return TongTien;
        }

        // View giỏ hàng
        public ActionResult GioHang()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        #endregion


        #region Đặt Hàng
        // Đặt Hàng
        [HttpGet]
        [Authorize]
        public ActionResult DatHang()
        {

            //if(Session["KiemTraDangNhapChua"] == null || (int)Session["KiemTraDangNhapChua"] == 0 )
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            AspNetUser user = new AspNetUser();
            user.Id = User.Identity.GetUserId();
            user.Email = User.Identity.GetUserName();
            Session["User"] = user;

            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        public ActionResult DatHang(FormCollection collection)
        {

            bool CheckThoiGianGiaoHang = false;

            DONDATHANG ddh = new DONDATHANG();
            AspNetUser user = (AspNetUser)Session["User"];

            // Thêm Vào Đơn Hàng
            ddh.Id = user.Id;
            ddh.Ngaydat = DateTime.Now;
            try
            {
                var NgayGiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
                ddh.Ngaygiao = DateTime.Parse(NgayGiao);
                CheckThoiGianGiaoHang = true;
            }
            catch
            {
                CheckThoiGianGiaoHang = false;
                SetAlert("Xin Vui Lòng Chọn Ngày Giao Hàng Hợp Lệ","error");
            }

            if(CheckThoiGianGiaoHang == true)
            {

                ddh.Tinhtranggiaohang = false;
                ddh.Dathanhtoan = false;

                db.DONDATHANGs.InsertOnSubmit(ddh);
                db.SubmitChanges();

                // Thêm Vào CTDH
                foreach (var item in lstGioHang)
                {
                    CHITIETDONTHANG ctdh = new CHITIETDONTHANG();
                    ctdh.MaDonHang = ddh.MaDonHang;
                    ctdh.Masach = item.MaSach;
                    ctdh.Soluong = item.SoLuong;
                    ctdh.Dongia = (decimal)item.DonGia;
                    db.SP_INSERT_CTDH(ctdh.MaDonHang, ctdh.Masach, ctdh.Soluong, ctdh.Dongia);
                }
                db.SubmitChanges();

                lstGioHang.Clear();

                return RedirectToAction("XacNhanDonHang", "GioHang");
            }
            else
            {
                return RedirectToAction("DatHang", "GioHang");
            }

        }
        #endregion


        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}