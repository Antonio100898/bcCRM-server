USE [@NewDatabaseName]

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[benchmarkReportFactors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[factorName] [nvarchar](100) NOT NULL,
	[weightInTotal] [decimal](18, 2) NOT NULL,
	[entityType] [nvarchar](50) NULL,
	[entityId] [bigint] NULL,
	[lastUpdate] [datetime] NULL,
 CONSTRAINT [PK_meiraReportFactors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branchActivities]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchActivities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NULL,
	[posId] [int] NULL,
	[activityId] [int] NULL,
	[activityDetails] [nvarchar](max) NULL,
	[abstractName] [nvarchar](50) NULL,
	[abstractId] [bigint] NULL,
	[activityTime] [datetime] NULL,
	[commitTime] [datetime] NULL,
 CONSTRAINT [PK_branchActivities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[branchActivityType]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchActivityType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[activityName] [nvarchar](50) NULL,
	[alertLevel] [nvarchar](50) NULL,
 CONSTRAINT [PK_branchActivityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branchBenchmarkConfig]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchBenchmarkConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NULL,
	[area] [nvarchar](100) NULL,
	[league] [nvarchar](100) NULL,
	[areaManager] [nvarchar](100) NULL,
	[chef] [nvarchar](100) NULL,
	[chairsCount] [int] NULL,
	[lastUpdate] [datetime] NULL,
 CONSTRAINT [PK_branchMeiraConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branchContacts]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchContacts](
	[Id] [int] NOT NULL,
	[branchId] [int] NULL,
	[firstName] [nvarchar](50) NULL,
	[lastName] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[mobile] [nvarchar](50) NULL,
 CONSTRAINT [PK_branchContacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branchCurrentMonthTopTenCategories]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchCurrentMonthTopTenCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[categoryId] [int] NULL,
	[branchId] [int] NOT NULL,
	[cusYear] [int] NOT NULL,
	[cusMonth] [int] NOT NULL,
	[startDate] [datetime] NULL,
	[CatName] [nvarchar](255) NULL,
	[CatQuan] [float] NULL,
	[CatSum] [float] NULL,
 CONSTRAINT [PK_branchCurrentMonthTopTenCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branchCurrentMonthTopTenProducts]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchCurrentMonthTopTenProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[dishId] [bigint] NULL,
	[branchId] [int] NOT NULL,
	[cusYear] [int] NOT NULL,
	[cusMonth] [int] NOT NULL,
	[startDate] [datetime] NULL,
	[DishName] [nvarchar](255) NULL,
	[Quan] [float] NULL,
	[PriceSum] [float] NULL,
 CONSTRAINT [PK_branchCurrentMonthTopTenProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branchDailyStats]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchDailyStats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[zId] [bigint] NULL,
	[branchId] [int] NOT NULL,
	[cusYear] [int] NOT NULL,
	[cusMonth] [int] NOT NULL,
	[cusDay] [int] NOT NULL,
	[startDate] [datetime] NULL,
	[PaymentsSum] [float] NULL,
	[OrdersSum] [float] NULL,
	[ServiceSum] [float] NULL,
	[CancelsSum] [float] NULL,
	[CuponCommisionSum] [float] NULL,
	[ordersCount] [int] NULL,
	[cancelsCount] [int] NULL,
	[dishesCount] [int] NULL,
	[guests] [int] NULL,
	[cash] [decimal](18, 2) NULL,
	[creditCard] [decimal](18, 2) NULL,
	[coupons] [decimal](18, 2) NULL,
	[hakafa] [decimal](18, 2) NULL,
	[checks] [decimal](18, 2) NULL,
	[other] [decimal](18, 2) NULL,
 CONSTRAINT [PK_branchDailyStats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branchDailyStats_Brakdown]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branchDailyStats_Brakdown](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchDailyStatsId] [int] NULL,
	[posId] [int] NULL,
	[ServiceSum] [int] NULL,
	[CouponCommision] [int] NULL,
	[OrderType0Count] [int] NULL,
	[OrderType0Sum] [float] NULL,
	[OrderType0Dinners] [int] NULL,
	[OrderType0CancelCount] [int] NULL,
	[OrderType0CancelSum] [float] NULL,
	[OrderType1Count] [int] NULL,
	[OrderType1Sum] [float] NULL,
	[OrderType1Dinners] [nchar](10) NULL,
	[OrderType1CancelCount] [int] NULL,
	[OrderType1CancelSum] [float] NULL,
	[OrderType2Count] [int] NULL,
	[OrderType2Sum] [float] NULL,
	[OrderType2Dinners] [int] NULL,
	[OrderType2CancelCount] [int] NULL,
	[OrderType2CancelSum] [float] NULL,
	[OrderType3Count] [int] NULL,
	[OrderType3Sum] [float] NULL,
	[OrderType4Count] [int] NULL,
	[OrderType4Sum] [float] NULL,
	[OrderType5Count] [int] NULL,
	[OrderType5Sum] [float] NULL,
	[OrderType6Count] [int] NULL,
	[OrderType6Sum] [float] NULL,
	[OrderType7Count] [int] NULL,
	[OrderType7Sum] [float] NULL,
	[cash] [decimal](18, 2) NULL,
	[creditCard] [decimal](18, 2) NULL,
	[coupons] [decimal](18, 2) NULL,
	[hakafa] [decimal](18, 2) NULL,
	[checks] [decimal](18, 2) NULL,
	[other] [decimal](18, 2) NULL,
 CONSTRAINT [PK_branchDailyStats_Brakdown] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[branches]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[branches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchName] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[biCommEmail] [nvarchar](50) NULL,
	[kosher] [bit] NOT NULL,
	[cityId] [int] NULL,
	[ip] [nvarchar](16) NULL,
	[subChain] [int] NULL,
 CONSTRAINT [PK_branches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[CreditPayments]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[CreditPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[orderId] [bigint] NULL,
	[paymentId] [nvarchar](50) NOT NULL,
	[CardType] [int] NULL,
	[CardNumber] [nvarchar](18) NULL,
	[ExpDate] [nvarchar](6) NULL,
	[DealCode] [int] NULL,
	[ApprovalNum] [nvarchar](50) NULL,
	[SuvarNumber] [nvarchar](50) NULL,
	[ForgianCard] [bit] NULL,
	[SolekId] [int] NULL,
	[NumOfPayments] [int] NULL,
	[CommitTime] [datetime] NULL,
	[teudatZehut] [nvarchar](50) NULL,
	[OrderGuidID] [nvarchar](50) NULL,
	[CreditClubTypeID] [int] NULL,
	[CardTypeName] [nvarchar](50) NULL,
	[SolekName] [nvarchar](50) NULL,
	[CreditClubTypeName] [nvarchar](50) NULL
) ON [PRIMARY]


/****** Object:  Table [dbo].[CuponPayments]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[CuponPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[orderId] [bigint] NULL,
	[paymentId] [nvarchar](50) NOT NULL,
	[CuponType] [int] NULL,
	[Quntity] [int] NULL,
	[Value] [int] NULL,
	[OrderGuidID] [nvarchar](50) NULL,
	[CuponBarCode] [nvarchar](255) NULL,
	[CuponName] [nvarchar](255) NULL,
	[CommisionSum] [float] NULL,
	[CuponGroupID] [int] NULL,
	[CuponGroupName] [nvarchar](255) NULL,
	[zId] [bigint] NULL,
 CONSTRAINT [PK_CuponPayments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[dbConfig]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[dbConfig](
	[Id] [int] NOT NULL,
	[description] [nvarchar](50) NULL,
	[descValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[DishCategories]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[DishCategories](
	[Id] [int] NOT NULL,
	[CategotyName] [nvarchar](50) NULL,
	[groupId] [int] NULL,
	[lastUpdatePosId] [int] NOT NULL,
	[dateInserted] [datetime] NOT NULL,
 CONSTRAINT [PK_DishCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Dishes]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Dishes](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[dishCategoryId] [int] NULL,
	[price] [float] NULL,
	[lastUpdatePosId] [int] NOT NULL,
	[dateInserted] [datetime] NOT NULL,
	[currentMenu] [bit] NOT NULL,
 CONSTRAINT [PK_Dishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[DishesInOrder]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[DishesInOrder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[orderId] [bigint] NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[ZId] [bigint] NULL,
	[dishCategoryId] [int] NULL,
	[IndexDishesInOrder] [int] NOT NULL,
	[dishId] [bigint] NULL,
	[Quantity] [float] NULL,
	[OrderTime] [datetime] NULL,
	[Discount] [float] NULL,
	[TotalPriceDish] [float] NULL,
	[GeneralRemarks] [nvarchar](max) NULL,
	[DishesInOrderStatusID] [int] NULL,
	[ChargingStatusID] [int] NULL,
	[MealNum] [int] NULL,
	[Waitress] [smallint] NULL,
	[PrintintStatus] [int] NULL,
	[ItemRemark] [nvarchar](max) NULL,
	[MealIndex] [float] NULL,
	[MealCatagory] [float] NULL,
	[priceType] [float] NULL,
	[BelongTo] [nvarchar](100) NULL,
	[secondReminderPrint] [datetime] NULL,
	[CounterID] [int] NULL,
	[OrderGuidID] [nvarchar](50) NULL,
	[currentMenuID] [int] NULL,
	[unitPrice] [float] NULL,
	[DishCatID] [int] NULL,
	[DishName] [nvarchar](255) NULL,
	[NetID] [int] NULL,
	[PriceConDiscount] [float] NULL,
	[PriceConDiscountService] [float] NULL,
	[DishCatName] [nvarchar](255) NULL,
	[WaitressName] [nvarchar](255) NULL,
	[DishCost] [float] NULL,
	[OrderCloseTime] [datetime] NULL,
	[OrderType] [int] NULL,
	[OrderStatus] [int] NULL,
 CONSTRAINT [PK_DishesInOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[dishesInOrderCancelReasons]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[dishesInOrderCancelReasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NULL,
	[posId] [int] NULL,
	[zId] [bigint] NULL,
	[dishId] [bigint] NULL,
	[orderId] [bigint] NULL,
	[indexDishInOrderId] [int] NULL,
	[cancelReasonIndex] [int] NULL,
	[cancelReasonId] [int] NULL,
	[goneOut] [bit] NULL,
	[workerId] [bigint] NULL,
	[commitTime] [datetime] NULL,
	[orderGuidId] [nvarchar](50) NULL,
	[cancelComment] [nvarchar](250) NULL,
 CONSTRAINT [PK_dishesInOrderCancelReasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[DishesInOrderStatus]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[DishesInOrderStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_DishesInOrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Orders]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Orders](
	[Id] [bigint] NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[ZId] [bigint] NULL,
	[CounterID] [int] NULL,
	[DateCreat] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[OrderingStatus] [int] NULL,
	[OrderType] [float] NULL,
	[CloseTime] [datetime] NULL,
	[Discount] [float] NULL,
	[incservice] [bit] NOT NULL,
	[Service] [float] NULL,
	[CustomerID] [int] NULL,
	[Client] [nvarchar](50) NULL,
	[ClientTxt] [nvarchar](50) NULL,
	[DiscountResonID] [float] NULL,
	[Z] [int] NULL,
	[OrderIndex] [int] NULL,
	[OrderGuidID] [nvarchar](50) NULL,
	[OrderGuidIDIndex] [nvarchar](50) NULL,
	[MiniOrderID] [int] NULL,
	[Maam] [float] NULL,
	[BackupDateCreat] [datetime] NULL,
	[BackupCloseTime] [datetime] NULL,
	[OrderTarget] [int] NULL,
	[OpenYear] [int] NULL,
	[OpenMonth] [int] NULL,
	[OpenDay] [int] NULL,
	[OpenDayValue] [int] NULL,
	[OpenHour] [int] NULL,
	[CloseYear] [int] NULL,
	[CloseMonth] [int] NULL,
	[CloseDay] [int] NULL,
	[CloseDayValue] [int] NULL,
	[CloseHour] [int] NULL,
	[workerId] [bigint] NULL,
	[DiscountResonName] [nvarchar](255) NULL,
	[WorkerName] [nvarchar](255) NULL,
	[WorkerJobType] [int] NULL,
	[WorkerJobTypeName] [nvarchar](255) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[OrdersDelivery]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[OrdersDelivery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[orderID] [bigint] NOT NULL,
	[streetID] [int] NULL,
	[Number] [nvarchar](255) NULL,
	[Telephone] [nvarchar](50) NULL,
	[CityID] [int] NULL,
	[CustomerID] [int] NULL,
	[DeliveryBoyID] [int] NULL,
	[TeleAssit] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[SelectedForDelivery] [bit] NULL,
	[Floor] [nvarchar](50) NULL,
	[Apartment] [nvarchar](50) NULL,
	[StartDelivery] [datetime] NULL,
	[EndDelivery] [datetime] NULL,
	[OrderGuidID] [nvarchar](50) NULL,
	[DeliveryRemark] [nvarchar](max) NULL,
	[Dinners] [int] NULL,
	[ZoneID] [int] NULL,
	[ServerDeliveryBoyID] [int] NULL,
	[CityName] [nvarchar](50) NULL,
	[StreetName] [nvarchar](50) NULL,
	[DeliveryBoyName] [nvarchar](255) NULL,
	[zId] [bigint] NULL,
 CONSTRAINT [PK_OrdersDelivery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[OrdersRestaurant]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[OrdersRestaurant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [bigint] NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[DinersNum] [int] NULL,
	[waiterssId] [bigint] NULL,
	[TableNum] [int] NULL,
	[OrderGuidID] [nvarchar](50) NULL,
	[WaiterssName] [nvarchar](255) NULL,
	[zId] [bigint] NULL,
 CONSTRAINT [PK_OrdersRestaurant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[OrderSums]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[OrderSums](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[orderId] [bigint] NOT NULL,
	[Discount] [float] NULL,
	[Service] [float] NULL,
	[DishesSum] [float] NULL,
	[DishesConDiscount] [float] NULL,
	[OrderSum] [float] NULL,
	[PaymentsSum] [float] NULL,
	[DiscountSum] [float] NULL,
	[ServiceSum] [float] NULL,
	[zId] [bigint] NULL,
 CONSTRAINT [PK_OrderSums] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Payments]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Payments](
	[Id] [nvarchar](50) NOT NULL,
	[orderId] [bigint] NOT NULL,
	[branchId] [int] NULL,
	[posId] [int] NULL,
	[ZId] [bigint] NULL,
	[PaymentType] [int] NULL,
	[DateofPayment] [datetime] NULL,
	[Sum] [float] NULL,
	[Remarks] [nvarchar](50) NULL,
	[PaymentStatus] [int] NULL,
	[InvoiceType] [int] NULL,
	[CounterID] [smallint] NULL,
	[InvoiceID] [float] NULL,
	[ReportType] [int] NULL,
	[OrderGuidID] [nvarchar](50) NULL,
	[cusNameRemark] [nvarchar](50) NULL,
	[InvoiceGuidID] [nvarchar](50) NULL,
	[Tip] [float] NULL,
	[PaymentTypeName] [nvarchar](50) NULL,
	[SumByService] [float] NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[paymentTypes]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[paymentTypes](
	[Id] [int] NOT NULL,
	[typeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_paymentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[paymentTypes_pos]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[paymentTypes_pos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[paymentTypeId] [int] NULL,
	[posId] [int] NOT NULL,
	[PaymentTypeName] [nvarchar](50) NULL,
	[ShowInZ] [bit] NOT NULL,
	[CalcInTotalZ] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[pos]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[pos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[udid] [int] NULL,
	[dbVersion] [int] NULL,
	[posName] [nvarchar](50) NULL,
	[branchId] [int] NULL,
 CONSTRAINT [PK_pos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[posSumConfig]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[posSumConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[posId] [int] NULL,
	[deductServiceFromTotal] [bit] NULL,
	[deductServiceFromCash] [bit] NULL,
	[deductServiceFromCredit] [bit] NULL,
	[deductCouponComissionFromTotal] [bit] NULL,
	[deductCouponComissionFromCouponTotal] [bit] NULL,
 CONSTRAINT [PK_posSumConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Seasons]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Seasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[startDate] [date] NULL,
	[endDate] [date] NULL
) ON [PRIMARY]


/****** Object:  Table [dbo].[shifts]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[shifts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[value] [decimal](18, 2) NULL,
 CONSTRAINT [PK_shifts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[shifts_branch]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[shifts_branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NULL,
	[shiftId] [int] NULL,
	[startDay] [smallint] NULL,
	[endDay] [smallint] NULL,
	[startHour] [time](7) NULL,
	[endHour] [time](7) NULL,
 CONSTRAINT [PK_shifts_branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[shifts_branch_overHours]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[shifts_branch_overHours](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NULL,
	[maxHours] [int] NULL,
	[AdditionalPayment] [decimal](18, 2) NULL,
 CONSTRAINT [PK_shifts_branch_overHours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[ShiftsGlobals]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[ShiftsGlobals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[startHour] [time](0) NULL,
	[endHour] [time](0) NULL,
	[Name] [nvarchar](50) NULL
) ON [PRIMARY]


/****** Object:  Table [dbo].[weatherInfo]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[weatherInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[cityId] [int] NULL,
	[date] [date] NULL,
	[summery] [nvarchar](150) NULL,
	[precipType] [nvarchar](50) NULL,
	[precipProbability] [decimal](18, 2) NULL,
	[precipIntensityMax] [decimal](18, 2) NULL,
	[precipIntensity] [decimal](18, 2) NULL,
	[icon] [nvarchar](50) NULL,
	[humidity] [decimal](18, 2) NULL,
	[windSpeed] [decimal](18, 2) NULL,
	[windBearing] [decimal](18, 2) NULL,
	[cloudCover] [decimal](18, 2) NULL,
	[sunriseTime] [datetime] NULL,
	[sunsetTime] [datetime] NULL,
	[moonPhase] [decimal](18, 2) NULL,
	[temperatureMin] [decimal](18, 2) NULL,
	[temperatureMinTime] [datetime] NULL,
	[temperatureMax] [decimal](18, 2) NULL,
	[temperatureMaxTime] [datetime] NULL,
	[apparentTemperatureMin] [decimal](18, 2) NULL,
	[apparentTemperatureMinTime] [datetime] NULL,
	[apparentTemperatureMax] [decimal](18, 2) NULL,
	[apparentTemperatureMaxTime] [datetime] NULL,
	[dewPoint] [decimal](18, 2) NULL,
	[visibility] [decimal](18, 2) NULL,
	[pressure] [decimal](18, 2) NULL,
	[ozone] [decimal](18, 2) NULL,
 CONSTRAINT [PK_weatherInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[WorkerJobType]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[WorkerJobType](
	[Id] [int] NOT NULL,
	[JobName] [nvarchar](50) NULL,
	[CanOpenOrder] [bit] NOT NULL,
 CONSTRAINT [PK_WorkerJobType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Workers]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Workers](
	[Id] [bigint] NOT NULL,
	[branchId] [int] NULL,
	[posId] [int] NULL,
	[workerJobTypeId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[SurName] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Phone1] [nvarchar](50) NULL,
	[Phone2] [nvarchar](50) NULL,
	[emailAddress] [nvarchar](50) NULL,
	[Gender] [int] NULL,
	[WorkerStatusID] [int] NULL,
	[DateCreated] [datetime] NULL,
	[Password] [nvarchar](255) NULL,
	[SecurityLevel] [int] NULL,
	[InShift] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[TeodatZhut] [nvarchar](50) NULL,
	[wage] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Workers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[WorkingTimer]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[WorkingTimer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NULL,
	[ZId] [bigint] NULL,
	[workerjobTypeId] [int] NULL,
	[workerId] [bigint] NULL,
	[In_Date] [datetime] NOT NULL,
	[Out_Date] [datetime] NOT NULL,
	[comments] [nvarchar](50) NULL,
	[tip] [float] NULL,
	[BackupIn_Date] [datetime] NULL,
	[BackupOut_Date] [datetime] NULL,
	[SummerClock] [bit] NOT NULL,
	[Counter] [int] NULL,
	[WorkerName] [nvarchar](255) NULL,
	[WorkerjobTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_WorkingTimer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[X]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[X](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[posId] [int] NOT NULL,
	[CusName] [nvarchar](max) NULL,
	[StartTime] [datetime] NULL,
	[CloseTime] [datetime] NULL,
	[sumClose] [float] NULL,
	[sumOpen] [float] NULL,
	[sumCancel] [float] NULL,
	[dinnerClose] [int] NULL,
	[dinnersOpen] [int] NULL,
	[OrdersCountOpen] [int] NULL,
	[OrdersCountClose] [int] NULL,
	[OrdersCountCancel] [int] NULL,
	[sumDiscount] [float] NULL,
	[sumService] [float] NULL,
	[openWorkerShifts] [int] NULL,
	[CommitTime] [datetime] NULL,
	[OrderType0Sum] [float] NULL,
	[OrderType1Sum] [float] NULL,
	[OrderType2Sum] [float] NULL,
	[WeekAgoSum] [float] NULL,
	[ThisMonthSum] [float] NULL,
	[DinnersCloseTakeAway] [int] NULL,
	[DinnerCloseDelivery] [int] NULL,
	[DinnerCloserestaurant] [int] NULL,
 CONSTRAINT [PK_X] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[XDishes]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[XDishes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[xId] [int] NOT NULL,
	[dishID] [bigint] NOT NULL,
	[dishCategoryId] [bigint] NULL,
	[NetID] [nvarchar](50) NULL,
	[DishName] [nvarchar](max) NULL,
	[DishQuan] [float] NULL,
	[DishSums] [float] NULL,
	[CommitTime] [datetime] NULL,
	[CatName] [nvarchar](50) NULL,
 CONSTRAINT [PK_XDishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[XHistory]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[XHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[posId] [int] NOT NULL,
	[CusName] [nvarchar](max) NULL,
	[StartTime] [datetime] NULL,
	[CloseTime] [datetime] NULL,
	[sumClose] [float] NULL,
	[sumOpen] [float] NULL,
	[sumCancel] [float] NULL,
	[dinnerClose] [int] NULL,
	[dinnersOpen] [int] NULL,
	[OrdersCountOpen] [int] NULL,
	[OrdersCountClose] [int] NULL,
	[OrdersCountCancel] [int] NULL,
	[sumDiscount] [float] NULL,
	[sumService] [float] NULL,
	[openWorkerShifts] [int] NULL,
	[CommitTime] [datetime] NULL,
	[OrderType0Sum] [float] NULL,
	[OrderType1Sum] [float] NULL,
	[OrderType2Sum] [float] NULL,
 CONSTRAINT [PK_XHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[XPayments]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[XPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[xId] [int] NOT NULL,
	[PaymentType] [int] NOT NULL,
	[PaymentTypeName] [nvarchar](50) NULL,
	[PaymentSum] [float] NULL,
	[CommitTime] [datetime] NULL,
 CONSTRAINT [PK_XPayments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[ZEntrytable]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[ZEntrytable](
	[Id] [bigint] NOT NULL,
	[branchId] [int] NOT NULL,
	[posId] [int] NOT NULL,
	[ZIndex] [int] NOT NULL,
	[LastZMade] [datetime] NULL,
	[TotalSum] [int] NULL,
	[CounterID] [int] NULL,
	[Zrange] [datetime] NULL,
	[CreditCard] [float] NULL,
	[Cash] [float] NULL,
	[CheckSum] [float] NULL,
	[Cupon] [float] NULL,
	[Hakafa] [float] NULL,
	[Dinners] [float] NULL,
	[OrderCount] [int] NULL,
	[cancelCount] [int] NULL,
	[refundCount] [int] NULL,
	[refundSum] [float] NULL,
	[orderDiscountCount] [int] NULL,
	[orderDiscountSum] [float] NULL,
	[orderCancelSum] [float] NULL,
	[orderCashBackCount] [int] NULL,
	[orderCashBackSum] [float] NULL,
	[valueDate] [datetime] NULL,
	[dbVersion] [int] NOT NULL,
	[dateInserted] [datetime] NOT NULL,
 CONSTRAINT [PK_ZEntrytable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[zPayments]    Script Date: 08/03/2017 18:58:30 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[zPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[zId] [bigint] NULL,
	[paymentTypeId] [int] NULL,
	[totalAmount] [float] NULL,
	[isNegtive] [bit] NULL,
 CONSTRAINT [PK_zPayments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Index [dashBoard_getComparisonData_1]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [dashBoard_getComparisonData_1] ON [dbo].[branchDailyStats]
(
	[branchId] ASC,
	[startDate] ASC
)
INCLUDE ( 	[PaymentsSum],
	[ordersCount],
	[cancelsCount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [dashBoard_getComparisonData_2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [dashBoard_getComparisonData_2] ON [dbo].[branchDailyStats]
(
	[cusYear] ASC,
	[cusMonth] ASC,
	[branchId] ASC
)
INCLUDE ( 	[PaymentsSum],
	[ordersCount],
	[cancelsCount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [dashBoard_getData_3]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [dashBoard_getData_3] ON [dbo].[branchDailyStats]
(
	[branchId] ASC,
	[startDate] ASC
)
INCLUDE ( 	[cusYear],
	[cusMonth],
	[cusDay],
	[PaymentsSum]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [dashBoard_getYearlyStats]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [dashBoard_getYearlyStats] ON [dbo].[branchDailyStats]
(
	[branchId] ASC,
	[startDate] ASC
)
INCLUDE ( 	[cusYear],
	[cusMonth],
	[PaymentsSum],
	[ordersCount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_branchDailyStats_zId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchDailyStats_zId] ON [dbo].[branchDailyStats]
(
	[zId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_branchId_startDate__payment_service_guests]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_startDate__payment_service_guests] ON [dbo].[branchDailyStats]
(
	[branchId] ASC,
	[startDate] ASC
)
INCLUDE ( 	[PaymentsSum],
	[ServiceSum],
	[guests]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-234916]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-234916] ON [dbo].[branchDailyStats]
(
	[zId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [income_getTimeMachineStats]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_getTimeMachineStats] ON [dbo].[branchDailyStats_Brakdown]
(
	[branchDailyStatsId] ASC
)
INCLUDE ( 	[OrderType0Count],
	[OrderType0CancelCount],
	[OrderType1Count],
	[OrderType1CancelCount],
	[OrderType2Count],
	[OrderType2CancelCount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-234953]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-234953] ON [dbo].[branchDailyStats_Brakdown]
(
	[branchDailyStatsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_CreditPayments_paymentId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_CreditPayments_paymentId] ON [dbo].[CreditPayments]
(
	[paymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235030]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235030] ON [dbo].[CreditPayments]
(
	[branchId] ASC,
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_CuponPayments_paymentId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_CuponPayments_paymentId] ON [dbo].[CuponPayments]
(
	[paymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235226]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235226] ON [dbo].[CuponPayments]
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [NonClusteredIndex-20160708-125205]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160708-125205] ON [dbo].[DishCategories]
(
	[CategotyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [delete_dishInOrder]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [delete_dishInOrder] ON [dbo].[DishesInOrder]
(
	[branchId] ASC,
	[ZId] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_benchmark_1]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_benchmark_1] ON [dbo].[DishesInOrder]
(
	[dishId] ASC,
	[OrderStatus] ASC,
	[DishesInOrderStatusID] ASC,
	[OrderCloseTime] ASC,
	[OrderType] ASC
)
INCLUDE ( 	[branchId],
	[Quantity]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_benchmark_2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_benchmark_2] ON [dbo].[DishesInOrder]
(
	[dishCategoryId] ASC,
	[OrderStatus] ASC,
	[DishesInOrderStatusID] ASC,
	[OrderCloseTime] ASC,
	[OrderType] ASC
)
INCLUDE ( 	[branchId],
	[Quantity]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_branchId_closetime__dishname_price]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_closetime__dishname_price] ON [dbo].[DishesInOrder]
(
	[branchId] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[DishName],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_branchId_discount_time__remark]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_discount_time__remark] ON [dbo].[DishesInOrder]
(
	[branchId] ASC,
	[Discount] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[ItemRemark]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_branchId_orderId_price]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_orderId_price] ON [dbo].[DishesInOrder]
(
	[branchId] ASC
)
INCLUDE ( 	[orderId],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_branchId_time__dishId_price]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_time__dishId_price] ON [dbo].[DishesInOrder]
(
	[branchId] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[dishId],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_branchId_time_price_categoryname]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_time_price_categoryname] ON [dbo].[DishesInOrder]
(
	[branchId] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[PriceConDiscount],
	[DishCatName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_brancId_time__orderid_price]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_brancId_time__orderid_price] ON [dbo].[DishesInOrder]
(
	[branchId] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[orderId],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_Category_Based]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_Category_Based] ON [dbo].[DishesInOrder]
(
	[OrderStatus] ASC,
	[branchId] ASC,
	[dishCategoryId] ASC,
	[DishesInOrderStatusID] ASC,
	[OrderCloseTime] ASC,
	[OrderType] ASC,
	[dishId] ASC
)
INCLUDE ( 	[Quantity],
	[PriceConDiscount],
	[DishName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_closeTime_orderId_index_price]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_closeTime_orderId_index_price] ON [dbo].[DishesInOrder]
(
	[OrderCloseTime] ASC
)
INCLUDE ( 	[orderId],
	[IndexDishesInOrder],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_DishesInOrder_Branch_groups_items]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_DishesInOrder_Branch_groups_items] ON [dbo].[DishesInOrder]
(
	[OrderStatus] ASC,
	[branchId] ASC,
	[DishesInOrderStatusID] ASC,
	[PriceConDiscount] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[dishCategoryId],
	[Quantity]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_dishesInOrder_OrderCloseTime]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_dishesInOrder_OrderCloseTime] ON [dbo].[DishesInOrder]
(
	[OrderCloseTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_NetID_OrderStatus_type_Time]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_NetID_OrderStatus_type_Time] ON [dbo].[DishesInOrder]
(
	[NetID] ASC,
	[OrderStatus] ASC,
	[DishesInOrderStatusID] ASC,
	[OrderCloseTime] ASC,
	[OrderType] ASC
)
INCLUDE ( 	[Quantity],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_orderId_index_closeTime]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_orderId_index_closeTime] ON [dbo].[DishesInOrder]
(
	[orderId] ASC,
	[IndexDishesInOrder] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_OrderId_Status]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_OrderId_Status] ON [dbo].[DishesInOrder]
(
	[orderId] ASC,
	[OrderStatus] ASC
)
INCLUDE ( 	[DishesInOrderStatusID],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_Status]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_Status] ON [dbo].[DishesInOrder]
(
	[OrderStatus] ASC,
	[DishesInOrderStatusID] ASC
)
INCLUDE ( 	[orderId],
	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_status_branchId_discount_dioStatusId_time__dishId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_status_branchId_discount_dioStatusId_time__dishId] ON [dbo].[DishesInOrder]
(
	[OrderStatus] ASC,
	[branchId] ASC,
	[Discount] ASC,
	[DishesInOrderStatusID] ASC,
	[OrderCloseTime] ASC
)
INCLUDE ( 	[dishId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_Status_branchId_Time]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_Status_branchId_Time] ON [dbo].[DishesInOrder]
(
	[DishesInOrderStatusID] ASC,
	[branchId] ASC,
	[OrderTime] ASC
)
INCLUDE ( 	[PriceConDiscount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_ZID]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_ZID] ON [dbo].[DishesInOrder]
(
	[ZId] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [status_branchId_time_discount_dioStatusId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [status_branchId_time_discount_dioStatusId] ON [dbo].[DishesInOrder]
(
	[OrderStatus] ASC,
	[branchId] ASC,
	[OrderTime] ASC,
	[Discount] ASC,
	[DishesInOrderStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_branchId_dishId_orderId_index]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_dishId_orderId_index] ON [dbo].[dishesInOrderCancelReasons]
(
	[branchId] ASC
)
INCLUDE ( 	[dishId],
	[orderId],
	[indexDishInOrderId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_branchId_orderId_index_reasonId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_orderId_index_reasonId] ON [dbo].[dishesInOrderCancelReasons]
(
	[branchId] ASC
)
INCLUDE ( 	[orderId],
	[indexDishInOrderId],
	[cancelReasonId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_dishId_branchId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_dishId_branchId] ON [dbo].[dishesInOrderCancelReasons]
(
	[dishId] ASC,
	[branchId] ASC
)
INCLUDE ( 	[orderId],
	[indexDishInOrderId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_goneOut_branchId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_goneOut_branchId] ON [dbo].[dishesInOrderCancelReasons]
(
	[goneOut] ASC,
	[branchId] ASC
)
INCLUDE ( 	[orderId],
	[indexDishInOrderId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [_dta_index_Orders_55_1899153811__K10]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [_dta_index_Orders_55_1899153811__K10] ON [dbo].[Orders]
(
	[CloseTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [_dta_index_Orders_months_sales]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [_dta_index_Orders_months_sales] ON [dbo].[Orders]
(
	[Id] ASC,
	[OrderingStatus] ASC,
	[branchId] ASC,
	[CloseTime] ASC,
	[CloseMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [_dta_index_Orders_seasons]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [_dta_index_Orders_seasons] ON [dbo].[Orders]
(
	[Id] ASC,
	[branchId] ASC,
	[OrderingStatus] ASC,
	[CloseTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [dashBoard_getHourlyStats_idx2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [dashBoard_getHourlyStats_idx2] ON [dbo].[Orders]
(
	[CloseYear] ASC,
	[CloseMonth] ASC,
	[CloseDay] ASC,
	[OrderingStatus] ASC
)
INCLUDE ( 	[Id],
	[CloseHour]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [income_getSummeryDash]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_getSummeryDash] ON [dbo].[Orders]
(
	[OrderingStatus] ASC,
	[branchId] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Id],
	[CloseDayValue]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [income_getSummeryDash_idx2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_getSummeryDash_idx2] ON [dbo].[Orders]
(
	[OrderingStatus] ASC,
	[branchId] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Id],
	[CloseHour]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [income_getSummeryDash_idx3]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_getSummeryDash_idx3] ON [dbo].[Orders]
(
	[OrderingStatus] ASC,
	[branchId] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Id],
	[CloseMonth]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [income_getZOrders]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_getZOrders] ON [dbo].[Orders]
(
	[ZId] ASC,
	[OrderingStatus] ASC
)
INCLUDE ( 	[Id],
	[branchId],
	[posId],
	[CloseTime],
	[DiscountResonName],
	[WorkerName],
	[WorkerJobTypeName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_BranchId_CloseTime]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_BranchId_CloseTime] ON [dbo].[Orders]
(
	[OrderingStatus] ASC,
	[branchId] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Id],
	[workerId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_branchId_closeTime__Id_WorkerId_TypeName]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_branchId_closeTime__Id_WorkerId_TypeName] ON [dbo].[Orders]
(
	[branchId] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Id],
	[workerId],
	[WorkerName],
	[WorkerJobTypeName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [Ix_OrderId_BranchId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [Ix_OrderId_BranchId] ON [dbo].[Orders]
(
	[branchId] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_status_branchId_closeTime_discount_workerId_workerName]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_status_branchId_closeTime_discount_workerId_workerName] ON [dbo].[Orders]
(
	[OrderingStatus] ASC,
	[branchId] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Discount],
	[workerId],
	[WorkerName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_status_branchId_ordertype_time]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_status_branchId_ordertype_time] ON [dbo].[Orders]
(
	[OrderingStatus] ASC,
	[branchId] ASC,
	[OrderType] ASC,
	[CloseTime] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235340]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235340] ON [dbo].[Orders]
(
	[ZId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_OrdersDelivery_orderID]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_OrdersDelivery_orderID] ON [dbo].[OrdersDelivery]
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235422]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235422] ON [dbo].[OrdersDelivery]
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [income_getIncomeByHours]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_getIncomeByHours] ON [dbo].[OrdersRestaurant]
(
	[orderId] ASC
)
INCLUDE ( 	[DinersNum]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_dinersNum]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_dinersNum] ON [dbo].[OrdersRestaurant]
(
	[DinersNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_ZID]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_ZID] ON [dbo].[OrdersRestaurant]
(
	[zId] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235442]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235442] ON [dbo].[OrdersRestaurant]
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [idx_orderId_general]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [idx_orderId_general] ON [dbo].[OrderSums]
(
	[orderId] ASC
)
INCLUDE ( 	[OrderSum]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_Zid]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_Zid] ON [dbo].[OrderSums]
(
	[zId] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235500]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235500] ON [dbo].[OrderSums]
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [dashBoard_getHourlyStats]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [dashBoard_getHourlyStats] ON [dbo].[Payments]
(
	[PaymentStatus] ASC,
	[branchId] ASC
)
INCLUDE ( 	[orderId],
	[posId],
	[PaymentType],
	[Sum]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_Payments_orderId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_Payments_orderId] ON [dbo].[Payments]
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_ZID]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_ZID] ON [dbo].[Payments]
(
	[ZId] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235521]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235521] ON [dbo].[Payments]
(
	[orderId] ASC,
	[ZId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [income_weather_gettopProductsForTemp_idx2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_weather_gettopProductsForTemp_idx2] ON [dbo].[weatherInfo]
(
	[temperatureMax] ASC
)
INCLUDE ( 	[cityId],
	[date]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [idx_income_getEmployeeWageTable]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [idx_income_getEmployeeWageTable] ON [dbo].[WorkingTimer]
(
	[branchId] ASC
)
INCLUDE ( 	[posId],
	[ZId],
	[workerId],
	[In_Date],
	[Out_Date],
	[WorkerName],
	[WorkerjobTypeName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [IX_POS_Z_WORKER_TIP]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_POS_Z_WORKER_TIP] ON [dbo].[WorkingTimer]
(
	[branchId] ASC
)
INCLUDE ( 	[posId],
	[ZId],
	[workerId],
	[In_Date],
	[Out_Date],
	[tip],
	[WorkerjobTypeName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_WorkingTimer_ZId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_WorkingTimer_ZId] ON [dbo].[WorkingTimer]
(
	[ZId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [NonClusteredIndex-20160626-235616]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160626-235616] ON [dbo].[WorkingTimer]
(
	[ZId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

SET ANSI_PADDING ON


/****** Object:  Index [online_getDashData_2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [online_getDashData_2] ON [dbo].[XDishes]
(
	[xId] ASC
)
INCLUDE ( 	[NetID],
	[DishName],
	[DishQuan],
	[DishSums],
	[CatName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_XPayments_xId]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [IX_XPayments_xId] ON [dbo].[XPayments]
(
	[xId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [delete_zEntryItem]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [delete_zEntryItem] ON [dbo].[ZEntrytable]
(
	[branchId] ASC,
	[ZIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [idx_income_getEmployeeWageTable_2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [idx_income_getEmployeeWageTable_2] ON [dbo].[ZEntrytable]
(
	[branchId] ASC,
	[valueDate] ASC
)
INCLUDE ( 	[Id],
	[posId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [idx_income_getZ]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [idx_income_getZ] ON [dbo].[ZEntrytable]
(
	[branchId] ASC,
	[LastZMade] ASC,
	[valueDate] ASC
)
INCLUDE ( 	[TotalSum],
	[CreditCard],
	[Cash],
	[Dinners],
	[OrderCount],
	[cancelCount]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER INDEX [idx_income_getZ] ON [dbo].[ZEntrytable] DISABLE

/****** Object:  Index [idx_z_pulling]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [idx_z_pulling] ON [dbo].[ZEntrytable]
(
	[posId] ASC
)
INCLUDE ( 	[ZIndex]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [idx_Z_pulling_2]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [idx_Z_pulling_2] ON [dbo].[ZEntrytable]
(
	[posId] ASC
)
INCLUDE ( 	[ZIndex]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [idx_Z_pulling_3]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [idx_Z_pulling_3] ON [dbo].[ZEntrytable]
(
	[posId] ASC,
	[ZIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [income_getZdata]    Script Date: 08/03/2017 18:58:30 ******/
CREATE NONCLUSTERED INDEX [income_getZdata] ON [dbo].[zPayments]
(
	[zId] ASC
)
INCLUDE ( 	[paymentTypeId],
	[totalAmount],
	[isNegtive]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE [dbo].[branchCurrentMonthTopTenProducts] ADD  CONSTRAINT [DF__branchCurr__Quan__44CA3770]  DEFAULT ((0)) FOR [Quan]

ALTER TABLE [dbo].[branchCurrentMonthTopTenProducts] ADD  CONSTRAINT [DF__branchCur__Price__45BE5BA9]  DEFAULT ((0)) FOR [PriceSum]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__branc__787EE5A0]  DEFAULT ((0)) FOR [branchId]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__cusYe__797309D9]  DEFAULT ((0)) FOR [cusYear]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__cusMo__7A672E12]  DEFAULT ((0)) FOR [cusMonth]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__Payme__7B5B524B]  DEFAULT ((0)) FOR [PaymentsSum]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__Order__7C4F7684]  DEFAULT ((0)) FOR [OrdersSum]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__Servi__7D439ABD]  DEFAULT ((0)) FOR [ServiceSum]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__Cance__7E37BEF6]  DEFAULT ((0)) FOR [CancelsSum]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF__branchDai__Cupon__7F2BE32F]  DEFAULT ((0)) FOR [CuponCommisionSum]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_ordersCount]  DEFAULT ((0)) FOR [ordersCount]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_cancelsCount]  DEFAULT ((0)) FOR [cancelsCount]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_dishesCount]  DEFAULT ((0)) FOR [dishesCount]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_cash]  DEFAULT ((0)) FOR [cash]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_creditCard]  DEFAULT ((0)) FOR [creditCard]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_coupons]  DEFAULT ((0)) FOR [coupons]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_hakafa]  DEFAULT ((0)) FOR [hakafa]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_checks]  DEFAULT ((0)) FOR [checks]

