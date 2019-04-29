We chose to separate our SQL into many different files, so instead of "Tables.sql" you will find a "Tables" directory with many different SQL files in it.
The same goes for the Procedures.

Also, we chose to initialize our tables on the .NET side rather than the SQL side, so you will find Init_Tables.cs in the Initialization directory.

Finally, the entirety of the project code is stored in the "Restaurants Database" directory. Opening the .sln will help navigating throught the code.
If you would like to run the code, you will need to run the "BuildDatabase.ps1" file and change all connection strings in Repository classes from "nivlac12" 
to your username.

You can also find all this code on "https://github.com/nivlac12/Restaurant-Application