# Persona A — menu y musica opcional

## 1. Preparar tu laptop

Aceptar la invitacion como colaborador de JuegoSnake- en tu propia cuenta.
Abrir una terminal y ejecutar:

```powershell
git clone https://github.com/lpzealexjoelquispeti-dot/JuegoSnake-.git
cd JuegoSnake-
code .
git config user.name "TU NOMBRE"
git config user.email "TU CORREO VINCULADO A GITHUB O TU NOREPLY"
git switch -c feature/menu
```

Si ya clonaste el proyecto, con el trabajo guardado usa `git switch main`,
`git pull --ff-only origin main` y despues `git switch -c feature/menu`.
No volver a usar git init. Los comandos se ejecutan en la raiz del clon,
no dentro de Assets.

## 2. Integrar tu parte del respaldo

El paquete `docs/entregas/persona-a-menu.zip` contiene SOLO el menu de la
base anterior, con sus .meta. Es una referencia reutilizada, preparada con
ayuda de IA, que aun no esta importada en Assets del repositorio nuevo.

Para integrarla desde la terminal de VS Code, con tu rama limpia:

```powershell
Expand-Archive -LiteralPath docs/entregas/persona-a-menu.zip -DestinationPath .
```

Si las rutas de destino ya existen, revisa git status: no sobrescribas
trabajo previo. Tambien puedes abrir el ZIP y copiar sus carpetas Assets a
la raiz del clon conservando todos los .meta.

El paquete agrega:

- `Assets/Snake/Menu/MenuScreen.cs` y `.cs.meta`
- `Assets/Snake/Menu.meta`
- `Assets/Scenes/Menu.unity` y `.unity.meta`

**Tu aporte debe incluir una personalizacion que entiendas**, por ejemplo
ajustar el diseno, las posiciones y los colores manteniendo solo titulo
SNAKE y boton Iniciar. Conserva namespace SnakeGame, clase MenuScreen,
nombre del archivo y GUID del .meta. El boton llama a
`SceneManager.LoadScene("Game")`.

## 3. Configurar y probar en Unity

Unity Hub → Add → carpeta JuegoSnake-. Usar Unity 6000.3.16f1.
Esperar a que importe. Ejecutar **Snake → Preparar escenas (solo si faltan)**.
Este paso incluye Menu y Game y deja Menu como primera escena.

Abrir Menu.unity, Play e Iniciar. Comprobar que aparece Game, que se puede
jugar y que Esc vuelve a Menu. Perder, Reiniciar y volver a Menu con su boton.
No modificar la logica, el archivo SnakeBuild, Packages o los sprites.

## 4. Hacer tu commit y push

```powershell
git status
git add Assets/Snake/Menu Assets/Snake/Menu.meta Assets/Scenes/Menu.unity Assets/Scenes/Menu.unity.meta ProjectSettings/EditorBuildSettings.asset
git diff --cached --stat
git diff --cached -- Assets/Snake/Menu/MenuScreen.cs
git commit -m "Integrar y personalizar menu de inicio desde la base compartida"
git push -u origin feature/menu
```

Si git status muestra otros archivos, revisarlos; no usar git add . ni
agregar cambios ajenos a tu parte. GitHub te pedira autenticarte tu mismo.
No compartir contrasenas ni tokens con otra persona o con la IA.

GitHub → Compare & pull request → base main, compare feature/menu.
Explicar que importaste de la base, que personalizaste y que probaste.
El responsable revisa e integra conservando tus commits.

## 5. Musica opcional, despues del menu

Despues de integrar feature/menu, volver a main y actualizar:

```powershell
git switch main
git pull --ff-only origin main
git switch -c feature/music
```

Seguir [MUSICA.md](MUSICA.md). Se entrega la pista, sus creditos y un script
independiente. La musica no debe duplicarse al reiniciar o cambiar de escena.

## Prompt para recibir ayuda

```text
Estoy en mi clon de https://github.com/lpzealexjoelquispeti-dot/JuegoSnake-.git,
rama feature/menu. Mi tarea es seguir docs/PERSONA_A.md: integrar el menu
del paquete de referencia, entenderlo y personalizarlo. Guiame paso a paso.
Solo modifica MenuScreen.cs, conserva sus nombres y GUID y deja un unico
boton Iniciar que cargue Game. Ayudame a probarlo en Unity y revisar mi diff
antes de mi commit. No hagas cambios en logica, sprites o paquetes, no uses
credenciales de otras personas y no afirmes pruebas que no hicimos.
```
