# entrega240910

<img width="441" alt="Diagrama Entidad Relación - MetaBank" src="https://github.com/user-attachments/assets/5174ac56-593b-4557-b165-9f00ae6597db">

...
- por ahora están los archivos de configuración subidos, simplemente para que sea más fácil armar el challenge y evaluarlo.
- me enfoqué principalmente en la arquitectura, haciendo foco en que la estructura se acople a través de interfaces y que los distintos servicios estén loosly coupled. A partir de ahí se puede edificar una batería de tests, cambiar y complicar el modelo de dominio, y darle coherencia al código de cada handler y al manejo de errores (que está en borrador y no abarca todas las aristas en este momento).
- el proyecto incluye seed de datos básicos, algunas tarjetas y todas con claves '1234'
- Con SQLSERVER bastaría con crear migración y actualizar base de datos:
  ```
  > Add-Migration First
  > update-database
  ```

Este sería un primer sprint. Hay mucho por hacer todavía:

- versionado de apis.
- en general revisar TODO de nuevo:
    Coherencia con excepciones
    Estilo
    Arquitectura
    -TODO- necesita varios sprints más para que quede bien lindo.
- Testing en general:
    domain layer unit testing
    app layer unit testing
    integration testing
    functional testing
    architectural testing
- documentación.
