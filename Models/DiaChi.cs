using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThanhToanTienNuoc.Models
{
    [Table("DiaChi")]
    public class DiaChi
    {
        [Key]
        public int DiaChiId { get; set; }

        [Required]
        [Display(Name = "Xóm")]
        public string TenXom { get; set; }

        [Display(Name = "Thôn")]
        public string TenThon { get; set; }


        // Mối quan hệ: Một địa chỉ có thể gán cho nhiều người dùng
        public ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
