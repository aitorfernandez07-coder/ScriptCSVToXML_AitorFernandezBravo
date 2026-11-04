# Practica2.2
Practica para la asignatura de Acceso a Datos.

Ejercicio:
La intención de esta práctica es desarrollar un programa para cambiar el formato de un
fichero, en este caso el formato irá de un .csv a un .xml. Para este caso el CSV que vamos
a usar, el cual lo tenéis en Moodle, va sobre una lista de 584 películas que tiene Netflix en
su catálogo, concretamente las películas producidas por la propia Netflix.

El nombre del fichero es NetflixOriginals.csv.

De CSV a XML
Vamos a empezar creando un menú que tendrá las siguientes opciones:
● Exportar todo el documento: Pasa todo el documento CSV a XML.
● Exportar por género: Da a elegir el género que queremos exportar y exporta
únicamente las películas con ese género. La forma de ver qué géneros tiene el CSV
vais a tener que hacerlo en el propio código, o sea, haciendo una primera pasada y
sacando una lista de todos los géneros que hay.
● Exportar por año: Recibe del teclado un año y tiene que exportar las películas de ese
año y superiores.
● Exportar por lenguaje: Funciona igual que exportar por género pero con el idioma.
Junto a esto vamos a desarrollar un apartado que nos permita elegir el path (dirección)
donde tenemos alojado el archivo CSV y otro apartado para elegir donde queremos guardar
el archivo XML.

De XML a CSV
La idea de esta segunda parte de la práctica es hacer el mismo desarrollo pero a la inversa,
o sea hacer la exportación de XML a CSV. Tenéis que implementar tanto la exportación
como las opciones alternativas explicadas en el apartado anterior.

Formato del CSV
Cada línea del CSV está compuesta por los siguientes campos:
● Title: El título de la película.
● Genre: El género al que pertenece esa película.
● Premiere: La fecha de estreno.
● Runtime: El tiempo de duración en minutos.
● IMDB Score: La puntuación en IMDB.
● Language: La lengua en la que se ha grabado.

Formato XML
Como bien sabemos un formato XML bien formado tiene que empezar con un elemento raíz
llamado Peliculas. De ahí cada película empezará por su elemento Pelicula y dentro de esta
cada elemento del CSV tendrá su etiqueta propia.

Entrega
La entrega la haréis vía Github, creando un repositorio llamado
ScriptCSVToXML_NombreApellidos. La URL del repositorio me la publicaréis en Moodle.
Dentro de este repo meteréis el código y los archivos con los datos.
