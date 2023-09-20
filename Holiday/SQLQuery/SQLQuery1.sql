USE master

IF DB_ID ('BirthdayDatabase') IS NOT NULL
BEGIN
      ALTER DATABASE BirthdayDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	  DROP DATABASE BirthdayDatabase
END


CREATE DATABASE BirthdayDatabase

USE BirthdayDatabase


CREATE TABLE Birthday
(
  navn NVARCHAR (100) NOT NULL,
  alder INT NOT NULL,
  birthdaydato DATE NOT NULL,
  køn NVARCHAR (10) NOT NULL
);


INSERT INTO Birthday
VALUES ('Egon Olsen', '18', '07-03-2005', 'Mand');
INSERT INTO Birthday
VALUES ('Jean Paul', '27', '12-11-1999', 'Mand');

SELECT * FROM Birthday
