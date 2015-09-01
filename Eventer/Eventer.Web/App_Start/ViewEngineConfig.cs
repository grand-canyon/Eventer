namespace Eventer.Web
{
    using System.Web.Mvc;

    public class ViewEngineConfig
    {
        internal static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}