ALTER TABLE [dbo].[branchDailyStats] ADD  CONSTRAINT [DF_branchDailyStats_other]  DEFAULT ((0)) FOR [other]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Servi__02084FDA]  DEFAULT ((0)) FOR [ServiceSum]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Coupo__02FC7413]  DEFAULT ((0)) FOR [CouponCommision]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__03F0984C]  DEFAULT ((0)) FOR [OrderType0Count]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__04E4BC85]  DEFAULT ((0)) FOR [OrderType0Sum]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__05D8E0BE]  DEFAULT ((0)) FOR [OrderType0Dinners]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__06CD04F7]  DEFAULT ((0)) FOR [OrderType0CancelCount]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__07C12930]  DEFAULT ((0)) FOR [OrderType1Count]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__08B54D69]  DEFAULT ((0)) FOR [OrderType1Sum]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__09A971A2]  DEFAULT ((0)) FOR [OrderType1Dinners]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__0A9D95DB]  DEFAULT ((0)) FOR [OrderType1CancelCount]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__0B91BA14]  DEFAULT ((0)) FOR [OrderType2Count]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__0C85DE4D]  DEFAULT ((0)) FOR [OrderType2Sum]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__0D7A0286]  DEFAULT ((0)) FOR [OrderType2Dinners]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF__branchDai__Order__0E6E26BF]  DEFAULT ((0)) FOR [OrderType2CancelCount]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF_branchDailyStats_Brakdown_cash]  DEFAULT ((0)) FOR [cash]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF_branchDailyStats_Brakdown_creditCard]  DEFAULT ((0)) FOR [creditCard]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF_branchDailyStats_Brakdown_coupons]  DEFAULT ((0)) FOR [coupons]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF_branchDailyStats_Brakdown_hakafa]  DEFAULT ((0)) FOR [hakafa]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF_branchDailyStats_Brakdown_checks]  DEFAULT ((0)) FOR [checks]

