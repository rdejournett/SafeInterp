 USE [SafeInterpret]
GO
/****** Object:  Table [dbo].[LabLookup]    Script Date: 6/15/2017 4:33:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabLookup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FacilityName] [nvarchar](100) NULL,
	[LabName] [nvarchar](100) NULL,
	[CommonLabName] [nvarchar](100) NULL,
	[LabCode] [nvarchar](100) NULL,
	[ResultName] [nvarchar](100) NULL,
	[CommonResultName] [nvarchar](100) NULL,
	[ResultCode] [nvarchar](100) NULL,
	[ResultValue] [nvarchar](100) NULL,
	[CanSkip] [bit] NULL,
	[IsPositive] [bit] NULL,
 CONSTRAINT [PK_LabLookup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[LabLookup] ON 

GO
INSERT [dbo].[LabLookup] ([ID], [FacilityName], [LabName], [CommonLabName], [LabCode], [ResultName], [CommonResultName], [ResultCode], [ResultValue], [CanSkip], [IsPositive]) VALUES (1, N'Quest', N'CHLAMYDIA/GC RNA,TMA', N'CHLAMYDIA & GONORRHOEAE', N'11363', N'CHLAMYDIA TRACHOMATIS RNA, TMA', N'CHLAMYDIA', N'70043800', N'DETECTED', 0, 1)
GO
INSERT [dbo].[LabLookup] ([ID], [FacilityName], [LabName], [CommonLabName], [LabCode], [ResultName], [CommonResultName], [ResultCode], [ResultValue], [CanSkip], [IsPositive]) VALUES (2, N'Quest', N'CHLAMYDIA/GC RNA,TMA', N'CHLAMYDIA & GONORRHOEAE', N'11363', N'CHLAMYDIA TRACHOMATIS RNA, TMA', N'CHLAMYDIA', N'70043800', N'NOT DETECTED', 0, 0)
GO
INSERT [dbo].[LabLookup] ([ID], [FacilityName], [LabName], [CommonLabName], [LabCode], [ResultName], [CommonResultName], [ResultCode], [ResultValue], [CanSkip], [IsPositive]) VALUES (3, N'Quest', N'CHLAMYDIA/GC RNA,TMA', N'CHLAMYDIA & GONORRHOEAE', N'11363', N'NEISSERIA GONORRHOEAE RNA, TMA', N'GONORRHOEAE', N'70043900', N'DETECTED', 0, 1)
GO
INSERT [dbo].[LabLookup] ([ID], [FacilityName], [LabName], [CommonLabName], [LabCode], [ResultName], [CommonResultName], [ResultCode], [ResultValue], [CanSkip], [IsPositive]) VALUES (4, N'Quest', N'CHLAMYDIA/GC RNA,TMA', N'CHLAMYDIA & GONORRHOEAE', N'11363', N'NEISSERIA GONORRHOEAE RNA, TMA', N'GONORRHOEAE', N'70043900', N'NOT DETECTED', 0, 0)
GO
INSERT [dbo].[LabLookup] ([ID], [FacilityName], [LabName], [CommonLabName], [LabCode], [ResultName], [CommonResultName], [ResultCode], [ResultValue], [CanSkip], [IsPositive]) VALUES (5, N'Quest', N'SPEC ID NOTIFICATION', NULL, N'17134', N'COMMENT:', NULL, N'86006556', NULL, 1, NULL)
GO
INSERT [dbo].[LabLookup] ([ID], [FacilityName], [LabName], [CommonLabName], [LabCode], [ResultName], [CommonResultName], [ResultCode], [ResultValue], [CanSkip], [IsPositive]) VALUES (6, N'Quest', NULL, NULL, N'ClinicalPDFReport1', NULL, NULL, N'ClinicalPDFReport1', NULL, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[LabLookup] OFF
GO
