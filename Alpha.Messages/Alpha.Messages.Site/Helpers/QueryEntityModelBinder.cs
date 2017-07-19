using Alpha.Messages.Site.Requests;
using Sunplus.Agent.Model.Entities;
using System;
using System.Web.Mvc;

namespace Alpha.Messages.Site.Helpers
{
    public class QueryEntityModelBinder : DefaultModelBinder
    {
        private static readonly Type[] IUserSessionType = { typeof(IUserSession) };
        private static readonly Type PopupAnnouncementQueryType = typeof(PopupAnnouncementQuery);
        private static readonly Type HeaderAnnouncementQueryType = typeof(HeaderAnnouncementQuery);

        protected override object CreateModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            Type modelType)
        {
            if (modelType != null
                && modelType.GetConstructor(IUserSessionType) != null
                && (PopupAnnouncementQueryType.IsAssignableFrom(modelType)
                || HeaderAnnouncementQueryType.IsAssignableFrom(modelType)))
            {
                return CreateInstanceOfAssignableReportBaseQuery(modelType);
            }

            return CallBaseCreateModel(controllerContext, bindingContext, modelType);
        }

        protected virtual object CreateInstanceOfAssignableReportBaseQuery(Type modelType)
            => Activator.CreateInstance(modelType, DependencyResolver.Current.GetService<IUserSession>());

        protected virtual object CallBaseCreateModel(
           ControllerContext controllerContext,
           ModelBindingContext bindingContext,
           Type modelType) => base.CreateModel(controllerContext, bindingContext, modelType);
    }
}