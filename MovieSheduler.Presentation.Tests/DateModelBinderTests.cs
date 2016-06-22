using System;
using System.Web.Mvc;
using MovieSheduler.Presentation.Core.ModelBinder;
using Shouldly;
using Xunit;

namespace MovieSheduler.Presentation.Tests
{
    public class DateModelBinderTests
    {
        [Fact]
        public void BindModel_Should_Return_Date()
        {
            //Arrange     
            var formCollection = new FormCollection
            {
                {"Date", "22.06.2016"}
            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(DateModelBinder));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Date",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };


            var modelBinder = new DateModelBinder();

            //Act
            object result = modelBinder.BindModel(null, bindingContext);

            //Assert            
            result.ShouldBe(new DateTime(2016, 6, 22));
        }

        [Fact]
        public void BindModel_Should_Throw_FormatException()
        {
            //Arrange     
            var formCollection = new FormCollection
            {
                {"Date", "testString"}
            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(DateModelBinder));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Date",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            var modelBinder = new DateModelBinder();

            //Act

            //Assert   
            Should.Throw<FormatException>(() => modelBinder.BindModel(null, bindingContext));
        }

        [Fact]
        public void BindModel_Should_Throw_ArgumentNullException()
        {
            //Arrange     
            var modelBinder = new DateModelBinder();

            //Act

            //Assert   
            Should.Throw<ArgumentNullException>(() => modelBinder.BindModel(null, null));
        }
    }
}
