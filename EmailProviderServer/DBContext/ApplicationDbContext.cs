using System;
using System.Collections.Generic;
using EmailProviderServer.DBContext.DbEncryption;
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

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageRecipient> MessageRecipients { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

    public virtual DbSet<UserMessageFolder> UserMessageFolders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Future Development will break dynamic searching by keyword
        var encrypt = new EncryptedStringConverter();

        modelBuilder.Entity<Message>()
            .Property(m => m.Body)
            .HasConversion(encrypt);

        modelBuilder.Entity<Message>()
            .Property(m => m.Subject)
            .HasConversion(encrypt);

        modelBuilder.Entity<BulkIncomingMessage>()
            .Property(b => b.RawData)
            .HasConversion(encrypt);

        modelBuilder.Entity<BulkOutgoingMessage>()
            .Property(b => b.RawData)
            .HasConversion(encrypt);

        modelBuilder.Entity<MessageRecipient>()
            .Property(r => r.Email)
            .HasConversion(encrypt);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasConversion(encrypt);

        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumber)
            .HasConversion(encrypt);

        modelBuilder.Entity<File>()
            .Property(f => f.Name)
            .HasConversion(encrypt);
        */


        modelBuilder.Entity<BulkIncomingMessage>(entity =>
        {
            entity.ToTable("BULK_INCOMING_MESSAGES", tb => tb.HasComment("Table for incoming message to be processed"));

            entity.HasIndex(e => e.IncomingMessageId, "UX_BULK_INCOMING_MESSAGES_MESSAGE_ID").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("ID for incoming message to be processed")
                .HasColumnName("ID");
            entity.Property(e => e.IncomingMessageId)
                .HasComment("refrence to message")
                .HasColumnName("INCOMING_MESSAGE_ID");
            entity.Property(e => e.RawData)
                .HasMaxLength(1024)
                .HasComment("Raw data")
                .HasColumnName("RAW_DATA");
            entity.Property(e => e.ReceivedDate)
                .HasComment("Time to be sent")
                .HasColumnType("datetime")
                .HasColumnName("RECEIVED_DATE");

            entity.HasOne(d => d.IncomingMessage).WithOne(p => p.BulkIncomingMessage)
                .HasForeignKey<BulkIncomingMessage>(d => d.IncomingMessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RAW_INCOMING_MESSAGE_MESSAGE_ID");
        });

        modelBuilder.Entity<BulkOutgoingMessage>(entity =>
        {
            entity.ToTable("BULK_OUTGOING_MESSAGES", tb => tb.HasComment("Table for incoming message to be processed"));

            entity.HasIndex(e => e.OutgoingMessageId, "UX_BULK_OUTGOING_MESSAGES_MESSAGE_ID").IsUnique();

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
            entity.Property(e => e.SentDate)
                .HasComment("Time to be sent")
                .HasColumnType("datetime")
                .HasColumnName("SENT_DATE");

            entity.HasOne(d => d.OutgoingMessage).WithOne(p => p.BulkOutgoingMessage)
                .HasForeignKey<BulkOutgoingMessage>(d => d.OutgoingMessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RAW_OUTGOING_MESSAGE_MESSAGE_ID");
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

            entity.HasIndex(e => e.MessageId, "IX_FILES_MESSAGE_ID");

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
            entity.HasKey(e => e.Id).HasName("PK_FOLDERS_ID");

            entity.ToTable("FOLDERS", tb => tb.HasComment("Table for user-defined folders"));

            entity.HasIndex(e => e.UserId, "IX_FOLDERS_USER_ID");

            entity.HasIndex(e => new { e.Name, e.UserId, e.FolderDirection }, "UX_FOLDERS_NAME_USER_ID_FOLDER_DIRECTION").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("ID")
                .HasColumnName("ID");
            entity.Property(e => e.FolderDirection)
                .HasComment("Direction of messages inside the folder")
                .HasColumnName("FOLDER_DIRECTION");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasComment("Name of the folder")
                .HasColumnName("NAME");
            entity.Property(e => e.UserId)
                .HasComment("ID of user who created the folder")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MESSAGES_ID");

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
        });

        modelBuilder.Entity<MessageRecipient>(entity =>
        {
            entity.ToTable("MESSAGE_RECIPIENTS", tb => tb.HasComment("Table for message recipients"));

            entity.HasIndex(e => new { e.MessageId, e.Email }, "UX_MESSAGE_RECIPIENTS_MESSAGE_ID_EMAIL").IsUnique();

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
           entity.Property(e => e.IsOurUser)
                .HasComment("Is the recipient our user")
                .HasColumnName("IS_OUR_USER");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MESSAGE_R__MESSA__7DF8932B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_USERS_ID");

            entity.ToTable("USERS", tb => tb.HasComment("Table for system users"));

            entity.HasIndex(e => e.CountryId, "IX_USERS_COUNTRY_ID");

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
            entity.Property(e => e.Photo)
                .HasComment("Profile photo of the user (binary image)")
                .HasColumnName("PHOTO")
                .HasColumnType("varbinary(max)")
                .IsRequired(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Users).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.ToTable("USER_MESSAGES", tb => tb.HasComment("Table to store per-user message state"));

            entity.HasIndex(e => e.UserId, "IX_USER_MESSAGES_USER_ID");

            entity.HasIndex(e => new { e.UserId, e.MessageId }, "UX_USER_MESSAGES_USER_ID_MESSAGE_ID").IsUnique();

            entity.Property(e => e.Id)
                .HasComment("Identifier")
                .HasColumnName("ID");
            entity.Property(e => e.IsDeleted)
                .HasComment("Whether the user has deleted the message")
                .HasColumnName("IS_DELETED");
            entity.Property(e => e.IsRead)
                .HasComment("Whether the message has been read")
                .HasColumnName("IS_READ");
            entity.Property(e => e.MessageId)
                .HasComment("Message ID")
                .HasColumnName("MESSAGE_ID");
            entity.Property(e => e.UserId)
                .HasComment("User ID")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Message).WithMany(p => p.UserMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_MESS__MESSA__00D4FFD6");

            entity.HasOne(d => d.User).WithMany(p => p.UserMessages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_MESSAGES_USERS");
        });

        modelBuilder.Entity<UserMessageFolder>(entity =>
        {
            entity.ToTable("USER_MESSAGE_FOLDERS", tb => tb.HasComment("Table for incoming message to be processed"));

            entity.HasIndex(e => e.FolderId, "IX_USER_MESSAGE_FOLDERS_FOLDER_ID");

            entity.HasIndex(e => e.UserMessageId, "IX_USER_MESSAGE_FOLDERS_MESSAGE_ID");

            entity.Property(e => e.Id)
                .HasComment("Identifier")
                .HasColumnName("ID");
            entity.Property(e => e.FolderId)
                .HasComment("refrence to message")
                .HasColumnName("FOLDER_ID");
            entity.Property(e => e.UserMessageId)
                .HasComment("Time to be sent")
                .HasColumnName("USER_MESSAGE_ID");

            entity.HasOne(d => d.Folder).WithMany(p => p.UserMessageFolders)
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("FK_USER_MESSAGE_FOLDERS_FOLDERS");

            entity.HasOne(d => d.UserMessage).WithMany(p => p.UserMessageFolders)
                .HasForeignKey(d => d.UserMessageId)
                .HasConstraintName("FK_USER_MESSAGE_FOLDERS_USER_MESSAGE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
