using System;
using System.Collections.Generic;

namespace EmailProviderServer.Models;

/// <summary>
/// Table for incoming message to be processed
/// </summary>
public partial class BulkIncomingMessage
{
    /// <summary>
    /// ID for incoming message to be processed
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Raw data
    /// </summary>
    public string RawData { get; set; } = null!;
}
