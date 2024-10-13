using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToAToa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atividade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividade", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Atividade",
                columns: new[] { "Id", "Ativo", "Descricao" },
                values: new object[,]
                {
                    { 1, true, "Participar uma roda de samba" },
                    { 2, true, "Participar uma roda de choro" },
                    { 3, true, "Jogar capoeira" },
                    { 4, true, "Preparar uma feijoada" },
                    { 5, true, "Fazer pão de queijo" },
                    { 6, true, "Enrolar brigadeiro" },
                    { 7, true, "Dançar forró" },
                    { 8, true, "Assistir a um capítulo de uma novela" },
                    { 9, true, "Fazer compras na feira livre" },
                    { 10, true, "Participar de uma festa junina" },
                    { 11, true, "Aprender a tocar cavaquinho" },
                    { 12, true, "Aprender a tocar violão" },
                    { 13, true, "Fazer um churrasco" },
                    { 14, true, "Visitar uma escola de samba" },
                    { 15, true, "Fazer pão de queijo" },
                    { 16, true, "Assistir a um jogo de futebol" },
                    { 17, true, "Assistir a um jogo de futebol de botão" },
                    { 18, true, "Assistir a um jogo de futsal" },
                    { 19, true, "Assistir a um jogo de vôlei" },
                    { 20, true, "Jogar uma partida de futebol" },
                    { 21, true, "Jogar uma partida de futebol de botão" },
                    { 22, true, "Jogar uma partida de futsal" },
                    { 23, true, "Jogar uma partida de vôlei" },
                    { 24, true, "Fazer uma trilha na mata atlântica" },
                    { 25, true, "Fazer uma trilha no pantanal" },
                    { 26, true, "Fazer uma trilha no cerrado" },
                    { 27, true, "Fazer uma trilha na floresta amazônica" },
                    { 28, true, "Fazer uma trilha no pampa" },
                    { 29, true, "Fazer uma trilha na caatinga" },
                    { 30, true, "Fazer um passeio de barco no pantanal" },
                    { 31, true, "Participar de um bloco de carnaval" },
                    { 32, true, "Ir a um festival de música sertaneja" },
                    { 33, true, "Ir a um festival e MPB" },
                    { 34, true, "Visitar uma plantação de soja" },
                    { 35, true, "Visitar uma plantação de milho" },
                    { 36, true, "Visitar uma plantaçao de cana-de-açúcar" },
                    { 37, true, "Visitar uma vinícola" },
                    { 38, true, "Visitar uma fábrica de cachaça" },
                    { 39, true, "Visitar uma fábrica de rapadura" },
                    { 40, true, "Visitar um alambique" },
                    { 41, true, "Visitar um engenho" },
                    { 42, true, "Visitar uma fábrica de cerveja artesanal" },
                    { 43, true, "Visitar um mercado de artesanato" },
                    { 44, true, "Assistir a uma peça de teatro" },
                    { 45, true, "Fazer um passeio de trem" },
                    { 46, true, "Participar de uma corrida de rua" },
                    { 47, true, "Plantar uma árvore" },
                    { 48, true, "Fazer tapioca" },
                    { 49, true, "Visitar uma cachoeira" },
                    { 50, true, "Tomar um cafezinho" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividade");
        }
    }
}
