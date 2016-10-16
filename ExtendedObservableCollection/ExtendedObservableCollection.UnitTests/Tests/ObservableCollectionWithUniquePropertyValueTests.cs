using System;
using System.Collections.Generic;
using ExtendedObservableCollection.UnitTests.TestModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtendedObservableCollection.UnitTests.Tests
{
	[TestClass]
	public class ObservableCollectionWithUniquePropertyValueTests
	{
		private ObservableCollectionWithUniquePropertyValue<Person, string> _persons;
		private List<string> _recievedEvents;

		[TestInitialize]
		public void InitializeTestData()
		{
			_recievedEvents = new List<string>();

			_persons = new ObservableCollectionWithUniquePropertyValue<Person, string>("LastName")
			{
				new Person {FirstName = "John", LastName = "Doe"},
				new Person {FirstName = "John", LastName = "Smith"}
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
		public void Test_ObservableCollectionWithUniquePropertyValue_ModifySingleObject_SinglePropertyChangedEventRaised()
		{
			_persons[0].LastName = "Wayne";

			Assert.AreEqual(1, _recievedEvents.Count);
			Assert.AreEqual("LastName", _recievedEvents[0]);
		}

		[TestMethod]
		public void Test_ObservableCollectionWithUniquePropertyValue_ModifyMultipleObjects_MultiplePropertyChangedEventRaised()
		{
			_persons[0].FirstName = "Jane";
			_persons[0].LastName = "Wayne";

			Assert.AreEqual(2, _recievedEvents.Count);
			Assert.AreEqual("FirstName", _recievedEvents[0]);
			Assert.AreEqual("LastName", _recievedEvents[1]);
		}

		[TestMethod]
		public void Test_ObservableCollectionWithUniquePropertyValue_AddNewObject_NoPropertyChangedEventRaised()
		{
			_persons.Add(new Person { FirstName = "Jane", LastName = "Smith" });

			Assert.AreEqual(0, _recievedEvents.Count);
		}

		[TestMethod]
		public void Test_ObservableCollectionWithUniquePropertyValue_RemoveObject_NoPropertyChangedEventRaised()
		{
			_persons.RemoveAt(0);

			Assert.AreEqual(0, _recievedEvents.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception), "LastName is not unique.")]
		public void Test_ObservableCollectionWithUniquePropertyValue_UniquePropertyExceptionThrown()
		{

			_persons[1].LastName = "Doe";
			Assert.Fail();
		}
	}
}