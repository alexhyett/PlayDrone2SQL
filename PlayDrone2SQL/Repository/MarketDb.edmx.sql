
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/16/2015 23:13:14
-- Generated from EDMX file: D:\Dropbox\Alex\Projects\PlayDrone2SQL\PlayDrone2SQL\Repository\MarketDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PlayMarket];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AppCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_AppCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Apps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Apps'
CREATE TABLE [dbo].[Apps] (
    [Id] uniqueidentifier  NOT NULL,
    [AppId] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [DeveloperName] nvarchar(max)  NOT NULL,
    [Category] uniqueidentifier  NOT NULL,
    [Free] bit  NOT NULL,
    [VersionCode] bigint  NOT NULL,
    [VersionString] nvarchar(max)  NOT NULL,
    [InstallationSize] bigint  NOT NULL,
    [Downloads] bigint  NOT NULL,
    [StarRating] float  NOT NULL,
    [SnapshotDate] datetime  NOT NULL,
    [MetadataUrl] nvarchar(max)  NOT NULL,
    [ApkUrl] nvarchar(max)  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [App_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Apps'
ALTER TABLE [dbo].[Apps]
ADD CONSTRAINT [PK_Apps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [App_Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_AppCategory]
    FOREIGN KEY ([App_Id])
    REFERENCES [dbo].[Apps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppCategory'
CREATE INDEX [IX_FK_AppCategory]
ON [dbo].[Categories]
    ([App_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------