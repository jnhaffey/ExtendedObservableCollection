using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ExtendedObservableCollection
{
	public abstract class ObservableCollectionExtendedBase<T> : ObservableCollection<T>
		where T : INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler ItemPropertyChanged;

		#endregion

		#region Protected Methods

		protected override void ClearItems()
		{
			UnRegisterPropertyChange(this);
			base.ClearItems();
		}

		protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender,
				IndexOf((T) sender));
			OnCollectionChanged(args);

			ItemPropertyChanged?.Invoke(sender, e);
		}

		#endregion

		#region Constructors

		protected ObservableCollectionExtendedBase()
		{
			CollectionChanged += OnCollectionChanged;
		}

		protected ObservableCollectionExtendedBase(IEnumerable<T> items)
			: this()
		{
			foreach (var item in items)
				Add(item);
		}

		#endregion

		#region Private Methods

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					RegisterPropertyChange(e.NewItems);
					break;
				case NotifyCollectionChangedAction.Remove:
				case NotifyCollectionChangedAction.Reset:
					UnRegisterPropertyChange(e.OldItems);
					break;
				case NotifyCollectionChangedAction.Replace:
					UnRegisterPropertyChange(e.OldItems);
					RegisterPropertyChange(e.NewItems);
					break;
				case NotifyCollectionChangedAction.Move:
					// DO NOTHING
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void RegisterPropertyChange(IList items)
		{
			foreach (INotifyPropertyChanged item in items)
				if (item != null)
					item.PropertyChanged += OnPropertyChanged;
		}

		private void UnRegisterPropertyChange(IList items)
		{
			foreach (INotifyPropertyChanged item in items)
				if (item != null)
					item.PropertyChanged -= OnPropertyChanged;
		}

		#endregion
	}
}