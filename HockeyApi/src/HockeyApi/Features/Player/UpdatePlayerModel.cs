using System;

namespace HockeyApi.Features
{
    public class UpdatePlayerModel
    {
        public int PlayerId { get; set; }
        public string TeamCode { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
