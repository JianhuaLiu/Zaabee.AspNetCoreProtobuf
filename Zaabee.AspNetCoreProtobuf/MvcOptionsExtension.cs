using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Zaabee.AspNetCoreProtobuf
{
    public static class MvcOptionsExtension
    {
        public static void AddProtobufSupport(this MvcOptions options)
        {
            options.InputFormatters.Add(new ProtobufInputFormatter());
            options.OutputFormatters.Add(new ProtobufOutputFormatter());
            options.FormatterMappings.SetMediaTypeMappingForFormat("protobuf",
                MediaTypeHeaderValue.Parse("application/x-protobuf"));
        }
    }
}