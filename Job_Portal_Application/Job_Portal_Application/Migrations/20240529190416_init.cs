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
                    { new Guid("06e35822-fce4-4211-ae6e-f52393189407"), "Python" },
                    { new Guid("117a0a27-5dd3-4381-b764-ac7c6d7f8855"), "Objective-C" },
                    { new Guid("135c9c80-4c07-44e0-930a-b04d4c852090"), "GCP" },
                    { new Guid("1b1478ef-c436-4b12-a62b-6f7f7a5b68ab"), "NoSQL" },
                    { new Guid("34fc10c1-e426-48e2-bdc9-346269933828"), "HTML" },
                    { new Guid("39b71b6c-82d6-4edd-8581-cf45d7b69d5d"), "Ruby" },
                    { new Guid("4c4a5f4b-9b17-4ed0-884c-fff074c7520d"), "Express" },
                    { new Guid("50e36203-c98c-4daa-b83d-418ec77e7fbd"), "PHP" },
                    { new Guid("5a81a14b-1170-4c5c-b4de-47764c6fb57a"), "Swift" },
                    { new Guid("63ef03b5-9c5a-4a86-873a-50b5a5096d70"), "Kubernetes" },
                    { new Guid("745444cf-4a2e-477c-852a-efd0d257711a"), "React" },
                    { new Guid("75bee84d-e780-48f8-8120-67081bd8fb28"), "GraphQL" },
                    { new Guid("78ea7caf-e080-43a2-a4c3-0eb301d453c6"), "Kotlin" },
                    { new Guid("855631f1-63cf-48b8-8fe7-7d3f77e96552"), "Spring" },
                    { new Guid("8b950496-e8e6-469b-b67e-3f6911a67dde"), "JavaScript" },
                    { new Guid("9a73a72c-3c8b-4511-8e22-2e21b2ee0ab1"), "Flask" },
                    { new Guid("a248b731-b010-4ef9-ba89-38eccd0b801e"), "Django" },
                    { new Guid("a7c9c1dc-8aad-4ab8-b1bc-6e03fb118fbc"), "Rails" },
                    { new Guid("a8485575-7c3d-42f2-b95f-0d2048e97687"), "Docker" },
                    { new Guid("b4669812-a6b8-4d03-9d14-7ad2e200a34e"), "Vue" },
                    { new Guid("bf5b165c-ea6b-4f91-8074-5c2c562bbc88"), "Java" },
                    { new Guid("cda91beb-4ca8-4e2d-814c-a64f5eec3514"), "SQL" },
                    { new Guid("dc872140-2d08-4960-a485-5361b3ae221e"), "Node.js" },
                    { new Guid("dd2f5d8f-fdd5-44eb-b52c-d2764212f955"), "AWS" },
                    { new Guid("e1c57374-5492-4441-80a1-148d6b1bd98f"), "C#" },
                    { new Guid("f12af59b-912b-4c58-95aa-52e79fdf5ef0"), "ASP.NET" },
                    { new Guid("f514c09e-21b9-44ff-a85b-d0e9fee22194"), "Angular" },
                    { new Guid("f73d59d8-de3d-4c38-9d7b-3b5fb1d310de"), "TypeScript" },
                    { new Guid("f7b9a546-0979-47e7-9cb2-7f5c6a43d7db"), "Azure" },
                    { new Guid("fbc52102-d6c7-49b0-a47e-00f7be40a1b9"), "CSS" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { new Guid("02ec8dd6-bfc1-4b8c-b95c-e168d97fe645"), "Systems Administrator" },
                    { new Guid("16c5d006-2ad3-495e-a28a-a848531448d9"), "VR/AR Developer" },
                    { new Guid("1e77f0bc-cd29-4bf1-87c4-967d992e8056"), "Project Manager" },
                    { new Guid("200d443e-6787-4e5d-b96a-5d9ebc3c5bf5"), "Business Analyst" },
                    { new Guid("33c0afdc-694c-4c13-b372-3cf0ee26d427"), "Back End Developer" },
                    { new Guid("42661970-2ab3-4636-884c-b49aec4e4912"), "Machine Learning Engineer" },
                    { new Guid("467af571-0141-4f4d-8aba-5b9778da34e5"), "Database Developer" },
                    { new Guid("4788eaef-1467-4570-bd6e-ed2e542d558b"), "Artificial Intelligence Engineer" },
                    { new Guid("4c307494-2b76-4271-ba98-41559ce4bbe6"), "Technical Support Engineer" },
                    { new Guid("55d43346-ca04-46a3-8671-65f7044002eb"), "Cloud Engineer" },
                    { new Guid("5f4d4d1b-db11-44de-b402-5b982169cd6e"), "Data Scientist" },
                    { new Guid("6206235b-4228-4c72-9f35-50e9bceb7f89"), "Database Administrator" },
                    { new Guid("6bc0a051-825f-40a3-a882-79a6a00cc104"), "Software Engineer" },
                    { new Guid("6df53d9d-4ea5-4080-94b4-be33e793798b"), "Mobile Developer" },
                    { new Guid("7955f03c-7700-43b9-b791-2695ef8dcede"), "Full Stack Developer" },
                    { new Guid("7a808ae3-e104-4e46-a849-0e9eb1c3ff7b"), "Game Developer" },
                    { new Guid("7e288b69-93f7-450b-b706-6c71d52c5851"), "DevOps Engineer" },
                    { new Guid("865d78d3-fe76-41ab-851d-475f23250a0a"), "UI/UX Designer" },
                    { new Guid("9a41095b-7a83-4de3-9e30-00ffce59bd56"), "Security Analyst" },
                    { new Guid("b1057ccf-5c97-4342-a852-460179db8b55"), "QA Engineer" },
                    { new Guid("c02c55e9-1b82-4d1d-8953-684c4336e6a1"), "Cloud Architect" },
                    { new Guid("cb33b182-80f5-40dd-83b5-400f3c8ed587"), "Front End Developer" },
                    { new Guid("ce390c5d-5b8a-4f89-911c-58d320dfa47a"), "Network Engineer" },
                    { new Guid("e8f45d90-c0d3-41f0-93bc-d3aa4670cae1"), "Blockchain Developer" },
                    { new Guid("f14196e1-4191-4859-b349-d2aa820122d5"), "Product Manager" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "City", "Dob", "Email", "HasCode", "Name", "Password", "Phonenumber", "PortfolioLink", "ResumeUrl" },
                values: new object[] { new Guid("bae1166a-75fd-4cc7-864c-711a14f33081"), null, null, new DateOnly(2020, 1, 1), "Admin@jobportal.com", new byte[] { 64, 168, 173, 35, 170, 162, 26, 101, 7, 116, 28, 110, 21, 189, 47, 159, 68, 180, 31, 32, 151, 188, 134, 48, 250, 148, 182, 103, 6, 41, 88, 179, 179, 9, 207, 158, 175, 89, 19, 123, 160, 134, 36, 20, 227, 239, 201, 128, 80, 155, 101, 59, 228, 94, 62, 38, 61, 196, 154, 26, 163, 197, 206, 176, 201, 234, 46, 44, 172, 145, 93, 141, 137, 155, 215, 253, 69, 36, 29, 151, 44, 216, 23, 40, 143, 80, 16, 21, 84, 207, 173, 130, 250, 119, 2, 212, 27, 193, 86, 63, 29, 175, 146, 243, 191, 52, 216, 201, 239, 227, 195, 36, 146, 96, 80, 205, 57, 14, 28, 10, 188, 171, 64, 214, 123, 104, 31, 65 }, "Admin", new byte[] { 214, 72, 129, 159, 116, 176, 175, 144, 193, 214, 211, 104, 204, 229, 164, 66, 229, 212, 236, 95, 205, 242, 248, 145, 102, 158, 253, 177, 156, 129, 236, 32, 61, 29, 42, 220, 133, 197, 240, 184, 162, 217, 51, 195, 159, 48, 236, 128, 104, 161, 198, 31, 31, 62, 229, 51, 178, 173, 211, 69, 150, 197, 178, 241 }, null, null, null });

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
