﻿using System.ComponentModel.DataAnnotations;

namespace Application.AppSettings;

public sealed class ConnectionOptions : BaseOptions
{
    public const string ConfigSectionPath = "ConnectionStrings";

    [Required]
    public string SqlConnection { get; private init; }

    [Required]
    public string NoSqlConnection { get; private init; }

    [Required]
    public string CacheConnection { get; private init; }
}
