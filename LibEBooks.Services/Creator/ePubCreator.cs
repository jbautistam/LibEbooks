using Bau.Libraries.LibCompressor;
using Bau.Libraries.LibEBooks.Models.eBook;
using Bau.Libraries.LibEBooks.Models.ePub;
using Bau.Libraries.LibEBooks.Models.ePub.Container;
using Bau.Libraries.LibEBooks.Models.ePub.NCX;
using Bau.Libraries.LibEBooks.Models.ePub.OPF;
using Bau.Libraries.LibEBooks.Services.Constants;
using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibHelper.Files;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibEBooks.Services.Creator;

/// <summary>
///		Creador de archivos ePub
/// </summary>
internal class ePubCreator
{
	// Constantes privadas
	private const string FileNameOpf =  "ebook.opf";

	/// <summary>
	///		Crea un archivo ePub a partir de un objeto <see cref="Book"/>
	/// </summary>
	internal void Create(string fileName, string pathTemporalFiles, Book eBook)
	{
		Create(fileName, pathTemporalFiles, new eBookConvertEPub().Convert(eBook));
	}

	/// <summary>
	///		Crea un archivo ePub
	/// </summary>
	internal void Create(string fileName, string pathTemporalFiles, ePubEBook eBook)
	{
		Compressor compressor = new Compressor();

			// Crea el directorio raíz
			HelperFiles.MakePath(pathTemporalFiles);
			// Crea el archivo mimeType
			HelperFiles.SaveTextFile(Path.Combine(pathTemporalFiles, "mimetype"), "application/epub+zip", System.Text.Encoding.Default);
			// Crea el archivo Container.xml
			CreateContainer(pathTemporalFiles, eBook);
			// Crea los archivos de contenido
			CreateContent(pathTemporalFiles, eBook);
			// Comprime el directorio
			compressor.Compress(fileName, pathTemporalFiles, Compressor.CompressType.Zip);
	}

	/// <summary>
	///		Crea el archivo contenedor (container.xml) en el directorio META-INF
	/// </summary>
	private void CreateContainer(string path, ePubEBook eBook)
	{
		string pathMeta = Path.Combine(path, "META-INF");
		MLFile fileML = new MLFile();
		MLNode rootML = fileML.Nodes.Add(ContainerConstants.TagRoot);
		MLNode rootMLFiles;

			// Asigna la codificación al archivo
			fileML.Encoding = MLFile.EncodingMode.UTF8;
			// Añade los atributos al nodo raíz
			rootML.Attributes.Add(ContainerConstants.TagRootVersion, ContainerConstants.TagRootVersionValue);
			rootML.Attributes.Add(ContainerConstants.TagRootNameSpace, ContainerConstants.TagRootNameSpaceValue);
			// Añade el nodo de archivos raíz
			rootMLFiles = rootML.Nodes.Add(ContainerConstants.TagRootFilesRoot);
			// Añade los rootFiles
			for (int index = 0; index < eBook.Container.RootFiles.Count; index++)
			{
				MLNode rootMLFile = rootMLFiles.Nodes.Add(ContainerConstants.TagRootFileRoot);

					// Añade los atributos al nodo rootFile
					rootMLFile.Attributes.Add(ContainerConstants.TagRootFilePath, Path.Combine(GetRootFilePath(index), FileNameOpf));
					rootMLFile.Attributes.Add(ContainerConstants.TagRootFileMediaType,
											  ContainerConstants.TagRootFileMediaTypeValue);
			}
			// Graba el archivo
			SaveMLFile(Path.Combine(pathMeta, "container.xml"), fileML);
	}

	/// <summary>
	///		Crea los archivos de contenido
	/// </summary>
	private void CreateContent(string path, ePubEBook eBook)
	{
		int index = 0;

			foreach (RootFile rootFile in eBook.Container.RootFiles)
			{
				string pathPackage = Path.Combine(path, GetRootFilePath(index).Replace("/", "\\"));

					// Crea el directorio
					HelperFiles.MakePath(pathPackage);
					// Crea el paquete OPF
					foreach (OPFPackage package in rootFile.Packages)
					{ 
						// Crea el archivo OPF
						CreateOpf(pathPackage, package);
						// Crea el archivo NCX
						CreateNcx(pathPackage, package);
					}
					// Incrementa el índice de rootFile
					index++;
			}
	}

