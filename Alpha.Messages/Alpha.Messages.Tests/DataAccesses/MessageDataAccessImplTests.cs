namespace Alpha.Messages.Tests.DataAccesses
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Alpha.Messages.Constants;
    using Alpha.Messages.Criteria.Message;
    using Alpha.Messages.Models.Message;
    using Database;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using Messages.DataAccesses;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class MessageDataAccessImplTests
    {
        private IObjectDb objectDb;
        private MessageDataAccessImpl messageDataAccess;

        [SetUp]
        public void SetUp()
        {
            DbSettingProviderManager.StartTestingSession().Run();

            objectDb = Substitute.For<IObjectDb>();
            ObjectDbFactory.Mock((settingKey, commandTimeout) => objectDb);

            messageDataAccess = new MessageDataAccessImpl();
        }

        [Test]
        public void GetMessages_NullCriteria_ThrowsArgumentNullException()
        {
            // Arrange
            ICriteria criteria = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => messageDataAccess.GetMessagesHistory(criteria));
        }

        [Test]
        public void GetMessages_EmptyDataReturnFromDb_ReturnsDefaultData()
        {
            // Arrange
            var dataTable = new DataTable();
            var dataReader = dataTable.CreateDataReader();
            objectDb.ExecuteReader(Arg.Any<Hashtable>()).Returns(dataReader);
            var expectedData = new List<Message>();
            var criteria = new GetMessageCriteria(1, "1", 0, 0, DateTime.Now, DateTime.Now, true, MessageType.Private);

            // Act
            var actualData = messageDataAccess.GetMessagesHistory(criteria);

            // Assert
            Assert.AreEqual(expectedData, actualData);
        }

        [Test]
        public void GetMessages_HasData_ReturnsDataFromObjectDb()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var dataReader = MessageDatabase.MessageData.CreateDataReader();
            objectDb.ExecuteReader(Arg.Any<Hashtable>()).Returns(dataReader);
            var expectedData = new List<Message>
            {
                new Message(
                    1074241137,
                    "@@",
                    "@@",
                    "@@",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    new DateTime(2017,11,07),
                    new DateTime(2017,11,07),
                    new DateTime(2017,11,07),
                    1,
                    false,
                    false,
                    70000101,
                    8,
                    8,
                    "sa",
                    "sa",
                    true,
                    "9799000034, 9799000035, 9799000036",
                    1176086,
                    "agent01",
                    0)
            };
            var criteria = new GetMessageCriteria(1, "1", 0, 0, DateTime.Now, DateTime.Now, true, MessageType.Private);

            // Act
            var actualData = messageDataAccess.GetMessagesHistory(criteria);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedData, actualData).AreEqual);
        }
    }
}
