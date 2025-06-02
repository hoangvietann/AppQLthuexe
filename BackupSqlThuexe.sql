--
-- PostgreSQL database cluster dump
--

-- Started on 2025-06-02 21:40:04

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS PASSWORD 'SCRAM-SHA-256$4096:ghVY0GXoMH5Wj/gdYvkDVQ==$boh3OkrCXV6oShfn/fzTLrhs319zb9ycskY3RtM5pwU=:x27EAFka84krG6dnx6ysk0jcbPbBkdiHHYatN2Sfvm8=';

--
-- User Configurations
--








--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2025-06-02 21:40:04

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Completed on 2025-06-02 21:40:05

--
-- PostgreSQL database dump complete
--

--
-- Database "postgres" dump
--

\connect postgres

--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2025-06-02 21:40:05

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2 (class 3079 OID 16384)
-- Name: adminpack; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS adminpack WITH SCHEMA pg_catalog;


--
-- TOC entry 4841 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION adminpack; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION adminpack IS 'administrative functions for PostgreSQL';


--
-- TOC entry 225 (class 1255 OID 73728)
-- Name: tinh_tong_tien(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tinh_tong_tien() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    ma_hd INT;
BEGIN
    -- Lấy mã hợp đồng cần cập nhật
    IF (TG_OP = 'DELETE') THEN
        ma_hd := OLD.MaHD;
    ELSE
        ma_hd := NEW.MaHD;
    END IF;

    -- Cập nhật tổng tiền trong bảng HopDong
    UPDATE HopDong
    SET Tongtien = (
        SELECT COALESCE(SUM(Thanhtien), 0)
        FROM ChiTietHD
        WHERE MaHD = ma_hd
    )
    WHERE MaHD = ma_hd;

    RETURN NULL;
END;
$$;


ALTER FUNCTION public.tinh_tong_tien() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 73729)
-- Name: chitiethd; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.chitiethd (
    macthd integer NOT NULL,
    mahd integer NOT NULL,
    max integer NOT NULL,
    thuetaixe character varying(20) NOT NULL,
    ngaythue timestamp without time zone NOT NULL,
    ngaytra timestamp without time zone NOT NULL,
    dongia integer,
    sogiothue numeric(10,2),
    thanhtien integer,
    CONSTRAINT chitiethd_dongia_check CHECK ((dongia >= 0)),
    CONSTRAINT chitiethd_thanhtien_check CHECK ((thanhtien >= 0))
);


ALTER TABLE public.chitiethd OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 73734)
-- Name: chitiethd_macthd_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.chitiethd_macthd_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.chitiethd_macthd_seq OWNER TO postgres;

--
-- TOC entry 4842 (class 0 OID 0)
-- Dependencies: 220
-- Name: chitiethd_macthd_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.chitiethd_macthd_seq OWNED BY public.chitiethd.macthd;


--
-- TOC entry 221 (class 1259 OID 73735)
-- Name: hopdong; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hopdong (
    mahd integer NOT NULL,
    makh integer NOT NULL,
    ngaylap timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    tongtien integer,
    CONSTRAINT hopdong_tongtien_check CHECK ((tongtien >= 0))
);


ALTER TABLE public.hopdong OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 73740)
-- Name: hopdong_mahd_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.hopdong_mahd_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.hopdong_mahd_seq OWNER TO postgres;

--
-- TOC entry 4843 (class 0 OID 0)
-- Dependencies: 222
-- Name: hopdong_mahd_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.hopdong_mahd_seq OWNED BY public.hopdong.mahd;


--
-- TOC entry 217 (class 1259 OID 24577)
-- Name: khachhang; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.khachhang (
    makh integer NOT NULL,
    hoten character varying(100) NOT NULL,
    cccd character varying(20) NOT NULL,
    diachi text,
    sdt character varying(15)
);


ALTER TABLE public.khachhang OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 24576)
-- Name: khachhang_makh_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.khachhang_makh_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.khachhang_makh_seq OWNER TO postgres;

--
-- TOC entry 4844 (class 0 OID 0)
-- Dependencies: 216
-- Name: khachhang_makh_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.khachhang_makh_seq OWNED BY public.khachhang.makh;


--
-- TOC entry 218 (class 1259 OID 24587)
-- Name: taikhoan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.taikhoan (
    tentk character varying(50) NOT NULL,
    matkhau text NOT NULL,
    loaitk character varying(20) DEFAULT 'nhanvien'::character varying
);


