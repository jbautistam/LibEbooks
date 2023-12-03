namespace Bau.Libraries.LibEBooks.Models.eBook;

/// <summary>
///		Colección de <see cref="PageFile"/>
/// </summary>
public class PageFilesCollection : Base.eBookBaseCollection<PageFile>
{
	/// <summary>
	///		Añade un archivo
	/// </summary>
	public void Add(string id, string name, string fileName, string mediaType)
	{
		Add(new PageFile(id, name, fileName, mediaType));
	}

	/// <summary>
	///		Busca una página a partir de la URL
	/// </summary>
	public PageFile? SearchByURL(string url)
	{ 
		// Busca la página a partir de la URL
		foreach (PageFile page in this)
			if (!string.IsNullOrEmpty(page.Url))
			{
				string[] urls = page.Url.Split('#');

					if (urls[0].Equals(url) || page.Url.Equals(url))
						return page;
			}
		// Si ha llegado hasta aquí es porque no ha encontrado nada
		return null;
	}

	/// <summary>
	///		Busca una página a partir de un nombre de archivo
	/// </summary>
	public PageFile? SearchByFileName(string fileName)
	{ 
		// Busca la página a partir de la URL
		foreach (PageFile page in this)
			if (!string.IsNullOrEmpty(page.Url) && (page.Url.Equals(fileName, StringComparison.CurrentCultureIgnoreCase) ||
					Path.GetFileName(page.Url).Equals(fileName, StringComparison.CurrentCultureIgnoreCase)))
				return page;
		// Si ha llegado hasta aquí es porque no ha encontrado nada
		return null;
	}

	/// <summary>
	///		Normaliza los IDs de las páginas
	/// </summary>
	public void NormalizeID()
	{
		int index = 1;

			foreach (PageFile file in this)
				file.ID = $"Page{index++}";
	}
}
