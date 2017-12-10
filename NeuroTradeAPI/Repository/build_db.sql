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

INSERT INTO "public"."Instruments"( InstrumentId, Alias, DownloadAlias ) VALUES ( 1, 'RTS-12.17(RIZ7)', 'SPFB.RTS-12.17' ); 
INSERT INTO "public"."Instruments"( InstrumentId, Alias, DownloadAlias ) VALUES ( 2, 'SBPR-12.17(SPZ7)', 'SPFB.SBPR-12.17' ); 

INSERT INTO "public"."Batches"( BatchId, BeginTime, EndTime, InstrumentId, Interval ) VALUES ( 1, '2017-11-01 10:00:00', null, 1, '0 years 0 mons 0 days 1 hours 0 mins 0.00 secs' ); 
INSERT INTO "public"."Batches"( BatchId, BeginTime, EndTime, InstrumentId, Interval ) VALUES ( 2, '2017-11-01 10:00:00', null, 2, '0 years 0 mons 0 days 0 hours 30 mins 0.00 secs' ); 

INSERT INTO "public"."Candles"( CandleId, BatchId, BeginTime, Close, High, Low, Open, Volume ) VALUES ( 1, 1, null, 112480.0, 112600.0, 111700.0, 111700.0, 80244 ); 
INSERT INTO "public"."Candles"( CandleId, BatchId, BeginTime, Close, High, Low, Open, Volume ) VALUES ( 2, 1, null, 112510.0, 112720.0, 112470.0, 112470.0, 38690 ); 
INSERT INTO "public"."Candles"( CandleId, BatchId, BeginTime, Close, High, Low, Open, Volume ) VALUES ( 3, 2, null, 16091.0, 16115.0, 16001.0, 16047.0, 1751 ); 
INSERT INTO "public"."Candles"( CandleId, BatchId, BeginTime, Close, High, Low, Open, Volume ) VALUES ( 4, 2, null, 16086.0, 16092.0, 16058.0, 16091.0, 919 ); 

