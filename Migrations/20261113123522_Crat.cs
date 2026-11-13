using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuissariatPro.Migrations
{
    /// <inheritdoc />
    public partial class Crat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumeroPiece = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstActif = table.Column<bool>(type: "bit", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    NumeroAgrement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstActive = table.Column<bool>(type: "bit", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Huissiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricule = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LieuNaissance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateAssermentation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    EtudeId = table.Column<int>(type: "int", nullable: false),
                    UserCreation_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huissiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huissiers_Etudes_EtudeId",
                        column: x => x.EtudeId,
                        principalTable: "Etudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDossier = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Objet = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DateOuverture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCloture = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tribunal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Honoraires = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Frais = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    HuissierId = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dossiers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dossiers_Huissiers_HuissierId",
                        column: x => x.HuissierId,
                        principalTable: "Huissiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroActe = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DateActe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSignification = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Destinataire = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LieuSignification = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Contenu = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Cout = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DossierId = table.Column<int>(type: "int", nullable: false),
                    HuissierId = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actes_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actes_Huissiers_HuissierId",
                        column: x => x.HuissierId,
                        principalTable: "Huissiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Audiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAudience = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure = table.Column<TimeSpan>(type: "time", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Tribunal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrdreDuJour = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Resultat = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EstReporte = table.Column<bool>(type: "bit", nullable: false),
                    DossierId = table.Column<int>(type: "int", nullable: false),
                    HuissierId = table.Column<int>(type: "int", nullable: false),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audiences_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Audiences_Huissiers_HuissierId",
                        column: x => x.HuissierId,
                        principalTable: "Huissiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paiements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DatePaiement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Devise = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DossierId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    EstSupprime = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paiements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paiements_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actes_DossierId",
                table: "Actes",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_Actes_HuissierId",
                table: "Actes",
                column: "HuissierId");

            migrationBuilder.CreateIndex(
                name: "IX_Actes_NumeroActe",
                table: "Actes",
                column: "NumeroActe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audiences_DossierId",
                table: "Audiences",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_Audiences_HuissierId",
                table: "Audiences",
                column: "HuissierId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_ClientId",
                table: "Dossiers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_HuissierId",
                table: "Dossiers",
                column: "HuissierId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_NumeroDossier",
                table: "Dossiers",
                column: "NumeroDossier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Huissiers_EtudeId",
                table: "Huissiers",
                column: "EtudeId");

            migrationBuilder.CreateIndex(
                name: "IX_Huissiers_Matricule",
                table: "Huissiers",
                column: "Matricule",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_ClientId",
                table: "Paiements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_DossierId",
                table: "Paiements",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_Reference",
                table: "Paiements",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actes");

            migrationBuilder.DropTable(
                name: "Audiences");

            migrationBuilder.DropTable(
                name: "Paiements");

            migrationBuilder.DropTable(
                name: "Dossiers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Huissiers");

            migrationBuilder.DropTable(
                name: "Etudes");
        }
    }
}
