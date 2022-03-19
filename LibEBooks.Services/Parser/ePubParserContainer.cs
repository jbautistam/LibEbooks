using System;

using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibEBooks.Models.ePub.Container;
using Bau.Libraries.LibEBooks.Services.Constants;

namespace Bau.Libraries.LibEBooks.Services.Parser
{
	/// <summary>
	///		Intérprete del archivo Container.XML
	/// </summary>
	/// <example>
	///		<?xml version='1.0' encoding='utf-8'?>
	///		<container xmlns="urn:oasis:names:tc:opendocument:xmlns:container" version="1.0">
	///			<rootfiles>
	///				<rootfile media-type="application/oebps-package+xml" full-path="25640/content.opf"/>
	///			</rootfiles>
	///		</container>
	/// </example>
	internal class ePubParserContainer
	{
		/// <summary>
		///		Interpreta el archivo container.xml. Devuelve una colección de cadenas con los nombres de los
		///	archivos de índice
		/// </summary>
		internal ContainerFile Parse(string pathBase)
		{
			ContainerFile container = new ContainerFile();
			MLFile fileML = new XMLParser().Load(System.IO.Path.Combine(System.IO.Path.Combine(pathBase, "META-INF"), 
																		"container.xml"));

				// Carga los datos del archivo
				if (fileML != null)
					foreach (MLNode mlNode in fileML.Nodes)
						if (mlNode.Name == ContainerConstants.TagRoot)
							foreach (MLNode mlChild in mlNode.Nodes)
								if (mlChild.Name == ContainerConstants.TagRootFilesRoot)
									foreach (MLNode mlRootFile in mlChild.Nodes)
										if (mlRootFile.Name == ContainerConstants.TagRootFileRoot)
										{
											RootFile file = new RootFile();

												// Interpreta los datos
												file.MediaType = mlRootFile.Attributes[ContainerConstants.TagRootFileMediaType].Value;
												file.URL = mlRootFile.Attributes[ContainerConstants.TagRootFilePath].Value;
												// Añade el archivo raíz a la colección
												if (!string.IsNullOrEmpty(file.URL))
													container.RootFiles.Add(file);
										}
				// Devuelve los datos del archivo contenedor
				return container;
		}
	}
}
