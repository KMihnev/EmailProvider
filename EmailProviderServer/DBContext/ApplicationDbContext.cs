using System;
using System.Collections.Generic;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using File = EmailServiceIntermediate.Models.File;

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

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<EmailServiceIntermediate.Models.File> Files { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageRecipient> MessageRecipients { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

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

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_COUNTRY_ID");

            entity.ToTable("COUNTRIES", tb => tb.HasComment("Table for countries list"));

            entity.Property(e => e.Id)
                .HasComment("Country ID")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasComment("Name of the country")
                .HasColumnName("NAME");
            entity.Property(e => e.PhoneNumberCode)
                .HasMaxLength(5)
                .HasComment("Country dialing code prefix")
                .HasColumnName("PHONE_NUMBER_CODE");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FILES_ID");

            entity.ToTable("FILES", tb => tb.HasComment("Table for storing attachments/files linked to messages"));

            entity.Property(e => e.Id)
                .HasComment("File ID")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasComment("Binary content of the file")
                .HasColumnName("CONTENT");
            entity.Property(e => e.MessageId)
                .HasComment("Message ID the file is attached to")
                .HasColumnName("MESSAGE_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasComment("File name including extension")
                .HasColumnName("NAME");

            entity.HasOne(d => d.Message).WithMany(p => p.Files)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FOLDERS__3214EC27AC17414E");

            entity.ToTable("FOLDERS", tb => tb.HasComment("Table for user-defined folders"));

            entity.HasIndex(e => new { e.UserId, e.Name, e.FolderDirection }, "UX_FOLDERS_USER_FOLDER").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("ID")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasComment("Name of the folder")
                .HasColumnName("NAME");
            entity.Property(e => e.UserId)
                .HasComment("User owning the folder")
                .HasColumnName("USER_ID");
            entity.Property(e => e.FolderDirection)
                .HasComment("Direction of messages inside the folder")
                .HasColumnName("FOLDER_DIRECTION");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FOLDERS_USERS");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MESSAGES__3214EC27490A47D7");

            entity.ToTable("MESSAGES", tb => tb.HasComment("Table for storing message metadata and content"));

            entity.Property(e => e.Id)
                .HasComment("ID")
                .HasColumnName("ID");
            entity.Property(e => e.Body)
                .HasComment("Body/content of the message")
                .HasColumnName("BODY");
            entity.Property(e => e.DateOfRegistration)
                .HasComment("Date/time the message was sent")
                .HasColumnType("datetime")
                .HasColumnName("DATE_OF_REGISTRATION");
            entity.Property(e => e.Direction)
                .HasComment("Message direction (incoming, outgoing, internal)")
                .HasColumnName("DIRECTION");
            entity.Property(e => e.FromEmail)
                .HasMaxLength(128)
                .HasComment("Sender email address")
                .HasColumnName("FROM_EMAIL");
            entity.Property(e => e.Status)
                .HasComment("Message status (draft, sent, etc.)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Subject)
                .HasMaxLength(128)
                .HasComment("Subject of the message")
                .HasColumnName("SUBJECT");
            modelBuilder.Entity<Message>()
                .Property(m => m.IsInternal)
                .HasColumnName("IS_INTERNAL")
                .HasDefaultValue(false)
                .HasComment("True if message is between users only");
        });

        modelBuilder.Entity<MessageRecipient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MESSAGE___3214EC27AB4FB781");

            entity.ToTable("MESSAGE_RECIPIENTS", tb => tb.HasComment("Table for message recipients"));

            entity.Property(e => e.Id)
                .HasComment("ID")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .HasComment("Email address of recipient")
                .HasColumnName("EMAIL");
            entity.Property(e => e.MessageId)
                .HasComment("Id of message")
                .HasColumnName("MESSAGE_ID");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MESSAGE_R__MESSA__47E69B3D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_USERS_ID");

            entity.ToTable("USERS", tb => tb.HasComment("Table for system users"));

            entity.Property(e => e.Id)
                .HasComment("User ID")
                .HasColumnName("ID");
            entity.Property(e => e.CountryId)
                .HasDefaultValue(1)
                .HasComment("Foreign key to country")
                .HasColumnName("COUNTRY_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasComment("Email address of the user")
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasComment("Full name of the user")
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasComment("Password hash or secret")
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("User phone number")
                .HasColumnName("PHONE_NUMBER");

            entity.HasOne(d => d.Country).WithMany(p => p.Users).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MessageId });

            entity.ToTable("USER_MESSAGES", tb => tb.HasComment("Table to store per-user message state"));

            entity.Property(e => e.UserId)
                .HasComment("User ID")
                .HasColumnName("USER_ID");
            entity.Property(e => e.MessageId)
                .HasComment("Message ID")
                .HasColumnName("MESSAGE_ID");
            entity.Property(e => e.FolderId)
                .HasComment("Folder ID")
                .HasColumnName("FOLDER_ID");
            entity.Property(e => e.IsDeleted)
                .HasComment("Whether the user has deleted the message")
                .HasColumnName("IS_DELETED");
            entity.Property(e => e.IsRead)
                .HasComment("Whether the message has been read")
                .HasColumnName("IS_READ");
            entity.Property(e => e.Pinned)
                .HasComment("Whether the message is pinned")
                .HasColumnName("PINNED");

            entity.HasOne(d => d.Folder).WithMany(p => p.UserMessages)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_MESS__FOLDE__4BB72C21");

            entity.HasOne(d => d.Message).WithMany(p => p.UserMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_MESS__MESSA__4AC307E8");

            entity.HasOne(d => d.User).WithMany(p => p.UserMessages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_MESSAGES_USERS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
