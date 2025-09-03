using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThanhToanTienNuoc.Models;

namespace ThanhToanTienNuoc.Models
{
    [Table("NguoiDung")]    
    public class NguoiDung
    {
        [Key]
        [MaxLength(20)]
        [Display(Name = "Mã khách hàng")]
        public string MaKhachHang { get; set; }

        [Required(ErrorMessage = "Họ tên không được bỏ trống")]
        [MaxLength(100)]
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; }

        [MaxLength(20)]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [ForeignKey("DiaChi")]
        [Display(Name = "Mã địa chỉ")]
        public int DiaChiId { get; set; }

        public DiaChi DiaChi { get; set; }

        public ICollection<ChiSoNuoc> ChiSoNuocs { get; set; }
    }
}
