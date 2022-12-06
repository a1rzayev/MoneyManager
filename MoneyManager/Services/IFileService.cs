using MoneyManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyManager.Services
{
    public interface IFileService
    {
        public void Save(UserModel userModel);
        public void SaveCurrentUser();
        public void SaveUsersList();
        public static void RefreshUsersList() { }
        public void Recreate(UserModel userModel);
        public void Refresh(ref UserModel userModel);
        public void Create(UserModel userModel);
        public static void RefreshCurrentId() { }
        public static void OnStartUp() { }
        public void SaveId(UInt64 Id, String path);
        public void RefreshCurrentUser(UInt64 userId);

        public void SaveCurrentUserById();
        public static void RefreshCurrentCardId() { }

        public static void RefreshCurrentUser() { }
        public static void RefreshHistory(String path) { }
        public void SaveHistory(String path);
    }
}
