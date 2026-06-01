# Task_Management
Interview trial task project

Steps to run the whole project:

Clone the repo from https://github.com/MiroslavAleksovski/Task_Management.git, publicly accessible repo by Miroslav Aleksovski, so permission issue should not happen
#Frontend
	Frontend part is under Frontend folder, open this folder with Visual Studio Code and navigate to this folder to be root directory
	Run npm i, to install all neccesarry libraries
	Run npm run dev to start the React app, it will be locally hosted (in my case http://localhost:5173/tasks)
#Backend
	Backend part can be found under Backend folder, the Tasks WebAPI main poject and relevant DLLs
	Open the solution in Visual Studio and launch the backend API by clicking the IISExpress deployment, the settings shold be set
	The backend API should be hosted on http://localhost:64593/swagger/index.html
#Database
	Install SQL Server from https://www.microsoft.com/en-us/sql-server/sql-server-downloads, for development i used SQL Server Standard Developer Edition, no prod edition
	Install SSMS from https://learn.microsoft.com/en-us/ssms/install/install, i used SSMS 22
	Run the SQL script SQL_database_script.sql, found in the root directory of the project, in order to create the database
	Run the SQL script SQL_tables_script.sql, found in the root directory of the project, in order to create the relevant tables in the database
	
With all of this set, the whole app should be functional