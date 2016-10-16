using System;
using System.Collections.Generic;
using ExtendedObservableCollection.UnitTests.TestModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtendedObservableCollection.UnitTests.Tests
{
	[TestClass]
	public class ObservableCollectionWithSinglePropertyValueTests
	{
		private ObservableCollectionWithSinglePropertyValue<PhoneNumber, bool> _phoneNumbers;
		private List<string> _recievedEvents;

		[TestInitialize]
		public void InitializeTestData()
		{
			_recievedEvents = new List<string>();

			_phoneNumbers = new ObservableCollectionWithSinglePropertyValue<PhoneNumber, bool>("IsDefault", true)
			{
				new PhoneNumber {IsDefault = true, Number = "123456789"},
				new PhoneNumber {IsDefault = false, Number = "987654321"},
				new PhoneNumber {IsDefault = false, Number = "147258369"}
			};

			_phoneNumbers.ItemPropertyChanged += (sender, args) => { _recievedEvents.Add(args.PropertyName); };
		}

		[TestCleanup]
		public void CleanupTestData()
		{
			_phoneNumbers = null;
			_recievedEvents = null;
		}

		[TestMethod]
		public void Test_ObservableCollectionWithSinglePropertyValue_ModifySingleObject_SinglePropertyChangedEventRaised()
		{
			_phoneNumbers[0].Number = "951357852";

			Assert.AreEqual(1, _recievedEvents.Count);
			Assert.AreEqual("Number", _recievedEvents[0]);
		}

		[TestMethod]
		public void Test_ObservableCollectionWithSinglePropertyValue_ModifyMultipleObjects_MultiplePropertyChangedEventRaised()
		{
			_phoneNumbers[0].Number = "951357852";
			_phoneNumbers[0].IsDefault = false;

			Assert.AreEqual(2, _recievedEvents.Count);
			Assert.AreEqual("Number", _recievedEvents[0]);
			Assert.AreEqual("IsDefault", _recievedEvents[1]);
		}

		[TestMethod]
		public void Test_ObservableCollectionWithSinglePropertyValue_AddNewObject_NoPropertyChangedEventRaised()
		{
			_phoneNumbers.Add(new PhoneNumber {IsDefault = false, Number = "654852753"});

			Assert.AreEqual(0, _recievedEvents.Count);
		}

		[TestMethod]
		public void Test_ObservableCollectionWithSinglePropertyValue_AddNewObject_ExceptionThrown()
		{
			_phoneNumbers.Add(new PhoneNumber {IsDefault = true, Number = "654852753"});

			Assert.AreEqual(0, _recievedEvents.Count);
		}

		[TestMethod]
		public void Test_ObservableCollectionWithSinglePropertyValue_RemoveObject_NoPropertyChangedEventRaised()
		{
			_phoneNumbers.RemoveAt(0);

			Assert.AreEqual(0, _recievedEvents.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception), "LastName is not unique.")]
		public void Test_ObservableCollectionWithSinglePropertyValue_ExceptionThrown()
		{
			_phoneNumbers[1].IsDefault = true;
			Assert.Fail();
		}
	}
}