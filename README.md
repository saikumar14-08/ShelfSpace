# ShelfSpace
===================== Razor pages ====================
-> Razor pages are often recommended for new, simpler, page-driven applications or when migrating from traditional web forms, due to their ease of use and rapid development capabilities.
-> This is easy to implement and can be used for smaller pages.
-> We don't have traditional Model, View and Controller folders here. We just have the Pages folder.
-> The routing follows the file system structure of the /Pages folder.
-> Each file in pages folder is tied to its respective .cshtml file. For example, We have Index.cshtml file tied to Index.cshtml.cs file.
-> To find whether the project is razor pages or MVC, we have to go to the Program.cs file and there we can see the AddRazorPages builder service method.
and also we can see the app.MapRazorPages(); method. these methods differ for the MVC project.

-> Create models folder:
		-> This is important because we have to create table for our database.
		-> 
-> Create data folder to create and store Entity Framework DBContext class. 
