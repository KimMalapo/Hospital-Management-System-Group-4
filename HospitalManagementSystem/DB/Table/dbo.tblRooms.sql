CREATE TABLE dbo.tblRooms
(
    RoomNo    NVARCHAR(20)  NOT NULL,                 -- e.g. '101', 'ICU-A'
    RoomType  NVARCHAR(50)  NOT NULL,                 -- logical link to tblRoomTypes.Code
    Rate      DECIMAL(10,2) NOT NULL CONSTRAINT DF_tblRooms_Rate DEFAULT (0),
    Status    NVARCHAR(30)  NOT NULL CONSTRAINT DF_tblRooms_Status DEFAULT (N'Available'),
    IsActive  BIT           NOT NULL CONSTRAINT DF_tblRooms_IsActive DEFAULT (1),
    CreatedAt DATETIME2(0)  NOT NULL CONSTRAINT DF_tblRooms_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt DATETIME2(0)  NULL,
    CONSTRAINT PK_tblRooms PRIMARY KEY CLUSTERED (RoomNo),
    CONSTRAINT CK_tblRooms_Rate CHECK (Rate >= 0),
    CONSTRAINT CK_tblRooms_Status CHECK (Status IN (N'Available', N'Occupied', N'Maintenance', N'Reserved'))
);
GO

-- Index for the logical FK (RoomType stores the Code value)
CREATE NONCLUSTERED INDEX IX_tblRooms_RoomType
    ON dbo.tblRooms (RoomType)
    WHERE IsActive = 1;
