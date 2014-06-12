using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Problem
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string Location { get; set; }

        public virtual string Difficulty { get; set; }

        [NotMapped]
        public virtual ProblemStatement ProblemStatement { get; set; }
    }
}
