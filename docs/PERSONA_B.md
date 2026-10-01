# Persona B — sprites

## 1. Preparar tu laptop

Aceptar la invitacion al repositorio nuevo desde tu propia cuenta.
En una terminal:

```powershell
git clone https://github.com/lpzealexjoelquispeti-dot/JuegoSnake-.git
cd JuegoSnake-
code .
git config user.name "TU NOMBRE"
git config user.email "TU CORREO VINCULADO A GITHUB O TU NOREPLY"
git switch -c feature/sprites
```

Si ya clonaste: guardar el trabajo, `git switch main`,
`git pull --ff-only origin main` y crear feature/sprites. No hacer git init.

## 2. Preparar tus tres sprites

Si ya realizaste los sprites, copia tus PNG y .png.meta desde el respaldo.
La base original no incluye sprites definitivos: los cuadrados visibles
se dibujan por codigo. Si aun no tienes los PNG, debes crearlos.

Seguir [SPRITES.md](SPRITES.md) para los parametros y prompts.
Los archivos finales se colocan en:

```text
Assets/Resources/Sprites/Snake/snake_head.png
Assets/Resources/Sprites/Snake/snake_body.png
Assets/Resources/Sprites/Snake/snake_tail.png
```

Cada archivo es PNG de 32 x 32 con transparencia real. Cabeza mirando a la
derecha; cola con union ancha a la derecha y punta a la izquierda; cuerpo
cuadrado sin direccion. La logica los cargara y rotara automaticamente.
No modificar scripts, escenas, ProjectSettings o Packages.

## 3. Importar y probar en Unity

Unity Hub → Add → carpeta JuegoSnake-, Unity 6000.3.16f1.
En cada PNG: Sprite (2D and UI), Single, 32 Pixels Per Unit, Center (0.5,0.5),
Full Rect, Point, Compression None, Mip Maps desactivado, Wrap Clamp,
Alpha Is Transparency activado. Pulsar Apply. Unity genera los .png.meta.

Abrir Game.unity y pulsar Play. Verificar cabeza, cuerpo, cola y orientacion
al girar. Puedes probar directamente Game aunque el menu aun este pendiente.
Comprobar tambien crecimiento al comer y la pantalla final.

## 4. Hacer tu commit y push

Desde la raiz del clon:

```powershell
git status
git add Assets/Resources/Sprites/Snake/snake_head.png Assets/Resources/Sprites/Snake/snake_head.png.meta Assets/Resources/Sprites/Snake/snake_body.png Assets/Resources/Sprites/Snake/snake_body.png.meta Assets/Resources/Sprites/Snake/snake_tail.png Assets/Resources/Sprites/Snake/snake_tail.png.meta
git diff --cached --stat
git commit -m "Agregar sprites de cabeza cuerpo y cola del Snake"
git push -u origin feature/sprites
```

Autenticarte tu mismo en GitHub. No compartir contrasenas ni tokens.
Abrir pull request con base main, compare feature/sprites. Describir como
creaste los sprites, si usaste IA y las pruebas que realizaste.
El responsable revisara e integrara conservando tus commits.
