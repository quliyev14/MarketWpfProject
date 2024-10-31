using System;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MarketWpfProject.Data
{
    public static class DB
    {
        private static readonly object _psro = new object();

        public static IEnumerable<T>? JsonRead<T>(string path) =>
                PathCheck.OpenOrClosed(path)
                ? JsonSerializer.Deserialize<IEnumerable<T>>(File.ReadAllText(path))
                : throw new FileNotFoundException(nameof(path));

        public static void ProductWriteLog<T>(in string log, IEnumerable<T> objects) => WriteLog<T>(log, objects);

        private static void WriteLog<T>(in string log, IEnumerable<T> objects)
        {
            using (var sw = new StreamWriter(log, true))
            {
                var enumerator = objects.GetEnumerator();
                while (enumerator.MoveNext())
                    sw.WriteLine($"{enumerator.Current}");
                enumerator.Dispose();
            }
        }

        public static void JsonWrite<T>(string path, in string log, IEnumerable<T> objects)
        {
            lock (_psro)
            {
                var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
                if (!PathCheck.OpenOrClosed(path))
                {
                    File.WriteAllText(path, JsonSerializer.Serialize(objects, jsonOptions));
                    objects = JsonRead<T>(path)!;
                }
                File.WriteAllText(path, JsonSerializer.Serialize(objects, jsonOptions));
                ProductWriteLog(log, objects);
            }
        }
    }
}
