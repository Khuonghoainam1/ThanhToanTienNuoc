using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThanhToanTienNuoc.Models
{
    [Table("ChiSoNuoc")]
    public class ChiSoNuoc
    {
        [Key]
        [Column("ChiSoId")]
        public int ChiSoId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("MaKhachHang")]
        public string MaKhachHang { get; set; }

        [ForeignKey("MaKhachHang")]
        public NguoiDung NguoiDung { get; set; }

        [Column("Nam")]
        public int Nam { get; set; }

        [Column("Quy")]
        public int Quy { get; set; }

        [Column("ChiSoCu")]
        public int ChiSoCu { get; set; }

        [Column("ChiSoMoi")]
        public int ChiSoMoi { get; set; }

        [Column("SoThucDung")]
        public int SoThucDung { get; set; }

        [Column("ThanhTien", TypeName = "decimal(18,2)")]
        public decimal ThanhTien { get; set; }

        [Column("TrangThaiThanhToan")]
        public bool TrangThaiThanhToan { get; set; }

        [Column("HinhThucThanhToan")]
        [StringLength(50)]
        public string HinhThucThanhToan { get; set; }

        [Column("NgayThanhToan")]
        public DateTime? NgayThanhToan { get; set; }

        [Column("ThongBaoZalo")]
        public bool ThongBaoZalo { get; set; }
    }
}
