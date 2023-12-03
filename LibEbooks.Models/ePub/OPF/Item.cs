namespace Bau.Libraries.LibEBooks.Models.ePub.OPF;

/// <summary>
///		Elemento de un libro (página, archivo de estilo, imagen ...)
/// </summary>
public class Item : Base.eBookBase
{ 
	/// <summary>
	///		URL del archivo
	/// </summary>
	public string URL { get; set; } = default!;

	/// <summary>
	///		Tipo del archivo
	/// </summary>
	public string MediaType { get; set; } = default!;
}
