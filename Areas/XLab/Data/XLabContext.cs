using Microsoft.EntityFrameworkCore;
using ProLab.Areas.XLab.Models.DataModels;

namespace ProLab.Areas.XLab.Data
{
    public class XLabContext : DbContext
    {
        public XLabContext(DbContextOptions<XLabContext> options)
            : base(options)
        {
        }

        public DbSet<XLabVideo> XLabVideos => Set<XLabVideo>();
    }
}