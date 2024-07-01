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
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanySize = table.Column<int>(type: "int", nullable: true),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                values: new object[] { new Guid("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"), new byte[] { 201, 35, 127, 75, 69, 90, 101, 21, 157, 16, 247, 178, 98, 139, 58, 23, 51, 203, 67, 216, 65, 214, 171, 153, 22, 118, 228, 179, 83, 32, 47, 94, 180, 210, 52, 19, 41, 46, 201, 5, 55, 166, 105, 100, 184, 233, 147, 41, 31, 197, 183, 11, 80, 111, 11, 187, 214, 252, 26, 217, 126, 215, 45, 55, 190, 74, 194, 53, 145, 181, 219, 145, 62, 29, 38, 118, 223, 1, 102, 126, 77, 135, 234, 117, 69, 245, 34, 249, 85, 71, 206, 152, 149, 164, 161, 101, 103, 252, 213, 78, 83, 31, 29, 128, 238, 23, 5, 136, 88, 11, 156, 186, 99, 10, 203, 124, 152, 4, 251, 96, 88, 141, 233, 96, 156, 231, 113, 254 }, new byte[] { 59, 255, 5, 2, 240, 113, 234, 194, 153, 188, 175, 151, 142, 241, 210, 148, 197, 98, 131, 235, 40, 1, 157, 202, 195, 90, 49, 64, 135, 42, 17, 7, 98, 240, 150, 162, 217, 159, 239, 241, 137, 11, 4, 8, 211, 202, 186, 82, 211, 61, 216, 243, 213, 31, 153, 5, 38, 99, 83, 82, 7, 130, 33, 32 }, "Admin" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "SkillName" },
                values: new object[,]
                {
                    { new Guid("067c60c1-a6cf-4b8a-b6d5-6a8d0441f1eb"), "TypeScript" },
                    { new Guid("06ed34cc-985b-418c-aea0-2263a430e41f"), "ELK Stack" },
                    { new Guid("07f9bf86-7661-4123-be87-dea35317b371"), "Express" },
                    { new Guid("0b4078c9-3883-4364-8ca7-2cf5c480396f"), "C" },
                    { new Guid("0b92ba65-391a-4147-824e-d4ef442f57f4"), "Objective-C" },
                    { new Guid("0c359f7d-8fde-420e-94fb-d6ba8c2b8900"), "C++" },
                    { new Guid("100a275a-2423-4774-b9e7-d266ad117369"), "Mercurial" },
                    { new Guid("103eb993-6b3e-4064-bc8b-71ade88c3d4f"), "Perl" },
                    { new Guid("1516a9c6-4c7f-4eaa-b3b9-c8c99b32568d"), "Flask" },
                    { new Guid("18057da6-e0ca-4daa-b89d-68ea9d125185"), "Kotlin" },
                    { new Guid("1a18f503-4705-4d4b-94a8-d9a5b83a7019"), "Ansible" },
                    { new Guid("260563cd-ffe6-4490-b87e-34dd57611146"), "Customer Service" },
                    { new Guid("27abfe34-c511-42c9-b6be-3afc9b8cae17"), "SQL" },
                    { new Guid("27f289e6-8667-4c9a-b053-18c73e509ff2"), "Project Management" },
                    { new Guid("2b83b500-be59-4846-a536-83df8502891c"), "Adaptability" },
                    { new Guid("30c3a050-c257-4852-8744-d00dded1b622"), "HTML" },
                    { new Guid("353c4eae-a7c1-45f7-b781-57b8256c9632"), "Swift" },
                    { new Guid("355b3651-66a8-4683-95cc-b5fd5af64264"), "Attention to Detail" },
                    { new Guid("3651a089-9cb7-4233-820f-0651734cf87b"), "Azure" },
                    { new Guid("3ceef870-e43f-4789-ba5d-3a08362f9229"), "Rails" },
                    { new Guid("3eb128ae-b84d-4750-95d2-d3771b3e39f5"), "Scrum" },
                    { new Guid("3eeffde0-4bab-4878-8f93-ee26d11a2369"), "Django" },
                    { new Guid("445401ca-70a1-4fe4-a96c-3a235f2ae3bb"), "Travis CI" },
                    { new Guid("446f44ea-c3a9-475d-b061-1b65ed037eff"), "Teamwork" },
                    { new Guid("452696ad-ce35-425b-9ced-dfe21d7701ae"), "GCP" },
                    { new Guid("487e42ce-8919-455f-b9f5-6017778cdf6b"), "PowerShell" },
                    { new Guid("5293920b-6599-41f2-810a-a2db395f6ac4"), "Webpack" },
                    { new Guid("57330fa7-d64c-46f0-8129-0f3e9fc6c21c"), "Python" },
                    { new Guid("5809d6ae-55e0-4262-925a-e724c31f377b"), "Grafana" },
                    { new Guid("6102c69b-262f-451f-82dc-f0be8bc394ee"), "Rust" },
                    { new Guid("64f5a7e9-7fc8-4507-b24f-2c10e468f014"), "Chef" },
                    { new Guid("69f00980-a4ca-4ce1-b281-683aa4a1b55d"), "Kanban" },
                    { new Guid("6bc950d3-7bef-4390-9ac4-a800824e8c7f"), "Negotiation" },
                    { new Guid("73663c18-e372-47c6-977a-e9ca52ca9969"), "Problem-Solving" },
                    { new Guid("741faa07-7e34-4008-b762-fb32e070cd72"), "Agile Methodology" },
                    { new Guid("77526fe6-86fb-4605-8add-ce0a53b9dbd6"), "Jest" },
                    { new Guid("7ad98392-a333-4385-8d7d-75433eb5cd8d"), "NoSQL" },
                    { new Guid("8b17679e-5c6e-418f-834b-f41cb4b8b00a"), "CSS" },
                    { new Guid("8e52eaa0-2342-4adb-b4f1-947417e5ea23"), "Leadership" },
                    { new Guid("93660f09-81a0-4e5c-9d7b-be2454a7f8e8"), "Conflict Resolution" },
                    { new Guid("97fc1054-55cc-4b8a-b0fd-e1de7a8aa54c"), "Puppet" },
                    { new Guid("98b966b8-22da-4d22-81e3-369f25e6ba07"), "Git" },
                    { new Guid("9aba24cd-c813-45af-8908-d30209788fbf"), "Angular" },
                    { new Guid("9e42c6a7-5eb4-40e1-8e2e-5b687f984ea6"), "PHP" },
                    { new Guid("9e4cc79c-a787-4ad4-9c7f-0390eae40abb"), "Vue" },
                    { new Guid("a002cb5f-9c92-4cc1-b860-20871a608e00"), "Jenkins" },
                    { new Guid("a64968da-0d27-420e-9006-b097a93cb946"), "Spring" },
                    { new Guid("adc3861b-f9e8-4594-aa64-8c45791b0c3d"), "C#" },
                    { new Guid("adf84009-3b96-4434-a91b-b3acf2895e6b"), "Redux" },
                    { new Guid("b09d0bcc-3c2d-417c-bbd1-177a0d0fe1a1"), "Node.js" },
                    { new Guid("b2021b7e-f1b8-4d91-88f4-1f6e50cab60b"), "Terraform" },
                    { new Guid("b232bde8-ffc3-4d0d-b230-9bca452b8690"), "CircleCI" },
                    { new Guid("b32b2d08-8fe2-43a1-b325-3499d79a0e43"), "Kubernetes" },
                    { new Guid("bb83e719-600f-4e26-ae8f-236c00ccfea1"), "Prometheus" },
                    { new Guid("bc484f38-033f-4771-944d-61735ae276e2"), "ASP.NET" },
                    { new Guid("c24cd5cc-e74b-42a7-86f4-1fcddcbdb924"), "AWS" },
                    { new Guid("c2dc6a31-4691-454a-aec6-73b75b9104f2"), "Babel" },
                    { new Guid("c41ca4d8-6e3a-4584-9c24-b38a9bfcf412"), "Shell Scripting" },
                    { new Guid("cabe5ebb-c7e3-4b0f-8d60-0b3c839437f4"), "Java" },
                    { new Guid("cc1ffa5f-586b-4778-a649-a3b5519fc074"), "Communication" },
                    { new Guid("ccc6a65c-2498-4806-b4d5-e785bbec7fba"), "GraphQL" },
                    { new Guid("ccda3852-6d5d-49c8-84f9-c847fede24fb"), "Mocha" },
                    { new Guid("d10a8f6c-bafd-4b46-9de2-547598957377"), "Time Management" },
                    { new Guid("d22d42a8-ae0f-4ea4-99ac-2e342360dff7"), "Critical Thinking" },
                    { new Guid("d377acf6-8078-4bc0-b9bd-0e615ef84a90"), "Ruby" },
                    { new Guid("d52285f0-960e-4dba-8819-ea856bcc2e2d"), "Creativity" },
                    { new Guid("d70e473e-3bb4-4a25-a154-e06e20fa6d04"), "Cucumber" },
                    { new Guid("e1292581-50bf-4479-8701-48bae7b4eaa4"), "Go" },
                    { new Guid("e229520a-9447-4dc2-883e-395efc5785db"), "Subversion" },
                    { new Guid("e4069da1-3bc0-4394-8c36-6b471fb527c6"), "LESS" },
                    { new Guid("e5dfcb9c-3f4e-4f31-a14e-3f97d6919e6c"), "JavaScript" },
                    { new Guid("e7ea90fd-6545-4f26-afdd-7baabc432c94"), "Chai" },
                    { new Guid("ecb454c5-294c-47ff-9278-677c04652145"), "Docker" },
                    { new Guid("f106880e-78b9-41ca-a816-20f70ac63dc1"), "SASS" },
                    { new Guid("f38f9581-5d99-490b-9672-d05853ba6585"), "Splunk" },
                    { new Guid("fa624ad8-0caa-4e78-b6bd-e43e63af0ea5"), "React" },
                    { new Guid("ff893a54-fcf7-4cdf-a579-7e0506aadeb0"), "MobX" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { new Guid("00c16778-7bd5-47f3-ad1f-c18d83eab6dc"), "ERP Consultant" },
                    { new Guid("02574ea6-c8f2-4b2e-a411-4edde55a2b4a"), "IT Support Specialist" },
                    { new Guid("06f2acf9-ec58-4d83-9c39-ecb8ee8d8afb"), "Engineering Manager" },
                    { new Guid("08e39665-8f26-4939-a186-6c4266edc305"), "Technical Writer" },
                    { new Guid("0a6274f6-2f81-4c81-9697-9532e9a1d13a"), "Full Stack Developer" },
                    { new Guid("0af28709-faf9-4037-95ae-d86bfc5376ad"), "Database Developer" },
                    { new Guid("0cf98239-b587-42ce-bc38-9fb89075c90b"), "Cryptographer" },
                    { new Guid("0ddbf4ef-859c-4e9a-a36c-c39201e8ad71"), "Telecommunications Specialist" },
                    { new Guid("1586504d-f29f-4f37-80cc-98d8838b4713"), "CRM Developer" },
                    { new Guid("18d06394-1d07-4968-8837-160eced1730c"), "Chief Information Officer" },
                    { new Guid("1bc4f455-22aa-4c7f-8d5f-9b6a5cac35b5"), "Technical Support Engineer" },
                    { new Guid("1dbbecb9-5bf5-4b12-ac79-e829111d8d3c"), "Data Privacy Officer" },
                    { new Guid("20d7c477-fa53-45cb-9547-10357fbcc9b4"), "Game Developer" },
                    { new Guid("23f6baee-38cd-4890-9ac5-4ca5535ff442"), "IoT Engineer" },
                    { new Guid("264a929b-896e-4166-a53a-6243f5e7955d"), "Technical Lead" },
                    { new Guid("29ad869c-a1fb-4abb-84cd-789b8aeab07d"), "Business Analyst" },
                    { new Guid("2ac07f01-eac4-4116-9193-6fd8baa4495e"), "Hadoop Developer" },
                    { new Guid("2d0a527e-de8d-4f64-b7f6-ba22af4474b0"), "Systems Integrator" },
                    { new Guid("2fc9daae-6003-461a-907d-295a77157881"), "Chief Information Officer" },
                    { new Guid("33adebe9-9abb-4240-99ba-5c60646f8726"), "IT Operations Manager" },
                    { new Guid("3569ac60-11a7-4d83-a3f2-e87121aeb328"), "Solution Architect" },
                    { new Guid("36de31e6-8352-4361-a38a-2a863b387908"), "Data Engineer" },
                    { new Guid("38c10386-f241-47a4-9562-fc6fe553511d"), "Back End Developer" },
                    { new Guid("3f93f4c1-2fe4-4b27-a499-2479fbae9f35"), "Software Architect" },
                    { new Guid("409fcddf-a835-480b-811c-df6b02e72682"), "Blockchain Developer" },
                    { new Guid("44285da5-b767-41d3-904f-ca3f06e70ac0"), "Release Manager" },
                    { new Guid("4831e2fc-f6d6-4ff2-aad1-8e9801d49e36"), "Site Reliability Engineer" },
                    { new Guid("4c5d6a4c-deb6-49e0-ab40-8bb73f54d12d"), "QA Engineer" },
                    { new Guid("51981627-545f-4b95-8114-65ee7d160938"), "Chief Data Officer" },
                    { new Guid("521941d9-e89c-40c6-b225-deed7100e8e2"), "Hardware Engineer" },
                    { new Guid("56b517d2-00ae-4fcb-b4ef-61dbb5f639bd"), "IT Project Manager" },
                    { new Guid("5b1d949b-3f24-465a-b01d-ee5e33bb0f2a"), "Computer Vision Engineer" },
                    { new Guid("5ca425be-e1f4-4574-9828-ce33eb9d5289"), "Mobile Developer" },
                    { new Guid("6103537f-5f3a-48cb-aa2c-61508dcb5616"), "UX Researcher" },
                    { new Guid("63caaed3-fa26-4642-9a1d-6623520f4f2b"), "Cloud Engineer" },
                    { new Guid("64b6303c-7a31-4ccb-94a0-1ca9fcb989e8"), "IT Trainer" },
                    { new Guid("693ad10c-ccdd-4935-ac27-72981fbcce07"), "Software Development Manager" },
                    { new Guid("6e3cce29-39ca-4a44-8776-9fd9643ee30c"), "Artificial Intelligence Engineer" },
                    { new Guid("6f583b9b-2e81-4185-8f10-788fed87ca6a"), "IT Procurement Manager" },
                    { new Guid("79d2b753-50c2-4f07-82d7-6bc5756d6471"), "Enterprise Systems Manager" },
                    { new Guid("7b8a100d-d915-4ba8-ac20-4307065961dc"), "IT Consultant" },
                    { new Guid("7c60ce39-d3f9-4383-9383-6d0aa3af5581"), "Security Engineer" },
                    { new Guid("7f6b1602-f380-4e83-b63f-7397351f3ed2"), "Project Manager" },
                    { new Guid("86cc2889-f578-4f94-b7ec-0d73b6632080"), "Data Analyst" },
                    { new Guid("8c2c5e23-9a5f-45fc-809a-a34bc4ec3828"), "Information Technology Specialist" },
                    { new Guid("8df47acd-fe16-4c10-9f8c-e43a52cb0a43"), "DevSecOps Engineer" },
                    { new Guid("91062b7f-61e6-443b-bf1a-83f1aaade9cd"), "Machine Learning Engineer" },
                    { new Guid("92fe2398-0765-4a70-9b73-a339f5d43432"), "Product Manager" },
                    { new Guid("934b2daa-61f7-41c7-889a-587370af4a48"), "Natural Language Processing Engineer" },
                    { new Guid("93b98550-caf7-4943-8a91-bd8c08f902d9"), "Quality Assurance Manager" },
                    { new Guid("96c45bc0-641c-42a6-bfa9-28dcbfd2ad5c"), "UI/UX Designer" },
                    { new Guid("98670a8c-ae31-4b53-a528-ecd95bd21d44"), "Digital Forensics Specialist" },
                    { new Guid("98f6d1dd-a4e5-4116-b400-c0874f7f2d80"), "IT Director" },
                    { new Guid("a35b3c8e-7593-4c43-9f25-04041e9d8e58"), "Information Security Analyst" },
                    { new Guid("a53ab615-3ca0-4993-ac09-81b7f8535034"), "Business Systems Analyst" },
                    { new Guid("a66ad865-a29d-41bf-a730-b879dd1cff54"), "IT Auditor" },
                    { new Guid("a6a2a59b-942b-4757-aaa0-04b5382726ea"), "Front End Developer" },
                    { new Guid("aa51ba32-6fea-448c-8659-b9c8778141de"), "Security Analyst" },
                    { new Guid("af16fc7a-2713-49ee-8684-d2d4a2e6c557"), "Cloud Architect" },
                    { new Guid("b3677100-83eb-4f5e-857b-fa51b1d18e0a"), "Ethical Hacker" },
                    { new Guid("b423e33a-9fcb-4ed4-abb2-dc86fd874488"), "Embedded Systems Engineer" },
                    { new Guid("b42e8bd7-a63f-4969-8acc-854013fbc8ba"), "Systems Administrator" },
                    { new Guid("b60db7c5-2506-4dd7-bc77-551bedd2d9e5"), "Firmware Engineer" },
                    { new Guid("b704db78-979b-4870-87b0-06401e32a760"), "Robotics Engineer" },
                    { new Guid("b7400fc5-a39f-4a00-9eb3-507dda053840"), "Network Architect" },
                    { new Guid("b94f2568-ee49-4380-a107-f8e16c435470"), "DevOps Engineer" },
                    { new Guid("c1844c00-78ff-4119-9861-4df0145563ac"), "Cloud Security Engineer" },
                    { new Guid("c8d7a1cc-6a31-4333-8a56-13a05a6da260"), "Big Data Engineer" },
                    { new Guid("ca0dffcc-733a-4e60-95fb-04970b593949"), "IT Director" },
                    { new Guid("caf2a8a5-bbe7-4ddc-8efe-5cc4b0e19f95"), "Site Engineer" },
                    { new Guid("cc63f8a3-de37-4d3f-8805-29f78075dbe2"), "Database Administrator" },
                    { new Guid("d2d98351-09d8-418b-a146-7cefc1a4ec07"), "Help Desk Technician" },
                    { new Guid("d41f99f7-06d7-4859-9b39-d4cc550e343a"), "Penetration Tester" },
                    { new Guid("da00b5c9-e2b0-4b7b-be72-2aa4e1774f95"), "Compliance Analyst" },
                    { new Guid("db75f17c-a37f-4f50-b30e-f0969e39ecd1"), "Network Engineer" },
                    { new Guid("de936739-f694-4e10-bc78-73a93f42734b"), "IT Manager" },
                    { new Guid("df1e5823-a137-4d97-9687-7eb529a670f2"), "Scrum Master" },
                    { new Guid("e05c90bf-20e5-4d7d-92e6-79ed6b977aca"), "VR/AR Developer" },
                    { new Guid("e2959333-a5d5-48b1-a314-d592b138c3fe"), "Enterprise Architect" },
                    { new Guid("e3202513-acb4-4b05-bbc2-d67bd63cbb25"), "Agile Coach" },
                    { new Guid("e3c7621b-7d20-4f1c-bbd6-e5d43fb4f387"), "Configuration Manager" },
                    { new Guid("eb93ce79-2d85-477d-8aa8-0155b6f965fb"), "Application Support Engineer" },
                    { new Guid("ec8d27b2-9a8b-45f3-9851-f3a2b4825be9"), "Bioinformatics Specialist" },
                    { new Guid("f174a4b9-42f1-4b82-8e06-cf5e52fe7724"), "Systems Analyst" },
                    { new Guid("f1c040f6-5b9e-4815-a858-61fc036d987c"), "Chief Information Security Officer" },
                    { new Guid("f245c806-dbc4-4d55-b82a-0c180b05d726"), "Chief Technology Officer" },
                    { new Guid("f5feb18b-09c6-4003-8a03-7bcb413d51a2"), "Incident Response Analyst" },
                    { new Guid("f73bb07c-9bf6-4a20-bf32-c5f9f3017671"), "Build Engineer" },
                    { new Guid("faf14050-1b4a-4164-ae27-17c143e51b26"), "Software Engineer" },
                    { new Guid("fd3c4d12-5816-4c25-906e-d09b518f1241"), "IT Project Coordinator" },
                    { new Guid("fd888b70-bfdd-442a-8cd0-647ce7da45fb"), "Data Scientist" },
                    { new Guid("fde9f697-f47b-40ed-bb34-f3882d15ae99"), "Business Intelligence Developer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AboutMe", "Address", "City", "CredentialId", "Dob", "Email", "Name", "Phonenumber", "PortfolioLink", "ProfilePictureUrl", "ResumeUrl" },
                values: new object[] { new Guid("df9b412d-c5e7-4bbf-8883-89c469f22ec2"), null, null, null, new Guid("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"), new DateOnly(1, 1, 1), "Admin@jobportal.com", "Admin", null, null, null, null });

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
