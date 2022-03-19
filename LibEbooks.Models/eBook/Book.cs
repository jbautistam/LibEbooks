using System;

namespace Bau.Libraries.LibEBooks.Models.eBook
{
	/// <summary>
	///		Clase con los datos de un libro
	/// </summary>
	public class Book : Base.eBookBase
	{
		/// <summary>
		///		Creador del libro
		/// </summary>
		public string Creator { get; set; }

		/// <summary>
		///		Título del libro
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Autor del libro
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		///		Editor
		/// </summary>
		public string Publisher { get; set; }

		/// <summary>
		///		Origen del libro
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		///		Asunto del libro
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		///		Derechos
		/// </summary>
		public string Rights { get; set; }

		/// <summary>
		///		Idioma del libro
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		///		Número de páginas
		/// </summary>
		public int Pages { get; set; }

		/// <summary>
		///		Número máximo de páginas
		/// </summary>
		public int MaxPageNumber { get; set; }

		/// <summary>
		///		Fecha de publicación del original
		/// </summary>
		public string DateOriginalPublished { get; set; }

		/// <summary>
		///		Fecha de publicación
		/// </summary>
		public string DatePublished { get; set; }

		/// <summary>
		///		Archivos
		/// </summary>
		public PageFilesCollection Files { get; } = new PageFilesCollection();

		/// <summary>
		///		Indice del libro
		/// </summary>
		public IndexItemsCollection Index { get; } = new IndexItemsCollection();

		/// <summary>
		///		Tabla de contenido
		/// </summary>
		public IndexItemsCollection TableOfContent { get; } = new IndexItemsCollection();
	}
}
