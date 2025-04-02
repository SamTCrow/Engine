// Licensed to the .NET Foundation under one or more agreements.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models;

public abstract class BaseAction(GameItem itemInUse)
{
    protected readonly GameItem _itemInUse = itemInUse;
    public event EventHandler<string>? OnActionPerformed;

    protected void ReportResult(string result)
    {
        OnActionPerformed?.Invoke(this, result);
    }
}
