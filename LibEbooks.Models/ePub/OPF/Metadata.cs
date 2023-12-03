namespace Bau.Libraries.LibEBooks.Models.ePub.OPF;

/// <summary>
///		Metadatos del archivo OPF
/// </summary>
public class Metadata : Base.eBookBase
{
	/// <summary>
	///		Título del libro
	/// </summary>
	public string Title { get; set; } = default!;
	
	/// <summary>
	///		Autor
	/// </summary>
	public string Author { get; set; } = default!;
	
	/// <summary>
	///		Editor
	/// </summary>
	public string Publisher { get; set; } = default!;
	
	/// <summary>
	///		Fecha de publicación del original
	/// </summary>
	public string DateOriginalPublished { get; set; } = default!;
	
	/// <summary>
	///		Fecha de publicación del eBook
	/// </summary>
	public string DatePublished { get; set; } = default!;
	
	/// <summary>
	///		Tema del libro
	/// </summary>
	public string Subject { get; set; } = default!;
	
	/// <summary>
	///		Origen del eBook
	/// </summary>
	public string Source { get; set; } = default!;
	
	/// <summary>
	///		Derechos de copia
	/// </summary>
	public string Rights { get; set; } = default!;
	
	/// <summary>
	///		Idioma del eBook
	/// </summary>
	public string Language { get; set; } = default!;
}