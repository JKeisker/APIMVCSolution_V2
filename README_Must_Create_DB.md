# BadAPIMVCSolution_V2
This version does not call a remote API, but has it's own functional API as a project

In order to download and run this code, you'll have to create the database as follows:
1) in Sql Server, create a new database called TweetsDB
2) create a new table called Tweets with columns : id (numeric(18,0) not null), stamp(datetime2(7) not null), text (varchar(200) not null)
3) run script "insert_data.sql" to create data

DONE ! 
