USE [Database]
GO

/****** Object:  Table [dbo].[Telephones]    Script Date: 06.07.2016 23:51:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Telephones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[PhoneNumber] [char](12) NOT NULL,
 CONSTRAINT [PK_Telephones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Telephones]  WITH CHECK ADD  CONSTRAINT [FK_Telephones_Contacts1] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacts] ([Id])
GO

ALTER TABLE [dbo].[Telephones] CHECK CONSTRAINT [FK_Telephones_Contacts1]
GO


