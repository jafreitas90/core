using EntityFrameworkCore.ManyToMany.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.ManyToMany.EntityConfigurations
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public ApplicationDbContext()
        {
            // Ensure DataBase is created
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // If more convenient, you can use an in-memory database
            //optionsBuilder.UseInMemoryDatabase("EFTutorial");

            optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Database=EFTutorial5;integrated security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactSkill>().HasKey(k => new { k.SkillId, k.ContactId });

            modelBuilder.Entity<ContactSkill>()
                .HasOne(x => x.Skill)
                .WithMany()
                .HasForeignKey(x => x.SkillId);

            modelBuilder.Entity<ContactSkill>()
               .HasOne(x => x.Contact)
               .WithMany(x => x.Skills)
               .HasForeignKey(x => x.ContactId);

            base.OnModelCreating(modelBuilder);
        }
    }
}