using AndcultureCode.CSharp.Sitefinity.Core.Extensions;
using Shouldly;
using System;
using Telerik.Sitefinity.DynamicModules.Model;
using Xunit;

namespace AndcultureCode.CSharp.Sitefinity.Core.Tests.Unit.Extensions
{
    public class DynamicContentExtensionsTests : BaseUnitTest
    {
        public class TestValidClass
        {
            public long TestLongProperty { get; set; }
            public bool TestBoolProperty { get; set; }
            public string TestStringProperty { get; set; }
            public DateTimeOffset TestDateTimeOffsetProperty { get; set; }
            public DateTimeOffset? TestNullableDateTimeOffsetProperty { get; set; }
        }

        public class TestInvalidClass
        {
            public long TestPropertyWithDifferentName { get; set; }
            public DateTimeOffset? TestDateTimeOffsetProperty { get; set; } // Type is different, shouldn't map.
        }

        // Usually, these types are dynamically defined through the Sitefinity UI
        // vs. in code, but for the purposes of testing, we must define them
        // in our test class.
        public class TestDynamicClass : DynamicContent
        {
            public long TestLongProperty { get; set; }
            public bool TestBoolProperty { get; set; }
            public string TestStringProperty { get; set; }
            public DateTimeOffset TestDateTimeOffsetProperty { get; set; }
            public DateTimeOffset? TestNullableDateTimeOffsetProperty { get; set; }
        }

        [Fact]
        public void MapTo_Given_Null_DynamicContent_Throws_ArgumentNullException()
        {
            // Arrange
            DynamicContent test = null;

            // Act & Assert
            Should.Throw<ArgumentNullException>(() => test.MapTo<TestValidClass>());
        }

        [Fact]
        public void MapTo_Given_Properties_Match_Returns_Class_With_Property_Values_Populated_From_DynamicContent()
        {
            // Arrange
            var content = new TestDynamicClass();

            content.TestLongProperty = 1;
            content.TestBoolProperty = true;
            content.TestStringProperty = "Test";
            content.TestDateTimeOffsetProperty = DateTimeOffset.Now;
            content.TestNullableDateTimeOffsetProperty = DateTimeOffset.Now;

            // Act
            var mappedContent = content.MapTo<TestValidClass>();

            // Assert
            mappedContent.TestLongProperty.ShouldBe(content.TestLongProperty);
            mappedContent.TestBoolProperty.ShouldBe(content.TestBoolProperty);
            mappedContent.TestStringProperty.ShouldBe(content.TestStringProperty);
            mappedContent.TestDateTimeOffsetProperty.ShouldBe(content.TestDateTimeOffsetProperty);
            mappedContent.TestNullableDateTimeOffsetProperty.ShouldBe(content.TestNullableDateTimeOffsetProperty);
        }

        [Fact]
        public void MapTo_Given_Properties_Dont_Match_Returns_Class_With_Property_Values_Instantiated_As_Default()
        {
            // Arrange
            var content = new TestDynamicClass();

            content.TestLongProperty = 1;
            content.TestBoolProperty = true;
            content.TestStringProperty = "Test";
            content.TestDateTimeOffsetProperty = DateTimeOffset.Now;
            content.TestNullableDateTimeOffsetProperty = DateTimeOffset.Now;

            // Act
            var mappedContent = content.MapTo<TestInvalidClass>();

            // Assert
            mappedContent.TestPropertyWithDifferentName.ShouldBe(default(long));
            mappedContent.TestDateTimeOffsetProperty.ShouldBe(default(DateTimeOffset?));
        }
    }
}
