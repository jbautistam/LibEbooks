using Bau.Libraries.LibEBooks.Models.eBook;
using Bau.Libraries.LibEBooks.Models.ePub;
using Bau.Libraries.LibEBooks.Models.ePub.Container;
using Bau.Libraries.LibEBooks.Models.ePub.NCX;
using Bau.Libraries.LibEBooks.Models.ePub.OPF;

namespace Bau.Libraries.LibEBooks.Services.Creator;

/// <summary>
///		Conversor del formato eBook al formato ePub
/// </summary>
internal class eBookConvertEPub
{
	/// <summary>
	///		Convierte un archivo eBook al formato ePub
	/// </summary>
	internal ePubEBook Convert(Book eBook)
	{
		ePubEBook ePub = new ePubEBook();
		OPFPackage package = CreatePackage(ePub);
		NCXFile ncxFile = new();

			// Asigna los metadatos
			package.Metadata.Author = eBook.Author;
			package.Metadata.DateOriginalPublished = eBook.DateOriginalPublished;
			package.Metadata.DatePublished = eBook.DatePublished;
			package.Metadata.Language = eBook.Language;
			package.Metadata.Publisher = eBook.Publisher;
			package.Metadata.Rights = eBook.Rights;
			package.Metadata.Source = eBook.Source;
			package.Metadata.Subject = eBook.Subject;
			package.Metadata.Title = eBook.Title;
			// Normaliza los IDs de páginas
			NormalizePagesID(eBook);
			// Añade las páginas
			foreach (PageFile page in eBook.Files)
			{
				Item item = new();

					// Asigna los datos
					item.ID = page.ID;
					item.MediaType = page.MediaType;
					item.URL = page.Url;
					// Añade la página
					package.Manifest.Add(item);
			}
			// Añade el spine
			package.Spine.AddRange(GetSpine(eBook, eBook.Index));
			// Añade las tablas de contenido al índice
			ncxFile.Pages.AddRange(GetNavPoints(eBook.TableOfContent));
			// Añade el índice al paquete
			package.TablesOfContents.Add(ncxFile);
			// Devuelve el archivo en formato ePub
			return ePub;
	}

	/// <summary>
	///		Normaliza los IDs de las páginas
	/// </summary>
	private void NormalizePagesID(Book eBook)
	{
		int index = 1;

			// Recorre las páginas
			foreach (PageFile page in eBook.Files)
			{
				string newId = "Page" + (index++).ToString();

					// Cambia los IDs de los índices
					ChangeIndexID(page.ID, newId, eBook.Index);
					ChangeIndexID(page.ID, newId, eBook.TableOfContent);
					// Cambia el ID de la página
					page.ID = newId;
			}
	}

	/// <summary>
	///		Cambia el índice de una página
	/// </summary>
	private void ChangeIndexID(string id, string newId, IndexItemsCollection indexItems)
	{
		foreach (IndexItem index in indexItems)
		{ 
			// Cambia el ID de la página
			if (index.IdPage.Equals(id, StringComparison.CurrentCultureIgnoreCase))
				index.IdPage = newId;
			// Cambia el ID de los índices internos
			ChangeIndexID(id, newId, index.Items);
		}
	}

	/// <summary>
	///		Obtiene los elementos para el índice principal
	/// </summary>
	private ItemsRefCollection GetSpine(Book eBook, IndexItemsCollection indexItems)
	{
		ItemsRefCollection itemsRef = new();

			// Añade los elementos del índice
			foreach (IndexItem index in indexItems)
			{
				PageFile page = eBook.Files.SearchByFileName(index.URL);

					// Añade la página al índice
					if (page != null)
					{
						ItemRef itemRef = new();

							// Asigna los datos
							itemRef.ID = index.ID;
							itemRef.IDRef = page.ID;
							itemRef.IsLinear = true;
							// Añade la referencia al archivo
							itemsRef.Add(itemRef);
					}
					// Añade las páginas hija
					itemsRef.AddRange(GetSpine(eBook, index.Items));
			}
			// Devuelve la colección
			return itemsRef;
	}

	/// <summary>
	///		Crea los puntos de navegación
	/// </summary>
	private NavPointsCollection GetNavPoints(IndexItemsCollection indexItems)
	{
		NavPointsCollection navPoints = new();

			// Crea los índices
			for (int index = 0; index < indexItems.Count; index++)
			{
				NavPoint navPoint = new();

					// Asigna las propiedades
					navPoint.ID = indexItems[index].ID;
					navPoint.Order = index + 1;
					navPoint.Title = indexItems[index].Name;
					navPoint.URL = indexItems[index].URL;
					// Añade los índices
					navPoint.Pages.AddRange(GetNavPoints(indexItems[index].Items));
					// Añade el navPoint a la colección
					navPoints.Add(navPoint);
			}
			// Devuelve el índice
			return navPoints;
	}

	/// <summary>
	///		Crea el paquete
	/// </summary>
	private OPFPackage CreatePackage(ePubEBook ePub)
	{
		RootFile rootFile = new();
		OPFPackage package = new();

			// Añade el paquete al rootFile
			rootFile.Packages.Add(package);
			// Añade el rootFile
			ePub.Container.RootFiles.Add(rootFile);
			// Devuelve el paquete
			return package;
	}
}
