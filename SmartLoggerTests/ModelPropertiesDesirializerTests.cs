using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLogger.Deserializers;
using System;

namespace SmartLoggerTests
{
    [TestClass]
    public class ModelPropertiesDesirializerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string expectedResult = "{{ Name = yolo }{ Id = 2 }{ Age = 5 }{ Test1 = test }\r\n{ TestTime = " + DateTime.UtcNow + " }}";

            var model = new TestModel
            {
                Id = 2,
                Age = 5,
                Name = "yolo",
                Test1 = "test",
                TestTime = DateTime.UtcNow
            };

            // Act
            string result = ModelPropertiesDeserializer.Deserialize(model);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
