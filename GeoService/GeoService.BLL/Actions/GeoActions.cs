using GeoService.BLL.DTO;
using GeoService.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.BLL.Actions
{
    public static class GeoActions
    {
        public static ExperimentDTO InitExperiment(this GeoContext ctx, int userId, string title)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Team is null)
                    throw new ApiException("Чтобы начать эксперимент, необходимо быть участником команды", nameof(InitExperiment), 400);

                var now = DateTime.Now;
                ctx.Experiments.Add(new Experiment
                {
                    Title = title,
                    CreateDate = now,
                    CreatorUserId = userId,
                    Team = dbUser.Team
                });

                ctx.SaveChanges();

                return new ExperimentDTO { Title = title, CreateDate = now };
            }
            else
                throw new ApiException("Фатальная ошибка, текущий пользователь не обнаружен", nameof(InitExperiment), 404);
        }


    }
}
