using AndcultureCode.CSharp.Sitefinity.Core.Extensions;
using Shouldly;
using System;
using System.Globalization;
using Telerik.OpenAccess;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Xunit;

namespace AndcultureCode.CSharp.Sitefinity.Core.Tests.Unit.Extensions
{
    public class DynamicContentExtensionsTests : BaseUnitTest
    {
        #region Test Classes

        public class TestValidClass
        {
            public long TestLongProperty { get; set; }
            public bool TestBoolProperty { get; set; }
            public string TestStringProperty { get; set; }
            public DateTimeOffset TestDateTimeOffsetProperty { get; set; }
            public DateTimeOffset? TestNullableDateTimeOffsetProperty { get; set; }
            public TestEnum TestEnumProperty { get; set; }
            public Lstring TestLstringProperty { get; set; }
        }

        public class TestInvalidClass
        {
            public long TestPropertyWithDifferentName { get; set; } // Name is different, shouldn't map.
            public DateTimeOffset? TestDateTimeOffsetProperty { get; set; } // Type is different, shouldn't map.
        }

        // Usually, these classes are dynamically defined through the Sitefinity UI
        // vs. in code, but for the purposes of testing, we must define them
        // in our test class.
        public class TestDynamicClass : DynamicContent
        {
            public long TestLongProperty { get; set; }
            public bool TestBoolProperty { get; set; }
            public string TestStringProperty { get; set; }
            public DateTimeOffset TestDateTimeOffsetProperty { get; set; }
            public DateTimeOffset? TestNullableDateTimeOffsetProperty { get; set; }
            public string TestEnumProperty { get; set; }
            [DataMember]
            [MetadataMapping(true, false)]
            [UserFriendlyDataType(UserFriendlyDataType.LongText)]
            [CommonProperty]
            public Lstring TestLstringProperty { get; set; }
        }

        public enum TestEnum
        {
            TestEnumWithoutNumber,
            TestEnumWithNumber = 4
        }

        #endregion Test Classes

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
            content.TestLstringProperty = new Lstring("test", new CultureInfo("en"));

            // Act
            var mappedContent = content.MapTo<TestValidClass>();

            // Assert
            mappedContent.TestLongProperty.ShouldBe(content.TestLongProperty);
            mappedContent.TestBoolProperty.ShouldBe(content.TestBoolProperty);
            mappedContent.TestStringProperty.ShouldBe(content.TestStringProperty);
            mappedContent.TestDateTimeOffsetProperty.ShouldBe(content.TestDateTimeOffsetProperty);
            mappedContent.TestNullableDateTimeOffsetProperty.ShouldBe(content.TestNullableDateTimeOffsetProperty);
            mappedContent.TestLstringProperty.ShouldBe(content.TestLstringProperty);
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

        [Fact]
        public void MapTo_Given_Enum_Type_With_Number_Value_Properties_Match_Returns_Class_With_Property_Enum_Value_Set()
        {
            // Arrange
            var content = new TestDynamicClass();

            content.TestEnumProperty = TestEnum.TestEnumWithNumber.GetHashCode().ToString();

            // Act
            var mappedContent = content.MapTo<TestValidClass>();

            // Assert
            mappedContent.TestEnumProperty.ShouldBe(TestEnum.TestEnumWithNumber);
        }

        [Fact]
        public void MapTo_Given_Enum_Type_Without_Number_Value_Properties_Match_Returns_Class_With_Property_Enum_Value_Set()
        {
            // Arrange
            var content = new TestDynamicClass();

            content.TestEnumProperty = TestEnum.TestEnumWithoutNumber.ToString();

            // Act
            var mappedContent = content.MapTo<TestValidClass>();

            // Assert
            mappedContent.TestEnumProperty.ShouldBe(TestEnum.TestEnumWithoutNumber);
        }

        [Fact]
        public void MapTo_Given_Enum_Type_With_Null_Property_Value_Does_Not_Throw_Exception()
        {
            // Arrange
            var content = new TestDynamicClass();

            content.TestEnumProperty = null;

            // Act & Assert
            var mappedContent = content.MapTo<TestValidClass>(); 
        }
    }

    internal class DataMemberAttribute : Attribute
    {
    }
}
