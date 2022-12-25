// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer;
using System.Text.Json;

namespace ATCer.Client.MAUI
{
    public class FileReader : IFileReader
    {
        public Task<T> LoadFromJsonAsync<T>(string jsonText)
        {
            try
            {
                var obj = JsonSerializer.Deserialize<T>(jsonText);
                return Task.FromResult(obj);
            }
            catch (Exception ex)
            {
                return Task.FromResult(default(T));
            }
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            #region wpf sample
            //private async Task<string> ReadData()
            //{
            //    using var reader = new StreamReader("wwwroot/data.txt");

            //    return await reader.ReadToEndAsync();
            //}
            #endregion

            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(path);
                using var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
            
        }

        public async Task<T> ReadAsJsonAsync<T>(string path)
        {
            var str = await ReadAllTextAsync(path);
            if (string.IsNullOrWhiteSpace(str))
                return default(T);
            else
                return await LoadFromJsonAsync<T>(str);
        }
    }
}
