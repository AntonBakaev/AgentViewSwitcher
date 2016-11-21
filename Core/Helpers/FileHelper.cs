using System.IO;
using System.Text;
using Core.Helpers.Interfaces;

namespace Core.Helpers
{
	public class FileHelper : IFileHelper
	{
		public string ReadFile(string path)
		{
			using (var reader = new StreamReader(File.OpenRead(path)))
			{
				return reader.ReadToEnd();
			}
		}

		public void WriteFile(string path, string newContent)
		{
			File.WriteAllText(path, newContent, Encoding.UTF8);
		}
	}
}
