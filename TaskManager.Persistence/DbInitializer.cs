namespace TaskManager.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(TaskManagerDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
