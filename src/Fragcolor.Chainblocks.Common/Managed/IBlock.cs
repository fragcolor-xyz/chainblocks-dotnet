/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Collections.Generic;

namespace Fragcolor.Chainblocks.Managed
{
  public interface IBlock
  {
    // TODO: properties or methods?
    // FIXME: some arg or return type to be replaced by proper types

    CBVar Activate(object context, CBVar input);

    void Cleanup();

    object Compose(object data);

    object Composed(CBChainRef chain, object data);

    void Crossover(CBVar state1, CBVar state2);

    void Destroy();

    object ExposedVariables();

    CBVar GetParam(int index);

    CBVar GetState();

    uint Hash();

    string Help();

    string InputHelp();

    object InputTypes();

    void Mutate(CBTable options);

    string Name();

    void NextFrame(object context);

    string OutputHelp();

    object OutputTypes();

    IReadOnlyCollection<object> Parameters();

    CBTable Properties();

    object RequiredVariables();

    void ResetState();

    void SetParam(int index, CBVar value);

    void SetState(CBVar state);

    void Setup();

    void Warmup(object context);
  }
}
