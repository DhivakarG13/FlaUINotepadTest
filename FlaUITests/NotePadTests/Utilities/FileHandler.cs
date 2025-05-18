using NotePadTests.Models;
using System;
using System.IO;
using System.Text.Json;

namespace NotePadTests.Utilities
{
    /// <summary>
    /// Provides utility methods for handling file operations.
    /// </summary>
    public static class FileHandler
    {
        static string FilePath = "C:\\Users\\Sruthi.Subaraja\\Desktop\\Dhivakar\\TestData\\TestLog\\" + $"TestData[{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}].txt";

        /// <summary>
        /// Reads the specified configuration file, deserializes its JSON content, and returns a <see cref="FolderInfo"/> object.
        /// </summary>
        /// <param name="configFilePath">The full path to the configuration file to be read. Must be a valid file path.</param>
        /// <returns>A <see cref="FolderInfo"/> object containing the deserialized data from the configuration file or null if the file content is not in valid JSON format or an error occurs during deserialization.</returns>
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

        /// <summary>
        /// Appends the specified log message to a file.
        /// </summary>
        /// <param name="log">The log message to write to the file. Cannot be null or empty.</param>
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
