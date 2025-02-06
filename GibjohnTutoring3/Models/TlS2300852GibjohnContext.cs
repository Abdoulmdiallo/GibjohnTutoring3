using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace GibjohnTutoring3.Models;

public partial class TlS2300852GibjohnContext : DbContext
{
    public TlS2300852GibjohnContext()
    {
    }

    public TlS2300852GibjohnContext(DbContextOptions<TlS2300852GibjohnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Leaderboard> Leaderboards { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       // => optionsBuilder.UseMySql("name=MySqlConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PRIMARY");

            entity.ToTable("answers");

            entity.HasIndex(e => e.QuestionId, "questionID");

            entity.Property(e => e.AnswerId).HasColumnName("answerID");
            entity.Property(e => e.AnswerText)
                .HasMaxLength(255)
                .HasColumnName("answerText");
            entity.Property(e => e.IsCorrect)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isCorrect");
            entity.Property(e => e.QuestionId).HasColumnName("questionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("answers_ibfk_1");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PRIMARY");

            entity.ToTable("comments");

            entity.HasIndex(e => e.CustomerId, "customerID");

            entity.Property(e => e.CommentId).HasColumnName("commentID");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("comments_ibfk_1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Level)
                .HasDefaultValueSql("'1'")
                .HasColumnName("level");
            entity.Property(e => e.NextLevelXp)
                .HasDefaultValueSql("'100'")
                .HasColumnName("next_level_xp");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
            entity.Property(e => e.Xp)
                .HasDefaultValueSql("'0'")
                .HasColumnName("xp");
        });

        modelBuilder.Entity<Leaderboard>(entity =>
        {
            entity.HasKey(e => e.LeaderboardId).HasName("PRIMARY");

            entity.ToTable("leaderboards");

            entity.HasIndex(e => e.CustomerId, "customerID");

            entity.Property(e => e.LeaderboardId).HasColumnName("leaderboardID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Rank).HasColumnName("rank");

            entity.HasOne(d => d.Customer).WithMany(p => p.Leaderboards)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("leaderboards_ibfk_1");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PRIMARY");

            entity.ToTable("questions");

            entity.HasIndex(e => e.QuizId, "quizID");

            entity.Property(e => e.QuestionId).HasColumnName("questionID");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(255)
                .HasColumnName("correctAnswer");
            entity.Property(e => e.QuestionText)
                .HasColumnType("text")
                .HasColumnName("questionText");
            entity.Property(e => e.QuizId).HasColumnName("quizID");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("questions_ibfk_1");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PRIMARY");

            entity.ToTable("quizzes");

            entity.Property(e => e.QuizId).HasColumnName("quizID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Difficulty)
                .HasColumnType("enum('easy','medium','hard')")
                .HasColumnName("difficulty");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
