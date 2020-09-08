using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lojax.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESC = table.Column<string>(maxLength: 60, nullable: false),
                    STATUS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COSTUMERS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(maxLength: 20, nullable: false),
                    LAST_NAME = table.Column<string>(maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(maxLength: 150, nullable: true),
                    ADDRESS = table.Column<string>(maxLength: 300, nullable: true),
                    ADDRESS_NR = table.Column<string>(maxLength: 20, nullable: true),
                    NEIGHBORHOOD = table.Column<string>(maxLength: 100, nullable: true),
                    ZIP_CODE = table.Column<string>(maxLength: 10, nullable: true),
                    PHONE = table.Column<string>(maxLength: 15, nullable: true),
                    STATUS = table.Column<int>(nullable: false),
                    TYPE_ENTITY = table.Column<int>(nullable: false),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COSTUMERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT_METHODS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESC = table.Column<string>(maxLength: 60, nullable: false),
                    INTERVAL = table.Column<int>(nullable: false),
                    REPEAT_NR = table.Column<int>(nullable: false),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT_METHODS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SETTINGS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESC = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETTINGS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(maxLength: 150, nullable: true),
                    USERNAME = table.Column<string>(maxLength: 20, nullable: false),
                    PASSWORD = table.Column<string>(maxLength: 20, nullable: false),
                    STATUS = table.Column<int>(nullable: false),
                    ROLE = table.Column<string>(nullable: true),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(maxLength: 150, nullable: false),
                    DESCRIPTION = table.Column<string>(maxLength: 1024, nullable: true),
                    PRICE = table.Column<decimal>(nullable: false),
                    COST = table.Column<decimal>(nullable: false),
                    ID_CAT = table.Column<int>(nullable: false),
                    STATUS = table.Column<int>(nullable: false),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_CATEGORIES_ID_CAT",
                        column: x => x.ID_CAT,
                        principalTable: "CATEGORIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FINANCE_AP_AR",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORIGIN_STATUS = table.Column<int>(nullable: false),
                    STATUS = table.Column<int>(nullable: false),
                    TYPE = table.Column<int>(nullable: false),
                    ENTITY_ID = table.Column<int>(nullable: false),
                    TOTAL = table.Column<decimal>(nullable: false),
                    PAYMENT_ID = table.Column<int>(nullable: false),
                    NOTE = table.Column<string>(maxLength: 100, nullable: true),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FINANCE_AP_AR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FINANCE_AP_AR_COSTUMERS_ENTITY_ID",
                        column: x => x.ENTITY_ID,
                        principalTable: "COSTUMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FINANCE_AP_AR_PAYMENT_METHODS_PAYMENT_ID",
                        column: x => x.PAYMENT_ID,
                        principalTable: "PAYMENT_METHODS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOCK",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROD_ID = table.Column<int>(nullable: false),
                    QUANTITY = table.Column<decimal>(nullable: false),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOCK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STOCK_PRODUCTS_PROD_ID",
                        column: x => x.PROD_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOCK_MOV",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROD_ID = table.Column<int>(nullable: false),
                    ORIGIN_STATUS = table.Column<int>(nullable: false),
                    DATE_MOV = table.Column<DateTime>(nullable: false),
                    QUANTITY = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOCK_MOV", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STOCK_MOV_PRODUCTS_PROD_ID",
                        column: x => x.PROD_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FINANCE_INSTALLMENT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FINANCE_ID = table.Column<int>(nullable: false),
                    STATUS = table.Column<int>(nullable: false),
                    TOTAL = table.Column<decimal>(nullable: false),
                    PAYMENT_OK = table.Column<decimal>(nullable: false),
                    NOTE = table.Column<string>(maxLength: 100, nullable: true),
                    DATE_DUE = table.Column<DateTime>(nullable: false),
                    DATE_LAST_PAY = table.Column<DateTime>(nullable: false),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false),
                    ENTITY_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FINANCE_INSTALLMENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FINANCE_INSTALLMENT_COSTUMERS_ENTITY_ID",
                        column: x => x.ENTITY_ID,
                        principalTable: "COSTUMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FINANCE_INSTALLMENT_FINANCE_AP_AR_FINANCE_ID",
                        column: x => x.FINANCE_ID,
                        principalTable: "FINANCE_AP_AR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SALES",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COSTUMER_ID = table.Column<int>(nullable: false),
                    FINANCE_AR_ID = table.Column<int>(nullable: false),
                    STATUS = table.Column<int>(nullable: false),
                    TOTAL_SALE = table.Column<decimal>(nullable: false),
                    PAYMENT_ID = table.Column<int>(nullable: false),
                    NOTE = table.Column<string>(maxLength: 100, nullable: true),
                    DATE_SALE = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SALES_COSTUMERS_COSTUMER_ID",
                        column: x => x.COSTUMER_ID,
                        principalTable: "COSTUMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SALES_FINANCE_AP_AR_FINANCE_AR_ID",
                        column: x => x.FINANCE_AR_ID,
                        principalTable: "FINANCE_AP_AR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SALES_PAYMENT_METHODS_PAYMENT_ID",
                        column: x => x.PAYMENT_ID,
                        principalTable: "PAYMENT_METHODS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SALES_PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROD_ID = table.Column<int>(nullable: false),
                    SALE_ID = table.Column<int>(nullable: false),
                    QUANTITY = table.Column<decimal>(nullable: false),
                    PRICE = table.Column<decimal>(nullable: false),
                    TOTAL = table.Column<decimal>(nullable: false),
                    DATE_CREATED = table.Column<DateTime>(nullable: false),
                    DATE_UPDATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALES_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SALES_PRODUCTS_PRODUCTS_PROD_ID",
                        column: x => x.PROD_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SALES_PRODUCTS_SALES_SALE_ID",
                        column: x => x.SALE_ID,
                        principalTable: "SALES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FINANCE_AP_AR_ENTITY_ID",
                table: "FINANCE_AP_AR",
                column: "ENTITY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCE_AP_AR_PAYMENT_ID",
                table: "FINANCE_AP_AR",
                column: "PAYMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCE_INSTALLMENT_ENTITY_ID",
                table: "FINANCE_INSTALLMENT",
                column: "ENTITY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FINANCE_INSTALLMENT_FINANCE_ID",
                table: "FINANCE_INSTALLMENT",
                column: "FINANCE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_ID_CAT",
                table: "PRODUCTS",
                column: "ID_CAT");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_COSTUMER_ID",
                table: "SALES",
                column: "COSTUMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_FINANCE_AR_ID",
                table: "SALES",
                column: "FINANCE_AR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_PAYMENT_ID",
                table: "SALES",
                column: "PAYMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_PRODUCTS_PROD_ID",
                table: "SALES_PRODUCTS",
                column: "PROD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_PRODUCTS_SALE_ID",
                table: "SALES_PRODUCTS",
                column: "SALE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCK_PROD_ID",
                table: "STOCK",
                column: "PROD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCK_MOV_PROD_ID",
                table: "STOCK_MOV",
                column: "PROD_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FINANCE_INSTALLMENT");

            migrationBuilder.DropTable(
                name: "SALES_PRODUCTS");

            migrationBuilder.DropTable(
                name: "SETTINGS");

            migrationBuilder.DropTable(
                name: "STOCK");

            migrationBuilder.DropTable(
                name: "STOCK_MOV");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "SALES");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "FINANCE_AP_AR");

            migrationBuilder.DropTable(
                name: "CATEGORIES");

            migrationBuilder.DropTable(
                name: "COSTUMERS");

            migrationBuilder.DropTable(
                name: "PAYMENT_METHODS");
        }
    }
}
