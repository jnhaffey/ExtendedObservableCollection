using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ExtendedObservableCollection
{
	public sealed class ObservableCollectionWithUniquePropertyValue<T, TPt> : ObservableCollectionExtendedBase<T>
		where T : INotifyPropertyChanged
	{
		private readonly string _propertyName;

		public ObservableCollectionWithUniquePropertyValue(string propertyName)
		{
			_propertyName = propertyName;
		}

		public ObservableCollectionWithUniquePropertyValue(IEnumerable<T> items, string propertyName)
			: this(propertyName)
		{
			foreach (var item in items)
				Add(item);
		}

		#region Overrides of ObservableCollectionExtendedBase<T>

		protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var listedValues = (from item in Items
				let propertyInfo = item.GetType().GetProperty(_propertyName)
				where propertyInfo != null
				select (TPt) propertyInfo.GetValue(item, null)).ToList();
			if (listedValues.Any(value => listedValues.Count(p => p.Equals(value)) != 1))
				throw new Exception($"{_propertyName} is not unique.");


			base.OnPropertyChanged(sender, e);
		}

		#endregion
	}
}