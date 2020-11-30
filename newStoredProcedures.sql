USE [TropicalServer]
GO
/****** Object:  StoredProcedure [dbo].[spGetOrdersByTimePeriod]    Script Date: 10/5/2020 10:48:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetOrdersByTimePeriod]
	@OrderDate varchar(50) = 'Today'
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT o.OrderTrackingNumber,
				    o.OrderDate, 
				    o.OrderCustomerNumber,
				    c.CustName,
				    c.CustOfficeAddress1,
				    o.OrderRouteNumber,
					c.CustID,
					o.OrderID
	FROM tblOrder o 
	JOIN tblCustomer c ON o.OrderCustomerNumber = c.CustNumber
	WHERE 1 = (
			    CASE
			    WHEN @OrderDate = 'Today'		  AND DATEDIFF(day, o.OrderDate, '02-28-2012')    = 0 THEN 1
			    WHEN @OrderDate = 'Last 7 Days'	  AND DATEDIFF(day, o.OrderDate, '02-28-2012')   <= 7 THEN 1
			    WHEN @OrderDate = 'Last 1 Month'  AND DATEDIFF(month, o.OrderDate, '02-28-2012') <= 1 THEN 1
			    WHEN @OrderDate = 'Last 6 Months' AND DATEDIFF(month, o.OrderDate, '02-28-2012') <= 6 THEN 1
			    ELSE 0 
			    END
			  )
	ORDER BY o.OrderDate DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_updateOrdersTable]    Script Date: 10/5/2020 10:48:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_updateOrdersTable]
	@OrderID int,
	@OrderTrackingNumber varchar(50),
	@OrderCustomerNumber int,
	@OrderRouteNumber int
AS
BEGIN
	UPDATE tblOrder
	SET 
		OrderTrackingNumber = @OrderTrackingNumber,
		OrderCustomerNumber = @OrderCustomerNumber,
		OrderRouteNumber = @OrderRouteNumber
		WHERE OrderID = @OrderID
END



GO
/****** Object:  StoredProcedure [dbo].[sp_updateCustomerTable]    Script Date: 10/5/2020 10:48:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_updateCustomerTable]
	@CustID int,
	@CustName varchar(100),
	@CustAddress varchar(100)
AS
BEGIN
	UPDATE tblCustomer
	SET 
		CustName = @CustName,
		CustBillingAddress1 = @CustAddress
		WHERE CustID = @CustID
END



GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteOrder]    Script Date: 10/5/2020 10:48:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_DeleteOrder]
	@OrderID int
AS
BEGIN
	DELETE FROM tblOrder
	WHERE OrderID = @OrderID
END
