<h1 align="center"><img src="/images/razenager.png" alt="razenager"></h1>
<h2 align="center">Razenager-modded v1.1</h2>

## Tabla de contenidos

- [Descripcion](#descripcion)
- [Instalacion](#instalacion)
- [Uso](#uso)

## Descripcion

Razenager te ayuda a obtener la informacion del alumno haciendo uso de la <a href="https://aws.amazon.com/es/what-is/api/">API</a> de la <a href="https://www.upn.edu.pe/">UPN</a>(Universidad Privada del Norte)

## Instalacion

#### Clona este repositorio

```
git clone https://github.com/razeleakers/Razenager-modded.git

```

#### Dependencias

Ve a herramientas -> Administrador de paquetes NuGet -> Consola de administrador de paquetes

```
Update-Package -Reinstall

```

## Uso

Configuracion: Ingresa el token del alumno en el archivo "Web/Config.cs"

<img src="/images/token.png" alt="token">

Importante: Esta es una version de consola, no formulario como el original

<img src="/images/menu.png" alt="menu">

Tendras la opcion de escoger si ver la informacion ordenada en tablas(como los imagenes) o en <a href="https://www.json.org/json-es.html">JSON</a>

- [1] Personal Info: Obtendras sus datos personales: DNI, nombre completo, fecha de nacimiento, contacto personal, etc.
- [2] Historical grades: Obtendras la informacion de los cursos que llevo antes, sus notas y promedio final.
- [3] Current Courses: Obtendras la informacion de los cursos que esta llevando actualmente.

-> Si quieres podras descargar la informacion que se muestre(descarga solo disponible en JSON por ahora)

<div style="display:flex;">
  <img src="/images/personalInformation.png" alt="personalInformation" style="width:45%;">
  <img src="/images/historicalGrades.png" alt="historicalGrades" style="width:45%;">
  <img src="/images/currentCourses.png" alt="currentCourses" style="width:45%;">
  <img src="/images/download.png" alt="download" style="width:45%;">
</div>

##

<h4 align="center">Created by HappyLife modded by M3RFR3T</h1>
