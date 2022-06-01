/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

namespace Fragcolor.Shards.UnityEditor.Settings
{
  internal static class ShardsManager
  {
    /// <summary>
    /// Initializes this manager.
    /// </summary>
    /// <remarks>
    /// This method is meant to be called directly by <see cref="GlobalInitialization"/>.
    /// </remarks>
    internal static void InitializeGlobalState()
    {
      ShardsSettings.OnModificationGlobal += OnSettingsChanged;
    }

    /// <summary>
    /// Uninitializes this manager.
    /// </summary>
    /// <remarks>
    /// This method is meant to be called directly by <see cref="GlobalInitialization"/>.
    /// </remarks>
    internal static void ShutdownGlobalState()
    {
      ShardsSettings.OnModificationGlobal -= OnSettingsChanged;
    }

    /// <summary>
    /// Called when the state of any settings instance has changed.
    /// </summary>
    /// <param name="settings">The settings instance.</param>
    /// <param name="modificationEvent">The type of event that triggered the change.</param>
    /// <param name="eventData">Data for that particular event.</param>
    private static void OnSettingsChanged(ShardsSettings settings, ShardsSettings.ModificationEvent modificationEvent, object? eventData)
    {
      switch (modificationEvent)
      {
        case ShardsSettings.ModificationEvent.RegistryAdded:
          // nothing for now
          break;

        case ShardsSettings.ModificationEvent.RegistryRemoved:
          // nothing for now
          break;

        case ShardsSettings.ModificationEvent.EntryCreated:
          // nothing for now
          break;

        case ShardsSettings.ModificationEvent.EntryRemoved:
          // nothing for now
          break;
      }
    }
  }
}
