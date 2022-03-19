using System;

using Bau.Libraries.LibEBooks.Models.eBook;
using Bau.Libraries.LibEBooks.Models.ePub;
using Bau.Libraries.LibEBooks.Services.Creator;
using Bau.Libraries.LibEBooks.Services.Parser;

namespace Bau.Libraries.LibEBooks.Services
{
	/// <summary>
	///		Manager de eBooks
	/// </summary>
	public class EBooksManager
	{ 
		// Enumerados públicos
		/// <summary>
		///		Tipo de libro
		/// </summary>
		public enum BookType
		{
			/// <summary>Formato neutral</summary>
			eBookNeutral,
			/// <summary>Formato ePub</summary>
			ePub
		}

		/// <summary>
		///		Carga los datos de un libro
		/// </summary>
		public ePubEBook Load(string fileName, string pathTarget)
		{
			// Crea el directorio
			LibHelper.Files.HelperFiles.MakePath(pathTarget);
			// Carga el archivo
			return new ePubParser().Parse(fileName, pathTarget);
		}

		/// <summary>
		///		Obtiene un <see cref="Book"/> a partir de un archivo
		/// </summary>
		public Book Load(BookType type, string fileName, string pathTarget)
		{
			// Crea el directorio
			LibHelper.Files.HelperFiles.MakePath(pathTarget);
			// Interpreta el archivo
			switch (type)
			{
				case BookType.ePub:
					return Convert(new ePubParser().Parse(fileName, pathTarget));
				default:
					throw new NotImplementedException("Unknown ebook type");
			}
		}

		/// <summary>
		///		Convierte un archivo
		/// </summary>
		public Book Convert(ePubEBook eBook)
		{
			return new ePubConvertEBook().Convert(eBook);
		}

		/// <summary>
		///		Graba un archivo
		/// </summary>
		public void Save(BookType type, Book eBook, string fileName, string pathTemporalFiles)
		{
			switch (type)
			{
				case BookType.ePub:
						new ePubCreator().Create(fileName, pathTemporalFiles, eBook);
					break;
				default:
					throw new NotImplementedException("Unknown ebook type");
			}
		}
	}
}
