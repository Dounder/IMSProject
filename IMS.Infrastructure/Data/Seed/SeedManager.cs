using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Data.Seed;

public static class SeedManager
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        UserSeed.Seed(modelBuilder);
    }
}