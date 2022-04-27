using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Enums;

namespace TryBeingFit.Domain.Models
{
    public abstract class Training : BaseEntity
    {
        public string Title { get; set; }
        public double Time { get; set; }
        public string Description { get; set; }
        public TrainingDifficulty Difficulty { get; set; }
        public double Rating { get; set; }
    } 
}
