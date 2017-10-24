using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSachCLKBooks.Models;
using WebBanSachCLKBooks.ViewModels;

namespace WebBanSachCLKBooks.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        #region Thông Báo Ngoại Lệ Cập Nhật Sách
        private void SetAlert(String alert , String type)
        {
            TempData["AlertMessage_Admin"] = alert;
            if(type == "success")
            {
                TempData["AlertType_Admin"] = "alert-success";
            }
            else if(type == "warning")
            {
                TempData["AlertType_Admin"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType_Admin"] = "alert-danger";
            }
        }
        #endregion
      
        public ActionResult IndexAdmin()
        {
            var query = db.SP_SELECT_SACH_TENCD_TENNXB();

            return View(query);
        }

        #region Create Book
        public ActionResult CreateBook()
        {

            SachViewModels viewModels = new SachViewModels
            {
                SachViewModels_MaCD = db.SP_SELECT_CHUDE(),
                SachViewModels_MaNXB = db.SP_SELECT_NHAXUATBAN(),

                SelectedTenCD = "Tên Chủ Đề",
                SelectedTenNXB = "Tên Nhà Xuất Bản"
            };    

            return View(viewModels);
        }
        #endregion

        #region Create Book POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(SachViewModels viewModels)
        {
            db.SP_INSERT_SACH(viewModels.TenSach, viewModels.GiaBan, viewModels.MoTa, viewModels.AnhBia, viewModels.NgayCapNhat, viewModels.SoLuongTon, viewModels.SelectedMaCD, viewModels.SelectedMaNXB);
            db.SubmitChanges();

            return RedirectToAction("IndexAdmin","Admin");
        }
        #endregion

        #region Edit Book
        public ActionResult EditBook(int id)
        {
            var query = db.SP_SELECT_SACH_THEO_MASACH(id).Single();

            SachViewModels viewModels = new SachViewModels
            {
                // 2 dòng này select dữ liệu ra để đưa vào combo box
                SachViewModels_MaCD = db.SP_SELECT_CHUDE(),
                SachViewModels_MaNXB = db.SP_SELECT_NHAXUATBAN(),

                // Mã tác giả và sách đang được chọn để edit
                SelectedMaCD = (int)query.MaCD,

                SelectedMaNXB = (int)query.MaNXB,

                MaSach = query.Masach,
                TenSach = query.Tensach,
                GiaBan = (int)query.Giaban,
                MoTa = query.Mota,
                AnhBia = query.Anhbia,
                NgayCapNhat = (DateTime)query.Ngaycapnhat,
                SoLuongTon = (int)query.Soluongton
            };

            return View(viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(int MaSach , String TenSach , decimal GiaBan , String MoTa , String AnhBia , DateTime NgayCapNhat , int SoLuongTon, int SelectedMaCD, int SelectedMaNXB)
        {
            if(MaSach == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.SP_UPDATE_SACH_THEO_MASACH(MaSach , TenSach , GiaBan , MoTa , AnhBia , NgayCapNhat, SoLuongTon , SelectedMaCD, SelectedMaNXB);
            }
            return RedirectToAction("IndexAdmin","Admin");
        }
        #endregion

        #region Delete Book
        public ActionResult DeleteBook(int id)
        {

            try 
            {
                db.SP_DELETE_SACH_THEO_MASACH(id);

                db.SubmitChanges();
            }
            catch
            {
                SetAlert("Không Thể Xóa Sách Này","error");
            }


            return RedirectToAction("IndexAdmin","Admin");
        }
        #endregion
    }
}