using System;

namespace GeoService.API.Models
{
    /// <summary> Модель аутентификации/регистрации пользователя </summary>
    public class AuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        /// <summary> Если true - срок действия токена - 1 месяц, иначе - 2 часа </summary>
        public bool RememberMe { get; set; }
    }

    /// <summary> Информация об авторизованном пользователе и сроке действия токена </summary>
    public class AuthInfoModel
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string AvatarSrc { get; set; }

        /// <summary> Срок действия токена авторизации </summary>
        public DateTime Expiration { get; set; }
    }
}
