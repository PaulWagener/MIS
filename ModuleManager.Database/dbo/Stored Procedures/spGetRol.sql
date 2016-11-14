CREATE PROCEDURE [dbo].[spGetRol]
	@UserName varchar(100)
AS
Begin
	Declare @ReturnRol varchar(10)

	Begin
		Select @ReturnRol = SysteemRol From [User] Where UserNaam = @UserName
	End
	
	Select @ReturnRol
End
RETURN 0
