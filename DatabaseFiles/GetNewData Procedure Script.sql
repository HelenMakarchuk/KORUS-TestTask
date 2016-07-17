USE [Database]
GO

/****** Object:  StoredProcedure [dbo].[GetNewData]    Script Date: 07.07.2016 9:37:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNewData](@lastId int)
AS
BEGIN
	SELECT c.Id, c.FullName, c.Address, t.PhoneNumber 
	FROM Contacts as c 
	LEFT JOIN Telephones as t 
	ON t.ContactId = c.Id
	WHERE c.Id > @lastId
END

GO


