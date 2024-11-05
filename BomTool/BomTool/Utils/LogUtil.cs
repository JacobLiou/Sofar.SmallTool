using System.Collections.ObjectModel;
using System.Text.Json;

namespace BomTool.Utils
{
    internal class LogUtil
    {
        public static void LogObservableCollection<T>(ObservableCollection<T> oc)
        {
            string result = string.Join(", ", oc);
            NLogManager.Info($"ObservableCollection contents: [{result}]");
        }

        public static void LogObservableJson<T>(ObservableCollection<T> oc)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true    // 格式化输出
            };

            string json = JsonSerializer.Serialize(oc, options);
            NLogManager.Info($"ObservableCollection contents:\n{json}");
        }

        public static void LogList<T>(List<T> list)
        {
            string result = string.Join(", ", list);
            NLogManager.Info($"List contents: [{result}]");
        }

        public static void LogListJson<T>(List<T> list)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true    // 格式化输出
            };

            string json = JsonSerializer.Serialize(list, options);
            NLogManager.Info($"List contents:\n{json}");
        }
    }
}
