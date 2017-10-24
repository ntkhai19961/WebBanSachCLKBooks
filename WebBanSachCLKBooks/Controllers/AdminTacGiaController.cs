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
    public class AdminTacGiaController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        #region Thông Báo Ngoại Lệ Cập Nhật Tác Giả
        private void SetAlert(String alert, String type)
        {
            TempData["AlertMessage_AdminTacGia"] = alert;
            if (type == "success")
            {
                TempData["AlertType_AdminTacGia"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType_AdminTacGia"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType_AdminTacGia"] = "alert-danger";
            }
        }
        #endregion

        // GET: AdminTacGia
        public ActionResult IndexAdminTacGia()
        {
            var query = db.SP_SELECT_TACGIA();

            return View(query);
        }

        #region Edit Tác Giả
        public ActionResult EditTacGia(int id)
        {
            var query = db.SP_SELECT_TACGIA_THEO_MATACGIA(id).Single();

            TacGiaViewModels viewModel = new TacGiaViewModels
            {
                MaTG = query.MaTG,
                TenTG = query.TenTG,
                DiaChi = query.Diachi,
                TieuSu = query.Tieusu,
                DienThoai = query.Dienthoai
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditTacGia(TacGiaViewModels viewModel)
        {
            db.SP_UPDATE_TACGIA_THEO_MATACGIA(viewModel.MaTG, viewModel.TenTG, viewModel.DiaChi, viewModel.TieuSu, viewModel.DienThoai);
            db.SubmitChanges();

            return RedirectToAction("IndexAdminTacGia","AdminTacGia");
        }
        #endregion


        #region Delete Tác Giả
        public ActionResult DeleteTacGia(int id)
        {
            try
            {
                db.SP_DELETE_TACGIA_THEO_MATACGIA(id);
                db.SubmitChanges();
            }   
            catch
            {
                SetAlert("Không thể xóa tác giả này","error");
            }
            return RedirectToAction("IndexAdminTacGia", "AdminTacGia");
        }
        #endregion


        #region Create Tác Giả
        public ActionResult CreateTacGia()
        {
            TacGiaViewModels viewModel = new TacGiaViewModels
            {

            };
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateTacGia(TacGiaViewModels viewModel)
        {
            db.SP_INSERT_TACGIA(viewModel.TenTG, viewModel.DiaChi, viewModel.TieuSu, viewModel.DienThoai);
            db.SubmitChanges();

            return RedirectToAction("IndexAdminTacGia", "AdminTacGia");
        }
        #endregion
    }
}