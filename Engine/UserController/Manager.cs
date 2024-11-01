using System.Text.Json;

namespace Engine.UserController
{
    public class Manager
    {
        private static string path = Config.UserDirectory;

        public static IEnumerable<string> GetSavedUsers()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Directory.GetFiles(path);
        }

        public static User CreateNewUser()
        {
            User user = new User();

            return user;
        }

        public static User? LoadUser(string path)
        {
            return JsonSerializer.Deserialize<User>(File.ReadAllText(path), Config.JsonOptions);
        }

        public static void Save(User user)
        {
            string stringUser = JsonSerializer.Serialize(user, Config.JsonOptions);

            if (File.Exists(path + "\\" + user.name + ".json"))
            {
                File.Delete(path + "\\" + user.name + ".json");
            }
            File.AppendAllText(path + "\\" + user.name + ".json", stringUser);
        }
    }
}