using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Services.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string HashPassword { get; set; }

    public User()
    {
        
    }

    public User(string userName, string hashPassword, int? userId)
    {
        if (userId != null)
            UserId = (int)userId;

        UserName = userName;
        HashPassword = hashPassword;
    }
}
