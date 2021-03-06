USE [master]
GO
/****** Object:  Database [Luna]    Script Date: 13/07/2015 09:15:23 ******/
CREATE DATABASE [Luna]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Luna', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Luna.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Luna_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Luna_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Luna] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Luna].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Luna] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Luna] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Luna] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Luna] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Luna] SET ARITHABORT OFF 
GO
ALTER DATABASE [Luna] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Luna] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Luna] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Luna] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Luna] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Luna] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Luna] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Luna] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Luna] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Luna] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Luna] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Luna] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Luna] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Luna] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Luna] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Luna] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Luna] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Luna] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Luna] SET  MULTI_USER 
GO
ALTER DATABASE [Luna] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Luna] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Luna] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Luna] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Luna] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Luna]
GO
/****** Object:  Schema [crm]    Script Date: 13/07/2015 09:15:23 ******/
CREATE SCHEMA [crm]
GO
/****** Object:  Schema [meta]    Script Date: 13/07/2015 09:15:23 ******/
CREATE SCHEMA [meta]
GO
/****** Object:  Table [crm].[Addresses]    Script Date: 13/07/2015 09:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [crm].[Addresses](
	[id_address] [uniqueidentifier] NOT NULL,
	[id_repository] [uniqueidentifier] NOT NULL,
	[address] [nvarchar](250) NOT NULL,
	[zipCode] [nvarchar](20) NULL,
	[city] [nvarchar](250) NOT NULL,
	[country] [nvarchar](250) NULL,
	[latitude] [float] NULL,
	[longitude] [float] NULL,
 CONSTRAINT [PK__Addresse__3214EC07E7BA4B69] PRIMARY KEY CLUSTERED 
(
	[id_address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [crm].[Contacts]    Script Date: 13/07/2015 09:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [crm].[Contacts](
	[id_contact] [uniqueidentifier] NOT NULL,
	[id_repository] [uniqueidentifier] NOT NULL,
	[id_contact_address] [uniqueidentifier] NULL,
	[id_billing_address] [uniqueidentifier] NULL,
	[name] [nvarchar](250) NOT NULL,
	[surname] [nvarchar](100) NULL,
	[title] [nvarchar](20) NULL,
	[phone] [nvarchar](20) NULL,
	[mobile] [nvarchar](20) NULL,
	[phone_second] [nvarchar](20) NULL,
	[mobile_second] [nvarchar](20) NULL,
	[fax] [nvarchar](20) NULL,
	[email] [nvarchar](50) NULL,
	[email_second] [nvarchar](50) NULL,
	[no_mail] [bit] NULL,
	[skype] [nvarchar](100) NULL,
	[linkedin] [nvarchar](100) NULL,
	[twitter] [nvarchar](100) NULL,
	[facebook] [nvarchar](100) NULL,
	[company] [nvarchar](100) NULL,
	[description] [nvarchar](max) NULL,
	[source] [nvarchar](100) NULL,
	[comment] [nvarchar](max) NULL,
 CONSTRAINT [PK__Contacts__3214EC07AA533EA2] PRIMARY KEY CLUSTERED 
(
	[id_contact] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [crm].[Link_Contacts_Tags]    Script Date: 13/07/2015 09:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [crm].[Link_Contacts_Tags](
	[id_contact] [uniqueidentifier] NOT NULL,
	[id_tag] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Link_Contacts_Tags] PRIMARY KEY CLUSTERED 
(
	[id_contact] ASC,
	[id_tag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [crm].[Tags]    Script Date: 13/07/2015 09:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [crm].[Tags](
	[id_tag] [uniqueidentifier] NOT NULL,
	[id_repository] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[color] [varchar](7) NULL,
	[last_update] [datetime] NOT NULL,
	[version] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__Tags__3214EC075913556E] PRIMARY KEY CLUSTERED 
(
	[id_tag] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [meta].[Repositories]    Script Date: 13/07/2015 09:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [meta].[Repositories](
	[id_repository] [uniqueidentifier] NOT NULL,
	[id_subscription] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[schema_version] [decimal](19, 6) NOT NULL,
	[params] [nvarchar](max) NULL,
	[last_update] [datetime2](7) NOT NULL,
	[version] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_repository] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [meta].[Subscriptions]    Script Date: 13/07/2015 09:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [meta].[Subscriptions](
	[id_subscription] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[start] [datetime] NOT NULL,
	[closing] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_subscription] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [meta].[Users]    Script Date: 13/07/2015 09:15:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [meta].[Users](
	[id_user] [uniqueidentifier] NOT NULL,
	[id_subscription] [uniqueidentifier] NOT NULL,
	[azure_login] [nvarchar](255) NOT NULL,
	[last_query] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [crm].[Contacts] ADD  CONSTRAINT [DF__Contacts__Id__628FA481]  DEFAULT (newid()) FOR [id_contact]
GO
ALTER TABLE [crm].[Tags] ADD  CONSTRAINT [DF__Tags__Id__5CD6CB2B]  DEFAULT (newid()) FOR [id_tag]
GO
ALTER TABLE [crm].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Repositories] FOREIGN KEY([id_repository])
REFERENCES [meta].[Repositories] ([id_repository])
GO
ALTER TABLE [crm].[Addresses] CHECK CONSTRAINT [FK_Addresses_Repositories]
GO
ALTER TABLE [crm].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Addresses] FOREIGN KEY([id_contact_address])
REFERENCES [crm].[Addresses] ([id_address])
GO
ALTER TABLE [crm].[Contacts] CHECK CONSTRAINT [FK_Contacts_Addresses]
GO
ALTER TABLE [crm].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Addresses1] FOREIGN KEY([id_billing_address])
REFERENCES [crm].[Addresses] ([id_address])
GO
ALTER TABLE [crm].[Contacts] CHECK CONSTRAINT [FK_Contacts_Addresses1]
GO
ALTER TABLE [crm].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Repositories] FOREIGN KEY([id_repository])
REFERENCES [meta].[Repositories] ([id_repository])
GO
ALTER TABLE [crm].[Contacts] CHECK CONSTRAINT [FK_Contacts_Repositories]
GO
ALTER TABLE [crm].[Link_Contacts_Tags]  WITH CHECK ADD  CONSTRAINT [FK_Link_Contacts_Tags_Contacts] FOREIGN KEY([id_contact])
REFERENCES [crm].[Contacts] ([id_contact])
GO
ALTER TABLE [crm].[Link_Contacts_Tags] CHECK CONSTRAINT [FK_Link_Contacts_Tags_Contacts]
GO
ALTER TABLE [crm].[Link_Contacts_Tags]  WITH CHECK ADD  CONSTRAINT [FK_Link_Contacts_Tags_Tags] FOREIGN KEY([id_tag])
REFERENCES [crm].[Tags] ([id_tag])
GO
ALTER TABLE [crm].[Link_Contacts_Tags] CHECK CONSTRAINT [FK_Link_Contacts_Tags_Tags]
GO
ALTER TABLE [crm].[Tags]  WITH CHECK ADD  CONSTRAINT [FK_Tags_Repositories] FOREIGN KEY([id_repository])
REFERENCES [meta].[Repositories] ([id_repository])
GO
ALTER TABLE [crm].[Tags] CHECK CONSTRAINT [FK_Tags_Repositories]
GO
USE [master]
GO
ALTER DATABASE [Luna] SET  READ_WRITE 
GO
