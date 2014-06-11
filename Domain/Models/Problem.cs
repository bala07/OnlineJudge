using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Problem
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Location { get; set; }

        public virtual string Difficulty { get; set; }

        [NotMapped]
        public virtual string Statement { get; set; }
    }
}
