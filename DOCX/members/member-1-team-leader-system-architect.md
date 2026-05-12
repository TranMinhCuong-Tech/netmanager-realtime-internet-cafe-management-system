# Member 1 - Team Leader / System Architect

## 1. Vai tro

Ban la nguoi giu huong di tong the cua du an. Nhiem vu cua ban la giu scope dung, kien truc dong bo, milestone ro rang, va integration khong bi vo.

## 2. Muc tieu cuoi cung

- Du an chay dung pham vi demo da chot.
- Server, client va shared contract khong xung dot nhau.
- Moi thanh vien biet ro minh lam gi, lam den dau, va khi nao xong.
- Team co the tiep tuc lam viec o session sau ma khong phai reread toan bo codebase.

## 3. Viec can lam tu dau den cuoi

### Giai doan khoi tao

- Chot pham vi MVP.
- Loai bo cac tinh nang khong phuc vu demo.
- Xac nhan branch rule, commit rule, folder rule.
- Ghi lai architecture, API contract, va tracker file.
- Phan cong owner chinh cho tung module.

### Giai doan thiet ke

- Chot structure `ServerApp`, `ClientApp`, `Shared`.
- Chot packet type, state model, va async model.
- Kiem tra cac quy uoc dat ten co dong bo giua server va client.
- Dinh nghia dau vao va dau ra cho moi module.
- Dam bao cac member khong lam overlap vao cung mot write scope.

### Giai doan xay dung nen tang

- Theo doi tien do TCP core, auth core, UI core, va DB core.
- Mo lich integration som thay vi doi cuoi ky.
- Kiem tra cac contract nhu `LOGIN`, `STATUS`, `LOCK`, `UNLOCK`, `NOTIFICATION`, `TIMER`, `CHAT`.
- Neu co module cham, dieu chinh uu tien ngay.
- Giai quyet conflict ve packet, state, hoac thread model.

### Giai doan tich hop

- Review source changes truoc khi merge.
- Kiem tra client va server co cung hieu mot packet hay khong.
- Kiem tra UI co bi block boi network work hay khong.
- Kiem tra database auth co tra ve dung trang thai hay khong.
- Xac nhan multi-client flow truoc khi de mo rong them.

### Giai doan hoan thien

- Uu tien bug co lien quan den login, reconnect, lock, timer, notification.
- Doi chieu bug list voi test result.
- Kiem tra build demo va huong dan chay.
- Dam bao docs va code khong mo thuan nhau.
- Chot version dem0-ready.

## 4. Checklist cong viec theo phase

### Phase 0 - Project control

- [ ] Chot scope MVP
- [ ] Chot branch conventions
- [ ] Chot commit conventions
- [ ] Chot folder layout
- [ ] Chot ownership theo member

### Phase 1 - Architecture and contract

- [ ] Xac nhan luong server/client
- [ ] Xac nhan shared packet contract
- [ ] Xac nhan state model
- [ ] Xac nhan database schema muc tieu
- [ ] Xac nhan threading rules

### Phase 2 - Build tracking

- [ ] Theo doi task cua tung member
- [ ] Cap nhat blocker hang ngay
- [ ] Ghi decision khi co thay doi lon
- [ ] Kiem tra phan tich rui ro
- [ ] Doi chieu milestone voi tien do that

### Phase 3 - Integration and release

- [ ] Review merge request / commit changes
- [ ] Kiem tra regression
- [ ] Chot demo flow
- [ ] Chot bug list
- [ ] Chot final docs

## 5. Cong viec theo tung module

### Server

- Kiem tra co accept nhieu client duoc khong.
- Kiem tra command dispatch co dung packet type khong.
- Kiem tra login va auth response co dung khong.
- Kiem tra dashboard co cap nhat realtime khong.

### Client

- Kiem tra connect/login flow.
- Kiem tra lock/unlock screen.
- Kiem tra notification, timer, chat.
- Kiem tra disconnect va reconnect.

### Shared

- Kiem tra packet model co on dinh khong.
- Kiem tra enums, constants, va utility co dong bo khong.
- Kiem tra schema changes co duoc cap nhat tai API.md khong.

### Database

- Kiem tra account lookup.
- Kiem tra password handling.
- Kiem tra session tracking.
- Kiem tra config persistence.

## 6. Nhiem vu hang ngay

- Kiem tra `PROJECT_STATE.md` truoc khi bat dau.
- Kiem tra `TASKS.md` de lay task tiep theo.
- Ghi note ngan neu co decision moi.
- Neu co bug moi, dua vao `BUGS.md`.
- Neu co doi schema hay protocol, cap nhat `API.md`.

## 7. Deliverables can nop

- Roadmap da chia phase ro rang.
- Architecture va API contract dong bo.
- Task tracking cap nhat lien tuc.
- Bug list va decision log day du.
- Build demo on dinh.

## 8. Definition of Done

- Scope khong lech.
- Milestone khong bi tre ma khong co ly do.
- Team co the tiep tuc lam viec ma khong mat ngu canh.
- Tat ca file memory phan anh dung trang thai hien tai.
