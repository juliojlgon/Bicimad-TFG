# Bicimad-TFG
  A continuación se explica un poco la configuración del proyecto.


###Carpetas del proyecto:
  Al ser un proyecto que incluye Frontend, Backend y la APP. Puede resultar algo complicado de entender que es cada cosa.
En principio la carpeta cuenta con distintos archivos. Los unicos que se deben diferenciar son `Bicimad-Tfg-App` y `Bicimad-TFG.sln`.

+ Bicimad-Tfg-App es la carpeta que contiene la Aplicación Android.Esta carpeta deberá poder abrirse usando ´Android Studio´.
+ Bicimad-TFG.sln es el archivo de solucion de Visual Studio. Este archivo es el archivo que une todas las demás carpetas en único 
proyecto y cualquier **IDE compatible con Visual Studio** deberá capaz de interpretar estos archivos sin ningún tipo de problema 
para generar un proyecto.

####En detalle:
+ Bicimad.API: Contiene todos los archivos relacionados con el API.
+ Bicimad.Web: Contiene todos los archivos relacionados con la Web.
+ Bicimad.Services: Contiene todo la logica de acceso al dominio de datos.
+ Bicimad.Core: Contiene todos los objetos de dominio y las sentencias SQL para crear la base de datos.
+ Bicimad.Enums: Carpeta auxiliar que contiene los ´Enums´ usados por ambos proyectos.
+ Bicimad.Mappers: Carpeta auxiliar que contiene mapeadores. Son usados por ambos proyectos.
+ Bicimad.Models: Carpeta auxiliar que contiene ciertos modelos de datos usados por los proyectos.
+ Bicimad.Resources: Contiene los recursos usados por los proyectos. En este caso contiene los textos usados.
+ Bicimad.Helpers: Contiene una serie de Helpers creados para facilitar el uso de ciertos metodos.
 
 
