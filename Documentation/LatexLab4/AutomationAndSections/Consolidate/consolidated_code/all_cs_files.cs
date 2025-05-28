
// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Exceptions/UserNotFoundException.cs ====================

using System;

namespace AimReactionAPI.Exceptions {
    public class UserNotFoundException : Exception {
        public UserNotFoundException(string message) : base(message) {

        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Exceptions/PasswordEmptyException.cs ====================

namespace AimReactionAPI.Exceptions
{
    public class PasswordEmptyException : Exception
    {
        public PasswordEmptyException(string message) : base(message) { }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Exceptions/UserAlreadyExistsException.cs ====================

using System;

namespace AimReactionAPI.Exceptions {
    public class UserAlreadyExistsException : Exception {
        public UserAlreadyExistsException(string message) : base(message) {

        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Exceptions/InvalidPasswordException.cs ====================

using System;

namespace AimReactionAPI.Exceptions {
    public class InvalidPasswordException : Exception {
        public InvalidPasswordException(string message) : base(message) {

        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20241214092607_initialize.cs ====================

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameName = table.Column<string>(type: "text", nullable: false),
                    GameDescription = table.Column<string>(type: "text", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "text", nullable: false),
                    TargetSpeed = table.Column<int>(type: "integer", nullable: false),
                    MaxTargets = table.Column<int>(type: "integer", nullable: false),
                    GameDuration = table.Column<int>(type: "integer", nullable: false),
                    GameType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_Targets_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    GameSessionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GameType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.GameSessionId);
                    table.ForeignKey(
                        name: "FK_GameSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    DateAchieved = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReactionTime = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameType = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreId);
                    table.ForeignKey(
                        name: "FK_Scores_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_UserId",
                table: "GameSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_GameId",
                table: "Scores",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_UserId",
                table: "Scores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Targets_GameId",
                table: "Targets",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSessions");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20250321174456_UpdateGame.Designer.cs ====================

﻿// <auto-generated />
using System;
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AimReactionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250321174456_UpdateGame")]
    partial class UpdateGame
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameId"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GameDuration")
                        .HasColumnType("integer");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxTargets")
                        .HasColumnType("integer");

                    b.Property<int>("TargetSpeed")
                        .HasColumnType("integer");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GameId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.Property<int>("GameSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameSessionId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.Property<int>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScoreId"));

                    b.Property<DateTime>("DateAchieved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReactionTime")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("ScoreId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TargetId"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("TargetId");

                    b.HasIndex("GameId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("Scores")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", null)
                        .WithMany("Targets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Navigation("Scores");

                    b.Navigation("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Navigation("GameSessions");

                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20250321175356_AddGameUser.cs ====================

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGameUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameUsers",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => new { x.GameId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GameUsers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameUsers_UserId",
                table: "GameUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUsers");
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20250524102728_AddGlobalMessage.Designer.cs ====================

﻿// <auto-generated />
using System;
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AimReactionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250524102728_AddGlobalMessage")]
    partial class AddGlobalMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameId"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GameDuration")
                        .HasColumnType("integer");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxTargets")
                        .HasColumnType("integer");

                    b.Property<int>("TargetSpeed")
                        .HasColumnType("integer");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GameId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.Property<int>("GameSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameSessionId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameUser", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GlobalMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("GlobalMessages");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.Property<int>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScoreId"));

                    b.Property<DateTime>("DateAchieved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReactionTime")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("ScoreId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TargetId"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("TargetId");

                    b.HasIndex("GameId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameUser", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("GameUsers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GlobalMessage", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("Scores")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", null)
                        .WithMany("Targets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Navigation("GameUsers");

                    b.Navigation("Scores");

                    b.Navigation("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Navigation("GameSessions");

                    b.Navigation("GameUsers");

                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20250524102728_AddGlobalMessage.cs ====================

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGlobalMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_SenderId",
                table: "GlobalMessages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalMessages");
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20241214092607_initialize.Designer.cs ====================

﻿// <auto-generated />
using System;
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AimReactionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241214092607_initialize")]
    partial class initialize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameId"));

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GameDuration")
                        .HasColumnType("integer");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxTargets")
                        .HasColumnType("integer");

                    b.Property<int>("TargetSpeed")
                        .HasColumnType("integer");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.Property<int>("GameSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameSessionId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.Property<int>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScoreId"));

                    b.Property<DateTime>("DateAchieved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReactionTime")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("ScoreId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TargetId"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("TargetId");

                    b.HasIndex("GameId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("Scores")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", null)
                        .WithMany("Targets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Navigation("Scores");

                    b.Navigation("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Navigation("GameSessions");

                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/AppDbContextModelSnapshot.cs ====================

﻿// <auto-generated />
using System;
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AimReactionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameId"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GameDuration")
                        .HasColumnType("integer");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxTargets")
                        .HasColumnType("integer");

                    b.Property<int>("TargetSpeed")
                        .HasColumnType("integer");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GameId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.Property<int>("GameSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameSessionId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameUser", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GlobalMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("GlobalMessages");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.Property<int>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScoreId"));

                    b.Property<DateTime>("DateAchieved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReactionTime")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("ScoreId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TargetId"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("TargetId");

                    b.HasIndex("GameId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameUser", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("GameUsers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GlobalMessage", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("Scores")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", null)
                        .WithMany("Targets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Navigation("GameUsers");

                    b.Navigation("Scores");

                    b.Navigation("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Navigation("GameSessions");

                    b.Navigation("GameUsers");

                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20250321175356_AddGameUser.Designer.cs ====================

﻿// <auto-generated />
using System;
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AimReactionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250321175356_AddGameUser")]
    partial class AddGameUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameId"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GameDuration")
                        .HasColumnType("integer");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxTargets")
                        .HasColumnType("integer");

                    b.Property<int>("TargetSpeed")
                        .HasColumnType("integer");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GameId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.Property<int>("GameSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GameSessionId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameUser", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("GameId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.Property<int>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScoreId"));

                    b.Property<DateTime>("DateAchieved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReactionTime")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("ScoreId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.Property<int>("TargetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TargetId"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("TargetId");

                    b.HasIndex("GameId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameSession", b =>
                {
                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.GameUser", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("GameUsers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("GameUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Score", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", "Game")
                        .WithMany("Scores")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AimReactionAPI.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AimReactionAPI.Models.Target", b =>
                {
                    b.HasOne("AimReactionAPI.Models.Game", null)
                        .WithMany("Targets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AimReactionAPI.Models.Game", b =>
                {
                    b.Navigation("GameUsers");

                    b.Navigation("Scores");

                    b.Navigation("Targets");
                });

            modelBuilder.Entity("AimReactionAPI.Models.User", b =>
                {
                    b.Navigation("GameSessions");

                    b.Navigation("GameUsers");

                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Migrations/20250321174456_UpdateGame.cs ====================

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Visibility",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreatorId",
                table: "Games",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_CreatorId",
                table: "Games",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_CreatorId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreatorId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Games");
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Controllers/LeaderboardController.cs ====================

using AimReactionAPI.Data;
using AimReactionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AimReactionAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public LeaderboardController(AppDbContext context)
    {
        _context = context;
    }

    // Endpoint to get all users and their scores
    [HttpGet("all-users")]
    public async Task<ActionResult<IEnumerable<object>>> GetAllUsersScores()
    {
        var allUsersScores = await _context.Scores
            .OrderByDescending(s => s.Value)
            .Select(s => new
            {
                s.UserId,
                s.Value,
                User = _context.Users.FirstOrDefault(u => u.UserId == s.UserId)
            })
            .ToListAsync();

        var result = allUsersScores.Select(s => new 
        {
            UserId = s.UserId,
            UserName = s.User?.Name, 
            UserEmail = s.User?.Email,
            Score = s.Value
        });

        return Ok(result);
    }

    // Endpoint to get top N scores
    [HttpGet("top-scores/{topCount}")]
    public async Task<ActionResult<IEnumerable<object>>> GetTopScores(int topCount, GameType gameType)
    {
        var topScores = await _context.Scores
            .Where(s => s.GameType == gameType)
            .OrderByDescending(s => s.Value)
            .Take(topCount)
            .Select(s => new
            {
                s.UserId,
                s.Value,
                s.DateAchieved,
                s.GameType,
                User = _context.Users.FirstOrDefault(u => u.UserId == s.UserId)
            })
            .ToListAsync();

        var result = topScores.Select(s => new
        {
            UserId = s.UserId,
            UserName = s.User?.Name,
            UserEmail = s.User?.Email,
            Score = s.Value,
            DateAchieved = s.DateAchieved,
            GameType = s.GameType
        });

        return Ok(result);
    }

    [HttpGet("User-Top-Score/{userId}")]
    public async Task<ActionResult<object>> GetUserHighScore(int userId) {
        var userHighScore = await _context.Scores
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.Value)
            .FirstOrDefaultAsync();

        if (userHighScore == null) {
            return NotFound("No scores found for the user.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        var result = new {
            UserId = user.UserId,
            UserName = user.Name,
            UserEmail = user.Email,
            HighScore = userHighScore.Value,
            DateAchieved = userHighScore.DateAchieved,
            gameId = userHighScore.GameId,
            GameType = userHighScore.GameType
        };
        return Ok(result);
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Controllers/MessageController.cs ====================

using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly MessageService _messageService;
    public MessageController(MessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("global")]
    public async Task<ActionResult<List<GlobalMessageResponse>>> GetGlobalMessages([FromQuery(Name = "user-id")] int userId)
    {
        try
        {
            return await _messageService.GetGlobalMessages(userId);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not authorized");
        }
        catch (Exception)
        {
            return StatusCode(500, $"Unexpected error occurred");
        }
    }

    [HttpGet("room")]
    public async Task<ActionResult<List<GameRoomMessageResponse>>> GetGameRoomMessages(
        [FromQuery(Name = "user-id")] int userId, 
        [FromQuery(Name = "room-id")] Guid roomId)
    {
        try
        {
            return await _messageService.GetGameRoomMessages(userId, roomId);
        }
         catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not authorized");
        }
        catch (Exception)
        {
            return StatusCode(500, $"Unexpected error occurred");
        }
    }
}



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Controllers/AuthController.cs ====================

﻿using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using AimReactionAPI.Services;
using AimReactionAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AppDbContext context, AuthService authService, ILogger<AuthController> logger)
    {
        _context = context;
        _authService = authService;
        _logger = logger;
    }


    // Serilog, nlog?????
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserRegisterDto userDto)
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
            {
                throw new UserAlreadyExistsException("Email is already registered.");
            }

            if (string.IsNullOrEmpty(userDto.Password))
            {
                throw new PasswordEmptyException("Password cannot be empty.");
            }

            var hashedPassword = _authService.HashPassword(userDto.Password);

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);

        }
        catch (UserAlreadyExistsException ex)
        {
            _logger.LogError(ex, "User Registration failed: {Email}", userDto.Email);
            return BadRequest(ex.Message);
        }
        catch (PasswordEmptyException ex)
        {
            _logger.LogError(ex, "Password is empty for email: {Email}", userDto.Email);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during registration for email: {Email}", userDto.Email);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException("Invalid email or password");
            }

            var isValidPassword = _authService.VerifyPassword(loginDto.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                throw new InvalidPasswordException("Invalid email or password");
            }

            return Ok(user.UserId);
        }
        catch (UserNotFoundException ex)
        {
            _logger.LogWarning(ex, "User not found during login attempt for email {Email}.", loginDto.Email);
            return Unauthorized(ex.Message);
        }
        catch (InvalidPasswordException ex)
        {
            _logger.LogWarning(ex, "Invalid password during login attempt for email {Email}.", loginDto.Email);
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during login for email {Email}.", loginDto.Email);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
}



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Controllers/GenericGameController.cs ====================

﻿using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenericGameController : ControllerBase
{
    private readonly GameSessionHandler<GameType> _gameSessionHandler;
    private readonly AppDbContext _context;

    public GenericGameController(GameSessionHandler<GameType> gameSessionHandler, AppDbContext context)
    {
        _gameSessionHandler = gameSessionHandler;
        _context = context;
    }

    [HttpPost("{userId}/start/{gameType}")]
    public async Task<IActionResult> StartGameSession(int userId, GameType gameType)
    {
        var session = await _gameSessionHandler.StartSessionAsync(userId, gameType);
        return Ok(session);
    }

    [HttpPost("end/{sessionId}")]
    public async Task<IActionResult> EndGameSession(int sessionId)
    {
        var duration = await _gameSessionHandler.EndSessionAsync(sessionId);
        return Ok(duration);
    }

    [HttpGet("games")]
    public async Task<ActionResult> GetAllGames([FromQuery] int userId)
    {
        var games = await _context.Games
            .Where(g =>
                g.CreatorId == userId ||
                g.Visibility == GameVisibility.PUBLIC ||
                _context.GameUsers.Any(gu => gu.GameId == g.GameId && gu.UserId == userId)
            )
            .Select(g => new MiniGameDto
            {
                GameId = g.GameId,
                CreatorId = g.CreatorId,
                GameDescription = new GameDescription(g.GameName, g.GameDescription, g.GameType),
                GameDifficulty = g.DifficultyLevel
            })
            .ToListAsync();

        return Ok(games);
    }

    [HttpGet("games/{id}")]
    public async Task<ActionResult<GameDto>> GetGameById(int id, [FromQuery] int userId)
    {
        Game? game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        if (!HasAccess(game, userId))
        {
            return Unauthorized("Unauthorized access");
        }

        List<int> allowedUsers = _context.GameUsers
                                    .Where(gu => gu.GameId == game.GameId)
                                    .Select(gu => gu.UserId)
                                    .ToList();

        var gameDto = new GameDto
        {
            GameId = game.GameId,
            Name = game.GameName,
            Description = game.GameDescription,
            DifficultyLevel = game.DifficultyLevel,
            TargetSpeed = game.TargetSpeed,
            MaxTargets = game.MaxTargets,
            GameDuration = game.GameDuration,
            GameType = game.GameType,
            AllowedUsers = allowedUsers,
            CreatorId = game.CreatorId,
            Visibility = game.Visibility
        };

        return Ok(gameDto);
    }

    [HttpGet("games/{id}/targets")]
    public async Task<ActionResult<IEnumerable<Target>>> GetGameTargets(int id)
    {
        Game? game = await _context.Games
            .Include(g => g.Targets)
            .FirstOrDefaultAsync(g => g.GameId == id);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        return Ok(game.Targets);
    }

    [HttpDelete("games/{id}")]
    public async Task<IActionResult> DeleteGame(int id, [FromQuery] int userId)
    {
        Game? game = await _context.Games.FindAsync(id);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        if (!HasAccess(game, userId))
        {
            return Unauthorized("Unauthorized access");
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{userId}/addscore")]
    public async Task<IActionResult> AddScore([FromRoute] int userId, [FromBody] AddScoreDto scoreDto)
    {
        Console.WriteLine($"Received gameId: {scoreDto.GameId}, userId: {userId}"); ;
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == scoreDto.GameId);
        if (game == null)
        {
            return NotFound("Game not found");
        }

        _context.Attach(game);

        var newScore = new Score
        {
            Value = scoreDto.Value,
            DateAchieved = scoreDto.DateAchieved,
            GameId = scoreDto.GameId,
            GameType = scoreDto.GameType,
            UserId = userId
        };

        _context.Scores.Add(newScore);
        await _context.SaveChangesAsync();

        return Ok(newScore);
    }

    [HttpGet("active")]
    public IActionResult GetActiveSessionCount()
    {
        var activeCount = _gameSessionHandler.GetActiveSessionCount();
        return Ok(new { activeSessions = activeCount });
    }


    private bool HasAccess(Game game, int userId)
    {
        return game.CreatorId == userId ||
            game.Visibility == GameVisibility.PUBLIC ||
            _context.GameUsers.Any(gu => gu.GameId == game.GameId && gu.UserId == userId);
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Controllers/UsersController.cs ====================

using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserService _userService;
    public UsersController(AppDbContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }


    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] int userId)
    {
        return await _userService.GetUsers(userId);
    }
}



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Controllers/GameConfigController.cs ====================

﻿using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using AimReactionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameConfigController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<GameConfigController> _logger;
    private readonly GameService _gameService;

    public GameConfigController(AppDbContext context, ILogger<GameConfigController> logger, GameService gameService)
    {
        _context = context;
        _logger = logger;
        _gameService = gameService;
    }


    // PUT api/gameconfig
    [HttpPut]
    public async Task<IActionResult> CreateOrUpdateGame([FromBody] GameConfigDto gameConfig)
    {
        if (gameConfig == null)
        {
            return BadRequest("Invalid game configuration data.");
        }
        try
        {
            Game? game = await _gameService.CreateOrUpdateGameAsync(gameConfig);

            if (game == null)
            {
                return StatusCode(500, "Operation failed.");
            }

            return Ok(game);
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(400, ex.Message);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving game configuration.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Controllers/TargetController.cs ====================

﻿using AimReactionAPI.Models;
using AimReactionAPI.Services;
using AimReactionAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AimReactionAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TargetController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<TargetController> _logger;

    public TargetController(AppDbContext context, ILogger<TargetController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Target>>> GetAllTargets()
    {
        return await _context.Targets.ToListAsync();  // Retrieve all targets
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Target>> GetTargetById(int id)
    {
        Target? target = await _context.Targets.FindAsync(id);

        if (target == null)
        {
            return NotFound("Target not found");
        }

        return target;
    }

    [HttpGet("filterBySpeed/{speedThreshold}")]
    public async Task<ActionResult<IEnumerable<Target>>> GetTargetsBySpeed(int speedThreshold)
    {
        var targets = await _context.Targets.ToListAsync();

        var filteredTargets = targets.FilterTargetsBySpeed(speedThreshold).ToList();

        return Ok(filteredTargets);
    }

    [HttpPost]
    public async Task<ActionResult<Target>> AddTarget(Target target)
    {
        // Validate and add the new target
        _context.Targets.Add(target);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTargetById), new { id = target.TargetId }, target);
    }

    [HttpDelete("{gameId}/targets/{id}")]
    public async Task<IActionResult> DeleteTarget(int gameId, int id)
    {
        var game = await _context.Games
        .Include(g => g.Targets)
        .FirstOrDefaultAsync(g => g.GameId == gameId);

        if (game == null)
        {
            return NotFound("Game not found");
        }

        Target? targetToDelete = null;

        foreach (var target in game)
        {
            if (target.TargetId == id)
            {
                targetToDelete = target;
                break;
            }
        }

        if (targetToDelete == null)
        {
            return NotFound("Target not found");
        }

        _context.Targets.Remove(targetToDelete);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Events/SendGlobalMessageEvent.cs ====================

using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class SendGlobalMessageEvent : BaseEventHandler<GlobalMessageRequest>
{
    private readonly IServiceScopeFactory _scopeFactory;
    public SendGlobalMessageEvent(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public override async Task Handle(GlobalMessageRequest dto, IWebSocketConnection socket)
    {
        using var scope = _scopeFactory.CreateScope();
        var messageService = scope.ServiceProvider.GetRequiredService<MessageService>();
        await messageService.SendGlobalMessage(dto);
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Events/SendGameRoomMessageEvent.cs ====================

using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class SendGameRoomMessageEvent : BaseEventHandler<GameRoomMessageRequest>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SendGameRoomMessageEvent(IServiceScopeFactory scopeFactory)
    {
       _scopeFactory = scopeFactory;
    }
    public override async Task Handle(GameRoomMessageRequest dto, IWebSocketConnection socket)
    {
        using var scope = _scopeFactory.CreateScope();
        var messageService = scope.ServiceProvider.GetRequiredService<MessageService>();
        await messageService.SendGameRoomMessage(dto);
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Events/StartRoomEvent.cs ====================

using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class StartRoomEvent : BaseEventHandler<StartRoomRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public StartRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(StartRoomRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.StartRoom(dto.PlayerId, dto.RoomId);
        return Task.CompletedTask;
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Events/CreateRoomEvent.cs ====================

using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class CreateRoomEvent : BaseEventHandler<CreateRoomRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public CreateRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(CreateRoomRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.CreateRoom(dto.PlayerId, dto.RoomName, dto.Visibility, dto.AllowedPlayers);
        return Task.CompletedTask;
    }
}



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Events/JoinRoomEvent.cs ====================

using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;


public class JoinRoomEvent : BaseEventHandler<JoinRoomRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public JoinRoomEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(JoinRoomRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.JoinRoom(dto.PlayerId, dto.RoomId);
        return Task.CompletedTask;
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Events/HitTargetEvent.cs ====================

using AimReactionAPI.DTOs;
using AimReactionAPI.Services;
using Fleck;
using WebSocketBoilerplate;

namespace AimReactionAPI.Events;

public class HitTargetEvent : BaseEventHandler<HitTargetRequest>
{
    private readonly MultiplayerService _multiplayerService;

    public HitTargetEvent(MultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    public override Task Handle(HitTargetRequest dto, IWebSocketConnection socket)
    {
        _multiplayerService.RegisterTargetHit(dto.PlayerId, dto.RoomId, dto.ReactionTime);
        return Task.CompletedTask;
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/GameType.cs ====================

﻿namespace AimReactionAPI.Models;

public enum GameType
{
    MovingTargets,
    ReflexTest,
    ReactionTimeChallenge,
    CustomChallenge
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/GlobalMessage.cs ====================

namespace AimReactionAPI.Models;

public class GlobalMessage
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual User? Sender { get; set; }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/Target.cs ====================

﻿using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models;

public class Target
{
    public int TargetId { get; set; }
    public int Size { get; set; }
    public int Speed { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public int GameId { get; set; }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/GameSession.cs ====================

﻿namespace AimReactionAPI.Models;

public class GameSession
{
    public int GameSessionId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public GameType GameType { get; set; }

    public TimeSpan GetDuration()
    {
        return EndTime - StartTime;
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/GameUser.cs ====================

using System.Collections;

namespace AimReactionAPI.Models;

public class GameUser
{
    public int GameId { get; set; }
    public int UserId { get; set; }
    public virtual Game? Game { get; set; }
    public virtual User? User { get; set; }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/Score.cs ====================

﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models;

public record Score
{
    public int ScoreId { get; set; }

    private int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Score value cannot be negative.");
            }
            _value = value; 
        }
    }

    public DateTime DateAchieved { get; set; }
    public int ReactionTime { get; set; }

    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }

    public GameType GameType { get; set; }
    public int GameId { get; set; }

    [JsonIgnore]
    public Game Game { get; set; }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/Room.cs ====================

using AimReactionAPI.Services;

namespace AimReactionAPI.Models;

public enum RoomStatus
{
    WAITING,
    PLAYING
}

public class Room
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public int CreatorId { get; set; }
    public HashSet<int> Players { get; set; }
    public HashSet<int> AllowedPlayers { get; set;}
    public Dictionary<int, double> PlayerTimes { get; set; } = [];
    public RoomStatus RoomStatus { get; set; } 
    public GameVisibility RoomVisibility { get; set; } 

    public Room(Guid id, int creatorId, string roomName, GameVisibility visibility, HashSet<int> allowedPlayers)
    {
        Id = id;
        CreatorId = creatorId;
        Name = roomName;
        RoomVisibility = visibility;
        Players = [creatorId]; 
        AllowedPlayers = allowedPlayers ?? [];
        AllowedPlayers.Add(CreatorId); 
    }

    public bool AddToRoom(int userId)
    {
        if (RoomStatus == RoomStatus.WAITING)
        {
            return Players.Add(userId);
        }
        return false;
    }
    public bool RemoveFromRoom(int userId)
    {
        return Players.Remove(userId);
    }
    public void RegisterPlayerHit(int userId, double reactionTime)
    {
        PlayerTimes.Add(userId, reactionTime);
    }
    public void ResetPlayerTimes()
    {
        PlayerTimes.Clear();
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/GameDescription.cs ====================

﻿
namespace AimReactionAPI.Models
{
    public class GameDescription
    {
        public GameDescription(string gameName, string gameDescr, GameType gameType)
        {
            GameName = gameName;
            GameDescr = gameDescr;
            GameType = gameType;
        }

        public string GameName { get; private set; }
        public string GameDescr { get; private set; }
        public GameType GameType {get; private set;}
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/Game.cs ====================

﻿using System.Collections;

namespace AimReactionAPI.Models;

public class Game : IEnumerable<Target>
{
    public int GameId { get; set; }
    public string GameName { get; set; }
    public string GameDescription { get; set; }
    public string DifficultyLevel { get; set; }
    public int TargetSpeed { get; set; }
    public int MaxTargets { get; set; }
    public int GameDuration { get; set; }
    public int CreatorId { get; set; }
    public GameVisibility Visibility { get; set; }
    public GameType GameType { get; set; }
    public ICollection<Target> Targets { get; set; }
    public ICollection<Score> Scores { get; set; }
    public virtual User Creator { get; set; }
    public virtual ICollection<GameUser> GameUsers { get; set; }
    public Game()
    {
        Targets = new List<Target>();
    }

    public IEnumerator<Target> GetEnumerator()
    {
        return Targets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/GameVisibility.cs ====================

namespace AimReactionAPI.Models;

public enum GameVisibility
{
    PUBLIC,
    PRIVATE
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/Player.cs ====================

using Fleck;

namespace AimReactionAPI.Models;

public class Player(string username, IWebSocketConnection connection)
{
    public string Username { get; set; } = username;
    public IWebSocketConnection Connection { get; set; } = connection;
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Models/User.cs ====================

﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public ICollection<Score> Scores { get; set; } = new List<Score>();
    [JsonIgnore]
    public ICollection<GameSession> GameSessions { get; set; }
    public virtual ICollection<GameUser> GameUsers { get; set; }

}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Extensions/GameCollectionExtensions.cs ====================

﻿using AimReactionAPI.Models;

namespace AimReactionAPI.Extensions;

public static class GameCollectionExtensions
{
    public static IEnumerable<Target> FilterTargetsBySpeed(this List<Target> targets, int speedThreshold)
    {
        return targets.Where(t => t.Speed >= speedThreshold);
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Data/AppDbContext.cs ====================

﻿using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Target> Targets { get; set; }
    public DbSet<GameUser> GameUsers { get; set; }
    public DbSet<GlobalMessage> GlobalMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
                    .HasKey(g => g.GameId);

        modelBuilder.Entity<Game>()
                    .HasMany(g => g.Targets)
                    .WithOne()
                    .HasForeignKey(t => t.GameId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Game>()
                    .Property(g => g.GameType)
                    .HasConversion<string>();

        modelBuilder.Entity<Game>()
                    .Property(g => g.Visibility)
                    .HasConversion<string>();

        modelBuilder.Entity<Game>()
            .HasOne(g => g.Creator)
            .WithMany()
            .HasForeignKey(g => g.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
                    .HasMany(u => u.Scores)
                    .WithOne(s => s.User)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
                    .HasOne(s => s.Game)
                    .WithMany(g => g.Scores)
                    .HasForeignKey(s => s.GameId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
                    .HasOne(s => s.User)
                    .WithMany(u => u.Scores)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
                    .Property(s => s.GameType)
                    .HasConversion<string>();

        modelBuilder.Entity<GameSession>()
                    .HasOne(gs => gs.User)
                    .WithMany(u => u.GameSessions)
                    .HasForeignKey(gs => gs.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameSession>()
                    .Property(gs => gs.GameType)
                    .HasConversion<string>();

        modelBuilder.Entity<GameUser>()
          .HasKey(gu => new { gu.GameId, gu.UserId });

        modelBuilder.Entity<GameUser>()
            .HasOne(gu => gu.Game)
            .WithMany(g => g.GameUsers)
            .HasForeignKey(gu => gu.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameUser>()
            .HasOne(gu => gu.User)
            .WithMany(g => g.GameUsers)
            .HasForeignKey(gu => gu.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GlobalMessage>()
            .HasOne(gm => gm.Sender)
            .WithMany() 
            .HasForeignKey(gm => gm.SenderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/RoomPlayerDto.cs ====================

namespace AimReactionAPI.DTOs;

public record RoomPlayerDto(string Name, int Id, double ReactionTime);


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/GameRoomMessageResponse.cs ====================

namespace AimReactionAPI.DTOs;

public class GameRoomMessageResponse(Guid gameRoomId, string content, string sender, DateTime createdAt) : MessageResponse(content, sender, createdAt)
{
    public Guid GameRoomId { get; set; } = gameRoomId;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/JoinRoomRequest.cs ====================

using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;
public class JoinRoomRequest(int playerId, Guid roomId) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public required Guid RoomId { get; set; } = roomId;
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/MiniGameDto.cs ====================

using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class MiniGameDto
{
    public int GameId { get; set; }
    public int CreatorId { get; set; }
    public GameDescription GameDescription { get; set; }
    public string GameDifficulty { get; set; }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/GameDto.cs ====================

﻿using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class GameDto
{
    public int GameId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DifficultyLevel { get; set; }
    public int TargetSpeed { get; set; }
    public int MaxTargets { get; set; }
    public int CreatorId { get; set; }
    public int GameDuration { get; set; }
    public GameType GameType { get; set; }
    public GameVisibility Visibility { get; set; }
    public required List<int> AllowedUsers { get; set; }

}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/RoomResponse.cs ====================

using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class RoomResponse(Guid id, string name, int creatorId, List<string> players, string roomStatus) : BaseDto
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public int CreatorId { get; set; } = creatorId;
    public List<string> Players { get; set; } = players;
    public string RoomStatus { get; set; } = roomStatus;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/GlobalMessageRequest.cs ====================

namespace AimReactionAPI.DTOs;

public class GlobalMessageRequest(int senderId, string content) : MessageRequest(senderId, content)
{
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/RoomRoundResultsResponse.cs ====================


using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class RoomRoundResultsResponse(List<RoomPlayerDto> remainingPlayers, List<RoomPlayerDto> eliminatedPlayers) : BaseDto
{
    public List<RoomPlayerDto> RemainingPlayers { get; init; } = remainingPlayers ?? [];
    public List<RoomPlayerDto> EliminatedPlayers { get; init; } = eliminatedPlayers ?? [];
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/TargetResponse.cs ====================

using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class TargetResponse(int x, int y) : BaseDto
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/CreateRoomRequest.cs ====================

using AimReactionAPI.Models;
using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;

public class CreateRoomRequest(int playerId, string roomName,
    GameVisibility visibility, HashSet<int> allowedPlayers) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public required string RoomName { get; set; } = roomName;
    public GameVisibility Visibility { get; set; } = visibility;
    public HashSet<int> AllowedPlayers { get; set; } = allowedPlayers;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/HitTargetRequest.cs ====================

using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;
public class HitTargetRequest(int playerId, Guid roomId, double reactionTime) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public required Guid RoomId { get; set; } = roomId;
    public double ReactionTime { get; set; } = reactionTime;
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/UserDto.cs ====================

namespace AimReactionAPI.DTOs;

public record UserDto(string Name, int Id);


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/UserRegisterDto.cs ====================

﻿namespace AimReactionAPI.DTOs;

public class UserRegisterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/LoginDto.cs ====================

﻿namespace AimReactionAPI.DTOs;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/MessageRequest.cs ====================

using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class MessageRequest(int sender, string content) : BaseDto
{
    public int SenderId { get; set; } = sender;
    public string Content { get; set; } = content;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/StartRoomRequest.cs ====================

using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;
public class StartRoomRequest(int playerId, Guid roomId) : BaseDto
{
    public int PlayerId { get; set; } = playerId;
    public Guid RoomId { get; set; } = roomId;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/AvailableRoomsResponse.cs ====================

using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class AvailableRoomsResponse(List<RoomResponse> rooms) : BaseDto
{
    public List<RoomResponse> Rooms { get; set; } = rooms;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/GameConfigDto.cs ====================

﻿using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class GameConfigDto
{
    public int? GameId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DifficultyLevel { get; set; }
    public int TargetSpeed { get; set; }
    public int MaxTargets { get; set; }
    public int CreatorId { get; set; }
    public int GameDuration { get; set; }
    public GameType GameType { get; set; }
    public GameVisibility Visibility { get; set; }
    public required List<int> AllowedUsers { get; set; }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/MessageResponse.cs ====================

using System.Text.Json.Serialization;
using WebSocketBoilerplate;

namespace AimReactionAPI.DTOs;

public class MessageResponse(string content, string sender, DateTime createdAt) : BaseDto
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = content;
    [JsonPropertyName("sender")]
    public string Sender { get; set; } = sender;
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = createdAt;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/AddScoreDto.cs ====================

﻿using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class AddScoreDto
{
    public int GameId { get; set; }
    public GameType GameType { get; set; }
    public DateTime DateAchieved { get; set; }
    public int Value { get; set; }
}



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/GlobalMessageResponse.cs ====================

namespace AimReactionAPI.DTOs;

public class GlobalMessageResponse(string content, string sender, DateTime createdAt) : MessageResponse(content, sender, createdAt)
{
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/GameRoomMessageRequest.cs ====================

namespace AimReactionAPI.DTOs;

public class GameRoomMessageRequest(Guid gameRoomId, int senderId, string content) : MessageRequest(senderId, content)
{
    public Guid GameRoomId { get; set; } = gameRoomId;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/DTOs/CreateRoomResponse.cs ====================

using WebSocketBoilerplate;
namespace AimReactionAPI.DTOs;

public class CreateRoomResponse(Guid roomId) : BaseDto
{
    public Guid RoomId { get; set; } = roomId;
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/MultiplayerService.cs ====================

using System.Collections.Concurrent;
using System.Text.Json;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Fleck;
using Microsoft.IdentityModel.Tokens;

namespace AimReactionAPI.Services;

public class MultiplayerService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<int, Player> _players = new();
    private readonly ConcurrentDictionary<Guid, Room> _rooms = new();
    private readonly ILogger<MultiplayerService> _logger;
    private const int ROUND_DURATION_SECONDS = 5;
    private const int UI_UPDATE_DURATION_SECONDS = 5;
    public MultiplayerService(IServiceProvider serviceProvider, ILogger<MultiplayerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Connect(int playerId, IWebSocketConnection ws)
    {
        _logger.LogInformation($"player({playerId}) is connecting");
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<UserService>();
        User? user = await userService.FindUser(playerId) ??
            throw new InvalidDataException($"User with ID {playerId} not found.");

        Player player = new(user.Name, ws);
        _players.TryAdd(playerId, player);
        SendMessageToPlayer(playerId, JsonSerializer.Serialize(new AvailableRoomsResponse(GetJoinableRooms(playerId))));
        _logger.LogInformation($"player({playerId}) connected");
    }

    public void CreateRoom(int playerId, string roomName, GameVisibility visibility, HashSet<int> playersWithAccess)
    {
        _logger.LogInformation($"player({playerId}) is creating room({roomName})");
        if (!_players.ContainsKey(playerId))
        {
            throw new InvalidDataException($"User {playerId} not found.");
        }
        ValidateCanJoin(playerId);  
        Guid roomGuid = Guid.NewGuid();
        Room room = new(roomGuid, playerId, roomName, visibility, playersWithAccess);
        _rooms.TryAdd(roomGuid, room);
        SendMessageToPlayer(playerId, JsonSerializer.Serialize(GetRoomResponse(room)));
        BroadcastJoinableGames();
        _logger.LogInformation($"player({playerId}) created room({roomName})");
    }

    public void JoinRoom(int playerId, Guid roomId)
    {
        _logger.LogInformation($"player({playerId}) is joining room({roomId})");
        if (!_players.ContainsKey(playerId) ||
            !_rooms.TryGetValue(roomId, out var room))
        {
            throw new InvalidDataException($"User {playerId} Or room {roomId} not found");
        }
        ValidateCanJoin(playerId, roomId);
        room.AddToRoom(playerId);
        BroadcastToRoom(room, JsonSerializer.Serialize(GetRoomResponse(room)));
        _logger.LogInformation($"player({playerId}) joined room({roomId})");
    }
    public void StartRoom(int playerId, Guid roomId)
    {
        _logger.LogInformation($"player({playerId}) is starting room({roomId})");
        if (!_rooms.TryGetValue(roomId, out var room))
        {
            throw new InvalidDataException($"Room {roomId} not found");
        }
        if (room.CreatorId != playerId)
        {
            throw new InvalidOperationException($"User {playerId} not allowed to start.");
        }
        if (room.Players.Count < 2)
        {
            throw new InvalidOperationException($"Minimum 2 players required(Room {roomId}).");
        }
        room.RoomStatus = RoomStatus.PLAYING;
        StartRound(room);
        _logger.LogInformation($"player({playerId}) started room({roomId})");
    }

    public void RegisterTargetHit(int playerId, Guid roomId, double reactionTime)
    {
        _logger.LogInformation($"registering hit for player({playerId}) in room({roomId})");
        if (!_rooms.TryGetValue(roomId, out var room) ||
            !room.Players.Contains(playerId))
        {
            throw new InvalidDataException($"User {playerId} not in room Or room {roomId} not found");
        }
        if (room.RoomStatus != RoomStatus.PLAYING)
        {
            throw new InvalidOperationException($"Room {roomId} is not in a playing state.");
        }
        room.RegisterPlayerHit(playerId, reactionTime);
        _logger.LogInformation($"registered hit for player({playerId}) in room({roomId})");

    }

    public List<RoomResponse> GetJoinableRooms(int playerId)
    {
        return _rooms.Values
        .Where(room => room.RoomStatus == RoomStatus.WAITING &&
                (room.RoomVisibility == GameVisibility.PUBLIC ||
                (room.RoomVisibility == GameVisibility.PRIVATE &&
                room.AllowedPlayers.Contains(playerId)))
            )
        .Select(room => new RoomResponse(
            room.Id,
            room.Name,
            room.CreatorId,
            room.Players
                .Select(id => _players[id].Username)
                .ToList(),
            room.RoomStatus.ToString()))
        .ToList();
    }

    public void Disconnect(int playerId)
    {
        _logger.LogInformation($"player({playerId}) is disconnecting");
        if (!_players.TryRemove(playerId, out var player))
        {
            return;
        }
        foreach (var room in _rooms)
        {
            room.Value.RemoveFromRoom(playerId);
            if (room.Value.Players.Count == 0)
            {
                _rooms.TryRemove(room);
            }
        }
        player.Connection.Close();
        _logger.LogInformation($"player({playerId}) disconnected");
    }

    public bool TryGetRoom(Guid roomId, out Room? room)
    {
        return _rooms.TryGetValue(roomId, out room);
    }

    public void Broadcast(string message)
    {
        foreach (var (playerId, _) in _players)
        {
            SendMessageToPlayer(playerId, message);
        }
    }
    public void BroadcastToRoom(Room room, string message)
    {
        foreach (var playerId in room.Players)
        {
            SendMessageToPlayer(playerId, message);
        }
    }

    private void ValidateCanJoin(int playerId, Guid roomId = new())
    {
        foreach (var (id, room) in _rooms)
        {
            if (room.Players.Contains(playerId) && id != roomId)
            {
                throw new InvalidOperationException($"User {playerId} is already in the room {id}");
            }
        }
    }

    private RoomResponse GetRoomResponse(Room room)
    {
        return new RoomResponse(
            room.Id,
            room.Name,
            room.CreatorId,
            room.Players
                .Select(id => _players[id].Username)
                .ToList(),
            room.RoomStatus.ToString());
    }

    private void BroadcastRoundResults(Room room, HashSet<int> eliminatedPlayerIds)
    {
        List<RoomPlayerDto> eliminatedPlayers = _players.Where(p => eliminatedPlayerIds.Contains(p.Key))
                    .Select(p => new RoomPlayerDto(p.Value.Username, p.Key, room.PlayerTimes.GetValueOrDefault(p.Key)))
                    .ToList();
        List<RoomPlayerDto> remainingPlayers = _players.Where(p => room.Players.Contains(p.Key) && !eliminatedPlayerIds.Contains(p.Key))
                    .Select(p => new RoomPlayerDto(p.Value.Username, p.Key, room.PlayerTimes.GetValueOrDefault(p.Key)))
                    .ToList();
        var results = new RoomRoundResultsResponse(remainingPlayers, eliminatedPlayers);
        var serializedResults = JsonSerializer.Serialize(results);
        BroadcastToRoom(room, serializedResults);
    }

    private void SendMessageToPlayer(int playerId, string message)
    {
        if (_players.TryGetValue(playerId, out var player))
        {
            player.Connection.Send(message);
        }
    }

    private async void HandleRoundEnd(Room room)
    {
        _logger.LogInformation($"round of room({room.Id}) ended");

        var eliminatedPlayers = room.Players
            .Where(playerId => !room.PlayerTimes.ContainsKey(playerId))
            .ToHashSet();
        if (eliminatedPlayers.Count == 0)
        {
            var slowestPlayer = room.PlayerTimes
                        .OrderByDescending(p => p.Value)
                        .FirstOrDefault();
            eliminatedPlayers.Add(slowestPlayer.Key);
        }
        _logger.LogInformation($"eliminated player ids: {string.Join(", ", eliminatedPlayers)}");

        BroadcastRoundResults(room, eliminatedPlayers);
        await Task.Delay(UI_UPDATE_DURATION_SECONDS * 1000); 
        foreach (var player in eliminatedPlayers)
        {
            room.RemoveFromRoom(player);
        }
        if (room.Players.Count > 1)
        {
            StartRound(room);
        }
        else
        {
            _rooms.TryRemove(room.Id, out var _);
        }
    }

    private void CreateAndBroadcastTargetToRoom(Room room, Target target)
    {
        var targetDto = new TargetResponse(target.X, target.Y);
        var serializedTarget = JsonSerializer.Serialize(targetDto);
        BroadcastToRoom(room, serializedTarget);
    }

    private void BroadcastJoinableGames()
    {
        foreach (var (playerId, _) in _players)
        {
            var joinableRooms = GetJoinableRooms(playerId);
            var response = new AvailableRoomsResponse(joinableRooms);
            if (!joinableRooms.IsNullOrEmpty())
                SendMessageToPlayer(playerId, JsonSerializer.Serialize(response));
        }
    }
  
    private void StartRound(Room room)
    {
        _logger.LogInformation($"round of room({room.Id}) started");
        room.ResetPlayerTimes();
        Target target = TargetService.GenerateTarget();
        CreateAndBroadcastTargetToRoom(room, target);
        Task.Delay(ROUND_DURATION_SECONDS * 1000).ContinueWith(t => HandleRoundEnd(room));
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/AuthService.cs ====================

﻿using AimReactionAPI.Data;
using System.Security.Cryptography;
using System.Text;

namespace AimReactionAPI.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly ILogger<GameService> _logger;

    public AuthService(AppDbContext context, ILogger<GameService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        var hashedPassword = HashPassword(password);
        return hashedPassword == storedHash;
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/RoomChatStateService.cs ====================

using System.Collections.Concurrent;
using AimReactionAPI.DTOs;

namespace AimReactionAPI.Services;

public class RoomChatStateService
{
    private readonly ConcurrentDictionary<Guid, List<GameRoomMessageResponse>> _roomMessages = new();

    public void SaveMessage(GameRoomMessageResponse message)
    {
        _roomMessages.AddOrUpdate(
            message.GameRoomId,
            _ => new List<GameRoomMessageResponse> { message },
            (_, list) => { list.Add(message); return list; });
    }

    public List<GameRoomMessageResponse> GetMessages(Guid gameRoomId)
    {
        return _roomMessages.TryGetValue(gameRoomId, out var messages)
            ? messages
            : new();
    }

    public void DeleteRoomMessages(Guid gameRoomId)
    {
         _roomMessages.TryRemove(gameRoomId, out _);
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/GameSessionHandler.cs ====================

﻿using System.Collections.Concurrent;
using AimReactionAPI.Data;
using AimReactionAPI.Models;

namespace AimReactionAPI.Services;

public class GameSessionHandler<TGameType> where TGameType : struct, Enum
{
    private readonly AppDbContext _context;
    private readonly IServiceScopeFactory _scopeFactory;
    private static readonly ConcurrentDictionary<int, int> ActiveSessions = new();
    public GameSessionHandler(AppDbContext context, IServiceScopeFactory scopeFactory)
    {
        _context = context;
        _scopeFactory = scopeFactory;
    }
  

    public async Task<GameSession> StartSessionAsync(int userId, TGameType gameType)
    {
        var session = new GameSession
        {
            UserId = userId,
            StartTime = DateTime.UtcNow,
            GameType = (GameType)(object)gameType,
            
        };

        _context.GameSessions.Add(session);
        await _context.SaveChangesAsync();

        ActiveSessions.TryAdd(session.GameSessionId, userId);

         _ = EndSessionAfterDelayAsync(session.GameSessionId, TimeSpan.FromHours(1));
        
        return session;
    }

    private async Task EndSessionAfterDelayAsync(int sessionId, TimeSpan delay)
    {
        await Task.Delay(delay);

        // Use a new scope to ensure DbContext is valid
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (ActiveSessions.ContainsKey(sessionId))
        {
            try
            {
                var session = await dbContext.GameSessions.FindAsync(sessionId);

                if (session != null)
                {
                    session.EndTime = DateTime.UtcNow;
                    await dbContext.SaveChangesAsync();
                    ActiveSessions.TryRemove(sessionId, out _);
                    Console.WriteLine($"Session {sessionId} automatically ended after {delay.TotalSeconds} seconds.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error automatically ending session {sessionId}: {ex.Message}");
            }
        }
    }


    public async Task<TimeSpan> EndSessionAsync(int sessionId)
    {
        var session = await _context.GameSessions.FindAsync(sessionId);

        if (session == null)
        {
            throw new InvalidOperationException("Session not found.");
        }

        session.EndTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        ActiveSessions.TryRemove(sessionId, out _);

        return session.GetDuration();
    }

    public int GetActiveSessionCount()
    {
        return ActiveSessions.Count;
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/GameService.cs ====================

﻿using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Services;

public class GameService
{
    private readonly AppDbContext _context;
    private readonly ILogger<GameService> _logger;
    private readonly TargetService _targetService;
    private readonly GameUserService _gameUserService;
    private object value;

    //added stubs in testing
    public GameService(object value)
    {
        this.value = value;
    }

    public GameService(AppDbContext context, ILogger<GameService> logger, TargetService targetService, GameUserService gameUserService)
    {
        _context = context;
        _logger = logger;
        _targetService = targetService;
        _gameUserService = gameUserService;
    }
    public virtual async Task<Game?> CreateOrUpdateGameAsync(GameConfigDto gameConfig)
    {
        if (gameConfig == null)
        {
            throw new ArgumentNullException("Game configuration cannot be null.");
        }

        try
        {
            Game game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameConfig.GameId)
                        ?? new Game { CreatorId = gameConfig.CreatorId };

            if (gameConfig.GameId.HasValue && game.CreatorId != gameConfig.CreatorId)
            {
                throw new UnauthorizedAccessException("User is not allowed to make changes.");
            }

            game.GameName = gameConfig.Name;
            game.GameDescription = gameConfig.Description;
            game.DifficultyLevel = gameConfig.DifficultyLevel;
            game.TargetSpeed = gameConfig.TargetSpeed;
            game.MaxTargets = gameConfig.MaxTargets;
            game.GameDuration = gameConfig.GameDuration;
            game.Visibility = gameConfig.Visibility;
            game.GameType = gameConfig.GameType;
            game.Targets = _targetService.GenerateTargets(gameConfig.MaxTargets, gameConfig.TargetSpeed);

            if (!gameConfig.GameId.HasValue)
            {
                _context.Games.Add(game);
            }

            await _context.SaveChangesAsync();

            if (game.Visibility == GameVisibility.PRIVATE)
            {
                await _gameUserService.SetGameUsersAsync(game.GameId, gameConfig.AllowedUsers ?? new List<int>());
            }
            else if (gameConfig.GameId.HasValue)
            {
                await _gameUserService.DeleteCurrentGameUsersAsync(game.GameId);
            }

            return game;
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized users attempts to create/update a game.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating or updating a game.");
            return null;
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/TargetService.cs ====================

﻿using AimReactionAPI.Models;

namespace AimReactionAPI.Services;

public class TargetService
{
    public List<Target> GenerateTargets(int maxTargets = 10, int targetSpeed = 10)
    {
        var targets = new List<Target>();
        for (int i = 0; i < maxTargets; i++)
        {
            targets.Add(GenerateTarget(targetSpeed));
        }
        return targets;
    }

    public static Target GenerateTarget(int targetSpeed = 0)
    {
        return new Target
        {
            X = new Random().Next(0, 100),
            Y = new Random().Next(0, 100),
            Size = new Random().Next(1, 10),
            Speed = targetSpeed
        };
    }
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/GameUserService.cs ====================

using AimReactionAPI.Data;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace AimReactionAPI.Services;

public class GameUserService
{
    private readonly AppDbContext _context;
    private readonly object value;

    //added stubs in testing
    public GameUserService(object value)
    {
        this.value = value;
    }

    public GameUserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task SetGameUsersAsync(int gameId, List<int> userIds)
    {
        if (userIds == null || userIds.Count == 0)
        {
            return;
        }

        await DeleteCurrentGameUsersAsync(gameId);

        var gameUsers = userIds.Select(userId => new GameUser
        {
            GameId = gameId,
            UserId = userId
        }).ToList();

        await _context.GameUsers.AddRangeAsync(gameUsers);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCurrentGameUsersAsync(int gameId)
    {
        var usersToDelete = await _context.GameUsers.Where(gu => gu.GameId == gameId).ToListAsync();
        if (usersToDelete.Count != 0)
        {
            _context.GameUsers.RemoveRange(usersToDelete);
            await _context.SaveChangesAsync();
        }
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/UserService.cs ====================

using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> GetUsers(int userId)
    {
        return await _context.Users
                        .Where(u => u.UserId != userId)
                        .Select(u => new UserDto(u.Name, u.UserId))
                        .ToListAsync();
    }

    public virtual async Task<User?> FindUser(int userId)
    {
        return await _context.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefaultAsync();
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Services/MessageService.cs ====================

using System.Text.Json;
using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Services;

public class MessageService
{
    private readonly RoomChatStateService _roomChatStateService;
    private readonly AppDbContext _dbContext;
    private readonly MultiplayerService _multiplayerService;
    private readonly UserService _userService;
    private readonly ILogger<MessageService> _logger;

    public MessageService(
        AppDbContext dbContext,
        MultiplayerService multiplayerService,
        UserService userService,
        RoomChatStateService roomChatStateService,
        ILogger<MessageService> logger)
    {
        _dbContext = dbContext;
        _roomChatStateService = roomChatStateService;
        _multiplayerService = multiplayerService;
        _userService = userService;
        _logger = logger;
    }

    public async Task SendGlobalMessage(GlobalMessageRequest request)
    {
        _logger.LogInformation("Sending global message from user ID {UserId}", request.SenderId);

        User user = await _userService.FindUser(request.SenderId)
            ?? throw new UnauthorizedAccessException();

        GlobalMessage message = new GlobalMessage
        {
            Content = request.Content,
            SenderId = user.UserId,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.GlobalMessages.Add(message);
        await _dbContext.SaveChangesAsync();

        GlobalMessageResponse response = new(message.Content, user.Name, message.CreatedAt);
        _multiplayerService.Broadcast(JsonSerializer.Serialize(response));
    }

    public async Task<List<GlobalMessageResponse>> GetGlobalMessages(int userId)
    {
        _logger.LogInformation("Fetching global messages for user ID {UserId}", userId);

        var user = await _userService.FindUser(userId)
            ?? throw new UnauthorizedAccessException();

        return await _dbContext.GlobalMessages
            .OrderBy(m => m.CreatedAt)
            .Select(m => new GlobalMessageResponse(m.Content, m.Sender.Name, m.CreatedAt))
            .ToListAsync();
    }

    public async Task SendGameRoomMessage(GameRoomMessageRequest request)
    {
        _logger.LogInformation("Sending game room message from user ID {UserId} to room {RoomId}", request.SenderId, request.GameRoomId);

        var user = await _userService.FindUser(request.SenderId);
        if (user == null
            || !_multiplayerService.TryGetRoom(request.GameRoomId, out Room? room)
            || (room != null && !room.Players.Contains(request.SenderId)))
        {
            throw new UnauthorizedAccessException();
        }

        GameRoomMessageResponse message = new(
            request.GameRoomId,
            request.Content,
            user.Name,
            DateTime.UtcNow
        );

        _roomChatStateService.SaveMessage(message);
        _multiplayerService.BroadcastToRoom(room!, JsonSerializer.Serialize(message));
    }

    public async Task<List<GameRoomMessageResponse>> GetGameRoomMessages(int userId, Guid gameRoomId)
    {
        _logger.LogInformation("Fetching game room messages for user ID {UserId} in room {RoomId}", userId, gameRoomId);

        if (await _userService.FindUser(userId) == null
            || !_multiplayerService.TryGetRoom(gameRoomId, out Room? _))
        {
            throw new UnauthorizedAccessException();
        }

        return _roomChatStateService.GetMessages(gameRoomId);
    }
}


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/obj/Debug/net8.0/AimReactionAPI.GlobalUsings.g.cs ====================

// <auto-generated/>
global using global::Microsoft.AspNetCore.Builder;
global using global::Microsoft.AspNetCore.Hosting;
global using global::Microsoft.AspNetCore.Http;
global using global::Microsoft.AspNetCore.Routing;
global using global::Microsoft.Extensions.Configuration;
global using global::Microsoft.Extensions.DependencyInjection;
global using global::Microsoft.Extensions.Hosting;
global using global::Microsoft.Extensions.Logging;
global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Net.Http.Json;
global using global::System.Threading;
global using global::System.Threading.Tasks;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/obj/Debug/net8.0/AimReactionAPI.AssemblyInfo.cs ====================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Reflection;

[assembly: System.Reflection.AssemblyCompanyAttribute("AimReactionAPI")]
[assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")]
[assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")]
[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0+c2fed9ff7c4a398f34ad072c62d604dbf4ae7927")]
[assembly: System.Reflection.AssemblyProductAttribute("AimReactionAPI")]
[assembly: System.Reflection.AssemblyTitleAttribute("AimReactionAPI")]
[assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]

// Generated by the MSBuild WriteCodeFragment class.



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/obj/Debug/net8.0/AimReactionAPI.MvcApplicationPartsAssemblyInfo.cs ====================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Reflection;

[assembly: Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartAttribute("Swashbuckle.AspNetCore.SwaggerGen")]

// Generated by the MSBuild WriteCodeFragment class.



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/obj/Debug/net8.0/.NETCoreApp,Version=v8.0.AssemblyAttributes.cs ====================

// <autogenerated />
using System;
using System.Reflection;
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v8.0", FrameworkDisplayName = ".NET 8.0")]


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/AimReactionAPI/Program.cs ====================

using AimReactionAPI.Data;
using AimReactionAPI.Services;
using Microsoft.EntityFrameworkCore;
using Fleck;
using WebSocketBoilerplate;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader()
    ));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register services
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<GameUserService>();
builder.Services.AddScoped<TargetService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddSingleton<MultiplayerService>();
builder.Services.AddSingleton<RoomChatStateService>();
builder.Services.AddScoped(typeof(GameSessionHandler<>));

var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());
var app = builder.Build();

var multiplayerService = app.Services.GetRequiredService<MultiplayerService>();
var wsServer = new WebSocketServer("ws://0.0.0.0:8081");
wsServer.Start(ws =>
{
    int? userId = null;
    ws.OnOpen = async () =>
    {
        if (int.TryParse(ws.ConnectionInfo.Path.Trim('/'), out int id))
        {
            userId = id ;
            await multiplayerService.Connect(id, ws);
        }
    };
    ws.OnMessage = async message =>
    {
        try
        {
            await app.InvokeClientEventHandler(clientEventHandlers, ws, message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    };
    ws.OnClose = () =>
    {
        if (userId.HasValue)
        {
            multiplayerService.Disconnect(userId.Value);
        }
    };
});

// Ensure CORS is applied before routing and authentication
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
