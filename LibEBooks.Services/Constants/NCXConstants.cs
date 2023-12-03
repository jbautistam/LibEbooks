namespace Bau.Libraries.LibEBooks.Services.Constants;

/// <summary>
///		Constantes de los archivos NCX
/// </summary>
internal static class NCXConstants
{ 
	// Constantes para el XML del nodo raíz
	internal const string TagRoot = "ncx";
	internal const string TagRootAttrNameSpace = "xmlns";
	internal const string TagRootAttrNameSpaceValue = "http://www.daisy.org/z3986/2005/ncx/";
	internal const string TagRootAttrVersion = "version";
	internal const string TagRootAttrVersionValue = "2005-1";
	internal const string TagRootAttrLanguage = "xml:lang";
	internal const string TagRootAttrLanguageSpanish = "es";
	// Constantes para la cabecera
	internal const string TagHead = "head";
	internal const string TagHeadMeta = "meta";
	internal const string TagHeadMetaAttrContent = "content";
	internal const string TagHeadMetaAttrName = "name";
	internal const string TagHeadMetaAttrNameIDValue = "dtb:uid";
	internal const string TagHeadMetaAttrNameDepthValue = "dtb:depth";
	internal const string TagHeadMetaAttrNameGeneratorValue = "dtb:generator";
	internal const string TagHeadMetaAttrNameTotalPageCountValue = "dtb:totalPageCount";
	internal const string TagHeadMetaAttrNameMaxPageNumberValue = "dtb:maxPageNumber";
	// Constantes para el título
	internal const string TagDocTitle = "docTitle";
	internal const string TagText = "text";
	// Constantes para el navMap
	internal const string TagNavMap = "navMap";
	internal const string TagNavPoint = "navPoint";
	internal const string TagNavPoid = "id";
	internal const string TagNavPointOrder = "playOrder";
	internal const string TagNavPointLabel = "navLabel";
	internal const string TagNavPointContent = "content";
	internal const string TagNavPointContentSrc = "src";
	// Otras constantes
	internal const string MediaType = "application/x-dtbncx+xml";
}
