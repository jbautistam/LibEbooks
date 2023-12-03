using Bau.Libraries.LibEBooks.Models.eBook;
using Bau.Libraries.LibEBooks.Models.ePub;
using Bau.Libraries.LibEBooks.Models.ePub.Container;
using Bau.Libraries.LibEBooks.Models.ePub.NCX;
using Bau.Libraries.LibEBooks.Models.ePub.OPF;

namespace Bau.Libraries.LibEBooks.Services.Creator;

/// <summary>
///		Convierte un objeto ePub en un objeto eBook
/// </summary>
internal class ePubConvertEBook
{
	/// <summary>
	///		Convierte un <see cref="ePubEBook"/> en un <see cref="Book"/>
	/// </summary>
	internal Book Convert(ePubEBook ePub)
	{
		Book eBook = new Book();

			// Asigna los metadatos
			AssignMetadata(ePub.Container.RootFiles [0].Packages [0].Metadata, eBook);
			// Añade los archivos
			AddPages(ePub, eBook);
			// Añade el índice
			AddIndex(ePub, eBook);
			// Indexa las páginas
			IndexPagesNumber(eBook);
			// Devuelve el libro
			return eBook;
	}

	/// <summary>
	///		Asigna los metadatos de un ePub a un eBook
	/// </summary>
	private void AssignMetadata(Metadata metadata, Book eBook)
	{
		eBook.Author = metadata.Author;
		eBook.DateOriginalPublished = metadata.DateOriginalPublished;
		eBook.DatePublished = metadata.DatePublished;
		eBook.Language = metadata.Language;
		eBook.Publisher = metadata.Publisher;
		eBook.Rights = metadata.Rights;
		eBook.Source = metadata.Source;
		eBook.Subject = metadata.Subject;
		eBook.Title = metadata.Title;
	}

	/// <summary>
	///		Añade los archivos
	/// </summary>
	private void AddPages(ePubEBook ePub, Book eBook)
	{
		foreach (RootFile rootFile in ePub.Container.RootFiles)
			foreach (OPFPackage package in rootFile.Packages)
				foreach (Item item in package.Manifest)
					eBook.Files.Add(item.ID, item.ID,
									Path.Combine(Path.GetDirectoryName(rootFile.URL), item.URL),
									item.MediaType);
	}

	/// <summary>
	///		Añade los índices
	/// </summary>
	private void AddIndex(ePubEBook ePub, Book eBook)
	{
		foreach (RootFile rootFile in ePub.Container.RootFiles)
			foreach (OPFPackage package in rootFile.Packages)
			{ 
				// Añade la tabla de contenidos
				AddTableOfContent(Path.GetDirectoryName(rootFile.URL), package.TablesOfContents, eBook.TableOfContent);
				// Añade el índice
				AddIndex(package.Spine, eBook);
			}
	}

	/// <summary>
	///		Añade un índice
	/// </summary>
	private void AddTableOfContent(string pathBase, NCXFilesCollection ncxFiles, IndexItemsCollection indexItems)
	{
		foreach (NCXFile ncxFile in ncxFiles)
		{
			IndexItem item = new IndexItem(ncxFile.Title, ncxFile.ID, null);

				// Añade los puntos de navegación
				foreach (NavPoint navPoint in ncxFile.Pages)
					AddIndex(pathBase, navPoint, item);
				// Añade el elemento a la colección
				indexItems.Add(item);
		}
	}

	/// <summary>
	///		Añade el elemento
	/// </summary>
	private void AddIndex(string pathBase, NavPoint navPoint, IndexItem itemParent)
	{
		IndexItem item = new IndexItem(navPoint.Title, navPoint.ID, Path.Combine(pathBase, navPoint.URL));

			// Añade el elemento 
			itemParent.Items.Add(item);
			// Añade los hijos
			foreach (NavPoint navChild in navPoint.Pages)
				AddIndex(pathBase, navChild, item);
	}

	/// <summary>
	///		Añade un índice
	/// </summary>
	private void AddIndex(ItemsRefCollection itemsRefs, Book book)
	{
		int index = 1;

			foreach (ItemRef itemRef in itemsRefs)
			{
				PageFile page = book.Files.Search(itemRef.IDRef);

					if (page != null)
					{ 
						// Añade la página
						book.Index.Add($"Chapter {index}", itemRef.ID, page.Url);
						// Incrementa el índice
						index++;
					}
			}
	}

	/// <summary>
	///		Indexa el número de páginas
	/// </summary>
	private void IndexPagesNumber(Book eBook)
	{
		int page = 0;

			// Indexa las páginas
			IndexPagesNumber(eBook.Index, ref page);
			// Guarda el número de páginas
			eBook.MaxPageNumber = page;
	}

	/// <summary>
	///		Crea los números de páginas del índice
	/// </summary>
	private void IndexPagesNumber(IndexItemsCollection indexItems, ref int page)
	{
		foreach (IndexItem indexItem in indexItems)
		{ 
			// Asigna el número de página
			indexItem.PageNumber = ++page;
			// Asigna el número de páginas hijas
			if (indexItem.Items != null && indexItem.Items.Count > 0)
				IndexPagesNumber(indexItem.Items, ref page);
		}
	}
}
