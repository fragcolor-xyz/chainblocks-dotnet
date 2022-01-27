# Unity integration

## Required external libraries

### libcbl

Get it from [chainblocks](https://github.com/fragcolor-xyz/chainblocks) repo by building the `cbl-dll` target.
Copy it in the appropriate CPU folder depending on the platform (e.g. x86_64).

### Fragcolor.Chainblocks.Common

Compile the project under `src/`  and copy the `Fragcolor.Chainblocks.Common.dll` here.

### System.Runtime.CompilerServices.Unsafe

Get it from [nuget](https://www.nuget.org/packages/System.Runtime.CompilerServices.Unsafe/), then open the package file with an archvie reader (e.g. 7-zip) and extract the netstandard2.0 version of the dll.
