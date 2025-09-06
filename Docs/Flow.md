## FLOW
A. Luồng UI cho Admin (Người Quản Lý)
Luồng này tập trung vào hiệu quả công việc và khả năng quản lý toàn diện.

1. Màn hình: Đăng nhập

Mục đích: Xác thực quyền truy cập của người quản lý.

Thành phần chính:

Ô nhập "Tên đăng nhập" / "Email".

Ô nhập "Mật khẩu".

Nút "Đăng nhập".

Hành động:

Đăng nhập thành công -> Chuyển đến màn hình Bảng điều khiển.

Đăng nhập thất bại -> Hiển thị thông báo lỗi.

2. Màn hình: Bảng điều khiển (Dashboard)

Mục đích: Cung cấp cái nhìn tổng quan nhanh về tình hình hoạt động ngay sau khi đăng nhập.

Thành phần chính:

Menu điều hướng chính (Sidebar hoặc Header): Dẫn đến các mục Quản lý Hộ dân, Hóa đơn & Ghi số, Báo cáo, Thông báo.

Các số liệu thống kê nhanh: Số hộ chưa thanh toán, Tổng doanh thu kỳ này, Tổng sản lượng nước tiêu thụ.

Lối tắt (Quick Actions): Nút "Nhập chỉ số nước kỳ mới", Nút "Xem danh sách hộ nợ cước".

Hành động:

Nhấp vào các mục trên Menu để di chuyển đến các màn hình chức năng tương ứng.

Nhấp vào các lối tắt để thực hiện nhanh các tác vụ phổ biến.

3. Màn hình: Quản lý Hộ dân

Mục đích: Xem, tìm kiếm, thêm mới và quản lý danh sách tất cả các hộ dân.

Thành phần chính:

Ô tìm kiếm (theo tên, mã khách hàng, số điện thoại).

Danh sách các hộ dân hiển thị dưới dạng bảng (gồm các cột: Mã KH, Tên chủ hộ, Địa chỉ, SĐT).

Nút "Thêm hộ dân mới".

Hành động:

Nhập để tìm kiếm và lọc danh sách.

Nhấp vào một hộ dân trong danh sách -> Chuyển đến màn hình Chi tiết Hộ dân.

Nhấp nút "Thêm hộ dân mới" -> Mở form/trang để nhập thông tin hộ mới.

4. Màn hình: Chi tiết Hộ dân

Mục đích: Xem toàn bộ thông tin và lịch sử của một hộ dân cụ thể, đồng thời thực hiện các thao tác riêng cho hộ đó.

Thành phần chính:

Thông tin cố định của hộ (Tên, địa chỉ, SĐT...).

Lịch sử ghi số và thanh toán qua các tháng.

Các nút hành động: "Sửa thông tin", "Cập nhật thanh toán", "Nhập chỉ số mới cho hộ này".

Hành động: Thực hiện các tác vụ chỉnh sửa, cập nhật cho riêng hộ dân đang xem.

5. Màn hình: Hóa đơn & Ghi số

Mục đích: Ghi chỉ số công tơ nước hàng loạt cho tất cả các hộ vào đầu kỳ.

Thành phần chính:

Bộ lọc chọn Kỳ (Tháng/Năm) để nhập liệu.

Ô nhập "Đơn giá nước" áp dụng cho kỳ này.

Danh sách tất cả các hộ dân với các cột: Mã KH, Tên chủ hộ, Chỉ số cũ (tự động điền), ô nhập Chỉ số mới.

Nút "Lưu và Tạo hóa đơn".

Hành động: Admin điền chỉ số mới cho từng hộ, sau đó bấm nút để hệ thống tự động tính toán và sinh hóa đơn cho cả xã.

B. Luồng UI cho User (Người Dân Tra Cứu)
Luồng này được tối ưu hóa cho sự đơn giản tuyệt đối: Truy cập -> Nhập mã -> Xem kết quả.

1. Màn hình: Trang chủ Tra cứu

Mục đích: Cung cấp một lối vào duy nhất và đơn giản nhất để người dân tìm thông tin.

Thành phần chính:

Tiêu đề lớn, rõ ràng (ví dụ: "TRA CỨU THÔNG TIN NƯỚC SẠCH").

Một ô nhập liệu duy nhất: "Vui lòng nhập Mã khách hàng (hoặc Số điện thoại)".

Một nút bấm lớn: "XEM".

(Tùy chọn) Thông báo chung nổi bật nếu có.

Hành động: Người dân nhập mã số của mình và bấm "XEM" -> Chuyển đến màn hình Kết quả chi tiết.

2. Màn hình: Kết quả chi tiết

Mục đích: Hiển thị tất cả thông tin người dân cần trên một trang duy nhất, không cần chuyển trang hay bấm thêm.

Thành phần chính (sắp xếp từ trên xuống dưới):

Phần 1: Thông tin chủ hộ: Tên, địa chỉ, mã khách hàng.

Phần 2: Hóa đơn kỳ này (Nổi bật nhất):

Trạng thái thanh toán (to, rõ, có màu sắc. VD: "CHƯA THANH TOÁN").

Chỉ số cũ - Chỉ số mới.

Lượng nước đã dùng (m³).

Đơn giá (VNĐ/m³).

Tổng tiền phải trả.

Phần 3: Lịch sử các tháng trước:

Một danh sách các tháng cũ hơn, mỗi tháng là một mục riêng.

Mỗi mục hiển thị đầy đủ: Chỉ số cũ, Chỉ số mới, Lượng nước dùng, Đơn giá của tháng đó, và Số tiền đã đóng.

Phần 4: Thông tin liên hệ hỗ trợ: Số điện thoại người cần liên hệ khi có thắc mắc.

Hành động: Chỉ có hành động duy nhất là đọc và nắm thông tin. Không có nút bấm hay tương tác phức tạp nào khác.