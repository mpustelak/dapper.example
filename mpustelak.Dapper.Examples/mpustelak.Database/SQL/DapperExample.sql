--
-- Change database name here
--
USE YOUR_DATABASE_GOES_HERE

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--
-- Create User table
--
CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--
-- Create UserDetail table
--
CREATE TABLE [dbo].[UserDetail](
	[UserId] [INT] NOT NULL,
	[FirstName] [NVARCHAR](50) NOT NULL,
	[MiddleName] [NVARCHAR](50) NULL,
	[LastName] [NVARCHAR](50) NOT NULL,
	[Created] [DATETIME] NOT NULL,
 CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserDetail_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO

ALTER TABLE [dbo].[UserDetail] CHECK CONSTRAINT [FK_UserDetail_User]
GO

--
-- Feed database with users
--
INSERT INTO dbo.[User] ( UserId, UserName ) VALUES  ( 1, N'mpustelak')
INSERT INTO dbo.[User] ( UserId, UserName ) VALUES  ( 2, N'testuser')
INSERT INTO dbo.[User] ( UserId, UserName ) VALUES  ( 3, N'usertest')

--
-- Feed database with user details
--
INSERT INTO dbo.UserDetail ( UserId, FirstName, MiddleName, LastName, Created) VALUES  (1, N'Mat', null, N'Pustelak', '2014-01-01 10:10:00')
INSERT INTO dbo.UserDetail ( UserId, FirstName, MiddleName, LastName, Created) VALUES  (2, N'Johnny', N'', N'English', '2015-05-07 15:12:00')
INSERT INTO dbo.UserDetail ( UserId, FirstName, MiddleName, LastName, Created) VALUES  (3, N'James', N'Killer', N'B', '2016-10-15 18:45:00')