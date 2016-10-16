using System.Collections.Generic;
using ExtendedObservableCollection.UnitTests.TestModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtendedObservableCollection.UnitTests.Tests
{
	[TestClass]
	public class ObservableCollectionExtendedTests
	{
		private ObservableCollectionExtended<Person> _persons;
		private List<string> _recievedEvents;

		[TestInitialize]
		public void InitializeTestData()
		{
			_recievedEvents = new List<string>();

			_persons = new ObservableCollectionExtended<Person>
			{
				new Person {FirstName = "John", LastName = "Doe"},
				new Person {FirstName = "Jane", LastName = "Doe"}
			};

			_persons.ItemPropertyChanged += (sender, args) => { _recievedEvents.Add(args.PropertyName); };
		}

		[TestCleanup]
		public void CleanupTestData()
		{
			_persons = null;
			_recievedEvents = null;
		}

		[TestMethod]
		public void Test_ObservableCollectionExtended_ModifySingleObject_SinglePropertyChangedEventRaised()
		{
			_persons[0].LastName = "Smith";

			Assert.AreEqual(1, _recievedEvents.Count);
			Assert.AreEqual("LastName", _recievedEvents[0]);
		}

		[TestMethod]
		public void Test_ObservableCollectionExtended_ModifyMultipleObjects_MultiplePropertyChangedEventRaised()
		{
			_persons[0].FirstName = "Jane";
			_persons[0].LastName = "Smith";

			Assert.AreEqual(2, _recievedEvents.Count);
			Assert.AreEqual("FirstName", _recievedEvents[0]);
			Assert.AreEqual("LastName", _recievedEvents[1]);
		}

		[TestMethod]
		public void Test_ObservableCollectionExtended_AddNewObject_NoPropertyChangedEventRaised()
		{
			_persons.Add(new Person {FirstName = "John", LastName = "Smith"});

			Assert.AreEqual(0, _recievedEvents.Count);
		}

		[TestMethod]
		public void Test_ObservableCollectionExtended_RemoveObject_NoPropertyChangedEventRaised()
		{
			_persons.RemoveAt(0);

			Assert.AreEqual(0, _recievedEvents.Count);
		}
	}
}