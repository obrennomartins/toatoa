using Microsoft.EntityFrameworkCore;
using ToAToa.Domain.Entities;

namespace ToAToa.DataAccess;

public static class ToAToaDbSeeder
{
    public static void Seeder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atividade>().HasData(
            new Atividade(1, "Participar uma roda de samba"),
            new Atividade(2, "Participar uma roda de choro"),
            new Atividade(3, "Jogar capoeira"),
            new Atividade(4, "Preparar uma feijoada"),
            new Atividade(5, "Fazer pão de queijo"),
            new Atividade(6, "Enrolar brigadeiro"),
            new Atividade(7, "Dançar forró"),
            new Atividade(8, "Assistir a um capítulo de uma novela"),
            new Atividade(9, "Fazer compras na feira livre"),
            new Atividade(10, "Participar de uma festa junina"),
            new Atividade(11, "Aprender a tocar cavaquinho"),
            new Atividade(12, "Aprender a tocar violão"),
            new Atividade(13, "Fazer um churrasco"),
            new Atividade(14, "Visitar uma escola de samba"),
            new Atividade(15, "Fazer pão de queijo"),
            new Atividade(16, "Assistir a um jogo de futebol"),
            new Atividade(17, "Assistir a um jogo de futebol de botão"),
            new Atividade(18, "Assistir a um jogo de futsal"),
            new Atividade(19, "Assistir a um jogo de vôlei"),
            new Atividade(20, "Jogar uma partida de futebol"),
            new Atividade(21, "Jogar uma partida de futebol de botão"),
            new Atividade(22, "Jogar uma partida de futsal"),
            new Atividade(23, "Jogar uma partida de vôlei"),
            new Atividade(24, "Fazer uma trilha na mata atlântica"),
            new Atividade(25, "Fazer uma trilha no pantanal"),
            new Atividade(26, "Fazer uma trilha no cerrado"),
            new Atividade(27, "Fazer uma trilha na floresta amazônica"),
            new Atividade(28, "Fazer uma trilha no pampa"),
            new Atividade(29, "Fazer uma trilha na caatinga"),
            new Atividade(30, "Fazer um passeio de barco no pantanal"),
            new Atividade(31, "Participar de um bloco de carnaval"),
            new Atividade(32, "Ir a um festival de música sertaneja"),
            new Atividade(33, "Ir a um festival e MPB"),
            new Atividade(34, "Visitar uma plantação de soja"),
            new Atividade(35, "Visitar uma plantação de milho"),
            new Atividade(36, "Visitar uma plantaçao de cana-de-açúcar"),
            new Atividade(37, "Visitar uma vinícola"),
            new Atividade(38, "Visitar uma fábrica de cachaça"),
            new Atividade(39, "Visitar uma fábrica de rapadura"),
            new Atividade(40, "Visitar um alambique"),
            new Atividade(41, "Visitar um engenho"),
            new Atividade(42, "Visitar uma fábrica de cerveja artesanal"),
            new Atividade(43, "Visitar um mercado de artesanato"),
            new Atividade(44, "Assistir a uma peça de teatro"),
            new Atividade(45, "Fazer um passeio de trem"),
            new Atividade(46, "Participar de uma corrida de rua"),
            new Atividade(47, "Plantar uma árvore"),
            new Atividade(48, "Fazer tapioca"),
            new Atividade(49, "Visitar uma cachoeira"),
            new Atividade(50, "Tomar um cafezinho")
        );
    }
}
