using System.Data.Entity;

using Domain.Models;

namespace DataAccessLayer
{
    public class OnlineJudgeContext : DbContext
    {
        public DbSet<Problem> Problems { get; set; } 
    }
}