ALTER TABLE public.taikhoan OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 73741)
-- Name: xeoto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.xeoto (
    max integer NOT NULL,
    tenx character varying(100) NOT NULL,
    theloai character varying(100) NOT NULL,
    bienso character varying(20) NOT NULL,
    dongiax integer,
    trangthai character varying(20) DEFAULT 'Trống'::character varying,
    CONSTRAINT xeoto_dongiax_check CHECK ((dongiax >= 0))
);


ALTER TABLE public.xeoto OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 73746)
-- Name: xeoto_max_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.xeoto_max_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.xeoto_max_seq OWNER TO postgres;

--
-- TOC entry 4845 (class 0 OID 0)
-- Dependencies: 224
-- Name: xeoto_max_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.xeoto_max_seq OWNED BY public.xeoto.max;


--
-- TOC entry 4657 (class 2604 OID 73779)
-- Name: chitiethd macthd; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd ALTER COLUMN macthd SET DEFAULT nextval('public.chitiethd_macthd_seq'::regclass);


--
-- TOC entry 4658 (class 2604 OID 73780)
-- Name: hopdong mahd; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hopdong ALTER COLUMN mahd SET DEFAULT nextval('public.hopdong_mahd_seq'::regclass);


--
-- TOC entry 4655 (class 2604 OID 73781)
-- Name: khachhang makh; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.khachhang ALTER COLUMN makh SET DEFAULT nextval('public.khachhang_makh_seq'::regclass);


--
-- TOC entry 4660 (class 2604 OID 73782)
-- Name: xeoto max; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.xeoto ALTER COLUMN max SET DEFAULT nextval('public.xeoto_max_seq'::regclass);


--
-- TOC entry 4830 (class 0 OID 73729)
-- Dependencies: 219
-- Data for Name: chitiethd; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.chitiethd (macthd, mahd, max, thuetaixe, ngaythue, ngaytra, dongia, sogiothue, thanhtien) FROM stdin;
1	1	2	Có	2025-05-17 02:34:40.878621	2025-05-19 02:34:40	100000	48.00	5040000
2	1	3	Không	2025-05-17 02:34:40.878621	2025-05-19 02:34:40	150000	48.00	7200000
3	2	1	Không	2025-05-17 21:48:41.663647	2025-05-18 10:48:41	100000	13.00	1300000
4	3	11	Có	2025-05-18 06:30:12	2025-05-18 11:00:12	400000	4.50	1890000
5	4	10	Có	2025-04-30 06:00:03	2025-04-30 11:30:03	320000	5.50	1848000
6	7	5	Có	2025-05-18 01:50:42.857359	2025-05-18 05:50:42	200000	4.00	840000
31	19	2	Có	2024-04-18 20:00:00	2024-04-19 08:00:00	100000	12.00	1260000
32	19	4	Không	2024-04-19 09:00:00	2024-04-19 18:00:00	150000	9.00	1350000
33	20	6	Không	2025-02-02 20:00:00	2025-02-03 08:00:00	200000	12.00	2400000
34	20	7	Có	2025-02-03 09:00:00	2025-02-03 18:00:00	300000	9.00	2835000
35	21	9	Có	2025-05-24 06:30:00	2025-05-24 20:30:00	320000	14.00	4704000
36	21	11	Không	2025-05-25 08:00:00	2025-05-25 18:00:00	400000	10.00	4000000
37	22	10	Không	2024-06-23 11:00:00	2024-06-23 20:00:00	320000	9.00	2880000
38	22	15	Có	2024-06-24 08:00:00	2024-06-24 17:00:00	400000	9.00	3780000
39	23	16	Không	2025-06-14 14:30:00	2025-06-15 02:30:00	400000	12.00	4800000
40	23	17	Có	2025-06-15 08:00:00	2025-06-15 20:00:00	450000	12.00	5670000
41	23	18	Không	2025-06-16 08:00:00	2025-06-16 18:00:00	420000	10.00	4200000
42	24	19	Có	2024-10-28 05:30:00	2024-10-28 17:30:00	500000	12.00	6300000
43	24	20	Không	2024-10-29 08:00:00	2024-10-29 18:00:00	500000	10.00	5000000
44	25	3	Có	2024-07-10 09:00:00	2024-07-10 21:00:00	150000	12.00	1890000
45	25	5	Không	2024-07-11 08:00:00	2024-07-11 18:00:00	200000	10.00	2000000
46	26	11	Không	2024-10-03 14:30:00	2024-10-03 22:30:00	400000	8.00	3200000
47	26	4	Có	2024-10-04 07:00:00	2024-10-04 19:00:00	150000	12.00	1890000
48	27	18	Có	2024-12-21 10:00:00	2024-12-21 22:00:00	420000	12.00	5292000
49	28	12	Không	2025-01-11 09:30:00	2025-01-11 19:30:00	400000	10.00	4000000
50	29	17	Có	2025-03-02 16:50:00	2025-03-03 04:50:00	450000	12.00	5670000
51	32	4	Không	2025-06-01 21:10:34.338703	2025-06-01 23:10:34	150000	2.00	300000
\.


