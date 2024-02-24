using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyrsachAPI.Models;

[Table("Task")]
public partial class Tasks
{
    [Key]
    public int TaskId { get; set; }

    public int TaskItemBrandId { get; set; }

    public int TaskProductTypeId { get; set; }

    public string TaskProductModel { get; set; } = null!;

    public DateTime? TaskCreateDate { get; set; }

    public int TaskStatusId { get; set; }

    public DateTime? TaskPlanExeTime { get; set; }

    public DateTime? TaskCloseDate { get; set; }

    public string TaskUserProblemDesc { get; set; } = null!;

    public string? TaskProblems { get; set; }
}
