using System.Xml;

string opcion = "";
string rutaCSV = "";
string rutaXML = "";

while (rutaCSV == string.Empty && opcion != "2")
{
    rutaCSV = ObtenerRutaCSV();

    if (rutaCSV == string.Empty)
    {
        Console.WriteLine("¿Quieres indicar otra ruta?");
        Console.WriteLine("1. SÍ");
        Console.WriteLine("2. NO");
        opcion = Console.ReadLine() ?? string.Empty;

        switch (opcion)
        {
            case "1":
                break;
            
            case "2":
                Console.WriteLine("Programa cerrado...");
                break;

            default:
                Console.WriteLine("Opción no valida.");
                break;
        }
    }
}

if (rutaCSV == string.Empty)
{
    opcion = "5";
}

if (rutaCSV != string.Empty)
{
    rutaXML = ObtenerRutaXML();

    while (rutaXML == string.Empty)
    {
        rutaXML = ObtenerRutaXML();
    }
}

while (opcion != "5")
{
    Console.Clear();

    Console.WriteLine("==== MENU DE OPCIONES ====");
    Console.WriteLine("1. Exportar todo el documento (CSV a XML).");
    Console.WriteLine("2. Exportar solo las de un género.");
    Console.WriteLine("3. Exportar de un año en adelante.");
    Console.WriteLine("4. Exportar solo las de un idioma.");
    Console.WriteLine("5. Cerrar aplicación.");
    Console.WriteLine();
    Console.Write("Seleccione una opción: ");

    opcion = Console.ReadLine() ?? string.Empty;

    switch (opcion)
    {
        case "1":
            string[] lineas = LeerLineas(rutaCSV, "todos", "");
            
            ConvertirXML(lineas, rutaXML, "NetflixOriginals.xml");

            Console.WriteLine("Documento CSV exportado correctamente a XML.");
            Console.WriteLine($"Se han exportado {lineas.Length} películas.");
            break;

        case "2":
            string[] generos = ObtenerGeneros(rutaCSV);

            Console.WriteLine("=== GÉNEROS DISPONIBLES ===");

            for (int i = 0; i < generos.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {generos[i]}");
            }

            Console.WriteLine();
            Console.Write("Selecciona un género: ");

            int opcionGenero;

            while (!int.TryParse(Console.ReadLine(), out opcionGenero) 
                || opcionGenero < 1 
                || opcionGenero > generos.Length)
            {
                Console.Write("Opción no válida. Selecciona un género: ");
            }

            string genero = generos[opcionGenero - 1];

            string[] lineasGenero = LeerLineas(rutaCSV, "genero", genero);

            ConvertirXML(lineasGenero, rutaXML, $"NetflixOriginals_{genero}.xml");

            Console.WriteLine($"Datos del género {genero} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasGenero.Length} películas.");
            break;

        case "3":
            Console.Write("Introduce el año: ");
            
            string ano = Console.ReadLine() ?? string.Empty;
            string[] lineasAno = LeerLineas(rutaCSV, "ano", ano);

            ConvertirXML(lineasAno, rutaXML, $"NetflixOriginals_{ano}.xml");

            Console.WriteLine($"Datos a partir del año {ano} exportados correctamente.");
            break;
        
        case "4":
            string[] idiomas = ObtenerIdiomas(rutaCSV);

            Console.WriteLine("=== IDIOMAS DISPONIBLES ===");

            for (int i = 0; i < idiomas.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {idiomas[i]}");
            }

            Console.WriteLine();
            Console.Write("Selecciona un idioma: ");

            int opcionIdioma;

            while (!int.TryParse(Console.ReadLine(), out opcionIdioma)
                || opcionIdioma < 1
                || opcionIdioma > idiomas.Length)
            {
                Console.Write("Opción no válida. Selecciona un idioma: ");
            }

            string idioma = idiomas[opcionIdioma - 1];

            string[] lineasIdioma = LeerLineas(rutaCSV, "idioma", idioma);

            ConvertirXML(lineasIdioma, rutaXML, $"NetflixOriginals_{idioma}.xml");

            Console.WriteLine($"Datos con idioma {idioma} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasIdioma.Length} películas.");
            break;

        case "5":
            Console.WriteLine("Programa cerrado...");
            break;

        default:
            Console.WriteLine("Opción no valida.");
            break;
    }

    if (opcion != "5")
    {
        Console.WriteLine();
        Console.WriteLine("Pulsa Enter para continuar...");
        Console.ReadKey();
    }
}

string ObtenerRutaCSV()
{
    Console.Write("Introduce la ruta del archivo CSV: ");
    string ruta = (Console.ReadLine() ?? string.Empty).Trim();

    if (ruta == string.Empty)
    {
        Console.WriteLine("La ruta no puede estar vacia.");
        return string.Empty;
    }

    if (!File.Exists(ruta))
    {
        Console.WriteLine("El archivo no existe en esa ruta.");
        return string.Empty;
    }

    return ruta;
}

string ObtenerRutaXML()
{
    Console.Write("Introduce la ruta donde quieres guardar los XML: ");
    string ruta = (Console.ReadLine() ?? string.Empty).Trim();

    if (ruta == string.Empty)
    {
        Console.WriteLine("La ruta no puede estar vacía.");
        return string.Empty;
    }

    if (!Directory.Exists(ruta))
    {
        Console.WriteLine("La carpeta no existe.");
        return string.Empty;
    }

    return ruta;
}