ALTER TABLE [dbo].[branchDailyStats_Brakdown] ADD  CONSTRAINT [DF_branchDailyStats_Brakdown_other]  DEFAULT ((0)) FOR [other]

ALTER TABLE [dbo].[DishCategories] ADD  DEFAULT ((1)) FOR [lastUpdatePosId]

ALTER TABLE [dbo].[DishCategories] ADD  DEFAULT (getdate()) FOR [dateInserted]

ALTER TABLE [dbo].[Dishes] ADD  DEFAULT ((1)) FOR [lastUpdatePosId]

ALTER TABLE [dbo].[Dishes] ADD  DEFAULT (getdate()) FOR [dateInserted]

ALTER TABLE [dbo].[Dishes] ADD  DEFAULT ((1)) FOR [currentMenu]

ALTER TABLE [dbo].[DishesInOrder] ADD  CONSTRAINT [DF__DishesInO__Price__656C112C]  DEFAULT ((0)) FOR [PriceConDiscount]

ALTER TABLE [dbo].[DishesInOrder] ADD  CONSTRAINT [DF__DishesInO__Price__66603565]  DEFAULT ((0)) FOR [PriceConDiscountService]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__OpenYear__4CA06362]  DEFAULT ((0)) FOR [OpenYear]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__OpenMont__4D94879B]  DEFAULT ((0)) FOR [OpenMonth]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__OpenDay__4E88ABD4]  DEFAULT ((0)) FOR [OpenDay]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__OpenDayV__4F7CD00D]  DEFAULT ((0)) FOR [OpenDayValue]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__OpenHour__5070F446]  DEFAULT ((0)) FOR [OpenHour]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__CloseYea__5165187F]  DEFAULT ((0)) FOR [CloseYear]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__CloseMon__52593CB8]  DEFAULT ((0)) FOR [CloseMonth]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__CloseDay__534D60F1]  DEFAULT ((0)) FOR [CloseDay]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__CloseDay__5441852A]  DEFAULT ((0)) FOR [CloseDayValue]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__CloseHou__5535A963]  DEFAULT ((0)) FOR [CloseHour]

ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF__Orders__workerId__5629CD9C]  DEFAULT ((0)) FOR [workerId]

