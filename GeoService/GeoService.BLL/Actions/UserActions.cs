using GeoService.BLL.DTO;
using GeoService.BLL.Utils;
using GeoService.DAL;
using GeoService.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoService.BLL.Actions
{
    public static class UserActions
    {
        public static void TryRegisterUser(GeoContext ctx, string login, string password)
        {
            if (ctx.Users.Any(x => x.Login == login))
                throw new ApiException("Пользователь с таким логином уже существует", nameof(TryRegisterUser), 400);

            var hasher = new PasswordHash();

            ctx.Users.Add(new User
            {
                Login = login,
                Salt = hasher.Salt,
                PasswordHash = hasher.Hash(password),
                Role = RoleEnum.NonDefined,
                RegistrationDate = DateTime.Now
            });
            ctx.SaveChanges();
        }

        public static UserDTO AuthenticationUser(GeoContext ctx, string login, string password)
        {
            if (ctx.Users.FirstOrDefault(x => x.Login == login) is User userDb)
            {
                var hasher = new PasswordHash(userDb.Salt);
                if (!hasher.Verify(userDb.PasswordHash, password))
                    throw new ApiException("Введен неверный пароль", nameof(AuthenticationUser), 401);
                else
                    return new UserDTO 
                    {
                        Id = userDb.Id,
                        Login = userDb.Login,
                        FullName = userDb.Role != RoleEnum.NonDefined ? $"{userDb.Surname} {userDb.Name}" : string.Empty,
                        Role = userDb.Role,
                        TeamTitle = userDb.TeamId.HasValue ? userDb.Team.Title : string.Empty,
                        TeamColor = userDb.TeamId.HasValue ? userDb.Team.Color : string.Empty
                    };
            }
            else
                throw new ApiException("Пользователь с таким логином не найден", nameof(AuthenticationUser), 401);
        }

        //public static void UpdateProfile(GeoContext ctx, UserDTO model)
        //{
        //    //if (ctx.Users.Any(x => x.Login == model.Login))
        //    //    throw new ApiException("Пользователь с таким логином уже существует");

        //}

        public static User GetUserById(GeoContext ctx, int id)
        {
            if (ctx.Users.Find(id) is User us)
                return us;
            else
                throw new ApiException("Фатальная ошибка, текущий пользователь не обнаружен", nameof(AuthenticationUser), 404);
        }
    }
}
