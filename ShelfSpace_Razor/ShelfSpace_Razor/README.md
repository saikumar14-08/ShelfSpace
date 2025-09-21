0 — Assumptions

.NET 8 app (your .csproj already shows EF Core 8 packages).

You have Program.cs, appsettings.json, an ApplicationDBContext, and an entity Category.

You’re using Razor Pages, but examples work the same for controllers.

1 — Connection string (appsettings.json)

Put your DB connection there (example for local SQL Server):

{
  "ConnectionStrings": {
    "DBConn": "Server=.;Database=MyDatabase;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

2 — Register DbContext in Program.cs (DI)

Minimal .NET 8 example:

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConn")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();


Why: AddDbContext<T> registers ApplicationDBContext as scoped (one instance per HTTP request). DI will create it for you and pass configured DbContextOptions<T> via the constructor.

3 — ApplicationDBContext and Category (minimal)
public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = default!;
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}


Why the constructor? DI provides DbContextOptions<T>; EF needs that constructor to configure SQL Server, logging, etc.

4 — Install / verify EF tools (if you’ll use CLI)

If you use terminal/VS Code, install the CLI tool:

dotnet tool install --global dotnet-ef
# or update
dotnet tool update --global dotnet-ef

dotnet ef --version


If you use Visual Studio Package Manager Console, you can run Add-Migration there.

If you get add-migration : The term 'add-migration' is not recognized — that means you ran the PMC command in a regular PowerShell window. Use PMC in Visual Studio, or run the dotnet ef CLI.

5 — Add a migration & apply it

CLI (recommended outside VS):

dotnet ef migrations add Add_Category_Table
dotnet ef database update


Package Manager Console (Visual Studio):

Add-Migration Add_Category_Table
Update-Database


Multi-project solutions: specify --project and --startup-project:

dotnet ef migrations add Add_Category_Table --project src/My.Data --startup-project src/My.Web

6 — Basic reads (LINQ & EF)

Synchronous (not recommended for I/O heavy apps):

var categories = _db.Categories.ToList();


Async (recommended for web apps):

using Microsoft.EntityFrameworkCore;
var categories = await _db.Categories.ToListAsync();


Single item:

var cat = await _db.Categories.FirstOrDefaultAsync(c => c.Id == 1);


Filtered:

var electronics = await _db.Categories
                           .Where(c => c.Name == "Electronics")
                           .ToListAsync();

7 — Use DbContext in a Razor Page (dependency injection)

Pages/Categories/Index.cshtml.cs:

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class IndexModel : PageModel
{
    private readonly ApplicationDBContext _db;
    public List<Category> CategoryList { get; set; } = new();

    public IndexModel(ApplicationDBContext db) => _db = db;

    public async Task OnGetAsync()
    {
        CategoryList = await _db.Categories.AsNoTracking().ToListAsync();
    }
}


Index.cshtml:

@page
@model IndexModel

<table class="table">
  <thead><tr><th>Id</th><th>Name</th></tr></thead>
  <tbody>
  @foreach (var c in Model.CategoryList) {
    <tr><td>@c.Id</td><td>@c.Name</td></tr>
  }
  </tbody>
</table>

8 — CRUD examples

Create (OnPostAsync):

[BindProperty]
public Category Category { get; set; } = new();

public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid) return Page();

    await _db.Categories.AddAsync(Category);
    await _db.SaveChangesAsync();
    return RedirectToPage("Index");
}


Update:

public async Task<IActionResult> OnPostEditAsync(int id)
{
    var category = await _db.Categories.FindAsync(id);
    if (category == null) return NotFound();

    category.Name = "New Name";
    await _db.SaveChangesAsync(); // tracked entity => update
    return RedirectToPage("Index");
}


Delete:

public async Task<IActionResult> OnPostDeleteAsync(int id)
{
    var category = await _db.Categories.FindAsync(id);
    if (category == null) return NotFound();

    _db.Categories.Remove(category);
    await _db.SaveChangesAsync();
    return RedirectToPage("Index");
}

9 — Useful query & performance tips

Use AsNoTracking() for read-only queries (less overhead).

Use Include(...) for eager-loading related data:

var list = await _db.Categories.Include(c => c.Products).ToListAsync();


Use projection to DTOs to avoid pulling full entities:

var dtos = await _db.Categories
                    .Select(c => new { c.Id, c.Name })
                    .ToListAsync();


Pagination:

int page = 1, pageSize = 10;
var pageItems = await _db.Categories
                         .OrderBy(c => c.Id)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();


See generated SQL (dev only):

var q = _db.Categories.Where(c => c.Name == "x");
var sql = q.ToQueryString(); // helpful for debugging


Enable EF logging in dev (console logging, dev only):

options.UseSqlServer(conn)
       .LogTo(Console.WriteLine, LogLevel.Information)
       .EnableSensitiveDataLogging(); // dev only — shows parameter values

10 — Transactions & concurrency

SaveChanges() is atomic for tracked changes in a single DbContext by default.

For multiple SaveChanges/explicit grouping use:

using var tx = await _db.Database.BeginTransactionAsync();
try {
  // multiple operations
  await _db.SaveChangesAsync();
  await tx.CommitAsync();
} catch {
  await tx.RollbackAsync();
  throw;
}


For concurrency control, add a rowversion/timestamp column and use EF concurrency tokens.

11 — Migrations troubleshooting (common problems)

add-migration not found → you’re in regular PowerShell, not Package Manager Console. Use PMC in Visual Studio or dotnet ef CLI.

Wrong project/startup project → supply --project and --startup-project.

dotnet ef not installed → run dotnet tool install --global dotnet-ef.

Missing Design package → add Microsoft.EntityFrameworkCore.Design if tools complain:

dotnet add package Microsoft.EntityFrameworkCore.Design


If migrations target a different assembly, configure MigrationsAssembly in UseSqlServer or pass args.

12 — Testing tips

For unit tests use UseInMemoryDatabase("TestDb") or UseSqlite (SQLite file in-memory).

Avoid using your production DB in unit tests.

13 — Best practices checklist

Use async EF methods in web apps.

Use AsNoTracking for read-only lists.

Don’t inject DbContext into singletons (only scoped or transient where appropriate).

Use small DTOs / ViewModels in the UI layer instead of exposing entities directly.

Keep migrations in the data project and apply them in deployment pipelines (CI/CD).

Seed data via OnModelCreating or via separate seeding step in startup.

14 — Quick command summary

Install EF CLI:

dotnet tool install --global dotnet-ef


Add migration:

dotnet ef migrations add Add_Category_Table


Apply migration:

dotnet ef database update


PMC equivalents:

Add-Migration Add_Category_Table
Update-Database

15 — Final short example (end-to-end)

Add connection string to appsettings.json.

Register AddDbContext<ApplicationDBContext> in Program.cs.

Create Category entity and ApplicationDBContext.

dotnet ef migrations add Init → dotnet ef database update.

Inject ApplicationDBContext in Razor Page and run await _db.Categories.ToListAsync() to fetch and render.