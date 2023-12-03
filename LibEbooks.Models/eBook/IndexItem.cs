namespace Bau.Libraries.LibEBooks.Models.eBook;

/// <summary>
///		Elemento del �ndice
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
	///		Identificador del �ndice
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	///		Identificador de la p�gina
	/// </summary>
	public string IdPage { get; set; }

	/// <summary>
	///		Nombre del archivo con la p�gina
	/// </summary>
	public string URL { get; set; }

	/// <summary>
	///		N�mero de p�ginas
	/// </summary>
	public int PageNumber { get; set; }

	/// <summary>
	///		Sub�ndices
	/// </summary>
	public IndexItemsCollection Items { get; } = new();
}
