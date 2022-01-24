/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

namespace Fragcolor.Chainblocks.UnityEditor.Settings
{
  internal static class ChainblocksManager
  {
    /// <summary>
    /// Initializes this manager.
    /// </summary>
    /// <remarks>
    /// This method is meant to be called directly by <see cref="GlobalInitialization"/>.
    /// </remarks>
    internal static void InitializeGlobalState()
    {
      ChainblocksSettings.OnModificationGlobal += OnSettingsChanged;
    }

    /// <summary>
    /// Uninitializes this manager.
    /// </summary>
    /// <remarks>
    /// This method is meant to be called directly by <see cref="GlobalInitialization"/>.
    /// </remarks>
    internal static void ShutdownGlobalState()
    {
      ChainblocksSettings.OnModificationGlobal -= OnSettingsChanged;
    }

    /// <summary>
    /// Called when the state of any settings instance has changed.
    /// </summary>
    /// <param name="settings">The settings instance.</param>
    /// <param name="modificationEvent">The type of event that triggered the change.</param>
    /// <param name="eventData">Data for that particular event.</param>
    private static void OnSettingsChanged(ChainblocksSettings settings, ChainblocksSettings.ModificationEvent modificationEvent, object? eventData)
    {
      switch (modificationEvent)
      {
        case ChainblocksSettings.ModificationEvent.RegistryAdded:
          // nothing for now
          break;

        case ChainblocksSettings.ModificationEvent.RegistryRemoved:
          // nothing for now
          break;

        case ChainblocksSettings.ModificationEvent.EntryCreated:
          // nothing for now
          break;

        case ChainblocksSettings.ModificationEvent.EntryRemoved:
          // nothing for now
          break;
      }
    }
  }
}
