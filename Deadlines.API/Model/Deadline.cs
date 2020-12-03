using System;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Deadlines.API.Model
{
    public class Deadline
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}