USE [ModuleManager.Domain]
GO
/****** Object:  StoredProcedure [dbo].[spAuthenticateUser]    Script Date: 12/16/2015 10:45:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spAuthenticateUser]
@UserName varchar(50),
@Password varchar(50)

as
Begin
 Declare @Count int
 Declare @Blocked bit

 Select @Count = COUNT(UserNaam) From [User]
 Where [UserNaam] = @UserName

 Select @Blocked = Blocked From [User]
 Where [UserNaam] = @UserName

 if(@Blocked = 1)
 Begin
	Select -2 as ReturnCode
 End
 else if(@Count = 1)
 Begin
  Select 1 as ReturnCode
 End
 Else
 Begin
  Select -1 as ReturnCode
 End
End

GO
/****** Object:  StoredProcedure [dbo].[spEditUser]    Script Date: 12/16/2015 10:45:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spEditUser]
@OldUserNaam varchar(50),
@NewUserNaam varchar(50),
@SysteemRol varchar(50),
@email varchar(50),
@naam varchar(50),
@Blocked bit

as
Begin
 Declare @Count int
 Declare @ReturnCode int

 Select @Count = COUNT(UserNaam)
 FROM [User] WHERE UserNaam = @NewUserNaam AND @NewUserNaam != @OldUserNaam 
 if @Count > 0
 Begin
  Set @ReturnCode = -1
 End
 Else
 Begin
  Set @ReturnCode = 1
  UPDATE [User]
	SET Usernaam= @NewUserNaam, SysteemRol = @SysteemRol, email = @email, naam = @naam, Blocked = @Blocked
	WHERE UserNaam=@OldUserNaam; 
 End
 Select @ReturnCode as ReturnValue
End

GO
/****** Object:  StoredProcedure [dbo].[spGetRol]    Script Date: 12/16/2015 10:45:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spGetRol]
@UserName varchar(100)
as
Begin
 
 Declare @ReturnRol varchar(10)

 Begin
  Select @ReturnRol = SysteemRol From [User] Where UserNaam = @UserName
 End
 Select @ReturnRol
End

GO
/****** Object:  StoredProcedure [dbo].[spRegisterUser]    Script Date: 12/16/2015 10:45:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spRegisterUser]
@UserNaam varchar(50),
@SysteemRol varchar(50),
@email varchar(50),
@naam varchar(50)
as
Begin
 Declare @Count int
 Declare @ReturnCode int

 Select @Count = COUNT(UserNaam)
 FROM [User] WHERE UserNaam = @UserNaam
 if @Count > 0
 Begin
  Set @ReturnCode = -1
 End
 Else
 Begin
  Set @ReturnCode = 1
  Insert into [User] values
  (@UserNaam, @SysteemRol, 0, @email, @naam)
 End
 select @ReturnCode
End

GO
