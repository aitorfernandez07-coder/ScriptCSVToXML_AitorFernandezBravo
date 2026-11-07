using System.Xml;

string opcion = "";
string rutaCSV = "";
string rutaXML = "";
string rutaXMLLectura = "";
string rutaCSVGuardar = "";

while (opcion != "9")
{
    Console.Clear();

    Console.WriteLine("==== MENU DE OPCIONES ====");
    Console.WriteLine("De CSV a XML");
    Console.WriteLine("1. Exportar todo el documento (CSV a XML).");
    Console.WriteLine("2. Exportar solo las de un género.");
    Console.WriteLine("3. Exportar de un año en adelante.");
    Console.WriteLine("4. Exportar solo las de un idioma.");
    Console.WriteLine();
    Console.WriteLine("De XML a CSV");
    Console.WriteLine("5. Exportar todo el documento (XML a CSV).");
    Console.WriteLine("6. Exportar solo las de un género.");
    Console.WriteLine("7. Exportar de un año en adelante.");
    Console.WriteLine("8. Exportar solo las de un idioma.");
    Console.WriteLine();
    Console.WriteLine("9. Cerrar aplicación.");
    Console.WriteLine();
    Console.Write("Seleccione una opción: ");

    opcion = Console.ReadLine() ?? string.Empty;

    if (opcion == "1" || opcion == "2" || opcion == "3" || opcion == "4")
    {
        if (rutaCSV == string.Empty)
        {
            rutaCSV = ObtenerRutaCSV();
        }

        if (rutaCSV != string.Empty && rutaXML == string.Empty)
        {
            rutaXML = ObtenerRutaXML();
        }

        if (rutaCSV == string.Empty || rutaXML == string.Empty)
        {
            Console.WriteLine();
            Console.WriteLine("Pulsa Enter para continuar...");
            Console.ReadKey();

            continue;
        }
    }

    if (opcion == "5" || opcion == "6" || opcion == "7" || opcion == "8")
    {
        if (rutaXMLLectura == string.Empty)
        {
            rutaXMLLectura = ObtenerRutaXMLLectura();
        }

        if (rutaXMLLectura != string.Empty && rutaCSVGuardar == string.Empty)
        {
            rutaCSVGuardar = ObtenerRutaGuardarCSV();
        }

        if (rutaXMLLectura == string.Empty || rutaCSVGuardar == string.Empty)
        {
            Console.WriteLine();
            Console.WriteLine("Pulsa Enter para continuar...");
            Console.ReadKey();

            continue;
        }
    }

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
            string nombreGenero = genero.Replace("/", "-");

            string[] lineasGenero = LeerLineas(rutaCSV, "genero", genero);

            ConvertirXML(lineasGenero, rutaXML, $"NetflixOriginals_{nombreGenero}.xml");

            Console.WriteLine($"Datos del género {genero} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasGenero.Length} películas.");
            break;

        case "3":
            Console.Write("Introduce el año: ");

            int anoNumero;

            while (!int.TryParse(Console.ReadLine(), out anoNumero))
            {
                Console.Write("Año no válido. Introduce el año: ");
            }

            string ano = anoNumero.ToString();
            string[] lineasAno = LeerLineas(rutaCSV, "ano", ano);

            ConvertirXML(lineasAno, rutaXML, $"NetflixOriginals_{ano}.xml");

            Console.WriteLine($"Datos a partir del año {ano} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasAno.Length} películas.");
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
            string nombreIdioma = idioma.Replace("/", "-");

            string[] lineasIdioma = LeerLineas(rutaCSV, "idioma", idioma);

            ConvertirXML(lineasIdioma, rutaXML, $"NetflixOriginals_{nombreIdioma}.xml");

            Console.WriteLine($"Datos con idioma {idioma} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasIdioma.Length} películas.");
            break;

        case "5":
            string[] lineasXML = LeerPeliculasXML(rutaXMLLectura, "todos", "");

            ConvertirCSV(lineasXML, rutaCSVGuardar, "NetflixOriginals_Exportado.csv");

            Console.WriteLine("Documento XML exportado correctamente a CSV.");
            Console.WriteLine($"Se han exportado {lineasXML.Length} películas.");
            break;

        case "6":
            string[] generosXML = ObtenerGenerosXML(rutaXMLLectura);

            Console.WriteLine("=== GÉNEROS DISPONIBLES ===");

            for (int i = 0; i < generosXML.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {generosXML[i]}");
            }

            Console.WriteLine();
            Console.Write("Selecciona un género: ");

            int opcionGeneroXML;

            while (!int.TryParse(Console.ReadLine(), out opcionGeneroXML)
                || opcionGeneroXML < 1
                || opcionGeneroXML > generosXML.Length)
            {
                Console.Write("Opción no válida. Selecciona un género: ");
            }

            string generoXML = generosXML[opcionGeneroXML - 1];
            string nombreGeneroXML = generoXML.Replace("/", "-");

            string[] lineasGeneroXML = LeerPeliculasXML(rutaXMLLectura, "genero", generoXML);

            ConvertirCSV(lineasGeneroXML, rutaCSVGuardar, $"NetflixOriginals_Exportado_{nombreGeneroXML}.csv");

            Console.WriteLine($"Datos del género {generoXML} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasGeneroXML.Length} películas.");
            break;

        case "7":
            Console.Write("Introduce el año: ");

            int anoNumeroXML;

            while (!int.TryParse(Console.ReadLine(), out anoNumeroXML))
            {
                Console.Write("Año no válido. Introduce el año: ");
            }

            string anoXML = anoNumeroXML.ToString();
            string[] lineasAnoXML = LeerPeliculasXML(rutaXMLLectura, "ano", anoXML);

            ConvertirCSV(lineasAnoXML, rutaCSVGuardar, $"NetflixOriginals_Exportado_{anoXML}.csv");

            Console.WriteLine($"Datos a partir del año {anoXML} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasAnoXML.Length} películas.");
            break;

        case "8":
            string[] idiomasXML = ObtenerIdiomasXML(rutaXMLLectura);

            Console.WriteLine("=== IDIOMAS DISPONIBLES ===");

            for (int i = 0; i < idiomasXML.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {idiomasXML[i]}");
            }

            Console.WriteLine();
            Console.Write("Selecciona un idioma: ");

            int opcionIdiomaXML;

            while (!int.TryParse(Console.ReadLine(), out opcionIdiomaXML)
                || opcionIdiomaXML < 1
                || opcionIdiomaXML > idiomasXML.Length)
            {
                Console.Write("Opción no válida. Selecciona un idioma: ");
            }

            string idiomaXML = idiomasXML[opcionIdiomaXML - 1];
            string nombreIdiomaXML = idiomaXML.Replace("/", "-");

            string[] lineasIdiomaXML = LeerPeliculasXML(rutaXMLLectura, "idioma", idiomaXML);

            ConvertirCSV(lineasIdiomaXML, rutaCSVGuardar, $"NetflixOriginals_Exportado_{nombreIdiomaXML}.csv");

            Console.WriteLine($"Datos con idioma {idiomaXML} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasIdiomaXML.Length} películas.");
            break;

        case "9":
            Console.WriteLine("Programa cerrado...");
            break;

        default:
            Console.WriteLine("Opción no valida.");
            break;
    }

    if (opcion != "9")
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

