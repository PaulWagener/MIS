CREATE PROCEDURE [dbo].[spAuthenticateUser]
	@UserName varchar(50),
	@Password varchar(50)
AS
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
RETURN 0
