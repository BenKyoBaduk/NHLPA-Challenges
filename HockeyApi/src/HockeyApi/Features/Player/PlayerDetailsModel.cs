using System;

namespace HockeyApi.Features
{
    public class PlayerDetailsModel
    {
        public PlayerDetailsModel(int playerId, string lastName, string firstName, string teamCode, int rosterTransactionTypeId, DateTime effectiveDate)
        {
            PlayerId = playerId;
            LastName = lastName;
            FirstName = firstName;
            TeamCode = teamCode;
            RosterTransactionTypeId = rosterTransactionTypeId;
            EffectiveDate = effectiveDate;
        }

        public int PlayerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string TeamCode { get; set; }
        public int RosterTransactionTypeId { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
