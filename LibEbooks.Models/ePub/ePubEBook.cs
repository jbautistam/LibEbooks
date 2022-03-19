using System;

namespace Bau.Libraries.LibEBooks.Models.ePub
{
	/// <summary>
	///		Clase con los datos de un eBook en el formato ePub
	/// </summary>
	public class ePubEBook
	{
		/// <summary>
		///		Archivo contenedor. Indica dónde está el archivo de índice
		/// </summary>
		public Container.ContainerFile Container { get; set; } = new Container.ContainerFile();
	}
}