	/// <summary>
	///		Crea el archivo del paquete OPF
	/// </summary>
	private void CreateOpf(string path, OPFPackage package)
	{
		MLFile fileML = new();
		MLNode rootML = fileML.Nodes.Add(OPFPackageConstants.TagRoot);

			// Añade los espacios de nombres
			rootML.NameSpaces.Add(string.Empty, OPFPackageConstants.TagRootNameSpace);
			// Añade los atributos
			rootML.Attributes.Add(OPFPackageConstants.TagRootVersion, OPFPackageConstants.TagRootVersionValue);
			rootML.Attributes.Add(OPFPackageConstants.TagRootUniqueIdentifier, OPFPackageConstants.TagRootUniqueIdentifierValue);
			// Añade los metadatos
			rootML.Nodes.Add(GetMetadata(package));
			// Añade el manifiesto con los archivos
			rootML.Nodes.Add(GetManifest(package, Path.GetFileName(path)));
			// Añade el spine
			rootML.Nodes.Add(GetSpine(package));
			// Graba el archivo
			SaveMLFile(Path.Combine(path, FileNameOpf), fileML);
	}

	/// <summary>
	///		Obtiene el nodo con los metadatos del paquete
	/// </summary>
	private MLNode GetMetadata(OPFPackage package)
	{
		MLNode nodeML = new MLNode(OPFPackageConstants.TagMetadata);

			// Añade los espacios de nombres
			nodeML.NameSpaces.Add(OPFPackageConstants.TagRootNameSpacePrefix, OPFPackageConstants.TagRootNameSpace);
			nodeML.NameSpaces.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.NameSpace);
			// Añade los metadatos
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagRights, package.Metadata.Rights);
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagIdentifier, package.Metadata.ID);
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagCreator, package.Metadata.Author);
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagTitle, package.Metadata.Title);
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagLanguage, package.Metadata.Language);
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagSubject, package.Metadata.Subject);
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagDate, ConvertDate(package.Metadata.DateOriginalPublished));
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagDate, ConvertDate(package.Metadata.DatePublished));
			nodeML.Nodes.Add(DublinCoreConstants.NameSpacePrefix, DublinCoreConstants.TagSource, package.Metadata.Source);
			// Devuelve el nodo
			return nodeML;
	}

	/// <summary>
	///		Convierte la fecha
	/// </summary>
	private string ConvertDate(string date) => $"{date.GetDateTime(DateTime.Now):yyyy-MM-dd}";

	/// <summary>
	///		Obtiene el manifiesto
	/// </summary>
	private MLNode GetManifest(OPFPackage package, string pathOpfFile)
	{
		MLNode nodeML = new MLNode(OPFPackageConstants.TagManifest);

			// Añade los archivos
			foreach (Item item in package.Manifest)
				nodeML.Nodes.Add(GetNodeItem(item.ID, HelperFiles.GetFileNameRelative(pathOpfFile, item.URL), item.MediaType));
			// Añade el archivo NCX que se va a crear
			for (int index = 0; index < package.TablesOfContents.Count; index++)
				nodeML.Nodes.Add(GetNodeItem("ncx", GetNcxFileName(index), NCXConstants.MediaType));
			// Devuelve el nodo
			return nodeML;
	}

	/// <summary>
	///		Obtiene un nodo con un elemento (Item)
	/// </summary>
	private MLNode GetNodeItem(string id, string url, string mediaType)
	{
		MLNode itemML = new MLNode(OPFPackageConstants.TagManifestItem);

			// Añade los atributos
			itemML.Attributes.Add(OPFPackageConstants.TagManifestID, id);
			itemML.Attributes.Add(OPFPackageConstants.TagManifestHref, url);
			itemML.Attributes.Add(OPFPackageConstants.TagManifestMediatype, mediaType);
			// Devuelve el nodo
			return itemML;
	}

	/// <summary>
	///		Obtiene el índice principal
	/// </summary>
	private MLNode GetSpine(OPFPackage package)
	{
		MLNode nodeML = new MLNode(OPFPackageConstants.TagSpine);

			// Añade los atributos
			nodeML.Attributes.Add(OPFPackageConstants.TagSpineToc, OPFPackageConstants.TagSpineTocValue);
			// Añade las referencias
			foreach (ItemRef item in package.Spine)
			{
				MLNode itemML = new MLNode(OPFPackageConstants.TagSpineItemRef);

					// Añade los atributos
					itemML.Attributes.Add(OPFPackageConstants.TagSpineIdRef, item.IDRef);
					itemML.Attributes.Add(OPFPackageConstants.TagSpineLinear, item.IsLinear);
					// Añade el nodo
					nodeML.Nodes.Add(itemML);
			}
			// Devuelve el nodo
			return nodeML;
	}

	/// <summary>
	///		Crea el archivo NCX del paquete
	/// </summary>
	private void CreateNcx(string path, OPFPackage package)
	{
		int index = 0;

			foreach (NCXFile ncxFile in package.TablesOfContents)
			{
				MLFile fileML = new MLFile();
				MLNode rootML = fileML.Nodes.Add(NCXConstants.TagRoot);
				MLNode titleML;

					// Añade los atributos
					rootML.Attributes.Add(NCXConstants.TagRootAttrNameSpace, NCXConstants.TagRootAttrNameSpaceValue);
					rootML.Attributes.Add(NCXConstants.TagRootAttrVersion, NCXConstants.TagRootAttrVersionValue);
					rootML.Attributes.Add(NCXConstants.TagRootAttrNameSpace, NCXConstants.TagRootAttrLanguage,
										  NCXConstants.TagRootAttrLanguageSpanish);
					// Añade el nodo con la cabecera
					rootML.Nodes.Add(NCXConstants.TagHead);
					// Añade el nodo con el título
					titleML = rootML.Nodes.Add(NCXConstants.TagDocTitle);
					titleML.Nodes.Add(NCXConstants.TagText, package.Metadata.Title);
					// Añade el índice
					rootML.Nodes.Add(NCXConstants.TagNavMap).Nodes.Add(GetNodesNavPoint(Path.GetFileName(path), ncxFile.Pages));
					// Graba el archivo
					SaveMLFile(Path.Combine(path, GetNcxFileName(index)), fileML);
					// Incrementa el índice
					index++;
			}
	}

	/// <summary>
	///		Obtiene el nodo de una colección de puntos de navegación
	/// </summary>
	private MLNodesCollection GetNodesNavPoint(string pathTocFile, NavPointsCollection navPoints)
	{
		MLNodesCollection nodesML = new MLNodesCollection();

			// Añade los puntos de navegación
			foreach (NavPoint navPoint in navPoints)
			{
				MLNode navPointML = new MLNode(NCXConstants.TagNavPoint);
				MLNode contentML;

					// Añade los atributos
					navPointML.Attributes.Add(NCXConstants.TagNavPoid, navPoint.ID);
					navPointML.Attributes.Add(NCXConstants.TagNavPointOrder, navPoint.Order.ToString());
					// Añade la etiqueta
					navPointML.Nodes.Add(NCXConstants.TagNavPointLabel).Nodes.Add(NCXConstants.TagText, navPoint.Title);
					// Añade el contenido
					contentML = navPointML.Nodes.Add(NCXConstants.TagNavPointContent);
					contentML.Attributes.Add(NCXConstants.TagNavPointContentSrc, HelperFiles.GetFileNameRelative(pathTocFile, navPoint.URL));
					// Añade los hijos
					navPointML.Nodes.Add(GetNodesNavPoint(pathTocFile, navPoint.Pages));
					// Añade el nodo a la colección
					nodesML.Add(navPointML);
			}
			// Devuelve la colección de nodos
			return nodesML;
	}

	/// <summary>
	///		Obtiene el nombre del directorio para los RootFiles
	/// </summary>
	private string GetRootFilePath(int index)
	{
		if (index == 0)
			return "OPS";
		else
			return $"OPS{index}";
	}

	/// <summary>
	///		Obtiene el nombre de un archivo NCX
	/// </summary>
	private string GetNcxFileName(int index)
	{
		if (index == 0)
			return "toc.ncx";
		else
			return $"toc{index}.ncx";
	}

	/// <summary>
	///		Graba un archivo XML
	/// </summary>
	private void SaveMLFile(string fileName, MLFile fileML)
	{
		new XMLWriter().Save(fileName, fileML);
	}
}
