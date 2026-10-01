# Responsable — tu parte e integracion

El asistente prepara y sube tu parte usando la cuenta autorizada en esta
laptop y la identidad Git del responsable. Estos commits declaran la ayuda
de IA y la reutilizacion del proyecto anterior. No se atribuyen a tus compañeros.

## Parte inicial

- Plantilla Unity 6000.3.16f1 y exclusiones de Git.
- SnakeModel: lista del cuerpo, direccion, comida, puntos y colisiones.
- GameScreen: teclado, dibujo, efectos y reinicio; funciona sin Menu.
- Escena Game, configuracion de compilacion y herramientas Snake del editor.
- Pruebas de reglas y guias de entrega.

La base inicial comienza directamente en Game. Cuando se integre Menu y
se habilite su escena, los botones de regreso y Esc estaran disponibles.

## Invitar a los compañeros

En https://github.com/lpzealexjoelquispeti-dot/JuegoSnake- → Settings →
Collaborators → Add people. Usar sus usuarios de GitHub. Ellos aceptan la
invitacion; no necesitas sus contrasenas. Los permisos del repo anterior
no se copian automaticamente.

Enviar a A el enlace de PERSONA_A.md y a B el de PERSONA_B.md. Ambos clonan
JuegoSnake-, trabajan en sus ramas y hacen sus propios commits y push.

## Revisar los pull requests

Comprobar que el PR de menu contiene solo su script, escena, .meta y el
orden de escenas; que el de sprites contiene solo los PNG y .png.meta;
y que el de musica contiene su script, audio, creditos y .meta.

Leer su descripcion de cambios y pruebas. Usar **Create a merge commit**
en GitHub, para conservar los commits individuales. Puedes compartir los
enlaces de los PR con el asistente para solicitar revision antes del merge.

Despues de cada merge, en esta carpeta o en tu clon:

```powershell
git switch main
git pull --ff-only origin main
git log --oneline --graph --decorate --all
```

Luego abrir Unity y probar el conjunto. Despues del menu, comprobar Menu →
Iniciar → Game → perder → Reiniciar → Menu y Esc. Despues de los sprites,
comprobar orientacion y crecimiento. Despues de la musica, comprobar que
continua una sola pista al cambiar de escena.

Crear de nuevo el ejecutable Windows final. Entregar toda su carpeta.
No entregar el ejecutable anterior que todavia no incluye sus aportes.

Cada integrante debe poder explicar su parte; el historial respalda la
integracion real, no convierte codigo importado en desarrollo propio.
