using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSachCLKBooks.ViewModels
{
    public class TacGiaViewModels
    {

        public int MaTG { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Không quá 50 ký tự")]
        public String TenTG { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Không quá 100 ký tự")]
        public String DiaChi { get; set; }

        [Required]
        public String TieuSu { get; set; }

        [Required]
        [MaxLength(11)]
        [MinLength(8)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
        public String DienThoai { get; set; }

    }
}