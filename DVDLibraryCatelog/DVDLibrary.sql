use master 
go

if exists(select * from sys.databases where name='DVDLibrary')
drop database DVDLibrary
go

create database DVDLibrary
go

use DVDLibrary
go

-- Tables
IF EXISTS(SELECT * FROM sys.tables WHERE name='Dvd')
	DROP TABLE Dvd
GO

CREATE TABLE Dvd (
	dvdId int identity(1,1) primary key not null,
	rating varchar(50) not null,
	director varchar(100) not null,
	title varchar(50) not null,
	realeaseYear int not null,
	notes varchar(100) null
)
GO

-- Stored Procedures
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdDelete')
      DROP PROCEDURE DvdDelete
GO

CREATE PROCEDURE DvdDelete(
	@dvdId int
)
AS 
	DELETE FROM Dvd
	WHERE  dvdId = @dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdUpdate')
      DROP PROCEDURE DvdUpdate
GO

Create PROCEDURE DvdUpdate(
	@dvdId int,
	@rating varchar(50),
	@director varchar(100),
	@title varchar(50),
	@realeaseYear int,
	@notes varchar(100)
)
AS 
	UPDATE Dvd
	SET rating = @rating,
	director = @director,
	title = @title,
	realeaseYear = @realeaseYear,
	notes = @notes
	WHERE dvdId = @dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdInsert')
      DROP PROCEDURE DvdInsert
GO

CREATE PROCEDURE DvdInsert (
	@dvdId int output,
	@director varchar(100),
	@rating varchar(50),
	@title Varchar(50),
	@realeaseYear int,
	@notes varchar(100)
)
AS
	INSERT INTO Dvd (director, rating, title, realeaseYear, notes)
	VALUES (@director, @rating, @title, @realeaseYear, @notes)

	SET @dvdId = SCOPE_IDENTITY()
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectById')
      DROP PROCEDURE DvdSelectById
GO

CREATE PROCEDURE DvdSelectById (
	@dvdId int
)
AS
	SELECT *
	FROM Dvd
	WHERE dvdId = @dvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectAll')
      DROP PROCEDURE DvdSelectAll
GO

CREATE PROCEDURE DvdSelectAll
AS
	SELECT *
	FROM Dvd  
	ORDER BY title
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectByTitle')
      DROP PROCEDURE SelectByTitle
GO

CREATE PROCEDURE SelectByTitle (
	@term varchar(50)
)
AS	
	SELECT *
	FROM Dvd
	WHERE title = @term
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectByDirector')
      DROP PROCEDURE SelectByDirector
GO

CREATE PROCEDURE SelectByDirector (
	@term varchar(100)
)
AS	
	SELECT *
	FROM Dvd
	WHERE director = @term
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectByRating')
      DROP PROCEDURE SelectByRating
GO

CREATE PROCEDURE SelectByRating (
	@term varchar(50)
)
AS	
	SELECT *
	FROM Dvd
	WHERE rating = @term
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectByReleaseYear')
      DROP PROCEDURE SelectByReleaseYear
GO

CREATE PROCEDURE SelectByReleaseYear (
	@term int
)
AS	
	SELECT *
	FROM Dvd
	WHERE realeaseYear = @term
GO

--insert data
INSERT INTO Dvd (title, director, rating, realeaseYear, notes)
	VALUES ('A Good Tale', 'Joe Smith', 'PG-13', 2012, 'This really is a great tale!'),
	('The Lion King', 'Jon Favreau', 'G', 1994, 'Great story'),
	('King Kong', 'Peter Jackson', 'PG-13', 2005, 'Great remake of king kong!')

--Logins
USE master
GO

CREATE LOGIN DVDLibrary WITH PASSWORD='Testing123'
GO

USE DVDLibrary
GO

CREATE USER DVDLibrary FOR LOGIN DVDLibrary
GO

GRANT EXECUTE ON DvdDelete TO DVDLibrary
GRANT EXECUTE ON DvdInsert TO DVDLibrary
GRANT EXECUTE ON DvdSelectAll TO DVDLibrary
GRANT EXECUTE ON DvdSelectById TO DVDLibrary
GRANT EXECUTE ON DvdUpdate TO DVDLibrary
GRANT EXECUTE ON SelectByDirector TO DVDLibrary
GRANT EXECUTE ON SelectByRating TO DVDLibrary
GRANT EXECUTE ON SelectByReleaseYear TO DVDLibrary
GRANT EXECUTE ON SelectByTitle TO DVDLibrary
GO

GRANT SELECT ON Dvd TO DVDLibrary
GRANT INSERT ON Dvd TO DVDLibrary
GRANT UPDATE ON Dvd TO DVDLibrary
GRANT DELETE ON Dvd TO DVDLibrary
GO