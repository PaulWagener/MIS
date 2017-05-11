CREATE PROCEDURE [dbo].[spRegisterUser]
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
		
		Insert into [User] values (@UserNaam, @SysteemRol, 0, @email, @naam, null)
	End

	select @ReturnCode
End
RETURN 0
