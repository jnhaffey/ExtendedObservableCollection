using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ExtendedObservableCollection
{
	public sealed class ObservableCollectionWithSinglePropertyValue<T, PT> : ObservableCollectionExtendedBase<T>
		where T : INotifyPropertyChanged
	{
		private readonly string _propertyName;
		private readonly PT _singleValue;

		public ObservableCollectionWithSinglePropertyValue(string propertyName, PT singleValue)
		{
			_propertyName = propertyName;
			_singleValue = singleValue;
		}

		public ObservableCollectionWithSinglePropertyValue(IEnumerable<T> items, string propertyName, PT singleValue)
			: this(propertyName, singleValue)
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
				select (PT) propertyInfo.GetValue(item, null)).ToList();
			if (listedValues.Any(value => listedValues.Count(p => p.Equals(_singleValue)) > 1))
				throw new Exception($"{_propertyName} with value {_singleValue} is used more than once.");


			base.OnPropertyChanged(sender, e);
		}

		#endregion
	}
}