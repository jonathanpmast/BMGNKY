using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BizManWeb.Data;

namespace BizManWebRC2.Migrations
{
    [DbContext(typeof(BMGContext))]
    [Migration("20160523034921_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BizManWeb.Models.Golfer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Handicap");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TeamID");

                    b.HasKey("ID");

                    b.HasIndex("TeamID");

                    b.ToTable("Golfers");
                });

            modelBuilder.Entity("BizManWeb.Models.GolferMatchScore", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GolferID");

                    b.Property<int>("MatchID");

                    b.Property<int>("Score");

                    b.Property<int?>("SubbedForID");

                    b.HasKey("ID");

                    b.HasIndex("GolferID");

                    b.HasIndex("MatchID");

                    b.HasIndex("SubbedForID");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("BizManWeb.Models.Login", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int?>("GolferID");

                    b.Property<string>("Identifier")
                        .IsRequired();

                    b.Property<int>("LoginType");

                    b.HasKey("ID");

                    b.HasIndex("GolferID");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("BizManWeb.Models.Match", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchRoundID");

                    b.Property<int?>("TeamOneID");

                    b.Property<int?>("TeamTwoID");

                    b.Property<int>("TeeOrder");

                    b.HasKey("ID");

                    b.HasIndex("MatchRoundID");

                    b.HasIndex("TeamOneID");

                    b.HasIndex("TeamTwoID");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("BizManWeb.Models.Round", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("Order");

                    b.HasKey("ID");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("BizManWeb.Models.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("BizManWeb.Models.Golfer", b =>
                {
                    b.HasOne("BizManWeb.Models.Team")
                        .WithMany()
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BizManWeb.Models.GolferMatchScore", b =>
                {
                    b.HasOne("BizManWeb.Models.Golfer")
                        .WithMany()
                        .HasForeignKey("GolferID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BizManWeb.Models.Match")
                        .WithMany()
                        .HasForeignKey("MatchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BizManWeb.Models.Golfer")
                        .WithMany()
                        .HasForeignKey("SubbedForID");
                });

            modelBuilder.Entity("BizManWeb.Models.Login", b =>
                {
                    b.HasOne("BizManWeb.Models.Golfer")
                        .WithMany()
                        .HasForeignKey("GolferID");
                });

            modelBuilder.Entity("BizManWeb.Models.Match", b =>
                {
                    b.HasOne("BizManWeb.Models.Round")
                        .WithMany()
                        .HasForeignKey("MatchRoundID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BizManWeb.Models.Team")
                        .WithMany()
                        .HasForeignKey("TeamOneID");

                    b.HasOne("BizManWeb.Models.Team")
                        .WithMany()
                        .HasForeignKey("TeamTwoID");
                });
        }
    }
}
