using Fanex.Data.Repository;
using Alpha.Messages.Configurations;

namespace Alpha.Messages.Criteria
{
    public abstract class OnUserCriteriaBase : CriteriaBase
    {
        public override bool IsValid()
        {
            return !AppSettings.IsDisabledRecheckin;
        }
    }
}