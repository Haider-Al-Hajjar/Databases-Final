/*  
    =============================== 
              TRG01 Function
    ===============================
*/
CREATE OR REPLACE PROCEDURE prc_create_trg01 (
    table_name_in VARCHAR2
) AS
    v_sql CLOB;
BEGIN
    v_sql := 'CREATE OR REPLACE TRIGGER '
             || table_name_in
             || '_trg01 '
             || 'BEFORE INSERT OR UPDATE ON '
             || table_name_in
             || ' '
             || 'FOR EACH ROW '
             || 'BEGIN '
             || 'IF INSERTING THEN '
             || ':NEW.'
             || lower(table_name_in)
             || '_crtd_id := USER; '
             || ':NEW.'
             || lower(table_name_in)
             || '_crtd_dt := SYSDATE; '
             || 'END IF; '
             || ':NEW.'
             || lower(table_name_in)
             || '_updt_id := USER; '
             || ':NEW.'
             || lower(table_name_in)
             || '_updt_dt := SYSDATE; '
             || 'END;';

    EXECUTE IMMEDIATE v_sql;
END;
/
/*  
    =============================== 
              TRG02 Function
    ===============================
*/
CREATE OR REPLACE PROCEDURE prc_create_trg02 (
    table_name_in VARCHAR2,
    guid_name_in  VARCHAR2
) AS
    v_sql VARCHAR2(2000);
BEGIN
    v_sql := ' CREATE OR REPLACE TRIGGER '
             || table_name_in
             || '_trg02';
    v_sql := v_sql
             || ' BEFORE INSERT OR UPDATE ON '
             || table_name_in;
    v_sql := v_sql || ' FOR EACH ROW';
    v_sql := v_sql || ' BEGIN';
    v_sql := v_sql
             || ' IF inserting and :new.'
             || guid_name_in
             || ' is null THEN';
    v_sql := v_sql
             || ' :new.'
             || guid_name_in
             || ' := sys_guid();';
    v_sql := v_sql || ' END IF;';
    v_sql := v_sql || ' IF updating THEN';
    v_sql := v_sql
             || ' :new.'
             || guid_name_in
             || ' := :old.'
             || guid_name_in
             || ';';

    v_sql := v_sql || ' END IF;';
    v_sql := v_sql || ' END;';
    EXECUTE IMMEDIATE v_sql;
END;
/

CREATE OR REPLACE PROCEDURE prc_create_all_triggers AS
    v_pk_col VARCHAR2(128);
BEGIN
    FOR t IN (
        SELECT
            table_name
        FROM
            user_tables
    ) LOOP

        -- get primary key column
        SELECT
            column_name
        INTO v_pk_col
        FROM
            user_cons_columns
        WHERE
                constraint_name = (
                    SELECT
                        constraint_name
                    FROM
                        user_constraints
                    WHERE
                            table_name = t.table_name
                        AND constraint_type = 'P'
                )
            AND ROWNUM = 1;

        -- TRG01
        prc_create_trg01(t.table_name);

        -- TRG02
        prc_create_trg02(t.table_name, v_pk_col);
    END LOOP;
END;
/

/*
    --------------------
        Create Tables   
    --------------------
*/

-- strong entities

-- 1
CREATE TABLE customer (
    customer_id         VARCHAR2(32) NOT NULL,
    customer_first_name VARCHAR2(30),
    customer_last_name  VARCHAR2(30),
    customer_crtd_id    VARCHAR2(40) NOT NULL,
    customer_crtd_dt    DATE NOT NULL,
    customer_updt_id    VARCHAR2(40) NOT NULL,
    customer_updt_dt    DATE NOT NULL,
    CONSTRAINT customer_pk PRIMARY KEY ( customer_id ) ENABLE
);

-- 2
CREATE TABLE product (
    product_id      VARCHAR2(32) NOT NULL,
    product_name    VARCHAR2(50),
    product_crtd_id VARCHAR2(40) NOT NULL,
    product_crtd_dt DATE NOT NULL,
    product_updt_id VARCHAR2(40) NOT NULL,
    product_updt_dt DATE NOT NULL,
    CONSTRAINT product_pk PRIMARY KEY ( product_id ) ENABLE
);

-- 3
CREATE TABLE zip (
    zipcode      VARCHAR2(5) NOT NULL,
    zipcode_type VARCHAR2(20),
    city         VARCHAR2(30),
    state        VARCHAR2(2),
    zip_crtd_id  VARCHAR2(40) NOT NULL,
    zip_crtd_dt  DATE NOT NULL,
    zip_updt_id  VARCHAR2(40) NOT NULL,
    zip_updt_dt  DATE NOT NULL,
    CONSTRAINT zip_pk PRIMARY KEY ( zipcode ) ENABLE
);


