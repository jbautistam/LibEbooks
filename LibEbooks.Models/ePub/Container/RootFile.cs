namespace Bau.Libraries.LibEBooks.Models.ePub.Container;

/// <summary>
///		Datos de un elemento RootFile de un archivo contenedor
/// </summary>
public class RootFile : Base.eBookBase
{ 
	// Variables privadas
	private string _url = default!;

	/// <summary>
	///		Tipo de medio
	/// </summary>
	public string MediaType { get; set; } = default!;

	/// <summary>
	///		Nombre de archivo
	/// </summary>
	public string URL
	{
		get { return _url; }
		set
		{ 
			// Asigna el valor
			_url = value;
			// Quita las barras
			if (!string.IsNullOrEmpty(_url))
				_url = _url.Replace('/', '\\');
		}
	}

	/// <summary>
	///		Paquetes con los archivos
	/// </summary>
	public OPF.OPFPackagesCollection Packages { get; } = new();
}
