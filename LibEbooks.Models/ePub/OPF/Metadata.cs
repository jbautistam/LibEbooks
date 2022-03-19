using System;

namespace Bau.Libraries.LibEBooks.Models.ePub.OPF
{
	/// <summary>
	///		Metadatos del archivo OPF
	/// </summary>
	public class Metadata : Base.eBookBase
	{
		/// <summary>
		///		Título del libro
		/// </summary>
		public string Title { get; set; }
		
		/// <summary>
		///		Autor
		/// </summary>
		public string Author { get; set; }
		
		/// <summary>
		///		Editor
		/// </summary>
		public string Publisher { get; set; }
		
		/// <summary>
		///		Fecha de publicación del original
		/// </summary>
		public string DateOriginalPublished { get; set; }
		
		/// <summary>
		///		Fecha de publicación del eBook
		/// </summary>
		public string DatePublished { get; set; }
		
		/// <summary>
		///		Tema del libro
		/// </summary>
		public string Subject { get; set; }
		
		/// <summary>
		///		Origen del eBook
		/// </summary>
		public string Source { get; set; }
		
		/// <summary>
		///		Derechos de copia
		/// </summary>
		public string Rights { get; set; }
		
		/// <summary>
		///		Idioma del eBook
		/// </summary>
		public string Language { get; set; }
	}
}