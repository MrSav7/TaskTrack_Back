using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KyrsachAPI.Models;

public partial class TaskStep
{
    [Key]
    public int TaskStepId { get; set; }

    public int TaskId { get; set; }

    public string TaskStepText { get; set; } = null!;

    public int? TaskStepUserId { get; set; }

    public DateTime TaskStepCreateDate { get; set; }
}
