using System.Collections.Generic;
using HockeyApi.Common;
using System.Data.SqlClient;
using System;
using System.Data;

namespace HockeyApi.Features
{
	public interface IUpdatePlayerService
	{
		void Trade(UpdatePlayerModel player);
		void Healthy(UpdatePlayerModel player);
		void Injured(UpdatePlayerModel player);
	}

	public class UpdatePlayerService : IUpdatePlayerService
	{
		private readonly IDb _db;

		public UpdatePlayerService(IDb db)
		{
			_db = db;
		}

		public void Trade(UpdatePlayerModel player)
		{
			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@player_id", player.PlayerId);
				cmd.Parameters.AddWithValue("@team_code", player.TeamCode);
				cmd.Parameters.AddWithValue("@roster_transaction_type_id", 4);
				cmd.Parameters.AddWithValue("@effective_date", player.EffectiveDate);
				cmd.CommandText = string.Format($@"
						INSERT INTO roster_transaction (
									player_id,
									team_code,
									roster_transaction_type_id,
									effective_date)
						VALUES    ( @player_id, @team_code, @roster_transaction_type_id, @effective_date)");
				
				cmd.ExecuteScalar();
			}
		}

		public void Healthy(UpdatePlayerModel player)
		{
			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@player_id", player.PlayerId);
				cmd.Parameters.AddWithValue("@team_code", player.TeamCode);
				cmd.Parameters.AddWithValue("@roster_transaction_type_id", 3);
				cmd.Parameters.AddWithValue("@effective_date", player.EffectiveDate);
				cmd.CommandText = string.Format($@"
						INSERT INTO roster_transaction (
									player_id,
									team_code,
									roster_transaction_type_id,
									effective_date)
						VALUES    ( @player_id, @team_code, @roster_transaction_type_id, @effective_date)");

				cmd.ExecuteScalar();
			}
		}

		public void Injured(UpdatePlayerModel player)
		{
			using (var conn = (SqlConnection)_db.CreateConnection())
			using (var cmd = conn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandTimeout = 300;
				cmd.Parameters.AddWithValue("@player_id", player.PlayerId);
				cmd.Parameters.AddWithValue("@team_code", player.TeamCode);
				cmd.Parameters.AddWithValue("@roster_transaction_type_id", 2);
				cmd.Parameters.AddWithValue("@effective_date", player.EffectiveDate);
				cmd.CommandText = string.Format($@"
						INSERT INTO roster_transaction (
									player_id,
									team_code,
									roster_transaction_type_id,
									effective_date)
						VALUES    ( @player_id, @team_code, @roster_transaction_type_id, @effective_date)");

				cmd.ExecuteScalar();
			}
		}
	}
}
