using System;
using System.Collections.Generic;
using System.Threading;

namespace PSPHilosPiedraPapel
{
    class Program
    {
        // Opciones disponibles para el juego
        private static readonly string[] Opciones = { "Piedra", "Papel", "Tijera" };
        // Generador de números aleatorios
        private static readonly Random RandomGenerator = new();
        // Objeto para sincronizar el acceso a la consola
        private static readonly object ConsoleLock = new();

        static void Main()
        {
            // Imprimir encabezado del torneo
            Console.WriteLine("======================================");
            Console.WriteLine("   TORNEO DE PIEDRA, PAPEL O TIJERA   ");
            Console.WriteLine("======================================\n");

            // Lista de jugadores
            List<string> jugadores = new()
            {
                "Jugador 1", "Jugador 2", "Jugador 3", "Jugador 4",
                "Jugador 5", "Jugador 6", "Jugador 7", "Jugador 8",
                "Jugador 9", "Jugador 10", "Jugador 11", "Jugador 12",
                "Jugador 13", "Jugador 14", "Jugador 15", "Jugador 16"
            };

            // Gestionar el torneo y obtener el ganador
            string ganadorTorneo = GestionarTorneo(jugadores);

            // Imprimir el ganador del torneo
            Console.WriteLine($"\nEl ganador del torneo es: {ganadorTorneo}");
        }

        // Método para gestionar el torneo
        private static string GestionarTorneo(List<string> jugadores)
        {
            // Mientras haya más de un jugador, continuar el torneo
            while (jugadores.Count > 1)
            {
                Console.WriteLine("\n======================================");
                Console.WriteLine($"   --- RONDA CON {jugadores.Count} JUGADORES ---   ");
                Console.WriteLine("======================================\n");

                List<string> ganadores = new();

                // Enfrentar a los jugadores de dos en dos
                for (int i = 0; i < jugadores.Count; i += 2)
                {
                    var jugador1 = jugadores[i];
                    var jugador2 = jugadores[i + 1];

                    Console.WriteLine($"Enfrentamiento: {jugador1} vs {jugador2}");
                    string ganador = MejorDeTres(jugador1, jugador2);
                    ganadores.Add(ganador);

                    Console.WriteLine($"Ganador: {ganador}\n");
                }

                // Actualizar la lista de jugadores con los ganadores de la ronda
                jugadores = ganadores;
            }

            // Retornar el último jugador restante como el ganador del torneo
            return jugadores[0];
        }

        // Método para determinar el ganador de un enfrentamiento al mejor de tres
        private static string MejorDeTres(string jugador1, string jugador2)
        {
            int victoriasJugador1 = 0, victoriasJugador2 = 0;
            string jugadaJugador1 = "", jugadaJugador2 = "";

            // Crear hilos para generar las jugadas de los jugadores
            var hilo1 = new Thread(() => { jugadaJugador1 = GenerarJugada(jugador1); });
            var hilo2 = new Thread(() => { jugadaJugador2 = GenerarJugada(jugador2); });

            // Mientras ninguno de los jugadores haya ganado dos partidas
            while (victoriasJugador1 < 2 && victoriasJugador2 < 2)
            {
                hilo1.Start();
                hilo2.Start();

                hilo1.Join();
                hilo2.Join();

                // Comparar las jugadas de los jugadores
                int resultado = CompararJugadas(jugadaJugador1, jugadaJugador2);

                lock (ConsoleLock)
                {
                    Console.WriteLine($"{jugador1} eligió: {jugadaJugador1}");
                    Console.WriteLine($"{jugador2} eligió: {jugadaJugador2}");

                    if (resultado > 0)
                    {
                        victoriasJugador1++;
                        Console.WriteLine($"{jugador1} gana esta partida.");
                    }
                    else if (resultado < 0)
                    {
                        victoriasJugador2++;
                        Console.WriteLine($"{jugador2} gana esta partida.");
                    }
                    else
                    {
                        Console.WriteLine("Empate en esta partida.");
                    }

                    Console.WriteLine($"Marcador: {jugador1} {victoriasJugador1} - {jugador2} {victoriasJugador2}\n");
                }

                // Reiniciar los hilos para la siguiente jugada
                hilo1 = new Thread(() => { jugadaJugador1 = GenerarJugada(jugador1); });
                hilo2 = new Thread(() => { jugadaJugador2 = GenerarJugada(jugador2); });
            }

            // Retornar el jugador con más victorias
            return victoriasJugador1 > victoriasJugador2 ? jugador1 : jugador2;
        }

        // Método para generar una jugada aleatoria
        private static string GenerarJugada(string jugador)
        {
            Thread.Sleep(100); // Simula tiempo para "pensar" la jugada
            return Opciones[RandomGenerator.Next(Opciones.Length)];
        }

        // Método para comparar dos jugadas y determinar el ganador
        private static int CompararJugadas(string jugada1, string jugada2)
        {
            return (jugada1, jugada2) switch
            {
                ("Piedra", "Tijera") or ("Tijera", "Papel") or ("Papel", "Piedra") => 1,
                ("Tijera", "Piedra") or ("Papel", "Tijera") or ("Piedra", "Papel") => -1,
                _ => 0
            };
        }
    }
}