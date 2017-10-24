using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanSachCLKBooks.Models;

namespace WebBanSachCLKBooks.ViewModels
{
    public class VietSachViewModels
    {

        // 2 dòng này select dữ liệu ra để đưa vào combo box
        public IEnumerable<SP_SELECT_SACHResult> VietSach_Sach { get; set; }
        public IEnumerable<SP_SELECT_TACGIAResult> VietSach_TacGia { get; set; }

        // dùng để lấy ra mã tác giả và mã sách (trong hàm hidden for) để biết viết sách nào mà edit  
        // còn selectedMaTacGia là nội dung mới muốn edit , do khách hàng chọn cái mới nên là selected 
        // có thể hiểu là NEW Selected MaTacGia
        public int MaTG_HienTai { get; set; }
        public int MaSach_HienTai { get; set; }

        // Các thuộc tính trong bảng Viết sách
        public int SelectedMaTacGia { get; set; }
        public String SelectedTenTacGia { get; set; }

        public int SelectedMaSach { get; set; }
        public String SelectedTenSach { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Không quá 50 ký tự")]
        public String VaiTro { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Không quá 50 ký tự")]
        public String ViTri { get; set; }
    }
}