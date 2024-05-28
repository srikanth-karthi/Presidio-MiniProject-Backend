using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Job_Portal_Application.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HasCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanySize = table.Column<int>(type: "int", nullable: false),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Skill_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HasCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Dob = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobType = table.Column<int>(type: "int", nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceRequired = table.Column<float>(type: "real", nullable: true),
                    Lpa = table.Column<float>(type: "real", nullable: true),
                    DatePosted = table.Column<DateOnly>(type: "date", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreasOfInterests",
                columns: table => new
                {
                    AreasOfInterestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lpa = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasOfInterests", x => x.AreasOfInterestId);
                    table.ForeignKey(
                        name: "FK_AreasOfInterests_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreasOfInterests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartYear = table.Column<DateOnly>(type: "date", nullable: false),
                    EndYear = table.Column<DateOnly>(type: "date", nullable: true),
                    Percentage = table.Column<float>(type: "real", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsCurrentlyStudying = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_Educations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartYear = table.Column<DateOnly>(type: "date", nullable: false),
                    EndYear = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    UserSkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.UserSkillsId);
                    table.ForeignKey(
                        name: "FK_UserSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobActivities",
                columns: table => new
                {
                    UserJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ResumeViewed = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppliedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobActivities", x => x.UserJobId);
                    table.ForeignKey(
                        name: "FK_JobActivities_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    JobSkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.JobSkillsId);
                    table.ForeignKey(
                        name: "FK_JobSkills_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Skill_Name" },
                values: new object[,]
                {
                    { new Guid("141e4f69-6f3b-4c34-83f9-b312f627421e"), "Vue" },
                    { new Guid("1835f19e-1b08-4eb9-a053-a7d9e7aa7874"), "Spring" },
                    { new Guid("23b60721-74b9-4209-9a57-386ffeebd57f"), "HTML" },
                    { new Guid("265a67cd-9040-43b4-ac4a-8e76c46d85fd"), "ASP.NET" },
                    { new Guid("2880f9a8-c59d-4892-8295-057484923ba0"), "Swift" },
                    { new Guid("4ac34316-88ae-4ba3-8461-ab04b3e3adaa"), "Ruby" },
                    { new Guid("4f4ae1b2-532b-472c-9de2-88ae0995c295"), "Objective-C" },
                    { new Guid("577b76eb-a170-428a-b9f9-3274d985ca3a"), "Flask" },
                    { new Guid("5b5535e7-f592-4006-b257-35d015c67115"), "PHP" },
                    { new Guid("5dd912e7-2668-4210-8f18-e1b675199f7c"), "GCP" },
                    { new Guid("6181eb1f-21d7-4e47-8817-c4821c5c81af"), "Node.js" },
                    { new Guid("699e7a3a-eb03-4191-ae61-26ab54959ee4"), "JavaScript" },
                    { new Guid("6cd69895-b90f-4c91-afc5-9db6f616d482"), "Kubernetes" },
                    { new Guid("6cdbb00b-86c3-47db-b8ec-51dfff2e8cb1"), "CSS" },
                    { new Guid("6ce122a6-91f9-4e8f-9050-0652ff78ff74"), "React" },
                    { new Guid("7b632a0b-c4bb-45e7-a7d6-60c38e0a639a"), "Java" },
                    { new Guid("7d44b987-cb75-4383-a496-47570830adea"), "Python" },
                    { new Guid("7dd97511-e415-4f7f-9df6-b98faf5b628d"), "Angular" },
                    { new Guid("86eb1e41-e46a-41b7-b249-72d40037e95d"), "Docker" },
                    { new Guid("8ac2b3ce-f1e4-4d71-ad25-4aee93384e0a"), "AWS" },
                    { new Guid("98c28ae6-6654-40f0-86f3-98d45deaf017"), "GraphQL" },
                    { new Guid("a591a5b4-94b6-4588-8806-aa1d7da4e040"), "Rails" },
                    { new Guid("ada5ad61-71a4-459a-8494-7bdadba7fed4"), "Azure" },
                    { new Guid("b39270a0-f3c3-401b-bc64-fe948385be51"), "Kotlin" },
                    { new Guid("b9d92cce-f3cc-47f2-9181-c116bf6a09de"), "NoSQL" },
                    { new Guid("b9e0013e-7dec-40d1-ad8c-d404d66fdd12"), "C#" },
                    { new Guid("c54d9df3-48a7-4f8b-a486-d302f857caa4"), "Django" },
                    { new Guid("ced03da9-accd-4c3a-8bf4-e8200653eab2"), "Express" },
                    { new Guid("ecbb456d-5759-4746-88ab-e6cf9b15ae90"), "SQL" },
                    { new Guid("f0817fdc-f342-4509-a8fd-6d360fac73d0"), "TypeScript" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { new Guid("159d6de8-f85d-441f-a59a-76eab718494f"), "Project Manager" },
                    { new Guid("1ce78d42-c795-4cca-9b58-b2a9b6cb2ebd"), "Mobile Developer" },
                    { new Guid("1ebf82d2-974f-4630-bc0c-3632f8cb4847"), "Front End Developer" },
                    { new Guid("3b6d0e62-2b2b-46f2-a108-38dd3d80f3e0"), "Business Analyst" },
                    { new Guid("49b1c0ba-4466-4850-89b3-ab798eda2c4d"), "Machine Learning Engineer" },
                    { new Guid("4e8d8d9d-5a51-4f37-8404-9ed18f82135a"), "Systems Administrator" },
                    { new Guid("51c7ec5f-3ddb-47ef-9cb8-dba1d4180940"), "Full Stack Developer" },
                    { new Guid("53deb86e-a904-4760-a824-85cf6ed39f91"), "Data Scientist" },
                    { new Guid("5f6798b5-d070-4f6c-b5e2-781e63f7f3b9"), "Game Developer" },
                    { new Guid("60a4e3ff-de49-47d4-8b5c-a69d4b6511d4"), "Network Engineer" },
                    { new Guid("7bab2fdb-2807-4cd6-8914-efe3120da31a"), "Software Engineer" },
                    { new Guid("8bcd2b71-58dc-422c-808a-c77345cc6ea7"), "Technical Support Engineer" },
                    { new Guid("9243fdd6-a706-4187-ae8a-2e7de171278e"), "Cloud Engineer" },
                    { new Guid("97a2b333-55a5-4357-9fd5-276c2ffbce6e"), "Security Analyst" },
                    { new Guid("99da2a06-ea51-47f7-adc1-5cf24a70e222"), "Back End Developer" },
                    { new Guid("9e59c86a-58d6-4659-a7e1-9fd0c60cc3a6"), "DevOps Engineer" },
                    { new Guid("a022d1a9-fc89-422b-a928-586678dbfac9"), "Cloud Architect" },
                    { new Guid("b2870e60-8c0d-4870-b0c7-45c7f7a5cd80"), "QA Engineer" },
                    { new Guid("cb0559ee-e156-4c4d-985b-13f3fc966725"), "Product Manager" },
                    { new Guid("d45e60de-f0b3-4891-8723-1ae53c58eaf4"), "Database Administrator" },
                    { new Guid("dd1ddeb2-299c-42c7-b517-24edb0333ecf"), "VR/AR Developer" },
                    { new Guid("e0a958c6-4d2b-41a0-80e5-c24c72e46a07"), "UI/UX Designer" },
                    { new Guid("e78def9d-f7b2-47a1-8d24-5f83f6c78ff3"), "Artificial Intelligence Engineer" },
                    { new Guid("e91031c0-1d21-49ab-85d3-0b356077792f"), "Database Developer" },
                    { new Guid("ff51502d-acad-4f4b-ad2a-c1e53c89ca98"), "Blockchain Developer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreasOfInterests_TitleId",
                table: "AreasOfInterests",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AreasOfInterests_UserId",
                table: "AreasOfInterests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_TitleId",
                table: "Experiences",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobActivities_JobId",
                table: "JobActivities",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobActivities_UserId",
                table: "JobActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TitleId",
                table: "Jobs",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId",
                table: "JobSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_SkillId",
                table: "UserSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreasOfInterests");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "JobActivities");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
