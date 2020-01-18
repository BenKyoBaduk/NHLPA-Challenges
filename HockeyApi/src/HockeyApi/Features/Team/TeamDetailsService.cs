using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HockeyApi.Common;

namespace HockeyApi.Features
{
	public interface ITeamDetailsService
	{
		IEnumerable<TeamDetailsModel> List(string teamCode);
	}

	public class TeamDetailsService : ITeamDetailsService
	{
		private readonly IDb _db;

		public TeamDetailsService(IDb db)
		{
			_db = db;
		}

		public IEnumerable<TeamDetailsModel> List(string teamCode)
		{
			var teamDetails = new HashSet<TeamDetailsModel>();

			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@teamCode", teamCode);
				cmd.CommandText = string.Format($@"
                    SELECT      t.team_name
							  , p.player_id
							  , p.last_name
                              , p.first_name
                              , r.effective_date
					FROM		team t
					INNER JOIN  roster_transaction r
								ON r.team_code = t.team_code
					INNER JOIN  player p
								ON p.player_id = r.player_id
					WHERE		t.team_code = @teamCode");

				using (var rd = cmd.ExecuteReader())
				{
					while (rd.Read())
					{
						teamDetails.Add(
							new TeamDetailsModel(
								rd.GetString(0),
								rd.GetInt32(1),
								rd.GetString(2),
								rd.GetString(3),
								rd.GetDateTime(4)));
					}
				}
			}

			return teamDetails;
		}
	}
}
