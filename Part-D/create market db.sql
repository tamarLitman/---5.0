create database Market

create table supplier
(
	supplier_Id int identity(1,1) primary key,
	supllier_Name varchar(10),
	company_Name varchar(10),
	phone_Number varchar(10),
	representative varchar(10),
)
create table Stock
(
	Prod_Id int identity(1,1) primary key,
	Prod_Name varchar(10),
	Price numeric(3,2),
	Min_Amount int,
	supplier_Id int
	FOREIGN KEY (supplier_Id) REFERENCES supplier(supplier_Id) 
)
ALTER TABLE Stock
DROP COLUMN ColumnName;
create table states
(
	state_Id int primary key,
	state_Description varchar(10)
)
create table orders
(
	order_Id int identity(1,1) primary key,
	order_state_Id int,
	FOREIGN KEY (order_state_Id) REFERENCES states(state_Id) 
)
create table order_Stock
(
	order_Id int,
	Prod_Id int,
	FOREIGN KEY (order_Id) REFERENCES orders(order_Id), 
	FOREIGN KEY (Prod_Id) REFERENCES stock(Prod_Id) 
)
alter table states alter column state_Description varchar(15)
insert into states
(
	state_Id,
	state_Description
)
values
(1,'done'),
(2,'in progress'),
(3,'completed')

insert into orders
(
	order_state_Id
)
values
(1),
(2),
(3)
