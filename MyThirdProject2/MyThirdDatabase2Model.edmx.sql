
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/13/2016 22:06:43
-- Generated from EDMX file: C:\See Sharp\MyThirdProject2\MyThirdProject2\MyThirdDatabase2Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ProjectThirdDatabase2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Platforms'
CREATE TABLE [dbo].[Platforms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Device] nvarchar(100)  NOT NULL,
    [OS] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Publisher] nvarchar(100)  NOT NULL,
    [Release_Year] int  NOT NULL
);
GO

-- Creating table 'Scores'
CREATE TABLE [dbo].[Scores] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Count] int  NOT NULL,
    [Time] int  NOT NULL,
    [GameId] int  NOT NULL,
    [PlayerId] int  NOT NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nickname] nvarchar(24)  NOT NULL,
    [PlayerUser_Information_Player_Id] int  NOT NULL
);
GO

-- Creating table 'User_Information'
CREATE TABLE [dbo].[User_Information] (
    [Country] nvarchar(100)  NOT NULL,
    [First_Name] nvarchar(100)  NOT NULL,
    [Last_Name] nvarchar(100)  NOT NULL,
    [Year] smallint  NOT NULL,
    [Month] smallint  NOT NULL,
    [Day] smallint  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'PlatformGame'
CREATE TABLE [dbo].[PlatformGame] (
    [Platforms_Id] int  NOT NULL,
    [Games_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Platforms'
ALTER TABLE [dbo].[Platforms]
ADD CONSTRAINT [PK_Platforms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [PK_Scores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User_Information'
ALTER TABLE [dbo].[User_Information]
ADD CONSTRAINT [PK_User_Information]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Platforms_Id], [Games_Id] in table 'PlatformGame'
ALTER TABLE [dbo].[PlatformGame]
ADD CONSTRAINT [PK_PlatformGame]
    PRIMARY KEY CLUSTERED ([Platforms_Id], [Games_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Platforms_Id] in table 'PlatformGame'
ALTER TABLE [dbo].[PlatformGame]
ADD CONSTRAINT [FK_PlatformGame_Platform]
    FOREIGN KEY ([Platforms_Id])
    REFERENCES [dbo].[Platforms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Games_Id] in table 'PlatformGame'
ALTER TABLE [dbo].[PlatformGame]
ADD CONSTRAINT [FK_PlatformGame_Game]
    FOREIGN KEY ([Games_Id])
    REFERENCES [dbo].[Games]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlatformGame_Game'
CREATE INDEX [IX_FK_PlatformGame_Game]
ON [dbo].[PlatformGame]
    ([Games_Id]);
GO

-- Creating foreign key on [GameId] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [FK_GameScore]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameScore'
CREATE INDEX [IX_FK_GameScore]
ON [dbo].[Scores]
    ([GameId]);
GO

-- Creating foreign key on [PlayerId] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [FK_PlayerScore]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerScore'
CREATE INDEX [IX_FK_PlayerScore]
ON [dbo].[Scores]
    ([PlayerId]);
GO

-- Creating foreign key on [PlayerUser_Information_Player_Id] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK_PlayerUser_Information]
    FOREIGN KEY ([PlayerUser_Information_Player_Id])
    REFERENCES [dbo].[User_Information]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerUser_Information'
CREATE INDEX [IX_FK_PlayerUser_Information]
ON [dbo].[Players]
    ([PlayerUser_Information_Player_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------