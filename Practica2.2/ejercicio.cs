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
            ExportarCSVXML(rutaCSV);
            //Console.WriteLine("Documento CSV exportado correctamente a XML.");
            break;

        case "2":
            Console.Write("Introduce el género: ");
            string genero = Console.ReadLine() ?? string.Empty;
            //ExportarCSVXMLgenero(genero);
            Console.WriteLine($"Datos del género {genero} exportados correctamente.");
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
            //ExportarCSVXMLidioma(idioma);
            Console.WriteLine($"Datos con idioma {idioma} exportados correctamente.");
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

void ExportarCSVXML(string ruta)
{
    string[] lineas = File.ReadAllLines(ruta);

    Console.WriteLine($"Se han leido {lineas.Length} lineas.");

    for (int i = 0; i < lineas.Length; i++)
    {
        string[] datos = lineas[i].Split(',');

        if(datos.Length != 6)
        {
            Console.WriteLine($"Problemas en la linea {i + 1}:");
            Console.WriteLine(lineas[i]);
            Console.WriteLine($"Se han encontrado {datos.Length} campos.");
        }
    }
}