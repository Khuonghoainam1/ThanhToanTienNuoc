using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq;
using ThanhToanTienNuoc.Models;
using ThanhToanTienNuoc.Data;
namespace ThanhToanTienNuoc.Controllers;


public class NguoiDungController : Controller
{
    private readonly AppDbContext _context;

    public NguoiDungController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Danh sách khách hàng
    public IActionResult Index(string maKhachHang, string tenKhachHang, string tenXom)
    {
        var query = _context.NguoiDungs
            .Include(nd => nd.DiaChi)
            .AsQueryable();

        if (!string.IsNullOrEmpty(maKhachHang))
        {
            query = query.Where(nd => nd.MaKhachHang.Contains(maKhachHang));
        }

        if (!string.IsNullOrEmpty(tenKhachHang))
        {
            query = query.Where(nd => nd.HoTen.Contains(tenKhachHang));
        }

        if (!string.IsNullOrEmpty(tenXom))
        {
            query = query.Where(nd => nd.DiaChi != null && nd.DiaChi.TenXom.Contains(tenXom));
        }

        ViewBag.MaKhachHang = maKhachHang;
        ViewBag.TenKhachHang = tenKhachHang;
        ViewBag.TenXom = tenXom;
        ViewBag.DiaChiList = _context.DiaChis.ToList();

        var listNguoiDung = query.ToList();
        return View(listNguoiDung);
    }

    public IActionResult Create()
    {
        ViewBag.DiaChiList = _context.DiaChis.ToList();
        var listNguoiDung = _context.NguoiDungs.ToList();
        return View();
    }
    // POST: Thêm khách hàng
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NguoiDung model)
    {
        if (!ModelState.IsValid)
        {
            _context.NguoiDungs.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Load lại dữ liệu cho view
        ViewBag.DiaChiList = _context.DiaChis.ToList();
        var listNguoiDung = _context.NguoiDungs.ToList();

        return View("Index", listNguoiDung);
    }


    // GET: Sửa khách hàng
    public IActionResult Edit(int id)
    {
        var nguoiDung = _context.NguoiDungs.Find(id);
        if (nguoiDung == null) return NotFound();
        return View(nguoiDung);
    }



    [HttpGet]
    public IActionResult Edit(string id)
    {
        if (id == null) return NotFound();

        var nguoiDung = _context.NguoiDungs
            .Include(n => n.DiaChi)
            .FirstOrDefault(x => x.MaKhachHang == id);

        var chiSoNuoc = _context.ChiSoNuocs
            .FirstOrDefault(c => c.MaKhachHang == id);

        if (nguoiDung == null) return NotFound();

        ViewBag.ChiSoNuoc = chiSoNuoc;
        ViewBag.DiaChiList = _context.DiaChis.OrderBy(x => x.TenXom).ToList();
        ViewBag.DonGia = 7000M; // ví dụ, có thể đọc từ cấu hình

        return View(nguoiDung);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(
      NguoiDung model,
      [Bind(Prefix = "ChiSoNuoc")] ChiSoNuoc chiSoNuoc,
      decimal donGia)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ChiSoNuoc = chiSoNuoc;
            ViewBag.DiaChiList = _context.DiaChis.ToList();
            ViewBag.DonGia = donGia;
            return View(model);
        }

        var existingNguoiDung = _context.NguoiDungs.FirstOrDefault(x => x.MaKhachHang == model.MaKhachHang);
        if (existingNguoiDung == null) return NotFound();

        // Cập nhật thông tin khách hàng
        existingNguoiDung.HoTen = model.HoTen;
        existingNguoiDung.SoDienThoai = model.SoDienThoai;
        existingNguoiDung.DiaChiId = model.DiaChiId;

        // Tính số thực dùng và thành tiền
        chiSoNuoc.SoThucDung = Math.Max(chiSoNuoc.ChiSoMoi - chiSoNuoc.ChiSoCu, 0);
        chiSoNuoc.ThanhTien = chiSoNuoc.SoThucDung * donGia;

        // Kiểm tra chỉ số cũ có chưa
        var existingChiSo = _context.ChiSoNuocs
            .FirstOrDefault(x => x.MaKhachHang == model.MaKhachHang && x.Nam == chiSoNuoc.Nam && x.Quy == chiSoNuoc.Quy);

        if (existingChiSo != null)
        {
            existingChiSo.ChiSoCu = chiSoNuoc.ChiSoCu;
            existingChiSo.ChiSoMoi = chiSoNuoc.ChiSoMoi;
            existingChiSo.SoThucDung = chiSoNuoc.SoThucDung;
            existingChiSo.ThanhTien = chiSoNuoc.ThanhTien;
        }
        else
        {
            chiSoNuoc.MaKhachHang = model.MaKhachHang;
            _context.ChiSoNuocs.Add(chiSoNuoc);
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    // GET: Xóa khách hàng
    public IActionResult Delete(int id)
    {
        var nguoiDung = _context.NguoiDungs.Find(id);
        if (nguoiDung == null) return NotFound();
        return View(nguoiDung);
    }

    // POST: Xác nhận xóa
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var nguoiDung = _context.NguoiDungs.Find(id);
        if (nguoiDung != null)
        {
            _context.NguoiDungs.Remove(nguoiDung);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }



    // Lưu dữ liệu vào DB
    [HttpPost]
    public IActionResult SaveFromPreview(List<NguoiDungImport> data)
    {
        if (data != null)
        {
            foreach (var item in data)
            {
                var nguoiDung = new NguoiDung
                {
                    MaKhachHang = item.MaKhachHang,
                    HoTen = item.HoTen,
                    SoDienThoai = item.SoDienThoai,
                    DiaChiId = GetDiaChiIdFromTenXom(item.TenXom) // Hàm tự viết để lấy ID từ tên xóm
                };
                _context.NguoiDungs.Add(nguoiDung);
            }
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    public IActionResult TimKiem(string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
        {
            return View(null);
        }

        var nguoiDung = _context.NguoiDungs
            .FirstOrDefault(u => u.MaKhachHang == keyword || u.SoDienThoai == keyword);

        if (nguoiDung == null)
        {
            return View(null);
        }

        var chiSoList = _context.ChiSoNuocs
            .Where(c => c.MaKhachHang == nguoiDung.MaKhachHang)
            .OrderByDescending(c => c.Nam)
            .ThenByDescending(c => c.Quy)
            .ToList();

        ViewBag.NguoiDung = nguoiDung;
        return View(chiSoList);
    }

    private int GetDiaChiIdFromTenXom(string tenXom)
    {
        var diaChi = _context.DiaChis.FirstOrDefault(x => x.TenXom == tenXom);
        return diaChi != null ? diaChi.DiaChiId : 0;
    }


}