-- 4
CREATE TABLE category_type (
    category_type_id      VARCHAR2(32) NOT NULL,
    category_type_desc    VARCHAR2(100),
    category_type_crtd_id VARCHAR2(40) NOT NULL,
    category_type_crtd_dt DATE NOT NULL,
    category_type_updt_id VARCHAR2(40) NOT NULL,
    category_type_updt_dt DATE NOT NULL,
    CONSTRAINT category_type_pk PRIMARY KEY ( category_type_id ) ENABLE
);


-- 5
CREATE TABLE order_status (
    order_status_id      VARCHAR2(32) NOT NULL,
    order_status_desc    VARCHAR2(30),
    order_status_crtd_id VARCHAR2(40) NOT NULL,
    order_status_crtd_dt DATE NOT NULL,
    order_status_updt_id VARCHAR2(40) NOT NULL,
    order_status_updt_dt DATE NOT NULL,
    CONSTRAINT order_status_pk PRIMARY KEY ( order_status_id ) ENABLE
);

-- weak entities

-- 6
CREATE TABLE address (
    address_id      VARCHAR2(32) NOT NULL,
    address_line1   VARCHAR2(100),
    address_line2   VARCHAR2(100),
    address_line3   VARCHAR2(100),
    address_zipcode VARCHAR2(5),
    address_crtd_id VARCHAR2(40) NOT NULL,
    address_crtd_dt DATE NOT NULL,
    address_updt_id VARCHAR2(40) NOT NULL,
    address_updt_dt DATE NOT NULL,
    CONSTRAINT address_pk PRIMARY KEY ( address_id ) ENABLE
);

ALTER TABLE address
    ADD CONSTRAINT address_fk1
        FOREIGN KEY ( address_zipcode )
            REFERENCES zip ( zipcode )
        ENABLE;


-- 7
CREATE TABLE customer_address (
    customer_address_id          VARCHAR2(32) NOT NULL,
    customer_address_customer_id VARCHAR2(32),
    customer_address_address_id  VARCHAR2(32),
    customer_address_dflt        NUMBER(1),
    customer_address_actv_ind    NUMBER(1),
    customer_address_crtd_id     VARCHAR2(40) NOT NULL,
    customer_address_crtd_dt     DATE NOT NULL,
    customer_address_updt_id     VARCHAR2(40) NOT NULL,
    customer_address_updt_dt     DATE NOT NULL,
    CONSTRAINT customer_address_pk PRIMARY KEY ( customer_address_id ) ENABLE
);

ALTER TABLE customer_address
    ADD CONSTRAINT customer_address_fk1
        FOREIGN KEY ( customer_address_customer_id )
            REFERENCES customer ( customer_id )
        ENABLE;

ALTER TABLE customer_address
    ADD CONSTRAINT customer_address_fk2
        FOREIGN KEY ( customer_address_address_id )
            REFERENCES address ( address_id )
        ENABLE;        
        
-- 8
CREATE TABLE orders (
    orders_id                  VARCHAR2(32) NOT NULL,
    orders_customer_id         VARCHAR2(32),
    orders_date                DATE,
    orders_order_status_id     VARCHAR2(32),
    orders_customer_address_id VARCHAR2(32),
    orders_crtd_id             VARCHAR2(40) NOT NULL,
    orders_crtd_dt             DATE NOT NULL,
    orders_updt_id             VARCHAR2(40) NOT NULL,
    orders_updt_dt             DATE NOT NULL,
    CONSTRAINT orders_pk PRIMARY KEY ( orders_id ) ENABLE
);

ALTER TABLE orders
    ADD CONSTRAINT orders_fk1
        FOREIGN KEY ( orders_customer_id )
            REFERENCES customer ( customer_id )
        ENABLE;

ALTER TABLE orders
    ADD CONSTRAINT orders_fk2
        FOREIGN KEY ( orders_order_status_id )
            REFERENCES order_status ( order_status_id )
        ENABLE;

ALTER TABLE orders
    ADD CONSTRAINT orders_fk3
        FOREIGN KEY ( orders_customer_address_id )
            REFERENCES customer_address ( customer_address_id )
        ENABLE;
        
-- 9
CREATE TABLE order_line (
    order_line_id         VARCHAR2(32) NOT NULL,
    order_line_order_id   VARCHAR2(32),
    order_line_product_id VARCHAR2(32),
    order_line_qty        NUMBER(5),
    order_line_unit_price NUMBER(9, 2),
    order_line_crtd_id    VARCHAR2(40) NOT NULL,
    order_line_crtd_dt    DATE NOT NULL,
    order_line_updt_id    VARCHAR2(40) NOT NULL,
    order_line_updt_dt    DATE NOT NULL,
    CONSTRAINT order_line_pk PRIMARY KEY ( order_line_id ) ENABLE
);

ALTER TABLE order_line
    ADD CONSTRAINT order_line_fk1
        FOREIGN KEY ( order_line_order_id )
            REFERENCES orders ( orders_id )
        ENABLE;

