# Stride sample

## Required external libraries

### libshards

Get it from [shards](https://github.com/fragcolor-xyz/shards) repo by building the `shards-dll` target.

Copy it into the `MyGame` project folder and in Visual Studio set property 'Copy to Output Directory' to 'Copy if newer'.
Or edit `MyGame.csproj` and add the lines:

```xml
  <ItemGroup>
    <None Update="libshards.dll">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
```
