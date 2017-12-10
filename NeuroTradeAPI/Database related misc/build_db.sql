CREATE SCHEMA "public";

CREATE SEQUENCE "public"."Algorithms_AlgorithmId_seq" START WITH 1;

CREATE SEQUENCE "public"."Batches_BatchId_seq" START WITH 1;

CREATE SEQUENCE "public"."Candles_CandleId_seq" START WITH 1;

CREATE SEQUENCE "public"."Instruments_InstrumentId_seq" START WITH 1;

CREATE SEQUENCE "public"."TrainedModels_TrainedModelId_seq" START WITH 1;

CREATE SEQUENCE "public"."Users_UserId_seq" START WITH 1;

CREATE TABLE "public"."Instruments" ( 
	"InstrumentId"       serial  NOT NULL,
	"Alias"              text  ,
	"DownloadAlias"      text  ,
	CONSTRAINT "PK_Instruments" PRIMARY KEY ( "InstrumentId" )
 );

CREATE TABLE "public"."Users" ( 
	"UserId"             serial  NOT NULL,
	"Email"              text  ,
	"Name"               text  ,
	"Password"           text  ,
	CONSTRAINT "PK_Users" PRIMARY KEY ( "UserId" )
 );

CREATE TABLE "public"."Algorithms" ( 
	"AlgorithmId"        serial  NOT NULL,
	"Path"               text  ,
	"UserId"             integer  NOT NULL,
	"Description"        text  ,
	CONSTRAINT "PK_Algorithms" PRIMARY KEY ( "AlgorithmId" )
 );

CREATE INDEX IX_Algorithms_UserId ON "public"."Algorithms" ( "UserId" );

CREATE TABLE "public"."Batches" ( 
	"BatchId"            serial  NOT NULL,
	"BeginTime"          timestamp  NOT NULL,
	"EndTime"            timestamp  ,
	"InstrumentId"       integer  NOT NULL,
	"Interval"           interval  NOT NULL,
	CONSTRAINT "PK_Batches" PRIMARY KEY ( "BatchId" )
 );

CREATE INDEX IX_Batches_InstrumentId ON "public"."Batches" ( "InstrumentId" );

CREATE TABLE "public"."Candles" ( 
	"CandleId"           bigserial  NOT NULL,
	"BatchId"            integer  NOT NULL,
	"BeginTime"          timestamp  ,
	"Close"              real  NOT NULL,
	"High"               real  NOT NULL,
	"Low"                real  NOT NULL,
	"Open"               real  NOT NULL,
	"Volume"             integer  NOT NULL,
	CONSTRAINT "PK_Candles" PRIMARY KEY ( "CandleId" )
 );

CREATE INDEX IX_Candles_BatchId ON "public"."Candles" ( "BatchId" );

CREATE TABLE "public"."TrainedModels" ( 
	"TrainedModelId"     serial  NOT NULL,
	"AlgorithmId"        integer  NOT NULL,
	"Data"               jsonb  ,
	"InstrumentId"       integer  NOT NULL,
	"Parameters"         jsonb  ,
	"Performance"        real  NOT NULL,
	"TestBegin"          timestamp  NOT NULL,
	"TestEnd"            timestamp  NOT NULL,
	"TrainBegin"         timestamp  NOT NULL,
	"TrainEnd"           timestamp  NOT NULL,
	CONSTRAINT "PK_TrainedModels" PRIMARY KEY ( "TrainedModelId" )
 );

CREATE INDEX IX_TrainedModels_AlgorithmId ON "public"."TrainedModels" ( "AlgorithmId" );

CREATE INDEX IX_TrainedModels_InstrumentId ON "public"."TrainedModels" ( "InstrumentId" );

ALTER TABLE "public"."Algorithms" ADD CONSTRAINT "FK_Algorithms_Users_UserId" FOREIGN KEY ( "UserId" ) REFERENCES "public"."Users"( "UserId" ) ON DELETE CASCADE;

ALTER TABLE "public"."Batches" ADD CONSTRAINT "FK_Batches_Instruments_InstrumentId" FOREIGN KEY ( "InstrumentId" ) REFERENCES "public"."Instruments"( "InstrumentId" ) ON DELETE CASCADE;

ALTER TABLE "public"."Candles" ADD CONSTRAINT "FK_Candles_Batches_BatchId" FOREIGN KEY ( "BatchId" ) REFERENCES "public"."Batches"( "BatchId" ) ON DELETE CASCADE;

ALTER TABLE "public"."TrainedModels" ADD CONSTRAINT "FK_TrainedModels_Algorithms_AlgorithmId" FOREIGN KEY ( "AlgorithmId" ) REFERENCES "public"."Algorithms"( "AlgorithmId" ) ON DELETE CASCADE;

ALTER TABLE "public"."TrainedModels" ADD CONSTRAINT "FK_TrainedModels_Instruments_InstrumentId" FOREIGN KEY ( "InstrumentId" ) REFERENCES "public"."Instruments"( "InstrumentId" ) ON DELETE CASCADE;

