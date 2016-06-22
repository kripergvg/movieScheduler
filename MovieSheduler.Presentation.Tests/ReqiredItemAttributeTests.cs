using System.Collections.Generic;
using MovieSheduler.Presentation.Core.ValidationAttributes;
using Shouldly;
using Xunit;

namespace MovieSheduler.Presentation.Tests
{
    public class ReqiredItemAttributeTests
    {
        [Fact]
        public void IsValid_Should_Return_False()
        {
            //Arrange        
            var requiredAttribute = new RequiredItemAttribute();
            var emptyList = new List<string>();

            //Act
            var valid = requiredAttribute.IsValid(emptyList);

            //Assert            
            valid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_Should_Return_True()
        {
            //Arrange        
            var requiredAttribute = new RequiredItemAttribute();
            var emptyList = new List<string>
            {
                "test"
            };

            //Act
            var valid = requiredAttribute.IsValid(emptyList);

            //Assert            
            valid.ShouldBeTrue();
        }

        [Fact]
        public void IsValid_Should_Return_False_When_Not_Collection()
        {
            //Arrange        
            var requiredAttribute = new RequiredItemAttribute();
            var testObject = new object();

            //Act
            var valid = requiredAttribute.IsValid(testObject);

            //Assert            
            valid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_Should_Return_False_When_Not_Null()
        {
            //Arrange        
            var requiredAttribute = new RequiredItemAttribute();

            //Act
            var valid = requiredAttribute.IsValid(null);

            //Assert            
            valid.ShouldBeFalse();
        }
    }
}
