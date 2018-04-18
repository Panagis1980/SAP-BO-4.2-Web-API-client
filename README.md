# SAP-BO-4.2-Web-API-client
Incomplete yet functional SAP Business Objects 4.x WebAPI client for tesing requests and exporting Report List, DataProviders and FHSQL under folderID. It works in the Java version, now we turn .NET

The FHSQL is exported in an SSIS importable form so the user can import it in SQL server and through queries on the FHSQL.

The base of the export is always a folder. All Webi documents below this folder will be exported along with their information and dataprovider type type like :  
* Universe (unv & unx)
* Relational  

Also the Dataprovider connection name is exported for easily finding affected reports from changes.

I hope you enjoy this application as I did in a huge project of report migration... :)

Panagis Loukatos ! 2018

