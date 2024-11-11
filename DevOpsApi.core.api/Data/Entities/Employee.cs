using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DevOpsApi.core.api.Data.Entities
{
    [Table("tblEmployee")]
    public class Employee
    {
        //this is the PK
        [Key]
        public long EmployeeID { get; set; }

        public long? HomeCompanyID { get; set; }

        public long? HomeCompanyDepartmentID { get; set; }
    }
}
