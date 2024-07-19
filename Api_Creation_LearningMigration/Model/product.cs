using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api_Creation_LearningMigration.Model
{
    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Warranty { get; set; }
        public string Cost { get; set; }
        public string ProductDispatchAddress { get; set; }
    }
}
