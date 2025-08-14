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
	
-> Usually we don't create databases directly instead we write models and the entity framework makes use of this models and creates a database.
-> Files in Controllers and Views folders follow a naming convention like below:
	-> Controller: HomeController.cs
	-> Viewls: Index.cshtml
-> But models don't have such criteria. You can name them whatever you want.
-> We created a category model and added some properties.
-> To make the property as primary key we have to data annotation.
-> Data Annotation is nothing but [Key], [Required] - Just put these before the properties.
-> Now that we have created columns(properties) of the database and mentioned the primary key and mandatory fields. We have to create a connection Stirng.

-> Install SQL Server and SQL Server Management Studio(SMMS).
-> After that connect to any server like localhost or . 
-> Although we set up our entity framework in Program.cs but we have to declare our DB string in our appSettings.json.
-> Create a ConnectionStrings entry in the file and put the connection details like shown below:
	"ConnectionStrings": {
		"DBConn": "Server:.;Database=Shelf;Trusted_Connection=True;TrustedServerCertificate=True"
	  }

-> Settign up .NET entity framework: 
	-> Right click on the prject folder and select "Manage NuGet Packages" and then install the below mentioned 3 packages.
	-> Make sure you install the packages with version same as your .Net version. If not things doesn't work.
	-> Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
	-> Create a new folder in parent directory with name "Data".
	-> Create a ApplicationDbContext class in data folder.
	-> Any class in ApplicationDbContext should extend DbContext class.
	-> Create a constructor.
	-> DbContext class -- Manages connection + represents your database.
	-> Entity class	-- Represents one table row (schema of your table)
	-> Registering DbContext -- Tells the app how to create your DbContext with config.

-> Click on Tools -> NuGet Package Manager -> Package Manager Console.
-> To create a database, type update-database in the console and enter.
	-> You can get two types of errors here:
		1. Either the key in appsettings.json is wrong.
		2. Or the DB connection String is wrong.

-> Creating a Category table:
	-> We already declared the Category Model.
	-> In our ApplicationDBContext we have to create a DBSet of our table.
	-> Type add-migration <Any name> in console  and enter.
	-> After this migrations folder will be added directly to our project structure.
	-> But you cannot see the table in your SQLMS.
	-> For that, you need to update-database.

-> CRUD operations of Category table.
	-> Create a Category controller.
	-> Create a Category folder in Views folder then Index.cshtml in it.
	-> This view loads when you navigate to the category url
	-> Go to _Layout.cshtml in Shared folder in Views folder and add Catrgory there.
	-> You can use attributes like asp-controller="Category" asp-action="Index" in the anchor to 
		understand what controller should be used and what view should be loaded.

-> Seeding Category table:
	-> We can directly add entries in our SQL but we can do that using our entity core framework.
	-> Create a function and give the information there like shown below:
		 protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }

                );
		 }
	-> After this we have to give add-migration SeedCategoryTable in Nuget console.
	-> Then update-databse to get details updated on our table in SQLMS.

-> Get all Categories in the UI:
	-> Goto CategoryController.cs and then create a constructor which takes the ApplicationDBContext as an arg.
	-> This is responsible to fetch the data from DB and then we do View(data).
	-> This passes the data and we use this data in our view like below:
	-> In the starting line we put @model List<Category> and this can be iterated using @foreach(var cat in Model)
		and then @cat.<your data>

-> Creating a new category from UI:
	-> Create a button in UI.
	-> We first have to create an action method that will be invoked and call the view.
	-> These action methods are written in the controller(CategoryController).
	-> After writing these, right click on the method and select Add View.
	-> This creates a cshtml view and now you can edit the file based on your requirement.
	-> Before that we have to map the button to our action method using the asp-action attr
-> Input tag helper: The asp-for attribute is a server-side attribute used within HTML elements (like <input>, <label>, <select>, <textarea>) in Razor views in ASP.NET Core.
	Its primary function is to bind an HTML element to a specific property of a model passed to the view.

-> We can use DisplayName Annotation in our Category.cs model to display the required name for the field.

-> To save our data in the DB, we used post form. For that, now we have to goto out category controller and we have create a action method which takes the Category obj as arg and add the obj to the DB and save the changes to db.
	-> _db.Add(obj); _db.SaveChanges()

Server Side Validations:
-> Server-side validation always runs regardless of client-side validation and is essential for security.
-> Model validation happens automatically when a controller action receives a model parameter decorated with validation attributes.
-> You check validation state in the controller using:
	if (!ModelState.IsValid) — means validation failed.
