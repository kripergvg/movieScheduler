using System;
using MovieSheduler.Application.Infrastructure;
using Shouldly;
using Xunit;

namespace MovieSheduler.Application.Tests
{
    public class ValidationDictionaryTests
    {
        [Fact]
        public void IsValid_Should_Return_False()
        {
            //Arrange        
            var validationDictionary = new ValidationDictionary();

            //Act
            validationDictionary.AddError("testKey", "testMessage");

            //Assert            
            validationDictionary.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_Should_Return_True()
        {
            //Arrange        
            var validationDictionary = new ValidationDictionary();

            //Act

            //Assert            
            validationDictionary.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Errors_Shold_Contain_Error()
        {
            //Arrange        
            var validationDictionary = new ValidationDictionary();

            //Act
            validationDictionary.AddError("testKey", "testMessage");

            //Assert            
            validationDictionary.Errors.ShouldContain(e => e.Key == "testKey" && e.Value == "testMessage");
        }

        [Fact]
        public void AddError_Should_Throw_ArgumentExteption()
        {
            //Arrange        
            var validationDictionary = new ValidationDictionary();

            //Act
            
            //Assert 
            Should.Throw<ArgumentException>(() => validationDictionary.AddError("", "testMessage"));
            Should.Throw<ArgumentException>(() => validationDictionary.AddError("", ""));
            Should.Throw<ArgumentException>(() => validationDictionary.AddError("", null));
            Should.Throw<ArgumentException>(() => validationDictionary.AddError(null, null));
            Should.Throw<ArgumentException>(() => validationDictionary.AddError(null, ""));
        }
    }
}
