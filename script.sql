USE [master]
GO 

CREATE DATABASE DigitalCurrency
Go

USE DigitalCurrency
Go

CREATE TABLE [dbo].[Wallets](
	Id int IDENTITY(1, 1) primary key,
	Holder varchar(50) NOT NULL,
	Balance decimal(18, 0) NOT NULL
);
GO

INSERT Wallets ( Holder, Balance) VALUES 
         ( 'Ahmad', 10000 ),
		 ( 'Reem', 5000 ); 
Go

CREATE PROCEDURE AddWallet
@Holder varchar(50), 
@Balance decimal(18,0)
AS
BEGIN
  INSERT INTO WALLETS ( Holder, Balance) VALUES (@Holder, @Balance)
END
GO

CREATE PROCEDURE [dbo].[GetAllWallets]
AS
BEGIN
    SELECT * FROM Wallets
END
GO