--
-- TOC entry 4832 (class 0 OID 73735)
-- Dependencies: 221
-- Data for Name: hopdong; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.hopdong (mahd, makh, ngaylap, tongtien) FROM stdin;
1	1	2025-05-17 02:35:14.425498	12240000
2	1	2025-05-17 21:49:23.448964	1300000
3	1	2025-05-17 22:05:37.347943	1890000
4	1	2025-04-30 05:40:07	1848000
32	20	2025-06-01 21:10:52.937774	300000
7	3	2025-05-18 01:51:21.668675	840000
19	13	2024-04-18 19:14:00	2610000
20	14	2025-02-02 19:29:00	5235000
21	10	2025-05-24 06:01:00	8704000
22	11	2024-06-23 10:52:00	6660000
23	20	2025-06-14 14:45:00	14670000
24	16	2024-10-28 05:34:00	11300000
25	15	2024-07-10 08:45:00	3890000
26	11	2024-10-03 14:20:00	5090000
27	18	2024-12-21 10:00:00	5292000
28	12	2025-01-11 09:30:00	4000000
29	17	2025-03-02 16:50:00	5670000
\.


--
-- TOC entry 4828 (class 0 OID 24577)
-- Dependencies: 217
-- Data for Name: khachhang; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.khachhang (makh, hoten, cccd, diachi, sdt) FROM stdin;
1	Nguyễn Văn A	123456789012	Hà Nội	0987654321
2	Trần Thị B	987654321098	Bắc Giang	0912345678
3	Lê Văn C	567890123456	TP.HCM	0938765432
10	Hoàng Văn Anh	097863793827	Đồi Ngô - Lục Nam - Bắc Giang	0985432745
11	Nguyễn Văn Hùng	123456789001	Thị trấn Đồi Ngô, huyện Lục Nam, Bắc Giang	0987654321
12	Trần Thị Mai	123456789002	Xã Tiên Hưng, huyện Lục Nam, Bắc Giang	0978123456
13	Lê Văn Khánh	123456789003	Xã Bảo Sơn, huyện Lục Nam, Bắc Giang	0367891234
14	Phạm Thị Hòa	123456789004	Xã Cương Sơn, huyện Lục Nam, Bắc Giang	0912345678
15	Hoàng Văn Quý	123456789005	Xã Trường Sơn, huyện Lục Nam, Bắc Giang	0905123456
16	Vũ Thị Ngọc	123456789006	Xã Nghĩa Phương, huyện Lục Nam, Bắc Giang	0981122334
17	Đặng Văn Toàn	123456789007	Xã Tam Dị, huyện Lục Nam, Bắc Giang	0937894561
18	Ngô Thị Lan	123456789008	Xã Phương Sơn, huyện Lục Nam, Bắc Giang	0941122333
19	Bùi Văn Sơn	123456789009	Xã Đông Hưng, huyện Lục Nam, Bắc Giang	0922233445
20	Đinh Thị Hương	123456789010	Xã Bắc Lũng, huyện Lục Nam, Bắc Giang	0966677889
\.


--
-- TOC entry 4829 (class 0 OID 24587)
-- Dependencies: 218
-- Data for Name: taikhoan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.taikhoan (tentk, matkhau, loaitk) FROM stdin;
An	12345	Admin
hoàng an	12345678910	Nhân viên
nhanvien02	xyz12345	Nhân viên
admin	123456	Admin
nhanvien01	12345	Nhân viên
\.


