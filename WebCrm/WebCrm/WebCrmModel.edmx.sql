
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/03/2018 14:01:32
-- Generated from EDMX file: C:\Users\amel_\Documents\GitHub\WebCrm\WebCrm\WebCrm\WebCrmModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WebCrm];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CompanyNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NoteSet] DROP CONSTRAINT [FK_CompanyNote];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet] DROP CONSTRAINT [FK_CompanyPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskSet] DROP CONSTRAINT [FK_CompanyTask];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskSet] DROP CONSTRAINT [FK_PersonTask];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NoteSet] DROP CONSTRAINT [FK_PersonNote];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CompanySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanySet];
GO
IF OBJECT_ID(N'[dbo].[NoteSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NoteSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[TaskSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CompanySet'
CREATE TABLE [dbo].[CompanySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Zip] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [CreateUser] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NoteSet'
CREATE TABLE [dbo].[NoteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreateUser] nvarchar(max)  NOT NULL,
    [Company_Id] int  NOT NULL,
    [Person_Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Forename] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [CreateUser] nvarchar(max)  NOT NULL,
    [Company_Id] int  NOT NULL
);
GO

-- Creating table 'TaskSet'
CREATE TABLE [dbo].[TaskSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [CreateUser] nvarchar(max)  NOT NULL,
    [Company_Id] int  NOT NULL,
    [Person_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CompanySet'
ALTER TABLE [dbo].[CompanySet]
ADD CONSTRAINT [PK_CompanySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NoteSet'
ALTER TABLE [dbo].[NoteSet]
ADD CONSTRAINT [PK_NoteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TaskSet'
ALTER TABLE [dbo].[TaskSet]
ADD CONSTRAINT [PK_TaskSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Company_Id] in table 'NoteSet'
ALTER TABLE [dbo].[NoteSet]
ADD CONSTRAINT [FK_CompanyNote]
    FOREIGN KEY ([Company_Id])
    REFERENCES [dbo].[CompanySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyNote'
CREATE INDEX [IX_FK_CompanyNote]
ON [dbo].[NoteSet]
    ([Company_Id]);
GO

-- Creating foreign key on [Company_Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [FK_CompanyPerson]
    FOREIGN KEY ([Company_Id])
    REFERENCES [dbo].[CompanySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyPerson'
CREATE INDEX [IX_FK_CompanyPerson]
ON [dbo].[PersonSet]
    ([Company_Id]);
GO

-- Creating foreign key on [Company_Id] in table 'TaskSet'
ALTER TABLE [dbo].[TaskSet]
ADD CONSTRAINT [FK_CompanyTask]
    FOREIGN KEY ([Company_Id])
    REFERENCES [dbo].[CompanySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyTask'
CREATE INDEX [IX_FK_CompanyTask]
ON [dbo].[TaskSet]
    ([Company_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'TaskSet'
ALTER TABLE [dbo].[TaskSet]
ADD CONSTRAINT [FK_PersonTask]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonTask'
CREATE INDEX [IX_FK_PersonTask]
ON [dbo].[TaskSet]
    ([Person_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'NoteSet'
ALTER TABLE [dbo].[NoteSet]
ADD CONSTRAINT [FK_PersonNote]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonNote'
CREATE INDEX [IX_FK_PersonNote]
ON [dbo].[NoteSet]
    ([Person_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------