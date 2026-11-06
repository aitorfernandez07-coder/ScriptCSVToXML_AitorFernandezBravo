using System.Xml;

string opcion = "";
string rutaCSV = "";

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
            
            ConvertirXML(lineas, "NetflixOriginals.xml");

            Console.WriteLine("Documento CSV exportado correctamente a XML.");
            Console.WriteLine($"Se han exportado {lineas.Length} películas.");
            break;

        case "2":
            Console.Write("Introduce el género: ");

            string genero = Console.ReadLine() ?? string.Empty;
            string[] lineasGenero = LeerLineas(rutaCSV, "genero", genero);

            ConvertirXML(lineasGenero, $"NetflixOriginals_{genero}");

            Console.WriteLine($"Datos del género {genero} exportados correctamente.");
            Console.WriteLine($"Se han exportado {lineasGenero.Length} películas.");
            break;

        case "3":
            Console.Write("Introduce el año: ");
            string ano = Console.ReadLine() ?? string.Empty;
            //ExportarCSVXMLano(ano);
            Console.WriteLine($"Datos a partir del año {ano} exportados correctamente.");
            break;
        
        case "4":
            Console.Write("Introduce el idioma: ");

            string idioma = Console.ReadLine() ?? string.Empty;
            string[] lineasIdioma = LeerLineas(rutaCSV, "idioma", idioma);

            ConvertirXML(lineasIdioma, $"NetflixOriginals_{idioma}");

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
        } else
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

void ConvertirXML (string[] lineas, string nombreXML)
{
    XmlWriterSettings configuracion = new XmlWriterSettings();
    configuracion.Indent = true;

    using (XmlWriter xml = XmlWriter.Create(nombreXML, configuracion))
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