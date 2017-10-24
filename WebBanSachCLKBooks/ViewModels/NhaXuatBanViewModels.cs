using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSachCLKBooks.ViewModels
{
    public class NhaXuatBanViewModels
    {

        public int MaNXB { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Không quá 50 ký tự")]
        public String TenNXB { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Không quá 200 ký tự")]
        public String DiaChi { get; set; }

        [Required]
        [MaxLength(11)]
        [MinLength(8)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
        public String DienThoai { get; set; }

    }
}