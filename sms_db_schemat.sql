USE [sms_db]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 16/9/2022 18:20:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Order] [tinyint] NULL,
	[Message] [nvarchar](960) NOT NULL,
	[CountryCode] [smallint] NOT NULL,
	[RequestId] [varchar](100) NOT NULL,
	[SenderEmail] [nvarchar](250) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_Created]  DEFAULT (getdate()) FOR [Created]
GO
