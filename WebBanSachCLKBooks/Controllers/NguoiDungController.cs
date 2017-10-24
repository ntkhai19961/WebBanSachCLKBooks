using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSachCLKBooks.Models;

namespace WebBanSachCLKBooks.Controllers
{
    public class NguoiDungController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        // POST : Hàm DangKy(post) nhận dữ liệu từ trang DangKy
        [HttpPost]
        public ActionResult DangKy(FormCollection collection)
        {
            var HoTen = collection["HoTenKH"];
            var TenDN = collection["TenDN"];
            var MatKhau = collection["MatKhau"];
            var MatKhauNhapLai = collection["MatKhauNhapLai"];
            var Email = collection["Email"];
            var DiaChi = collection["DiaChi"];
            var DienThoai = collection["DienThoai"];
            var NgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);

            if (String.IsNullOrEmpty(HoTen))
            {
                ViewData["Loi1"] = "Họ Tên Khách Hàng Không Được Để Trống";
            }
            else if (String.IsNullOrEmpty(TenDN))
            {
                ViewData["Loi2"] = "Phải Nhập Tên Đăng Nhập";
            }
            else if (String.IsNullOrEmpty(MatKhau))
            {
                ViewData["Loi3"] = "Phải Nhập Mật Khẩu";
            }
            else if (String.IsNullOrEmpty(MatKhauNhapLai))
            {
                ViewData["Loi4"] = "Phải Nhập Lại Mật Khẩu";
            }
            else if (String.IsNullOrEmpty(Email))
            {
                ViewData["Loi5"] = "Email Không Được Bỏ Trống";
            }
            else if (String.IsNullOrEmpty(DienThoai))
            {
                ViewData["Loi6"] = "Phải Nhập Điện Thoại";
            }
            else {

                //try
                //{
                //    DateTime NgaySinhParse = DateTime.Parse(NgaySinh);
                //    db.SP_INSERT_KHACHHANG(HoTen, TenDN, MatKhau, Email, DiaChi, DienThoai, NgaySinhParse);
                //    db.SubmitChanges();
                //    return RedirectToAction("DangNhap");
                //}
                //catch
                //{
                //    ViewData["Loi7"] = "Ngày Sinh Không Phù Hợp";
                //}
            }

            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var TenDN = collection["TenDN"];
            var MatKhau = collection["MatKhau"];

            if(String.IsNullOrEmpty(TenDN))
            {
                ViewData["Loi1"] = "Phải Nhập Tên Đăng Nhập";
            }
            else if(String.IsNullOrEmpty(MatKhau))
            {
                ViewData["Loi2"] = "Chưa Nhập Mật Khẩu";
            }
            else
            {
                //KHACHHANG kh = new KHACHHANG();

                //var query = db.SP_KIEMTRA_DANGNHAP(TenDN,MatKhau).SingleOrDefault();

                //if (query != null)
                //{

                //    kh.MaKH = query.MaKH;
                //    kh.Taikhoan = query.Taikhoan;
                //    kh.Matkhau = query.Matkhau;
                //    kh.Email = query.Email;
                //    kh.DiachiKH = query.DiachiKH;
                //    kh.DienthoaiKH = query.DienthoaiKH;
                //    kh.Ngaysinh = query.Ngaysinh;

                //    ViewBag.Thongbao = "Đã Đăng Nhập Thành Công";
                //    Session["Taikhoan"] = kh;
                //}
                //else
                //{
                //    ViewBag.Thongbao = "Đăng Nhập Thất Bại";
                //}

            }
            
            return View();
        }

    }
}