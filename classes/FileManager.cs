namespace TrainLikeAChampion
{

  public static class FileManager
  {
    public static void SaveToFile<T>(List<T> data, string fileName)
    {
      using (StreamWriter writer = new StreamWriter(fileName))
      {
        foreach (T item in data)
        {
          writer.WriteLine(item.ToString());
        }
      }
    }

    public static List<T> LoadFromFile<T>(string fileName, Func<string, T> parser)
    {
      List<T> data = new List<T>();
      using (StreamReader reader = new StreamReader(fileName))
      {
        while (!reader.EndOfStream)
        {
          string line = reader.ReadLine();
          data.Add(parser(line));
        }
      }
      return data;
    }
  }
}