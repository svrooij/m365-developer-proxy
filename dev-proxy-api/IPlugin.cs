
using Azure.Core;
using Azure.Identity;
namespace dev_proxy_api
{
    public interface IPluginRegistration
    {
        public void Register(IServiceCollection services);
    }

    public interface IPlugin
    {
        public void OnRequest(RequestEventArgs args);
    }

    public class MyPluginRegistration : IPluginRegistration
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<TokenCredential>(new DefaultAzureCredential());
            services.AddScoped<IPlugin, MyPlugin>();
        }
    }

    public class MyPlugin : IPlugin
    {
        private readonly TokenCredential _tokenCredential;

        public MyPlugin(TokenCredential tokenCredential)
        {
            _tokenCredential = tokenCredential;
        }

        public void OnRequest(RequestEventArgs args)
        {
            // Do something with the token credential
        }
    }
}
