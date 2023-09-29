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


INSERT INTO Birthday (navn, alder, birthdaydato, køn)
VALUES ('Egon Olsen', '18', '2005-03-07', 'Mand');

INSERT INTO Birthday (navn, alder, birthdaydato, køn)
VALUES ('Jean Paul', '27', '1995-11-12', 'Mand');


SELECT * FROM Birthday
