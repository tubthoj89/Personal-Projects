use master 
go

if exists(select * from sys.databases where name='Calculator')
drop database Calculator
go

create database Calculator
go

use Calculator
go

--Table
IF EXISTS(SELECT * FROM sys.tables WHERE name='Calculations')
	DROP TABLE Calculations
GO

CREATE TABLE Calculations (
	CalculationId int identity(1,1) primary key not null,
	MathCal nvarchar(300) not null
)
GO