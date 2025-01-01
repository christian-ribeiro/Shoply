using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoply.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    senha = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    idioma = table.Column<int>(type: "int", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    chave_login = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    codigo_recuperacao_senha = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_usuario_alteracao = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                    table.ForeignKey(
                        name: "fkey_usuario_id_usuario_alteracao",
                        column: x => x.id_usuario_alteracao,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_usuario_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "categoria_produto",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_usuario_alteracao = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoria_produto", x => x.id);
                    table.ForeignKey(
                        name: "fkey_categoria_produto_id_usuario_alteracao",
                        column: x => x.id_usuario_alteracao,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_categoria_produto_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    primeiro_nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    sobrenome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    data_nascimento = table.Column<DateOnly>(type: "date", nullable: true),
                    documento = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    tipo_pessoa = table.Column<int>(type: "int", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_usuario_alteracao = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cliente", x => x.id);
                    table.ForeignKey(
                        name: "fkey_cliente_id_usuario_alteracao",
                        column: x => x.id_usuario_alteracao,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_cliente_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "marca",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_usuario_alteracao = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marca", x => x.id);
                    table.ForeignKey(
                        name: "fkey_marca_id_usuario_alteracao",
                        column: x => x.id_usuario_alteracao,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_marca_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "unidade_medida",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_usuario_alteracao = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unidade_medida", x => x.id);
                    table.ForeignKey(
                        name: "fkey_unidade_medida_id_usuario_alteracao",
                        column: x => x.id_usuario_alteracao,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_unidade_medida_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "endereco_cliente",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cliente = table.Column<long>(type: "bigint", nullable: false),
                    tipo_endereco = table.Column<int>(type: "int", nullable: false),
                    logradouro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    complemento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    bairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cep = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    referencia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    observacao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_usuario_alteracao = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_endereco_cliente", x => x.id);
                    table.ForeignKey(
                        name: "fkey_endereco_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fkey_endereco_cliente_id_usuario_alteracao",
                        column: x => x.id_usuario_alteracao,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_endereco_cliente_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    codigo_barras = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    valor_custo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valor_venda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    id_categoria_produto = table.Column<long>(type: "bigint", nullable: true),
                    id_unidade_medida = table.Column<long>(type: "bigint", nullable: false),
                    id_marca = table.Column<long>(type: "bigint", nullable: false),
                    markup = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_usuario_alteracao = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produto", x => x.id);
                    table.ForeignKey(
                        name: "fkey_produto_id_categoria_produto",
                        column: x => x.id_categoria_produto,
                        principalTable: "categoria_produto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_produto_id_marca",
                        column: x => x.id_marca,
                        principalTable: "marca",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_produto_id_unidade_medida",
                        column: x => x.id_unidade_medida,
                        principalTable: "unidade_medida",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_produto_id_usuario_alteracao",
                        column: x => x.id_usuario_alteracao,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fkey_produto_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "imagem_produto",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_arquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tamanho_arquivo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    link_imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_produto = table.Column<long>(type: "bigint", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usuario_criacao = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imagem_produto", x => x.id);
                    table.ForeignKey(
                        name: "fkey_imagem_produto_id_produto",
                        column: x => x.id_produto,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fkey_imagem_produto_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "id", "data_alteracao", "id_usuario_alteracao", "data_criacao", "id_usuario_criacao", "email", "idioma", "chave_login", "nome", "senha", "codigo_recuperacao_senha", "refresh_token" },
                values: new object[] { 1L, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "default@shoply.com", 0, null, "Usuario Padrão", "$2a$11$252h2vGrxOa1D/ZO.SCreeO3NWC4cSzKJlF.dyzxIQlbJ24ooULO2", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_categoria_produto_id_usuario_alteracao",
                table: "categoria_produto",
                column: "id_usuario_alteracao");

            migrationBuilder.CreateIndex(
                name: "IX_categoria_produto_id_usuario_criacao",
                table: "categoria_produto",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_id_usuario_alteracao",
                table: "cliente",
                column: "id_usuario_alteracao");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_id_usuario_criacao",
                table: "cliente",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_cliente_id_cliente",
                table: "endereco_cliente",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_cliente_id_usuario_alteracao",
                table: "endereco_cliente",
                column: "id_usuario_alteracao");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_cliente_id_usuario_criacao",
                table: "endereco_cliente",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_imagem_produto_id_produto",
                table: "imagem_produto",
                column: "id_produto");

            migrationBuilder.CreateIndex(
                name: "IX_imagem_produto_id_usuario_criacao",
                table: "imagem_produto",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_marca_id_usuario_alteracao",
                table: "marca",
                column: "id_usuario_alteracao");

            migrationBuilder.CreateIndex(
                name: "IX_marca_id_usuario_criacao",
                table: "marca",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_produto_id_categoria_produto",
                table: "produto",
                column: "id_categoria_produto");

            migrationBuilder.CreateIndex(
                name: "IX_produto_id_marca",
                table: "produto",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_produto_id_unidade_medida",
                table: "produto",
                column: "id_unidade_medida");

            migrationBuilder.CreateIndex(
                name: "IX_produto_id_usuario_alteracao",
                table: "produto",
                column: "id_usuario_alteracao");

            migrationBuilder.CreateIndex(
                name: "IX_produto_id_usuario_criacao",
                table: "produto",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_unidade_medida_id_usuario_alteracao",
                table: "unidade_medida",
                column: "id_usuario_alteracao");

            migrationBuilder.CreateIndex(
                name: "IX_unidade_medida_id_usuario_criacao",
                table: "unidade_medida",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_usuario_alteracao",
                table: "usuario",
                column: "id_usuario_alteracao");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_usuario_criacao",
                table: "usuario",
                column: "id_usuario_criacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endereco_cliente");

            migrationBuilder.DropTable(
                name: "imagem_produto");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "categoria_produto");

            migrationBuilder.DropTable(
                name: "marca");

            migrationBuilder.DropTable(
                name: "unidade_medida");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
