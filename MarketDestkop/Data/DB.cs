using System.IO;
using System.Text.Json;

namespace MarketWpfProject.Data
{
    public static class DB
    {
        private static readonly object _psro = new object();
        public static void ProductWriteLog<T>(string log, IEnumerable<T> objects) => WriteLog<T>(log, objects);
        private static void WriteLog<T>(string log, IEnumerable<T> objects)
        {
            using (var sw = new StreamWriter(log, true))
            {
                var enumerator = objects.GetEnumerator();
                while (enumerator.MoveNext())
                    sw.WriteLine($"{enumerator.Current}");
                enumerator.Dispose();
            }
        }
        public static async void JsonWrite<T>(string path, IEnumerable<T> obj) =>
               await File.WriteAllTextAsync(path,
                   JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = true }));

        public static List<T> JsonRead<T>(string path)
        {
            if (File.Exists(path))
            {
                var rata = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(rata)) return new();

                else
                {
                    var users = JsonSerializer.Deserialize<List<T>>(rata) ?? new List<T>();
                    return users;
                }
            }
            else return new();
        }
    }
}
