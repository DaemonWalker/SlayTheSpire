using Microsoft.AspNetCore.Mvc;
using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SlayTheSpire.Web.Controllers
{
    /// <summary>
    /// 服务发现接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConsulController : ControllerBase
    {
        /// <summary>
        /// 请求服务接口
        /// </summary>
        /// <param name="postData">请求数据</param>
        /// <param name="requestUrl">接口地址</param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostData")]
        public object PostData([FromHeader] string postData, [FromBody] string requestUrl)
        {
            var url = ConsulUtils.GetSaveServiceUrl();
            return PostService($"{url}{requestUrl}", postData);

        }
        private object PostService(string url, string data)
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            if (string.IsNullOrEmpty(data) == false)
            {
                return wc.UploadString(url, "POST", data);
            }
            else
            {
                return wc.UploadString(url, "POST", "");
            }
        }
    }
}
