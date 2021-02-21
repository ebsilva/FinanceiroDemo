Alter Table ASPNETROLES
ADD
 ConcurrencyStamp varchar(255) null,               
 NormalizedName varchar(255) null

 --Drop Table AspNetUserTokens

 CREATE TABLE [AspNetUserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (450) NOT NULL,
    [Name]          NVARCHAR (450) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens]
 PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
)

-- alter table  AspNetUsers  active int not null default 1
Alter Table AspNetUsers
 Add
 ConcurrencyStamp varchar(255) null,
 LockoutEnd DateTime null,
 NormalizedEmail varchar(255) null,
 NormalizedUserName varchar(255) null

--Drop Table [AspNetRoleClaims]

CREATE TABLE [AspNetRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [RoleId]     NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims]
 PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
 FOREIGN KEY ([RoleId])
  REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
)


GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId]
    ON [AspNetRoleClaims]([RoleId] ASC)

Alter Table AspNetUserLogins
   Add  ProviderDisplayName varchar(255) null