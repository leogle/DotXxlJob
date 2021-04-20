using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DotXxlJob.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNetCoreExecutor
{
    public class XxlJobExecutorMiddleware
    {
        private readonly IServiceProvider _provider;
        private readonly RequestDelegate _next;

        private readonly XxlRestfulServiceHandler _rpcService;
        private List<string> _inceptorUrls;
        public XxlJobExecutorMiddleware(IServiceProvider provider, RequestDelegate next)
        {
            this._provider = provider;
            this._next = next;
            this._rpcService = _provider.GetRequiredService<XxlRestfulServiceHandler>();
            this._inceptorUrls.AddRange(new string[]{ "/run", "/idleBeat", "/beat", "/kill", "/log" });
        }


        public async Task Invoke(HttpContext context)
        {
            string contentType = context.Request.ContentType;

            if ("POST".Equals(context.Request.Method, StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrEmpty(contentType)
                && _inceptorUrls.Contains(context.Request.Path.Value)
                && contentType.ToLower().StartsWith("application/json"))
            {

                await _rpcService.HandlerAsync(context.Request,context.Response);              

                return;
            }
            
            await _next.Invoke(context);
        }
    }
}