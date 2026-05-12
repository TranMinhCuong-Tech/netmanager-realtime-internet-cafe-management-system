# Member 2 - Network Engineer

## 1. Vai tro

Ban phu trach toan bo lop networking. Nhiem vu cua ban la giu ket noi TCP on dinh, packet ro rang, multi-client chay duoc, va khong lam tre UI.

## 2. Muc tieu cuoi cung

- Server va client ket noi duoc qua TCP.
- Packet JSON duoc gui nhan on dinh.
- Multi-client hoat dong song song.
- Disconnect, timeout, reconnect co xu ly an toan.
- Cac module khac co the dung network API ma khong phai doan.

## 3. Viec can lam tu dau den cuoi

### Giai doan khoi tao

- Xac dinh port, IP, va mo hinh ket noi.
- Chon cach frame packet on dinh.
- Chot cach encode/decode JSON.
- Tao skeleton cho server listener va client connector.

### Giai doan core connection

- Viet TCP server accept loop.
- Viet TCP client connect loop.
- Test echo/handshake co ban.
- Bao dam server khong block khi mot client cham.
- Tao connection/session object rieng cho moi client.

### Giai doan packet contract

- Dinh nghia packet type chinh.
- Xay parser/serializer.
- Chot loi output neu packet sai format.
- Dam bao packet model khop voi `API.md`.
- Khong thay doi schema vo toi va lap lai.

### Giai doan dispatch

- Route packet theo `type`.
- Tach handler cho login, status, chat, lock, unlock, timer, notification.
- Tra ket qua ro rang khi packet khong hop le.
- Ghi log loi socket va loi parse.

### Giai doan on dinh

- Bat exception socket co kiem soat.
- Xu ly disconnect sach se.
- Ho tro reconnect co ban cho client.
- Kiem tra timeout va mat ket noi giua chung.
- Giu thread UI khong bi block.

### Giai doan integration

- Kiem tra network voi server GUI.
- Kiem tra network voi client GUI.
- Kiem tra message broadcast neu can.
- Kiem tra state sync giua nhieu client.
- Kiem tra thoi gian phan hoi khi co nhieu event lien tiep.

### Giai doan release

- Chay test multi-client.
- Chay test packet invalid.
- Chay test server restart.
- Chay test reconnect sau disconnect.
- Xac nhan network layer doan truoc demo.

## 4. Checklist cong viec theo phase

### Phase 1 - Connection

- [ ] Tao server listener
- [ ] Tao client connector
- [ ] Tao handshake
- [ ] Tao basic send/receive loop

### Phase 2 - Packet contract

- [ ] Dinh nghia packet type
- [ ] Dinh nghia shared DTO
- [ ] Dinh nghia framing rule
- [ ] Dinh nghia parse error behavior

### Phase 3 - Routing and state

- [ ] Xay packet dispatcher
- [ ] Ganh client session state
- [ ] Ganh server machine state
- [ ] Dong bo status update

### Phase 4 - Resilience

- [ ] Xu ly disconnect
- [ ] Xu ly timeout
- [ ] Xu ly reconnect
- [ ] Ghi log loi mang

### Phase 5 - Integration

- [ ] Ket noi voi server UI
- [ ] Ket noi voi client UI
- [ ] Test broadcast
- [ ] Test multi-client

## 5. Cong viec theo tung loai packet

### Auth packets

- `LOGIN`
- `LOGOUT`

### Control packets

- `LOCK`
- `UNLOCK`
- `TIMER`

### Status packets

- `STATUS`
- `SESSION`
- `HEARTBEAT` neu duoc bo sung sau

### Communication packets

- `CHAT`
- `NOTIFICATION`

## 6. Nguyen tac ky thuat

- Khong doc ghi socket tren UI thread.
- Khong update WinForms control truc tiep tu background thread.
- Khong de schema packet thay doi ma khong cap nhat shared docs.
- Khong de loi socket lam sap toan bo server.
- Khong de message format tua nhau giua server va client.

## 7. Deliverables can nop

- TCP server/client core.
- Packet parser/serializer.
- Dispatcher and handlers.
- Reconnect and disconnect handling.
- Multi-client test note.

## 8. Definition of Done

- 2-3 client ket noi on dinh.
- Packet qua lai dung schema.
- Disconnect khong lam crash app.
- Module khac co the tich hop vao API ma khong xung dot.
