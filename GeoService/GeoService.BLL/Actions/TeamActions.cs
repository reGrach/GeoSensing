using GeoService.BLL.DTO;
using GeoService.DAL;
using GeoService.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoService.BLL.Actions
{
    public static class TeamActions
    {
        public static void CreateTeam(this GeoContext ctx, int userId, string title, string color)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Role == RoleEnum.Leader)
                    throw new ApiException("Чтобы создать новую команду, необходимо покинуть текущую", nameof(CreateTeam), 400);

                if (ctx.Teams.Any(x => x.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)))
                    throw new ApiException("Команда с такми названием существует", nameof(CreateTeam), 400);

                ctx.Teams.Add(new Team
                {
                    Title = title,
                    Color = color,
                    IsActive = true,
                    Users = new List<User> { dbUser }
                });

                ctx.SaveChanges();
            }
            else
                throw new ApiException("Фатальная ошибка, текущий пользователь не обнаружен", nameof(CreateTeam), 404);
        }

        public static void UpdateTeamByLeader(this GeoContext ctx, int userId, string title, string color)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Role != RoleEnum.Leader)
                    throw new ApiException("Только лидер команды может изменять данные", nameof(UpdateTeamByLeader), 400);

                if (ctx.Teams.Any(x => x.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)))
                    throw new ApiException("Команда с такми названием существует", nameof(CreateTeam), 400);

                dbUser.Team.Title = title;
                dbUser.Team.Color = color;
                ctx.SaveChanges();
            }
            else
                throw new ApiException("Фатальная ошибка, текущий пользователь не обнаружен", nameof(UpdateTeamByLeader), 404);
        }

        public static IEnumerable<TeamDTO> GetTeams(this GeoContext ctx, bool onlyIsActive = true) =>
            onlyIsActive
            ? ctx.GetAllTeams().Select(x => x.ToDTO())
            : ctx.GetActiveTeams().Select(x => x.ToDTO());

        public static TeamExtensionDTO GetTeamById(this GeoContext ctx, int id)
        {
            if (ctx.Teams.Find(id) is Team dbTeam)
            {
                return new TeamExtensionDTO
                {
                    Id = dbTeam.Id,
                    Title = dbTeam.Title,
                    Color = dbTeam.Color,
                    Participants = dbTeam.Users.Select(x => new UserDTO
                    {
                        Name = x.Name,
                        SurName = x.Surname
                    }).ToList()
                };
            }
            else
                throw new ApiException("Фатальная ошибка, команда не обнаружена", nameof(GetTeamById), 404);
        }

        public static TeamExtensionDTO GetTeamByUser(this GeoContext ctx, int id)
        {
            if (ctx.Users.Find(id) is User dbUser)
            {
                if (dbUser.Team is Team dbTeam)
                {
                    return new TeamExtensionDTO
                    {
                        Id = dbTeam.Id,
                        Title = dbTeam.Title,
                        Color = dbTeam.Color,
                        Participants = dbTeam.Users.Select(x => new UserDTO { Name = x.Name, SurName = x.Surname }).ToList(),
                        Polygon = null
                    };
                }
                else
                    throw new ApiException("Вы еще не вступили ни в одну из команд", nameof(GetTeamByUser), 400);
            }
            else
                throw new ApiException("Фатальная ошибка, пользователь не обнаружен", nameof(GetTeamByUser), 404);
        }


        public static void ChangeActiveTeam(this GeoContext ctx, int teamId)
        {
            if (ctx.Teams.Find(teamId) is Team dbTeam)
                dbTeam.IsActive = !dbTeam.IsActive;
            else
                throw new ApiException("Фатальная ошибка, команда не обнаружена", nameof(ChangeActiveTeam), 404);
        }

        public static void AddUserToTeam(this GeoContext ctx, string login, int teamId)
        {
            if (ctx.Users.FirstOrDefault(x => x.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase)) is User dbUser)
            {
                if (dbUser.Role == RoleEnum.Leader)
                    throw new ApiException("Нельзя добавить лидера другой команды", nameof(AddUserToTeam), 400);
                else
                {
                    if (dbUser.Role == RoleEnum.NonDefined)
                        dbUser.Role = RoleEnum.Participant;

                    if (ctx.Teams.Find(teamId) is Team dbTeam)
                        dbTeam.Users.Add(dbUser);
                    else
                        throw new ApiException("Фатальная ошибка, команда не обнаружена", nameof(AddUserToTeam), 400);
                }
                ctx.SaveChanges();
            }
            else
                throw new ApiException("Фатальная ошибка, пользователь не обнаружен", nameof(AddUserToTeam), 404);
        }

        public static void RemoveUserFromTeam(this GeoContext ctx, int userId, int teamId)
        {
            if (ctx.Teams.Find(teamId) is Team dbTeam)
            {
                if (dbTeam.Users.FirstOrDefault(x => x.Id == userId) is User dbUser && dbUser.Role != RoleEnum.Leader)
                    dbTeam.Users.Remove(dbUser);
                else
                    throw new ApiException("Нельзя удалить пользователя из команды", nameof(RemoveUserFromTeam), 400);

                ctx.SaveChanges();
            }
            else
                throw new ApiException("Фатальная ошибка, команда не обнаружена", nameof(RemoveUserFromTeam), 404);
        }


        private static IEnumerable<Team> GetAllTeams(this GeoContext ctx) =>
            ctx.Teams.AsNoTracking().AsEnumerable();

        private static IEnumerable<Team> GetActiveTeams(this GeoContext ctx) =>
            ctx.Teams.AsNoTracking().Where(x => x.IsActive).AsEnumerable();

        internal static TeamDTO ToDTO(this Team team) =>
            new TeamDTO
            {
                Id = team.Id,
                Title = team.Title,
                Color = team.Color
            };
    }
}
