using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSachCLKBooks.ViewModels
{
    public class ChuDeViewModels
    {

        public int MaCD { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Không quá 50 ký tự")]
        public String TenChuDe { get; set; }

    }
}