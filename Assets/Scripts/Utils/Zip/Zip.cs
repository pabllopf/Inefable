//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Zip.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.ZIP
{
    using System.IO;
    using System.IO.Compression;

    /// <summary>Class Zip</summary>
    public class Zip
    {
        /// <summary>Decompresses the specified stream.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="pathSource">The path source.</param>
        /// <param name="pathDestination">The path destination.</param>
        public static void Decompress(Stream stream, string pathSource, string pathDestination)
        {
            FileStream file = File.Create(pathSource);
            stream.CopyTo(file);
            file.Close();

            ZipFile.ExtractToDirectory(pathSource, pathDestination);
            File.Delete(pathSource);
        }

        /// <summary>Decompresses the specified stream.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="pathSource">The path source.</param>
        /// <param name="pathDestination">The path destination.</param>
        /// <param name="deleteZipAfter">if set to <c>true</c> [delete zip after].</param>
        public static void Decompress(Stream stream, string pathSource, string pathDestination, bool deleteZipAfter)
        {
            FileStream file = File.Create(pathSource);
            stream.CopyTo(file);
            file.Close();

            ZipFile.ExtractToDirectory(pathSource, pathDestination);

            if (deleteZipAfter) 
            {
                File.Delete(pathSource);
            }
        }

        /// <summary>Compresses the specified path.</summary>
        /// <param name="path">The path.</param>
        /// <returns>Return the file compressed</returns>
        public static FileStream Compress(string path)
        {
            if (File.Exists(path + ".zip"))
            {
                File.Delete(path + ".zip");
            }
            
            ZipFile.CreateFromDirectory(path, path + ".zip");
            return File.OpenRead(path + ".zip");
        }
    }
}