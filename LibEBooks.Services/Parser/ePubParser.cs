using System;

using Bau.Libraries.LibCompressor;
using Bau.Libraries.LibEBooks.Models.ePub;

namespace Bau.Libraries.LibEBooks.Services.Parser
{
	/// <summary>
	///		Intérprete de archivos OPF
	/// </summary>
	internal class ePubParser
	{
		/// <summary>
		///		Interpreta un archivo OPF
		/// </summary>
		internal ePubEBook Parse(string fileName, string pathTarget)
		{
			ePubEBook book = new ePubEBook();
			Compressor compressor = new Compressor();

				// Descomprime el libro
				compressor.Uncompress(fileName, pathTarget);
				// Interpreta el archivo container.xml
				book.Container = new ePubParserContainer().Parse(pathTarget);
				// Interpreta los metadatos
				new ePubParserPackage().Parse(book, pathTarget);
				// Devuelve el libro
				return book;
		}
	}
}