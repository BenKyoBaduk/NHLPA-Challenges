using HockeyApi.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HockeyApi.Features
{
	public interface ICreatePlayerService
	{
		void Create(CreatePlayerModel player);
	}

	public class CreatePlayerService : ICreatePlayerService
	{
		private readonly IDb _db;

		public CreatePlayerService(IDb db)
		{
			_db = db;
		}

		public void Create(CreatePlayerModel player)
		{
			int playerId;

			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@lastName", player.LastName);
				cmd.Parameters.AddWithValue("@firstName", player.FirstName);
				cmd.CommandText = string.Format($@"
						INSERT INTO player (
								    last_name
								  , first_name)
						VALUES    ( @lastName
								  , @firstName)");
				cmd.CommandText += $@"SELECT SCOPE_IDENTITY()";
				playerId = Convert.ToInt32(cmd.ExecuteScalar());
			}
			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@player_id", playerId);
				cmd.Parameters.AddWithValue("@team_code", player.TeamCode);
				cmd.Parameters.AddWithValue("@roster_transaction_type_id", 1);
				cmd.Parameters.AddWithValue("@effective_date", player.EffectiveDate);
				cmd.CommandText = string.Format($@"
						INSERT INTO roster_transaction (
									player_id,
									team_code,
									roster_transaction_type_id,
									effective_date)
						VALUES    ( @player_id, @team_code, @roster_transaction_type_id, @effective_date)");

				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.ExecuteScalar();
			}
		}
	}
}
