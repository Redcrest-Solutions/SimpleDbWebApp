namespace SimpleDbWebApp.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(80)]
        public string FirstName { get; set; }
        [MaxLength(80)]
        public string LastName { get; set; }
    }
}