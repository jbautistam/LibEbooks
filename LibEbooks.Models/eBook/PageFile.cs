namespace Bau.Libraries.LibEBooks.Models.eBook;

/// <summary>
///		Archivo del libro
/// </summary>
public class PageFile : Base.eBookBase
{
	public PageFile(string id, string name, string url, string mediaType)
	{
		ID = id;
		Name = name;
		Url = url;
		MediaType = mediaType;
	}

	/// <summary>
	///		Obtiene la URL relativa a un directorio
	/// </summary>
	public string GetRelativeUrl(string path)
	{
		string [] partsPathSource = SplitPath(path);
		string [] partsPathTarget = SplitPath(Url);
		string relativeUrl = string.Empty;
		int index = 0, indexTarget;

			// Quita los directorios iniciales que sean iguales
			while (index < partsPathTarget.Length &&
					index < partsPathSource.Length &&
					partsPathTarget[index].Equals(partsPathSource[index], StringComparison.CurrentCultureIgnoreCase))
				index++;
			// Añade todos los .. que sean necesarios
			indexTarget = index;
			while (indexTarget <= partsPathSource.Length - 1)
			{ 
				// Añade el salto
				relativeUrl += "../";
				// Incrementa el índice
				indexTarget++;
			}
			// Añade los archivos finales
			while (index < partsPathTarget.Length)
			{ 
				// Añade el directorio
				relativeUrl += partsPathTarget[index];
				// Añade el separador
				if (index < partsPathTarget.Length - 1)
					relativeUrl += "/";
				// Incrementa el índice
				index++;
			}
			// Reemplaza los separadores de directorio
			if (!string.IsNullOrWhiteSpace(relativeUrl))
				relativeUrl = relativeUrl.Replace('\\', '/');
			// Devuelve la URL
			return relativeUrl;
	}

	/// <summary>
	///		Parte una cadena por \ o por /
	/// </summary>
	private string [] SplitPath(string url)
	{ 
		if (string.IsNullOrWhiteSpace(url))
			return new string [] { "" };
		else if (url.IndexOf('/') >= 0)
			return url.Split('/');
		else
			return url.Split('\\');
	}

	/// <summary>
	///		Nombre de la página
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	///		Url del archivo
	/// </summary>
	public string Url { get; set; }

	/// <summary>
	///		Tipo de archivo
	/// </summary>
	public string MediaType { get; set; }
}
