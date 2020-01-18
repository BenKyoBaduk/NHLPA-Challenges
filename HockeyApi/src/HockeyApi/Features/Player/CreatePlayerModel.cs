using System;

namespace HockeyApi.Features
{
    public class CreatePlayerModel
    {
        public string TeamCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
