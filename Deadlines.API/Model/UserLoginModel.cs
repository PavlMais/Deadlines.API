using System.ComponentModel.DataAnnotations;


namespace Deadlines.API.Model
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string Email { get; set; }

     
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,24}$", 
            ErrorMessage = "Пароль повинен мати мінімум 6 символів, нижній і верхній регістр, та цифри!")]
        public string Password { get; set; }
    }
}