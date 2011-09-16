namespace Stump.Server.BaseServer.Plugins
{
    public static class PluginExtensions
    {
        public static string GetDefaultDescription(this IPlugin plugin)
        {
            return string.Format("'{0}' v{1} by {2}", plugin.Name, plugin.GetType().Assembly.GetName().Version, plugin.Author);
        }
    }

    public interface IPlugin
    {
        string Name
        {
            get;
        }

        string Description
        {
            get;
        }

        string Author
        {
            get;
        }

        string Version
        {
            get;
        }

        void LoadConfig(PluginContext context);

        void Initialize();
        void Shutdown();

        void Dispose();
    }
}