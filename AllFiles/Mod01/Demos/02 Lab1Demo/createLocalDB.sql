CREATE DATABASE [FlightsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FlightsDB', FILENAME = N'H:\_Databases\FlightsDB.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FlightsDB_log', FILENAME = N'H:\_Databases\FlightsDB_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )

GO
USE [FlightsDB]
GO

CREATE TABLE [dbo].[Flights](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Origin] [varchar](50) NOT NULL,
[Destination] [varchar](50) NOT NULL,
[FlightNumber] [varchar](50) NOT NULL,
[DepartureTime] [date] NOT NULL,

PRIMARY KEY CLUSTERED 
(
    [Id] ASC
))