/*
In Neon, databases are stored on branches. By default, a project has one branch and one database.
You can select the branch and database to use from the drop-down menus above.

Try generating sample data and querying it by running the example statements below, or click
New Query to clear the editor.
*/

-- Category Table 
CREATE TABLE IF NOT EXISTS category (
  category_id SERIAL PRIMARY KEY,
  category_name VARCHAR(45) NOT NULL UNIQUE,
  category_description VARCHAR(45) DEFAULT NULL,
  UNIQUE (category_id)
);

-- INSERT Catrories
INSERT INTO category (category_name, category_description) VALUES
    ('Electronics', 'Electronic devices and components'),
    ('Machinery', 'Industrial machinery and tools'),
    ('Clothing', 'Apparel and accessories'),
    ('Medical Equipment', 'Healthcare devices and equipment'),
    ('Automobile Parts', 'Car and truck components'),
    ('Groceries', 'Food and beverage products');


-- Industry Table
CREATE TABLE industry (
    industry_id SERIAL PRIMARY KEY,
    industry_name VARCHAR(45) NOT NULL UNIQUE
);

-- Insert Industries
INSERT INTO industry (industry_name) VALUES 
    ('Technology'),
    ('Manufacturing'),
    ('Retail'),
    ('Healthcare'),
    ('Automotive'),
    ('Food & Beverage');

-- User Table
CREATE TABLE "user" (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(45) NOT NULL UNIQUE,
    user_name VARCHAR(100) NOT NULL,
    user_lastName VARCHAR(100) NOT NULL,
    user_email VARCHAR(45) NOT NULL UNIQUE,
    user_password VARCHAR(250) NOT NULL,
    user_level TEXT NOT NULL DEFAULT '1' CHECK (user_level IN ('1', '2', '3', '4'))
);

-- Insert Users
INSERT INTO "user" (username, user_name, user_lastName, user_email, user_password, user_level) VALUES 
    ('saraltr', 'Sara', 'Latorre', 'slatorre@gmail.com', 'password1', '3'),
    ('asmith', 'Alice', 'Smith', 'asmith@gmail.com', 'password2', '2'),
    ('bjohnson', 'Bob', 'Johnson', 'bjohnson@gmail.com', 'password3', '1'),
    ('lmoreau', 'Lucie', 'Moreau', 'lmoreau@gmail.com', 'password4', '1'),
    ('jwilson', 'Jessica', 'Wilson', 'jwilson@gmail.com', 'password5', '1'),
    ('tdavis', 'Thomas', 'Davis', 'tdavis@gmail.com', 'password6', '1'),
    ('ewhite', 'Emily', 'White', 'ewhite@gmail.com', 'password7', '2'),
    ('cmoore', 'Lea', 'Moore', 'cmoore@gmail.com', 'password8', '2');


-- Company Table
CREATE TABLE company (
    company_id SERIAL PRIMARY KEY,
    company_name VARCHAR(70) NOT NULL,
    company_phone VARCHAR(45) NOT NULL,
    company_email VARCHAR(45) NOT NULL UNIQUE,
    company_address TEXT NOT NULL,
    company_description VARCHAR(100) DEFAULT NULL,
    industry_id INT NOT NULL,
    user_id INT NOT NULL,
    CONSTRAINT fk_company_industry FOREIGN KEY (industry_id) REFERENCES industry(industry_id),
    CONSTRAINT fk_company_user FOREIGN KEY (user_id) REFERENCES "user"(user_id)
);

-- Insert Companies
INSERT INTO company (company_name, company_phone, company_email, company_address, company_description, industry_id, user_id) VALUES
    ('Tech Solutions', '123-456-7890', 'info@techsolutions.com', '123 Tech Street', 'IT services company', 1, 2),
    ('Manufacture Plus', '987-654-3210', 'contact@manufactureplus.com', '456 Industry Rd', 'Industrial manufacturing', 2, 2),
    ('Retail World', '456-123-7890', 'support@retailworld.com', '789 Commerce Ave', 'Retail business', 3, 8),
    ('MediTech', '321-654-9870', 'info@meditech.com', '12 Health St', 'Medical technology solutions', 4, 2),
    ('Auto Parts Inc.', '654-321-9870', 'sales@autoparts.com', '88 Auto Blvd', 'Car parts distributor', 5, 7),
    ('Fresh Foods Ltd.', '777-888-9999', 'contact@freshfoods.com', '222 Food Market St', 'Organic food supplier', 6, 8);


