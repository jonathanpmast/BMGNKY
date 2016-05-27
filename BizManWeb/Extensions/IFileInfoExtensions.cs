using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;
using System.IO;

namespace BizManWebRC2.Extensions
{
    public static class IFileInfoExtensions
    {
        public static string ReadToEnd(this IFileInfo fileInfo)
        {
            using (var stream = fileInfo.CreateReadStream())
            using (var streamReader = new StreamReader(stream))
                return streamReader.ReadToEnd();
        }

    }
}
