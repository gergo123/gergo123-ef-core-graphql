using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Utils
{
    public static class DisplayAttributeUtils
    {
        /// <summary>
        /// Returns the Name part of DisplayAttribute on the propertyName param on the given obj param.
        /// If nothing is found, returns the original propertyName parameter.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetDisplayName(Type obj, string propertyName)
        {
            var displayString = string.Empty;
            var displayAttribute = obj.GetProperty(propertyName).GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                ?.Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>()?.Single();
            if (displayAttribute != null && string.IsNullOrEmpty(displayAttribute.Name))
            {
                displayString = propertyName;
            }
            else
            {
                displayString = displayAttribute.Name;
            }

            return displayString;
        }
    }
}
