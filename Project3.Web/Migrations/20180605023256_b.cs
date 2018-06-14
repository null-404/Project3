using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project3.Web.Migrations
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    coid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cid = table.Column<int>(nullable: false),
                    content = table.Column<string>(maxLength: 500, nullable: false),
                    createtime = table.Column<DateTime>(nullable: false),
                    ip = table.Column<string>(nullable: true),
                    mail = table.Column<string>(nullable: false),
                    nickname = table.Column<string>(maxLength: 10, nullable: false),
                    parentcoid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.coid);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    cid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    allowcomment = table.Column<int>(nullable: false),
                    banner = table.Column<string>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    createtime = table.Column<DateTime>(nullable: false),
                    excerpt = table.Column<string>(nullable: true),
                    index = table.Column<int>(nullable: false),
                    parent = table.Column<int>(nullable: false),
                    slug = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    updatetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.cid);
                });

            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    mid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.mid);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    rid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cid = table.Column<int>(nullable: false),
                    mid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.rid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    uid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    password = table.Column<string>(maxLength: 40, nullable: false),
                    username = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.uid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Metas");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
