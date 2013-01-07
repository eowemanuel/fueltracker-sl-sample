USE [master]
GO
/****** Object:  Database [FuelTracker]    Script Date: 10/12/2011 18:04:20 ******/
CREATE DATABASE [FuelTracker] ON  PRIMARY 
( NAME = N'FuelTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FuelTracker.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FuelTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FuelTracker_log.LDF' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FuelTracker] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FuelTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FuelTracker] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [FuelTracker] SET ANSI_NULLS OFF
GO
ALTER DATABASE [FuelTracker] SET ANSI_PADDING OFF
GO
ALTER DATABASE [FuelTracker] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [FuelTracker] SET ARITHABORT OFF
GO
ALTER DATABASE [FuelTracker] SET AUTO_CLOSE ON
GO
ALTER DATABASE [FuelTracker] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [FuelTracker] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [FuelTracker] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [FuelTracker] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [FuelTracker] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [FuelTracker] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [FuelTracker] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [FuelTracker] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [FuelTracker] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [FuelTracker] SET  ENABLE_BROKER
GO
ALTER DATABASE [FuelTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [FuelTracker] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [FuelTracker] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [FuelTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [FuelTracker] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [FuelTracker] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [FuelTracker] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [FuelTracker] SET  READ_WRITE
GO
ALTER DATABASE [FuelTracker] SET RECOVERY SIMPLE
GO
ALTER DATABASE [FuelTracker] SET  MULTI_USER
GO
ALTER DATABASE [FuelTracker] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [FuelTracker] SET DB_CHAINING OFF
GO
USE [FuelTracker]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 10/12/2011 18:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 10/12/2011 18:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 10/12/2011 18:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Manufacturer] [nvarchar](max) NULL,
	[Model] [nvarchar](max) NULL,
	[EngineVolume] [float] NOT NULL,
	[Types] [nvarchar](max) NULL,
	[Engine] [nvarchar](max) NULL,
	[ManufacturingDate] [datetime] NOT NULL,
	[UserProfileId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FuelTracks]    Script Date: 10/12/2011 18:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FuelTracks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FuelingDate] [datetime] NOT NULL,
	[DateExpiration] [datetime] NOT NULL,
	[Quantity] [float] NOT NULL,
	[CarId] [int] NOT NULL,
	[Distance] [float] NOT NULL,
	[CarMiliage] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [UserProfile_Cars]    Script Date: 10/12/2011 18:04:21 ******/
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [UserProfile_Cars] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfiles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [UserProfile_Cars]
GO
/****** Object:  ForeignKey [FuelTrack_Car]    Script Date: 10/12/2011 18:04:21 ******/
ALTER TABLE [dbo].[FuelTracks]  WITH CHECK ADD  CONSTRAINT [FuelTrack_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Cars] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FuelTracks] CHECK CONSTRAINT [FuelTrack_Car]
GO
