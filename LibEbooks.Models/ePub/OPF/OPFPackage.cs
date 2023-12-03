namespace Bau.Libraries.LibEBooks.Models.ePub.OPF;

/// <summary>
///		Datos del archivo OPF
/// </summary>
public class OPFPackage : Base.eBookBase
{
	/// <summary>
	///		Metadatos del archivo
	/// </summary>
	public Metadata Metadata { get; } = new();

	/// <summary>
	///		Manifiesto: archivos del eBook (páginas, imágenes, estilos ...)
	/// </summary>
	public ItemsCollection Manifest { get; } = new();

	/// <summary>
	///		Indice principal
	/// </summary>
	public ItemsRefCollection Spine { get; } = new();

	/// <summary>
	///		Tablas de contenidos
	/// </summary>
	public NCX.NCXFilesCollection TablesOfContents { get; } = new();
}