--
-- TOC entry 4834 (class 0 OID 73741)
-- Dependencies: 223
-- Data for Name: xeoto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.xeoto (max, tenx, theloai, bienso, dongiax, trangthai) FROM stdin;
6	Expander	7 chỗ	30A-30002	200000	Trống
7	Ford Transit	16 chỗ	30B-40001	300000	Trống
8	Ford Transit	16 chỗ	30B-40002	300000	Trống
9	Hyundai Solati	16 chỗ	30B-50001	320000	Trống
12	Hyundai County	29 chỗ	30F-60002	400000	Trống
13	Samco	29 chỗ	30F-70001	450000	Trống
14	Samco	29 chỗ	30F-70002	450000	Trống
15	Thaco	29 chỗ	30F-80001	400000	Trống
16	Thaco	29 chỗ	30F-80002	400000	Trống
17	Samco	35 chỗ	30G-90001	420000	Trống
18	Samco	35 chỗ	30G-90002	420000	Trống
19	Thaco	45 chỗ	30H-00001	500000	Trống
20	Thaco	45 chỗ	30H-00002	500000	Trống
2	Hyundai Accent	5 chỗ	30A-10002	100000	Trống
3	Vios	5 chỗ	30A-20001	150000	Trống
1	Hyundai Accent	5 chỗ	30A-10001	100000	Trống
11	Hyundai County	29 chỗ	30F-60001	400000	Trống
10	Hyundai Solati	16 chỗ	30B-50002	320000	Trống
5	Expander	7 chỗ	30A-30001	200000	Trống
4	Vios	5 chỗ	30A-20002	150000	Bảo trì
\.


--
-- TOC entry 4846 (class 0 OID 0)
-- Dependencies: 220
-- Name: chitiethd_macthd_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.chitiethd_macthd_seq', 51, true);


--
-- TOC entry 4847 (class 0 OID 0)
-- Dependencies: 222
-- Name: hopdong_mahd_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hopdong_mahd_seq', 32, true);


--
-- TOC entry 4848 (class 0 OID 0)
-- Dependencies: 216
-- Name: khachhang_makh_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.khachhang_makh_seq', 20, true);


--
-- TOC entry 4849 (class 0 OID 0)
-- Dependencies: 224
-- Name: xeoto_max_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.xeoto_max_seq', 20, true);


--
-- TOC entry 4673 (class 2606 OID 73752)
-- Name: chitiethd chitiethd_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd
    ADD CONSTRAINT chitiethd_pkey PRIMARY KEY (macthd);


--
-- TOC entry 4675 (class 2606 OID 73754)
-- Name: hopdong hopdong_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hopdong
    ADD CONSTRAINT hopdong_pkey PRIMARY KEY (mahd);


--
-- TOC entry 4667 (class 2606 OID 24586)
-- Name: khachhang khachhang_cccd_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.khachhang
    ADD CONSTRAINT khachhang_cccd_key UNIQUE (cccd);


--
-- TOC entry 4669 (class 2606 OID 24584)
-- Name: khachhang khachhang_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.khachhang
    ADD CONSTRAINT khachhang_pkey PRIMARY KEY (makh);


--
-- TOC entry 4671 (class 2606 OID 24594)
-- Name: taikhoan taikhoan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.taikhoan
    ADD CONSTRAINT taikhoan_pkey PRIMARY KEY (tentk);


--
-- TOC entry 4677 (class 2606 OID 73756)
-- Name: xeoto xeoto_bienso_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.xeoto
    ADD CONSTRAINT xeoto_bienso_key UNIQUE (bienso);


--
-- TOC entry 4679 (class 2606 OID 73758)
-- Name: xeoto xeoto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.xeoto
    ADD CONSTRAINT xeoto_pkey PRIMARY KEY (max);


--
-- TOC entry 4683 (class 2620 OID 73759)
-- Name: chitiethd tg_capnhat_tongtien; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER tg_capnhat_tongtien AFTER INSERT OR DELETE OR UPDATE ON public.chitiethd FOR EACH ROW EXECUTE FUNCTION public.tinh_tong_tien();


--
-- TOC entry 4680 (class 2606 OID 73760)
-- Name: chitiethd fk_hd_ct; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd
    ADD CONSTRAINT fk_hd_ct FOREIGN KEY (mahd) REFERENCES public.hopdong(mahd);


--
-- TOC entry 4682 (class 2606 OID 73765)
-- Name: hopdong fk_khachhang_hd; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hopdong
    ADD CONSTRAINT fk_khachhang_hd FOREIGN KEY (makh) REFERENCES public.khachhang(makh);


