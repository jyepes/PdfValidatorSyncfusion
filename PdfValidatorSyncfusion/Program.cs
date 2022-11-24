using PdfValidatorSyncfusion.Utils;
using System.Configuration;

try
{
    // leer claves de configuración
    string resultTextFile = "result.txt";
    string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
    string containerName = ConfigurationManager.AppSettings["ContainerName"];

    // crear archivo de texto para escritura
    StreamWriter file = new(resultTextFile);

    // variables
    PdfValidator PdfValidator = new PdfValidator();
    BlobStorage blobStorage = new BlobStorage(connectionString, containerName);
    Stream stream;
    bool isCorrupted;
    int counter = 0;

    Console.WriteLine("Obteniendo archivos PDF...");
    var blobs = await blobStorage.GetAllDocuments(connectionString, containerName);
    Console.WriteLine($"Se van a analizar un total de {blobs.Count} archivos...");
    Console.WriteLine("Iniciando análisis...");

    foreach (string item in blobs)
    {
        counter++;

        stream = await blobStorage.GetDocument(connectionString, containerName, item);

        if (stream != null)
        {
            isCorrupted = PdfValidator.AnalizePdfFile(stream);

            if (isCorrupted)
            {
                Console.WriteLine($"{counter} - {item}: Corrupted");
                file.WriteLine($"{counter} - {item}");
            }
            else
            {
                Console.WriteLine($"{counter} - {item}: Ok");
            }
        }
    }

    //foreach (string item in blobs)
    //{
    //    counter++;
    //    file.WriteLine($"{counter} - {item}");
    //    Console.WriteLine($"{counter} - {item}");
    //}

    file.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Análisis de archivo terminado");
Console.WriteLine("Presione cualquier tecla para terminar...");
Console.ReadLine();

