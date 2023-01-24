using Core.Entities;
using System.Security.Policy;

namespace Juan.Utilities
{
    public static class Extension
    {
        public  static bool CheckFileType( this IFormFile file,string type)
        {
           return  file.ContentType.Contains(type+"/");
            
        }
        public static bool CheckFileSize( this IFormFile file,int size)
        {
            return file.Length / 1024 > size;
        }
        public static async Task<string>  SaveFileAsync(this IFormFile file,string wwwroot, params string[] folders)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string resultPath = wwwroot;
            foreach (string folder in folders)
            {
                resultPath = Path.Combine(resultPath, folder);
            }
            resultPath = Path.Combine(resultPath, filename); 
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }
            return filename;
        }
    }
}
