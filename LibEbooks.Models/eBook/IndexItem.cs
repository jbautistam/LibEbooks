namespace Bau.Libraries.LibEBooks.Models.eBook;

/// <summary>
///		Elemento del índice
/// </summary>
public class IndexItem : Base.eBookBase
{
	public IndexItem(string name, string idPage, string url)
	{ 
		Name = name;
		IdPage = idPage;
		URL = url;
	}

	/// <summary>
	///		Identificador del índice
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	///		Identificador de la página
	/// </summary>
	public string IdPage { get; set; }

	/// <summary>
	///		Nombre del archivo con la página
	/// </summary>
	public string URL { get; set; }

	/// <summary>
	///		Número de páginas
	/// </summary>
	public int PageNumber { get; set; }

	/// <summary>
	///		Subíndices
	/// </summary>
	public IndexItemsCollection Items { get; } = new();
}
