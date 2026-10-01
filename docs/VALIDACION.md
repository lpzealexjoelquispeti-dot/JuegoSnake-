# Validacion inicial — 1 de octubre de 2026

La base de JuegoSnake- se comprobo con Unity 6000.3.16f1:

- Compilacion Windows x64: Build Finished, Result: Success; salida de Unity 0.
- Ejecutable creado en Builds/Windows/Snake.exe con sus archivos de soporte.
- Test Runner EditMode: 4 pruebas ejecutadas, 4 aprobadas, 0 fallidas; salida 0.
- La lista de compilacion inicial contiene solo Assets/Scenes/Game.unity.
- No hay dependencia de compilacion de MenuScreen: el menu se entrega despues.

Las cuatro pruebas cubren comida fuera del cuerpo, crecimiento y puntos,
bloqueo de inversion/doble giro y fin por choque con pared.

Registros locales conservados en Logs/base-build.log, Logs/base-tests.log
y Logs/TestResults.xml. Logs y Builds estan excluidos de Git.

Estas verificaciones corresponden a la base importada y adaptada con ayuda
de Codex. Los aportes de menu, sprites y musica todavia requieren sus propias
pruebas y commits desde las cuentas de los compañeros. Se debe generar un
nuevo ejecutable despues de integrarlos.
