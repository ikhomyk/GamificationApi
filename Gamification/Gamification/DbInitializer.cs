namespace Gamification
{
    public class DbInitializer
    {
        public static void Initialize(MyContext context)
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
