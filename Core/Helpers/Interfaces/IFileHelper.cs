namespace Core.Helpers.Interfaces
{
	public interface IFileHelper
	{
		string ReadFile(string path);
		void WriteFile(string path, string newContent);
	}
}
