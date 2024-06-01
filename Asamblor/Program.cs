string filePath = "path/to/your/file.asm";

if (!File.Exists(filePath))
{
    Console.WriteLine("File does not exist.");
}

try
{
    using (StreamReader sr = new StreamReader(filePath))
    {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}
catch (Exception e)
{
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}