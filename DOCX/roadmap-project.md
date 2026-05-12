# NETMANAGER - REALTIME INTERNET CAFE MANAGEMENT SYSTEM

## 1. Tổng Quan Dự Án

**Môn học:** Lập trình mạng  
**Loại project:** Desktop application theo mô hình Client-Server  
**Thời gian thực hiện:** 8 tuần  
**Số lượng thành viên:** 6 người

NetManager là hệ thống quản lý quán net realtime, tập trung vào giao tiếp mạng nội bộ qua TCP Socket, xử lý nhiều client đồng thời và đồng bộ trạng thái máy theo thời gian thực.

### Mục tiêu chính

- Xây dựng một hệ thống demo ổn định cho môn Lập trình mạng.
- Quản lý máy client theo thời gian thực.
- Gửi thông báo, khóa/mở khóa máy, và quản lý session timer.
- Hỗ trợ chat cơ bản giữa admin và client.
- Ưu tiên độ ổn định, khả năng demo và tính dễ hiểu của code.

### Phạm vi ưu tiên

Tập trung vào các chức năng cốt lõi:

- TCP connection
- Multi-client
- Realtime machine status
- Notification
- Lock / unlock
- Session timer
- Basic chat
- Client authentication

Không mở rộng sang các tính năng nặng như:

- File transfer
- Voice call
- Video streaming
- Báo cáo phức tạp

---

## 2. Kiến Trúc Hệ Thống

### Mô hình tổng thể

```text
Admin Dashboard
      |
      | TCP Socket
      v
TCP Server -----------------> Client 1
      |                      -> Client 2
      |                      -> Client 3
      |
      +-- Quản lý session, trạng thái máy, thông báo, lock/unlock
```

### Thành phần chính

- **ServerApp**: xử lý kết nối, quản lý client, phát lệnh, lưu trạng thái.
- **ClientApp**: kết nối server, nhận lệnh, hiển thị trạng thái, chat, lock screen.
- **Shared**: packet model, constants, utility dùng chung.

---

## 3. Tech Stack

- **Ngôn ngữ:** C#
- **GUI:** Windows Forms
- **Networking:** TCP Socket, async/await, multithreading
- **Database:** SQLite
- **IDE:** Microsoft Visual Studio
- **Version control:** GitHub

---

## 4. Chức Năng Hệ Thống

### 4.1. Server Admin

#### Xác thực

- Đăng nhập / đăng xuất admin

#### Quản lý máy

- Hiển thị danh sách máy realtime
- Trạng thái: `Online`, `Offline`, `Locked`, `Playing`

#### Điều khiển realtime

- Gửi thông báo đến client
- Khóa / mở khóa máy
- Quản lý session timer
- Chat realtime với client

#### Giám sát

- Theo dõi trạng thái kết nối
- Cập nhật UI ngay khi client thay đổi trạng thái

### 4.2. Client App

#### Kết nối

- Nhập IP / Port server
- Kết nối và tự động xử lý reconnect cơ bản

#### Xác thực

- Đăng nhập client
- Nhận session và trạng thái từ server

#### Nhận lệnh realtime

- Nhận thông báo
- Nhận lock / unlock
- Nhận và hiển thị timer

#### Chat

- Gửi / nhận tin nhắn với admin

#### Lock screen

- Hiển thị màn hình khóa toàn màn hình khi server yêu cầu

---

## 5. Thiết Kế Packet

Giao tiếp giữa server và client dùng JSON packet đơn giản, dễ debug.

### Các packet chính

#### Login

```json
{
  "type": "LOGIN",
  "username": "client01",
  "password": "123456"
}
```

#### Chat

```json
{
  "type": "CHAT",
  "sender": "PC01",
  "message": "Máy em bị lag"
}
```

#### Lock

```json
{
  "type": "LOCK"
}
```

#### Unlock

```json
{
  "type": "UNLOCK"
}
```

#### Notification

```json
{
  "type": "NOTIFICATION",
  "message": "Server restart after 10 minutes"
}
```

#### Timer

```json
{
  "type": "TIMER",
  "remainingMinutes": 45
}
```

#### Status

```json
{
  "type": "STATUS",
  "machineId": "PC01",
  "state": "ONLINE"
}
```

---

## 6. Cấu Trúc Project

```text
NetManager/
├── ServerApp/
│   ├── Forms/
│   ├── Networking/
│   ├── Models/
│   ├── Services/
│   └── Database/
├── ClientApp/
│   ├── Forms/
│   ├── Networking/
│   ├── Models/
│   └── Services/
├── Shared/
│   ├── Packets/
│   ├── Constants/
│   └── Utilities/
├── Docs/
└── README.md
```

---

## 7. Kế Hoạch Phát Triển 8 Tuần

### Tuần 1 - Nền tảng mạng và khởi tạo dự án

**Mục tiêu**

- Tạo khung project và quy ước làm việc chung.
- Dựng TCP server/client cơ bản trên LAN.

