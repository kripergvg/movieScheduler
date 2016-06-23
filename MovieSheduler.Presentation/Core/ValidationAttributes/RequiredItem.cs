using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
namespace MovieSheduler.Presentation.Core.ValidationAttributes
{
    public class RequiredItemAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IEnumerable;

            if (list != null)
            {
                var enumerator = list.GetEnumerator();
                enumerator.MoveNext();
                try
                {
                    var current = enumerator.Current;
                    return true;
                }
                //If error rised than collection is empty
                catch (InvalidOperationException)
                {
                    return false;
                }
                
            }
            return false;
        }
    }
}