-> Use ModelState.AddModelError to add custom validation errors manually in controller or service logic.
-> ModelState.AddModelError("PropertyName", "Error message") — property-specific error.
-> ModelState.AddModelError(string.Empty, "Error message") — model-level (general) error.
-> After validation fails, return the model and errors back to the view so errors can be displayed.

Server-side validation handles:
-> Complex rules that client-side can’t enforce (e.g., checking database uniqueness).
-> Protects against malicious or disabled client-side validation.
-> Validation attributes like [Required], [Range], etc., run on server during model binding.
-> You can implement custom validation attributes by inheriting from ValidationAttribute and overriding IsValid method.
-> Use TryValidateModel(model) if you want to validate a model manually inside controller or service.
-> To get all errors in code, you can iterate over ModelState.Values and their .Errors collection.


	=============== FLOW =============

1. Create a database model
Create a C# class (e.g., Category.cs) in the Models folder with the required fields:
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

2. Create a Data folder and ApplicationDbContext
In Data/ApplicationDbContext.cs:
	using Microsoft.EntityFrameworkCore;
	using YourNamespace.Models;

	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
		public DbSet<Category> Categories { get; set; }
	}

3. Register the DbContext in Program.cs
	builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

4. Add a migration
	Add-Migration InitialCreate
This generates the SQL structure for your database based on the model and context.

5. Update the database
	Update-Database
This actually creates the tables in your database.

6. Create CategoryController
Inject the context into the constructor:
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			var categories = _db.Categories.ToList();
			return View(categories);
		}
	}
7. Create the View
Create a Views/Category/Index.cshtml view to display the categories.


Client-Side Validation:
-> asp-validation-summary displays validation errors summary in one place.
-> "All" shows all errors (property-level + model-level).
-> "ModelOnly" shows only model-level errors.
-> "None" disables summary display (use field-level errors only).
-> asp-validation-for displays error message for a specific property next to its input field.

Common validation attributes and usage:
-> [Required(ErrorMessage = "Error message")] — field must not be empty.
-> [Range(1, 100, ErrorMessage = "Error message")] — value must be between 1 and 100.
-> [StringLength(50, MinimumLength = 5, ErrorMessage = "Error message")] — string length between min and max.
-> [EmailAddress(ErrorMessage = "Error message")] — validates email format.
-> [RegularExpression("regex", ErrorMessage = "Error message")] — custom pattern validation.
-> [Compare("OtherProperty", ErrorMessage = "Error message")] — compare values of two properties.

Client-side validation requires these scripts:
-> jquery.min.js
-> jquery.validate.min.js
-> jquery.validate.unobtrusive.min.js
-> Model-level errors can be added in controller using:
-> ModelState.AddModelError(string.Empty, "Error message")

These errors are not tied to any specific property.

Tips:
-> Use asp-validation-summary="All" for quick display of all errors.
-> Use asp-validation-for to show error messages near input fields.
-> Include client validation scripts for instant feedback.
-> Validation messages come from the ErrorMessage property of validation attributes.
-> [Required] fails validation if input is empty.
-> [Range] works only on numeric properties.

-> Finding differences between client side and server side validations.
	-> In client side validation, the validations happen even without loading the page.
	-> That means the network call doesn't happen where as in server side validation the call happens and we get the appropriate erroe as a response

-> Edit a category.
	-> When ever we open category page, we are getting the data from category table, 
	but to edit the data we need to fetch the data and again post the data after the change.
	-> So we have to create a [httpPost] for the Category data in controller.
	-> We should also create an edit method so that we can get the specific Id selected by the user.
	-> There are multiple ways to do this:
		-> Category? categoryFromDb = _db.Categories.Find(id);
			-> Find: 
				-> Available on List<T> only.
				-> Returns the first matching element or default if no match found.
				-> Stops after finding the first match.
			Example:
				var result = myList.Find(x => x.Id == 5);
		-> Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
			-> FirstOrDefault
				-> LINQ extension method — works on any IEnumerable<T>.
				-> Returns the first matching element or default if no match found.
				-> Similar to First() but avoids exception on no match.
			Example:
				var result = myList.FirstOrDefault(x => x.Id == 5)
		-> Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
			-> Where
				-> LINQ extension method — works on any IEnumerable<T>.
				-> Returns all matching elements as an IEnumerable<T>.
				-> Use .ToList() or .ToArray() to execute and store results.
			Example:
				var results = myList.Where(x => x.Id > 5).ToList();