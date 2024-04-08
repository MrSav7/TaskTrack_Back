using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KyrsachAPI.Models;

public partial class TaskExecutor
{
    [Key]
    public int TaskExecutorId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }
}
