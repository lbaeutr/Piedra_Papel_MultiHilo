# Piedra, Papel o Tijera Multihilo

Este proyecto es un simulador de un torneo de "Piedra, Papel o Tijera" utilizando múltiples hilos en C#. El torneo enfrenta a 16 jugadores en rondas eliminatorias hasta determinar un ganador.

## Descripción

El programa simula un torneo de "Piedra, Papel o Tijera" donde los jugadores se enfrentan en rondas eliminatorias. Cada enfrentamiento se decide al mejor de tres partidas, y las jugadas de los jugadores se generan de manera aleatoria utilizando hilos para simular el tiempo de "pensar" la jugada.

## Estructura del Código

- **Opciones**: Las opciones disponibles para el juego son "Piedra", "Papel" y "Tijera".
- **RandomGenerator**: Generador de números aleatorios para determinar las jugadas de los jugadores.
- **ConsoleLock**: Objeto para sincronizar el acceso a la consola y evitar problemas de concurrencia.
- **Main**: Método principal que inicia el torneo y muestra el ganador.
- **GestionarTorneo**: Método que gestiona el torneo, enfrentando a los jugadores en rondas eliminatorias.
- **MejorDeTres**: Método que determina el ganador de un enfrentamiento al mejor de tres partidas.
- **GenerarJugada**: Método que genera una jugada aleatoria para un jugador.
- **CompararJugadas**: Método que compara dos jugadas y determina el ganador.
