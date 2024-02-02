using KyrsachAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace KyrsachAPI.Context
{
    public class TaskTrackContext : DbContext
    {
        DbSet<User> Users { get; set; }
        public TaskTrackContext(DbContextOptions<TaskTrackContext> options) : base(options)
        {

        }
    }
}
