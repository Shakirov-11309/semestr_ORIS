namespace MyHttttpServer.Models
{

    /// <summary>
    /// Класс представялет пользователя с учетными данными
    /// </summary>
    public class User
    {
        /// <summary>
        /// Свойство задает и получает логин поль-ля
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Свойство задает и получает пароль поль-ля
        /// </summary>
        public string password { get; set; }

        public int id { get; set; }

        public bool is_admin { get; set; }
    }
}
