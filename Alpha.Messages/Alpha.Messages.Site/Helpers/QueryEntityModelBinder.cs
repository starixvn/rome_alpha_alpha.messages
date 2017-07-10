using Alpha.Messages.Site.Requests;
using Sunplus.Agent.Model.Entities;
using System;
using System.Web.Mvc;

namespace Alpha.Messages.Site.Helpers
{
    public class QueryEntityModelBinder : DefaultModelBinder
    {
#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        private static readonly Type[] IUserSessionType = { typeof(IUserSession) };
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        private static readonly Type HotAnnouncementQueryType = typeof(HotAnnouncementQuery);

        protected override object CreateModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            Type modelType)
        {
            if (modelType != null
                && HotAnnouncementQueryType.IsAssignableFrom(modelType)
                && modelType.GetConstructor(IUserSessionType) != null)
            {
                return CreateInstanceOfAssignableReportBaseQuery(modelType);
            }

            return CallBaseCreateModel(controllerContext, bindingContext, modelType);
        }

        protected virtual object CreateInstanceOfAssignableReportBaseQuery(Type modelType)
#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
            => Activator.CreateInstance(modelType, DependencyResolver.Current.GetService<IUserSession>());
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'

        protected virtual object CallBaseCreateModel(
           ControllerContext controllerContext,
           ModelBindingContext bindingContext,
           Type modelType) => base.CreateModel(controllerContext, bindingContext, modelType);
    }
}