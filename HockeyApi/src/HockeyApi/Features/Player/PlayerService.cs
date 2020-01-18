using System.Collections.Generic;
using HockeyApi.Common;
using System.Data.SqlClient;
using System.Data;

namespace HockeyApi.Features
{
	public interface IPlayerService
	{
		IEnumerable<PlayerDetailsModel> Get(int playerId);
		IEnumerable<PlayerModel> Search(string q);
		string GetCurrentTeamCode(int playerId);
	}

	public class PlayerService : IPlayerService
	{
		private readonly IDb _db;

		public PlayerService(IDb db)
		{
			_db = db;
		}

		public IEnumerable<PlayerDetailsModel> Get(int playerId)
		{
			var players = new HashSet<PlayerDetailsModel>();

			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@playerId", playerId);
				cmd.CommandText = $@"
                    SELECT TOP	(10)
							    p.player_id
                              , p.last_name
							  , p.first_name
							  , r.team_code
							  , r.roster_transaction_type_id
							  , r.effective_date
                    FROM        player p
					INNER JOIN  roster_transaction r
					ON			r.player_id = p.player_id
					WHERE		p.player_id = @playerId
					ORDER BY	r.effective_date DESC";

				using (var rd = cmd.ExecuteReader())
				{
					while (rd.Read())
					{
						players.Add(
							new PlayerDetailsModel(
								rd.GetInt32(0),
								rd.GetString(1),
								rd.GetString(2),
								rd.GetString(3),
								rd.GetInt32(4),
								rd.GetDateTime(5)));
					}
					
					return players;
				}
			}
		}

		public IEnumerable<PlayerModel> Search(string q)
		{
			var players = new HashSet<PlayerModel>();

			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@q", q);
				cmd.CommandText = @"
                    SELECT TOP  (10) 
								player_id
                              , last_name
							  , first_name
                    FROM        player
					WHERE		last_name = @q
					OR			first_name = @q";

				using (var rd = cmd.ExecuteReader())
				{
					while (rd.Read())
					{
						players.Add(
							new PlayerModel(
								rd.GetInt32(0),
								rd.GetString(1),
								rd.GetString(2)));
					}
				}
			}

			return players;
		}

		public string GetCurrentTeamCode(int playerId)
		{
			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@playerId", playerId);
				cmd.CommandText = $@"
                    SELECT TOP  (1)
								team_code
                    FROM        roster_transaction
					WHERE		player_id = @playerId
					ORDER BY	effective_date DESC";

				using (var rd = cmd.ExecuteReader())
				{
					if (rd.Read())
					{
						var s = rd.GetString(0);
						return s;
					}
					else
					{
						return null;
					}
				}
			}
		}
	}
}
