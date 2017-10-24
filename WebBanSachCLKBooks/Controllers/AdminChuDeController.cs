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
    public class AdminChuDeController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        #region Thông Báo Ngoại Lệ Cập Nhật Chủ Đề
        private void SetAlert(String alert, String type)
        {
            TempData["AlertMessage_AdminChuDe"] = alert;
            if (type == "success")
            {
                TempData["AlertType_AdminChuDe"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType_AdminChuDe"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType_AdminChuDe"] = "alert-danger";
            }
        }
        #endregion

        // GET: AdminChuDe
        public ActionResult IndexAdminChuDe()
        {
            var query = db.SP_SELECT_CHUDE();

            return View(query);
        }

        #region Edit Chủ Đề
        public ActionResult EditChuDe(int id)
        {
            var query = db.SP_SELECT_CHUDE_THE0_MACD(id).Single();

            ChuDeViewModels viewModels = new ChuDeViewModels
            {
                MaCD = query.MaCD,
                TenChuDe = query.TenChuDe
            };

            return View(viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChuDe(ChuDeViewModels viewModel)
        {
            db.SP_UPDATE_CHUDE_THE0_MACD(viewModel.MaCD , viewModel.TenChuDe);
            db.SubmitChanges();

            return RedirectToAction("IndexAdminChuDe","AdminChuDe"); ;
        }
        #endregion


        #region Delete Chủ Đề
        public ActionResult DeleteChuDe(int id)
        {
            try
            {
                db.SP_DELETE_CHUDE_THE0_MACD(id);
                db.SubmitChanges();
            }
            catch
            {
                SetAlert("Không Thể Xóa Chủ Đề Này","error");
            }

            return RedirectToAction("IndexAdminChuDe", "AdminChuDe"); 
        }
        #endregion


        #region Create Chủ Đề
        public ActionResult CreateChuDe()
        {
            ChuDeViewModels viewModel = new ChuDeViewModels
            {

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateChuDe(ChuDeViewModels viewModel)
        {
            db.SP_INSERT_CHUDE(viewModel.TenChuDe);
            db.SubmitChanges();

            return RedirectToAction("IndexAdminChuDe", "AdminChuDe");
        }
        #endregion
    }
}