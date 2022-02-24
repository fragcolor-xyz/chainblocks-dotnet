/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Claymore
{
  /// <summary>
  /// Native methods defined externally in the claymore library.
  /// </summary>
  /// <remarks>
  /// Names are case sensitive and must match the exported symbols.
  /// </remarks>
  internal static class NativeMethods
  {
    private const string Dll = "claymore";
    internal const CallingConvention Conv = CallingConvention.Cdecl;

    /// <summary>
    /// Starts a data request for the corresponding <paramref name="fragmentHash"/>.
    /// </summary>
    /// <param name="fragmentHash"></param>
    /// <returns>A struct that can be used to query progress on the request.</returns>
    [DllImport(Dll, CallingConvention = Conv)]
    internal static extern ClGetDataRequestPtr clmrGetDataStart([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeConst = 32)] byte[] fragmentHash);

    /// <summary>
    /// Frees a request previously created with <see cref="clmrGetDataStart(byte[])"/>.
    /// </summary>
    /// <param name="request">A reference to the request.</param>
    [DllImport(Dll, CallingConvention = Conv)]
    internal static extern void clmrGetDataFree(ClGetDataRequestPtr request);

    /// <summary>
    /// Gets the progress state of a data request.
    /// </summary>
    /// <param name="chain">A reference to the chain from the <see cref="ClGetDataRequest"/>.</param>
    /// <param name="state">A pointer to the state.</param>
    /// <returns><c>true</c> if the request has completed (either finished or failed); otherwise, <c>false</c> if it is still running.</returns>
    [DllImport(Dll, CallingConvention = Conv)]
    internal static extern CBBool clmrPoll(CBChainRef chain, out ClPollStatePtr state);

    /// <summary>
    /// Frees the state previously retrieved with <see cref="clmrPoll(CBChainRef, out ClPollStatePtr)"/>.
    /// </summary>
    /// <param name="state">A pointer to the state.</param>
    [DllImport(Dll, CallingConvention = Conv)]
    internal static extern void clmrPollFree(ClPollStatePtr state);
  }
}
