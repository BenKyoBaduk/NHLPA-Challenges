using System;

namespace HockeyApi.Features
{
    public class TeamDetailsModel
    {
        public TeamDetailsModel(string teamName, int playerId, string lastName, string firstName, DateTime effectiveDate)
        {
            TeamName = teamName;
            PlayerId = playerId;
            LastName = lastName;
            FirstName = firstName;
            EffectiveDate = effectiveDate;
        }

        public string TeamName { get; set; }
        public int PlayerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
