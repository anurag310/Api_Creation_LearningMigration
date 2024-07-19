using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api_Creation_LearningMigration.Model
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int EmpID { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string City { get; set; }
        public int DeptID { get; set; }

        public Department Department { get; set; }
    }
}
