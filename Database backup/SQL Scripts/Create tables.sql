Create database [Food Feast];

Use [Food Feast]

Create table [customer](
[customer_id] tinyint identity(1,1) primary key ,
[name] varchar(50) not null ,
[email] varchar(50) unique ,
[phone] varchar(50) not null unique ,
[password] varchar(50) not null
);

Drop table customer;

Create table [address](
[customer_id] tinyint foreign key references customer(customer_id) Primary key,
[area] varchar(50) not null ,
[street] varchar(50) ,
[building] varchar(50) not null ,
[floor] varchar(50)
);

Drop table [address];

Create table [review](
[customer_id] tinyint foreign key references customer(customer_id),
[review_id] tinyint identity(1,1),
[type] varchar(50) not null ,
[city] varchar(50) ,
[review] text not null ,
[date] datetime not null, 
constraint pk_customer_review_id primary key (customer_id , review_id)
);

Create table [food_item](
[item_id] int identity(1,1) primary key,
[image] varchar(100) ,
[name] varchar(50) not null,
[description] text ,
[sale_price] float not null,
[category] tinyint foreign key references [category](category_id) 
);

Create table [category](
[category_id] tinyint identity(1,1) primary key,
[name] varchar(50) not null unique,
);

Create table [payment_method](
[method_id] tinyint identity(1,1) primary key,
[name] varchar(50) not null unique,
);

Create table [order](
[order_id] uniqueidentifier primary key,
[customer_id] tinyint foreign key references customer(customer_id),
[payment_method] tinyint foreign key references [payment_method](method_id),
[total_price] real not null ,
[date] datetime not null
);

Create table [order_item](
[order_id] uniqueidentifier foreign key references [order](order_id),
[item_id] int foreign key references food_item(item_id),
[quantity] bit not null,
constraint pk_order_item_id primary key (order_id , item_id)
);

Create table cart(
item_id int foreign key references food_item(item_id) primary key,
quantity int 
);






