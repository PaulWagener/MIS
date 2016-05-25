USE DomainDal


ALTER TABLE Tag
DROP COLUMN Schooljaar;

AlTER TABLE ModuleTag
DROP COLUMN Tag_Schooljaar;