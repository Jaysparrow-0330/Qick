using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class TestType
    {
        public TestType()
        {
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string? QuizTypeName { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
