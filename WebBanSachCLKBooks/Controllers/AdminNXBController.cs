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
    public class AdminNXBController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        #region Thông Báo Ngoại Lệ Cập Nhật Nhà Xuất Bản
        private void SetAlert(String alert, String type)
        {
            TempData["AlertMessage_AdminNXB"] = alert;
            if (type == "success")
            {
                TempData["AlertType_AdminNXB"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType_AdminNXB"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType_AdminNXB"] = "alert-danger";
            }
        }
        #endregion

        // GET: AdminNXB
        public ActionResult IndexAdminNXB()
        {
            var query = db.SP_SELECT_NHAXUATBAN();

            return View(query);
        }

        #region Edit Nhà Xuất Bản
        public ActionResult EditNXB(int id)
        {
            var query = db.SP_SELECT_NXB_THEO_MANXB(id).Single();

            NhaXuatBanViewModels viewModel = new NhaXuatBanViewModels
            {
                MaNXB = query.MaNXB,
                TenNXB = query.TenNXB,
                DiaChi = query.Diachi,
                DienThoai = query.DienThoai
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditNXB(NhaXuatBanViewModels viewModel)
        {
            db.SP_UPDATE_NXB_THEO_MANXB(viewModel.MaNXB, viewModel.TenNXB, viewModel.DiaChi, viewModel.DienThoai);
            db.SubmitChanges();

            return RedirectToAction("IndexAdminNXB", "AdminNXB"); ;
        }
        #endregion


        #region Delete Nhà Xuất Bản
        public ActionResult DeleteNXB(int id)
        {
            try
            {
                db.SP_DELETE_NXB_THEO_MANXB(id);
                db.SubmitChanges();
            }
            catch
            {
                SetAlert("Không Thể Xóa Nhà Xuất Bản Này", "error");
            }

            return RedirectToAction("IndexAdminNXB", "AdminNXB");
        }
        #endregion


        #region Create Nhà Xuất Bản
        public ActionResult CreateNXB()
        {
            NhaXuatBanViewModels viewModel = new NhaXuatBanViewModels
            {

            };
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateNXB(NhaXuatBanViewModels viewModel)
        {
            db.SP_INSERT_NHAXUATBAN(viewModel.TenNXB, viewModel.DiaChi, viewModel.DienThoai);
            db.SubmitChanges();

            return RedirectToAction("IndexAdminNXB", "AdminNXB");
        }
        #endregion
    }
}