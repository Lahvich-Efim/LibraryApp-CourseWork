using System;
using System.Collections.Generic;

namespace LibraryApp.Model;

public partial class Notify
{
    public int IdNotify { get; set; }

    public string Message { get; set; } = null!;

    public string Header { get; set; } = null!;

    public DateTime DataNotify { get; set; }

    public int IdUser { get; set; }

    public bool IsRead { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
