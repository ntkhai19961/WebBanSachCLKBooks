using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BigSchool.ViewModels;
using WebBanSachCLKBooks.Models;
using WebBanSachCLKBooks.ViewModels;

namespace WebBanSachCLKBooks.ViewModels
{
    public class SachViewModels
    {
        public int MaSach { get; set; }    

        [Required]
        [StringLength(100,ErrorMessage ="Không quá 100 ký tự")]
        public String TenSach { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int GiaBan { get; set; }

        [Required]
        public String MoTa { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Không quá 50 ký tự")]
        public String AnhBia { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime NgayCapNhat { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int SoLuongTon { get; set; }

        public int SelectedMaCD { get; set; }
        public String SelectedTenCD { get; set; }

        public int SelectedMaNXB { get; set; }
        public String SelectedTenNXB { get; set; }


        public IEnumerable<SP_SELECT_CHUDEResult> SachViewModels_MaCD { get; set; }
        public IEnumerable<SP_SELECT_NHAXUATBANResult> SachViewModels_MaNXB { get; set; }
    }
}