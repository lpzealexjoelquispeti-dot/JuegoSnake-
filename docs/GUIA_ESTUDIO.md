# Guia de estudio del codigo de Snake

Esta guia explica la base inicial de JuegoSnake- (commit f990519). Las
lineas citadas corresponden a esa version y pueden cambiar con nuevos aportes.
El menu todavia esta en docs/entregas/persona-a-menu.zip; los sprites PNG y
la musica aun no estan integrados en Assets. El juego inicia en Game.

## 1. Mapa de archivos

| Archivo | Responsabilidad |
|---|---|
| Assets/Snake/Game/SnakeModel.cs | Posiciones, direccion, comida, puntos, crecimiento y colisiones |
| Assets/Snake/Game/GameScreen.cs | Teclado, tiempo, dibujo del tablero, imagenes, efectos y botones finales |
| Assets/Scenes/Game.unity | Guarda los objetos de la escena de juego y la referencia al componente GameScreen |
| Assets/Snake/Menu/MenuScreen.cs, dentro del ZIP | Dibuja el titulo y boton Iniciar y cambia a Game |
| Assets/Scenes/Menu.unity, dentro del ZIP | Escena de inicio con su componente MenuScreen |
| Assets/Resources/Sprites/Snake/ | Carpeta donde la persona B entrega cabeza, cuerpo y cola |
| Assets/Editor/SnakeBuild.cs | Prepara el orden de escenas y crea el ejecutable desde el editor |
| ProjectSettings/EditorBuildSettings.asset | Lista de escenas que se incluyen al compilar |
| Assets/Tests/EditMode/SnakeModelTests.cs | Comprueba cuatro reglas del juego |
| Assets/Snake/Snake.Runtime.asmdef | Agrupa el codigo de Snake y referencia Unity.InputSystem |
| Archivos .meta | Identifican los assets mediante GUID; mantienen referencias y ajustes de importacion |

Los archivos .cs son codigo; .unity son escenas; .png son imagenes.
Los .meta no son funciones de movimiento y deben conservarse con sus assets.

## 2. Como representamos la serpiente

En SnakeModel.cs, linea 11:

```csharp
private readonly List<Vector2Int> cells = new List<Vector2Int>();
```

Es una lista ordenada de casillas. Un Vector2Int guarda dos enteros: x e y.
`cells[0]` es la cabeza, los elementos intermedios son el cuerpo y el ultimo
elemento es la cola. El tablero mide 20 x 20, con coordenadas de 0 a 19.

El constructor (linea 24) empieza con:

```text
Cabeza: (10,10)
Cuerpo: (9,10)
Cola:   (8,10)
Direccion: derecha (1,0)
```

GameScreen.Awake, linea 19, ejecuta `new SnakeModel()` para crear ese estado.
Awake tambien carga las imagenes y prepara los efectos de sonido.

## 3. Como leemos el teclado y elegimos direccion

GameScreen.Update, linea 31, comprueba el teclado en cada frame. Unity
ejecuta Update mientras el componente esta activo. `Keyboard.current`
puede ser null, por eso se comprueba antes de leer las teclas.

| Tecla | Orden enviada | Vector |
|---|---|---|
| Flecha arriba o W | model.Turn(Vector2Int.up) | (0,1) |
| Flecha abajo o S | model.Turn(Vector2Int.down) | (0,-1) |
| Flecha izquierda o A | model.Turn(Vector2Int.left) | (-1,0) |
| Flecha derecha o D | model.Turn(Vector2Int.right) | (1,0) |

`wasPressedThisFrame` detecta una pulsacion nueva. Una vez seleccionada,
la serpiente sigue en esa direccion aunque sueltes la tecla.

SnakeModel.Turn, linea 33, valida y guarda la siguiente direccion en
queuedDirection. No permite una direccion diagonal, invertir el sentido,
girar cuando termino la partida ni guardar dos giros entre pasos.
Por ejemplo, si avanza a la derecha, una orden de izquierda se ignora.

## 4. Cuando y como avanza

GameScreen.cs, linea 11, define `StepSeconds = 0.15f`.
Update acumula `Time.deltaTime`, el tiempo transcurrido entre frames:

```csharp
elapsed += Time.deltaTime;
if (elapsed >= StepSeconds)
{
    elapsed %= StepSeconds;
    if (model.Step()) sound.PlayOneShot(eatSound);
}
```

Este extracto omite la comprobacion del sonido de derrota. Se ejecuta como
maximo un paso por frame; en condiciones normales avanza una casilla
aproximadamente cada 0.15 segundos. Un valor menor aumenta la velocidad.

El movimiento real esta en SnakeModel.Step, linea 43:

```csharp
direction = queuedDirection;
Vector2Int head = cells[0] + direction;
```

Ejemplo: `(10,10) + (1,0) = (11,10)`. La cabeza avanzara una casilla a la derecha.

Despues de comprobar comida y colisiones:

```csharp
cells.Insert(0, head);
if (!eats) cells.RemoveAt(cells.Count - 1);
```

Insert agrega la nueva cabeza al principio. RemoveAt elimina la ultima
casilla si no comio. Asi las posiciones anteriores pasan a ser el cuerpo:

```text
Antes:   [(10,10), (9,10), (8,10)]
Despues: [(11,10), (10,10), (9,10)]
```

Si comio, se conserva la cola y la lista crece en un segmento.

## 5. Que sprite corresponde a cada segmento

GameScreen.Awake, lineas 21-23, carga las texturas por nombre:

```csharp
head = Resources.Load<Texture2D>("Sprites/Snake/snake_head");
body = Resources.Load<Texture2D>("Sprites/Snake/snake_body");
tail = Resources.Load<Texture2D>("Sprites/Snake/snake_tail");
```

