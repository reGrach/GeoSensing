using GeoService.API.Models;
using GeoService.DAL;
using GeoService.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.BusinessLogic
{
    public static class UserActions
    {
        public static void TryRegisterUser(GeoContext ctx, RegistrationModel model)
        {
            if (ctx.Users.Any(x => x.Login == model.Login))
                throw new BusinessLogicException("Пользователь с таким логином уже существует");

            ctx.Users.Add(new User
            {
                Login = model.Login,
                Name = model.Name,
                Surname = model.Surname,
                TeamId = model.TeamId,
                Role = RoleEnum.Participant,
                RegistrationDate = DateTime.Now
            });

            ctx.SaveChanges();
        }
    }
}
