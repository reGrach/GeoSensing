using GeoService.API.BusinessLogic.Utils;
using GeoService.BLL.DTO;
using GeoService.DAL;
using GeoService.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoService.BLL.Actions
{
    public static class TeamActions
    {
        public static void CreateTeam(this GeoContext ctx, int userId, string title, string color)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.TeamId.HasValue)
                    throw new ApiException("Чтобы создать новую команду, необходимо покинуть текущую", nameof(CreateTeam), 400);

                if (ctx.Teams.Any(x => x.Title == title))
                    throw new ApiException("Команда с такми названием существует", nameof(CreateTeam), 400);

                dbUser.Role = RoleEnum.Leader;

                ctx.Teams.Add(new Team
                {
                    Title = title,
                    Color = color,
                    CreateDate = DateTime.UtcNow,
                    IsActive = true,
                    Users = new List<User> { dbUser }
                });

                ctx.SaveChanges();
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(CreateTeam), 404);
        }

        public static TeamDTO UpdateTeamByLeader(this GeoContext ctx, int userId, string title, string color)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Role != RoleEnum.Leader)
                    throw new ApiException("Только лидер может изменять данные команды", nameof(UpdateTeamByLeader), 400);

                if (ctx.Teams.Any(x => x.Title == title))
                    throw new ApiException("Команда с такми названием существует", nameof(UpdateTeamByLeader), 400);

                dbUser.Team.Title = title;
                dbUser.Team.Color = color;
                ctx.SaveChanges();
                return dbUser.Team.ToDTO();
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(UpdateTeamByLeader), 404);
        }

        public static TeamExtensionDTO GetTeamById(this GeoContext ctx, int id)
        {
            if (ctx.Teams.Find(id) is Team dbTeam)
                return dbTeam.ToExtensionDTO();
            else
                throw new ApiException(ConstMsg.TeamNotFound, nameof(GetTeamById), 404);
        }

        public static TeamExtensionDTO GetTeamByUser(this GeoContext ctx, int id)
        {
            if (ctx.Users.Find(id) is User dbUser)
            {
                if (dbUser.Team is Team dbTeam)
                    return dbUser.Team.ToExtensionDTO();
                else
                    throw new ApiException("Вы еще не вступили ни в одну из команд", nameof(GetTeamByUser), 400);
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(GetTeamByUser), 404);
        }

        public static void AddUserToTeam(this GeoContext ctx, int userId, int teamId)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Role != RoleEnum.NonDefined)
                    throw new ApiException("Нельзя добавить участника другой команды", nameof(AddUserToTeam), 400);
                else
                {
                    if (ctx.Teams.Find(teamId) is Team dbTeam)
                    {
                        dbUser.TeamId = teamId;
                        dbUser.Role = RoleEnum.Participant;
                    }
                    else
                        throw new ApiException(ConstMsg.TeamNotFound, nameof(AddUserToTeam), 404);
                }
                ctx.SaveChanges();
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(AddUserToTeam), 404);
        }

        public static void RemoveUserFromTeam(this GeoContext ctx, int userId)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (!dbUser.TeamId.HasValue)
                    throw new ApiException("Пользователь не состоит в команде", nameof(RemoveUserFromTeam), 400);

                dbUser.TeamId = null;
                dbUser.Role = RoleEnum.NonDefined;
                ctx.SaveChanges();
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(RemoveUserFromTeam), 404);
        }

        public static IEnumerable<TeamExtensionDTO> GetAllTeams(this GeoContext ctx) =>
            ctx.Teams.AsNoTracking().Select(x => x.ToExtensionDTO());

        public static IEnumerable<TeamDTO> GetActiveTeams(this GeoContext ctx) =>
            ctx.Teams.AsNoTracking().Where(x => x.IsActive).Select(x => x.ToDTO());

        public static void ChangeActiveTeam(this GeoContext ctx, int teamId)
        {
            if (ctx.Teams.Find(teamId) is Team dbTeam)
            {
                dbTeam.IsActive = !dbTeam.IsActive;
                foreach(var user in dbTeam.Users)
                {
                    user.TeamId = null;
                    user.Role = RoleEnum.NonDefined;
                }
                dbTeam.Users = null;
                ctx.SaveChanges();                    
            }                
            else
                throw new ApiException(ConstMsg.TeamNotFound, nameof(ChangeActiveTeam), 404);
        }


        internal static TeamDTO ToDTO(this Team team) =>
            new TeamDTO
            {
                Id = team.Id,
                Title = team.Title,
                Color = team.Color
            };

        internal static TeamExtensionDTO ToExtensionDTO(this Team team) =>
            new TeamExtensionDTO
            {
                Id = team.Id,
                CreateDate = team.CreateDate,
                Title = team.Title,
                Color = team.Color, 
                IsActive = team.IsActive,
                Participants = team.Users.Select(x => x.ToDTO()).OrderBy(x => x.IsLeader).ThenBy(x => x.Login).ToList(),
                Polygon = team.Polygon.HasValue ? team.Polygon.Value.ToPolygon() : null
            };
    }
}
