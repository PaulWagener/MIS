CREATE PROCEDURE [dbo].[spEditUser]
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
RETURN 0
