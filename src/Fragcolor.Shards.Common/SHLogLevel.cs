/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

namespace Fragcolor.Shards
{
  /// <summary>
  /// Log levels in shards.
  /// </summary>
  /// <seealso cref="SHCoreExtensions.Log(ref SHCore,string,SHLogLevel)"/>
  public enum SHLogLevel
  {
    //! Must match log level in shards (currently from spdlog)
    Trace = 0,
    Debug = 1,
    Info = 2,
    Warn = 3,
    Error = 4,
    Fatal = 5,
  }
}
