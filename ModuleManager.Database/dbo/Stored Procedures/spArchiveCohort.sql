CREATE PROCEDURE [dbo].[spArchiveCohort]
	@schooljaar INT
AS
BEGIN
	UPDATE [dbo].[Module]
	SET [ReadOnly] = 1
	WHERE [Schooljaar] = @schooljaar
END
GO