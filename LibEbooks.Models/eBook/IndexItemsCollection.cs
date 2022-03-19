using System;

namespace Bau.Libraries.LibEBooks.Models.eBook
{
	/// <summary>
	///		Colecci�n de <see cref="IndexItem"/>
	/// </summary>
	public class IndexItemsCollection : Base.eBookBaseCollection<IndexItem>
	{
		/// <summary>
		///		A�ade un elemento al �ndice
		/// </summary>
		public void Add(string name, string idPage, string url)
		{
			Add(new IndexItem(name, idPage, url));
		}
	}
}
