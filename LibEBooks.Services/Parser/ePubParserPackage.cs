using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibEBooks.Models.ePub;
using Bau.Libraries.LibEBooks.Models.ePub.Container;
using Bau.Libraries.LibEBooks.Models.ePub.OPF;

namespace Bau.Libraries.LibEBooks.Services.Parser
{
	/// <summary>
	///		Intérprete de un paquete
	/// </summary>
	internal class ePubParserPackage
	{
		/// <summary>
		///		Interpreta la información del paquete de un ePub
		/// </summary>
		internal void Parse(ePubEBook book, string path)
		{
			foreach (RootFile rootFile in book.Container.RootFiles)
			{ 
				// Limpia los paquetes
				rootFile.Packages.Clear();
				// Añade los paquetes del archivo
				rootFile.Packages.Add(ParsePackage(System.IO.Path.Combine(path, rootFile.URL)));
			}
		}

		/// <summary>
		///		Interpreta un paquete
		/// </summary>
		private OPFPackage ParsePackage(string fileName)
		{
			OPFPackage package = new OPFPackage();
			MLFile fileML = new XMLParser().Load(fileName);

				// Carga el archivo
				if (fileML != null)
				{
					MLNode mlNode = fileML.Nodes[Constants.OPFPackageConstants.TagRoot];

						// Interpreta el paquete
						if (mlNode != null)
						{ 
							// Interpreta los  metadatos
							ParseMetadata(package.Metadata, mlNode.Nodes[Constants.OPFPackageConstants.TagMetadata]);
							// Interpreta el manifiesto
							ParseManifest(package, mlNode.Nodes[Constants.OPFPackageConstants.TagManifest]);
							// Interpreta el índice
							ParseSpine(package, mlNode.Nodes[Constants.OPFPackageConstants.TagSpine]);
							// Interpreta el NCX
							new ePubParserNCX().Parse(package, System.IO.Path.GetDirectoryName(fileName));
						}
				}
				// Devuelve el paquete
				return package;
		}

		/// <summary>
		///		Interpreta los metadatos
		/// </summary>
		private void ParseMetadata(Metadata metadata, MLNode mlNode)
		{
			if (mlNode != null)
			{
				metadata.ID = mlNode.Nodes[Constants.DublinCoreConstants.TagIdentifier].Value;
				metadata.Title = mlNode.Nodes[Constants.DublinCoreConstants.TagTitle].Value;
				metadata.Author = Search(mlNode.Nodes, Constants.DublinCoreConstants.TagCreator,
										 Constants.OPFConstants.TagAttributesRole,
										 Constants.OPFConstants.TagValueRoleAut);
				metadata.Publisher = mlNode.Nodes[Constants.DublinCoreConstants.TagPublisher].Value;
				metadata.DateOriginalPublished = Search(mlNode.Nodes,
														Constants.DublinCoreConstants.TagDate,
														Constants.OPFConstants.TagAttributesEvent,
														Constants.OPFConstants.TagValueEventOriginalPublication);
				metadata.DatePublished = Search(mlNode.Nodes,
												Constants.DublinCoreConstants.TagDate,
												Constants.OPFConstants.TagAttributesEvent,
												Constants.OPFConstants.TagValueEventPublication);
				metadata.Subject = mlNode.Nodes[Constants.DublinCoreConstants.TagSubject].Value;
				metadata.Source = mlNode.Nodes[Constants.DublinCoreConstants.TagSource].Value;
				metadata.Rights = mlNode.Nodes[Constants.DublinCoreConstants.TagRights].Value;
				metadata.Language = mlNode.Nodes[Constants.DublinCoreConstants.TagLanguage].Value;
			}
		}

		/// <summary>
		///		Obtiene el valor del nodo con una etiqueta y un valor particular en un atributo
		/// </summary>
		private string Search(MLNodesCollection mlNodes, string tag, string attribute, string value)
		{ 
			// Busca la etiqueta en los nodos
			foreach (MLNode mlNode in mlNodes)
				if (mlNode.Name == tag)
				{
					MLAttribute mlAttribute = mlNode.Attributes.Search(attribute);

						if (mlAttribute?.Value == attribute)
							return mlNode.Value;
				}
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return null;
		}

		/// <summary>
		///		Interpreta el manifiesto del libro
		/// </summary>
		private void ParseManifest(OPFPackage package, MLNode mlNode)
		{ 
			// Limpia el manifiesto
			package.Manifest.Clear();
			// Carga los datos
			if (mlNode != null)
				foreach (MLNode mlChild in mlNode.Nodes)
					if (mlChild.Name == Constants.OPFPackageConstants.TagManifestItem)
					{
						Item item = new Item();

							// Interpreta el nodo
							item.ID = mlChild.Attributes[Constants.OPFPackageConstants.TagManifestID].Value;
							item.URL = mlChild.Attributes[Constants.OPFPackageConstants.TagManifestHref].Value;
							item.MediaType = mlChild.Attributes[Constants.OPFPackageConstants.TagManifestMediatype].Value;
							// Añade el elemento
							package.Manifest.Add(item);
					}
		}

		/// <summary>
		///		Interpreta el índice del libro
		/// </summary>
		private void ParseSpine(OPFPackage package, MLNode mlNode)
		{ 
			// Limpia el índice
			package.Spine.Clear();
			// Carga los datos
			if (mlNode != null)
				foreach (MLNode mlChild in mlNode.Nodes)
					if (mlChild.Name == Constants.OPFPackageConstants.TagSpineItemRef)
					{
						ItemRef item = new ItemRef();

							// Interpreta el nodo
							item.IDRef = mlChild.Attributes[Constants.OPFPackageConstants.TagSpineIdRef].Value;
							item.IsLinear = mlChild.Attributes[Constants.OPFPackageConstants.TagSpineLinear].Value.GetBool(true);
							// Añade el elemento
							package.Spine.Add(item);
					}
		}
	}
}
