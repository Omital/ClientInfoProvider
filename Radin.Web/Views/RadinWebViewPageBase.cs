using Abp.Web.Mvc.Views;

namespace Radin.Web.Views
{
    public abstract class RadinWebViewPageBase : RadinWebViewPageBase<dynamic>
    {

    }

    public abstract class RadinWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected RadinWebViewPageBase()
        {
            LocalizationSourceName = RadinConsts.LocalizationSourceName;
        }
    }
}