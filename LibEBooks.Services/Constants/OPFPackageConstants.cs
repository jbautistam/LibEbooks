using System;

namespace Bau.Libraries.LibEBooks.Services.Constants
{
	/// <summary>
	///		Constantes con las etiquetas de un archivo OPF
	/// </summary>
	internal static class OPFPackageConstants
	{ 
		// Nodo principal
		internal const string TagRoot = "package";
		internal const string TagRootNameSpacePrefix = "opf";
		internal const string TagRootNameSpace = "http://www.idpf.org/2007/opf";
		internal const string TagRootUniqueIdentifier = "unique-identifier";
		internal const string TagRootUniqueIdentifierValue = "EPB-UUID";
		internal const string TagRootVersion = "version";
		internal const string TagRootVersionValue = "2.0";
		// Metadatos
		internal const string TagMetadata = "metadata";
		// Manifiesto
		internal const string TagManifest = "manifest";
		internal const string TagManifestItem = "item";
		internal const string TagManifestID = "id";
		internal const string TagManifestHref = "href";
		internal const string TagManifestMediatype = "media-type";
		// Orden
		internal const string TagSpine = "spine";
		internal const string TagSpineToc = "toc";
		internal const string TagSpineTocValue = "ncx";
		internal const string TagSpineItemRef = "itemref";
		internal const string TagSpineIdRef = "idref";
		internal const string TagSpineLinear = "linear";
	}
}
