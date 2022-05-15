using Fragcolor.Chainblocks.Collections;
using NUnit.Framework;

namespace Fragcolor.Chainblocks.Claymore.Tests
{
   [TestFixture]
  internal sealed class SequenceTests : TestBase
  {
#pragma warning disable CS8618
    private Variable _chain;
    private Node _node;
#pragma warning restore CS8618
    private bool _isScheduled;

    private ref readonly CBChainRef Chain => ref _chain.Value.chain;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      _node = new Node();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
      _node.Dispose();
    }

    [TearDown]
    public void TearDown()
    {
      UnscheduleChain();
      _chain.Dispose();
    }

    public void ScheduleChain()
    {
      if (_isScheduled) return;
      _node.Schedule(Chain);
      _isScheduled = true;
    }

    public void Tick()
    {
      _node.Tick();
    }

    public void UnscheduleChain()
    {
      if (!_isScheduled) return;
      _node.Unschedule(Chain);
      _isScheduled = false;
    }

    [Test]
    public void TestWithAny()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"
(defloop {name}
  (Setup
    [""hello"", ""world""] >= .local)
  .local > .external
)", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      var external = new ExternalVariable(Chain, "external", CBType.Any);

      ScheduleChain();
      Tick();

      Assert.IsTrue(external.Value.seq.Count > 0);
      while (external.Value.seq.Count > 0)
      {
        var val = external.Value.seq.Pop();
        var type = val.type;
        var str = val.GetString();
      }
    }

    [Test]
    public void TestWithSeq()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"
(defloop {name}
  (Setup
    [""hello"", ""world""] >= .local)
  .local > .external
)", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      var external = new ExternalVariable(Chain, "external", CBType.Seq);

      ScheduleChain();
      Tick();

      Assert.IsTrue(external.Value.seq.Count == 2);
      while (external.Value.seq.Count > 0)
      {
        var val = external.Value.seq.Pop();
        var type = val.type;
        var str = val.GetString();
      }
    }

    [Test]
    public void TestWithSeqAndDummyString()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"
(defloop {name}
  (Setup
    [""hello"", ""world""] >= .local)
  .local > .external
)", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      var external = new ExternalVariable(Chain, "external", CBType.Seq);
      var dummy = VariableUtil.NewString("dummy");
      external.Value.seq.Push(ref dummy.Value);

      ScheduleChain();
      Tick();

      Assert.IsTrue(external.Value.seq.Count == 2);
      while (external.Value.seq.Count > 0)
      {
        var val = external.Value.seq.Pop();
        var type = val.type;
        var str = val.GetString();
      }
    }
  }
}
