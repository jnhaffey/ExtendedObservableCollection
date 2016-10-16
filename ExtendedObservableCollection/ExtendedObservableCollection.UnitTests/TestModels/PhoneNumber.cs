using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExtendedObservableCollection.UnitTests.TestModels
{
	public class PhoneNumber : INotifyPropertyChanged
	{
		private bool _isDefault;
		private string _number;

		public string Number
		{
			get { return _number; }
			set
			{
				if (value == _number) return;
				_number = value;
				OnPropertyChanged("Number");
			}
		}

		public bool IsDefault
		{
			get { return _isDefault; }
			set
			{
				if (value == _isDefault) return;
				_isDefault = value;
				OnPropertyChanged("IsDefault");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}