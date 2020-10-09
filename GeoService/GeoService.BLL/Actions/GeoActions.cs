using GeoService.BLL.DTO;
using GeoService.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var newExp = ctx.Experiments.Add(new Experiment
                {
                    Title = title,
                    CreateDate = now,
                    CreatorUserId = userId,
                    IsActive = true,
                    Team = dbUser.Team
                });

                ctx.SaveChanges();

                return new ExperimentDTO
                {
                    Id = newExp.Entity.Id,
                    Title = title,
                    CreateDate = now,
                    IsMyInit = true
                };
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(InitExperiment), 404);
        }

        public static void CloseExperiment(this GeoContext ctx, int userId, int experimentId)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Team is null)
                    throw new ApiException("Чтобы закрыть эксперимент, необходимо быть участником команды", nameof(CloseExperiment), 400);

                if (dbUser.Team.Experiments.SingleOrDefault(x => x.Id == experimentId) is Experiment dbExp)
                {
                    dbExp.IsActive = false;
                    ctx.SaveChanges();
                }
                else
                    throw new ApiException("Такой эксперимент не обнаружен", nameof(CloseExperiment), 404);
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(CloseExperiment), 404);
        }

        public static List<ExperimentDTO> GetExperimentsByUserId(this GeoContext ctx, int userId)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Team is null)
                    throw new ApiException("Чтобы получить эксперименты, необходимо быть участником команды", nameof(GetExperimentsByUserId), 400);

                return dbUser.Team.Experiments.Where(x => x.IsActive)
                    .Select(x =>
                    new ExperimentDTO
                    {
                        Id = x.Id,
                        Title = x.Title,
                        CreateDate = x.CreateDate,
                        IsMyInit = x.CreatorUserId == userId
                    }).ToList();
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(GetExperimentsByUserId), 404);
        }

        public static void AddPoint(this GeoContext ctx, SavePointDTO model, int userId)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                if (dbUser.Team is Team dbTeam && dbTeam.Experiments.SingleOrDefault(x => x.Id == model.ExperimentId) is Experiment exp && exp.IsActive)
                {
                    ctx.GeoParameters.Add(new GeoParameter
                    {
                        Accuracy = model.Accuracy,
                        Altitude = model.Altitude,
                        AltitudeAccuracy = model.AltitudeAccuracy,
                        Speed = model.Speed,
                        CreateTime = model.CreateTime,
                        ExperimentId = model.ExperimentId,
                        Heading = model.Heading,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        Mode = model.Mode,
                        CreatorUserId = dbUser.Id,
                    });
                    ctx.SaveChanges();
                }
                else
                    throw new ApiException("Нельзя добавить точку. Пользователь должен состоять в команде, с выбранным экспериментом", nameof(AddPoint), 400);
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(AddPoint), 404);
        }

        public static ResponsePointsDTO GetPoints(this GeoContext ctx, int userId)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                var points = ctx.Experiments
                    .Include(x => x.Coordinates)
                    .Where(x => x.TeamId == dbUser.TeamId)
                    .Select(x => new ListPointsDTO
                    {
                        ExperimentId = x.Id,
                        ExperimentTitle = x.Title,
                        Points = x.Coordinates
                            .Select(x => new UserPointDTO
                            {
                                Accuracy = x.Accuracy,
                                Altitude = x.Altitude,
                                AltitudeAccuracy = x.AltitudeAccuracy,
                                Speed = x.Speed,
                                CreateTime = x.CreateTime,
                                Heading = x.Heading,
                                Latitude = x.Latitude,
                                Longitude = x.Longitude,
                                Mode = x.Mode,
                                User = ctx.Users.FirstOrDefault(y => y.Id == x.CreatorUserId).ToDTO()
                            }).ToList()
                    }).ToList();


                var logins = points.SelectMany(x => x.Points.Select(y => y.User.Login)).Distinct().ToArray();

                var userImg = ctx.Users.Include(x => x.Avatar)
                    .Where(x => logins.Contains(x.Login))
                    .ToDictionary(k => k.Login, v => v.GetAvatarSrc());

                return new ResponsePointsDTO
                {
                    ListPoint = points,
                    UserImages = userImg
                };
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(GetPoints), 404);
        }
    }
}
