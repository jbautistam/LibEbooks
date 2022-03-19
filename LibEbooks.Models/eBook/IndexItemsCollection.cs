using System;

namespace Bau.Libraries.LibEBooks.Models.eBook
{
	/// <summary>
	///		Colección de <see cref="IndexItem"/>
	/// </summary>
	public class IndexItemsCollection : Base.eBookBaseCollection<IndexItem>
	{
		/// <summary>
		///		Añade un elemento al índice
		/// </summary>
		public void Add(string name, string idPage, string url)
		{
			Add(new IndexItem(name, idPage, url));
		}
	}
}