ALTER TABLE [dbo].[OrdersDelivery] ADD  CONSTRAINT [DF__OrdersDel__Serve__019E3B86]  DEFAULT ((0)) FOR [ServerDeliveryBoyID]

ALTER TABLE [dbo].[OrderSums] ADD  CONSTRAINT [DF__OrderSums__Disco__73852659]  DEFAULT ((0)) FOR [DiscountSum]

ALTER TABLE [dbo].[OrderSums] ADD  CONSTRAINT [DF__OrderSums__Servi__74794A92]  DEFAULT ((0)) FOR [ServiceSum]

ALTER TABLE [dbo].[pos] ADD  CONSTRAINT [DF_pos_dbVersion]  DEFAULT ((0)) FOR [dbVersion]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__sumClose__22AA2996]  DEFAULT ((0)) FOR [sumClose]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__sumOpen__239E4DCF]  DEFAULT ((0)) FOR [sumOpen]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__sumCancel__24927208]  DEFAULT ((0)) FOR [sumCancel]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__dinnerClose__25869641]  DEFAULT ((0)) FOR [dinnerClose]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__dinnersOpen__267ABA7A]  DEFAULT ((0)) FOR [dinnersOpen]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__OrdersCountOp__276EDEB3]  DEFAULT ((0)) FOR [OrdersCountOpen]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__OrdersCountCl__286302EC]  DEFAULT ((0)) FOR [OrdersCountClose]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__OrdersCountCa__29572725]  DEFAULT ((0)) FOR [OrdersCountCancel]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__sumDiscount__2A4B4B5E]  DEFAULT ((0)) FOR [sumDiscount]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__sumService__2B3F6F97]  DEFAULT ((0)) FOR [sumService]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF_X_openWorkerShifts]  DEFAULT ((0)) FOR [openWorkerShifts]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__CommitTime__2D27B809]  DEFAULT (getdate()) FOR [CommitTime]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__OrderType0Sum__2E1BDC42]  DEFAULT ((0)) FOR [OrderType0Sum]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__OrderType1Sum__2F10007B]  DEFAULT ((0)) FOR [OrderType1Sum]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__OrderType2Sum__300424B4]  DEFAULT ((0)) FOR [OrderType2Sum]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__WeekAgoSum__30F848ED]  DEFAULT ((0)) FOR [WeekAgoSum]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF__X__ThisMonthSum__31EC6D26]  DEFAULT ((0)) FOR [ThisMonthSum]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF_X_DinnersCloseTakeAway]  DEFAULT ((0)) FOR [DinnersCloseTakeAway]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF_X_DinnerCloseDelivery]  DEFAULT ((0)) FOR [DinnerCloseDelivery]

