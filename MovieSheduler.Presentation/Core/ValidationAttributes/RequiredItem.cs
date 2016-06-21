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
                return enumerator.Current != null;
            }
            return false;
        }
    }
}