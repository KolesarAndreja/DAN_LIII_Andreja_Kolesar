IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'dbHotel')
CREATE DATABASE dbHotel;
GO
USE dbHotel
--dropping tables
IF OBJECT_ID('vwAbsence') IS NOT NULL
DROP VIEW vwAbsence;

IF OBJECT_ID('vwStaff') IS NOT NULL
DROP VIEW vwStaff;

IF OBJECT_ID('tblAbsence') IS NOT NULL 
DROP TABLE tblAbsence;

IF OBJECT_ID('tblStaff') IS NOT NULL 
DROP TABLE tblStaff;

IF OBJECT_ID('tblManager') IS NOT NULL 
DROP TABLE tblManager;


IF OBJECT_ID('tblStatus') IS NOT NULL 
DROP TABLE tblStatus;

IF OBJECT_ID('tblUser') IS NOT NULL 
DROP TABLE tblUser;

IF OBJECT_ID('tblGender') IS NOT NULL 
DROP TABLE tblGender;

IF OBJECT_ID('tblProfessionalQualifications') IS NOT NULL 
DROP TABLE tblProfessionalQualifications;

IF OBJECT_ID('tblEngagement') IS NOT NULL 
DROP TABLE tblEngagement;


CREATE TABLE tblUser(
	userId INT PRIMARY KEY IDENTITY(1,1),
	fullname VARCHAR(30) NOT NULL,
	dateOfBirth DATE NOT NULL,
	email VARCHAR(30),
	username VARCHAR(30) not null,
	password VARCHAR(30) not null
	);
CREATE TABLE tblGender(
	id INT PRIMARY KEY IDENTITY(1,1),
	name CHAR(1) NOT NULL
);

CREATE TABLE tblProfessionalQualifications(
	id INT PRIMARY KEY IDENTITY(1,1),
	name CHAR(3) not null
);

CREATE TABLE tblEngagement (
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(20) not null
);

CREATE TABLE tblManager (
	managerId INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(20) not null,
	floorNumber INT not null,
	workExperience INT,
	qualificationId INT FOREIGN KEY REFERENCES  tblProfessionalQualifications(id),
	userId INT FOREIGN KEY REFERENCES  tblUser(userId)
);

CREATE TABLE tblStaff (
	staffId INT PRIMARY KEY IDENTITY(1,1),
	citizenship VARCHAR(30),
	salary NUMERIC,
	floorNumber INT not null,
	engegamentId INT FOREIGN KEY REFERENCES tblEngagement(id) ,
	genderId INT FOREIGN KEY REFERENCES  tblGender(id) ,
	userId INT FOREIGN KEY REFERENCES  tblUser(userId)
);

CREATE TABLE tblStatus (
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(20) not null

);
CREATE TABLE tblAbsence (
	absenceId INT PRIMARY KEY IDENTITY(1,1),
	firstDay DATE not null,
	lastDay DATE not null,
	reason VARCHAR(50),
	statusId INT FOREIGN KEY REFERENCES tblStatus(id),
	staffId INT FOREIGN KEY REFERENCES  tblStaff(staffId)
);

GO
CREATE VIEW vwAbsence
as
select u.fullname, u.username, s.floorNumber, e.name AS engagement, a.firstDay, a.lastDay, a.reason, st.name AS status
from tblStaff s
inner join tblAbsence a
on a.staffId = s.staffId
inner join tblUser u
on s.userId = u.userId
inner join tblEngagement e
on s.engegamentId = e.id
inner join tblStatus st
on st.id = a.statusId

GO
CREATE VIEW vwStaff
as
select u.fullname, u.username, u.dateOfBirth, u.email, u.password, s.floorNumber, e.name AS engagement, s.salary, g.name
from tblStaff s
inner join tblUser u
on s.userId = u.userId
inner join tblEngagement e
on s.engegamentId = e.id
inner join tblGender g
on g.id = s.genderId




GO
INSERT INTO tblGender(name)
VALUES ('M'), ('F'),('N'),('X');

INSERT INTO tblStatus(name)
VALUES('accepted'),('rejected'),('deleted'),('pending');


INSERT INTO tblProfessionalQualifications(name)
VALUES('I'),('II'),('III'),('IV'),('V'),('VI'),('VII');

INSERT INTO tblEngagement(name)
VALUES('cleaning'),('cooking'),('supervising'),('reporting');
	
