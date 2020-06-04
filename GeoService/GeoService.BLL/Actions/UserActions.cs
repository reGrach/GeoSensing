using GeoService.BLL.DTO;
using GeoService.BLL.Utils;
using GeoService.DAL;
using GeoService.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoService.BLL.Actions
{
    public static class UserActions
    {
        public static void TryRegisterUser(this GeoContext ctx, string login, string password)
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

        public static IdentityDTO AuthenticationUser(this GeoContext ctx, string login, string password)
        {
            if (ctx.Users.FirstOrDefault(x => x.Login == login) is User userDb)
            {
                var hasher = new PasswordHash(userDb.Salt);
                if (!hasher.Verify(userDb.PasswordHash, password))
                    throw new ApiException("Введен неверный пароль", nameof(AuthenticationUser), 401);
                else
                    return new IdentityDTO
                    {
                        Id = userDb.Id,
                        Login = userDb.Login,
                        Role = userDb.Role,
                    };
            }
            else
                throw new ApiException("Пользователь с таким логином не найден", nameof(AuthenticationUser), 401);
        }

        public static string GetLoginById(this GeoContext ctx, int id) =>
            ctx.Users.Find(id) is User userDb
            ? userDb.Login
            : throw new ApiException("Пользователь не найден", nameof(AuthenticationUser), 404);

        public static void UpdateProfile(this GeoContext ctx, int id, UserDTO model)
        {
            if (ctx.Users.Find(id) is User dbUser)
            {
                dbUser.Name = model.Name;
                dbUser.Surname = model.SurName;

                if (dbUser.Role == RoleEnum.NonDefined)
                    dbUser.Role = RoleEnum.Participant;
            }
            else
                throw new ApiException("Фатальная ошибка, текущий пользователь не обнаружен", nameof(AuthenticationUser), 404);
        }

        public static UserDTO GetProfile(this GeoContext ctx, int id)
        {
            if (ctx.Users.Find(id) is User dbUser)
                return new UserDTO
                {
                    Login = dbUser.Login,
                    Name = dbUser.Name,
                    SurName = dbUser.Surname,
                    IsLeader = dbUser.Role == RoleEnum.Admin,
                    Team = dbUser.Team is Team team ? team.ToDTO() : null
                };
            else
                throw new ApiException("Фатальная ошибка, текущий пользователь не обнаружен", nameof(AuthenticationUser), 404);
        }

        public static IEnumerable<UserDTO> GetFreeUsers(this GeoContext ctx, string query)
        {
            var freeUsers = ctx.Users.AsNoTracking()
                .Where(x => x.Role == RoleEnum.NonDefined)
                .AsEnumerable();
            if (string.IsNullOrEmpty(query))
                freeUsers = freeUsers
                    .Where(x => x.Login.Equals(query, StringComparison.InvariantCultureIgnoreCase)
                    || x.Name.Equals(query, StringComparison.InvariantCultureIgnoreCase)
                    || x.Surname.Equals(query, StringComparison.InvariantCultureIgnoreCase));

            return freeUsers.Select(x => new UserDTO
            {
                Login = x.Login,
                Name = x.Name,
                SurName = x.Surname
            });
        }
    }
}
