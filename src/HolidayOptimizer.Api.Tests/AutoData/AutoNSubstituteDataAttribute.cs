using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace HolidayOptimizer.Api.Tests.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
