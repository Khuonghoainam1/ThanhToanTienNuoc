using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThanhToanTienNuoc.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceKhachHangWithNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminAccounts",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAccounts", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "DiaChi",
                columns: table => new
                {
                    DiaChiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenXom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenThon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChi", x => x.DiaChiId);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    MaKhachHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiaChiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "FK_NguoiDung_DiaChi_DiaChiId",
                        column: x => x.DiaChiId,
                        principalTable: "DiaChi",
                        principalColumn: "DiaChiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiSoNuoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Quy = table.Column<int>(type: "int", nullable: false),
                    ChiSoCu = table.Column<float>(type: "real", nullable: false),
                    ChiSoMoi = table.Column<float>(type: "real", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThongBaoZalo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiSoNuoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiSoNuoc_NguoiDung_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiSoNuoc_KhachHangId",
                table: "ChiSoNuoc",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_DiaChiId",
                table: "NguoiDungs",
                column: "DiaChiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAccounts");

            migrationBuilder.DropTable(
                name: "ChiSoNuoc");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropTable(
                name: "DiaChi");
        }
    }
}
