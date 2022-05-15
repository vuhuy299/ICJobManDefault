using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ICSLib.Authen.Data.Migrations
{
    public partial class initDbCr_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CrDateTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 14, 6, 52, 326, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 22, 43, 49, 41, DateTimeKind.Local).AddTicks(9348));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8efe4468-2bf2-4943-b3d6-0718864ffd3f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "34409737-6e00-4ea8-82d5-7c7e9960e4f3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "cab0e1f1-3d2b-4aca-bf33-e5c5ffd85a78");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "008f15ba-7edb-42e9-9d88-7e71bb69e131");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "4525cff9-0f1c-425d-8aad-01110538e760");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "179d78b6-ea49-40d3-9236-ce298261f852");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "5a2df975-8bd1-4f97-9592-e8d3af38568e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "9c4a3f25-96bd-435c-abf5-2e3b39a47de6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "08101046-8609-462d-9898-f9c2928be0e0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "0a7b3dd1-53fd-4744-8d51-72ef207e8f5b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "a1db3e06-dce4-488d-af6e-d6692716968a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "ConcurrencyStamp",
                value: "fbd10223-560b-4912-a921-a509b6b72c45");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "ConcurrencyStamp",
                value: "e3873845-63ec-41d8-b522-8e52ac74b720");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "ConcurrencyStamp",
                value: "058fcfae-c1d5-4057-9bbc-806f920f4448");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 15,
                column: "ConcurrencyStamp",
                value: "3e10290f-c136-4db3-97b2-f9f26b682afe");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 16,
                column: "ConcurrencyStamp",
                value: "979286b0-be2a-4a22-aec6-a4f77efc5c97");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 17,
                column: "ConcurrencyStamp",
                value: "cf5fb946-ffa0-47f0-8f34-ea7a342b2b60");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 18,
                column: "ConcurrencyStamp",
                value: "759fff41-a0ab-40df-a279-8c85f27b3d41");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 19,
                column: "ConcurrencyStamp",
                value: "c0a72964-4f42-414d-a33a-29e68c000909");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 20,
                column: "ConcurrencyStamp",
                value: "7e99c92a-c1ee-49e7-abac-e90971fe4c21");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActiveDateTime", "ConcurrencyStamp", "CrDateTime", "PasswordHash" },
                values: new object[] { new DateTime(2022, 5, 10, 14, 6, 52, 398, DateTimeKind.Local).AddTicks(4666), "0ba523d7-9b77-43df-a3b6-657bfe3b0121", new DateTime(2022, 5, 10, 14, 6, 52, 398, DateTimeKind.Local).AddTicks(4315), "AQAAAAEAACcQAAAAEJAyfdjqlmPNLxLnd4glxqKwM+JRZE3qHdAuXUZkdpHg0IEtqpieoQuaG3DNgJh7Nw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CrDateTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 22, 43, 49, 41, DateTimeKind.Local).AddTicks(9348),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 14, 6, 52, 326, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "02b84634-00b4-4b4e-b2e3-907b30c11f14");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "15ff1c59-dfbc-4efb-bf17-b0c29e4f4025");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a10e20d8-ac58-4d9b-b14b-68c15c92bd10");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "94b2d05d-3b44-4d75-b518-f29884a9739f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "0a5960f1-2ca5-47f0-be77-212ea5e69c2a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "c858e843-cded-40c3-ac8c-96f4352b4654");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "8295216d-fc3e-4244-8a8e-94067843b032");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "d21de80a-bf57-4801-aa83-bf654a15eadb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "aa198f56-218e-47f2-9676-eae5a60f67b7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "604728b2-7a5e-4ec7-9fcb-9a4e67abf068");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "54f24523-fb5e-4351-aba4-4029c8cce114");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "ConcurrencyStamp",
                value: "be0b7196-6b6e-4bf1-bb2d-9c7c76422100");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "ConcurrencyStamp",
                value: "350905ce-0e04-42c0-b51e-b71e3076942e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "ConcurrencyStamp",
                value: "cb5ef774-95f8-40b2-bc3f-b7e9a45601e5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 15,
                column: "ConcurrencyStamp",
                value: "695ef50b-4793-4368-afe5-9fa06fe5bc9f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 16,
                column: "ConcurrencyStamp",
                value: "ae386ee8-e01a-45cf-b72e-6ee7015d1611");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 17,
                column: "ConcurrencyStamp",
                value: "bea16a48-a2fe-4ef8-a014-cf5f600d7506");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 18,
                column: "ConcurrencyStamp",
                value: "992aed14-c2c9-4ad9-a498-236fc7d27bcc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 19,
                column: "ConcurrencyStamp",
                value: "b7c64738-3bc0-447c-a806-649b0b8209a6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 20,
                column: "ConcurrencyStamp",
                value: "81559d37-e8ac-494e-801c-b0c1e2a615dc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActiveDateTime", "ConcurrencyStamp", "CrDateTime", "PasswordHash" },
                values: new object[] { new DateTime(2021, 12, 21, 22, 43, 49, 93, DateTimeKind.Local).AddTicks(5353), "e1c47bf0-f8b0-473e-a3ef-50014b7b72f2", new DateTime(2021, 12, 21, 22, 43, 49, 93, DateTimeKind.Local).AddTicks(4755), "AQAAAAEAACcQAAAAEGA0LlwZfwrZxQkqUcbx9DJcCBXUUfPiKeoIfGyN6dpXXKpqzfn6N4Oy3Wy8khxOMA==" });
        }
    }
}