**Công việc**

- Tạo repo GitHub và branch workflow.
- Tạo project `ServerApp`, `ClientApp`, `Shared`.
- Viết socket server và socket client cơ bản.
- Xây dựng cơ chế gửi / nhận JSON packet đơn giản.
- Kiểm tra kết nối 2-3 client cùng lúc.

**Kết quả cần đạt**

- Server chạy ổn định và nhận kết nối.
- Client gửi / nhận dữ liệu thành công.
- Có kết nối đa client cơ bản.

**Milestone:** `v0.1-network-base`

---

### Tuần 2 - Protocol giao tiếp và xác thực

**Mục tiêu**

- Chuẩn hóa packet JSON.
- Có flow đăng nhập cơ bản cho admin và client.

**Công việc**

- Định nghĩa các packet: `LOGIN`, `STATUS`, `LOCK`, `UNLOCK`, `NOTIFICATION`, `TIMER`, `CHAT`.
- Viết parser và handler cho server / client.
- Tạo flow login client.
- Thêm xác thực admin cơ bản.
- Đồng bộ trạng thái online / offline cho từng máy.

**Kết quả cần đạt**

- Login hoạt động và session được quản lý.
- Packet handler xử lý đúng các loại packet chính.
- Trạng thái client được đồng bộ.

**Milestone:** `v0.2-protocol-auth`

---

### Tuần 3 - GUI cơ bản cho server và client

**Mục tiêu**

- Hoàn thiện giao diện desktop cho cả hai phía.

**Công việc**

- Server: màn hình login admin, dashboard, danh sách máy, trạng thái.
- Client: màn hình kết nối, đăng nhập, hiển thị trạng thái, nhận thông báo.
- Gắn GUI vào networking layer.
- Đảm bảo UI không bị treo khi có sự kiện mạng.

**Kết quả cần đạt**

- Admin xem được danh sách máy realtime.
- Client kết nối, đăng nhập và nhận lệnh.
- Giao diện phản hồi tốt.

**Milestone:** `v0.3-gui-base`

---

### Tuần 4 - Realtime status và notification

**Mục tiêu**

- Làm cho dữ liệu máy cập nhật theo thời gian thực.

**Công việc**

- Đồng bộ trạng thái máy qua TCP / JSON.
- Hiển thị notification từ server lên client.
- Cập nhật trạng thái `Online`, `Offline`, `Locked`, `Playing`.
- Tối ưu refresh UI để tránh lag.

**Kết quả cần đạt**

- Server gửi thông báo và client hiển thị đúng.
- Dashboard cập nhật realtime.
- Ít nhất 3 client hoạt động ổn định cùng lúc.

**Milestone:** `v0.4-realtime-status`

---

### Tuần 5 - Lock / Unlock và Session Timer

**Mục tiêu**

- Hoàn thiện hai tính năng demo quan trọng nhất.

**Công việc**

- Tạo lệnh lock / unlock từ admin.
- Thiết kế màn hình khóa full-screen ở client.
- Đồng bộ session timer và thời gian còn lại.
- Kiểm tra tình huống mất kết nối, thoát app, reconnect.

**Kết quả cần đạt**

- Client bị khóa và mở khóa từ server.
- Timer chạy đúng và hiển thị rõ ràng.
- Xử lý mất kết nối an toàn.

**Milestone:** `v0.5-lock-system`

---

### Tuần 6 - Database nhỏ gọn và ổn định hóa

**Mục tiêu**

- Tích hợp SQLite cho xác thực và lưu cấu hình.

**Công việc**

- Lưu tài khoản admin / client vào SQLite.
- Xác thực login bằng database.
- Lưu session và thông tin cấu hình cần thiết.
- Thêm logging lỗi đơn giản để dễ debug.
- Bổ sung reconnect / retry cơ bản cho client.

**Kết quả cần đạt**

- Login dựa trên database hoạt động.
- Ứng dụng chịu được lỗi mạng nhẹ.
- Hệ thống ổn định hơn khi demo.

**Milestone:** `v0.6-database-auth`

---

### Tuần 7 - Testing và sửa lỗi

**Mục tiêu**

- Dồn lực kiểm thử và fix bug trước ngày trình diễn.

**Công việc**

- Test multi-client, lock / unlock, timer, notification, chat.
- Kiểm tra mất kết nối, timeout, logout.
- Làm sạch UI và thông báo lỗi thân thiện hơn.
- Chuẩn bị checklist demo.

**Kết quả cần đạt**

- Flow demo chính chạy ổn định.
- Không crash khi dùng chức năng cốt lõi.
- Có checklist sẵn sàng demo.

**Milestone:** `v0.9-demo-ready`

---

### Tuần 8 - Hoàn thiện và trình diễn

**Mục tiêu**

- Chốt bản demo cuối cùng.

**Công việc**