Resources.Load usa la ruta relativa a Assets/Resources, sin extension.
Por eso los PNG deben estar en Assets/Resources/Sprites/Snake con esos nombres.
Aunque se importen como sprites, este dibujo carga su Texture2D y usa OnGUI.

En OnGUI (linea 53), el indice elige la imagen: 0 usa head, el ultimo usa
tail y los intermedios usan body. Las imagenes son estaticas: no hay un
Animator ni una animacion de caminar. Las posiciones de cells producen
el movimiento, y OnGUI vuelve a dibujarlas en su nueva ubicacion.

La cabeza usa model.Direction para orientarse. Cada segmento posterior
usa la diferencia con la casilla que tiene delante. Para las texturas
orientadas originalmente a la derecha, el dibujo rota:

| Direccion | Angulo de dibujo |
|---|---|
| Derecha | 0 grados |
| Arriba | -90 grados |
| Abajo | 90 grados |
| Izquierda | 180 grados |

`GUIUtility.RotateAroundPivot` gira el dibujo alrededor del centro de la
casilla. El cuerpo simetrico no necesita una imagen distinta por direccion.
Mientras no existan los PNG, el codigo dibuja cuadrados verdes y ojos.

El metodo Cell (linea 117) convierte coordenadas del tablero a un rectangulo
de 32 x 32 en el diseno base. Invierte y con `19 - cell.y` porque en el
tablero y aumenta hacia arriba, pero en la interfaz aumenta hacia abajo.

## 6. Como reconoce y genera la fruta

SnakeModel.Food guarda una sola casilla. En Step, linea 49:

```csharp
bool eats = head == Food;
```

La fruta se reconoce porque la nueva cabeza tiene las mismas coordenadas.
El codigo no identifica colores o imagenes y no usa un Collider2D para comer.

Si cabeza y fruta estan en (12,10), eats vale true. Entonces se conserva
la cola, Score aumenta en uno y se llama a PlaceFood. Step devuelve true
y GameScreen reproduce el sonido de comer.

PlaceFood (linea 71) recorre las 400 casillas, guarda las que no estan
ocupadas por cells y elige una al azar. Asi la fruta no aparece dentro
de la serpiente. Si la serpiente ocupa todo el tablero, se marca Won y
IsOver y no se intenta generar otra fruta.

GameScreen.OnGUI, lineas 71-73, pinta un cuadrado rojo en la casilla Food.
La base actual no incluye un archivo de imagen de manzana.

## 7. Como detecta colisiones y termina

En SnakeModel.Step, lineas 50-57, se comprueba la nueva cabeza:

- Pared: x o y queda por debajo de 0 o llega a 20.
- Cuerpo: coincide con una casilla que seguira ocupada por la serpiente.

Si no come, la ultima casilla se libera en ese mismo paso; por eso se
excluye la cola que va a desaparecer de la comprobacion. Si come, la cola
permanece y tambien cuenta como ocupada.

Una colision marca IsOver = true. Update deja de llamar a Step y OnGUI
muestra Fin del juego y la puntuacion. Reiniciar recarga Game: Awake
crea otro SnakeModel y los puntos vuelven a cero.

## 8. Como funciona el menu

En la base nueva, MenuScreen.cs y Menu.unity siguen dentro del ZIP de
referencia. Persona A los importa, personaliza y entrega. El script usa
OnGUI para dibujar el titulo y el boton:

```csharp
if (GUI.Button(new Rect(330, 380, 300, 80), "Iniciar", button))
    SceneManager.LoadScene("Game");
```

GUI.Button devuelve true cuando se activa el boton. LoadScene carga la
escena Game; al crearse su GameScreen, Awake inicia la partida.

En GameScreen, MenuAvailable comprueba si Menu esta disponible en la lista
de escenas. Solo entonces Esc y el boton Menu la cargan. Antes de integrar
el aporte A, esos controles estan deshabilitados u ocultos.

SnakeBuild.Setup, linea 16, incluye solo Game si Menu no existe. Si Menu
existe, incluye Menu primero y Game segundo. El orden se guarda en
ProjectSettings/EditorBuildSettings.asset y determina donde inicia el ejecutable.
SnakeBuild es una herramienta del editor: no controla el movimiento durante la partida.

## 9. Como contarlo en la exposicion

> La serpiente se representa como una lista de casillas. GameScreen lee
> el teclado y pide un paso aproximadamente cada 0.15 segundos. SnakeModel
> calcula la nueva cabeza sumando la direccion, compara su casilla con la
> comida y comprueba colisiones. Al avanzar agrega una cabeza y elimina la
> cola; al comer conserva la cola y aumenta los puntos. GameScreen dibuja
> las posiciones con la textura de cabeza, cuerpo o cola y las rota segun
> la direccion. El menu inicia el juego cargando la escena Game.

## 10. Preguntas para practicar sin mirar

1. ¿Que archivo lee el teclado y que archivo calcula la nueva posicion?
2. ¿Que significa que la direccion sea (0,1)?
3. ¿Por que se agrega una cabeza y se elimina una cola?
4. ¿Que cambia al comer y por que crece la serpiente?
5. ¿Por que la fruta no aparece dentro del cuerpo?
6. ¿El sprite provoca movimiento o representa posiciones calculadas?
7. ¿Por que hay que conservar los .meta del menu y de los sprites?
8. ¿Que instruccion ejecuta el boton Iniciar?
9. ¿Que sucede si Menu existe como archivo pero no se habilita en Build Settings?
10. ¿Que variable cambiarias para aumentar la velocidad y en que archivo?

Para estudiar, abre primero SnakeModel.cs, luego GameScreen.cs y despues
el MenuScreen.cs del ZIP. En VS Code, Ctrl+G permite saltar a una linea.
