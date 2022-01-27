# Stride integration

## Required external libraries

### libcbl

Get it from [chainblocks](https://github.com/fragcolor-xyz/chainblocks) repo by building the `cbl-dll` target.

Copy it at the root of the project and in Visual Studio set property 'Copy to Output Directory' to 'Copy if newer'.
Or edit the `.csproj` and add the lines:

```xml
  <ItemGroup>
    <None Update="libcbl.dll">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
```
