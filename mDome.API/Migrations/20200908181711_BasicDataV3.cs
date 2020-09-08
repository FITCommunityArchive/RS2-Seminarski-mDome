﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mDome.API.Migrations
{
    public partial class BasicDataV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Album",
                columns: new[] { "AlbumId", "AlbumDescription", "AlbumGeneratedRating", "AlbumName", "AlbumPhoto", "AlbumPhotoThumb", "ArtistId", "DateReleased" },
                values: new object[,]
                {
                });

            migrationBuilder.InsertData(
                table: "ArtistGenre",
                columns: new[] { "GenreId", "ArtistId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 8, 1 },
                    { 3, 2 },
                    { 9, 2 }
                });

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "UserId",
                keyValue: 7,
                column: "SuspendedFlag",
                value: false);

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "UserId",
                keyValue: 8,
                column: "SuspendedFlag",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Album",
                keyColumn: "AlbumId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Album",
                keyColumn: "AlbumId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ArtistGenre",
                keyColumns: new[] { "GenreId", "ArtistId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ArtistGenre",
                keyColumns: new[] { "GenreId", "ArtistId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ArtistGenre",
                keyColumns: new[] { "GenreId", "ArtistId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "ArtistGenre",
                keyColumns: new[] { "GenreId", "ArtistId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "ArtistGenre",
                keyColumns: new[] { "GenreId", "ArtistId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "ArtistGenre",
                keyColumns: new[] { "GenreId", "ArtistId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "UserId",
                keyValue: 7,
                column: "SuspendedFlag",
                value: false);

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "UserId",
                keyValue: 8,
                column: "SuspendedFlag",
                value: false);
        }
    }
}