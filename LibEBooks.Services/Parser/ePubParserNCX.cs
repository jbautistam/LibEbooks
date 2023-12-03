using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibEBooks.Models.ePub.OPF;
using Bau.Libraries.LibEBooks.Models.ePub.NCX;
using Bau.Libraries.LibEBooks.Services.Constants;

namespace Bau.Libraries.LibEBooks.Services.Parser;

/// <summary>
///		Intérprete de los archivos NCX
/// </summary>
public class ePubParserNCX
{
	/// <summary>
	///		Interpreta los NCXs de un archivo
	/// </summary>
	internal void Parse(OPFPackage package, string path)
	{
		ItemsCollection tocs = package.Manifest.SearchMediaType(MediaTypeConstants.NCX);

			// Limpia el archivo NCX
			package.TablesOfContents.Clear();
			// Carga los archivos de contenido
			foreach (Item item in tocs)
				package.TablesOfContents.Add(Parse(Path.Combine(path, item.URL)));
	}

	/// <summary>
	///		Interpreta el NCX de un elemento 
	/// </summary>
	private NCXFile Parse(string fileName)
	{
		NCXFile ncx = new();
		MLFile fileML = new XMLParser().Load(fileName);

			// Carga el archivo
			if (fileML != null)
			{
				MLNode mlNode = fileML.Nodes[NCXConstants.TagRoot];

					// Carga los datos
					if (mlNode != null)
					{ 
						// Interpreta la cabecera
						ParseHeader(ncx, mlNode);
						// Interpreta el índice
						ncx.Pages.AddRange(ParseIndex(mlNode.Nodes[NCXConstants.TagNavMap]));
					}
			}
			// Devuelve el objeto
			return ncx;
	}

	/// <summary>
	///		Interpreta la cabecera
	/// </summary>
	private void ParseHeader(NCXFile ncx, MLNode mlNode)
	{
		foreach (MLNode mlChild in mlNode.Nodes)
			switch (mlChild.Name)
			{
				case NCXConstants.TagHead:
						foreach (MLNode objMLMeta in mlChild.Nodes)
						{
							int value = objMLMeta.Attributes[NCXConstants.TagHeadMetaAttrContent].Value.GetInt(0);

								// Asigna el valor a las propiedades
								switch (objMLMeta.Attributes[NCXConstants.TagHeadMetaAttrName].Value)
								{
									case NCXConstants.TagHeadMetaAttrNameIDValue:
											ncx.ID = value.ToString();
										break;
									case NCXConstants.TagHeadMetaAttrNameGeneratorValue:
											ncx.Generator = value.ToString();
										break;
									case NCXConstants.TagHeadMetaAttrNameDepthValue:
											ncx.Depth = value;
										break;
									case NCXConstants.TagHeadMetaAttrNameTotalPageCountValue:
											ncx.PageCount = value;
										break;
									case NCXConstants.TagHeadMetaAttrNameMaxPageNumberValue:
											ncx.PageNumber = value;
										break;
								}
						}
					break;
				case NCXConstants.TagDocTitle:
						ncx.Title = mlChild.Nodes [NCXConstants.TagText].Value;
					break;
			}
	}

	/// <summary>
	///		Interpreta el índice
	/// </summary>
	private NavPointsCollection ParseIndex(MLNode mlNode)
	{
		NavPointsCollection pages = new();

			// Carga los datos
			if (mlNode != null)
				foreach (MLNode mlChild in mlNode.Nodes)
					if (mlChild.Name == NCXConstants.TagNavPoint)
					{
						NavPoint page = new NavPoint();

							// Carga los datos básicos
							page.ID = mlChild.Attributes[NCXConstants.TagNavPoid].Value;
							page.Order = mlChild.Attributes[NCXConstants.TagNavPointOrder].Value.GetInt(0);
							// Carga el resto de datos
							foreach (MLNode objMLData in mlChild.Nodes)
								switch (objMLData.Name)
								{
									case NCXConstants.TagNavPointLabel:
											page.Title = objMLData.Nodes[NCXConstants.TagText].Value;
										break;
									case NCXConstants.TagNavPointContent:
											page.URL = objMLData.Attributes[NCXConstants.TagNavPointContentSrc].Value;
										break;
								}
							// Carga las páginas hija
							page.Pages.AddRange(ParseIndex(mlChild));
							// Añade la página a la colección
							pages.Add(page);
					}
			// Devuelve la colección
			return pages;
	}
}
