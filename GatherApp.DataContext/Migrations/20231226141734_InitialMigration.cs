using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GatherApp.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "emails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DateStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Banner = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    LocationUrl = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditedBy = table.Column<string>(type: "text", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OrganizedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    IsInviteOnly = table.Column<bool>(type: "boolean", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "passwordToken",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passwordToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Oid = table.Column<string>(type: "text", nullable: true),
                    ProfilePicture = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invitations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    InviteStatus = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invitations_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invitations_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "emails",
                columns: new[] { "Id", "Body", "Subject", "Type" },
                values: new object[,]
                {
                    { "1820e90d-df57-4519-b9b2-62bb85b69310", "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Click on the button to change your password.</h2><a href='{{url}}' target='_blank' style='background-color: #99BB58; border-radius: 10px; padding: 10px 20px; border: none; cursor: pointer;' onclick='disableButton()'>Change Password</button></a><script>function disableButton() {document.querySelector('button').disabled=true;}</script></body></html>", "GatherApp - Change Password", "ForgotPassword" },
                    { "48c3ece3-9fe8-42ff-b10a-06b40e8732dc", "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event organizer: {{organizer}}</h2><h2>Event title: {{title}}</h2><h2>Event date: {{date}}</h2><h2>Event time: {{time}}</h2></body></html>", "GatherApp - Event was cancelled", "CancelledEvent" },
                    { "64733c66-04af-4932-982a-f4e03c3a0446", "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event title: {{title}}</h2></body></html>", "GatherApp - Event was updated", "UpdatedEvent" },
                    { "76afb9b4-efc5-4702-a722-4d347f6b802b", "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h3>This event has been declined. The reason for declining the event is: {{declineReason}}</h3><a href='{{url}}' target='_blank' style='background-color: #99BB58; border-radius: 10px; padding: 10px 20px; border: none; cursor: pointer;>Click here to view the even</button></a></body></html>", "GatherApp - Your event had been declined", "DeclinedEvent" },
                    { "e6cb9c91-40f8-41e5-b3ad-f65be6de8815", "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event title: {{title}}</h2></br><p>You have been removed from the invitees list of the event. If the event is public, you can still respond to the event.</p></body></html>", "GatherApp - You have been removed from invitees list", "EmailDeletedInvite" },
                    { "fef837e1-4958-4295-b809-ec80a105254d", "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event title: {{title}}</h2></br><p>You have been invited to the event.</p></body></html>", "GatherApp - You have been invited", "EmailInvite" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { "2be3422b-1a71-44f1-8b1a-3719f9d1e21e", "User" },
                    { "93c85bae-ca8b-441c-9a51-a8398839ff3e", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_invitations_EventId",
                table: "invitations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_invitations_UserId",
                table: "invitations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tokens_UserId",
                table: "tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emails");

            migrationBuilder.DropTable(
                name: "invitations");

            migrationBuilder.DropTable(
                name: "passwordToken");

            migrationBuilder.DropTable(
                name: "tokens");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
