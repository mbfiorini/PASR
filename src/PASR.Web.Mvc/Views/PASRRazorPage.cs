using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace PASR.Web.Views
{
    public abstract class PASRRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected PASRRazorPage()
        {
            LocalizationSourceName = PASRConsts.LocalizationSourceName;
        }
    }
}
