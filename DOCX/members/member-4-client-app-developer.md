# Member 4 - Client App Developer

## 1. Vai tro

Ban phu trach ung dung client. Client phai ket noi duoc voi server, nhan lenh realtime, hien lock screen khi can, va giu UI on dinh.

## 2. Muc tieu cuoi cung

- Client connect va login duoc.
- Client nhan notification, timer, lock, unlock, chat.
- Client co lock screen full-screen.
- Client co xu ly disconnect va reconnect co ban.

## 3. Viec can lam tu dau den cuoi

### Giai doan khoi tao

- Tao connect form.
- Tao login flow client.
- Tao client state model.
- Chot cach luu IP/Port neu can.

### Giai doan connection flow

- Kiem tra ket noi den server.
- Gui login packet.
- Hien trang thai ket noi va auth ro rang.
- Xu ly loi connect khong lam crash app.

### Giai doan realtime receive

- Nhan notification tu server.
- Nhan timer update.
- Nhan lock/unlock command.
- Nhan chat message.
- Nhan status hoac session update neu co.

### Giai doan lock screen

- Tao man hinh khoa full-screen.
- Chan thao tac thoat de dang.
- Hien thong diep ro rang.
- Chi mo khoa khi nhan `UNLOCK`.

### Giai doan chat va status

- Hien chat voi admin.
- Gui message tu client.
- Cap nhat status khi co thay doi.
- Dam bao flow chat khong lam tre UI.

### Giai doan resilience

- Bao cho nguoi dung khi server mat ket noi.
- Thu reconnect co ban.
- Giu local state khong bi vo khi disconnect.
- Khong de packet xau lam crash client.

### Giai doan polish

- Don dep form, labels, va state feedback.
- Uu tien readability hon hieu ung phuc tap.
- Giup nguoi dung biet dang o trang thai nao.

## 4. Checklist cong viec theo phase

### Phase 1 - Connect and login

- [ ] Tao connect form
- [ ] Tao login form
- [ ] Tao connect state
- [ ] Tao auth result display

### Phase 2 - Receive commands

- [ ] Nhan notification
- [ ] Nhan lock/unlock
- [ ] Nhan timer
- [ ] Nhan chat

### Phase 3 - Lock screen

- [ ] Tao fullscreen lock form
- [ ] Chan thao tac khong dung
- [ ] Hien unlock state

### Phase 4 - Stability

- [ ] Xu ly disconnect
- [ ] Xu ly reconnect
- [ ] Xu ly invalid packet
- [ ] Xu ly network error an toan

### Phase 5 - Demo readiness

- [ ] Test ket noi on dinh
- [ ] Test lock/unlock
- [ ] Test notification/timer/chat
- [ ] Test UI khong freeze

## 5. Nguyen tac code

- Tach networking service khoi form.
- UI chi lam viec voi state va event.
- Khong parse packet truc tiep trong event handler UI neu co the avoid.
- Khong de lock flow bi phu thuoc vao button state alone.

## 6. Deliverables can nop

- Connect form.
- Client login flow.
- Main client UI.
- Full-screen lock screen.
- Notification, timer, chat handling.

## 7. Definition of Done

- Client ket noi va nhan lenh on dinh.
- Lock screen chay dung.
- UI khong block.
- Client demo doc lap duoc va hop voi server.
