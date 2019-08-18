CREATE TABLE [dbo].[Flights](
[FlightId] [int] IDENTITY(1,1) NOT NULL,
[Origin] [nvarchar](max) NULL,
[Destination] [nvarchar](max) NULL,
[FlightNumber] [nvarchar](max) NULL,
[DepartureTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Flights] PRIMARY KEY CLUSTERED 
(
[FlightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Travelers](
[TravelerId] [int] IDENTITY(1,1) NOT NULL,
[FirstName] [nvarchar](max) NULL,
[LastName] [nvarchar](max) NULL,
[MobilePhone] [nvarchar](max) NULL,
[Passport] [nvarchar](max) NULL,
[Email] [nvarchar](max) NULL,
[FlightId] [int] NULL,
 CONSTRAINT [PK_Travelers] PRIMARY KEY CLUSTERED 
(
[TravelerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[Travelers]  WITH CHECK ADD  CONSTRAINT [FK_Travelers_Flights_FlightId] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([FlightId])

ALTER TABLE [dbo].[Travelers] CHECK CONSTRAINT [FK_Travelers_Flights_FlightId]


INSERT [Flights] ([Origin], [Destination], [FlightNumber],[DepartureTime]) VALUES('New York', 'Paris','204837D','10/10/2018')
INSERT [Flights] ([Origin], [Destination], [FlightNumber],[DepartureTime]) VALUES('Paris', 'Rome','204837C','10/10/2017')

