namespace GeoService.API.Models
{
    /// <summary> Модель аутентификации/регистрации пользователя </summary>
    public class AuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
