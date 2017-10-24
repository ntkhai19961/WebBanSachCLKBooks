using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSachCLKBooks.Models
{
    public class GioHang
    {
        QLBanSachDataContext db = new QLBanSachDataContext();

        public int MaSach { get; set; }
        public String TenSach { get; set; }
        public String AnhBia { get; set; }
        public Double DonGia { get; set; }
        public int SoLuong { get; set; }
        public Double ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        public GioHang(int MaSach)
        {
            this.MaSach = MaSach;
            var Sach = db.SP_SELECT_SACH_THEO_MASACH(this.MaSach).Single();
            TenSach = Sach.Tensach;
            AnhBia = Sach.Anhbia;
            DonGia = double.Parse(Sach.Giaban.ToString());
            SoLuong = 1;
        }
    }
}