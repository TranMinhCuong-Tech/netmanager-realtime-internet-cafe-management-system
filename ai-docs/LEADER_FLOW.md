# LEADER FLOW

## Muc dich

Tai lieu nay danh cho team lead / system architect de dieu phoi project theo dung thu tu:

- ai lam truoc
- ai lam sau
- module nao lam truoc
- chuc nang nao phai xong truoc khi sang buoc tiep theo

Muc tieu la giu project di theo dung luong, tranh overlap giua member, va dam bao cac feature core duoc dong bo tu dau den cuoi.

## Nguyen tac dieu phoi

- Networking va shared contract phai chot truoc GUI polish.
- Auth va session phai co truoc khi dong bo trang thai nay sang dashboard.
- Client connect/login phai co truoc lock/unlock va timer.
- Test va documentation lam lien tuc, khong doi cuoi ky.
- Moi thay doi packet hoac state phai cap nhat docs ngay.

## Thu tu lam viec tong the

### Buoc 1 - Chot scope va quy uoc

Nguoi phu trach:

- Member 1

Cong viec:

- chot MVP
- loai bo tinh nang ngoai scope
- chot branch convention
- chot commit convention
- chot folder structure
- chot danh sach packet core

Ket qua can co:

- roadmap ro rang
- file memory dong bo
- member biet pham vi cua minh

### Buoc 2 - Chot shared contract

Nguoi phu trach:

- Member 1 dieu phoi
- Member 2 implementation chinh

Cong viec:

- dinh nghia packet type
- dinh nghia DTO chung
- dinh nghia state enum
- dinh nghia framing / parse rule
- chot contract API trong `API.md`

Thu tu:

1. login
2. status
3. lock / unlock
4. notification
5. timer
6. chat

Ket qua can co:

- server va client doc cung schema
- khong co packet mo ho

### Buoc 3 - Lam networking core

Nguoi phu trach:

- Member 2

Cong viec:

- tao TCP server
- tao TCP client
- tao send / receive loop
- tao parser va dispatcher
- test multi-client co ban

Thu tu lam chuc nang:

1. connect
2. handshake / login
3. status update
4. broadcast notification
5. lock / unlock
6. timer
7. chat

Ket qua can co:

- ket noi on dinh
- packet gui nhan duoc
- khong block UI

### Buoc 4 - Lam authentication va session

Nguoi phu trach:

- Member 5

Cong viec:

- tao schema SQLite
- tao repository / data access
- tao login validation
- tao session tracking
- tao last login / machine mapping neu can

Thu tu:

1. schema Users
2. schema Sessions
3. auth service
4. session service
5. tich hop voi server login flow

Ket qua can co:

- server xac thuc duoc admin/client
- session state co the luu va doc lai

### Buoc 5 - Lam server dashboard

Nguoi phu trach:

- Member 3

Cong viec:

- tao login form admin
- tao dashboard
- tao machine list
- tao control panel
- tao realtime update UI

Thu tu:

1. layout
2. machine list
3. status display
4. action buttons
5. realtime refresh

Phu thuoc:

- can packet/status tu Member 2
- can auth result tu Member 5

Ket qua can co:

- admin nhin thay may va trang thai
- button gui lenh dung packet

### Buoc 6 - Lam client app

Nguoi phu trach:

- Member 4

Cong viec:

- tao connect form
- tao login flow
- tao receive handlers
- tao lock screen
- tao notification / timer / chat UI

Thu tu:

1. connect
2. login
3. receive status
4. notification
5. timer
6. lock / unlock
7. chat

Phu thuoc:

- can packet contract tu Member 2
- can auth flow tu Member 5

Ket qua can co:

- client connect/login on dinh
- client xu ly command tu server

### Buoc 7 - Tich hop end-to-end

Nguoi phu trach:

- Member 1 dieu phoi
- Member 2, 3, 4, 5 tham gia

Cong viec:

- noi server voi client that
- test login end-to-end
- test machine state sync
- test lock/unlock
- test notification
- test timer
- test chat

Thu tu test:

1. connect
2. login
3. machine list
4. notification
5. lock / unlock
6. timer
7. chat

Ket qua can co:

- demo flow chay duoc tu dau den cuoi

### Buoc 8 - Testing va bug fixing

Nguoi phu trach:

- Member 6 chinh
- tat ca member ho tro

Cong viec:

- smoke test
- regression test
- multi-client test
- disconnect test
- invalid packet test
- bug triage

Thu tu:

1. test core flow
2. test edge cases
3. ghi bug
4. uu tien bug
5. sua bug
6. retest

Ket qua can co:

- bug lon duoc dong
- demo scenario on dinh

### Buoc 9 - Documentation va release

Nguoi phu trach:

- Member 6
- Member 1 review

Cong viec:

- cap nhat README
- cap nhat ai-docs
- cap nhat huong dan chay
- cap nhat bug log
- chot demo checklist

Ket qua can co:

- tai lieu khop voi code
- project san sang ban giao / demo

## Thu tu phan cong theo do uu tien

### Uu tien 1

- Member 2: networking core
- Member 5: auth schema va session

### Uu tien 2

- Member 3: server dashboard skeleton
- Member 4: client connect/login skeleton

### Uu tien 3

- Member 2: packet handlers cho core commands
- Member 3: realtime machine view
- Member 4: realtime client receive flow

### Uu tien 4

- Member 5: persistence hardening
- Member 6: test and docs
- Member 1: integration review

## Thu tu chuc nang can hoan thanh

### Core flow

1. TCP connect
2. login
3. machine/session state
4. notification
5. lock / unlock
6. timer
7. chat

### Support flow

1. auth database
2. reconnect
3. disconnect handling
4. logging
5. validation

### Final flow

1. multi-client demo
2. bug fixing
3. documentation
4. release checklist

## Rule khi co xung dot

- Neu packet contract thay doi, dung implementation lai de dong bo API truoc.
- Neu GUI doi layout nhung network chua xong, uu tien network core.
- Neu bug lien quan toi auth hoac disconnect, sua truoc bug UI dep.
- Neu co overlap giua members, Member 1 phai chot ai giu owner chinh.

## Output ma leader can theo doi hang ngay

- task nao done
- task nao blocked
- task nao can ho tro
- file nao phai update
- feature nao co risk

## Definition of Done cho Leader Flow

- Moi thanh vien biet minh lam gi va lam luc nao.
- Moi feature co thu tu ro rang.
- Khong co module nao bi lam truoc khi phu thuoc cua no san sang.
- Project co the di tu khoi tao den demo ma khong bi vo flow.
