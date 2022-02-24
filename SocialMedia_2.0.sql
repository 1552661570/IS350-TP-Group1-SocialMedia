USE master;
GO

IF DB_ID('SocialMedia') IS NOT NULL 
BEGIN
USE master;
ALTER DATABASE SocialMedia SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE SocialMedia;
PRINT N'Drop Database Success'
END
ELSE
PRINT N'Database is not exist'

IF @@ERROR = 3702 
   RAISERROR('Database cannot be dropped because there are still open connections.', 127, 127) WITH NOWAIT, LOG;

-- Create database
CREATE DATABASE SocialMedia;
GO

USE SocialMedia;
GO


CREATE TABLE [user] (

	userID INT NOT NULL IDENTITY,
	userName NVARCHAR(20) NOT NULL UNIQUE,
	userPassword NVARCHAR(16) NOT NULL,
	userMail NVARCHAR(50) NULL,
	userNumber  NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_UserID PRIMARY KEY(userID)
);


CREATE TABLE [post] (
	postID INT NOT NULL IDENTITY,
	userName  NVARCHAR(20) NOT NULL,
	content NVARCHAR(255) NOT NULL,
	sendDate DATETIME NOT NULL,
	picturePath NVARCHAR(100) NOT NULL,
	thumbUpNum INT NOT NULL,
	CONSTRAINT PK_Postid PRIMARY KEY(postID)

);

PRINT N'Database Created'

--INSERT INTO [user] VALUES(N'Jack', N'1234', N'1example@gmail.com', '1022021234');
--INSERT INTO [user] VALUES(N'Tom', N'1234', N'2example@gmail.com', '2022021234');
--INSERT INTO [user] VALUES(N'Candy', N'1234', N'3example@gmail.com', '3022021234');


--INSERT INTO [post] VALUES('Jack','Today is Tuesday','20220201 09:00:00.000','E:\picture',3);
--INSERT INTO [post] VALUES('Jack','Today is Wednesday','20220202 09:00:00.000','E:\picture',5);
--INSERT INTO [post] VALUES('Jack','Today is Thursday','20220203 09:00:00.000','E:\picture',11);



--SELECT * FROM [user]
--SELECT * FROM [post]


