namespace Bau.Libraries.LibEBooks.Models.ePub.OPF;

/// <summary>
///		Colección de <see cref="Item"/>
/// </summary>
public class ItemsCollection : Base.eBookBaseCollection<Item>
{
	/// <summary>
	///		Obtiene los elementos de determinado tipo
	/// </summary>
	public ItemsCollection SearchMediaType(string mediaType)
	{
		ItemsCollection items = new();

			// Obtiene los elementos con ese tipo de medio
			foreach (Item item in this)
				if (item.MediaType.Equals(mediaType, StringComparison.CurrentCultureIgnoreCase))
					items.Add(item);
			// Devuelve la colección de elementos
			return items;
	}
}
