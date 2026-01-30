using Microsoft.EntityFrameworkCore;
using ProLab.Areas.HockeyData.Data;
using ProLab.Areas.HockeyData.ViewModels.Public;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProLab.Areas.HockeyData.Services
{
    public class HockeyDataStatsService
    {
        private readonly HockeyDataContext _context;

        public HockeyDataStatsService(HockeyDataContext context)
        {
            _context = context;
        }

        public async Task<List<PublicGameRowVM>> GetPublicGamesAsync()
        {
            return await _context.HockeyGames
                .Include(g => g.HomeTeam)
                .Include(g => g.AwayTeam)
                .Include(g => g.HockeyArena)
                .Include(g => g.GameStatus)
                .OrderBy(g => g.GameDateTime)
                .Select(g => new PublicGameRowVM
                {
                    GameTime = g.GameDateTime,

                    HomeTeam = g.HomeTeam!.HockeyTeamName,
                    HomeTeamBadgePath = g.HomeTeam.HockeyTeamBadgePath,

                    AwayTeam = g.AwayTeam!.HockeyTeamName,
                    AwayTeamBadgePath = g.AwayTeam.HockeyTeamBadgePath,

                    HomeScore = g.HomeTeamScore,
                    AwayScore = g.AwayTeamScore,

                    Arena = g.HockeyArena!.HockeyArenaName,
                    Status = g.GameStatus!.HockeyGameStatusName
                })
                .ToListAsync();
        }

        public async Task<List<PublicStandingsRowVM>> GetStandingsAsync()
        {
            var games = await _context.HockeyGames
                .Where(g => g.HomeTeamScore != null && g.AwayTeamScore != null)
                .ToListAsync();

            var teams = await _context.HockeyTeams.ToListAsync();

            var standings = teams.Select(t =>
            {
                var playedGames = games
                    .Where(g => g.HockeyTeamId == t.Id || g.HockeyTeamId1 == t.Id)
                    .ToList();

                var goalsFor =
                    playedGames.Sum(g =>
                        g.HockeyTeamId == t.Id ? g.HomeTeamScore!.Value :
                        g.HockeyTeamId1 == t.Id ? g.AwayTeamScore!.Value : 0);

                var goalsAgainst =
                    playedGames.Sum(g =>
                        g.HockeyTeamId == t.Id ? g.AwayTeamScore!.Value :
                        g.HockeyTeamId1 == t.Id ? g.HomeTeamScore!.Value : 0);

                var points =
                    playedGames.Sum(g =>
                       // Vinst
                       g.HockeyTeamId == t.Id && g.HomeTeamScore > g.AwayTeamScore ? 2 :
                       g.HockeyTeamId1 == t.Id && g.AwayTeamScore > g.HomeTeamScore ? 2 :

                       // Oavgjort
                       g.HomeTeamScore == g.AwayTeamScore ? 1 :

                       // Förlust
                       0);

                return new PublicStandingsRowVM
                {
                    TeamId = t.Id,
                    TeamName = t.HockeyTeamName,
                    BadgePath = t.HockeyTeamBadgePath,

                    GamesPlayed = playedGames.Count,
                    GoalsFor = goalsFor,
                    GoalsAgainst = goalsAgainst,
                    Points = points
                };
            })
            .OrderByDescending(x => x.Points)
            .ThenByDescending(x => x.GoalDiff)
            .ToList();

            return standings;
        }

    }
}