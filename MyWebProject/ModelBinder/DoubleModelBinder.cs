using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace MyWebProject.ModelBinder
{
    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                double actualvalue = 0;
                bool success = false;
                try
                {
                    string decvalue = valueResult.FirstValue;
                    decvalue = decvalue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decvalue = decvalue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    actualvalue = Convert.ToDouble(decvalue, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }
                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualvalue);
                }
            }
            return Task.CompletedTask;
        }
    }
}
