using NotePadTests.Models;
using System;
using System.IO;
using System.Text.Json;

namespace NotePadTests.Utilities
{
    public static class FileHandler
    {
        static string FilePath = "C:\\Users\\Sruthi.Subaraja\\Desktop\\Dhivakar\\TestData\\TestLog\\" + $"TestData[{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}].txt";

        public static FolderInfo FetchConfigFileData(string configFilePath)
        {
            string configFileData;
            FolderInfo configData;

            using (FileStream configFileStream = new FileStream(configFilePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader configStreamReader = new StreamReader(configFileStream))
                {
                    configFileData = configStreamReader.ReadToEnd();
                }
            }

            try
            {
                configData = JsonSerializer.Deserialize<FolderInfo>(configFileData);
            }
            catch (JsonException ex)
            {
                configData = null;
                LogTestData("JsonException thrown while Deserializing the config file, Check whether the file's data is in JSON format");
            }
            catch(Exception ex)
            {
                configData = null;
                LogTestData($"{ex.Message} thrown while Deserializing");
            }

            return configData;
        }

        public static void LogTestData(string log)
        {
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(log);
                }
            }
        }

    }
}
