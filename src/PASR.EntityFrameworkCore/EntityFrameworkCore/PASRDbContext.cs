using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PASR.Authorization.Roles;
using PASR.Authorization.Users;
using PASR.MultiTenancy;
using PASR.Leads;
using PASR.Calls;
using PASR.Teams;
using PASR.Localization;
using PASR.Goals;

namespace PASR.EntityFrameworkCore
{
    public class PASRDbContext : AbpZeroDbContext<Tenant, Role, User, PASRDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Lead> Leads { get; set; }

        public DbSet<Call> Calls { get; set; }

        public DbSet<Team> Teams { get; set; }

        public PASRDbContext(DbContextOptions<PASRDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            //AbpUsers
            modelBuilder.Entity<User>(u =>
            {
                u.HasMany(u => u.Leads)
                    .WithOne(l => l.AssignedUser)
                    .OnDelete(DeleteBehavior.ClientSetNull); //This implies that a Lead can have no Assigned User as well

                u.OwnsOne("Address", 
                          u => u.Address, 
                          a => 
                          {
                              a.Property(a => a.Street).IsRequired();       //Rua (Endereço)
                              a.Property(a => a.Number).IsRequired();       //Número
                              a.Property(a => a.City).IsRequired();         //Cidade
                              a.Property(a => a.District).IsRequired();     //Bairro
                              a.Property(a => a.FederalUnity).IsRequired(); //UF
                          });

                //Shadow entity
                u.HasMany(u => u.Teams).WithMany(t => t.Users).UsingEntity(ut => ut.ToTable("UserTeams"));

                u.HasMany(u => u.Calls).WithOne(c => c.User);
            });
            
            modelBuilder.Entity<Lead>(l => 
            {
                l.ToTable("Leads");

                l.Property(l => l.Priority).HasDefaultValue(Lead.LeadPriority.Normal);

                l.HasIndex(l => l.IdentityCode).IsUnique();

                l.OwnsMany("Addresses", l => l.Addresses,
                          a => 
                          {
                              a.WithOwner().HasForeignKey("LeadId");
                              a.HasKey("Id");
                              a.Property(a => a.Street).IsRequired();       //Rua (Endereço)
                              a.Property(a => a.Number).IsRequired();       //Número
                              a.Property(a => a.City).IsRequired();         //Cidade
                              a.Property(a => a.District).IsRequired();     //Bairro
                              a.Property(a => a.FederalUnity).IsRequired(); //UF
                          });

                l.HasMany(l => l.Calls).WithOne(c => c.Lead);
            });

            modelBuilder.Entity<Call>(c =>
            {
                c.ToTable("Calls");
                c.Navigation(c => c.Lead).IsRequired();
            });

            // modelBuilder.Entity<Goal>(t => 
            // {
            //     t.ToTable("Goals");

            //     //ShadowProperty
            //     t.Property<int>("TeamId");

            // });

            modelBuilder.Entity<Team>(t =>
            {
                t.ToTable("Teams");

                t.OwnsMany<Goal>(t => t.Goals).ToTable("Goals")
                    .WithOwner(g => g.Team);
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
