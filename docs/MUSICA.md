# Aporte de musica de fondo desde el clon

Esta es una tarea pendiente para un integrante. La base actual sigue sin musica.
No se ha elegido ni probado una pista. El siguiente codigo es una propuesta
que el responsable debe comprender y probar en Unity antes de su commit.

## 1. Crear una rama

Desde la raiz del clon, en la terminal de VS Code y con los cambios anteriores guardados:

```powershell
git switch main
git pull --ff-only origin main
git switch -c feature/music
```

Si el mismo compañero hace el menu, primero debe terminar y subir su rama
feature/menu. Luego vuelve a main y crea feature/music. Son dos aportes separados.

## 2. Preparar el audio

Crear `Assets/Resources/Audio/` y colocar una pista corta en:

```text
Assets/Resources/Audio/background.ogg
```

Usar una pista propia o cuya licencia permita incluirla en este repositorio.
Mantener un solo archivo con el nombre background. Tambien se puede usar
background.wav o background.mp3; el codigo carga el nombre sin extension.
Guardar la fuente, autor y licencia en `Assets/Resources/Audio/CREDITOS.txt`.

Abrir el proyecto desde Unity Hub con Unity 6000.3.16f1. Unity generara los
archivos .meta del audio y las carpetas. En el Inspector, usar Load Type
Decompress On Load para una pista corta, activar Preload Audio Data y pulsar Apply.
Para un bucle continuo, elegir o editar una pista cuyos extremos conecten bien.

## 3. Crear un script independiente

Crear `Assets/Snake/Audio/BackgroundMusic.cs` desde VS Code:

```csharp
using UnityEngine;

namespace SnakeGame
{
    public static class BackgroundMusic
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void StartMusic()
        {
            AudioClip clip = Resources.Load<AudioClip>("Audio/background");
            if (clip == null)
            {
                Debug.LogWarning("Falta la pista Resources/Audio/background.");
                return;
            }

            var player = new GameObject("BackgroundMusic");
            Object.DontDestroyOnLoad(player);
            var source = player.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = clip;
            source.loop = true;
            source.volume = 0.12f;
            source.spatialBlend = 0f;
            source.Play();
        }
    }
}
```

El metodo se ejecuta al iniciar la aplicacion o entrar a Play. Crea un objeto
independiente que permanece al cambiar de escena. No hay que arrastrar el
script a una escena: la clase es estatica. La musica sonara en Menu y Game,
tambien en la pantalla final, sin reiniciarse al volver al menu o reiniciar
la partida. Una nueva ejecucion de la aplicacion empezara la pista de nuevo.

Este script queda dentro de Snake.Runtime y no necesita paquetes nuevos.
No editar MenuScreen.cs, GameScreen.cs, escenas, Packages ni ProjectSettings.
El archivo de audio solo, sin el script, no hara sonar musica.

Referencia: [inicio automatico del runtime](https://docs.unity3d.com/ja/current/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html),
[AudioSource](https://docs.unity3d.com/cn/6000.0/ScriptReference/AudioSource.html) y
[DontDestroyOnLoad](https://docs.unity3d.com/jp/current/ScriptReference/Object.DontDestroyOnLoad.html).

## 4. Probar el aporte

1. Abrir Menu.unity y pulsar Play: debe sonar una sola pista, a volumen bajo.
2. Pulsar Iniciar: la pista debe continuar y los efectos de comer/perder deben escucharse.
3. Reiniciar varias veces y regresar a Menu: no debe duplicarse ni reiniciarse la musica.
4. Detener Play: la musica debe detenerse. Entrar otra vez: debe empezar una sola pista.
5. Revisar Console: no debe haber errores de compilacion ni la advertencia de pista ausente.
6. Crear el ejecutable Windows y comprobar que incluya la musica.

## 5. Hacer el commit y subir

Despues de importar y probar en Unity (las carpetas y sus .meta ya deben existir):

```powershell
git status
git add Assets/Snake/Audio Assets/Snake/Audio.meta Assets/Resources/Audio Assets/Resources/Audio.meta
git diff --cached --stat
git diff --cached -- Assets/Snake/Audio/BackgroundMusic.cs
git commit -m "Agregar musica de fondo continua y creditos de la pista"
git push -u origin feature/music
```

Abrir GitHub → Compare & pull request → base main, compare feature/music.
Describir la pista usada, sus creditos, los cambios de escena probados y el resultado.
El responsable de integracion revisa el aporte y hace Merge pull request.

## Prompt para recibir ayuda

```text
Mi aporte al proyecto Snake es la musica de fondo. Estoy en feature/music.
Lee docs/MUSICA.md y explicame como funciona su propuesta de script.
Ayudame a integrar mi propia pista en Resources/Audio/background.ogg y
a crear BackgroundMusic.cs en Assets/Snake/Audio sin modificar escenas
ni los scripts del menu o del juego. Debe sonar una sola pista continua,
en bucle y con volumen 0.12, durante Menu y Game. Debe permanecer al
reiniciar una partida y al volver al menu. Incluye los creditos de mi pista.
Ayudame a revisar el diff y a hacer mis propias pruebas antes del commit.
No afirmes que hemos probado audio o ejecutable si no lo comprobamos.
```