- Hoàn thiện slide và kịch bản trình bày.
- Test lần cuối trên máy thật.
- Sửa các lỗi nhỏ còn sót.
- Xác nhận luồng demo và phân vai trình bày.

**Kết quả cần đạt**

- Hệ thống chạy mượt trong kịch bản demo.
- Có bản build hoặc project sẵn sàng trình diễn.
- Nhóm nắm rõ vai trò và thứ tự demo.

**Milestone:** `v1.0-final-release`

---

## 8. Phân Công Thành Viên

### Member 1 - Team Leader / System Architect

**Nhiệm vụ**

- Điều phối tiến độ theo tuần.
- Thiết kế tổng thể kiến trúc.
- Review code chính và hỗ trợ tích hợp.
- Giải quyết vấn đề khi networking hoặc GUI bị nghẽn.

**Deliverables**

- Roadmap 8 tuần.
- Kiến trúc `ServerApp` / `ClientApp` / `Shared`.
- Bản build demo ổn định.

### Member 2 - Network Engineer

**Nhiệm vụ**

- Xây dựng TCP server và TCP client.
- Quản lý multi-client.
- Thiết kế packet handler và parser.
- Xử lý reconnect, timeout và lỗi socket.

**Deliverables**

- Socket server/client hoạt động.
- Packet dispatcher và handler.
- Reconnect cơ bản.

### Member 3 - Server GUI Developer

**Nhiệm vụ**

- Xây dựng dashboard admin desktop.
- Hiển thị danh sách máy và trạng thái realtime.
- Triển khai lock/unlock, notification, timer.
- Tối ưu UI để tránh lag.

**Deliverables**

- Server UI cho demo.
- Control panel lock/unlock/notify.
- Machine list realtime.

### Member 4 - Client App Developer

**Nhiệm vụ**

- Xây dựng client desktop app.
- Kết nối IP/Port và xác thực client.
- Hiển thị thông báo, trạng thái lock, timer.
- Tạo màn hình khóa full-screen.

**Deliverables**

- Client app kết nối server.
- Màn hình lock/unlock.
- Realtime notification và timer.

### Member 5 - Database & Authentication

**Nhiệm vụ**

- Tích hợp SQLite cho quản lý tài khoản.
- Xây dựng login validation và lưu session.
- Hỗ trợ lưu cấu hình đơn giản.

**Deliverables**

- Database SQLite cơ bản.
- Hệ thống login admin/client.
- Lưu trữ session và cấu hình.

### Member 6 - Tester & Documentation

**Nhiệm vụ**

- Test multi-client, lock/timer/chat.
- Ghi nhận và phân loại bug.
- Viết README ngắn gọn và checklist demo.
- Hỗ trợ vận hành trong ngày demo.

**Deliverables**

- Test report.
- Danh sách lỗi và checklist.
- Tài liệu hướng dẫn sử dụng.

---

## 9. GitHub Workflow

### Branch structure

```text
main
develop
feature/socket
feature/gui
feature/chat
feature/database
feature/lock-system
```

### Luồng làm việc

```text
feature/* -> develop -> main
```

### Quy ước commit

Ví dụ:

- `[NETWORK] Add TCP server`
- `[GUI] Add dashboard form`
- `[DATABASE] Add SQLite login`
- `[CHAT] Implement realtime chat`

---

## 10. Công Cụ Quản Lý

- **Source control:** GitHub
- **Project management:** GitHub Projects
- **Communication:** Discord / Messenger / Zalo

---

## 11. Rủi Ro Dự Án

- Đồng bộ mạng không ổn định
- Xung đột thread
- Mất kết nối socket
- Packet loss
- GUI bị treo do xử lý blocking
- Phạm vi quá lớn so với 8 tuần
- Thiếu tích hợp sớm giữa các module

---

## 12. Giải Pháp Kiểm Soát Rủi Ro

- Chia milestone rõ ràng, hoàn thành từng giai đoạn.
- Ưu tiên networking trước GUI.
- Họp nhóm mỗi tuần để review tiến độ.
- Commit thường xuyên để giảm conflict.
- Test tích hợp sớm ngay khi có module đầu tiên.

---

## 13. Ưu Tiên Chức Năng

### High priority

- TCP connection
- Multi-client
- Realtime machine list
- Notification
- Lock / unlock
- Session timer

### Medium priority

- Chat system
- Logging system
- UI optimization

### Low priority

- File transfer
- Voice call
- Video streaming

---

## 14. Kết Luận

NetManager là một project phù hợp với môn Lập trình mạng vì có đầy đủ các yếu tố cốt lõi:

- Mô hình client-server
- Giao tiếp TCP socket
- Realtime communication
- Multithreading
- Desktop GUI

Mục tiêu của roadmap này là giúp nhóm tập trung vào phần cốt lõi, hoàn thiện bản demo ổn định và tránh mở rộng sang các tính năng không cần thiết trong giai đoạn học phần.
