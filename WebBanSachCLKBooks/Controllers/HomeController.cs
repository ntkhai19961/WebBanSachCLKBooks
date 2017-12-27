using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSachCLKBooks.Models;

using PagedList;
using PagedList.Mvc;

namespace WebBanSachCLKBooks.Controllers
{
    public class HomeController : Controller
    {

        QLBanSachDataContext db = new QLBanSachDataContext();

        #region Trang chủ
        public ActionResult Index(int ? page)
        {
            //biến quy định số sản phẩm trên mỗi trang
            int pageSize = 3;
            //tạo biến số trang
            int pageNum = (page ?? 1);

            var query = db.SP_SELECT_SACH().ToList();
            return View(query.ToPagedList(pageNum,pageSize));
        }
        #endregion

        #region Sách nổi bật , sách lượt xem nhiều
        public ActionResult SachNoiBat()
        {
            var query = db.SP_SACH_NOIBAT(60).ToList();
            return PartialView(query);
        }
        public ActionResult SachLuotXemNhieu()
        {
            var query = db.SP_SACH_LUOT_XEM_NHIEU(10).ToList();
            return PartialView(query);
        }
        #endregion

        #region Chủ đề
        public ActionResult ChuDe()
        {
            var query = db.SP_SELECT_CHUDE();

            return PartialView(query);
        }

        public ActionResult SPTheoChuDe(int id)
        {
            var query = db.SP_SELECT_SACH_THEO_CHUDE(id);

            return View(query);
        }
        #endregion

        #region Nhà Xuất Bản
        public ActionResult NhaXuatBan()
        {

            var query = db.SP_SELECT_NHAXUATBAN();
            
            return PartialView(query);
        }

        public ActionResult SPTheoNXB(int id)
        {
            var query = db.SP_SELECT_SACH_THEO_NXB(id);

            return View(query);
        }
        #endregion

        #region Chi tiết sách
        public ActionResult Detail(int id)
        {
            var query = db.SP_SELECT_SACH_THEO_MASACH(id);
            db.CAP_NHAT_LUOT_XEM_CUA_SACH_KHI_XEM_CHI_TIET_SACH_THEO_MA_SACH(id);

            return View(query.Single());
        }
        #endregion
    
        public ActionResult NoiBat()
        {
            var query = db.SP_SELECT_SACH_THEO_THUOC_TINH_NOI_BAT();
            return View(query);
        }

     }
}