--
-- TOC entry 4681 (class 2606 OID 73770)
-- Name: chitiethd fk_xe_ct; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd
    ADD CONSTRAINT fk_xe_ct FOREIGN KEY (max) REFERENCES public.xeoto(max);


-- Completed on 2025-06-02 21:40:05

--
-- PostgreSQL database dump complete
--

--
-- Database "qlthuexe" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2025-06-02 21:40:05

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4840 (class 1262 OID 24595)
-- Name: qlthuexe; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE qlthuexe WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';


ALTER DATABASE qlthuexe OWNER TO postgres;

\connect qlthuexe

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 224 (class 1255 OID 57344)
-- Name: tinh_tong_tien(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tinh_tong_tien() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    ma_hd INT;
BEGIN
    -- Lấy mã hợp đồng cần cập nhật
    IF (TG_OP = 'DELETE') THEN
        ma_hd := OLD.MaHD;
    ELSE
        ma_hd := NEW.MaHD;
    END IF;

    -- Cập nhật tổng tiền trong bảng HopDong
    UPDATE HopDong
    SET Tongtien = (
        SELECT COALESCE(SUM(Thanhtien), 0)
        FROM ChiTietHD
        WHERE MaHD = ma_hd
    )
    WHERE MaHD = ma_hd;

    RETURN NULL;
END;
$$;


ALTER FUNCTION public.tinh_tong_tien() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 223 (class 1259 OID 65630)
-- Name: chitiethd; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.chitiethd (
    macthd integer NOT NULL,
    mahd integer NOT NULL,
    max integer NOT NULL,
    thuetaixe character varying(20) NOT NULL,
    ngaythue timestamp without time zone NOT NULL,
    ngaytra timestamp without time zone NOT NULL,
    dongia integer,
    sogiothue numeric(10,2),
    thanhtien integer,
    CONSTRAINT chitiethd_dongia_check CHECK ((dongia >= 0)),
    CONSTRAINT chitiethd_thanhtien_check CHECK ((thanhtien >= 0))
);


ALTER TABLE public.chitiethd OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 65629)
-- Name: chitiethd_macthd_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.chitiethd_macthd_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.chitiethd_macthd_seq OWNER TO postgres;

--
-- TOC entry 4841 (class 0 OID 0)
-- Dependencies: 222
-- Name: chitiethd_macthd_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.chitiethd_macthd_seq OWNED BY public.chitiethd.macthd;


--
-- TOC entry 221 (class 1259 OID 65615)
-- Name: hopdong; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hopdong (
    mahd integer NOT NULL,
    makh integer NOT NULL,
    ngaylap timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    tongtien integer,
    CONSTRAINT hopdong_tongtien_check CHECK ((tongtien >= 0))
);


ALTER TABLE public.hopdong OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 65614)
-- Name: hopdong_mahd_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.hopdong_mahd_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.hopdong_mahd_seq OWNER TO postgres;

--
-- TOC entry 4842 (class 0 OID 0)
-- Dependencies: 220
-- Name: hopdong_mahd_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.hopdong_mahd_seq OWNED BY public.hopdong.mahd;


--
-- TOC entry 216 (class 1259 OID 24598)
-- Name: khachhang; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.khachhang (
    makh integer NOT NULL,
    hoten character varying(100) NOT NULL,
    cccd character varying(20) NOT NULL,
    diachi text,
    sdt character varying(15)
);


ALTER TABLE public.khachhang OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 24597)
-- Name: khachhang_makh_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.khachhang_makh_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.khachhang_makh_seq OWNER TO postgres;

--
-- TOC entry 4843 (class 0 OID 0)
-- Dependencies: 215
-- Name: khachhang_makh_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.khachhang_makh_seq OWNED BY public.khachhang.makh;


--
-- TOC entry 217 (class 1259 OID 24608)
-- Name: taikhoan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.taikhoan (
    tentk character varying(50) NOT NULL,
    matkhau text NOT NULL,
    loaitk character varying(20) DEFAULT 'nhanvien'::character varying
);


ALTER TABLE public.taikhoan OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 65598)
-- Name: xeoto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.xeoto (
    max integer NOT NULL,
    tenx character varying(100) NOT NULL,
    theloai character varying(100) NOT NULL,
    bienso character varying(20) NOT NULL,
    dongiax integer,
    trangthai character varying(20) DEFAULT 'Trống'::character varying,
    CONSTRAINT xeoto_dongiax_check CHECK ((dongiax >= 0))
);


