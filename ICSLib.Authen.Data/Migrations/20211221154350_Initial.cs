using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ICSLib.Authen.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    GenderDesc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroupRoles",
                columns: table => new
                {
                    RoleGroupId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroupRoles", x => new { x.RoleGroupId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    RoleGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleGroupName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RoleGroupDesc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => x.RoleGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Controler = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SortOrder = table.Column<short>(type: "smallint", nullable: false),
                    IsShow = table.Column<byte>(type: "tinyint", nullable: false),
                    ParentRoleId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<byte>(type: "tinyint", nullable: false),
                    StatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginProvider = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    UserLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    UserFullName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ActionCode = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ActionDesc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TargetObjectId = table.Column<int>(type: "int", nullable: false),
                    GuidTargetObjectId = table.Column<int>(type: "int", nullable: true),
                    OldeData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.UserLogId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleGoups",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleGoupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleGoups", x => new { x.UserId, x.RoleGoupId });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    OAuthId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OAuthName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CrDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 21, 22, 43, 49, 41, DateTimeKind.Local).AddTicks(9348)),
                    ActiveDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserStatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderId", "GenderDesc", "GenderName" },
                values: new object[,]
                {
                    { 1, "Nam", "Male" },
                    { 2, "Nữ", "Female" }
                });

            migrationBuilder.InsertData(
                table: "RoleGroupRoles",
                columns: new[] { "RoleGroupId", "RoleId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 10 },
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 1, 15 },
                    { 1, 16 },
                    { 1, 17 },
                    { 1, 18 },
                    { 1, 19 },
                    { 1, 20 },
                    { 1, 2 },
                    { 1, 1 },
                    { 1, 9 }
                });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "RoleGroupId", "RoleGroupDesc", "RoleGroupName", "SortOrder", "StatusId" },
                values: new object[] { 1, "Quản trị hệ thống", "System administrator", 1, (byte)1 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Action", "ConcurrencyStamp", "Controler", "Description", "Icon", "IsShow", "LevelId", "Name", "NormalizedName", "ParentRoleId", "SortOrder", "StatusId" },
                values: new object[,]
                {
                    { 20, "AssignRoleGroup", "81559d37-e8ac-494e-801c-b0c1e2a615dc", "User", "Cấp chức năng cho người dùng", "", (byte)0, (byte)4, "User-AssignRole", "User-AssignRole", 14, (short)6, (byte)1 },
                    { 2, "", "15ff1c59-dfbc-4efb-bf17-b0c29e4f4025", "", "Phân quyền", "", (byte)1, (byte)2, "Authorization", "Authorization", 1, (short)1, (byte)1 },
                    { 3, "Index", "a10e20d8-ac58-4d9b-b14b-68c15c92bd10", "RoleGroup", "Quyền hệ thống", "", (byte)1, (byte)3, "RoleGroup-Index", "RoleGroup-Index", 2, (short)1, (byte)1 },
                    { 4, "Create", "94b2d05d-3b44-4d75-b518-f29884a9739f", "RoleGroup", "Thêm quyền", "", (byte)0, (byte)4, "RoleGroup-Create", "RoleGroup-Create", 3, (short)1, (byte)1 },
                    { 5, "Edit", "0a5960f1-2ca5-47f0-be77-212ea5e69c2a", "RoleGroup", "Cập nhật quyền", "", (byte)0, (byte)4, "RoleGroup-Edit", "RoleGroup-Edit", 3, (short)2, (byte)1 },
                    { 6, "Delete", "c858e843-cded-40c3-ac8c-96f4352b4654", "RoleGroup", "Xóa quyền", "", (byte)0, (byte)4, "RoleGroup-Delete", "RoleGroup-Delete", 3, (short)3, (byte)1 },
                    { 7, "Details", "8295216d-fc3e-4244-8a8e-94067843b032", "RoleGroup", "Thông tin quyền", "", (byte)0, (byte)4, "RoleGroup-Details", "RoleGroup-Details", 3, (short)4, (byte)1 },
                    { 8, "AssignRole", "d21de80a-bf57-4801-aa83-bf654a15eadb", "RoleGroup", "Cập nhật chức năng cho quyền", "", (byte)0, (byte)4, "RoleGroup-AssignRole", "RoleGroup-AssignRole", 3, (short)5, (byte)1 },
                    { 9, "Index", "aa198f56-218e-47f2-9676-eae5a60f67b7", "Role", "Chức năng", "", (byte)1, (byte)3, "Role-Index", "Role-Index", 2, (short)2, (byte)1 },
                    { 10, "Create", "604728b2-7a5e-4ec7-9fcb-9a4e67abf068", "Role", "Thêm Chức năng", "", (byte)0, (byte)4, "Role-Create", "Role-Create", 9, (short)1, (byte)1 },
                    { 1, "", "02b84634-00b4-4b4e-b2e3-907b30c11f14", "", "Hệ thống", "", (byte)1, (byte)1, "System", "System", 0, (short)1, (byte)1 },
                    { 12, "Delete", "be0b7196-6b6e-4bf1-bb2d-9c7c76422100", "Role", "Xóa Chức năng", "", (byte)0, (byte)4, "Role-Delete", "Role-Delete", 9, (short)3, (byte)1 },
                    { 13, "Details", "350905ce-0e04-42c0-b51e-b71e3076942e", "Role", "Thông tin Chức năng", "", (byte)0, (byte)4, "Role-Details", "Role-Details", 9, (short)4, (byte)1 },
                    { 14, "Index", "cb5ef774-95f8-40b2-bc3f-b7e9a45601e5", "User", "Người dùng", "", (byte)1, (byte)3, "User-Index", "User-Index", 2, (short)3, (byte)1 },
                    { 15, "Create", "695ef50b-4793-4368-afe5-9fa06fe5bc9f", "User", "Thêm người dùng", "", (byte)0, (byte)4, "User-Create", "User-Create", 14, (short)1, (byte)1 },
                    { 16, "Edit", "ae386ee8-e01a-45cf-b72e-6ee7015d1611", "User", "Cập nhật người dùng", "", (byte)0, (byte)4, "User-Edit", "User-Edit", 14, (short)2, (byte)1 },
                    { 17, "Delete", "bea16a48-a2fe-4ef8-a014-cf5f600d7506", "User", "Xóa người dùng", "", (byte)0, (byte)4, "User-Delete", "User-Delete", 14, (short)3, (byte)1 },
                    { 18, "Details", "992aed14-c2c9-4ad9-a498-236fc7d27bcc", "User", "Thông tin người dùng", "", (byte)0, (byte)4, "User-Details", "User-Details", 14, (short)4, (byte)1 },
                    { 19, "AssignRoleGroup", "b7c64738-3bc0-447c-a806-649b0b8209a6", "User", "Cấp quyền cho người dùng", "", (byte)0, (byte)4, "User-AssignRoleGroup", "User-AssignRoleGroup", 14, (short)5, (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Action", "ConcurrencyStamp", "Controler", "Description", "Icon", "IsShow", "LevelId", "Name", "NormalizedName", "ParentRoleId", "SortOrder", "StatusId" },
                values: new object[] { 11, "Edit", "54f24523-fb5e-4351-aba4-4029c8cce114", "Role", "Cập nhật Chức năng", "", (byte)0, (byte)4, "Role-Edit", "Role-Edit", 9, (short)2, (byte)1 });

            migrationBuilder.InsertData(
                table: "UserRoleGoups",
                columns: new[] { "RoleGoupId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 19, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 12, 1 },
                    { 16, 1 },
                    { 11, 1 },
                    { 5, 1 },
                    { 9, 1 },
                    { 8, 1 },
                    { 7, 1 },
                    { 6, 1 },
                    { 4, 1 },
                    { 3, 1 },
                    { 2, 1 },
                    { 1, 1 },
                    { 10, 1 },
                    { 20, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ActiveDateTime", "Address", "Avatar", "Comments", "ConcurrencyStamp", "CrDateTime", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "FullName", "GenderId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OAuthId", "OAuthName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserStatusId" },
                values: new object[] { 1, 0, new DateTime(2021, 12, 21, 22, 43, 49, 93, DateTimeKind.Local).AddTicks(5353), "Hà Nội", "", "", "e1c47bf0-f8b0-473e-a3ef-50014b7b72f2", new DateTime(2021, 12, 21, 22, 43, 49, 93, DateTimeKind.Local).AddTicks(4755), new DateTime(1988, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thew0102@gmail.com", true, "Thế", "Vũ Văn Thế", 1, "Vũ Văn", false, null, "thew0102@gmail.com", "admin", "", "", "AQAAAAEAACcQAAAAEGA0LlwZfwrZxQkqUcbx9DJcCBXUUfPiKeoIfGyN6dpXXKpqzfn6N4Oy3Wy8khxOMA==", "0973214793", true, "", false, "admin", (byte)1 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RoleGroupRoles");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "UserRoleGoups");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
