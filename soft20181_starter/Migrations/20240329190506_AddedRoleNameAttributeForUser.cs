﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace soft20181_starter.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleNameAttributeForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "roleName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roleName",
                table: "AspNetUsers");
        }
    }
}
