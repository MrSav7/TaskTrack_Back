using System;
using System.Collections.Generic;

namespace KyrsachAPI.Models;

public partial class TaskExecutor
{
    public int TaskExecutorId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }
}
