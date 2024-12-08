using Microsoft.EntityFrameworkCore;
using EmailServiceIntermediate.Models;
using EmailProviderServer;
using EmailProviderServer.DBContext;
using EmailProvider.Models.DBModels;


public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BulkIncomingMessage> BulkIncomingMessages { get; set; }

    public virtual DbSet<BulkOutgoingMessage> BulkOutgoingMessages { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<EmailServiceIntermediate.Models.File> Files { get; set; }

    public virtual DbSet<IncomingMessage> IncomingMessages { get; set; }

    public virtual DbSet<InnerMessage> InnerMessages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<OutgoingMessage> OutgoingMessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewMessage> ViewMessageSerializable { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=EMAIL_DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BulkIncomingMessage>(entity =>
        {
            entity.ToTable("BULK_INCOMING_MESSAGES", tb => tb.HasComment("Table for incoming message to be processed"));

            entity.Property(e => e.Id)
                .HasComment("ID for incoming message to be processed")
                .HasColumnName("ID");
            entity.Property(e => e.RawData)
                .HasMaxLength(1024)
                .HasComment("Raw data")
                .HasColumnName("RAW_DATA");
        });

        modelBuilder.Entity<BulkOutgoingMessage>(entity =>
        {
            entity.ToTable("BULK_OUTGOING_MESSAGES", tb => tb.HasComment("Table for incoming message to be processed"));

            entity.Property(e => e.Id)
                .HasComment("ID for incoming message to be processed")
                .HasColumnName("ID");
            entity.Property(e => e.OutgoingMessageId)
                .HasComment("refrence to message")
                .HasColumnName("OUTGOING_MESSAGE_ID");
            entity.Property(e => e.RawData)
                .HasMaxLength(1024)
                .HasComment("Raw data")
                .HasColumnName("RAW_DATA");
            entity.Property(e => e.ScheduledDate)
                .HasComment("Time to be sent")
                .HasColumnName("SCHEDULED_DATE");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CATEGORIES_ID");

            entity.ToTable("CATEGORIES", tb => tb.HasComment("Table for user-defined categories"));

            entity.Property(e => e.Id)
                .HasComment("Id of category")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasComment("Name of category")
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_COUNTRY_ID");

            entity.ToTable("COUNTRIES", tb => tb.HasComment("Table for countries"));

            entity.Property(e => e.Id)
                .HasComment("Id of country")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasComment("Name of country")
                .HasColumnName("NAME");
            entity.Property(e => e.PhoneNumberCode)
                .HasMaxLength(5)
                .HasComment("Beginning code of phone number for country")
                .HasColumnName("PHONE_NUMBER_CODE");
        });

        modelBuilder.Entity<EmailServiceIntermediate.Models.File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FILES_ID");

            entity.ToTable("FILES", tb => tb.HasComment("Table for message files"));

            entity.Property(e => e.Id)
                .HasComment("Id of file")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasComment("Content of file")
                .HasColumnName("CONTENT");
            entity.Property(e => e.MessageId)
                .HasComment("Message which contains the file")
                .HasColumnName("MESSAGE_ID");

            entity.HasOne(d => d.Message).WithMany(p => p.Files)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FILE_MESSAGE_ID");
        });

        modelBuilder.Entity<IncomingMessage>(entity =>
        {
            entity.ToTable("INCOMING_MESSAGES", tb => tb.HasComment("Table for incoming email details"));

            entity.Property(e => e.Id)
                .HasComment("ID of incoming email")
                .HasColumnName("ID");
            entity.Property(e => e.MessageId)
                .HasComment("Reference to message ID")
                .HasColumnName("MESSAGE_ID");
            entity.Property(e => e.ReceiverId)
                .HasComment("Receiver user ID")
                .HasColumnName("RECEIVER_ID");
            entity.Property(e => e.SenderEmail)
                .HasMaxLength(64)
                .HasComment("Sender email address")
                .HasColumnName("SENDER_EMAIL");

            entity.HasOne(d => d.Message).WithMany(p => p.IncomingMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INCOMING_MESSAGE_ID");

            entity.HasOne(d => d.Receiver).WithMany(p => p.IncomingMessages)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INCOMING_RECEIVER_ID");
        });

        modelBuilder.Entity<InnerMessage>(entity =>
        {
            entity.ToTable("INNER_MESSAGES", tb => tb.HasComment("Table for inner email details"));

            entity.Property(e => e.Id)
                .HasComment("ID of inner email")
                .HasColumnName("ID");
            entity.Property(e => e.MessageId)
                .HasComment("Reference to message ID")
                .HasColumnName("MESSAGE_ID");
            entity.Property(e => e.ReceiverId)
                .HasComment("Receiver user ID")
                .HasColumnName("RECEIVER_ID");
            entity.Property(e => e.SenderId)
                .HasComment("Sender user ID")
                .HasColumnName("SENDER_ID");

            entity.HasOne(d => d.Message).WithMany(p => p.InnerMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INNER_MESSAGE_ID");

            entity.HasOne(d => d.Receiver).WithMany(p => p.InnerMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INNER_RECEIVER_ID");

            entity.HasOne(d => d.Sender).WithMany(p => p.InnerMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INNER_SENDER_ID");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("MESSAGES", tb => tb.HasComment("Table for emails"));

            entity.Property(e => e.Id)
                .HasComment("ID of the message")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(896)
                .HasComment("Content of the message")
                .HasColumnName("CONTENT");
            entity.Property(e => e.DateOfCompletion)
                .HasComment("Date in which the message was processed")
                .HasColumnType("datetime")
                .HasColumnName("DATE_OF_COMPLETION");
            entity.Property(e => e.Status)
                .HasComment("Current status of the email")
                .HasColumnName("STATUS");
            entity.Property(e => e.Subject)
                .HasMaxLength(128)
                .HasComment("Subject of the message")
                .HasColumnName("SUBJECT");
        });

        modelBuilder.Entity<OutgoingMessage>(entity =>
        {
            entity.ToTable("OUTGOING_MESSAGES", tb => tb.HasComment("Table for outgoing email details"));

            entity.Property(e => e.Id)
                .HasComment("ID of outgoing email")
                .HasColumnName("ID");
            entity.Property(e => e.MessageId)
                .HasComment("Reference to message ID")
                .HasColumnName("MESSAGE_ID");
            entity.Property(e => e.ReceiverEmail)
                .HasMaxLength(64)
                .HasComment("Receiver email address")
                .HasColumnName("RECEIVER_EMAIL");
            entity.Property(e => e.SenderId)
                .HasComment("Sender user ID")
                .HasColumnName("SENDER_ID");

            entity.HasOne(d => d.Message).WithMany(p => p.OutgoingMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OUTGOING_MESSAGE_ID");

            entity.HasOne(d => d.Sender).WithMany(p => p.OutgoingMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OUTGOING_SENDER_ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_USERS_ID");

            entity.ToTable("USERS", tb => tb.HasComment("Table for users"));

            entity.Property(e => e.Id)
                .HasComment("ID for users")
                .HasColumnName("ID");
            entity.Property(e => e.CountryId)
                .HasComment("Country id of user")
                .HasColumnName("COUNTRY_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasComment("Email of user")
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasComment("Name of user")
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasComment("Password of user")
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("Phone number of user")
                .HasColumnName("PHONE_NUMBER");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_COUNTRY_ID");
        });

        modelBuilder.Entity<ViewMessage>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("ViewMessageSerializable");
            entity.Ignore(e => e.ReceiverEmailsList);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
