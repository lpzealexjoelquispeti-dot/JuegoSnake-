# Juego Snake 2D

Universidad Privada Franz Tamayo

Materia: Programación gráfica y Multimedia

Gestión: 2026

## Integrantes

MARCELO JOSUE ESCOBAR CHIPANA — [@Josue-EC16](https://github.com/Josue-EC16)

ALEX JOEL QUISPE TICONA — [@lpzealexjoelquispeti-dot](https://github.com/lpzealexjoelquispeti-dot)

DANIEL GUSTAVO ZAMBRANA RONDON — [@dani05051234](https://github.com/dani05051234)

## Descripción

Juego Snake para un jugador, desarrollado en Unity 2D, versión 6000.3.16f1. La serpiente se mueve por un tablero de 20 × 20 casillas. Cada comida suma un punto y aumenta su longitud; la partida termina si choca con una pared o con su cuerpo.

El proyecto incluye un menú de inicio, puntuación, reinicio de partida, música de fondo y efectos de sonido al comer y perder. La versión para Windows está en `Executable/` y `Executable.zip`. Los sprites definitivos de cabeza, cuerpo y cola están pendientes de integración.

Las reglas se encuentran en `SnakeModel.cs`. `GameScreen.cs` gestiona el teclado, el tiempo entre pasos y el dibujo del juego. `MenuScreen.cs` muestra la pantalla de inicio.
