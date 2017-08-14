namespace Alpha.Messages.Client.Helpers
{
    using Announcements.Models;
    using Announcements.Specs;
    using Newtonsoft.Json.Linq;
    using System;

    public class SpecificationConverter : JsonCreationConverter<ISpecification<IParam>>
    {
        protected override Type GetTargetType(Type type, JObject obj)
        {
            return Type.GetType(obj.GetValue("TypeName", StringComparison.OrdinalIgnoreCase).ToString());
        }
    }
}