string[] LeerPeliculasXML(string ruta, string tipoFiltro, string filtro)
{
    XmlDocument documento = new XmlDocument();
    documento.Load(ruta);

    XmlNodeList peliculas = documento.GetElementsByTagName("Pelicula");
    List<string> lineas = new List<string>();

    foreach (XmlNode pelicula in peliculas)
    {
        string titulo = pelicula["Title"]?.InnerText ?? "";
        string genero = pelicula["Genre"]?.InnerText ?? "";
        string estreno = pelicula["Premiere"]?.InnerText ?? "";
        string duracion = pelicula["Runtime"]?.InnerText ?? "";
        string puntuacion = pelicula["IMDBScore"]?.InnerText ?? "";
        string idioma = pelicula["Language"]?.InnerText ?? "";

        string linea = $"\"{titulo}\",\"{genero}\",\"{estreno}\",\"{duracion}\",\"{puntuacion}\",\"{idioma}\"";

        if (tipoFiltro == "todos")
        {
            lineas.Add(linea);

        } else if (tipoFiltro == "genero" && genero == filtro)
        {
            lineas.Add(linea);

        } else if (tipoFiltro == "idioma" && idioma == filtro)
        {
            lineas.Add(linea);

        } else if (tipoFiltro == "ano")
        {
            int anoPeli = int.Parse(estreno.Substring(estreno.Length - 4));
            int anoFiltro = int.Parse(filtro);

            if (anoPeli >= anoFiltro)
            {
                lineas.Add(linea);
            }
        }
    }

    return lineas.ToArray();
}

string[] ObtenerGenerosXML(string ruta)
{
    XmlDocument documento = new XmlDocument();
    documento.Load(ruta);

    XmlNodeList peliculas = documento.GetElementsByTagName("Pelicula");
    List<string> generos = new List<string>();

    foreach (XmlNode pelicula in peliculas)
    {
        string genero = pelicula["Genre"]?.InnerText ?? "";

        if (!generos.Contains(genero))
        {
            generos.Add(genero);
        }
    }

    return generos.ToArray();
}

string[] ObtenerIdiomasXML(string ruta)
{
    XmlDocument documento = new XmlDocument();
    documento.Load(ruta);

    XmlNodeList peliculas = documento.GetElementsByTagName("Pelicula");
    List<string> idiomas = new List<string>();

    foreach (XmlNode pelicula in peliculas)
    {
        string idioma = pelicula["Language"]?.InnerText ?? "";

        if (!idiomas.Contains(idioma))
        {
            idiomas.Add(idioma);
        }
    }

    return idiomas.ToArray();
}

void ConvertirCSV(string[] lineas, string rutaGuardar, string nombreCSV)
{
    string rutaCompleta = Path.Combine(rutaGuardar, nombreCSV);

    using (StreamWriter csv = new StreamWriter(rutaCompleta))
    {
        csv.WriteLine("Title,Genre,Premiere,Runtime,IMDB Score,Language");

        foreach (string linea in lineas)
        {
            csv.WriteLine(linea);
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