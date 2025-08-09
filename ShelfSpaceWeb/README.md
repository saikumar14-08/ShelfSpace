-> Click on new project and select ASP .NET Core Web(Model-View-Controller)
-> Give project name and solution name.
-> Selected HTTPS checkbox while creating a project.
-> Pushed the starter code to github.
-> Ran the project and faced a certificate error in chrome but worked good in the brave.
	-> dotnet dev-certs https --clean
	-> dotnet dev-certs https --trust

-> To check project specifications right click on the prject folder and select "Edit Prject File" which opens a XML file.
	-> Each tag here is called MSBuild Elements.
	-> TargetFramework specifies the version of dotnet used.
	-> ImplicitUsings automatically imports the file usings.

-> Dependencies: This folder will store all the dependencies we use in our project.
Even if we want to use another project in our project, we use it in dependencies. This folder just have the views if the packages installed.
The original code lies at global NuGet cache (e.g., C:\Users\<User>\.nuget\packages).

-> Properties: This folder has launchSettings.json file which has the configuration of ports, URLs, environment vars for dev env.

-> wwwroot Folder: This folder is crucial for hosting all static content, such as CSS, JavaScript files, images, and other files that do not contain HTML code. The lecture emphasized the importance of organizing static files here to maintain consistency. Specific examples mentioned were global site.css and site.js, the latter currently being empty but intended as a template for future JavaScript code.

-> appsettings.json: This file plays a vital role in storing connection strings and secret keys. It is designated for managing sensitive information like email service keys and database connection strings in an organized manner. The lecture also highlighted that there can be environment-specific versions, such as appsettings.development.json and appsettings.production.json, which help ensure that the appropriate configurations are used based on the application’s environment.

-> Earlier we were using Program.cs and Startup.cs in .NET core both of them combined and only Program.cs is used.
-> *** Wheneve you have to config something in the pipeline program.cs is the file you have to change.
-> We have two major parts in Program.cs file:
	-> Add Services to container:
		-> Created and used builder to initialize the application.
		-> Dependency injection handled in this and also we have many more to this.
	-> Configurations:
		-> Routing config.
		-> HTTP redirection(environment config like production or development).
		-> Using static files.
		-> Authentication and authorization.


-> MVC Architecture: The MVC (Model-View-Controller) architecture is a design pattern that separates an application into three interconnected components, promoting organized code and improving maintainability. 
-> **Controller is the heart of MVC architecture.
	-> Model: The Model represents the data structure and business logic of the application. 
		It defines the shape of your data, including classes that represent database tables and entities; 
		for instance, in an e-commerce application, you would have models for products, orders, and customers. 
		Models manage the application's data and metadata, which can be fetched from a database.

	-> View: The View is the user interface of the application. 
		It controls what the user sees on the screen, such as the layout and design elements. 
		For example, all the HTML elements, forms, and charts that users interact with are part of the View.
		It renders the data provided by the Controller in a user-friendly format.

	-> Controller: The Controller acts as the intermediary between the Model and the View. 
		It handles user input by processing requests and determining actions based on the input. 
		When a user interacts with the View (like clicking a button), the request is sent to the Controller, which then retrieves the necessary data from the Model and sends it back to the View for display.
		The Controller essentially manages the application's flow and user requests.

-> The flow of information in MVC generally follows this sequence:
	The user interacts with the View (e.g., clicking a button).
	The Controller processes the request and retrieves data from the Model.
	The Model returns the data to the Controller, which then passes it to the View.
	The View updates to reflect the data provided by the Model.
	This architecture promotes a clear separation of concerns, making applications easier to develop and maintain.

Routing in MVC:
	-> In Program.cs, you have a file with content
	app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
	-> The pattern in above code tells us what is the controller and action in an MVC app URL.
	-> So the default pattern uses Home controller with Index action.
	-> The URL pattern for routing is considered after the domian name.
	Example: https://localhost:1234/Category/Index/2
	How we have to see the url: https://localhost:1234/{Controller}/{Index}/{Id}

Routing in action:
	-> When ever we use routing, as discussed above the route first goes to the controller which is Home in out case and there we have a method of type IActionResult
		and name of our controller.
	-> Then the method calls the view file .cshtml which gives us the view.
	-> We can always overwrite the routes in our controller or Program.cs by using the IActionResult method.
	-> The method with return type IActionResult simply returns the method name View if there is no argument in View();
	-> If there is an argument then the route goes to the prrovided argument name.
	-> Here the name of the method and argument in View should be the name of the cshtml file from our view.

Default Views:
	-> In every route, we get header and footer components in our page this is happening because we have RenderBody() in Views->Shared->_Layout.cshtml file.
	-> _Layout.csthml is the master page of our application.
		<div class="container">
			<main role="main" class="pb-3">
				@RenderBody()
			</main>
		</div>

-> We usually don't need _ before the file names. But we are using _ for Layout and ValidationnScripts because these files are used
all over the application.
-> These file specifically have no view in our application but used all across application. These are called partial views.
-> Partial views are the views that we do not use by themselves but instead they will be consumed in the main view.

Q.) How do we know that _layout.cshtml is the main layout of our application?
A.) This is defiend in the _ViewStart.cshtml

Q.) What is Dependency Injection?
A.) It is a design pattern in which class or object has its dependencies injected rather than directly creating them.
This is done so that we don't have to create, manage and dispose the object. It also helps loose coupling between classes.
Example:
	-> You have a email and db class and 3 pages where you have to implement them, so you generally create a new email and db objects in each of the page.
	-> By this code gets dulicated and uncessary memory gets allocated.
	-> To avoid this we create a layer with dependency injection container and in this we create objects and use them in the pages.
	-> By doing this a lot of memory gets saved, code looks good(few lines) and in future if you want to change the db or email class then you can change only at one place
	