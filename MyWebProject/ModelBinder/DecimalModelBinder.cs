using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace MyWebProject.ModelBinder
{
    public class DecimalModelBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if(valueResult!= ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                decimal actualvalue = 0;
                bool success = false;
                try
                {
                    string decvalue = valueResult.FirstValue;
                    decvalue = decvalue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decvalue = decvalue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    actualvalue = Convert.ToDecimal(decvalue, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch(FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe,bindingContext.ModelMetadata);
                }
                if(success)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualvalue);
                }
            }
            return Task.CompletedTask;
        }
    }
}
