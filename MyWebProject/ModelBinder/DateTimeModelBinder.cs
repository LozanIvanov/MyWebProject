using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace MyWebProject.ModelBinder
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly string customDateFormat;

        public DateTimeModelBinder(string _customDateFormat)
        {
            customDateFormat=_customDateFormat;//vzimame data
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                DateTime actualvalue = DateTime.MinValue;
                bool success = false;
                string datevalue = valueResult.FirstValue;
                try
                {
                    actualvalue = DateTime.ParseExact(datevalue, customDateFormat, CultureInfo.InvariantCulture);//pri podaden format
                   
                    success = true;
                }
                catch (FormatException )
                {
                    //pri exeptions,kogato ne e uspial parseExact
                    try
                    {
                        actualvalue = DateTime.Parse(datevalue, new CultureInfo("bg-bg"));
                    }
                    catch (Exception e)
                    {

                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    }
                   
                }
                catch (Exception e)
                {

                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
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