string[] LeerLineas(string ruta, string tipoFiltro, string filtro)
{
    string[] lineasCSV = File.ReadAllLines(ruta);
    List<string> lineas = new List<string>();

    for (int i = 1; i < lineasCSV.Length; i++)
    {
        string[] datos = SepararLineaCSV(lineasCSV[i]);
        
        if (tipoFiltro == "todos")
        {
            lineas.Add(lineasCSV[i]);

        } else if (tipoFiltro == "genero" && datos[1] == filtro)
        {
            lineas.Add(lineasCSV[i]);

        } else if (tipoFiltro == "idioma" && datos[5] == filtro)
        {
            lineas.Add(lineasCSV[i]);

        } else if (tipoFiltro == "ano")
        {
            string fecha = datos[2];
            int anoPeli = int.Parse(fecha.Substring(fecha.Length - 4));
            int anoFiltro = int.Parse(filtro);

            if (anoPeli >= anoFiltro)
            {
                lineas.Add(lineasCSV[i]);
            }
        }
    }

    return lineas.ToArray();
}

string[] ObtenerGeneros(string rutaCSV)
{
    string[] lineasCSV = File.ReadAllLines(rutaCSV);
    List<string> generos = new List<string>();

    for (int i = 1; i < lineasCSV.Length; i++)
    {
        string[] datos = SepararLineaCSV(lineasCSV[i]);
        string genero = datos[1];

        if (!generos.Contains(genero))
        {
            generos.Add(genero);
        }
    }

    return generos.ToArray();
}

string[] ObtenerIdiomas(string rutaCSV)
{
    string[] lineasCSV = File.ReadAllLines(rutaCSV);
    List<string> idiomas = new List<string>();

    for (int i = 1; i < lineasCSV.Length; i++)
    {
        string[] datos = SepararLineaCSV(lineasCSV[i]);

        string idioma = datos[5];

        if (!idiomas.Contains(idioma))
        {
            idiomas.Add(idioma);
        }
    }

    return idiomas.ToArray();
}

string[] SepararLineaCSV(string linea)
{
    string[] datos = new string[6];

    string campoActual = "";
    bool entreComillas = false;
    int posicion = 0;

    foreach (char charact in linea)
    {
        if (charact == '"')
        {
            entreComillas = !entreComillas;
        } else if (charact == ',' && !entreComillas)
        {
            datos[posicion] = campoActual;
            posicion ++;

            campoActual = "";
        } else
        {
            campoActual += charact;
        }
    }

    datos[posicion] = campoActual;

    return datos;
}

void ConvertirXML(string[] lineas, string rutaXML, string nombreXML)
{
    XmlWriterSettings configuracion = new XmlWriterSettings();
    configuracion.Indent = true;

    string rutaCompleta = Path.Combine(rutaXML, nombreXML);

    using (XmlWriter xml = XmlWriter.Create(rutaCompleta, configuracion))
    {
        xml.WriteStartDocument();
        xml.WriteStartElement("Peliculas");

        foreach (string linea in lineas)
        {
            string[] datos = SepararLineaCSV(linea);

            xml.WriteStartElement("Pelicula");

            xml.WriteElementString("Title", datos[0]);
            xml.WriteElementString("Genre", datos[1]);
            xml.WriteElementString("Premiere", datos[2]);
            xml.WriteElementString("Runtime", datos[3]);
            xml.WriteElementString("IMDBScore", datos[4]);
            xml.WriteElementString("Language", datos[5]);

            xml.WriteEndElement();
        }

        xml.WriteEndElement();
        xml.WriteEndDocument();
    }
}

void ConvertirCSV(string rutaXML, string rutaCSV, string nombreCSV)
{
    XmlDocument documento = new XmlDocument();
    documento.Load(rutaXML);

    string rutaCompleta = Path.Combine(rutaCSV, nombreCSV);

    using (StreamWriter csv = new StreamWriter(rutaCompleta))
    {
        csv.WriteLine("Title,Genre,Premiere,Runtime,IMDB Score,Language");

        XmlNodeList peliculas = documento.GetElementsByTagName("Pelicula");

        foreach (XmlNode pelicula in peliculas)
        {
            string titulo = pelicula["Title"]?.InnerText ?? "";
            string genero = pelicula["Genre"]?.InnerText ?? "";
            string estreno = pelicula["Premiere"]?.InnerText ?? "";
            string duracion = pelicula["Runtime"]?.InnerText ?? "";
            string puntuacion = pelicula["IMDBScore"]?.InnerText ?? "";
            string idioma = pelicula["Language"]?.InnerText ?? "";

            csv.WriteLine($"\"{titulo}\",\"{genero}\",\"{estreno}\",\"{duracion}\",\"{puntuacion}\",\"{idioma}\"");
        }
    }
}

string ObtenerRutaXMLLectura()
{
    Console.Write("Introduce la ruta del archivo XML: ");
    string ruta = (Console.ReadLine() ?? string.Empty).Trim();

    if (ruta == string.Empty)
    {
        Console.WriteLine("La ruta no puede estar vacía.");
        return string.Empty;
    }

    if (!File.Exists(ruta))
    {
        Console.WriteLine("El archivo no existe en esa ruta.");
        return string.Empty;
    }

    return ruta;
}

string ObtenerRutaGuardarCSV()
{
    Console.Write("Introduce la ruta donde quieres guardar el CSV: ");
    string ruta = (Console.ReadLine() ?? string.Empty).Trim();

    if (ruta == string.Empty)
    {
        Console.WriteLine("La ruta no puede estar vacía.");
        return string.Empty;
    }

    if (!Directory.Exists(ruta))
    {
        Console.WriteLine("La carpeta no existe.");
        return string.Empty;
    }

    return ruta;
}