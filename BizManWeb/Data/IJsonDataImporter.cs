using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Data
{
    public interface IJsonDataImporter
    {
        Task ImportAsync();
    }
}