ALTER TABLE public.xeoto OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 65597)
-- Name: xeoto_max_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.xeoto_max_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.xeoto_max_seq OWNER TO postgres;

--
-- TOC entry 4844 (class 0 OID 0)
-- Dependencies: 218
-- Name: xeoto_max_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.xeoto_max_seq OWNED BY public.xeoto.max;


--
-- TOC entry 4660 (class 2604 OID 65633)
-- Name: chitiethd macthd; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd ALTER COLUMN macthd SET DEFAULT nextval('public.chitiethd_macthd_seq'::regclass);


--
-- TOC entry 4658 (class 2604 OID 65618)
-- Name: hopdong mahd; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hopdong ALTER COLUMN mahd SET DEFAULT nextval('public.hopdong_mahd_seq'::regclass);


--
-- TOC entry 4654 (class 2604 OID 24601)
-- Name: khachhang makh; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.khachhang ALTER COLUMN makh SET DEFAULT nextval('public.khachhang_makh_seq'::regclass);


--
-- TOC entry 4656 (class 2604 OID 65601)
-- Name: xeoto max; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.xeoto ALTER COLUMN max SET DEFAULT nextval('public.xeoto_max_seq'::regclass);


--
-- TOC entry 4834 (class 0 OID 65630)
-- Dependencies: 223
-- Data for Name: chitiethd; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.chitiethd (macthd, mahd, max, thuetaixe, ngaythue, ngaytra, dongia, sogiothue, thanhtien) FROM stdin;
1	1	2	Có	2025-05-17 02:34:40.878621	2025-05-19 02:34:40	100000	48.00	5040000
2	1	3	Không	2025-05-17 02:34:40.878621	2025-05-19 02:34:40	150000	48.00	7200000
3	2	1	Không	2025-05-17 21:48:41.663647	2025-05-18 10:48:41	100000	13.00	1300000
4	3	11	Có	2025-05-18 06:30:12	2025-05-18 11:00:12	400000	4.50	1890000
5	4	10	Có	2025-04-30 06:00:03	2025-04-30 11:30:03	320000	5.50	1848000
6	7	5	Có	2025-05-18 01:50:42.857359	2025-05-18 05:50:42	200000	4.00	840000
31	19	2	Có	2024-04-18 20:00:00	2024-04-19 08:00:00	100000	12.00	1260000
32	19	4	Không	2024-04-19 09:00:00	2024-04-19 18:00:00	150000	9.00	1350000
33	20	6	Không	2025-02-02 20:00:00	2025-02-03 08:00:00	200000	12.00	2400000
34	20	7	Có	2025-02-03 09:00:00	2025-02-03 18:00:00	300000	9.00	2835000
35	21	9	Có	2025-05-24 06:30:00	2025-05-24 20:30:00	320000	14.00	4704000
36	21	11	Không	2025-05-25 08:00:00	2025-05-25 18:00:00	400000	10.00	4000000
37	22	10	Không	2024-06-23 11:00:00	2024-06-23 20:00:00	320000	9.00	2880000
38	22	15	Có	2024-06-24 08:00:00	2024-06-24 17:00:00	400000	9.00	3780000
39	23	16	Không	2025-06-14 14:30:00	2025-06-15 02:30:00	400000	12.00	4800000
40	23	17	Có	2025-06-15 08:00:00	2025-06-15 20:00:00	450000	12.00	5670000
41	23	18	Không	2025-06-16 08:00:00	2025-06-16 18:00:00	420000	10.00	4200000
42	24	19	Có	2024-10-28 05:30:00	2024-10-28 17:30:00	500000	12.00	6300000
43	24	20	Không	2024-10-29 08:00:00	2024-10-29 18:00:00	500000	10.00	5000000
44	25	3	Có	2024-07-10 09:00:00	2024-07-10 21:00:00	150000	12.00	1890000
45	25	5	Không	2024-07-11 08:00:00	2024-07-11 18:00:00	200000	10.00	2000000
46	26	11	Không	2024-10-03 14:30:00	2024-10-03 22:30:00	400000	8.00	3200000
47	26	4	Có	2024-10-04 07:00:00	2024-10-04 19:00:00	150000	12.00	1890000
48	27	18	Có	2024-12-21 10:00:00	2024-12-21 22:00:00	420000	12.00	5292000
49	28	12	Không	2025-01-11 09:30:00	2025-01-11 19:30:00	400000	10.00	4000000
50	29	17	Có	2025-03-02 16:50:00	2025-03-03 04:50:00	450000	12.00	5670000
51	32	4	Không	2025-06-01 21:10:34.338703	2025-06-01 23:10:34	150000	2.00	300000
\.


