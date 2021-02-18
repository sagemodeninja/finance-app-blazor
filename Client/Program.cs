using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazorFluentUI;

namespace FinanceApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazorFluentUI();

            builder.Services.AddHttpClient("FinanceApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("FinanceApp.ServerAPI"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://01c9ae72-0e14-4cd9-8d3f-4d76ba96ba1a/api.access");
                options.ProviderOptions.LoginMode = "redirect";
            });

            #region refer: https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/graph-api?view=aspnetcore-5.0#graph-sdk

            builder.Services.AddScoped<GraphAPIAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("GraphAPI",
                    client => client.BaseAddress = new Uri("https://graph.microsoft.com"))
                .AddHttpMessageHandler<GraphAPIAuthorizationMessageHandler>();

            #endregion

            await builder.Build().RunAsync();
        }
    }
}
