using System.Text.RegularExpressions;

namespace Engine
{
    public class SaveToFile
    {
        public static string SaveCsv(List<string> content)
        {
            if(!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }

            var path = "output\\export_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";
            
            FixLocale(content);
            File.AppendAllLines(path, content);

            return path;
        }

        private static void FixLocale(List<string> lines)
        {
            for(int i = 0; i < lines.Count; i++)
            {
                lines[i] = FixLineLocale(lines[i]);
            }
        }

        private static string FixLineLocale(string line)
        {
            Regex regex = new Regex(";\\d+,\\d+;");

            while (regex.IsMatch(line))
            {
                var match = regex.Match(line).Value;
                match = match.Replace(',','.');
                line = regex.Replace(line, match, 1);
            }

            return line;
        }
    }
}