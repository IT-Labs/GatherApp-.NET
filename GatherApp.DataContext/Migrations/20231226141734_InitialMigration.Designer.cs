﻿// <auto-generated />
using System;
using GatherApp.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GatherApp.DataContext.Migrations
{
    [DbContext(typeof(GatherAppContext))]
    [Migration("20231226141734_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GatherApp.Contracts.Entities.Email", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("emails", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1820e90d-df57-4519-b9b2-62bb85b69310",
                            Body = "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Click on the button to change your password.</h2><a href='{{url}}' target='_blank' style='background-color: #99BB58; border-radius: 10px; padding: 10px 20px; border: none; cursor: pointer;' onclick='disableButton()'>Change Password</button></a><script>function disableButton() {document.querySelector('button').disabled=true;}</script></body></html>",
                            Subject = "GatherApp - Change Password",
                            Type = "ForgotPassword"
                        },
                        new
                        {
                            Id = "76afb9b4-efc5-4702-a722-4d347f6b802b",
                            Body = "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h3>This event has been declined. The reason for declining the event is: {{declineReason}}</h3><a href='{{url}}' target='_blank' style='background-color: #99BB58; border-radius: 10px; padding: 10px 20px; border: none; cursor: pointer;>Click here to view the even</button></a></body></html>",
                            Subject = "GatherApp - Your event had been declined",
                            Type = "DeclinedEvent"
                        },
                        new
                        {
                            Id = "48c3ece3-9fe8-42ff-b10a-06b40e8732dc",
                            Body = "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event organizer: {{organizer}}</h2><h2>Event title: {{title}}</h2><h2>Event date: {{date}}</h2><h2>Event time: {{time}}</h2></body></html>",
                            Subject = "GatherApp - Event was cancelled",
                            Type = "CancelledEvent"
                        },
                        new
                        {
                            Id = "64733c66-04af-4932-982a-f4e03c3a0446",
                            Body = "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event title: {{title}}</h2></body></html>",
                            Subject = "GatherApp - Event was updated",
                            Type = "UpdatedEvent"
                        },
                        new
                        {
                            Id = "fef837e1-4958-4295-b809-ec80a105254d",
                            Body = "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event title: {{title}}</h2></br><p>You have been invited to the event.</p></body></html>",
                            Subject = "GatherApp - You have been invited",
                            Type = "EmailInvite"
                        },
                        new
                        {
                            Id = "e6cb9c91-40f8-41e5-b3ad-f65be6de8815",
                            Body = "<html><body style='text-align:center; font-family: Titillium Semibold; color: #383D3F;'><img src='https://www.it-labs.com/wp-content/uploads/2020/09/fb.jpg' alt='Your Image' style='max-width: 200px'><br /><h2>Event title: {{title}}</h2></br><p>You have been removed from the invitees list of the event. If the event is public, you can still respond to the event.</p></body></html>",
                            Subject = "GatherApp - You have been removed from invitees list",
                            Type = "EmailDeletedInvite"
                        });
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Banner")
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EditedBy")
                        .HasColumnType("text");

                    b.Property<bool?>("IsInviteOnly")
                        .HasColumnType("boolean");

                    b.Property<string>("LocationUrl")
                        .HasColumnType("text");

                    b.Property<string>("OrganizedBy")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Invitation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("InviteStatus")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("invitations", (string)null);
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.PasswordToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("passwordToken", (string)null);
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "93c85bae-ca8b-441c-9a51-a8398839ff3e",
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = "2be3422b-1a71-44f1-8b1a-3719f9d1e21e",
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Token", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tokens", (string)null);
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EditedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Oid")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Invitation", b =>
                {
                    b.HasOne("GatherApp.Contracts.Entities.Event", "Event")
                        .WithMany("Invitations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GatherApp.Contracts.Entities.User", "User")
                        .WithMany("Invitations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Token", b =>
                {
                    b.HasOne("GatherApp.Contracts.Entities.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.User", b =>
                {
                    b.HasOne("GatherApp.Contracts.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Event", b =>
                {
                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("GatherApp.Contracts.Entities.User", b =>
                {
                    b.Navigation("Invitations");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
