using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyWpfApp.MeAppClass;

namespace MyWpfApp.Services
{
    internal class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }
        public BindingList<MyAppModel> LoadDate()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<MyAppModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();

                if (fileText.Length > 0)
                    return JsonConvert.DeserializeObject<BindingList<MyAppModel>>(fileText);
                else return new BindingList<MyAppModel>();
            }
        }

        public void SaveDate(object myAppDateList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(myAppDateList);
                writer.Write(output);
            }
        }
    }
}
