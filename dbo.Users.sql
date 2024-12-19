CREATE TABLE [dbo].[Users] (
    [phoneNumber]   NVARCHAR (450) NOT NULL,
    [password]      NVARCHAR (MAX) NULL,
    [isVerify]      BIT            NULL,
    [otpCode]       NVARCHAR (MAX) NULL,
    [otpExpiration] NVARCHAR (MAX) NULL,
    [createdAt]     DATETIME2 (7)  NULL,
    [updatedAt]     DATETIME2 (7)  NULL,
    [role] NCHAR(10) NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([phoneNumber] ASC)
);

