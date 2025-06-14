

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project01.AppData;
using Project01.Data;

var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối từ cấu hình
var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection")
                     ?? throw new InvalidOperationException("Connection string 'SQLServerConnection' not found.");

// Cấu hình DbContext
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

// Cấu hình Identity
builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Không yêu cầu xác nhận email
})
.AddEntityFrameworkStores<AppDBContext>();

// Cấu hình LoginPath
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login"; // Đường dẫn đến trang đăng nhập
    //options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn khi bị từ chối quyền
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Thời gian hết hạn cookie
    options.SlidingExpiration = true; // Cập nhật thời gian hết hạn cookie khi hoạt động
});

// Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache(); // Bộ nhớ cho Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Project01.Session"; // Tên cookie của Session
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của Session
    options.Cookie.HttpOnly = true; // Cookie chỉ được sử dụng bởi HTTP
    options.Cookie.IsEssential = true; // Đảm bảo cookie luôn hoạt động
});


// Thêm các dịch vụ MVC và Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Cấu hình Middleware trong pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Trang lỗi cho môi trường không phải Development
}

// Cho phép sử dụng file tĩnh (CSS, JS, hình ảnh, v.v.)
app.UseStaticFiles();

// Kích hoạt Session
app.UseSession();

// Định tuyến
app.UseRouting();

// Authentication và Authorization
app.UseAuthentication(); // Xác thực người dùng
app.UseAuthorization();  // Phân quyền


//app.MapAreaControllerRoute(
//   name: "areas",
//   areaName: "Admin01",
//   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Định nghĩa các route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Route mặc định



app.MapAreaControllerRoute(
   name: "areas",
   areaName: "Admin01",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages(); // Hỗ trợ Razor Pages

// Chạy ứng dụng
app.Run();
