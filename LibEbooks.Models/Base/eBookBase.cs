namespace Bau.Libraries.LibEBooks.Base;

/// <summary>
///		Elemento base de los objetos de la librer�a
/// </summary>
public class eBookBase
{ 
	/// <summary>
	///		ID del elemento
	/// </summary>
	public string ID { get; set; } = Guid.NewGuid().ToString();
}
