//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="ZipUtil.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Util.ZIP
{
	using Ionic.Zip;
	using System.IO;

	/// <summary>Zip utils</summary>
	public class ZipUtil
	{
		/// <summary>Unzips the specified zip file path.</summary>
		/// <param name="zipFilePath">The zip file path.</param>
		/// <param name="location">The location.</param>
		public static void Unzip(string zipFilePath, string location)
		{
			Directory.CreateDirectory(location);

			using (ZipFile zip = ZipFile.Read(zipFilePath))
			{
				zip.ExtractAll(location, ExtractExistingFileAction.OverwriteSilently);
			}
		}

		/// <summary>Zips the specified zip file name.</summary>
		/// <param name="zipFileName">Name of the zip file.</param>
		/// <param name="files">The files.</param>
		public static void Zip(string zipFileName, params string[] files)
		{
			string path = Path.GetDirectoryName(zipFileName);
			Directory.CreateDirectory(path);

			using (ZipFile zip = new ZipFile())
			{
				foreach (string file in files)
				{
					zip.AddFile(file, "");
				}
				zip.Save(zipFileName);
			}
		}
	}
}
