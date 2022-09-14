USE [sep12New]
CREATE TABLE [dbo].[AccountInfo](
	[DocumentNumber] [bigint] NOT NULL,	
	[AccountNumber] [varchar](5000) NULL,
	[AccountType] [varchar](5000) NULL,
	[OfficerCode] [varchar](5000) NULL,
	[CustomerName] [varchar](5000) NULL,
	[CustomerTIN] [varchar](5000) NULL,
	[CustomerCIF] [varchar](5000) NULL,
	[AccountCloseDate] [varchar](5000) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AdditionalInfo](
	[DocumentNumber] [bigint] NOT NULL,	
	[FieldName] [varchar](128) NULL,
	[FieldValue] [varchar](5000) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[NoteInfo](
	[DocumentNumber] [bigint] NOT NULL,
	[NoteDate] [varchar](5000) NULL,
	[NoteText] [varchar](5000) NULL,
	[Operator] [varchar](5000) NULL,
	[PageNumber] [varchar](5000) NULL,
	[Priority] [varchar](5000) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SnippetInfo](
	[DocumentNumber] [bigint] NOT NULL,
	[SnippetFileType] [varchar](5000) NULL,
	[SnippetFileName] [varchar](5000) NULL
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[XMLDocumentInfo](
	[DocumentNumber] [bigint] IDENTITY(1,1) NOT NULL,	
	[DocumentType] [varchar](5000) NULL,
	[Date] [varchar](5000) NULL,
	[Description] [varchar](5000) NULL,
	[DocumentFileType] [varchar](5000) NULL,
	[DocumentFileName] [varchar](5000) NULL,
	[Path] [varchar](5000) NULL,
	[FirstPage] [varchar](5000) NULL,
	[LastPage] [varchar](5000) NULL,
	[NumberPages] [varchar](5000) NULL,
	[Security] [varchar](5000) NULL,
	[DocumentSize] [varchar](5000) NULL,
	[Location] [varchar](5000) NULL,
	[ExpDate] [varchar](5000) NULL,
	[ConcatImage] [bit] NULL,
	[DocumentTypeVerified] [bit] NULL,
	[IsDocumentSizeValid] [bit] NULL,
	[DocumentRecordDone] [bit] NULL,
	[ExcludeDocumentExtraction] [bit] NULL,	
	[FileName] [varchar](5000) NULL
PRIMARY KEY CLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO