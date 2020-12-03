using System;

namespace Deadlines.API.Entities
{
    public class Deadline
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DbUserId { get; set; }
        public virtual DbUser DbUser { get; set; }
    }
}