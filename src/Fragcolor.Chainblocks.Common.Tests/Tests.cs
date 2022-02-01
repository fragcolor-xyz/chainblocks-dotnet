/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Threading;

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
    [TestFixture]
    public class Tests : TestBase
    {
        [Test]
        public void Test1()
        {
            Native.Core.Log($"Starting {nameof(Test1)}...");

            using var chain = new Variable();
            var ok = Env.Eval(@$"(Chain ""{nameof(Test1)}"" :Looped (Msg ""XXX"") .position (Log) (Pause 1.0))", chain.Ptr);
            Assert.IsTrue(ok);

            var float4 = new Float4 { x = 3, y = 4, z = 5, w = 0};
            var position = new ExternalVariable(chain.Value.chain, "position");
            position.Value.float4 = float4;
            position.Value.type = CBType.Float4;
            position.Value.flags = (1 << 2);

            Native.Core.Schedule(Node, chain.Value.chain);

            for (var i = 0; i < 50; i++)
            {
                Thread.Sleep(100);
                position.Value.float4.w = i;
                Tick();
            }

            Native.Core.Unschedule(Node, chain.Value.chain);

            Native.Core.Log($"{nameof(Test1)} done.");
        }

        [Test]
        public void SetTest()
        {
            using var chain = new Variable();
            var ok = Env.Eval(@$"(Chain ""{nameof(SetTest)}"" :Looped .set (Log))", chain.Ptr);
            Assert.IsTrue(ok);

            var set = new ExternalVariable(chain.Value.chain, "set");
            set.Value.set = CBSet.New();
            set.Value.type = CBType.Set;

            Native.Core.Schedule(Node, chain.Value.chain);

            var float3 = new Variable();
            float3.Value.float3 = new Float3 { x = 5 };
            float3.Value.type = CBType.Float3;

            Assert.AreEqual(0, set.Value.set.Size());
            Tick();

            Assert.IsTrue(set.Value.set.Include(ref float3.Value));
            Assert.IsTrue(set.Value.set.Contains(ref float3.Value));
            Assert.AreEqual(1, set.Value.set.Size());
            Tick();

            var iterator = set.Value.set.GetIterator();
            Assert.IsTrue(set.Value.set.Next(ref iterator, out var value));
            Assert.AreEqual(value.float3, float3.Value.float3);
            Assert.IsFalse(set.Value.set.Next(ref iterator, out _));
            Assert.IsTrue(set.Value.set.Exclude(ref float3.Value));
            Assert.IsFalse(set.Value.set.Contains(ref float3.Value));
            Assert.AreEqual(0, set.Value.set.Size());
            Tick();

            var float4 = new Variable();
            float4.Value.float4 = new Float4 { y = 5 };
            float4.Value.type = CBType.Float4;
            Assert.IsTrue(set.Value.set.Include(ref float4.Value));
            Assert.IsTrue(set.Value.set.Contains(ref float4.Value));
            Assert.AreEqual(1, set.Value.set.Size());
            Tick();

            set.Value.set.Clear();
            Assert.AreEqual(0, set.Value.set.Size());
            Tick();

            Native.Core.Unschedule(Node, chain.Value.chain);
        }

        [Test]
        public void TableTest()
        {
            using var chain = new Variable();
            var ok = Env.Eval(@$"(Chain ""{nameof(TableTest)}"" :Looped .table (Log))", chain.Ptr);
            Assert.IsTrue(ok);

            var table = new ExternalVariable(chain.Value.chain, "table");
            table.Value.table = CBTable.New();
            table.Value.type = CBType.Table;

            Native.Core.Schedule(Node, chain.Value.chain);

            var float3 = new Variable();
            float3.Value.float3 = new Float3 { x = 5 };
            float3.Value.type = CBType.Float3;

            Assert.AreEqual(0, table.Value.table.Size());
            Tick();

            Assert.IsFalse(table.Value.table.Contains("key1"));
            ref var elem = ref table.Value.table.At("key1");
            Assert.IsTrue(elem.IsNone());
            elem.float3 = float3.Value.float3;
            elem.type = CBType.Float3;
            Assert.AreEqual(1, table.Value.table.Size());
            Assert.IsTrue(table.Value.table.Contains("key1"));
            Tick();

            elem.float3.z = 42;
            var iterator = table.Value.table.GetIterator();
            table.Value.table.Next(ref iterator, out var key, out var value);
            Assert.AreEqual("key1", key);
            Assert.AreEqual(42, value.float3.z);
            Tick();

            table.Value.table.Remove("key1");
            Assert.AreEqual(0, table.Value.table.Size());
            Tick();

            Assert.IsFalse(table.Value.table.Contains("key2"));
            _ = ref table.Value.table.At("key2");
            Assert.IsTrue(table.Value.table.Contains("key2"));
            Tick();

            table.Value.table.Clear();
            Assert.AreEqual(0, table.Value.table.Size());
            Tick();

            Native.Core.Unschedule(Node, chain.Value.chain);
        }
    }
}
