//using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Venna.Models;


//public class RefreshToken
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public int id { get; set; }
//    public string Token { get; set; }
//    public DateTime ExpiresOn { get; set; }
//    public DateTime CreatedOn { get; set; }
//    public bool IsExpired => DateTime.Now > ExpiresOn;
//    public int Userid { get; set; }
//    public User User { get; set; }

//}
