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
                name: "Credential",
                columns: table => new
                {
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HasCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => x.CredentialId);
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
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanySize = table.Column<int>(type: "int", nullable: false),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Companies_Credential_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credential",
                        principalColumn: "CredentialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Users_Credential_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credential",
                        principalColumn: "CredentialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    JobApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResumeViewed = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppliedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobActivities", x => x.JobApplicationId);
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
                table: "Credential",
                columns: new[] { "CredentialId", "HasCode", "Password", "Role" },
                values: new object[] { new Guid("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"), new byte[] { 175, 190, 82, 206, 212, 81, 147, 103, 88, 133, 20, 39, 116, 242, 32, 37, 170, 74, 129, 155, 240, 246, 132, 222, 19, 225, 149, 8, 30, 114, 94, 140, 61, 131, 5, 95, 80, 1, 181, 248, 249, 67, 153, 16, 94, 231, 213, 241, 185, 235, 182, 152, 65, 200, 111, 168, 104, 143, 39, 120, 207, 236, 177, 65, 205, 165, 57, 225, 30, 163, 105, 199, 211, 153, 61, 186, 81, 95, 254, 155, 10, 70, 103, 172, 90, 130, 81, 216, 58, 28, 195, 130, 245, 159, 35, 48, 37, 114, 67, 148, 75, 209, 40, 119, 82, 76, 144, 179, 203, 85, 162, 245, 175, 64, 95, 148, 127, 85, 210, 208, 65, 87, 175, 73, 183, 159, 150, 115 }, new byte[] { 235, 223, 233, 148, 68, 3, 186, 239, 233, 35, 249, 7, 131, 190, 72, 211, 88, 35, 50, 61, 43, 128, 14, 58, 121, 31, 171, 146, 157, 63, 94, 27, 86, 4, 134, 63, 242, 139, 2, 152, 132, 1, 200, 84, 234, 64, 194, 47, 223, 243, 213, 205, 164, 162, 233, 77, 131, 65, 67, 144, 15, 12, 31, 50 }, "Admin" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Skill_Name" },
                values: new object[,]
                {
                    { new Guid("0cd2f7f3-0517-45b4-a6fa-2c9d76074ec1"), "JavaScript" },
                    { new Guid("2a01f190-37f4-450a-85d7-612b21b94741"), "PHP" },
                    { new Guid("339124dc-5fce-4c69-b210-bf05e7f5f911"), "Django" },
                    { new Guid("5c6d5d20-c617-4a85-af97-068a8d51bee6"), "C#" },
                    { new Guid("63e1cada-ae43-440c-9e4d-b7d290102fcf"), "GCP" },
                    { new Guid("64e264cc-8016-4664-8e9d-e2c9eaa05b06"), "CSS" },
                    { new Guid("6dbea15b-4d00-4903-a393-1e357087bc88"), "Kotlin" },
                    { new Guid("6ef9e759-afbb-4db1-b981-c2f9d0f09628"), "Azure" },
                    { new Guid("6f5c05b7-3190-400d-a186-ab9652e42272"), "React" },
                    { new Guid("7056dbd6-4c81-47ea-b713-e899455a9004"), "Python" },
                    { new Guid("7188bf2f-7510-4007-9ebb-b75a4a2f5e87"), "SQL" },
                    { new Guid("7830b613-ce28-4069-be79-98358adf7383"), "Swift" },
                    { new Guid("7ace8f9d-20c5-4351-9693-f3f993ef9c1a"), "NoSQL" },
                    { new Guid("8a99acb2-9790-42c2-be31-54046fcb9460"), "Vue" },
                    { new Guid("94d7866d-10be-4ffe-91ee-5226ad334718"), "Angular" },
                    { new Guid("9638c0eb-92a5-4d56-8e5d-2b7af35eb151"), "Spring" },
                    { new Guid("96f32405-41e3-4902-a6d0-1e5f25c3556e"), "ASP.NET" },
                    { new Guid("a0b6b1c1-0d10-4812-b273-11e6dbeb4563"), "Express" },
                    { new Guid("a9ebd29d-7bbe-4ab5-a6f4-98aaa3a4dc1c"), "AWS" },
                    { new Guid("aae52f4c-f9c2-410f-98c2-033f2175290c"), "Node.js" },
                    { new Guid("af533f0f-ecc5-4f7d-8bc5-ff79171d3f3f"), "Flask" },
                    { new Guid("afaa26f2-3aee-47b0-b0ea-4c330e32f946"), "Java" },
                    { new Guid("b2e0a690-7970-4aef-96d4-e0aec6d83db0"), "Objective-C" },
                    { new Guid("b7882aa1-76f3-40b1-9d61-79078c44c3fb"), "TypeScript" },
                    { new Guid("bdd2f762-9087-40d7-b78a-0127b1089024"), "Ruby" },
                    { new Guid("be1fdbf5-9a8d-4952-9847-460283851002"), "Kubernetes" },
                    { new Guid("c1ee1fe1-adbf-47dd-b9a7-0cc2bcd1ddde"), "Docker" },
                    { new Guid("eebe1e66-ef0d-4553-bdab-8e1e43e13654"), "HTML" },
                    { new Guid("f09467df-beff-426d-9bc3-18b7fbade7be"), "Rails" },
                    { new Guid("fbabc07e-3d81-4fc8-a845-1d2ed7f0e3ac"), "GraphQL" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { new Guid("13d44e5e-fb16-4082-8ee6-0f14644ad351"), "Back End Developer" },
                    { new Guid("1a28a1ff-6104-494a-a40a-dbd07d0f9bf1"), "Mobile Developer" },
                    { new Guid("2daa4567-9097-47ce-a58a-9fff4e819da2"), "Network Engineer" },
                    { new Guid("3d383f12-ac3e-48f5-bee1-f8bed2be225f"), "Security Analyst" },
                    { new Guid("42898179-9b26-4672-86b0-b6ce3861a5f1"), "Technical Support Engineer" },
                    { new Guid("5fa9ea7f-5efc-4f67-9bfa-b0bf5b605ffb"), "VR/AR Developer" },
                    { new Guid("66ea45c7-940f-4be7-8839-b2c596f0c453"), "Front End Developer" },
                    { new Guid("67ac4dac-224f-4203-8b3e-389d7f71cd35"), "QA Engineer" },
                    { new Guid("69318531-1e75-4a65-b577-2e1dab59c618"), "Database Administrator" },
                    { new Guid("77f4bba6-63cc-437b-9d0f-15130f857e6a"), "Systems Administrator" },
                    { new Guid("85b38a56-8a33-48d2-aa27-02569392a714"), "Blockchain Developer" },
                    { new Guid("8f9a3186-dff7-4021-a734-4d9a6558b58e"), "DevOps Engineer" },
                    { new Guid("a118d173-bd94-4b0b-ba33-53583f61dd42"), "Business Analyst" },
                    { new Guid("a7d53d1e-3216-491e-acf5-bd6ff1edae17"), "Software Engineer" },
                    { new Guid("a85f6f91-74de-41a7-bfe3-e3d6cfccb6da"), "Product Manager" },
                    { new Guid("ac8c829b-5fe8-47c1-bf8c-ca8846b1635c"), "Database Developer" },
                    { new Guid("af7f51e1-674b-4213-8197-f2cb0295106b"), "Cloud Architect" },
                    { new Guid("b81b4844-7b5f-4487-934b-bd54f9709390"), "Game Developer" },
                    { new Guid("b9400096-5e2c-465c-a3ad-a03f7180cd27"), "Cloud Engineer" },
                    { new Guid("c3abb7e8-7f9c-44e5-a0f8-49303c3dc0f6"), "Machine Learning Engineer" },
                    { new Guid("e99192b0-d290-44af-8055-8d957e559a95"), "Project Manager" },
                    { new Guid("eef31387-03b9-4c22-9b65-ac3a2fb567fb"), "UI/UX Designer" },
                    { new Guid("f4e4cc47-887e-4307-b791-d1ba394302c1"), "Data Scientist" },
                    { new Guid("fb5bda27-edeb-4975-8073-b6789d28e1e3"), "Full Stack Developer" },
                    { new Guid("fcb3e80e-42a4-400c-967e-29e838af9f24"), "Artificial Intelligence Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "City", "CredentialId", "Dob", "Email", "Name", "Phonenumber", "PortfolioLink", "ResumeUrl" },
                values: new object[] { new Guid("904f7da1-79a2-45a8-b727-fbf4662609cb"), null, null, new Guid("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"), new DateOnly(1, 1, 1), "Admin@jobportal.com", "Admin", null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AreasOfInterests_TitleId",
                table: "AreasOfInterests",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AreasOfInterests_UserId",
                table: "AreasOfInterests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CredentialId",
                table: "Companies",
                column: "CredentialId",
                unique: true);

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
                name: "IX_Users_CredentialId",
                table: "Users",
                column: "CredentialId",
                unique: true);

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

            migrationBuilder.DropTable(
                name: "Credential");
        }
    }
}
