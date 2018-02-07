using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Zaabee.AspNetCoreProtobuf
{
    public static class MvcOptionsExtension
    {
        public static void AddProtobufSupport(this MvcOptions options, string contentType = "application/x-protobuf",
            string format = "protobuf")
        {
            if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentNullException(nameof(contentType));
            options.InputFormatters.Add(new ProtobufInputFormatter());
            options.OutputFormatters.Add(new ProtobufOutputFormatter(contentType));
            options.FormatterMappings.SetMediaTypeMappingForFormat(format, MediaTypeHeaderValue.Parse(contentType));
        }
    }
}