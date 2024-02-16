using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GatherApp.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "emails",
                keyColumn: "Id",
                keyValue: "1820e90d-df57-4519-b9b2-62bb85b69310");

            migrationBuilder.DeleteData(
                table: "emails",
                keyColumn: "Id",
                keyValue: "48c3ece3-9fe8-42ff-b10a-06b40e8732dc");

            migrationBuilder.DeleteData(
                table: "emails",
                keyColumn: "Id",
                keyValue: "64733c66-04af-4932-982a-f4e03c3a0446");

            migrationBuilder.DeleteData(
                table: "emails",
                keyColumn: "Id",
                keyValue: "76afb9b4-efc5-4702-a722-4d347f6b802b");

            migrationBuilder.DeleteData(
                table: "emails",
                keyColumn: "Id",
                keyValue: "e6cb9c91-40f8-41e5-b3ad-f65be6de8815");

            migrationBuilder.DeleteData(
                table: "emails",
                keyColumn: "Id",
                keyValue: "fef837e1-4958-4295-b809-ec80a105254d");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: "2be3422b-1a71-44f1-8b1a-3719f9d1e21e");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: "93c85bae-ca8b-441c-9a51-a8398839ff3e");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "users",
                newName: "CountryId");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_CountryId",
                table: "users",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Countries_CountryId",
                table: "users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Countries_CountryId",
                table: "users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_users_CountryId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "users",
                newName: "Location");

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
        }
    }
}
