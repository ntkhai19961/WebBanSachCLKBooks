using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSachCLKBooks.Models;

namespace WebBanSachCLKBooks.Controllers
{   
    //Tim kiếm sách
    public class TimKiemController : Controller
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        // GET: TimKiem
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string TenSachCanTim = f["txtTimKiem"].ToString();

            if (TenSachCanTim != "")
            {
                var query = db.SP_TIM_KIEM_SACH_THEO_TEN("%" + TenSachCanTim + "%").ToList();
                ViewBag.ThongBaoTimKiem = "Đã tìm thấy " + query.Count + " kết quả";
                return View(query);
            }

            ViewBag.ThongBaoTimKiem = "Không tìm thấy sản phẩm nào";
            return View(db.SP_TIM_KIEM_SACH_THEO_TEN(TenSachCanTim).ToList());
        }

        //    public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        //    {
        //        string GiaSachCanTim = f["txtTimKiem"].ToString();
        //        char[] a1 = new char[100];            

        //        if (GiaSachCanTim != "")
        //        {
        //            string GiaBanDau = null;
        //            string GiaBanCuoi = null;
        //            bool check = false;
        //            for (int i=0; i<GiaSachCanTim.Count() ; i++)
        //            {
        //                if(GiaSachCanTim[i] == '-')
        //                {
        //                    check = true;
        //                    i++;
        //                }
        //                if(check == false)
        //                {
        //                    a1[i] = GiaSachCanTim[i];
        //                    GiaBanDau = GiaBanDau + a1[i];
        //                }
        //                else if(check == true)
        //                {
        //                    a1[i] = GiaSachCanTim[i];
        //                    GiaBanCuoi = GiaBanCuoi + a1[i];
        //                }

        //            }

        //            decimal GiaBanDau_Decimal = decimal.Parse(GiaBanDau);
        //            decimal GiaBanCuoi_Decimal = decimal.Parse(GiaBanCuoi);

        //            var query = db.SP_TIM_KIEM_SACH_THEO_GIA(GiaBanDau_Decimal,GiaBanCuoi_Decimal).ToList();
        //            ViewBag.ThongBaoTimKiem = "Đã tìm thấy " + query.Count + " kết quả";
        //            return View(query);
        //        }

        //        ViewBag.ThongBaoTimKiem = "Không tìm thấy sản phẩm nào";
        //        return View(db.SP_TIM_KIEM_SACH_THEO_GIA(0,0).ToList());
        //    }
        }
    }