-- Supplier Table
CREATE TABLE supplier (
    supplier_id SERIAL PRIMARY KEY,
    category_id INT NOT NULL,
    supplier_name VARCHAR(45) NOT NULL,
    supplier_phone VARCHAR(45) NOT NULL,
    supplier_email VARCHAR(45) NOT NULL,
    supplier_address VARCHAR(45) NOT NULL,
    supplier_description VARCHAR(45) NOT NULL,
    user_id INT NOT NULL,
    company_id INT DEFAULT NULL,
    CONSTRAINT fk_supplier_company FOREIGN KEY (company_id) REFERENCES company(company_id),
    CONSTRAINT fk_supplier_user FOREIGN KEY (user_id) REFERENCES "user"(user_id),
    CONSTRAINT fk_supplier_category FOREIGN KEY (category_id) REFERENCES category(category_id)
);

-- Insert Suppliers
INSERT INTO supplier (category_id, supplier_name, supplier_phone, supplier_email, supplier_address, supplier_description, user_id, company_id) VALUES
    (1, 'Supplier1', '111-222-3333', 'supplier1@example.com', '123 Supplier St', 'Electronics supplier', 1, 1),
    (2, 'Supplier2', '222-333-4444', 'supplier2@example.com', '456 Industry Rd', 'Machinery supplier', 2, 2),
    (3, 'Supplier3', '333-444-5555', 'supplier3@example.com', '789 Retail St', 'Clothing supplier', 3, 3),
    (4, 'MediTech', '444-555-6666', 'meditech@example.com', '12 Health St', 'Medical equipment supplier', 4, 4),
    (5, 'Auto Parts Inc.', '555-666-7777', 'autoparts@example.com', '88 Auto Blvd', 'Automobile parts distributor', 5, 5),
    (6, 'Fresh Foods Ltd.', '666-777-8888', 'freshfoods@example.com', '222 Food Market St', 'Organic food supplier', 6, 6);


-- Order Table
CREATE TABLE "order" (
    order_id SERIAL PRIMARY KEY,
    supplier_id INT NOT NULL,
    user_id INT NOT NULL,
    order_date DATE NOT NULL,
    order_status TEXT NOT NULL CHECK (order_status IN ('pending', 'approved', 'shipped', 'completed', 'canceled')),
    order_amount DECIMAL(10,2) NOT NULL,
    CONSTRAINT fk_order_supplier FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id),
    CONSTRAINT fk_order_user FOREIGN KEY (user_id) REFERENCES "user"(user_id)
);

-- Insert Orders
INSERT INTO "order" (supplier_id, user_id, order_date, order_status, order_amount) VALUES
    (1, 1, '2024-03-01', 'pending', 500.00),
    (2, 2, '2024-03-02', 'approved', 1500.00),
    (3, 3, '2024-03-03', 'shipped', 800.00),
    (4, 4, '2024-03-04', 'completed', 3200.00),
    (5, 5, '2024-03-05', 'pending', 4500.00),
    (6, 6, '2024-03-06', 'approved', 5600.00);

-- Quote Table
CREATE TABLE quote (
    quotes_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    company_id INT DEFAULT NULL,
    supplier_id INT NOT NULL,
    quotes_date DATE NOT NULL,
    quote_details TEXT NOT NULL,
    quote_price DECIMAL(10,2) NOT NULL,
    CONSTRAINT fk_quote_company FOREIGN KEY (company_id) REFERENCES company(company_id),
    CONSTRAINT fk_quote_supplier FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id),
    CONSTRAINT fk_quote_user FOREIGN KEY (user_id) REFERENCES "user"(user_id)
);

-- Insert Quotes
INSERT INTO quote (user_id, company_id, supplier_id, quotes_date, quote_details, quote_price) VALUES
    (1, 1, 1, '2024-03-01', 'Request for 50 circuit boards', 2500.00),
    (2, 2, 2, '2024-03-02', 'Quote for 10 industrial drills', 12000.00),
    (3, 3, 3, '2024-03-03', 'Bulk order of 500 t-shirts', 7500.00),
    (2, 4, 4, '2024-03-04', 'Inquiry for 5 MRI machines', 500000.00),
    (7, 5, 5, '2024-03-05', 'Quote for 1000 brake pads', 15000.00),
    (8, 6, 6, '2024-03-06', 'Bulk order of 2000 organic fruits', 12000.00);