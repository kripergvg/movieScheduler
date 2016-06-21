using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace MovieSheduler.Presentation.Core.ValidationAttributes
{
    public class RegexItemAttribute : ValidationAttribute
    {
        private readonly string _regexPattern;

        public RegexItemAttribute(string regexPattern)
        {
            _regexPattern = regexPattern;
        }

        public override bool IsValid(object value)
        {
            var list = value as IEnumerable<string>;
            if (list != null)
            {
                if (list.Any())
                {
                    return list.All(item => Regex.IsMatch(item, _regexPattern));
                }
                return true;
            }
            return false;
        }
    }
}