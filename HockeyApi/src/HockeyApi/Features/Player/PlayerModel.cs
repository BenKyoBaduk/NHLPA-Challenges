using System;

namespace HockeyApi.Features
{
    public class PlayerModel
    {
        public PlayerModel(int playerId, string lastName, string firstName)
        {
            PlayerId = playerId;
            LastName = lastName;
            FirstName = firstName;
        }

        public int PlayerId { get; set; }
        public string LastName { get; set;  }
        public string FirstName { get; set;  }
    }
}
