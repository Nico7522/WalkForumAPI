using System.Runtime.CompilerServices;

namespace WalkForum.Infrastructure.Persistance
{
    public static class Initializer
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
