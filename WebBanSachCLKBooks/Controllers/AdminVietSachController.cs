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
    public class AdminVietSachController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        #region Thông Báo Ngoại Lệ Cập Nhật Viết Sách
        private void SetAlert(String alert, String type)
        {
            TempData["AlertMessage_AdminVietSach"] = alert;
            if (type == "success")
            {
                TempData["AlertType_AdminVietSach"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType_AdminVietSach"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType_AdminVietSach"] = "alert-danger";
            }
        }
        #endregion

        // GET: AdminVietSach
        public ActionResult IndexAdminVietSach()
        {
            var query = db.SP_SELECT_VIETSACH_GOM_MATG_MASACH();

            return View(query);
        }


        #region  Create Viết Sách
        public ActionResult CreateVietSach()
        {

            VietSachViewModels viewModels = new VietSachViewModels
            {
                // 2 dòng này select dữ liệu ra để đưa vào combo box
                VietSach_Sach = db.SP_SELECT_SACH(),
                VietSach_TacGia = db.SP_SELECT_TACGIA()
            };

            return View(viewModels);
        }

        [HttpPost]
        public ActionResult CreateVietSach(VietSachViewModels viewModel)
        {
            try
            {
                db.SP_INSERT_VIETSACH(viewModel.SelectedMaTacGia, viewModel.SelectedMaSach, viewModel.VaiTro, viewModel.ViTri);
                db.SubmitChanges();

                return RedirectToAction("IndexAdminVietSach", "AdminVietSach");
            }
            catch
            {
                SetAlert("Thông tin này đã tồn tại","error");

                return RedirectToAction("CreateVietSach", "AdminVietSach");
            }

            
        }
        #endregion


        #region Edit Viết Sách
        public ActionResult EditVietSach(int MaTG , int Masach)
        {
            
            // lấy ra thông tin để edit
            var query = db.SP_SELECT_VIETSACH_THEO_MATG_MASACH(MaTG , Masach).Single();

            VietSachViewModels viewModels = new VietSachViewModels
            {
                // 2 dòng này select dữ liệu ra để đưa vào combo box
                VietSach_Sach = db.SP_SELECT_SACH(),
                VietSach_TacGia = db.SP_SELECT_TACGIA(),

                // Mã tác giả và sách đang được chọn để edit
                SelectedMaTacGia = query.MaTG,
                SelectedTenTacGia = query.TenTG,

                SelectedMaSach = query.Masach,
                SelectedTenSach = query.Tensach,

                VaiTro = query.Vaitro,
                ViTri = query.Vitri
            };

            return View(viewModels);
        }

        [HttpPost]
        public ActionResult EditVietSach(int MaTG_HienTai , int MaSach_HienTai ,  int SelectedMaTacGia, int SelectedMaSach , String VaiTro , String ViTri)
        {
            try
            {
                db.SP_UPDATE_VIETSACH_THEO_MATG_MASACH(MaTG_HienTai, MaSach_HienTai, SelectedMaTacGia, SelectedMaSach, VaiTro, ViTri);
                db.SubmitChanges();

                return RedirectToAction("IndexAdminVietSach", "AdminVietSach");
            }
            catch
            {
                SetAlert("Thông tin này đã tồn tại","error");

                return RedirectToAction("EditVietSach", new { MaTG = MaTG_HienTai , Masach = MaSach_HienTai });
            }

            
        }
        #endregion


        #region Delete Viết Sách
        public ActionResult DeleteVietSach(int MaTG , int Masach)
        {
            try
            {
                db.SP_DELETE_VIETSACH_THEO_MATG_MASACH(MaTG, Masach);
                db.SubmitChanges();
            }
            catch
            {
                SetAlert("Không thể xóa sách này","error");
            }

            return RedirectToAction("IndexAdminVietSach","AdminVietSach");
        }
        #endregion
    }
}