--
-- TOC entry 4832 (class 0 OID 65615)
-- Dependencies: 221
-- Data for Name: hopdong; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.hopdong (mahd, makh, ngaylap, tongtien) FROM stdin;
1	1	2025-05-17 02:35:14.425498	12240000
2	1	2025-05-17 21:49:23.448964	1300000
3	1	2025-05-17 22:05:37.347943	1890000
4	1	2025-04-30 05:40:07	1848000
32	20	2025-06-01 21:10:52.937774	300000
7	3	2025-05-18 01:51:21.668675	840000
19	13	2024-04-18 19:14:00	2610000
20	14	2025-02-02 19:29:00	5235000
21	10	2025-05-24 06:01:00	8704000
22	11	2024-06-23 10:52:00	6660000
23	20	2025-06-14 14:45:00	14670000
24	16	2024-10-28 05:34:00	11300000
25	15	2024-07-10 08:45:00	3890000
26	11	2024-10-03 14:20:00	5090000
27	18	2024-12-21 10:00:00	5292000
28	12	2025-01-11 09:30:00	4000000
29	17	2025-03-02 16:50:00	5670000
\.


--
-- TOC entry 4827 (class 0 OID 24598)
-- Dependencies: 216
-- Data for Name: khachhang; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.khachhang (makh, hoten, cccd, diachi, sdt) FROM stdin;
1	Nguyễn Văn A	123456789012	Hà Nội	0987654321
2	Trần Thị B	987654321098	Bắc Giang	0912345678
3	Lê Văn C	567890123456	TP.HCM	0938765432
10	Hoàng Văn Anh	097863793827	Đồi Ngô - Lục Nam - Bắc Giang	0985432745
11	Nguyễn Văn Hùng	123456789001	Thị trấn Đồi Ngô, huyện Lục Nam, Bắc Giang	0987654321
12	Trần Thị Mai	123456789002	Xã Tiên Hưng, huyện Lục Nam, Bắc Giang	0978123456
13	Lê Văn Khánh	123456789003	Xã Bảo Sơn, huyện Lục Nam, Bắc Giang	0367891234
14	Phạm Thị Hòa	123456789004	Xã Cương Sơn, huyện Lục Nam, Bắc Giang	0912345678
15	Hoàng Văn Quý	123456789005	Xã Trường Sơn, huyện Lục Nam, Bắc Giang	0905123456
16	Vũ Thị Ngọc	123456789006	Xã Nghĩa Phương, huyện Lục Nam, Bắc Giang	0981122334
17	Đặng Văn Toàn	123456789007	Xã Tam Dị, huyện Lục Nam, Bắc Giang	0937894561
18	Ngô Thị Lan	123456789008	Xã Phương Sơn, huyện Lục Nam, Bắc Giang	0941122333
19	Bùi Văn Sơn	123456789009	Xã Đông Hưng, huyện Lục Nam, Bắc Giang	0922233445
20	Đinh Thị Hương	123456789010	Xã Bắc Lũng, huyện Lục Nam, Bắc Giang	0966677889
\.


--
-- TOC entry 4828 (class 0 OID 24608)
-- Dependencies: 217
-- Data for Name: taikhoan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.taikhoan (tentk, matkhau, loaitk) FROM stdin;
An	12345	Admin
hoàng an	12345678910	Nhân viên
nhanvien02	xyz12345	Nhân viên
admin	123456	Admin
nhanvien01	12345	Nhân viên
\.


