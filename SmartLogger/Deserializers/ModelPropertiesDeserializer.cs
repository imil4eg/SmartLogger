using System;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace SmartLogger.Deserializers
{
    internal sealed class ModelPropertiesDeserializer
    {
        public static string Deserialize<TModel>(TModel model) where TModel : new()
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            const int propertyPerLineCount = 4;
            PropertyInfo[] modelProperties = typeof(TModel).GetProperties();
            var sb = new StringBuilder("{");
            for(int propertyIndex = 0; propertyIndex < modelProperties.Length; propertyIndex++)
            {
                string value = string.Format(CultureInfo.InvariantCulture, "{{ {0} = {1} }}", modelProperties[propertyIndex].Name, modelProperties[propertyIndex].GetValue(model));
                if ((propertyIndex + 1) % propertyPerLineCount == 0)
                {
                    sb.AppendLine(value);
                    continue;
                }

                sb.Append(value);
            }

            sb.Append("}");

            return sb.ToString();
        }
    }
}
