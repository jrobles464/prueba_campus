# prueba_campus

## Configurar e instalar el proyecto

1. Clonar el proyecto en alguna ruta local, la de su preferencia.
2. Una vez clonado, abrir una terminal en la ruta raiz del proyecto
3. En la terminal, ejecutar el comando `cd ./PruebaDotnet` para dirigirse a la raíz del proyecto
4. Estando en la raíz del proyecto, ejecutar el comando `dotnet restore` y luego `dotnet run`

Esto debería configurar y crear las dependencias necesarias para la ejecución del proyecto

Antes de pasar a la configuración de la base de datos, deberá nueva base de datos, con el nombre que considere, pero deberá recordarlo para los siguientes pasos de la configuración

## Configurar la base de datos

1. Detener el comando `dotnet run` (Si es que este se sigue ejecutando)
2. Copiar y pegar en la misma ruta el archivo `appsettingsExample.json`
3. Renombrar el archivo pegado como `appsettings.json`
4. Configurar la cadena de conexión a la base de datos en la ruta del proyecto `appsettings.json`. Deberá modificar la propiedad `ConnectionStrings.DefaultConnection`.
5. Debe reemplazar las credenciales por las que necesite. host, clave, usuario, nombre de la base de datos. Si es una conexión a SQL-Server local, bastará con reemplazar la propiedad `Database` de la cadena de conexión

Una vez haya realizado estos pasos, ya podrá ejecutar el proyecto con total libertad

Es importante recordar que las credenciales según el rol son:

```json
{
    "admin":{
        "username": "admin@prueba.com",
        "password": "Admin123"
    },
    "cliente": {
        "username": "cliente@prueba.com",
        "password": "Cliente123"
    }
}
```


## Disclaimers

* Tuve un problema con el inicio de sesión de mi cuenta original, pero esta es [JuanRobles2164](https://github.com/JuanRobles2164)
* Para mejorar la escalabilidad del proyecto, lo correcto sería separar la capa de acceso a datos y la lógica de negocio. Para la capa de acceso a datos usar los archivos en `Repositories` y para la capa de lógica de negocio, usar los archivos de `Domains`.
* Para las request y las response, crear objetos especificos para cada una. De esa manera nos aseguramos de aplicar buenas prácticas y mejorar la arquitectura de la aplicación (Sea la que sea, no solo de esta)
* Debido a la falta de tiempo, no pude implementar la totalidad de estas prácticas, pero igual las dejo anotadas como constancia de que sé aplicarlas
* Tuve un problema con la instalación del paquete del JWT, pero igualmente implementé las funcionalidades como si estuviese todo trabajando correctamente