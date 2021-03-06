USE [master]
GO
/****** Object:  Database [LibraryDb]    Script Date: 1/26/2019 10:42:40 PM ******/
CREATE DATABASE [LibraryDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LibraryDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LibraryDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LibraryDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryDb] SET RECOVERY FULL 
GO
ALTER DATABASE [LibraryDb] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LibraryDb', N'ON'
GO
ALTER DATABASE [LibraryDb] SET QUERY_STORE = OFF
GO
USE [LibraryDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [LibraryDb]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 1/26/2019 10:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 1/26/2019 10:42:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Count] [int] NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 1/26/2019 10:42:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsFinish] [bit] NOT NULL,
	[DelayPrice] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AdminId] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/26/2019 10:42:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[BookId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (1, N'Coğrafiya', 11, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (2, N'Hərb və sülh', 20, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (4, N'Cinayətkar', 23, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (5, N'Pillələr', 22, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (6, N'Başsız atlı', 7, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (7, N'Küləklər şəhəri', 30, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (8, N'Azərbaycan tarixi', 11, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (9, N'Cəmiyyət', 9, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (10, N'Müharibə', 22, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Count], [UserId]) VALUES (11, N'Senator', 4, NULL)
SET IDENTITY_INSERT [dbo].[Books] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [StartDate], [EndDate], [IsFinish], [DelayPrice], [BookId], [UserId], [AdminId]) VALUES (22, CAST(N'2019-01-26T22:39:20.173' AS DateTime), CAST(N'2019-02-26T22:39:20.173' AS DateTime), 0, 5, 9, 7, NULL)
INSERT [dbo].[Orders] ([Id], [StartDate], [EndDate], [IsFinish], [DelayPrice], [BookId], [UserId], [AdminId]) VALUES (23, CAST(N'2019-01-26T22:39:28.847' AS DateTime), CAST(N'2019-02-26T22:39:28.847' AS DateTime), 0, 5, 11, 10, NULL)
INSERT [dbo].[Orders] ([Id], [StartDate], [EndDate], [IsFinish], [DelayPrice], [BookId], [UserId], [AdminId]) VALUES (24, CAST(N'2019-01-26T22:39:37.300' AS DateTime), CAST(N'2019-02-26T22:39:37.300' AS DateTime), 0, 5, 9, 8, NULL)
INSERT [dbo].[Orders] ([Id], [StartDate], [EndDate], [IsFinish], [DelayPrice], [BookId], [UserId], [AdminId]) VALUES (25, CAST(N'2019-01-26T22:39:45.953' AS DateTime), CAST(N'2019-02-26T22:39:45.953' AS DateTime), 0, 5, 8, 6, NULL)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (1, 917534, N'Nahid', N'Şükürlü', N'055-439-44-50', N'nahidash@gmail.com', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (2, 578264, N'Xalid', N'Şükürlü', N'070-225-12-12', N'xalid@mail.ru', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (3, 366543, N'Aqşin', N'Şükürlü', N'050-338-17-59', N'aqshin@mail.ru', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (4, 638324, N'Nihat', N'Camalov', N'051-223-16-74', N'niko@mail.ru', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (6, 985364, N'Sabir', N'Sabirli', N'055-227-34-96', N'sasha@mail.ru', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (7, 630184, N'Orxan', N'İbayev', N'055-164-12-64', N'kbftgh@mail.ru', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (8, 149146, N'Rəvan', N'Əliyev', N'077-584-37-28', N'revan@mail.ru', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (9, 245364, N'Fərid', N'Zahidli', N'077-855-37-37', N'farid@gmail.com', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (10, 102849, N'Rəşad', N'Təhməzli', N'055-334-16-48', N'resad@mail.ru', NULL)
INSERT [dbo].[Users] ([Id], [Number], [Name], [Surname], [Phone], [Email], [BookId]) VALUES (11, 957402, N'Zakir', N'Ferziyev', N'077-234-34-34', N'zakir@mail.ru', NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Users]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Admins] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Admins] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Admins]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Books]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Books]
GO
USE [master]
GO
ALTER DATABASE [LibraryDb] SET  READ_WRITE 
GO
