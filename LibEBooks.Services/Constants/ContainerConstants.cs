namespace Bau.Libraries.LibEBooks.Services.Constants;

/// <summary>
///		Constantes del archivo container.xml
/// </summary>
internal static class ContainerConstants
{   
	// Raíz
	internal const string TagRoot = "container";
	internal const string TagRootNameSpace = "xmlns";
	internal const string TagRootNameSpaceValue = "urn:oasis:names:tc:opendocument:xmlns:container";
	internal const string TagRootVersion = "version";
	internal const string TagRootVersionValue = "1.0";
	// RootFiles
	internal const string TagRootFilesRoot = "rootfiles";
	internal const string TagRootFileRoot = "rootfile";
	internal const string TagRootFileMediaType = "media-type";
	internal const string TagRootFileMediaTypeValue = "application/oebps-package+xml";
	internal const string TagRootFilePath = "full-path";
}
