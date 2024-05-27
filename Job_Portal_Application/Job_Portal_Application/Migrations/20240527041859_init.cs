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
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("2526bc61-baf0-4967-bc8d-6eb1c29a09f1"), "HTML" },
                    { new Guid("2b03cf51-f5c0-4ee3-86b8-006fd56dacaf"), "Python" },
                    { new Guid("2c86b77a-9150-4c33-85ed-cae9b3e894c4"), "CSS" },
                    { new Guid("2d7146d7-65a8-42b9-8740-99f9f27e8847"), "React" },
                    { new Guid("33997147-d435-47f1-99a2-17d09faf04a2"), "Objective-C" },
                    { new Guid("39843a19-7512-48c8-938a-e4b8f06443e1"), "Swift" },
                    { new Guid("464144c0-4edd-4cf2-869b-ac3f6fe5c034"), "Flask" },
                    { new Guid("50b98280-d10d-458c-9f4b-de2f9b9e5a6c"), "Java" },
                    { new Guid("61d8bcc3-dc94-4b98-8cbe-f89249354ae5"), "Rails" },
                    { new Guid("6d71f71e-88a1-4e7b-b8d7-30dbd5de280d"), "Ruby" },
                    { new Guid("7034e518-b7e0-4513-bc0a-5dbb56227f96"), "Kotlin" },
                    { new Guid("7e86c3cb-d4d4-4bfd-bd32-913abebdc7f3"), "JavaScript" },
                    { new Guid("8469f12b-b536-4eac-8385-419019e4f193"), "Django" },
                    { new Guid("a5252b60-ce57-4a03-bb3d-b057ab5378f3"), "PHP" },
                    { new Guid("ae40ece8-3233-402d-8267-ef8e0f52b089"), "TypeScript" },
                    { new Guid("b0d31ed4-37f9-4485-a6c0-099e3441ffcf"), "Express" },
                    { new Guid("c3ef6e93-dc34-4e08-9002-7581d2b07f79"), "Node.js" },
                    { new Guid("ce34c9b7-f2d9-49fa-9c10-bdb96bf1314a"), "Angular" },
                    { new Guid("e5b38f78-4a12-4443-946a-70531569bb99"), "Vue" },
                    { new Guid("fa773613-a5ce-4ac5-becc-a05c3e834bf7"), "Spring" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { new Guid("08bbd7d2-f1fc-4eae-b6ea-a75e6e274900"), "UI/UX Designer" },
                    { new Guid("1ade1a82-2a68-4420-8e1c-c8b0a6050fd1"), "Front End Developer" },
                    { new Guid("31cf9311-1fcd-42ea-b45b-18380200268a"), "Data Scientist" },
                    { new Guid("360dd497-0e6f-407c-9c8b-06641d1c47f5"), "Back End Developer" },
                    { new Guid("412db0e1-5720-4f34-b6bb-9179f41d7e49"), "Technical Support Engineer" },
                    { new Guid("420de26a-2738-4596-aa25-e78e3cf7e395"), "Network Engineer" },
                    { new Guid("429966cc-4c72-4516-94de-0779ab938ee6"), "Database Administrator" },
                    { new Guid("48312c0f-5673-473a-94bd-404db60c4d1b"), "Product Manager" },
                    { new Guid("50b3d904-fc8b-483a-8352-d643e80fc21e"), "Full Stack Developer" },
                    { new Guid("561be5cb-18ef-477f-a5dc-37d7d5999f44"), "Mobile Developer" },
                    { new Guid("6440c0cc-bd9e-4f3f-8756-a971dd82707e"), "Security Analyst" },
                    { new Guid("64b23c44-30aa-4841-877a-4c91bb32968f"), "Software Engineer" },
                    { new Guid("7a2b6cbd-458e-492d-b908-28a8372977e5"), "QA Engineer" },
                    { new Guid("92d4a0a7-67c5-4a1c-b5cf-caaf03e99d7a"), "Artificial Intelligence Engineer" },
                    { new Guid("9fba039e-545d-4023-9028-0ae453c9c872"), "Systems Administrator" },
                    { new Guid("abfdbb40-e2ee-41bd-b893-672823312c61"), "Project Manager" },
                    { new Guid("b51c6d58-2b3f-4ba6-a047-9b4b3ae044a2"), "Cloud Architect" },
                    { new Guid("c10e5444-3258-4c0e-ab87-24a0709b6419"), "Machine Learning Engineer" },
                    { new Guid("eaa9d01a-dc86-4c88-87d0-0315187f769f"), "Business Analyst" },
                    { new Guid("fc74ccad-6572-4f48-88f4-66d7a1494029"), "DevOps Engineer" }
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
