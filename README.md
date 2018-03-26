# SVGs-WebAPI
VS Solution of the SVGs WebAPI project

Follow these steps to test the WebAPI project:
1. Download all the files into a new folder.
2. Run Visual Studio as Administrator.
3. Open the SVGsWebAPI.sln file.
4. Wait for a few seconds while VS restores the dependencies of the project. A message like "Restore completed in 4.53 sec for C:\...\SVGsWebAPI\SVGsWebAPI" should appear in the status bar.
5. Build the solution (Menu Build | Build Solution)
6. Run the project (menu Debug | Start Debugging). A browser window will open with the http://localhost:62144/api/svgs url, and depending on the web browser selected to start debugging in VS, JSON data will be displayed on the browser or well, the browser will close and a window will pop up displaying a JSON file that can be saved. This means that the SVGs WebAPI project is working fine.
Running the WebAPI solution for first time will create a database named jmoncisbays, under the localhost\SQLEXPRESS instance of SQL Server, containing a single table named dbo.SVGs, with two sample-rows.

The following API methods are available:
GET http://localhost:62144/api/svgs - Returns all the records from dbo.SVGs
GET http://localhost:62144/api/svgs/n - where n is the ID of a dbo.SVGs record. Returns a single dbo.SVGs record.
POST http://localhost:62144/api/svgs - Insert a record into dbo.SVGs and returns the generated ID.
PUT http://localhost:62144/api/svgs - Update to update a record into dbo.SVGs
DELETE http://localhost:62144/api/svgs/n - where n is the ID of a dbo.SVGs record. Delete the dbo.SVGs record where its ID is equals to n.
