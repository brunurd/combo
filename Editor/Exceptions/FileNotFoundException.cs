namespace Combo.Editor.Exceptions
{
    public class FileNotFoundException : System.IO.FileNotFoundException
    {
        public FileNotFoundException(string path) : base($"Cannot find a file in the path: {path}")
        {
        }
    }
}