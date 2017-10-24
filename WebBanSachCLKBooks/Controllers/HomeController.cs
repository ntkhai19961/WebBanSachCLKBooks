using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSachCLKBooks.Models;

namespace WebBanSachCLKBooks.Controllers
{
    public class HomeController : Controller
    {

        QLBanSachDataContext db = new QLBanSachDataContext();

        public ActionResult Index()
        {
            //var lstSACH = LaySach();
            var query = db.SP_SELECT_SACH();
            return View(query);
        }
    
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

        public ActionResult Detail(int id)
        {
            var query = db.SP_SELECT_SACH_THEO_MASACH(id);

            return View(query.Single());
        }
    }
}