--
-- TOC entry 4830 (class 0 OID 65598)
-- Dependencies: 219
-- Data for Name: xeoto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.xeoto (max, tenx, theloai, bienso, dongiax, trangthai) FROM stdin;
6	Expander	7 chỗ	30A-30002	200000	Trống
7	Ford Transit	16 chỗ	30B-40001	300000	Trống
8	Ford Transit	16 chỗ	30B-40002	300000	Trống
9	Hyundai Solati	16 chỗ	30B-50001	320000	Trống
12	Hyundai County	29 chỗ	30F-60002	400000	Trống
13	Samco	29 chỗ	30F-70001	450000	Trống
14	Samco	29 chỗ	30F-70002	450000	Trống
15	Thaco	29 chỗ	30F-80001	400000	Trống
16	Thaco	29 chỗ	30F-80002	400000	Trống
17	Samco	35 chỗ	30G-90001	420000	Trống
18	Samco	35 chỗ	30G-90002	420000	Trống
19	Thaco	45 chỗ	30H-00001	500000	Trống
20	Thaco	45 chỗ	30H-00002	500000	Trống
2	Hyundai Accent	5 chỗ	30A-10002	100000	Trống
3	Vios	5 chỗ	30A-20001	150000	Trống
1	Hyundai Accent	5 chỗ	30A-10001	100000	Trống
11	Hyundai County	29 chỗ	30F-60001	400000	Trống
10	Hyundai Solati	16 chỗ	30B-50002	320000	Trống
5	Expander	7 chỗ	30A-30001	200000	Trống
4	Vios	5 chỗ	30A-20002	150000	Bảo trì
\.


--
-- TOC entry 4845 (class 0 OID 0)
-- Dependencies: 222
-- Name: chitiethd_macthd_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.chitiethd_macthd_seq', 51, true);


--
-- TOC entry 4846 (class 0 OID 0)
-- Dependencies: 220
-- Name: hopdong_mahd_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hopdong_mahd_seq', 32, true);


--
-- TOC entry 4847 (class 0 OID 0)
-- Dependencies: 215
-- Name: khachhang_makh_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.khachhang_makh_seq', 20, true);


--
-- TOC entry 4848 (class 0 OID 0)
-- Dependencies: 218
-- Name: xeoto_max_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.xeoto_max_seq', 20, true);


--
-- TOC entry 4678 (class 2606 OID 65637)
-- Name: chitiethd chitiethd_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd
    ADD CONSTRAINT chitiethd_pkey PRIMARY KEY (macthd);


--
-- TOC entry 4676 (class 2606 OID 65622)
-- Name: hopdong hopdong_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hopdong
    ADD CONSTRAINT hopdong_pkey PRIMARY KEY (mahd);


--
-- TOC entry 4666 (class 2606 OID 24607)
-- Name: khachhang khachhang_cccd_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.khachhang
    ADD CONSTRAINT khachhang_cccd_key UNIQUE (cccd);


--
-- TOC entry 4668 (class 2606 OID 24605)
-- Name: khachhang khachhang_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.khachhang
    ADD CONSTRAINT khachhang_pkey PRIMARY KEY (makh);


--
-- TOC entry 4670 (class 2606 OID 24615)
-- Name: taikhoan taikhoan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.taikhoan
    ADD CONSTRAINT taikhoan_pkey PRIMARY KEY (tentk);


--
-- TOC entry 4672 (class 2606 OID 65607)
-- Name: xeoto xeoto_bienso_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.xeoto
    ADD CONSTRAINT xeoto_bienso_key UNIQUE (bienso);


--
-- TOC entry 4674 (class 2606 OID 65605)
-- Name: xeoto xeoto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.xeoto
    ADD CONSTRAINT xeoto_pkey PRIMARY KEY (max);


--
-- TOC entry 4682 (class 2620 OID 65648)
-- Name: chitiethd tg_capnhat_tongtien; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER tg_capnhat_tongtien AFTER INSERT OR DELETE OR UPDATE ON public.chitiethd FOR EACH ROW EXECUTE FUNCTION public.tinh_tong_tien();


--
-- TOC entry 4680 (class 2606 OID 65638)
-- Name: chitiethd fk_hd_ct; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd
    ADD CONSTRAINT fk_hd_ct FOREIGN KEY (mahd) REFERENCES public.hopdong(mahd);


--
-- TOC entry 4679 (class 2606 OID 65623)
-- Name: hopdong fk_khachhang_hd; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hopdong
    ADD CONSTRAINT fk_khachhang_hd FOREIGN KEY (makh) REFERENCES public.khachhang(makh);


--
-- TOC entry 4681 (class 2606 OID 65643)
-- Name: chitiethd fk_xe_ct; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.chitiethd
    ADD CONSTRAINT fk_xe_ct FOREIGN KEY (max) REFERENCES public.xeoto(max);


-- Completed on 2025-06-02 21:40:05

--
-- PostgreSQL database dump complete
--

-- Completed on 2025-06-02 21:40:05

--
-- PostgreSQL database cluster dump complete
--

