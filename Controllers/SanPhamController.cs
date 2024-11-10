using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using YourProjectName.Models;  // Chú ý thay thế bằng namespace dự án của bạn

public class SanPhamController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();  // Sử dụng context kết nối đến cơ sở dữ liệu

    // Hiển thị danh sách tất cả sản phẩm
    public ActionResult Index()
    {
        var sanPhams = db.SanPhams.ToList();
        return View(sanPhams);  // Trả về view hiển thị danh sách sản phẩm
    }

    // Hiển thị form thêm sản phẩm
    public ActionResult Create()
    {
        return View();
    }

    // Lưu sản phẩm mới vào cơ sở dữ liệu
    [HttpPost]
    public ActionResult Create(SanPham sanPham)
    {
        if (ModelState.IsValid)  // Kiểm tra tính hợp lệ của dữ liệu nhập vào
        {
            db.SanPhams.Add(sanPham);  // Thêm sản phẩm vào context
            db.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            return RedirectToAction("Index");  // Quay lại trang danh sách sản phẩm
        }
        return View(sanPham);  // Nếu không hợp lệ, hiển thị lại form nhập sản phẩm
    }

    // Hiển thị form sửa sản phẩm
    public ActionResult Edit(int id)
    {
        var sanPham = db.SanPhams.Find(id);
        if (sanPham == null)
        {
            return HttpNotFound();  // Nếu không tìm thấy sản phẩm
        }
        return View(sanPham);  // Trả về view để chỉnh sửa sản phẩm
    }

    // Cập nhật sản phẩm trong cơ sở dữ liệu
    [HttpPost]
    public ActionResult Edit(SanPham sanPham)
    {
        if (ModelState.IsValid)  // Kiểm tra tính hợp lệ của dữ liệu
        {
            db.Entry(sanPham).State = EntityState.Modified;  // Đánh dấu trạng thái sửa đổi
            db.SaveChanges();  // Lưu thay đổi
            return RedirectToAction("Index");  // Quay lại danh sách sản phẩm
        }
        return View(sanPham);  // Nếu không hợp lệ, hiển thị lại form sửa
    }

    // Hiển thị trang xác nhận xóa sản phẩm
    public ActionResult Delete(int id)
    {
        var sanPham = db.SanPhams.Find(id);
        if (sanPham == null)
        {
            return HttpNotFound();  // Nếu không tìm thấy sản phẩm
        }
        return View(sanPham);  // Trả về view xác nhận xóa sản phẩm
    }

    // Xóa sản phẩm khỏi cơ sở dữ liệu
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var sanPham = db.SanPhams.Find(id);
        db.SanPhams.Remove(sanPham);  // Xóa sản phẩm
        db.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
        return RedirectToAction("Index");  // Quay lại danh sách sản phẩm
    }
}