ALTER TABLE [dbo].[X] ADD  CONSTRAINT [DF_X_DinnerCloserestaurant]  DEFAULT ((0)) FOR [DinnerCloserestaurant]

ALTER TABLE [dbo].[XDishes] ADD  CONSTRAINT [DF__XDishes__CommitT__35BCFE0A]  DEFAULT (getdate()) FOR [CommitTime]

ALTER TABLE [dbo].[XHistory] ADD  CONSTRAINT [DF__XHistory__Commit__44FF419A]  DEFAULT (getdate()) FOR [CommitTime]

ALTER TABLE [dbo].[XHistory] ADD  CONSTRAINT [DF__XHistory__OrderT__45F365D3]  DEFAULT ((0)) FOR [OrderType0Sum]

ALTER TABLE [dbo].[XHistory] ADD  CONSTRAINT [DF__XHistory__OrderT__46E78A0C]  DEFAULT ((0)) FOR [OrderType1Sum]

ALTER TABLE [dbo].[XHistory] ADD  CONSTRAINT [DF__XHistory__OrderT__47DBAE45]  DEFAULT ((0)) FOR [OrderType2Sum]

ALTER TABLE [dbo].[XPayments] ADD  CONSTRAINT [DF__XPayments__Commi__3D5E1FD2]  DEFAULT (getdate()) FOR [CommitTime]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_TotalSum]  DEFAULT ((0)) FOR [TotalSum]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_CreditCard]  DEFAULT ((0)) FOR [CreditCard]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_Cash]  DEFAULT ((0)) FOR [Cash]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_CheckSum]  DEFAULT ((0)) FOR [CheckSum]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_Cupon]  DEFAULT ((0)) FOR [Cupon]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_Hakafa]  DEFAULT ((0)) FOR [Hakafa]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_Dinners]  DEFAULT ((0)) FOR [Dinners]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_OrderCount]  DEFAULT ((0)) FOR [OrderCount]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_cancelCount]  DEFAULT ((0)) FOR [cancelCount]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_refundCount]  DEFAULT ((0)) FOR [refundCount]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_refundSum]  DEFAULT ((0)) FOR [refundSum]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_orderDiscountCount]  DEFAULT ((0)) FOR [orderDiscountCount]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_orderDiscountSum]  DEFAULT ((0)) FOR [orderDiscountSum]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_orderCencelSum]  DEFAULT ((0)) FOR [orderCancelSum]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_orderCashBackCount]  DEFAULT ((0)) FOR [orderCashBackCount]

