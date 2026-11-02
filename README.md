# Dude Theft Wars: 3D Dev Sandbox Edition

Một bản sandbox 3D mã nguồn mở phong cách thế giới mở Low-Poly của Dude Theft Wars, nhẹ mượt, chạy trực tiếp trên trình duyệt hoặc đóng gói thành file APK.

---

## 🌟 Tính năng nổi bật

1. **Thành phố 3D đầy đủ công trình lớn:**
   - 🏢 **Tòa Tháp Empire Skyscraper**: Cao 75 mét với sân bay trực thăng Helipad trên nóc.
   - 🚔 **Đồn Cảnh Sát (Police Station HQ)**: Đầy đủ đèn hiệu siren, bãi đỗ xe tuần tra.
   - 🔫 **Cửa Hàng Vũ Khí Ammu-Nation**: Biển hiệu neon đỏ và súng biểu tượng.
   - ⛽ **Trạm Xăng Octan**: Cây xăng, mái che, cửa hàng tiện lợi.
   - 🏠 **Biệt Thự Dude (Dude Villa)**: Biệt thự sang trọng với hồ bơi trong xanh.
   - 🏡 **Khu Đô Thị Nhà Dân (Suburban Houses)**: Dãy nhà nhiều màu sắc.
   - 🌳 **Cây cối & Đèn đường**: Hệ thống công viên cảnh quan.

2. **Cơ chế Lái Xe & Chiến Đấu:**
   - 🚗 Lên/xuống siêu xe thể thao, xe cảnh sát, xe bán pickup bằng phím `F` hoặc nút bấm trên màn hình.
   - 🔫 Bắn súng có hiệu ứng âm thanh Synthesizer tổng hợp chân thực (không cần mạng).
   - 👁️ Đổi góc nhìn: Thứ 3 (TPS) hoặc Thứ nhất (FPS) bằng phím `C`.

3. **⚡ ADMIN DEV PANEL BÍ MẬT (Kích hoạt khi đổi tên thành `dev`):**
   - Đổi tên nhân vật thành **`dev`** (hoặc `DEV`) -> Âm thanh Fanfare vang lên và mở khóa bảng điều khiển tối thượng:
     - **God Mode (Bất tử)**
     - **Cộng ngay +$1,000,000**
     - **Tốc độ chạy x3 (Super Speed)**
     - **Siêu Nhảy (Super Jump)**
     - **Thả Siêu Xe / Xe Cảnh Sát ngay trước mặt**
     - **Dịch chuyển tức thời (Teleport)** tới tất cả công trình (Đồn cảnh sát, Trạm xăng, Nóc tháp Empire...)
     - **Chuyển Ngày / Đêm tức thì**
     - **Trọng lực mặt trăng (Moon Gravity)** lơ lửng
     - **Tùy chỉnh độ truy nã (0 Sao -> 5 Sao)**

---

## 🎮 Cách chơi & Phím bấm
* **WASD / Phím mũi tên**: Di chuyển
* **Chuột**: Xoay góc nhìn / Click chuột trái để bắn
* **SPACE**: Nhảy
* **SHIFT**: Chạy nhanh
* **F**: Lên xe / Xuống xe
* **C**: Đổi góc nhìn (TPS / FPS)
* **P**: Bật/Tắt nhanh Admin Panel (khi đã mở khóa tên `dev`)

---

## 📱 Cách đóng gói thành file APK Android
Vì đây là Web Game HTML5/Three.js thuần túy, bạn có thể dễ dàng build ra APK bằng **Capacitor**:
```bash
npm init @capacitor/app
npx cap add android
npx cap run android
```
Hoặc đưa lên GitHub Pages để chơi trực tiếp trên điện thoại Android/iOS qua trình duyệt!
