using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article_categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_auth", x => x.Id);
                    table.UniqueConstraint("AK_user_auth_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NickName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_tags_TagId",
                        column: x => x.TagId,
                        principalTable: "tags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_users_user_auth_Id",
                        column: x => x.Id,
                        principalTable: "user_auth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    KeyWords = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CountLikes = table.Column<int>(type: "integer", nullable: false),
                    CountDislikes = table.Column<int>(type: "integer", nullable: false),
                    CountViewsPerDay = table.Column<int>(type: "integer", nullable: false),
                    CountViewsPerWeek = table.Column<int>(type: "integer", nullable: false),
                    CountViewsPerMonth = table.Column<int>(type: "integer", nullable: false),
                    CountViews = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    CountAppraisers = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_articles_article_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "article_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_articles_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "complaints_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaints_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_complaints_user_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FoundWords = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recommendations_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                columns: table => new
                {
                    ArticlesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => new { x.ArticlesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ArticleTag_articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTag_tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "commentaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CountLikes = table.Column<int>(type: "integer", nullable: false),
                    CountDislikes = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commentaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_commentaries_articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commentaries_commentaries_ParentId",
                        column: x => x.ParentId,
                        principalTable: "commentaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_commentaries_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "complaints_article",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaints_article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_complaints_article_articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "article_categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a58b52f5-d8dd-4530-a9bc-1c75a3e6a7f3"), "Sport category" });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("49bac43a-4522-4031-870d-302ed4441f8a"), "#sport" });

            migrationBuilder.InsertData(
                table: "user_auth",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[] { new Guid("afd97b6f-0511-4c46-a357-fb62c9582aa1"), "test@gmail.com", "12345", "user" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Avatar", "Description", "Name", "NickName", "RegistrationDate", "TagId" },
                values: new object[] { new Guid("afd97b6f-0511-4c46-a357-fb62c9582aa1"), null, "I`m TesT!", "TesT", "@testU5er", new DateTime(2023, 12, 21, 0, 37, 49, 190, DateTimeKind.Local).AddTicks(8107), null });

            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CountAppraisers", "CountDislikes", "CountLikes", "CountViews", "CountViewsPerDay", "CountViewsPerMonth", "CountViewsPerWeek", "CreationDate", "KeyWords", "Name", "Rating", "Status", "Text" },
                values: new object[] { new Guid("489d1832-f8d2-4592-8e2a-368e80618f01"), new Guid("afd97b6f-0511-4c46-a357-fb62c9582aa1"), new Guid("a58b52f5-d8dd-4530-a9bc-1c75a3e6a7f3"), 0, 0, 0, 0, 0, 0, 0, new DateTime(2023, 12, 21, 0, 37, 49, 190, DateTimeKind.Local).AddTicks(8156), "example event", "First event", 0.0, 0, "This is example event" });

            migrationBuilder.InsertData(
                table: "commentaries",
                columns: new[] { "Id", "ArticleId", "AuthorId", "CountDislikes", "CountLikes", "CreationDate", "ParentId", "Text" },
                values: new object[] { new Guid("482e68bf-1035-42fa-8b30-4df0cc73c56c"), new Guid("489d1832-f8d2-4592-8e2a-368e80618f01"), new Guid("afd97b6f-0511-4c46-a357-fb62c9582aa1"), 1, 11, new DateTime(2023, 12, 21, 0, 37, 49, 190, DateTimeKind.Local).AddTicks(8166), null, "Good event!" });

            migrationBuilder.CreateIndex(
                name: "IX_articles_AuthorId",
                table: "articles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_articles_CategoryId",
                table: "articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_TagsId",
                table: "ArticleTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_commentaries_ArticleId",
                table: "commentaries",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_commentaries_AuthorId",
                table: "commentaries",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_commentaries_ParentId",
                table: "commentaries",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_article_ArticleId",
                table: "complaints_article",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_user_UserId",
                table: "complaints_user",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_recommendations_UserId",
                table: "recommendations",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_TagId",
                table: "users",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTag");

            migrationBuilder.DropTable(
                name: "commentaries");

            migrationBuilder.DropTable(
                name: "complaints_article");

            migrationBuilder.DropTable(
                name: "complaints_user");

            migrationBuilder.DropTable(
                name: "recommendations");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "article_categories");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "user_auth");
        }
    }
}
