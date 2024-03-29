USE [ShopeeFood]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[addresses]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[addresses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[PhoneNumbers] [nvarchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_addresses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[areas]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[areas](
	[AreaID] [int] IDENTITY(1,1) NOT NULL,
	[NameofArea] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_areas] PRIMARY KEY CLUSTERED 
(
	[AreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailAreas]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailAreas](
	[IDDetailsArea] [int] IDENTITY(1,1) NOT NULL,
	[NameofDetailsArea] [nvarchar](max) NOT NULL,
	[AreaID] [int] NULL,
 CONSTRAINT [PK_detailAreas] PRIMARY KEY CLUSTERED 
(
	[IDDetailsArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[foods]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[foods](
	[FoodId] [int] IDENTITY(1,1) NOT NULL,
	[NameofFood] [nvarchar](max) NOT NULL,
	[Images] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[TypeofFood] [nvarchar](max) NOT NULL,
	[RestaurantID] [int] NOT NULL,
 CONSTRAINT [PK_foods] PRIMARY KEY CLUSTERED 
(
	[FoodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

alter column TypeofFood



ALTER TABLE foods
ADD Quantity int;

/****** Object:  Table [dbo].[invoiceDetails]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoiceDetails](
	[InvoicesID] [int] NOT NULL,
	[FoodId] [int] NOT NULL,
	[NameofFood] [nvarchar](max) NOT NULL,
	[Images] [nvarchar](max) NOT NULL,
	[Price] [real] NOT NULL,
	[Numbers] [int] NOT NULL,
 CONSTRAINT [PK_invoiceDetails] PRIMARY KEY CLUSTERED 
(
	[InvoicesID] ASC,
	[FoodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoices]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoices](
	[InvoicesID] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_invoices] PRIMARY KEY CLUSTERED 
(
	[InvoicesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[promotions]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[promotions](
	[PromotionID] [int] IDENTITY(1,1) NOT NULL,
	[PromotionName] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_promotions] PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[restaurants](
	[RestaurantID] [int] IDENTITY(1,1) NOT NULL,
	[NameofRestaurant] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[OpenTime] [datetime2](7) NOT NULL,
	[CloseTime] [datetime2](7) NOT NULL,
	[AreaID] [int] NOT NULL,
	[ID] [int] NULL,
 CONSTRAINT [PK_restaurants] PRIMARY KEY CLUSTERED 
(
	[RestaurantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tokens]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tokens](
	[TokenID] [int] IDENTITY(1,1) NOT NULL,
	[refreshToken] [nvarchar](max) NULL,
	[refreshTokenExpireTime] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_tokens] PRIMARY KEY CLUSTERED 
(
	[TokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[types]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NameofType] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_types] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/9/2022 11:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [nvarchar](12) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Sex] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [varchar](11) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221109033040_ShopeeFood', N'7.0.0')
GO
SET IDENTITY_INSERT [dbo].[areas] ON 

INSERT [dbo].[areas] ([AreaID], [NameofArea]) VALUES (1, N'TP.HCM')
INSERT [dbo].[areas] ([AreaID], [NameofArea]) VALUES (2, N'Hà Nội')
INSERT [dbo].[areas] ([AreaID], [NameofArea]) VALUES (3, N'Cần Thơ')
INSERT [dbo].[areas] ([AreaID], [NameofArea]) VALUES (4, N'Đà Nẵng')
INSERT [dbo].[areas] ([AreaID], [NameofArea]) VALUES (5, N'Hải Phòng')
SET IDENTITY_INSERT [dbo].[areas] OFF
GO
SET IDENTITY_INSERT [dbo].[detailAreas] ON 

INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (1, N'Quận 1', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (2, N'Quận 2', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (3, N'Quận 3', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (4, N'Quận 4', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (5, N'Quận 5', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (6, N'Quận 6 ', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (7, N'Quận 7 ', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (8, N'Quận 8 ', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (9, N'Quận 9', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (10, N'Quận 10', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (11, N'Quận 11', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (12, N'Quận 12', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (13, N'Bình Thạnh', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (14, N'Tân Bình', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (15, N'Tân Phú', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (16, N'Phú Nhuận', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (17, N'Bình Tân', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (18, N'Thủ Đức', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (19, N'Củ chi', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (20, N'Hóc Môn', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (21, N'Bình Chánh', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (22, N'Cần Giờ', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (23, N'Nhà Bè', 1)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (24, N'Ba Đình', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (25, N'Cầu Giấy', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (26, N'Đống Đa', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (27, N'Hà Đông', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (28, N'Hoàng Kiếm', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (29, N'Hoàng Mai', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (30, N'Long Biên', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (31, N'Tây Hồ', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (32, N'Thanh Trì', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (33, N'Bắc Từ Liêm', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (34, N'Nam Từ Liêm', 2)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (35, N'Quận Ninh Kiều', 3)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (36, N'Quận Bình Thủy', 3)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (37, N'Quận Cái Răng', 3)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (38, N'Quận Ô Môi', 3)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (39, N'Quận Cẩm Lệ', 4)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (40, N'Quận Hải Châu', 4)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (41, N'Quận Liên Chiểu', 4)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (42, N'Quận Sơn Trà', 4)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (43, N'Quận Dương Kinh', 5)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (44, N'Quận Đồ Sơn', 5)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (45, N'Quận Hải An', 5)
INSERT [dbo].[detailAreas] ([IDDetailsArea], [NameofDetailsArea], [AreaID]) VALUES (46, N'Quận Kiến Anh', 5)
SET IDENTITY_INSERT [dbo].[detailAreas] OFF
GO
SET IDENTITY_INSERT [dbo].[foods] ON 

INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (2, N'Bún bò Huế đặc biệt', N'https://images.foody.vn/res/g100004/1000036575/s120x120/0982396e-0b87-4276-8669-f8d98f1b-9dc8310f-211105162312.jpeg', N'Gồm bò, giò, chả', 39000, N'16', 2)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (4, N'Cafe sữa', N'https://images.foody.vn/res/g100004/1000036575/s120x120/ce84fba3-8d94-4770-bd0c-0a0ac13e-c65611c7-211105162627.jpeg', N' ', 17000, N'17', 2)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (5, N'Hồng trà dâu', N'https://images.foody.vn/res/g100004/1000036575/s120x120/e1113645-dede-49a9-9f53-35ab876c-ed7c5a61-211105162837.jpeg', N'Hồng trà hương dâu kèm Topping', 25000, N'17', 2)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (6, N'Trà atiso việt quất', N'	https://images.foody.vn/res/g100004/1000036575/s12…dfc-4899-8fb5-1cabed5e-7ee35572-211116182710.jpeg', N'Trà atiso việt quất kèm Topping', 25000, N'17', 2)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (8, N'Combo 2 người', N'https://images.foody.vn/res/g100004/1000036575/s12…b87-4276-8669-f8d98f1b-9dc8310f-211105162312.jpeg', N'2 bún bò đặc biệt + 2 trà chanh hoa đậu biếc', 99000, N'16', 2)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (9, N'Combo Bún bò đặc biệt+Trà vải', N'https://images.foody.vn/res/g100004/1000036575/s12…b87-4276-8669-f8d98f1b-9dc8310f-211105162312.jpeg', N'Hồng trà hương vải kèm topping', 62000, N'16', 2)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (10, N'Combo Bún bò đặc biệt+Trà atiso việt quất', N'Combo Bún bò đặc biệt+Trà atiso việt quất', N'Trà atiso việt quất kèm topping', 62000, N'16', 2)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (11, N'Bánh mì đặc biệt', N'	https://images.foody.vn/res/g109/1082517/s120x120/…13e-4a97-af85-07ba7673-e2826f80-210625190531.jpeg', N' ', 27000, N'19', 3)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (12, N'Bánh Mì Heo Quay', N'https://images.foody.vn/res/g109/1082517/s120x120/…e16-4c5e-b7a3-14ce5554-5116694c-211215223311.jpeg', N' ', 27000, N'19', 3)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (13, N'Bánh mì chả cá', N'https://images.foody.vn/res/g109/1082517/s120x120/…cb1-4622-8aa4-139cc666-32b6ba43-210625190314.jpeg', N' ', 22000, N'19', 3)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (14, N'Bánh mì thịt Chả Đầy Đủ', N'https://images.foody.vn/res/g109/1082517/s120x120/…21e-4f5c-ac1f-0b0f9f9d-1de8469e-210625190450.jpeg', N' ', 23000, N'19', 3)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (15, N'Hamburger Bò Trứng', N'https://images.foody.vn/res/g109/1082517/s120x120/…a46-411d-a1ff-73f42d5d-4036d74c-210625192655.jpeg', N' ', 27000, N'20', 3)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (16, N'Hamburger Thịt Chả Trứng', N'https://images.foody.vn/res/g109/1082517/s120x120/…786-4261-8ae6-8be81359-228e102d-210625195922.jpeg', N' ', 25000, N'20', 3)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (17, N'Humburger Bò + Thịt Chả', N'https://images.foody.vn/res/g109/1082517/s120x120/…bb8-402d-9cd9-ebfad59b-03c44594-211013120039.jpeg', N' ', 27000, N'20', 3)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (18, N'Combo 3 phần Mì Quảng Đặc Biệt Tặng 3 Chai Pepsi', N'https://images.foody.vn/res/g96/955075/s120x120/88743c75-aca3-4968-934c-e3f2a2faf6fd.jpg', N' ', 175000, N'18', 4)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (19, N'Hủ tiếu xương', N'https://images.foody.vn/res/g96/955075/s120x120/71…a8e-418a-9ae9-c9024e34-e9300c4e-221019162511.jpeg', N'Thịt xương', 55000, N'16', 4)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (20, N'Hủ tiếu nam vang', N'https://images.foody.vn/res/g96/955075/s120x120/95…58b-415f-b626-69aaa8e4-f47b6b94-221019162229.jpeg', N'Thịt tom, tim heo', 55000, N'16', 4)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (21, N'Mì Quảng Đặc Biệt Tặng Chai Pepsi', N'https://images.foody.vn/res/g96/955075/s120x120/bb3a90cb-77d0-4ae6-86ef-f7c2683fe830.jpg', N' ', 68000, N'14', 4)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (22, N'Combo 3 phần Mì quảng thường. 3 Chai Pepsi', N'https://images.foody.vn/res/g96/955075/s120x120/3b1782a1-b6d1-47d5-94f5-3247e5a840d5.jpg', N' ', 136000, N'14', 4)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (23, N'Pepsi lẻ', N'https://images.foody.vn/res/g96/955075/s120x120/d4…08d-4d04-858f-34088cc9-01cab68e-220716155124.jpeg', N' ', 12000, N'17', 4)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (24, N'Ức Gà Sốt Chua Ngọt - Món Mới', N'https://images.foody.vn/res/g113/1129196/s120x120/…83e-4027-88d3-c30fce1b-493050cc-220324111036.jpeg', N'Dùng kèm cơm gạo tím than và salad', 68000, N'1', 5)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (25, N'Ức Gà Nướng Mật Ong', N'https://images.foody.vn/res/g113/1129196/s120x120/…83e-4027-88d3-c30fce1b-493050cc-220324111036.jpeg', N'Grilled chicken breast with honey - Dùng kèm cơm gạo tím than và rau củ hấp', 68000, N'1', 5)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (26, N'Bò Sốt Cà Ri Xanh Thái Cay', N'https://images.foody.vn/res/g91/907668/s120x120/45…3e7-4702-890e-7a272c9e-830ad50d-220927163731.jpeg', N'Món dùng kèm cơm lứt đen, khách muốn dùng kèm mì hoặc bún ghi chú đổi cơm mì hoặc bún dùm quán. Xốt quán đã xào kèm vào đồ ăn, còn xốt mè, me, chanh là quán cho ăn kèm theo salad.', 69000, N'1', 5)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (27, N'Cá Basa Xốt Cà ri Xanh Thái Cay', N'https://images.foody.vn/res/g91/907668/s120x120/b1…ef4-427f-8e00-2fe92ff5-fb3f9a0c-220927163600.jpeg', N'Món dùng kèm cơm lứt đen', 69000, N'1', 5)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (28, N'Salad Ức Gà Sốt Chua Ngọt', N'	https://images.foody.vn/res/g91/907668/s120x120/e6f117c1-7e24-49fa-b228-3cc0eb4ef87f.jpeg', N' ', 69000, N'1', 5)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (29, N'Cơm gà chiên mắm tỏi', N'https://images.foody.vn/res/g2/11349/s120x120/fdff…6a5-494f-b297-5ee76750-7452e9b4-201109124604.jpeg', N' ', 45000, N'15', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (30, N'Cơm gà xối mỡ', N'https://images.foody.vn/res/g2/11349/s120x120/fdff…6a5-494f-b297-5ee76750-7452e9b4-201109124604.jpeg', N' ', 45000, N'15', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (31, N'Cơm dương châu đùi mắm tỏi', N'https://images.foody.vn/res/g2/11349/s120x120/fdff…6a5-494f-b297-5ee76750-7452e9b4-201109124604.jpeg', N' ', 45000, N'15', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (32, N'Cơm gà hấp muối', N'https://images.foody.vn/res/g2/11349/s120x120/fdff…6a5-494f-b297-5ee76750-7452e9b4-201109124604.jpeg', N' ', 45000, N'15', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (33, N'Cơm ba rọi chiên giòn', N'https://images.foody.vn/res/g2/11349/s120x120/49c3…798-49ef-a313-cf71842e-6f70bdec-201109124014.jpeg', N' ', 45000, N'15', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (34, N'Cơm sườn xào chua ngọt (có sốt)', N'https://images.foody.vn/res/g2/11349/s120x120/d6f0…f4d-44d4-9eab-014f6e6b-0512b289-201109124819.jpeg', N' ', 47000, N'15 ', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (35, N'Cơm Sườn Cọng Chiên', N'https://images.foody.vn/res/g2/11349/s120x120/405b…e97-4231-bcd1-fbf4b043-cc068549-220614010038.jpeg', N' ', 45000, N'21', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (36, N'Chả Cá Trứng Muối', N'https://images.foody.vn/res/g2/11349/s120x120/d90b…196-4bb2-b0d7-7dc5013b-2b67aaaa-220614010136.jpeg', N' ', 45000, N'21', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (37, N'Đùi Gà Hấp Muối', N'https://images.foody.vn/res/g2/11349/s120x120/b683…f30-4b1e-b980-54f4f1c5-d2d95c1c-201109125821.jpeg', N' ', 37000, N'21', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (38, N'Mực Chiên Giòn', N'https://images.foody.vn/res/g2/11349/s120x120/ff9a…a03-4f74-9da7-704e3b1b-3d822669-201109125600.jpeg', N' ', 37000, N'21', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (39, N'Canh Rong Biển', N'https://images.foody.vn/res/g2/11349/s120x120/3429…4db-4dbe-a1bc-3edb3159-f8206ee4-201109125940.jpeg', N' ', 10000, N'22', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (40, N'Canh Cải Ngọt', N'https://images.foody.vn/res/g2/11349/s120x120/7fe9fddc-b2de-435b-8443-517e6265b0e7.jpg', N' ', 8000, N'22', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (41, N'Canh Cải Bò Bằm', N'https://images.foody.vn/res/g2/11349/s120x120/22a8…ee4-4646-a809-374fbf71-9b95d2ba-201109125908.jpeg', N' ', 35000, N'22', 6)
INSERT [dbo].[foods] ([FoodId], [NameofFood], [Images], [Description], [Price], [TypeofFood], [RestaurantID]) VALUES (42, N'Canh Cải Thập Cẩm', N'https://images.foody.vn/res/g2/11349/s120x120/d92c…500-4d30-b221-288f17f4-b91c7170-201109125927.jpeg', N' ', 35000, N'22', 6)
SET IDENTITY_INSERT [dbo].[foods] OFF
GO
SET IDENTITY_INSERT [dbo].[restaurants] ON 

INSERT [dbo].[restaurants] ([RestaurantID], [NameofRestaurant], [Image], [Address], [OpenTime], [CloseTime], [AreaID], [ID]) VALUES (2, N'Mami’ Kitchen - Bún Bò - Hoàng Văn Thụ', N'https://images.foody.vn/res/g100004/1000036575/prof/s280x175/file_c6c87dd9-7915-4f19-bc34-280-16a3f06a-211105152432.jpeg', N'239 Hoàng Văn Thụ, P. 8, Phú Nhuận, TP. HCM', CAST(N'2000-01-01T07:00:00.0000000' AS DateTime2), CAST(N'2100-01-01T19:30:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[restaurants] ([RestaurantID], [NameofRestaurant], [Image], [Address], [OpenTime], [CloseTime], [AreaID], [ID]) VALUES (3, N'Bánh Mì Hà Nội Chính Hiệu - Trương Quốc Dung', N'https://images.foody.vn/res/g109/1082517/prof/s640…ad-api-foody-mobile-cv-2bbb053f-210614103407.jpeg', N'42 Trương Quốc Dung, P. 10, Phú Nhuận, TP. HCM', CAST(N'2000-01-01T03:00:00.0000000' AS DateTime2), CAST(N'2100-01-01T23:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[restaurants] ([RestaurantID], [NameofRestaurant], [Image], [Address], [OpenTime], [CloseTime], [AreaID], [ID]) VALUES (4, N'Hủ Tiếu Nam Vang Quỳnh Khương', N'https://images.foody.vn/res/g96/955075/prof/s640x4…oody-upload-api-foody-mobile-hm1-190910160342.jpg', N'309/1 Nguyễn Văn Trỗi, P. 1, Tân Bình, TP. HCM', CAST(N'2000-01-01T07:30:00.0000000' AS DateTime2), CAST(N'2100-01-01T22:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[restaurants] ([RestaurantID], [NameofRestaurant], [Image], [Address], [OpenTime], [CloseTime], [AreaID], [ID]) VALUES (5, N'Bột - Healthy & Weight Loss Food', N'https://images.foody.vn/res/g113/1129196/prof/s640…oad-api-foody-mobile-im-e9ddd43d-220308184848.jpg', N'29/6 Hoàng Diệu, P. 10, Phú Nhuận, TP. HCM', CAST(N'2000-01-01T09:00:00.0000000' AS DateTime2), CAST(N'2100-01-01T14:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[restaurants] ([RestaurantID], [NameofRestaurant], [Image], [Address], [OpenTime], [CloseTime], [AreaID], [ID]) VALUES (6, N'Cơm Gà Xối Mỡ 142 - Ba Đình', N'https://images.foody.vn/res/g2/11349/s120x120/fdff…6a5-494f-b297-5ee76750-7452e9b4-201109124604.jpeg', N'142 Ba Đình, P. 10, Quận 8, TP. HCM', CAST(N'2000-01-01T07:00:00.0000000' AS DateTime2), CAST(N'2100-01-01T20:00:00.0000000' AS DateTime2), 1, 1)
SET IDENTITY_INSERT [dbo].[restaurants] OFF
GO
SET IDENTITY_INSERT [dbo].[types] ON 

INSERT [dbo].[types] ([ID], [NameofType]) VALUES (1, N'Đồ Ăn')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (2, N'Thực Phẩm')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (3, N'Bia')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (4, N'Hoa')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (5, N'Siêu Thị')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (6, N'Thuốc')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (7, N'Thú Cưng')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (8, N'Đồ Chay')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (9, N'Bánh Kem')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (10, N'Tráng Miệng')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (11, N'Homemade')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (12, N'Pizza')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (13, N'Món Lẩu')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (14, N'Mì Phở')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (15, N'Cơm Hộp')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (16, N'Món Nước')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (17, N'Nước Uống')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (18, N'Combo')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (19, N'Bánh Mì')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (20, N'Hamburger')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (21, N'Topping')
INSERT [dbo].[types] ([ID], [NameofType]) VALUES (22, N'Canh')
SET IDENTITY_INSERT [dbo].[types] OFF
GO
ALTER TABLE [dbo].[addresses]  WITH CHECK ADD  CONSTRAINT [FK_addresses_users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[addresses] CHECK CONSTRAINT [FK_addresses_users_UserID]
GO
ALTER TABLE [dbo].[detailAreas]  WITH CHECK ADD  CONSTRAINT [FK_detailAreas_areas_AreaID] FOREIGN KEY([AreaID])
REFERENCES [dbo].[areas] ([AreaID])
GO
ALTER TABLE [dbo].[detailAreas] CHECK CONSTRAINT [FK_detailAreas_areas_AreaID]
GO
ALTER TABLE [dbo].[foods]  WITH CHECK ADD  CONSTRAINT [FK_foods_restaurants_RestaurantID] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[restaurants] ([RestaurantID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[foods] CHECK CONSTRAINT [FK_foods_restaurants_RestaurantID]
GO
ALTER TABLE [dbo].[invoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_invoiceDetails_foods_FoodId] FOREIGN KEY([FoodId])
REFERENCES [dbo].[foods] ([FoodId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[invoiceDetails] CHECK CONSTRAINT [FK_invoiceDetails_foods_FoodId]
GO
ALTER TABLE [dbo].[invoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_invoiceDetails_invoices_InvoicesID] FOREIGN KEY([InvoicesID])
REFERENCES [dbo].[invoices] ([InvoicesID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[invoiceDetails] CHECK CONSTRAINT [FK_invoiceDetails_invoices_InvoicesID]
GO
ALTER TABLE [dbo].[invoices]  WITH CHECK ADD  CONSTRAINT [FK_invoices_users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[invoices] CHECK CONSTRAINT [FK_invoices_users_UserID]
GO
ALTER TABLE [dbo].[promotions]  WITH CHECK ADD  CONSTRAINT [FK_promotions_users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[promotions] CHECK CONSTRAINT [FK_promotions_users_UserId]
GO
ALTER TABLE [dbo].[restaurants]  WITH CHECK ADD  CONSTRAINT [FK_restaurants_areas_AreaID] FOREIGN KEY([AreaID])
REFERENCES [dbo].[areas] ([AreaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[restaurants] CHECK CONSTRAINT [FK_restaurants_areas_AreaID]
GO
ALTER TABLE [dbo].[restaurants]  WITH CHECK ADD  CONSTRAINT [FK_restaurants_types_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[types] ([ID])
GO
ALTER TABLE [dbo].[restaurants] CHECK CONSTRAINT [FK_restaurants_types_ID]
GO
ALTER TABLE [dbo].[tokens]  WITH CHECK ADD  CONSTRAINT [FK_tokens_users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tokens] CHECK CONSTRAINT [FK_tokens_users_UserId]
GO
