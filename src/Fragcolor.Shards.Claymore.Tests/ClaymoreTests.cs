/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Threading.Tasks;
using Fragcolor.Shards.Collections;
using NUnit.Framework;

namespace Fragcolor.Shards.Claymore.Tests
{
  [TestFixture]
  internal sealed class ClaymoreTests : TestBase
  {
    [Test]
    public void TestHexToBytes()
    {
      Assert.IsEmpty(Util.HexToBytes(""));
      Assert.IsEmpty(Util.HexToBytes("0x"));
      Assert.Throws(typeof(ArgumentException), () => Util.HexToBytes("zz"));

      var bytes = Util.HexToBytes("0x0123456789abcdef");
      Assert.AreEqual(8, bytes.Length);
    }

    [Test, Ignore("Disabled until we can mock the sending side")]
    public void TestRequestData()
    {
      const string hash = "0x0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";
      using var result = Claymore.RequestData(hash);

      ref var value = ref result.Value;
      Assert.AreEqual(SHType.Table, value.type);

      ref var table = ref value.table;
      // TODO: later we should have a proper error message following the timeout
      Assert.AreEqual(0, table.Size());
    }

    [Test, Ignore("Disabled until we can mock the sending side")]
    public async Task TestRequestDataAsync()
    {
      const string hash = "0X0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF";
      using var result = await Claymore.RequestDataAsync(hash);
      Do(result);

      // note: local method used as a workaround since ref locals cannot be used inside async methods
      static void Do(Variable v)
      {
        ref var value = ref v.Value;
        Assert.AreEqual(SHType.Table, value.type);

        ref var table = ref value.table;
        // TODO: later we should have a proper error message following the timeout
        Assert.AreEqual(0, table.Size());
      }
    }

    [Test, Ignore("Disabled until we can mock the sending side")]
    public void TestRequestDestructor()
    {
      {
        _ = new GetRequest("");
      }
      // destructor eventually called when out of scope
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    [Test, Ignore("Disabled until we can mock the receiving side")]
    public void TestUpload()
    {
      var bytes = Array.Empty<byte>();
      Claymore.Upload(bytes, "audio");
    }

    [Test, Ignore("Disabled until we can mock the receiving side")]
    public async Task TestUploadAsync()
    {
      var bytes = Array.Empty<byte>();
      await Claymore.UploadAsync(bytes, "audio");
    }
  }
}
