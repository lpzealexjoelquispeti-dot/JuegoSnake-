# JuegoSnake

Repositorio: https://github.com/lpzealexjoelquispeti-dot/JuegoSnake-

Integrantes:

- MARCELO JOSUE ESCOBAR CHIPANA
- ALEX JOEL QUISPE TICONA
- DANIEL GUSTAVO ZAMBRANA RONDÓN

Unity 2D **6000.3.16f1**. Snake para un jugador. Esta entrega se inicia con
una plantilla importada de Snake- y la logica reutilizada de la base
preparada con ayuda de Codex. El historial nuevo registra esa importacion;
no representa una implementacion desde cero de esos archivos.

## Estado inicial

La parte del responsable de integracion incluye movimiento (flechas/WASD),
comida, puntuacion, crecimiento, colisiones, reinicio y efectos breves.
**Todavia no estan integrados el menu, los sprites definitivos ni la musica.**

Para probar: abrir la carpeta en Unity Hub y luego `Assets/Scenes/Game.unity`.
Pulsar Play. El juego usa cuadrados provisionales. La base inicia en Game;
los controles para volver a Menu apareceran cuando la escena Menu este
integrada y habilitada en Build Settings.

## Reparto

| Persona | Aporte | Rama | Guia |
|---|---|---|---|
| Responsable (tu) | Plantilla, logica, pruebas, configuracion e integracion | feature/game | [TU_APORTE.md](docs/TU_APORTE.md) |
| Persona A | Menu de inicio; musica opcional en otra rama | feature/menu y feature/music | [PERSONA_A.md](docs/PERSONA_A.md) |
| Persona B | Sprites de cabeza, cuerpo y cola | feature/sprites | [PERSONA_B.md](docs/PERSONA_B.md) |

Cada integrante hace sus commits y push desde su propia laptop y cuenta.
Los colaboradores del repositorio anterior deben ser invitados de nuevo
a JuegoSnake- y aceptar esa invitacion.

## Integrar

Despues de integrar el menu, Unity → **Snake → Preparar escenas (solo si faltan)**
coloca Menu primero y Game despues. La persona A entrega tambien el cambio
de `ProjectSettings/EditorBuildSettings.asset` generado por ese paso.

Unity → **Snake → Crear ejecutable Windows** crea `Builds/Windows/Snake.exe`.
Entregar toda la carpeta Windows en un ZIP. Cuando aun falta Menu, el
ejecutable inicia directamente en Game.

Pruebas: Window → General → Test Runner → EditMode → Run All.

Resultado de las comprobaciones iniciales: [VALIDACION.md](docs/VALIDACION.md).

