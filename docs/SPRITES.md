# Persona 2: sprites compatibles

Entrega estos tres archivos **PNG separados de 32 × 32 pixeles**:

| Nombre exacto | Dibujo | Orientacion original |
|---|---|---|
| `snake_head.png` | Cabeza cuadrada verde con dos ojos negros | Mira a la derecha |
| `snake_body.png` | Segmento cuadrado verde, sin ojos | Simetrico, sin direccion |
| `snake_tail.png` | Segmento de cola verde | Punta a la izquierda, union ancha a la derecha |

Color principal `#73DE63`, borde `#285C2A`, ojos `#111111`. Vista desde arriba, pixel art sencillo, sin sombras, sin animacion. Fondo **realmente transparente**, sin cuadricula dibujada. El cuerpo debe ocupar de x=2 a x=29 y de y=2 a y=29; los dos pixeles exteriores son transparentes. Cabeza y cola deben quedar dentro del mismo recuadro. El codigo rota automaticamente cabeza y cola.

## Prompt para generar cada imagen

Genera cada archivo por separado, cambiando solo la ultima linea:

```text
Crea un unico sprite para un juego Snake 2D muy simple. Vista superior,
pixel art plano, formas cuadradas y faciles de reconocer. Lienzo final
32 x 32 pixeles. Fondo alpha totalmente transparente. Sin texto, letras,
marcas de agua, fondo cuadriculado, sombra, brillo, perspectiva ni animacion.
Color verde principal #73DE63, borde de 1 pixel #285C2A; ojos #111111.
El dibujo debe mantenerse dentro del recuadro x=2..29, y=2..29,
dejando dos pixeles transparentes alrededor. Bordes duros sin suavizado.
Debe combinar visualmente con los otros segmentos de la misma serpiente.
No hagas una serpiente completa ni una hoja con varios sprites.

PIEZA: cabeza cuadrada de serpiente mirando a la DERECHA, con dos ojos
negros pequenos cerca del lado derecho (uno arriba y otro abajo).
Nombre final: snake_head.png.
```

Para el cuerpo reemplaza `PIEZA` y `Nombre final` por:

```text
PIEZA: segmento de cuerpo cuadrado verde uniforme con borde, simetrico,
sin ojos, sin escamas y sin direccion.
Nombre final: snake_body.png.
```

Para la cola reemplazalos por:

```text
PIEZA: cola verde cuya union ancha esta a la DERECHA y cuya punta
esta a la IZQUIERDA; forma sencilla y borde de 1 pixel.
Nombre final: snake_tail.png.
```

**Un prompt no garantiza medidas exactas.** Si la IA exporta a 1024 × 1024 u otro tamaño, recorta el lienzo transparente y exporta cada pieza a 32 × 32 con vecino mas cercano (nearest neighbor). Comprueba las dimensiones y el canal alpha antes de entregar. No basta con escribir .png en el nombre de un JPEG.

## Prompt para la IA de codigo que integra los PNG

```text
Estoy en la rama feature/sprites del repositorio JuegoSnake-.
Mi unica tarea son los sprites. Lee README.md y docs/SPRITES.md.
Coloca las tres imagenes terminadas en:
Assets/Resources/Sprites/Snake/snake_head.png
Assets/Resources/Sprites/Snake/snake_body.png
Assets/Resources/Sprites/Snake/snake_tail.png

Cada PNG debe tener 32 x 32 pixeles, transparencia alpha real y la
orientacion indicada en el documento. Abre Unity 6000.3.16f1 para que
cree los .meta. Configura cada imagen en el Inspector:
Texture Type = Sprite (2D and UI), Sprite Mode = Single,
Pixels Per Unit = 32, Pivot = Center (0.5, 0.5), Mesh Type = Full Rect,
Filter Mode = Point (no filter), Compression = None,
Generate Mip Maps = desactivado, Wrap Mode = Clamp, Alpha Is Transparency = activado.
Pulsa Apply. El juego carga estas texturas automaticamente por nombre.

No cambies scripts, escenas, paquetes, configuracion del proyecto ni README.
Comprueba los sprites iniciando Menu.unity, Play e Iniciar.
Haz commit con los tres PNG y sus tres .png.meta y sube feature/sprites.
No hagas push a main ni merges. Si no puedes abrir Unity, indicalo;
no inventes que probaste los sprites ni sus parametros de importacion.
```
