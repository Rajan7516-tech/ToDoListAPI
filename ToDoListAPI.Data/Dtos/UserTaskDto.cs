using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListAPI.Data.Dtos
{
    public class UserTaskDto 
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string TaskDescription { get; set; }
        public string TaskName { get; set; }
        public string TaskStatus { get; set; }
    }
}
