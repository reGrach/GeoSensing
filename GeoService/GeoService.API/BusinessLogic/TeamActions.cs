using GeoService.API.Models;
using GeoService.DAL;
using GeoService.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.BusinessLogic
{
    public static class TeamActions
    {

        public static void CreateTeam(GeoContext ctx, CreateTeamModel model, int creatorId)
        {

            if (ctx.Teams.Any(x => x.Title == model.Title))
                throw new BusinessLogicException("Команда с таким названием уже существует");

            if (ctx.Teams.Any(x => x.Color == model.Color))
                throw new BusinessLogicException("Команда с таким цветом уже существует");

            // Создаем команду
            ctx.Teams.Add(new Team
            {
                Title = model.Title,
                Color = model.Color,
            });
            ctx.SaveChanges();

            // Делаем текущего пользователя лидером команды
            var idTeam = ctx.Teams.First(x => x.Title == model.Title).Id;
            var dbUser = UserActions.GetUserById(ctx, creatorId);
            dbUser.Role = RoleEnum.Leader;
            dbUser.TeamId = idTeam;
            ctx.SaveChanges();
        }


        public static Team GetTeamById(GeoContext ctx, int id)
        {
            if (ctx.Teams.Find(id) is Team tm)
                return tm;
            else
                throw new BusinessLogicException("Фатальная ошибка, данная команда не обнаружена");
        }


        public static List<Team> GetAllTeam(GeoContext ctx) => ctx.Teams.ToList();


    }
}
