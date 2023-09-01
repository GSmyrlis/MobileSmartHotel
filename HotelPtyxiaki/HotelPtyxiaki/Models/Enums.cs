using System;
using System.Linq;

namespace HotelPtyxiaki.Models
{
    public class Enums
    {
        // Define a custom attribute class for associating string and image with enum values
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        sealed class EnumWithPropertiesAttribute : Attribute
        {
            public string Name { get; }
            public string ImagePath { get; } 

            public EnumWithPropertiesAttribute(string name, string imagePath)
            {
                Name = name;
                ImagePath = imagePath;
            }
        }

        public enum RequestState
        {
            [EnumWithProperties("Unsent", null)]
            Unsent = 0,
            [EnumWithProperties("Pending", "/Assets/pending.png")]
            Pending = 1,
            [EnumWithProperties("Approved", "/Assets/approved.png")]
            Approved = 2,
            [EnumWithProperties("Declined", "/Assets/declined.png")]
            Declined = 3,
            [EnumWithProperties("Done", null)]
            Done = 4
        }

        public string GetImagePathForRequestState(RequestState state)
        {
            // Use reflection to get the custom attribute for the enum value
            System.Reflection.FieldInfo enumField = state.GetType().GetField(state.ToString());
            Enums.EnumWithPropertiesAttribute attribute = enumField.GetCustomAttributes(typeof(Enums.EnumWithPropertiesAttribute), false)
                .Cast<Enums.EnumWithPropertiesAttribute>()
                .FirstOrDefault();

            // Check if the attribute exists and return the ImagePath
            if (attribute != null)
            {
                return attribute.ImagePath;
            }

            // Default value if the attribute is not found
            return string.Empty;
        }
    }
}