ALTER TABLE order_line
    ADD CONSTRAINT order_line_fk2
        FOREIGN KEY ( order_line_product_id )
            REFERENCES product ( product_id )
        ENABLE;
        
-- 10
CREATE TABLE product_price (
    product_price_id         VARCHAR2(32) NOT NULL,
    product_price_product_id VARCHAR2(32),
    product_price_eff_date   DATE,
    product_price_price      NUMBER(9, 2),
    product_price_crtd_id    VARCHAR2(40) NOT NULL,
    product_price_crtd_dt    DATE NOT NULL,
    product_price_updt_id    VARCHAR2(40) NOT NULL,
    product_price_updt_dt    DATE NOT NULL,
    CONSTRAINT product_price_pk PRIMARY KEY ( product_price_id ) ENABLE
);

ALTER TABLE product_price
    ADD CONSTRAINT product_price_fk1
        FOREIGN KEY ( product_price_product_id )
            REFERENCES product ( product_id )
        ENABLE;
     
-- 11
CREATE TABLE category (
    category_id               VARCHAR2(32) NOT NULL,
    category_name             VARCHAR2(200),
    category_prnt_category_id VARCHAR2(32),
    category_category_type_id VARCHAR2(32),
    category_crtd_id          VARCHAR2(40) NOT NULL,
    category_crtd_dt          DATE NOT NULL,
    category_updt_id          VARCHAR2(40) NOT NULL,
    category_updt_dt          DATE NOT NULL,
    CONSTRAINT category_pk PRIMARY KEY ( category_id ) ENABLE
);

ALTER TABLE category
    ADD CONSTRAINT category_fk1
        FOREIGN KEY ( category_prnt_category_id )
            REFERENCES category ( category_id )
        ENABLE;

ALTER TABLE category
    ADD CONSTRAINT category_fk2
        FOREIGN KEY ( category_category_type_id )
            REFERENCES category_type ( category_type_id )
        ENABLE;
        
-- 12
CREATE TABLE product_category (
    product_category_id          VARCHAR2(32) NOT NULL,
    product_category_product_id  VARCHAR2(32),
    product_category_category_id VARCHAR2(32),
    product_category_eff_date    DATE,
    product_category_crtd_id     VARCHAR2(40) NOT NULL,
    product_category_crtd_dt     DATE NOT NULL,
    product_category_updt_id     VARCHAR2(40) NOT NULL,
    product_category_updt_dt     DATE NOT NULL,
    CONSTRAINT product_category_pk PRIMARY KEY ( product_category_id ) ENABLE
);

ALTER TABLE product_category
    ADD CONSTRAINT product_category_fk1
        FOREIGN KEY ( product_category_product_id )
            REFERENCES product ( product_id )
        ENABLE;
/*
    ------------
        Views   
    ------------
*/

-- 1
CREATE OR REPLACE VIEW vw_product_price AS
    SELECT
        p.product_id,
        p.product_name,
        pp.product_price_id,
        pp.product_price_price,
        pp.product_price_eff_date AS startdate,
        LEAD(pp.product_price_eff_date)
        OVER(PARTITION BY p.product_id
             ORDER BY
                 pp.product_price_eff_date
        )                         AS enddate
    FROM
             product p
        JOIN product_price pp ON p.product_id = pp.product_price_product_id;

-- 2
CREATE OR REPLACE VIEW vw_product_max_price_increase AS
WITH price_diff AS (
    SELECT
        p.product_id,
        p.product_name,
        pp.product_price_id,
        pp.product_price_eff_date,
        pp.product_price_price,
        LAG(pp.product_price_price) OVER (
            PARTITION BY p.product_id
            ORDER BY pp.product_price_eff_date
        ) AS prev_price
    FROM product p
    JOIN product_price pp
        ON p.product_id = pp.product_price_product_id
),
calc AS (
    SELECT
        product_id,
        product_name,
        product_price_id,
        CASE
            WHEN prev_price IS NULL OR prev_price = 0 THEN NULL
            ELSE (product_price_price - prev_price) / prev_price
        END AS pct_increase
    FROM price_diff
),
ranked AS (
    SELECT
        product_id,
        product_name,
        product_price_id,
        pct_increase,
        DENSE_RANK() OVER (
            PARTITION BY product_id
            ORDER BY pct_increase DESC NULLS LAST
        ) AS rnk
    FROM calc
)
SELECT
    product_id,
    product_name,
    product_price_id,
    pct_increase
FROM ranked
WHERE rnk = 1;/*
    -----------------------
        Create Triggers     
    -----------------------
*/

BEGIN
    prc_create_all_triggers();
END;
/

/*
    Import zip.csv using sqldeveloper
*/

/*
    ----------------------------
        New OrderStatus Rows  
    ----------------------------
*/
INSERT INTO order_status ( order_status_desc ) VALUES ( 'New' );

INSERT INTO order_status ( order_status_desc ) VALUES ( 'In-progress' );

INSERT INTO order_status ( order_status_desc ) VALUES ( 'Shipped' );