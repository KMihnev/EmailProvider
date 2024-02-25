//Includes
using EmailProviderServer.Models;
using Microsoft.EntityFrameworkCore;


namespace EmailProviderServer.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BulkIncomingMessage> BulkIncomingMessages { get; set; }

        public DbSet<BulkOutgoingMessage> BulkOutgoingMessages { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Models.File> Files { get; set; }

        public DbSet<IncomingMessage> IncomingMessages { get; set; }

        public DbSet<OutgoingMessage> OutgoingMessages { get; set; }

        public DbSet<User> Users { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Задаваме pk за всяка таблица
            builder.Entity<BulkIncomingMessage>().HasKey(bim => bim.Id);
            builder.Entity<BulkOutgoingMessage>().HasKey(bom => bom.Id);
            builder.Entity<Category>().HasKey(c => c.Id);
            builder.Entity<Country>().HasKey(c => c.Id);
            builder.Entity<Models.File>().HasKey(f => f.Id);
            builder.Entity<IncomingMessage>().HasKey(im => im.Id);
            builder.Entity<OutgoingMessage>().HasKey(om => om.Id);
            builder.Entity<User>().HasKey(u => u.Id);

            //Задаваме референции
            builder.Entity<Category>()
                .HasOne<User>(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId);

            builder.Entity<User>()
                .HasOne<Country>(u => u.Country)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CountryId);

            builder.Entity<IncomingMessage>()
                .HasOne<User>(im => im.Sender)
                .WithMany(u => u.IncomingMessageSenders)
                .HasForeignKey(im => im.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IncomingMessage>()
                .HasOne<User>(im => im.Receiver)
                .WithMany(u => u.IncomingMessageReceivers)
                .HasForeignKey(im => im.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OutgoingMessage>()
                .HasOne<User>(om => om.Sender)
                .WithMany(u => u.OutgoingMessageSenders)
                .HasForeignKey(om => om.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OutgoingMessage>()
                .HasOne<User>(om => om.Receiver)
                .WithMany(u => u.OutgoingMessageReceivers)
                .HasForeignKey(om => om.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Models.File>()
                .HasMany<IncomingMessage>(f => f.IncomingMessages)
                .WithOne(im => im.File)
                .HasForeignKey(im => im.FileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Models.File>()
                .HasMany<OutgoingMessage>(f => f.OutgoingMessages)
                .WithOne(om => om.File)
                .HasForeignKey(om => om.FileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<BulkOutgoingMessage>()
                .HasOne<OutgoingMessage>(f => f.OutgoingMessage)
                .WithOne()
                .HasForeignKey<BulkOutgoingMessage>(bom => bom.OutgoingMessage);

            builder.Entity<Category>()
                .HasMany<IncomingMessage>(c => c.IncomingMessages)
                .WithOne(im => im.Category)
                .HasForeignKey(im => im.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
