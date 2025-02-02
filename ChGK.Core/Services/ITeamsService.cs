﻿using System.Collections.Generic;
using ChGK.Core.Models;

namespace ChGK.Core.Services
{
	public interface ITeamsService
	{
		List<Team> GetAllTeams ();

		List<int> GetAllResults (string questionId);

		void AddTeam (string name);

		void RemoveTeam (int teamID);
        
		void CleanResults ();

		void RemoveAllTeams ();

		void IncrementScore (string questionId, int teamId);

		void DecrementScore (string questionId, int teamId);

		int GetTeamScore (int teamID);
	}
}

