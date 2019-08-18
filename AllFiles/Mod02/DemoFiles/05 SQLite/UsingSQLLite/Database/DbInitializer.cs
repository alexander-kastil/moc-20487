

namespace MyFirstEF.Database
{
    public static class  DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            context.Database.EnsureCreated();

           // Code to create initial data
        }
    }
}