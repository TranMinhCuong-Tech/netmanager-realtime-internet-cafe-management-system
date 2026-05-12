# Member 3 - Server GUI Developer

## 1. Vai tro

Ban phu trach giao dien admin dashboard cua server. UI phai ro rang, realtime, de thao tac, va khong tre khi network co nhieu event.

## 2. Muc tieu cuoi cung

- Admin co the dang nhap va vao dashboard.
- Danh sach may cap nhat realtime.
- Lock, unlock, notification, timer, chat co the thao tac duoc.
- UI khong freeze khi co socket event.

## 3. Viec can lam tu dau den cuoi

### Giai doan khoi tao

- Tao khung dashboard.
- Tao layout header, content, control panel.
- Chot control naming va style naming.
- Tao form login admin neu can.

### Giai doan machine view

- Hien thi danh sach may.
- Hien thi trang thai online, offline, locked, playing.
- Hien thi IP, machine name, timer, connection state neu co.
- Tao refresh pattern cho machine list.

### Giai doan control flow

- Tao nut lock, unlock, notification, timer.
- Tao area xem chat neu co.
- Tao hanh dong admin ro rang, khong lam loan UI.
- Mapping button event sang network service.

### Giai doan realtime

- Nhan update tu network service.
- Invoke ve UI thread dung cach.
- Cap nhat mau sac, icon, badge, label theo state.
- Kiem tra khong co deadlock hay cross-thread error.

### Giai doan polish

- Don dep spacing, typography, va visual hierarchy.
- Dam bao dashboard de demo.
- Giam noise UI, tang readability.
- Thiet lap empty state va error state ro rang.

### Giai doan integration

- Ket noi voi socket layer.
- Ket noi voi auth layer neu dashboard co login.
- Ket noi voi shared packet models.
- Test voi client lock/unlock/timer/notify.

### Giai doan release

- Kiem tra dashboard chay on dinh khi co nhieu event.
- Kiem tra refresh lien tuc khong lam lag.
- Kiem tra admin action gui ra dung packet.
- Kiem tra UI sau reconnect va after disconnect.

## 4. Checklist cong viec theo phase

### Phase 1 - Layout

- [ ] Tao dashboard shell
- [ ] Tao login form
- [ ] Tao machine list panel
- [ ] Tao control panel

### Phase 2 - State display

- [ ] Hien machine status
- [ ] Hien connection state
- [ ] Hien timer state
- [ ] Hien notification area

### Phase 3 - Actions

- [ ] Gui lock command
- [ ] Gui unlock command
- [ ] Gui notification
- [ ] Gui timer command

### Phase 4 - Realtime UI

- [ ] Listen network updates
- [ ] Update UI thread safely
- [ ] Refesh machine cards
- [ ] Handle offline state

### Phase 5 - Demo readiness

- [ ] Polish visuals
- [ ] Test multi-client updates
- [ ] Remove UI clutter
- [ ] Validate interaction speed

## 5. Hanh vi UI can dam bao

- Khong block khi dang nhan packet.
- Khong update truc tiep tu background thread.
- Khong de button action gay trung lap packet.
- Khong de dashboard mat trang thai khi socket reconnect.

## 6. Deliverables can nop

- Admin login form.
- Realtime dashboard.
- Machine list/state view.
- Control panel cho lock/unlock/notification/timer.
- UI integration voi network service.

## 7. Definition of Done

- Admin thao tac on dinh.
- Machine state nhin thay ngay.
- UI khong freeze.
- Dashboard demo duoc ma khong can giai thich nhieu.
