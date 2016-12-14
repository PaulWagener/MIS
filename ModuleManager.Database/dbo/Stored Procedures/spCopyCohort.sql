CREATE PROCEDURE [dbo].[sp_CopyCohort]
	@schooljaarFrom INT,
	@schooljaarTo INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM [Schooljaar] WHERE [JaarId] = @schooljaarTo)
	BEGIN
		INSERT INTO [Schooljaar] VALUES (@schooljaarTo)
	END

	INSERT INTO [Module]
		   ([CursusCode]
           ,[Schooljaar]
           ,[Beschrijving]
           ,[Naam]
           ,[VerantwoordelijkeDocentId]
           ,[Status]
           ,[Gecontroleerd]
           ,[OnderdeelCode]
           ,[Icon]
           ,[Blok]
           ,[ReadOnly])
	SELECT
			[CursusCode]
           ,@schooljaarTo
           ,[Beschrijving]
           ,[Naam]
           ,[VerantwoordelijkeDocentId]
           ,[Status]
           ,[Gecontroleerd]
           ,[OnderdeelCode]
           ,[Icon]
           ,[Blok]
           ,[ReadOnly]
	FROM [Module]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [FaseModule] ([FaseNaam], [ModuleCursusCode], [ModuleSchooljaar])
	SELECT [FaseNaam], [ModuleCursusCode], @schooljaarTo
	FROM [FaseModule]
	WHERE [ModuleSchooljaar] = @schooljaarFrom;

	INSERT INTO [StudiePunt] ([CursusCode], [Schooljaar], [ToetsCode], [Toetsvorm], [EC], [Minimum])
	SELECT [CursusCode], @schooljaarTo, [ToetsCode], [Toetsvorm], [EC], [Minimum]
	FROM [StudiePunt]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [Leermiddel] ([CursusCode], [Schooljaar], [Beschrijving])
	SELECT [CursusCode], @schooljaarTo, [Beschrijving]
	FROM [Leermiddel]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [ModuleCompetentie] ([CursusCode], [Schooljaar], [CompetentieCode], [Niveau])
	SELECT [CursusCode], @schooljaarTo, [CompetentieCode], [Niveau]
	FROM [ModuleCompetentie]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [ModuleWerkvorm] ([CursusCode], [Schooljaar], [WerkvormType], [Organisatie])
	SELECT [CursusCode], @schooljaarTo, [WerkvormType], [Organisatie]
	FROM [ModuleWerkvorm]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [StudieBelasting] ([CursusCode], [Schooljaar], [Activiteit], [ContactUren], [Duur], [Frequentie], [SBU])
	SELECT [CursusCode], @schooljaarTo, [Activiteit], [ContactUren], [Duur], [Frequentie], [SBU]
	FROM [StudieBelasting]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [Weekplanning] ([CursusCode], [Schooljaar], [Week], [Onderwerp])
	SELECT [CursusCode], @schooljaarTo, [Week], [Onderwerp]
	FROM [Weekplanning]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [Leerdoel] ([CursusCode], [Schooljaar], [Beschrijving])
	SELECT [CursusCode], @schooljaarTo, [Beschrijving]
	FROM [Leerdoel]
	WHERE [Schooljaar] = @schooljaarFrom;

	INSERT INTO [Voorkennis] ([Voorkennis_CursusCode], [Voorkennis_Schooljaar], [Vervolg_CursusCode], [Vervolg_Schooljaar])
	SELECT [Voorkennis_CursusCode], @schooljaarTo, [Vervolg_CursusCode], [Vervolg_Schooljaar]
	FROM [Voorkennis]
	WHERE [Voorkennis_Schooljaar] = @schooljaarFrom;

	INSERT INTO [ModuleLeerlijn] ([Leerlijn_Naam], [Module_CursusCode], [Module_Schooljaar])
	SELECT [Leerlijn_Naam], [Module_CursusCode], @schooljaarTo
	FROM [ModuleLeerlijn]
	WHERE [Module_Schooljaar] = @schooljaarFrom;

	INSERT INTO [ModuleDocent] ([Docenten_Id], [Module_CursusCode], [Module_Schooljaar])
	SELECT [Docenten_Id], [Module_CursusCode], @schooljaarTo
	FROM [ModuleDocent]
	WHERE [Module_Schooljaar] = @schooljaarFrom;
END
GO
