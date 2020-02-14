using GeoService.API.BusinessLogic.Utils;
using GeoService.API.Models;
using GeoService.DAL;
using GeoService.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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

            var hasher = new PasswordHash();
            ctx.Users.Add(new User
            {
                Login = model.Login,
                Salt = hasher.Salt,
                PasswordHash = hasher.Hash(model.Password),
                Name = model.Name,
                Surname = model.Surname,
                TeamId = model.TeamId,
                Role = RoleEnum.Participant,
                RegistrationDate = DateTime.Now
            });
            ctx.SaveChanges();
        }

        public static User AuthenticationUser(GeoContext ctx, LoginModel model)
        {
            if (ctx.Users.FirstOrDefault(x => x.Login == model.Login) is User userDb)
            {
                var hasher = new PasswordHash(userDb.Salt);
                if (!hasher.Verify(userDb.PasswordHash, model.Password))
                    throw new BusinessLogicException("Введен неверный пароль");
                else
                    return userDb;
            }
            else
                throw new BusinessLogicException("Пользователь с таким логином не найден");
        }
    }
}
