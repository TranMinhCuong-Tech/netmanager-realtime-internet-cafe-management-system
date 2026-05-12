# Member 5 - Database & Authentication

## 1. Vai tro

Ban phu trach SQLite, login validation, luu account, luu session, va cac thong tin persistence co ban cho he thong.

## 2. Muc tieu cuoi cung

- Auth hoat dong on dinh.
- Du lieu user va session co the luu/lay lai duoc.
- Server co can cu de xac thuc admin/client.
- Schema don gian, de debug, de demo.

## 3. Viec can lam tu dau den cuoi

### Giai doan khoi tao

- Chot schema toi thieu.
- Chot field can thiet cho `Users`, `Sessions`, `Machines`.
- Chot cach hash password neu co.
- Chot luong auth cho admin va client.

### Giai doan data access

- Tao repository/data access layer rieng.
- Khong viet SQL truc tiep trong UI.
- Tao ham add/get/update/validate.
- Tao error handling cho SQLite ro rang.

### Giai doan authentication

- Validate username/password.
- Tra ket qua success/fail ro rang.
- Phan biet admin, client, disabled, locked neu can.
- Ghi last login neu co.

### Giai doan session and config

- Luu session co ban.
- Luu machine mapping neu can.
- Luu config ket noi neu project can.
- Khong luu du lieu nhay cam khong can thiet.

### Giai doan integration

- Noi auth vao server login flow.
- Noi auth vao dashboard neu co admin login.
- Noi session vao state machine cua server.
- Dam bao ket qua auth khop voi packet contract.

### Giai doan hardening

- Kiem tra sai mat khau.
- Kiem tra user khong ton tai.
- Kiem tra database bi loi.
- Kiem tra concurrent access co an toan khong.

### Giai doan release

- Kiem tra login tu client.
- Kiem tra login tu admin.
- Kiem tra session tracking.
- Kiem tra database khong lam app crash khi loi.

## 4. Checklist cong viec theo phase

### Phase 1 - Schema

- [ ] Tao `Users`
- [ ] Tao `Sessions`
- [ ] Tao `Machines`
- [ ] Chot field va key

### Phase 2 - Data access

- [ ] Tao repository
- [ ] Tao query helpers
- [ ] Tao error handling
- [ ] Tao seed data neu can

### Phase 3 - Authentication

- [ ] Validate login
- [ ] Tra auth result
- [ ] Luu last login
- [ ] Phan loai role

### Phase 4 - Session storage

- [ ] Luu session start/end
- [ ] Luu machine mapping
- [ ] Luu config co ban
- [ ] Dong bo voi server state

### Phase 5 - Stability

- [ ] Test sai mat khau
- [ ] Test user khong ton tai
- [ ] Test DB error handling
- [ ] Test concurrent access

## 5. Nguyen tac

- Schema don gian hon schema dep.
- Khong de SQL loang ra code UI.
- Khong de plaintext password ton tai trong luong xu ly.
- Khong de auth result mo ho.

## 6. Deliverables can nop

- SQLite schema hoac script.
- Data access layer.
- Auth service.
- Session/config storage.

## 7. Definition of Done

- Login on dinh.
- Data doc/ghi duoc.
- Team khac co the dung auth service ma khong doan schema.
- DB loi khong lam sap demo.
