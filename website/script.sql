/****** Object:  Database [VSQ]    Script Date: 1/20/2017 5:04:26 PM ******/
CREATE DATABASE [VSQ] ON  PRIMARY 
( NAME = N'VSQ', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.VSMJBNTBK20\MSSQL\DATA\VSQ.mdf' , SIZE = 52224KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'VSQ_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.VSMJBNTBK20\MSSQL\DATA\VSQ_log.ldf' , SIZE = 568896KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VSQ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VSQ] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VSQ] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VSQ] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VSQ] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VSQ] SET ARITHABORT OFF 
GO
ALTER DATABASE [VSQ] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VSQ] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VSQ] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VSQ] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VSQ] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VSQ] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VSQ] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VSQ] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VSQ] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VSQ] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VSQ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VSQ] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VSQ] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VSQ] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VSQ] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VSQ] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VSQ] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VSQ] SET RECOVERY FULL 
GO
ALTER DATABASE [VSQ] SET  MULTI_USER 
GO
ALTER DATABASE [VSQ] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VSQ] SET DB_CHAINING OFF 
GO
/****** Object:  Table [dbo].[ESS_module]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESS_module](
	[PK] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ESS_module] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ESS_User_Info]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESS_User_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_Email] [nvarchar](max) NOT NULL,
	[Company] [nvarchar](max) NULL,
	[Module_number] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HRMS_module]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HRMS_module](
	[PK] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HRMS_User_Info]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HRMS_User_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_Email] [nvarchar](max) NOT NULL,
	[Company] [nvarchar](max) NULL,
	[Module_number] [int] NOT NULL,
 CONSTRAINT [PK_HRMS_User_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HRSS_module]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HRSS_module](
	[PK] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_HRSS_module] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HRSS_User_Info]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HRSS_User_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_Email] [nvarchar](max) NOT NULL,
	[Company] [nvarchar](max) NULL,
	[Module_number] [int] NOT NULL,
 CONSTRAINT [PK_HRSS_User_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InputType]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InputType](
	[PK] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_InputType] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Question_Answer_Attachment]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question_Answer_Attachment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ref_FK] [int] NOT NULL,
	[doc_type] [int] NOT NULL,
 CONSTRAINT [PK_Question_Answer_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Question_Answer_OptionType]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question_Answer_OptionType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ref_FK] [int] NOT NULL,
	[Ans_Option] [nvarchar](max) NULL,
 CONSTRAINT [PK_Question_Answer_OptionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Question_Answer_TextType]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question_Answer_TextType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ref_FK] [int] NOT NULL,
	[Ans_Default] [nvarchar](max) NULL,
	[Field_Type] [int] NULL,
 CONSTRAINT [PK_Question_Answer_TextType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuestionBank]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionBank](
	[Ref_Code] [int] IDENTITY(1000,1) NOT NULL,
	[System_FK] [int] NULL,
	[Module_FK] [int] NOT NULL,
	[Ques] [nvarchar](max) NULL,
	[In_Type_FK] [int] NULL,
	[Date_Time] [datetime] NULL CONSTRAINT [DF_QuestionBank_Date_Time]  DEFAULT (getdate()),
	[Seq_Number] [int] NULL,
 CONSTRAINT [PK_QuestionBank] PRIMARY KEY CLUSTERED 
(
	[Ref_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SAAS_module]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SAAS_module](
	[PK] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SAAS_module] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SAAS_User_Info]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SAAS_User_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_Email] [nvarchar](max) NOT NULL,
	[Company] [nvarchar](max) NULL,
	[Module_number] [int] NOT NULL,
 CONSTRAINT [PK_SAAS_User_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_List]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_List](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_System_List] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Answer_Option]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Answer_Option](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_email] [nvarchar](max) NOT NULL,
	[ref_code] [int] NOT NULL,
	[answer_ID] [int] NOT NULL,
	[System_FK] [int] NOT NULL,
	[Module_FK] [int] NOT NULL,
 CONSTRAINT [PK_User_Answer_Option] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Answer_Text]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Answer_Text](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_email] [nvarchar](max) NOT NULL,
	[ref_code] [int] NOT NULL,
	[answer_text] [nvarchar](max) NULL,
	[System_FK] [int] NOT NULL,
	[Module_FK] [int] NOT NULL,
 CONSTRAINT [PK_User_Answer_Text] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Attachment]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Attachment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_email] [nvarchar](max) NOT NULL,
	[ref_code] [int] NOT NULL,
	[doc_type] [int] NOT NULL,
	[path] [nvarchar](max) NULL,
	[System_FK] [int] NOT NULL,
	[Module_FK] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAuth]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAuth](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[user_role] [nvarchar](1) NOT NULL,
	[Company] [nvarchar](max) NULL,
	[username] [nvarchar](max) NULL,
	[status] [char](1) NOT NULL,
 CONSTRAINT [PK_UserAuth] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (1, N'eLeave')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (2, N'eOvertime')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (3, N'eAttendance')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (4, N'eCanteen')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (5, N'eClaim')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (6, N'eStaff')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (7, N'eShare')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (8, N'ePayslip')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (9, N'eEA')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (10, N'ePCB')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (11, N'eTransfer')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (12, N'eTraining')
INSERT [dbo].[ESS_module] ([PK], [Name]) VALUES (13, N'eConfirmation')
SET IDENTITY_INSERT [dbo].[ESS_User_Info] ON 

INSERT [dbo].[ESS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (23, N'user', N'user', 1)
INSERT [dbo].[ESS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (24, N'user', N'user', 2)
INSERT [dbo].[ESS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (25, N'user', N'user', 4)
INSERT [dbo].[ESS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (26, N'user', N'user', 5)
INSERT [dbo].[ESS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (28, N'TDKLamda@Company.com', N'TDK Lamda', 1)
INSERT [dbo].[ESS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (29, N'TDKLamda@Company.com', N'TDK Lamda', 4)
SET IDENTITY_INSERT [dbo].[ESS_User_Info] OFF
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (1, N'Admin Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (2, N'Employee Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (3, N'Payroll Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (4, N'Attendance Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (5, N'Leave Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (6, N'Benefit Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (7, N'Transport Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (8, N'Inventory Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (9, N'FSI Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (10, N'Import Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (11, N'ESOS Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (12, N'Staff Inventory Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (13, N'Transport Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (14, N'Manpower Module')
INSERT [dbo].[HRMS_module] ([PK], [Name]) VALUES (15, N'Recruitment Module')
SET IDENTITY_INSERT [dbo].[HRMS_User_Info] ON 

INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (28, N'user', N'user', 1)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (29, N'user', N'user', 4)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (33, N'user', N'user', 2)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (34, N'user', N'user', 5)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (35, N'user', N'user', 7)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (36, N'user', N'user', 8)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (37, N'Chemsain@company.my', N'Chemsain', 1)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (38, N'Chemsain@company.my', N'Chemsain', 4)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (39, N'Chemsain@company.my', N'Chemsain', 5)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (40, N'Chemsain@company.my', N'Chemsain', 8)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (46, N'TDKLamda@Company.com', N'TDK Lamda', 1)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (47, N'TDKLamda@Company.com', N'TDK Lamda', 2)
INSERT [dbo].[HRMS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (48, N'TDKLamda@Company.com', N'TDK Lamda', 5)
SET IDENTITY_INSERT [dbo].[HRMS_User_Info] OFF
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (1, N'Blenz')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (2, N'BzAnalytics')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (3, N'OrgChart')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (4, N'Off-Boarding')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (5, N'Recruitment & Selection')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (6, N'Recruitment Portal')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (7, N'TNA')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (8, N'Performance Scorecard')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (9, N'People Development')
INSERT [dbo].[HRSS_module] ([PK], [Name]) VALUES (10, N'Manpower Turnover Forecasting')
SET IDENTITY_INSERT [dbo].[HRSS_User_Info] ON 

INSERT [dbo].[HRSS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (20, N'user', N'user', 2)
INSERT [dbo].[HRSS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (21, N'user', N'user', 4)
INSERT [dbo].[HRSS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (23, N'user', N'user', 1)
INSERT [dbo].[HRSS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (24, N'TDKLamda@Company.com', N'TDK Lamda', 1)
SET IDENTITY_INSERT [dbo].[HRSS_User_Info] OFF
INSERT [dbo].[InputType] ([PK], [Name]) VALUES (1, N'Text Box')
INSERT [dbo].[InputType] ([PK], [Name]) VALUES (2, N'Memo')
INSERT [dbo].[InputType] ([PK], [Name]) VALUES (3, N'Radio Button')
INSERT [dbo].[InputType] ([PK], [Name]) VALUES (4, N'Check Box')
INSERT [dbo].[InputType] ([PK], [Name]) VALUES (5, N'Attachment')
SET IDENTITY_INSERT [dbo].[Question_Answer_Attachment] ON 

INSERT [dbo].[Question_Answer_Attachment] ([ID], [Ref_FK], [doc_type]) VALUES (4, 1068, 1)
SET IDENTITY_INSERT [dbo].[Question_Answer_Attachment] OFF
SET IDENTITY_INSERT [dbo].[Question_Answer_OptionType] ON 

INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (212, 1059, N'1')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (213, 1059, N'2')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (214, 1059, N'3')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (215, 1059, N'4')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (220, 1061, N'1')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (221, 1061, N'2')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (222, 1061, N'3')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (223, 1061, N'4')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (224, 1058, N'2')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (225, 1058, N'4')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (226, 1060, N'1')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (227, 1060, N'3')
INSERT [dbo].[Question_Answer_OptionType] ([ID], [Ref_FK], [Ans_Option]) VALUES (228, 1060, N'4')
SET IDENTITY_INSERT [dbo].[Question_Answer_OptionType] OFF
SET IDENTITY_INSERT [dbo].[Question_Answer_TextType] ON 

INSERT [dbo].[Question_Answer_TextType] ([ID], [Ref_FK], [Ans_Default], [Field_Type]) VALUES (72, 1057, N'', 1)
INSERT [dbo].[Question_Answer_TextType] ([ID], [Ref_FK], [Ans_Default], [Field_Type]) VALUES (73, 1062, N'ada saja', 1)
INSERT [dbo].[Question_Answer_TextType] ([ID], [Ref_FK], [Ans_Default], [Field_Type]) VALUES (74, 1063, N'hehe', 1)
INSERT [dbo].[Question_Answer_TextType] ([ID], [Ref_FK], [Ans_Default], [Field_Type]) VALUES (75, 1064, N'hehe', 1)
INSERT [dbo].[Question_Answer_TextType] ([ID], [Ref_FK], [Ans_Default], [Field_Type]) VALUES (76, 1065, N'', 1)
INSERT [dbo].[Question_Answer_TextType] ([ID], [Ref_FK], [Ans_Default], [Field_Type]) VALUES (77, 1066, N'geralt', 1)
INSERT [dbo].[Question_Answer_TextType] ([ID], [Ref_FK], [Ans_Default], [Field_Type]) VALUES (78, 1067, N'hehe huhu hihi', 1)
SET IDENTITY_INSERT [dbo].[Question_Answer_TextType] OFF
SET IDENTITY_INSERT [dbo].[QuestionBank] ON 

INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1057, 1, 1, N'hello world', 1, CAST(N'2017-01-18 12:05:28.347' AS DateTime), 1)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1058, 1, 1, N'checkbox testing', 4, CAST(N'2017-01-20 16:44:33.873' AS DateTime), 2)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1059, 1, 1, N'radio button testing', 3, CAST(N'2017-01-19 09:37:48.883' AS DateTime), 3)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1060, 1, 1, N'radio button testing', 3, CAST(N'2017-01-20 16:44:44.373' AS DateTime), 4)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1061, 1, 1, N'checkbox testing', 4, CAST(N'2017-01-19 09:38:08.870' AS DateTime), 5)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1062, 1, 1, N'hello world', 1, CAST(N'2017-01-19 09:38:23.890' AS DateTime), 6)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1063, 1, 1, N'memo', 2, CAST(N'2017-01-19 09:39:00.537' AS DateTime), 8)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1064, 1, 1, N'memo', 2, CAST(N'2017-01-19 09:39:07.377' AS DateTime), 7)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1065, 1, 1, N'lagi2', 1, CAST(N'2017-01-19 09:39:39.730' AS DateTime), 9)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1066, 1, 1, N'lagi2', 1, CAST(N'2017-01-19 09:39:55.040' AS DateTime), 10)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1067, 1, 1, N'memu', 2, CAST(N'2017-01-19 09:40:23.353' AS DateTime), 11)
INSERT [dbo].[QuestionBank] ([Ref_Code], [System_FK], [Module_FK], [Ques], [In_Type_FK], [Date_Time], [Seq_Number]) VALUES (1068, 1, 1, N'attachment sample', 5, CAST(N'2017-01-20 15:38:08.023' AS DateTime), 12)
SET IDENTITY_INSERT [dbo].[QuestionBank] OFF
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (1, N'Admin Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (2, N'Employee Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (3, N'Payroll Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (4, N'Attendance Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (5, N'Leave Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (6, N'Benefit Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (7, N'Transport Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (8, N'Inventory Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (9, N'FSI Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (10, N'Import Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (11, N'ESOS Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (12, N'Staff Inventory Module')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (13, N'eLeave')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (14, N'eOvertime')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (15, N'eAttendance')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (16, N'eCanteen')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (17, N'eClaim')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (18, N'eStaff')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (19, N'eShare')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (20, N'ePayslip')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (21, N'eEA')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (22, N'ePCB')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (23, N'eTransfer')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (24, N'eTraining')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (25, N'eConfirmation')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (26, N'Blenz')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (27, N'BzAnalytics')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (28, N'OrgChart')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (29, N'Off-Boarding')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (30, N'Recruitment & Selection')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (31, N'Recruitment Portal')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (32, N'TNA')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (33, N'Performance Scorecard')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (34, N'People Development')
INSERT [dbo].[SAAS_module] ([PK], [Name]) VALUES (35, N'Manpower Turnover Forecasting')
SET IDENTITY_INSERT [dbo].[SAAS_User_Info] ON 

INSERT [dbo].[SAAS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (54, N'user', N'user', 1)
INSERT [dbo].[SAAS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (55, N'user', N'user', 2)
INSERT [dbo].[SAAS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (56, N'user', N'user', 4)
INSERT [dbo].[SAAS_User_Info] ([ID], [User_Email], [Company], [Module_number]) VALUES (57, N'user', N'user', 5)
SET IDENTITY_INSERT [dbo].[SAAS_User_Info] OFF
SET IDENTITY_INSERT [dbo].[System_List] ON 

INSERT [dbo].[System_List] ([ID], [Name]) VALUES (1, N'HRMS      ')
INSERT [dbo].[System_List] ([ID], [Name]) VALUES (2, N'ESS       ')
INSERT [dbo].[System_List] ([ID], [Name]) VALUES (3, N'HRSS      ')
INSERT [dbo].[System_List] ([ID], [Name]) VALUES (4, N'SAAS      ')
SET IDENTITY_INSERT [dbo].[System_List] OFF
SET IDENTITY_INSERT [dbo].[User_Answer_Option] ON 

INSERT [dbo].[User_Answer_Option] ([ID], [user_email], [ref_code], [answer_ID], [System_FK], [Module_FK]) VALUES (12, N'user', 1058, 199, 1, 1)
INSERT [dbo].[User_Answer_Option] ([ID], [user_email], [ref_code], [answer_ID], [System_FK], [Module_FK]) VALUES (13, N'user', 1058, 200, 1, 1)
SET IDENTITY_INSERT [dbo].[User_Answer_Option] OFF
SET IDENTITY_INSERT [dbo].[User_Answer_Text] ON 

INSERT [dbo].[User_Answer_Text] ([ID], [user_email], [ref_code], [answer_text], [System_FK], [Module_FK]) VALUES (26, N'user', 1057, N'helloo', 1, 1)
SET IDENTITY_INSERT [dbo].[User_Answer_Text] OFF
SET IDENTITY_INSERT [dbo].[User_Attachment] ON 

INSERT [dbo].[User_Attachment] ([ID], [user_email], [ref_code], [doc_type], [path], [System_FK], [Module_FK]) VALUES (12, N'user', 1068, 1, N'D:\Projects\Visualsolution\E-Questionnaire\VSQN\Attachment\Documents\EMPLOYEE QUESTIONNAIRE.doc', 1, 1)
SET IDENTITY_INSERT [dbo].[User_Attachment] OFF
SET IDENTITY_INSERT [dbo].[UserAuth] ON 

INSERT [dbo].[UserAuth] ([ID], [email], [password], [user_role], [Company], [username], [status]) VALUES (1, N'admin', N'nolZJJJbX6IQCzkJ2ksL/Q==', N'M', N'Visual Solutions', N'admin', N'E')
INSERT [dbo].[UserAuth] ([ID], [email], [password], [user_role], [Company], [username], [status]) VALUES (3, N'user', N'nolZJJJbX6IQCzkJ2ksL/Q==', N'U', N'user', N'user', N'E')
INSERT [dbo].[UserAuth] ([ID], [email], [password], [user_role], [Company], [username], [status]) VALUES (4, N'Chemsain@company.my', N'nolZJJJbX6IQCzkJ2ksL/Q==', N'U', N'Chemsain', N'Chemsain', N'E')
INSERT [dbo].[UserAuth] ([ID], [email], [password], [user_role], [Company], [username], [status]) VALUES (6, N'TDKLamda@Company.com', N'nolZJJJbX6IQCzkJ2ksL/Q==', N'U', N'TDK Lamda', N'TDK Lamda', N'E')
INSERT [dbo].[UserAuth] ([ID], [email], [password], [user_role], [Company], [username], [status]) VALUES (7, N'admin2', N'nolZJJJbX6IQCzkJ2ksL/Q==', N'A', N'admin second', N'admin second', N'E')
SET IDENTITY_INSERT [dbo].[UserAuth] OFF
ALTER TABLE [dbo].[ESS_User_Info]  WITH CHECK ADD  CONSTRAINT [FK_ESS_User_Info_ESS_module] FOREIGN KEY([Module_number])
REFERENCES [dbo].[ESS_module] ([PK])
GO
ALTER TABLE [dbo].[ESS_User_Info] CHECK CONSTRAINT [FK_ESS_User_Info_ESS_module]
GO
ALTER TABLE [dbo].[HRMS_User_Info]  WITH CHECK ADD  CONSTRAINT [FK_HRMS_User_Info_HRMS_module] FOREIGN KEY([Module_number])
REFERENCES [dbo].[HRMS_module] ([PK])
GO
ALTER TABLE [dbo].[HRMS_User_Info] CHECK CONSTRAINT [FK_HRMS_User_Info_HRMS_module]
GO
ALTER TABLE [dbo].[HRSS_User_Info]  WITH CHECK ADD  CONSTRAINT [FK_HRSS_User_Info_HRSS_module] FOREIGN KEY([Module_number])
REFERENCES [dbo].[HRSS_module] ([PK])
GO
ALTER TABLE [dbo].[HRSS_User_Info] CHECK CONSTRAINT [FK_HRSS_User_Info_HRSS_module]
GO
ALTER TABLE [dbo].[Question_Answer_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Question_Answer_Attachment_QuestionBank] FOREIGN KEY([Ref_FK])
REFERENCES [dbo].[QuestionBank] ([Ref_Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Question_Answer_Attachment] CHECK CONSTRAINT [FK_Question_Answer_Attachment_QuestionBank]
GO
ALTER TABLE [dbo].[Question_Answer_OptionType]  WITH CHECK ADD  CONSTRAINT [FK_Question_Answer_OptionType_QuestionBank] FOREIGN KEY([Ref_FK])
REFERENCES [dbo].[QuestionBank] ([Ref_Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Question_Answer_OptionType] CHECK CONSTRAINT [FK_Question_Answer_OptionType_QuestionBank]
GO
ALTER TABLE [dbo].[Question_Answer_TextType]  WITH CHECK ADD  CONSTRAINT [FK_Question_Answer_TextType_QuestionBank] FOREIGN KEY([Ref_FK])
REFERENCES [dbo].[QuestionBank] ([Ref_Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Question_Answer_TextType] CHECK CONSTRAINT [FK_Question_Answer_TextType_QuestionBank]
GO
ALTER TABLE [dbo].[QuestionBank]  WITH CHECK ADD  CONSTRAINT [FK_QuestionBank_InputType] FOREIGN KEY([In_Type_FK])
REFERENCES [dbo].[InputType] ([PK])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuestionBank] CHECK CONSTRAINT [FK_QuestionBank_InputType]
GO
ALTER TABLE [dbo].[QuestionBank]  WITH CHECK ADD  CONSTRAINT [FK_QuestionBank_System_List] FOREIGN KEY([System_FK])
REFERENCES [dbo].[System_List] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuestionBank] CHECK CONSTRAINT [FK_QuestionBank_System_List]
GO
ALTER TABLE [dbo].[SAAS_User_Info]  WITH CHECK ADD  CONSTRAINT [FK_SAAS_User_Info_SAAS_module] FOREIGN KEY([Module_number])
REFERENCES [dbo].[SAAS_module] ([PK])
GO
ALTER TABLE [dbo].[SAAS_User_Info] CHECK CONSTRAINT [FK_SAAS_User_Info_SAAS_module]
GO
ALTER TABLE [dbo].[User_Answer_Option]  WITH CHECK ADD  CONSTRAINT [FK_User_Answer_Option_QuestionBank] FOREIGN KEY([ref_code])
REFERENCES [dbo].[QuestionBank] ([Ref_Code])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Answer_Option] CHECK CONSTRAINT [FK_User_Answer_Option_QuestionBank]
GO
ALTER TABLE [dbo].[User_Answer_Text]  WITH CHECK ADD  CONSTRAINT [FK_User_Answer_Text_QuestionBank] FOREIGN KEY([ref_code])
REFERENCES [dbo].[QuestionBank] ([Ref_Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Answer_Text] CHECK CONSTRAINT [FK_User_Answer_Text_QuestionBank]
GO
ALTER TABLE [dbo].[User_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_User_Attachment_QuestionBank] FOREIGN KEY([ref_code])
REFERENCES [dbo].[QuestionBank] ([Ref_Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Attachment] CHECK CONSTRAINT [FK_User_Attachment_QuestionBank]
GO
ALTER TABLE [dbo].[UserAuth]  WITH CHECK ADD  CONSTRAINT [FK_UserAuth_UserAuth] FOREIGN KEY([ID])
REFERENCES [dbo].[UserAuth] ([ID])
GO
ALTER TABLE [dbo].[UserAuth] CHECK CONSTRAINT [FK_UserAuth_UserAuth]
GO
/****** Object:  StoredProcedure [dbo].[AddQuestionAnswerForOption]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddQuestionAnswerForOption]
	@Ref_Cod [int],
	@Answer_Option [nvarchar](max)
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
 
  -- Insert statements for procedure here
  INSERT INTO Question_Answer_OptionType(Ref_FK,Ans_Option) VALUES (@Ref_Cod,@Answer_Option)
END
GO
/****** Object:  StoredProcedure [dbo].[AddQuestionAnswerForText]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddQuestionAnswerForText]
	@Ref_Cod [int],
	@answer [nvarchar](max),
	@FieldNum [int]
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
 
  -- Insert statements for procedure here
  INSERT INTO Question_Answer_TextType(Ref_FK,Ans_Default,Field_Type) VALUES (@Ref_Cod,@answer,@FieldNum)
END
GO
/****** Object:  StoredProcedure [dbo].[AddQuestionAnswerOption]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddQuestionAnswerOption]
	@Ref_Code [int],
	@Answer_Option [nvarchar](max)
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
 
  -- Insert statements for procedure here
  INSERT INTO Question_Answer_OptionType(Ref_FK,Ans_Option) VALUES (@Ref_Code,@Answer_Option)
END
GO
/****** Object:  StoredProcedure [dbo].[AddQuestionInfo]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddQuestionInfo]
	@ques [nvarchar](max),
	@Module [int],
	@TOI [int],
	@REF_ID [int] OUTPUT
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
 
  -- Insert statements for procedure here
  INSERT INTO QuestionInfo(Module_FK,Ques,In_Type_FK) VALUES (@Module,@ques,@TOI)
  SET @REF_ID = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[AddQuestionProcedure]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddQuestionProcedure]
	@ques [nvarchar](max),
	@System [int],
	@Module [int],
	@TOI [int],
	@Seq [int]
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
 
  -- Insert statements for procedure here
  INSERT INTO QuestionBank(System_FK,Module_FK,Ques,In_Type_FK,Seq_Number) VALUES (@System,@Module,@ques,@TOI,@Seq)
  SELECT SCOPE_IDENTITY()
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteQuestionAnswerOption]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteQuestionAnswerOption]
	@Ref_Code [int]
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
 
  -- Delete all answer from table
  DELETE FROM Question_Answer_OptionType WHERE Ref_FK = @Ref_Code
END
GO
/****** Object:  StoredProcedure [dbo].[ExtractQuestionData]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ExtractQuestionData]
	@Ref_Cod [int]
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
  -- Insert statements for procedure here
 SELECT System_FK, Module_FK, Ques, In_Type_FK, Seq_Number from QuestionBank where Ref_Code = @Ref_Cod;
END
GO
/****** Object:  StoredProcedure [dbo].[ExtractQuestionInfoData]    Script Date: 1/20/2017 5:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ExtractQuestionInfoData]
	@Ref_Cod [int],
	@Module [int],
	@Ques [nvarchar](max),
	@Point_Refcode [int]
WITH EXECUTE AS CALLER
AS
BEGIN
  SET NOCOUNT ON;
  -- Insert statements for procedure here
 SELECT Module_FK, Ques, In_Type_FK from QuestionInfo where Ref_Code = @Ref_Cod;
END
GO
ALTER DATABASE [VSQ] SET  READ_WRITE 
GO
