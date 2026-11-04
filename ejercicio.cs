string rutaCSV = ObtenerRutaCSV();

string opcion = "";

if (rutaCSV == string.Empty)
{
    return;
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