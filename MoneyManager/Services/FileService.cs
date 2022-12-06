  using MoneyManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoneyManager.Services
{
    public class FileService : IFileService
    {
        public void Save(UserModel userModel)
        {
            using (FileStream fs = new FileStream(@$"C:\Money Manager\Users\{userModel.Id}\{userModel.Id}.json", FileMode.OpenOrCreate))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, userModel);
                }
            }
        }
        public void Create(UserModel userModel)
        {
            DirectoryInfo mainDirectoryControl = new DirectoryInfo(@$"C:\Money Manager\Users\{userModel.Id}");
            if (!mainDirectoryControl.Exists) { mainDirectoryControl.Create(); }
            using (FileStream fs = new FileStream(@$"C:\Money Manager\Users\{userModel.Id}\{userModel.Id}.json", FileMode.OpenOrCreate))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, userModel);
                }
            }
        }
        public void SaveUsersList()
        {
            using (FileStream fs = new FileStream(@$"C:\Money Manager\Common\Users List.json", FileMode. Create))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, App.usersList);
                }
            }
        }
        public void SaveCurrentUser()
        {
            using (FileStream fs = new FileStream(@$"C:\Money Manager\Common\Current User.json", FileMode.Create))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, App.currentUser);
                }
            }
        }
        public void SaveHistory(String path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, App.history);
                }
            }
        }
        public void SaveCurrentUserById()
        {
            using (FileStream fs = new FileStream(@$"C:\Money Manager\Users\{App.currentUser.Id}\{App.currentUser.Id}.json", FileMode.Create))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, App.currentUser);
                }
            }
        }
        public void SaveId(UInt64 Id, String path)
        {
            using (StreamWriter newTask = new StreamWriter(path, false))
            {
                newTask.WriteLine(Id.ToString());
            }
        } 
        public void Recreate(UserModel userModel)
        {
            using (FileStream fs = new FileStream(@$"C:\Money Manager\Users\{userModel.Id}\{userModel.Id}.json", FileMode.Create))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, userModel);
                }
            }
        }
        public void Refresh(ref UserModel userModel)
        {
            using (StreamReader sr = new StreamReader(@$"C:\Money Manager\Users\{userModel.Id}\{userModel.Id}.json"))
            {
                String json = sr.ReadToEnd();
                if (json != "") userModel = JsonConvert.DeserializeObject<UserModel>(json);
            }
        }
        public static void RefreshUsersList()
        {
            using (StreamReader sr = new StreamReader(@$"C:\Money Manager\Common\Users List.json"))
            {
                String json = sr.ReadToEnd();
                if(json != "") App.usersList = JsonConvert.DeserializeObject<List<UserModel>>(json);
            }
        }
        public static void OnStartUp()
        {
            DirectoryInfo mainDirectoryControl = new DirectoryInfo(@"C:\Money Manager");
            if (!mainDirectoryControl.Exists) { mainDirectoryControl.Create(); }
            DirectoryInfo userDirectoryControl = new DirectoryInfo(@"C:\Money Manager\Users");
            if (!userDirectoryControl.Exists) { userDirectoryControl.Create(); }
            DirectoryInfo commonDirectoryControl = new DirectoryInfo(@"C:\Money Manager\Common");
            if (!commonDirectoryControl.Exists) { commonDirectoryControl.Create(); }
            if (!File.Exists(@"C:\Money Manager\Common\Users List.json"))
            {
                using (FileStream fs = new FileStream(@"C:\Money Manager\Common\Users List.json", FileMode.OpenOrCreate))
                {
                    using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                    {
                        foreach (var elem in App.usersList) System.Text.Json.JsonSerializer.Serialize(writer, elem);
                    }
                }
            } else RefreshUsersList();
            if (!File.Exists(@"C:\Money Manager\Common\Current User.json"))
            {
                using (FileStream fs = new FileStream(@"C:\Money Manager\Common\Current User.json", FileMode.Create))
                {
                    using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                    {
                        System.Text.Json.JsonSerializer.Serialize(writer, App.currentUser);
                    }
                }
            } else RefreshCurrentUser();
            if (!File.Exists(@"C:\Money Manager\Common\Current Id.txt"))
            {
                using (FileStream fs = new FileStream(@"C:\Money Manager\Common\Current Id.txt", FileMode.Create)) { }

                using (StreamWriter sw = new StreamWriter(@"C:\Money Manager\Common\Current Id.txt"))
                {
                    sw.Write(App.currentId);
                }
            } else RefreshCurrentId();
            if (!File.Exists(@"C:\Money Manager\Common\Current Card Id.txt"))
            {
                using (FileStream fs = new FileStream(@"C:\Money Manager\Common\Current Card Id.txt", FileMode.Create)) { }

                using (StreamWriter sw = new StreamWriter(@"C:\Money Manager\Common\Current Card Id.txt"))
                {
                    sw.Write(App.currentCardId);
                }
            }  else RefreshCurrentCardId();
            if (!File.Exists(@"C:\Money Manager\Common\History.json"))
            {
                using (FileStream fs = new FileStream(@"C:\Money Manager\Common\History.json", FileMode.Create))
                {
                    using (Utf8JsonWriter writer = new Utf8JsonWriter(fs))
                    {
                        System.Text.Json.JsonSerializer.Serialize(writer, App.history);
                    }
                }
            } else RefreshHistory(@"C:\Money Manager\Common\History.json");
        }
        public static void RefreshHistory(String path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                String json = sr.ReadToEnd();
                if (json != "") App.history = JsonConvert.DeserializeObject<List<String>>(json);
            }
        }
        public static void RefreshCurrentId() {
            using (StreamReader sr = new StreamReader(@"C:\Money Manager\Common\Current Id.txt"))
            {
                String s = sr.ReadToEnd();
                UInt64 a;
                if(UInt64.TryParse(s,out a)) App.currentId = a;
            }
        }
        public void RefreshCurrentUser(UInt64 userId)
        {
            using (StreamReader sr = new StreamReader(@$"C:\Money Manager\Users\{userId}\{userId}.json"))
            {
                String json = sr.ReadToEnd();
                if (json != "") App.currentUser = JsonConvert.DeserializeObject<UserModel>(json);
            }
        }
        public static void RefreshCurrentUser()
        {
            using (StreamReader sr = new StreamReader(@"C:\Money Manager\Common\Current User.json"))
            {
                String json = sr.ReadToEnd();
                if (json != "") App.currentUser = JsonConvert.DeserializeObject<UserModel>(json);
            }
        } 
        public static void RefreshCurrentCardId()
        {
            using (StreamReader sr = new StreamReader(@"C:\Money Manager\Common\Current Card Id.txt"))
            {
                String s = sr.ReadToEnd();
                UInt64 a;
                if (UInt64.TryParse(s, out a)) App.currentCardId = a;
            }
        } 
    }
}
