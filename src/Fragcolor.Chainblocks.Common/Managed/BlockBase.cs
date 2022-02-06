/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Collections.Generic;

namespace Fragcolor.Chainblocks.Managed
{
  public abstract class BlockBase : IBlock
  {
    public abstract CBVar Activate(object context, CBVar input);

    public abstract void Cleanup();

    public abstract object Compose(object data);

    public abstract object Composed(CBChainRef chain, object data);

    public abstract void Crossover(CBVar state1, CBVar state2);

    public abstract void Destroy();

    public abstract object ExposedVariables();

    public abstract CBVar GetParam(int index);

    public abstract CBVar GetState();

    public abstract uint Hash();

    public abstract string Help();

    public abstract string InputHelp();

    public abstract object InputTypes();

    public abstract void Mutate(CBTable options);

    public abstract string Name();

    public abstract void NextFrame(object context);

    public abstract string OutputHelp();

    public abstract object OutputTypes();

    public abstract IReadOnlyCollection<object> Parameters();

    public abstract CBTable Properties();

    public abstract object RequiredVariables();

    public virtual void ResetState()
    {
      // ResetState is optional
    }

    public abstract void SetParam(int index, CBVar value);

    public virtual void SetState(CBVar state)
    {
      // SetState is optional
    }

    public virtual void Setup()
    {
      // Setup is optional
    }

    public virtual void Warmup(object context)
    {
      // Warmup is optional
    }
  }
}
