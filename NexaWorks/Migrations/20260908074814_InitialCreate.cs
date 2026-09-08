using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NexaWorks.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusTicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTicket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemsOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemsOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VersionProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersionProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VersionsOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    VersionProductId = table.Column<int>(type: "int", nullable: false),
                    SystemOsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionsOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersionsOs_SystemsOs_SystemOsId",
                        column: x => x.SystemOsId,
                        principalTable: "SystemsOs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VersionsOs_VersionProducts_VersionProductId",
                        column: x => x.VersionProductId,
                        principalTable: "VersionProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StatusTicketId = table.Column<int>(type: "int", nullable: false),
                    VersionOsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_StatusTicket_StatusTicketId",
                        column: x => x.StatusTicketId,
                        principalTable: "StatusTicket",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_VersionsOs_VersionOsId",
                        column: x => x.VersionOsId,
                        principalTable: "VersionsOs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResolutionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resolutions_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "IsDeleted", "NameProduct" },
                values: new object[,]
                {
                    { 1, false, "Trader en Herbe" },
                    { 2, false, "Maître des Investissements" },
                    { 3, false, "Planificateur d’Entraînement" },
                    { 4, false, "Planificateur d’Anxiété Sociale" }
                });

            migrationBuilder.InsertData(
                table: "StatusTicket",
                columns: new[] { "Id", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, false, "En cours" },
                    { 2, false, "Résolu" }
                });

            migrationBuilder.InsertData(
                table: "SystemsOs",
                columns: new[] { "Id", "IsDeleted", "NameSystem" },
                values: new object[,]
                {
                    { 1, false, "Linux" },
                    { 2, false, "MacOS" },
                    { 3, false, "Windows" },
                    { 4, false, "Android" },
                    { 5, false, "iOS" },
                    { 6, false, "Windows Mobile" }
                });

            migrationBuilder.InsertData(
                table: "VersionProducts",
                columns: new[] { "Id", "IsDeleted", "ProductId", "RefVersion" },
                values: new object[,]
                {
                    { 1, false, 1, "1.0" },
                    { 2, false, 1, "1.1" },
                    { 3, false, 1, "1.2" },
                    { 4, false, 1, "1.3" },
                    { 5, false, 2, "1.0" },
                    { 6, false, 2, "2.0" },
                    { 7, false, 2, "2.1" },
                    { 8, false, 3, "1.0" },
                    { 9, false, 3, "1.1" },
                    { 10, false, 3, "2.0" },
                    { 11, false, 4, "1.0" },
                    { 12, false, 4, "1.1" }
                });

            migrationBuilder.InsertData(
                table: "VersionsOs",
                columns: new[] { "Id", "IsDeleted", "SystemOsId", "VersionProductId" },
                values: new object[,]
                {
                    { 1, false, 1, 1 },
                    { 2, false, 3, 1 },
                    { 3, false, 1, 2 },
                    { 4, false, 2, 2 },
                    { 5, false, 3, 2 },
                    { 6, false, 1, 3 },
                    { 7, false, 2, 3 },
                    { 8, false, 3, 3 },
                    { 9, false, 4, 3 },
                    { 10, false, 5, 3 },
                    { 11, false, 6, 3 },
                    { 12, false, 2, 4 },
                    { 13, false, 3, 4 },
                    { 14, false, 4, 4 },
                    { 15, false, 5, 4 },
                    { 16, false, 2, 5 },
                    { 17, false, 5, 5 },
                    { 18, false, 2, 6 },
                    { 19, false, 4, 6 },
                    { 20, false, 5, 6 },
                    { 21, false, 2, 7 },
                    { 22, false, 3, 7 },
                    { 23, false, 4, 7 },
                    { 24, false, 5, 7 },
                    { 25, false, 1, 8 },
                    { 26, false, 2, 8 },
                    { 27, false, 1, 9 },
                    { 28, false, 2, 9 },
                    { 29, false, 3, 9 },
                    { 30, false, 4, 9 },
                    { 31, false, 5, 9 },
                    { 32, false, 6, 9 },
                    { 33, false, 2, 10 },
                    { 34, false, 3, 10 },
                    { 35, false, 4, 10 },
                    { 36, false, 5, 10 },
                    { 37, false, 2, 11 },
                    { 38, false, 3, 11 },
                    { 39, false, 4, 11 },
                    { 40, false, 5, 11 },
                    { 41, false, 2, 12 },
                    { 42, false, 3, 12 },
                    { 43, false, 4, 12 },
                    { 44, false, 5, 12 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreationDate", "Description", "IsDeleted", "StatusTicketId", "VersionOsId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 1, 10), "L'utilisateur indique que le logiciel ne se lance pas du tout lorsqu'il clique sur l'icône depuis son bureau. Rien ne s'affiche à l'écran.", false, 2, 1 },
                    { 2, new DateOnly(2023, 1, 15), "L'utilisateur tente d'importer la liste de ses investissements depuis un fichier texte, mais le logiciel affiche un message d'erreur indiquant que le fichier est illisible.", false, 1, 2 },
                    { 3, new DateOnly(2023, 2, 1), "L'utilisateur n'arrive pas à lire les noms des actions sur son écran car le texte s'affiche en noir sur un fond gris foncé.", false, 2, 3 },
                    { 4, new DateOnly(2023, 3, 10), "L'utilisateur ne reçoit aucune notification sur son ordinateur lorsque le prix d'une action atteint le seuil qu'il a défini.", false, 1, 4 },
                    { 5, new DateOnly(2023, 3, 20), "L'application devient très lente puis se ferme toute seule si on laisse le graphique des prix ouvert pendant plus de 20 minutes.", false, 2, 8 },
                    { 6, new DateOnly(2023, 4, 5), "Lors d'une tentative de vente d'actions, la validation par reconnaissance faciale ne réagit pas et la vente reste bloquée.", false, 1, 11 },
                    { 7, new DateOnly(2023, 4, 12), "L'utilisateur demande à générer son récapitulatif annuel de gains en PDF pour le partager, mais le document généré sort entièrement vierge.", false, 2, 11 },
                    { 8, new DateOnly(2023, 4, 25), "Si l'utilisateur tourne son téléphone sur le côté pour mettre l'écran à l'horizontale pendant le chargement des données, l'écran reste bloqué.", false, 1, 14 },
                    { 9, new DateOnly(2023, 4, 28), "L'utilisateur ne parvient pas à connecter son compte bancaire à l'application. Un message indique que la liaison a échoué.", false, 2, 15 },
                    { 10, new DateOnly(2023, 5, 10), "L'ordinateur de l'utilisateur se met à chauffer fortement et le ventilateur fait du bruit dès que la rubrique \"Dividendes\" reste ouverte.", false, 1, 16 },
                    { 11, new DateOnly(2023, 5, 15), "L'utilisateur tente de modifier son adresse e-mail dans son profil, mais le logiciel lui refuse l'accès en disant qu'il n'a pas les droits nécessaires.", false, 2, 17 },
                    { 12, new DateOnly(2023, 5, 22), "Sur le graphique des prévisions, les montants financiers très élevés sont coupés sur la droite de l'écran et on ne voit pas les derniers chiffres.", false, 1, 19 },
                    { 13, new DateOnly(2023, 5, 28), "L'affichage du cours en temps réel se fige dès que l'utilisateur clique sur une autre fenêtre de son ordinateur.", false, 2, 22 },
                    { 14, new DateOnly(2023, 6, 2), "L'utilisateur n'arrive pas à synchroniser son compte bancaire secondaire avec le logiciel. Le compte n'est pas détecté lors de la recherche.", false, 1, 24 },
                    { 15, new DateOnly(2023, 6, 8), "Le logiciel se ferme brutalement à l'ouverture si aucun casque ou haut-parleur n'est branché sur l'ordinateur.", false, 2, 25 },
                    { 16, new DateOnly(2023, 6, 14), "L'enregistrement du parcours de course à pied s'interrompt brusquement au bout de 10 minutes d'activité et ne trace plus la carte.", false, 1, 32 },
                    { 17, new DateOnly(2023, 6, 18), "Impossible de partager ses séances de sport avec l'application de santé du téléphone, un message bloque l'autorisation.", false, 2, 30 },
                    { 18, new DateOnly(2023, 6, 23), "Les vidéos montrant comment réaliser les étirements refusent de se lancer et affichent un écran noir.", false, 1, 33 },
                    { 19, new DateOnly(2023, 6, 27), "L'utilisateur décoche l'option d'envoi de statistiques anonymes, mais l'option se re-coche toute seule lorsqu'il redémarre le logiciel.", false, 2, 34 },
                    { 20, new DateOnly(2023, 7, 2), "Les rappels quotidiens pour faire les exercices de relaxation arrivent sur le téléphone avec plusieurs heures de retard par rapport à l'heure programmée.", false, 1, 36 },
                    { 21, new DateOnly(2023, 7, 6), "L'utilisateur ne peut plus taper de texte dans son journal personnel dès qu'il atteint une cinquantaine de lignes.", false, 2, 37 },
                    { 22, new DateOnly(2023, 7, 12), "Lors de l'exportation du bilan mensuel sous forme de document, les émojis utilisés pour exprimer l'humeur apparaissent sous forme de petits carrés vides.", false, 1, 39 },
                    { 23, new DateOnly(2023, 7, 16), "L'application se ferme immédiatement si l'utilisateur l'ouvre alors qu'il n'a pas de connexion Internet (Wi-Fi désactivé ou câble débranché).", false, 2, 38 },
                    { 24, new DateOnly(2023, 7, 20), "Sur la page du bilan de la semaine, la courbe d'évolution affiche des symboles incompréhensibles au lieu du pourcentage de réussite.", false, 1, 42 },
                    { 25, new DateOnly(2023, 7, 24), "Lorsque l'utilisateur clique sur le bouton pour exporter ses conseils du jour, rien ne se passe et aucun message n'indique ce qui bloque.", false, 2, 44 }
                });

            migrationBuilder.InsertData(
                table: "Resolutions",
                columns: new[] { "Id", "Description", "IsDeleted", "ResolutionDate", "TicketId" },
                values: new object[,]
                {
                    { 1, "Il manquait un composant système sur l'ordinateur de l'utilisateur. Envoi de la marche à suivre pour installer le composant manquant et mise à jour du guide d'installation.", false, new DateOnly(2023, 1, 12), 1 },
                    { 2, "L'utilisateur avait activé le mode sombre de son ordinateur. Demande envoyée à l'équipe technique pour que les couleurs du logiciel s'adaptent automatiquement au mode sombre.", false, new DateOnly(2023, 2, 5), 3 },
                    { 3, "Le graphique accumulait trop d'informations en mémoire sans se nettoyer. Une mise à jour a été envoyée pour corriger ce comportement.", false, new DateOnly(2023, 3, 22), 5 },
                    { 4, "Un réglage d'affichage empêchait les données d'apparaître sur le document avant impression. Un correctif a été appliqué.", false, new DateOnly(2023, 4, 14), 7 },
                    { 5, "La banque avait changé sa procédure de sécurité. L'application a été mise à jour pour s'adapter aux nouvelles règles de la banque.", false, new DateOnly(2023, 5, 3), 9 },
                    { 6, "La session de l'utilisateur avait expiré en arrière-plan sans l'avertir. L'utilisateur s'est déconnecté puis reconnecté pour valider sa modification.", false, new DateOnly(2023, 5, 17), 11 },
                    { 7, "Le logiciel mettait le flux en pause automatique. Une option a été ajoutée pour permettre au flux de continuer à se mettre à jour en arrière-plan.", false, new DateOnly(2023, 5, 29), 13 },
                    { 8, "Le logiciel cherchait à jouer un son de bienvenue obligatoirement. Le problème a été corrigé pour que le son soit ignoré si aucun matériel audio n'est présent.", false, new DateOnly(2023, 6, 10), 15 },
                    { 9, "L'utilisateur n'avait pas coché la case d'autorisation dans les réglages de son téléphone. Guidage de l'utilisateur pas à pas pour activer l'option.", false, new DateOnly(2023, 6, 19), 17 },
                    { 10, "Un problème de sauvegarde des préférences a été corrigé. Les choix de l'utilisateur sont désormais bien enregistrés.", false, new DateOnly(2023, 6, 28), 19 },
                    { 11, "La zone de texte avait une limite de taille trop petite. La limite a été retirée pour permettre des textes plus longs.", false, new DateOnly(2023, 7, 9), 21 },
                    { 12, "L'application a été adaptée pour pouvoir consulter ses anciens messages même sans connexion Internet.", false, new DateOnly(2023, 7, 18), 23 },
                    { 13, "Aucune application de gestion de fichiers compatible n'était configurée sur le téléphone. Un message explicite a été ajouté pour avertir l'utilisateur d'exporter ou de sauvegarder au format PDF.", false, new DateOnly(2023, 7, 27), 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resolutions_TicketId",
                table: "Resolutions",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StatusTicketId",
                table: "Tickets",
                column: "StatusTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VersionOsId",
                table: "Tickets",
                column: "VersionOsId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionProducts_ProductId",
                table: "VersionProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionsOs_SystemOsId",
                table: "VersionsOs",
                column: "SystemOsId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionsOs_VersionProductId",
                table: "VersionsOs",
                column: "VersionProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "StatusTicket");

            migrationBuilder.DropTable(
                name: "VersionsOs");

            migrationBuilder.DropTable(
                name: "SystemsOs");

            migrationBuilder.DropTable(
                name: "VersionProducts");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