ALTER TABLE [dbo].[ZEntrytable] ADD  CONSTRAINT [DF_ZEntrytable_orderCashBackSum]  DEFAULT ((0)) FOR [orderCashBackSum]

ALTER TABLE [dbo].[ZEntrytable] ADD  DEFAULT ((0)) FOR [dbVersion]

ALTER TABLE [dbo].[ZEntrytable] ADD  DEFAULT (getdate()) FOR [dateInserted]

ALTER TABLE [dbo].[zPayments] ADD  CONSTRAINT [DF_zPayments_isNegtive]  DEFAULT ((0)) FOR [isNegtive]

ALTER TABLE [dbo].[branchActivities]  WITH CHECK ADD  CONSTRAINT [FK_branchActivities_branchActivityType] FOREIGN KEY([activityId])
REFERENCES [dbo].[branchActivityType] ([Id])

ALTER TABLE [dbo].[branchActivities] CHECK CONSTRAINT [FK_branchActivities_branchActivityType]

ALTER TABLE [dbo].[branchActivities]  WITH CHECK ADD  CONSTRAINT [FK_branchActivities_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[branchActivities] CHECK CONSTRAINT [FK_branchActivities_branches]

ALTER TABLE [dbo].[branchActivities]  WITH CHECK ADD  CONSTRAINT [FK_branchActivities_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[branchActivities] CHECK CONSTRAINT [FK_branchActivities_pos]

ALTER TABLE [dbo].[branchContacts]  WITH CHECK ADD  CONSTRAINT [FK_branchContacts_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[branchContacts] CHECK CONSTRAINT [FK_branchContacts_branches]

ALTER TABLE [dbo].[branchCurrentMonthTopTenCategories]  WITH CHECK ADD  CONSTRAINT [FK_branchCurrentMonthTopTenCategories_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[branchCurrentMonthTopTenCategories] CHECK CONSTRAINT [FK_branchCurrentMonthTopTenCategories_branches]

ALTER TABLE [dbo].[branchCurrentMonthTopTenCategories]  WITH CHECK ADD  CONSTRAINT [FK_branchCurrentMonthTopTenCategories_DishCategories] FOREIGN KEY([categoryId])
REFERENCES [dbo].[DishCategories] ([Id])

ALTER TABLE [dbo].[branchCurrentMonthTopTenCategories] CHECK CONSTRAINT [FK_branchCurrentMonthTopTenCategories_DishCategories]

ALTER TABLE [dbo].[branchCurrentMonthTopTenProducts]  WITH CHECK ADD  CONSTRAINT [FK_branchCurrentMonthTopTenProducts_Dishes] FOREIGN KEY([dishId])
REFERENCES [dbo].[Dishes] ([Id])

ALTER TABLE [dbo].[branchCurrentMonthTopTenProducts] CHECK CONSTRAINT [FK_branchCurrentMonthTopTenProducts_Dishes]

ALTER TABLE [dbo].[branchDailyStats]  WITH CHECK ADD  CONSTRAINT [FK_branchDailyStats_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[branchDailyStats] CHECK CONSTRAINT [FK_branchDailyStats_branches]

ALTER TABLE [dbo].[branchDailyStats]  WITH CHECK ADD  CONSTRAINT [FK_branchDailyStats_ZEntrytable] FOREIGN KEY([zId])
REFERENCES [dbo].[ZEntrytable] ([Id])

ALTER TABLE [dbo].[branchDailyStats] CHECK CONSTRAINT [FK_branchDailyStats_ZEntrytable]

ALTER TABLE [dbo].[branchDailyStats_Brakdown]  WITH CHECK ADD  CONSTRAINT [FK_branchDailyStats_Brakdown_branchDailyStats] FOREIGN KEY([branchDailyStatsId])
REFERENCES [dbo].[branchDailyStats] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[branchDailyStats_Brakdown] CHECK CONSTRAINT [FK_branchDailyStats_Brakdown_branchDailyStats]

ALTER TABLE [dbo].[branchDailyStats_Brakdown]  WITH CHECK ADD  CONSTRAINT [FK_branchDailyStats_Brakdown_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[branchDailyStats_Brakdown] CHECK CONSTRAINT [FK_branchDailyStats_Brakdown_pos]

ALTER TABLE [dbo].[CuponPayments]  WITH CHECK ADD  CONSTRAINT [FK_CuponPayments_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[CuponPayments] CHECK CONSTRAINT [FK_CuponPayments_branches]

ALTER TABLE [dbo].[CuponPayments]  WITH CHECK ADD  CONSTRAINT [FK_CuponPayments_Orders] FOREIGN KEY([orderId])
REFERENCES [dbo].[Orders] ([Id])

ALTER TABLE [dbo].[CuponPayments] CHECK CONSTRAINT [FK_CuponPayments_Orders]

ALTER TABLE [dbo].[CuponPayments]  WITH CHECK ADD  CONSTRAINT [FK_CuponPayments_Payments] FOREIGN KEY([paymentId])
REFERENCES [dbo].[Payments] ([Id])

ALTER TABLE [dbo].[CuponPayments] CHECK CONSTRAINT [FK_CuponPayments_Payments]

ALTER TABLE [dbo].[CuponPayments]  WITH CHECK ADD  CONSTRAINT [FK_CuponPayments_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[CuponPayments] CHECK CONSTRAINT [FK_CuponPayments_pos]

ALTER TABLE [dbo].[Dishes]  WITH CHECK ADD  CONSTRAINT [FK_Dishes_DishCategories] FOREIGN KEY([dishCategoryId])
REFERENCES [dbo].[DishCategories] ([Id])

ALTER TABLE [dbo].[Dishes] CHECK CONSTRAINT [FK_Dishes_DishCategories]

ALTER TABLE [dbo].[DishesInOrder]  WITH CHECK ADD  CONSTRAINT [FK_DishesInOrder_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[DishesInOrder] CHECK CONSTRAINT [FK_DishesInOrder_branches]

ALTER TABLE [dbo].[DishesInOrder]  WITH CHECK ADD  CONSTRAINT [FK_DishesInOrder_DishCategories] FOREIGN KEY([dishCategoryId])
REFERENCES [dbo].[DishCategories] ([Id])

ALTER TABLE [dbo].[DishesInOrder] CHECK CONSTRAINT [FK_DishesInOrder_DishCategories]

ALTER TABLE [dbo].[DishesInOrder]  WITH CHECK ADD  CONSTRAINT [FK_DishesInOrder_Dishes] FOREIGN KEY([dishId])
REFERENCES [dbo].[Dishes] ([Id])

ALTER TABLE [dbo].[DishesInOrder] CHECK CONSTRAINT [FK_DishesInOrder_Dishes]

ALTER TABLE [dbo].[DishesInOrder]  WITH CHECK ADD  CONSTRAINT [FK_DishesInOrder_DishesInOrderStatus] FOREIGN KEY([DishesInOrderStatusID])
REFERENCES [dbo].[DishesInOrderStatus] ([Id])

ALTER TABLE [dbo].[DishesInOrder] CHECK CONSTRAINT [FK_DishesInOrder_DishesInOrderStatus]

ALTER TABLE [dbo].[DishesInOrder]  WITH CHECK ADD  CONSTRAINT [FK_DishesInOrder_Orders] FOREIGN KEY([orderId])
REFERENCES [dbo].[Orders] ([Id])

ALTER TABLE [dbo].[DishesInOrder] CHECK CONSTRAINT [FK_DishesInOrder_Orders]

ALTER TABLE [dbo].[DishesInOrder]  WITH CHECK ADD  CONSTRAINT [FK_DishesInOrder_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[DishesInOrder] CHECK CONSTRAINT [FK_DishesInOrder_pos]

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_branches]

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_pos]

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Workers] FOREIGN KEY([workerId])
REFERENCES [dbo].[Workers] ([Id])

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Workers]

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_ZEntrytable] FOREIGN KEY([ZId])
REFERENCES [dbo].[ZEntrytable] ([Id])

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_ZEntrytable]

ALTER TABLE [dbo].[OrdersDelivery]  WITH CHECK ADD  CONSTRAINT [FK_OrdersDelivery_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[OrdersDelivery] CHECK CONSTRAINT [FK_OrdersDelivery_branches]

ALTER TABLE [dbo].[OrdersDelivery]  WITH CHECK ADD  CONSTRAINT [FK_OrdersDelivery_Orders] FOREIGN KEY([orderID])
REFERENCES [dbo].[Orders] ([Id])

ALTER TABLE [dbo].[OrdersDelivery] CHECK CONSTRAINT [FK_OrdersDelivery_Orders]

ALTER TABLE [dbo].[OrdersDelivery]  WITH CHECK ADD  CONSTRAINT [FK_OrdersDelivery_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[OrdersDelivery] CHECK CONSTRAINT [FK_OrdersDelivery_pos]

ALTER TABLE [dbo].[OrdersRestaurant]  WITH CHECK ADD  CONSTRAINT [FK_OrdersRestaurant_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[OrdersRestaurant] CHECK CONSTRAINT [FK_OrdersRestaurant_branches]

ALTER TABLE [dbo].[OrdersRestaurant]  WITH CHECK ADD  CONSTRAINT [FK_OrdersRestaurant_Orders] FOREIGN KEY([orderId])
REFERENCES [dbo].[Orders] ([Id])

ALTER TABLE [dbo].[OrdersRestaurant] CHECK CONSTRAINT [FK_OrdersRestaurant_Orders]

ALTER TABLE [dbo].[OrdersRestaurant]  WITH CHECK ADD  CONSTRAINT [FK_OrdersRestaurant_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[OrdersRestaurant] CHECK CONSTRAINT [FK_OrdersRestaurant_pos]

ALTER TABLE [dbo].[OrderSums]  WITH CHECK ADD  CONSTRAINT [FK_OrderSums_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[OrderSums] CHECK CONSTRAINT [FK_OrderSums_branches]

ALTER TABLE [dbo].[OrderSums]  WITH CHECK ADD  CONSTRAINT [FK_OrderSums_Orders] FOREIGN KEY([orderId])
REFERENCES [dbo].[Orders] ([Id])

ALTER TABLE [dbo].[OrderSums] CHECK CONSTRAINT [FK_OrderSums_Orders]

ALTER TABLE [dbo].[OrderSums]  WITH CHECK ADD  CONSTRAINT [FK_OrderSums_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[OrderSums] CHECK CONSTRAINT [FK_OrderSums_pos]

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_branches]

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Orders] FOREIGN KEY([orderId])
REFERENCES [dbo].[Orders] ([Id])

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Orders]

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_paymentTypes] FOREIGN KEY([PaymentType])
REFERENCES [dbo].[paymentTypes] ([Id])

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_paymentTypes]

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_pos]

ALTER TABLE [dbo].[paymentTypes_pos]  WITH CHECK ADD  CONSTRAINT [FK_PaymentType_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[paymentTypes_pos] CHECK CONSTRAINT [FK_PaymentType_pos]

ALTER TABLE [dbo].[paymentTypes_pos]  WITH CHECK ADD  CONSTRAINT [FK_paymentTypes_pos_paymentTypes] FOREIGN KEY([paymentTypeId])
REFERENCES [dbo].[paymentTypes] ([Id])

ALTER TABLE [dbo].[paymentTypes_pos] CHECK CONSTRAINT [FK_paymentTypes_pos_paymentTypes]

ALTER TABLE [dbo].[pos]  WITH CHECK ADD  CONSTRAINT [FK_pos_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[pos] CHECK CONSTRAINT [FK_pos_branches]

ALTER TABLE [dbo].[shifts_branch]  WITH CHECK ADD  CONSTRAINT [FK_shifts_branch_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[shifts_branch] CHECK CONSTRAINT [FK_shifts_branch_branches]

ALTER TABLE [dbo].[shifts_branch]  WITH CHECK ADD  CONSTRAINT [FK_shifts_branch_shifts] FOREIGN KEY([shiftId])
REFERENCES [dbo].[shifts] ([Id])

ALTER TABLE [dbo].[shifts_branch] CHECK CONSTRAINT [FK_shifts_branch_shifts]

ALTER TABLE [dbo].[shifts_branch_overHours]  WITH CHECK ADD  CONSTRAINT [FK_shifts_branch_overHours_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[shifts_branch_overHours] CHECK CONSTRAINT [FK_shifts_branch_overHours_branches]

ALTER TABLE [dbo].[Workers]  WITH NOCHECK ADD  CONSTRAINT [FK_Workers_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[Workers] NOCHECK CONSTRAINT [FK_Workers_branches]

ALTER TABLE [dbo].[Workers]  WITH NOCHECK ADD  CONSTRAINT [FK_Workers_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[Workers] NOCHECK CONSTRAINT [FK_Workers_pos]

ALTER TABLE [dbo].[Workers]  WITH NOCHECK ADD  CONSTRAINT [FK_Workers_WorkerJobType] FOREIGN KEY([workerJobTypeId])
REFERENCES [dbo].[WorkerJobType] ([Id])

ALTER TABLE [dbo].[Workers] NOCHECK CONSTRAINT [FK_Workers_WorkerJobType]

ALTER TABLE [dbo].[WorkingTimer]  WITH NOCHECK ADD  CONSTRAINT [FK_WorkingTimer_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[WorkingTimer] NOCHECK CONSTRAINT [FK_WorkingTimer_branches]

ALTER TABLE [dbo].[WorkingTimer]  WITH NOCHECK ADD  CONSTRAINT [FK_WorkingTimer_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[WorkingTimer] NOCHECK CONSTRAINT [FK_WorkingTimer_pos]

ALTER TABLE [dbo].[WorkingTimer]  WITH NOCHECK ADD  CONSTRAINT [FK_WorkingTimer_WorkerJobType] FOREIGN KEY([workerjobTypeId])
REFERENCES [dbo].[WorkerJobType] ([Id])

ALTER TABLE [dbo].[WorkingTimer] NOCHECK CONSTRAINT [FK_WorkingTimer_WorkerJobType]

ALTER TABLE [dbo].[WorkingTimer]  WITH NOCHECK ADD  CONSTRAINT [FK_WorkingTimer_Workers] FOREIGN KEY([workerId])
REFERENCES [dbo].[Workers] ([Id])

ALTER TABLE [dbo].[WorkingTimer] NOCHECK CONSTRAINT [FK_WorkingTimer_Workers]

ALTER TABLE [dbo].[WorkingTimer]  WITH CHECK ADD  CONSTRAINT [FK_WorkingTimer_ZEntrytable] FOREIGN KEY([ZId])
REFERENCES [dbo].[ZEntrytable] ([Id])

ALTER TABLE [dbo].[WorkingTimer] CHECK CONSTRAINT [FK_WorkingTimer_ZEntrytable]

ALTER TABLE [dbo].[X]  WITH NOCHECK ADD  CONSTRAINT [FK_X_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[X] NOCHECK CONSTRAINT [FK_X_pos]

ALTER TABLE [dbo].[XDishes]  WITH CHECK ADD  CONSTRAINT [FK_XDishes_Dishes] FOREIGN KEY([dishID])
REFERENCES [dbo].[Dishes] ([Id])

ALTER TABLE [dbo].[XDishes] CHECK CONSTRAINT [FK_XDishes_Dishes]

ALTER TABLE [dbo].[XDishes]  WITH CHECK ADD  CONSTRAINT [FK_XDishes_X] FOREIGN KEY([xId])
REFERENCES [dbo].[X] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[XDishes] CHECK CONSTRAINT [FK_XDishes_X]

ALTER TABLE [dbo].[XHistory]  WITH CHECK ADD  CONSTRAINT [FK_XHistory_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[XHistory] CHECK CONSTRAINT [FK_XHistory_pos]

ALTER TABLE [dbo].[XPayments]  WITH CHECK ADD  CONSTRAINT [FK_XPayments_X] FOREIGN KEY([xId])
REFERENCES [dbo].[X] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[XPayments] CHECK CONSTRAINT [FK_XPayments_X]

ALTER TABLE [dbo].[ZEntrytable]  WITH CHECK ADD  CONSTRAINT [FK_ZEntrytable_branches] FOREIGN KEY([branchId])
REFERENCES [dbo].[branches] ([Id])

ALTER TABLE [dbo].[ZEntrytable] CHECK CONSTRAINT [FK_ZEntrytable_branches]

ALTER TABLE [dbo].[ZEntrytable]  WITH CHECK ADD  CONSTRAINT [FK_ZEntrytable_pos] FOREIGN KEY([posId])
REFERENCES [dbo].[pos] ([Id])

ALTER TABLE [dbo].[ZEntrytable] CHECK CONSTRAINT [FK_ZEntrytable_pos]

ALTER TABLE [dbo].[zPayments]  WITH CHECK ADD  CONSTRAINT [FK_zPayments_paymentTypes] FOREIGN KEY([paymentTypeId])
REFERENCES [dbo].[paymentTypes] ([Id])

ALTER TABLE [dbo].[zPayments] CHECK CONSTRAINT [FK_zPayments_paymentTypes]

ALTER TABLE [dbo].[zPayments]  WITH CHECK ADD  CONSTRAINT [FK_zPayments_ZEntrytable] FOREIGN KEY([zId])
REFERENCES [dbo].[ZEntrytable] ([Id])

ALTER TABLE [dbo].[zPayments] CHECK CONSTRAINT [FK_zPayments_ZEntrytable]

