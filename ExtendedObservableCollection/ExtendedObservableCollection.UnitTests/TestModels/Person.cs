using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExtendedObservableCollection.UnitTests.TestModels
{
	public class Person : INotifyPropertyChanged
	{
		private string _firstName;
		private string _lastName;

		public string FirstName
		{
			get { return _firstName; }
			set
			{
				if (value == _firstName) return;
				_firstName = value;
				OnPropertyChanged("FirstName");
			}
		}

		public string LastName
		{
			get { return _lastName; }
			set
			{
				if (value == _lastName) return;
				_lastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}