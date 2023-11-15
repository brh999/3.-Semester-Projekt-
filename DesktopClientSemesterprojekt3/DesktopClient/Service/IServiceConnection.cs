using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Service
{
    public interface IServiceConnection
    {
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }

        Task<HttpResponseMessage?> CallServiceGet();
        Task<HttpResponseMessage?> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage?> CallServicePut(StringContent postJson);
        Task<HttpResponseMessage?> CallServiceDelete();
    }
}
