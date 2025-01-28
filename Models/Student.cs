using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorTool.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public Char Gender { get; set; }
        public int Age { get; set; }
        public string Email {  get; set; }
    }
}
