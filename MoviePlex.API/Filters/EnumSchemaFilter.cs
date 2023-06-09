using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MoviePlex.API.Filters;

public class EnumSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Changes the numbers in the input fields to names 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (! context.Type.IsEnum)
        {
            return;
        }
        
        model.Enum.Clear(); 
        Enum.GetNames(context.Type)
            .ToList()
            .ForEach(name => model.Enum.Add(new OpenApiString($"{name}")));
        
        model.Type = "string"; 
        model.Format = string.Empty;
    }
}