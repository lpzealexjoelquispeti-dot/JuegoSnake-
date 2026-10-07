# JUEGO SNAKE 2D

**Universidad:** Universidad Privada Franz Tamayo  
**Materia:** Programación gráfica y Multimedia  
**Integrantes:**

- MARCELO JOSUE ESCOBAR CHIPANA
- ALEX JOEL QUISPE TICONA
- DANIEL GUSTAVO ZAMBRANA RONDON

**Fecha:** 2026

---

## 🐍 Descripción del Proyecto

Este proyecto consiste en una recreación moderna del clásico juego arcade **Snake** desarrollada en el motor **Unity (2D)**. El objetivo del jugador es controlar a la serpiente a través de una cuadrícula, recolectando comida para crecer y sumar puntos, mientras evita colisionar contra los bordes del mapa o contra su propio cuerpo.

El juego está diseñado con una arquitectura desacoplada que separa la lógica de estados y reglas del juego del sistema de renderizado e interfaz gráfica.

---

## 🎮 Características del Juego

- **Personaje Principal (Snake):**
  - Control suave y responsivo en 4 direcciones (arriba, abajo, izquierda, derecha).
  - Representación segmentada compuesta por cabeza, segmentos de cuerpo y cola.
  - Mecánica de crecimiento incremental: cada vez que consume una manzana/comida, el cuerpo se expande una casilla adicional y el puntaje aumenta.
  - Detección precisa de colisiones contra los límites del tablero (cuadrícula de 20x20) y colisión propia (auto-impacto).

- **Menú Principal (`Menu`):**
  - Pantalla inicial con título estilizado del juego.
  - Botón de interacción para iniciar la partida directamente.

- **Escena de Juego (`Game`):**
  - Tablero cuadriculado con diseño dinámico en patrón ajedrezado.
  - Marcador de puntuación en tiempo real.
  - Efectos de sonido procedurales/sintéticos para recolección de comida y fin de partida.
  - Pantalla de Game Over y Victoria con opción de reiniciar partida o volver al menú principal.

- **Controles del Jugador:**
  - **Moverse:** Teclas de flecha (`↑`, `↓`, `←`, `→`) o teclas `W`, `A`, `S`, `D`.
  - **Menú / Pausa:** Tecla `Esc` para retornar al Menú Principal.

---

## 📁 Estructura del Proyecto

El repositorio está organizado de manera modular:

```text
JuegoSnake-/
├── Assets/
│   ├── Scenes/
│   │   ├── Menu.unity          # Escena del Menú Principal
│   │   └── Game.unity          # Escena principal de juego
│   ├── Snake/
│   │   ├── Game/
│   │   │   ├── SnakeModel.cs   # Lógica pura del juego (cuadrícula, colisiones, puntaje)
│   │   │   └── GameScreen.cs   # Renderizado, control de entradas (Input) y audio
│   │   └── Menu/
│   │       └── MenuScreen.cs   # Interfaz gráfica y navegación del Menú
│   ├── Resources/              # Recursos gráficos y sprites
│   └── Tests/                  # Pruebas unitarias de las reglas del juego
├── Executable/                 # Archivos del juego ya compilado para Windows
├── Executable.zip             # Paquete comprimido portable del ejecutable
└── docs/                       # Documentación adicional y guías de estudio
```

---

## 🚀 ¿Cómo Correr el Juego?

El proyecto ya cuenta con versiones compiladas listas para jugar sin necesidad de instalar Unity.

### Opción 1: Ejecutar directamente (Recomendada)

1. Dirígete a la carpeta `Executable/` dentro de este repositorio.
   _(También puedes descomprimir el archivo `Executable.zip` si lo has descargado en otro equipo)_.
2. Localiza el archivo ejecutable:
   ```text
   Snake_Jam.exe
   ```
3. Haz doble clic en **`Snake_Jam.exe`** para iniciar el juego inmediatamente en Windows.

---

### Opción 2: Abrir y ejecutar desde Unity Editor (Desarrollo)

Si deseas visualizar el código fuente, depurar o modificar las escenas:

1. Abre **Unity Hub**.
2. Añade y abre la carpeta del proyecto `JuegoSnake-` (desarrollado en **Unity 6 / 6000.3.16f1**).
3. En la ventana **Project**, navega a `Assets/Scenes/` y abre **`Menu.unity`** (o `Game.unity`).
4. Haz clic en el botón **Play (▶)** en la parte superior del editor para probar el juego en tiempo real.
