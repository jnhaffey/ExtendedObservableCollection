using System.Collections.Generic;
using System.ComponentModel;

namespace ExtendedObservableCollection
{
	public sealed class ObservableCollectionExtended<T> : ObservableCollectionExtendedBase<T>
		where T : INotifyPropertyChanged
	{
		public ObservableCollectionExtended()
		{
		}

		public ObservableCollectionExtended(IEnumerable<T> items)
			: this()
		{
			foreach (var item in items)
				Add(item);
		}
	}
}