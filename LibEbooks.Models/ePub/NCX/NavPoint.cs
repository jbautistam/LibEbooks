namespace Bau.Libraries.LibEBooks.Models.ePub.NCX;

/// <summary>
///		Punto de navegación / página
/// </summary>
public class NavPoint : Base.eBookBase
{
	/// <summary>
	///		Título
	/// </summary>
	public string Title { get; set; } = default!;

	/// <summary>
	///		URL
	/// </summary>
	public string URL { get; set; } = default!;

	/// <summary>
	///		Orden
	/// </summary>
	public int Order { get; set; }

	/// <summary>
	///		Páginas
	/// </summary>
	public NavPointsCollection Pages { get; } = new();
}
