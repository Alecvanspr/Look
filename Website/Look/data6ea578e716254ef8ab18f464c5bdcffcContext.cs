using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Look
{
    public partial class data6ea578e716254ef8ab18f464c5bdcffcContext : DbContext
    {
        public data6ea578e716254ef8ab18f464c5bdcffcContext()
        {
        }

        public data6ea578e716254ef8ab18f464c5bdcffcContext(DbContextOptions<data6ea578e716254ef8ab18f464c5bdcffcContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=94.209.210.86;database=data6ea578e716254ef8ab18f464c5bdcffc;user=Groepje1E;password=b48e3c8796024b86b825276414a0ca4b;treattinyasboolean=true", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
