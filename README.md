# SAP BO 4.2 Web API client for RESTWS
A nice functional SAP Business Objects 4.x WebAPI client for testing requests and exporting Report List, DataProviders and FHSQL under folderID. It works in the Java version, now we turn .NET...  
.NET 4.5.2 is prerequisite.  

Apart from the ability to experiment with the API calls directly checking the response messages, document export is provided given an ancestor folder.  

The FHSQL is exported in an SSIS importable form so the user can import it in SQL server and throw queries on the FHSQL.
Column delimiter is ```*****************{LF} ```
and Row delimiter is ```#################{LF}```
Watch to make the FHSQL column to be Text and not VARCHAR as it can be realyyyyyyyy Loooong!!!!
 
The base of the export is always a folder. All Webi documents below this folder will be exported along with their information and dataprovider type type like :  
* Universe (unv & unx)
* Relational  

Also the Dataprovider connection name is exported for easily finding affected reports from changes.

For 4.2 and above, I use /cmsquery url while for 4.1 I use /infostore.
The Report list will be exported in Excel file if you have Microsoft Office installed. If not, the application will export automatically to CSV, readable by Excel directly wit ; as a delimiter.

I hope you enjoy this application as I did in a huge project of report migration... :)

Panagis Loukatos ! 2018

Many thanks to Argy AEK mono Havana banana Katw ston Peiraia and to MOTOC Koli Adis Abeba !
