using Xunit;

namespace Cadmus.Codicology.Parts.Test;

public sealed class CodLocationTest
{
    [Fact]
    public void Parse_Null_Null()
    {
        CodLocation? loc = CodLocation.Parse(null);
        Assert.Null(loc);
    }

    [Fact]
    public void Parse_Empty_Null()
    {
        CodLocation? loc = CodLocation.Parse("");
        Assert.Null(loc);
    }

    [Fact]
    public void Parse_Invalid_Null()
    {
        CodLocation? loc = CodLocation.Parse("invalid");
        Assert.Null(loc);
    }

    [Fact]
    public void ParseN()
    {
        CodLocation? loc = CodLocation.Parse("12");
        Assert.NotNull(loc);
        Assert.Equal(CodLocationEndleaf.None, loc!.Endleaf);
        Assert.False(loc.Rmn);
        Assert.Equal(12, loc.N);
        Assert.Null(loc.Word);
    }

    [Fact]
    public void ParseSysN()
    {
        CodLocation? loc = CodLocation.Parse("x:12");
        Assert.NotNull(loc);
        Assert.Equal("x", loc.S);
        Assert.Equal(12, loc.N);
        Assert.False(loc.Rmn);
        // all other properties are default
        Assert.Equal(CodLocationEndleaf.None, loc!.Endleaf);
        Assert.Null(loc.Sfx);
        Assert.Null(loc.V);
        Assert.Null(loc.C);
        Assert.Equal(0, loc.L);
        Assert.Null(loc.Word);
    }

    [Fact]
    public void ParseSysRmnN()
    {
        CodLocation? loc = CodLocation.Parse("x:^12");
        Assert.NotNull(loc);
        Assert.Equal("x", loc.S);
        Assert.True(loc.Rmn);
        Assert.Equal(12, loc.N);
        // all other properties are default
        Assert.Equal(CodLocationEndleaf.None, loc!.Endleaf);
        Assert.Null(loc.Sfx);
        Assert.Null(loc.V);
        Assert.Null(loc.C);
        Assert.Equal(0, loc.L);
        Assert.Null(loc.Word);
    }

    [Fact]
    public void ParseSysRmnNInBrackets()
    {
        CodLocation? loc = CodLocation.Parse("(x:^12)");
        Assert.NotNull(loc);
        Assert.Equal(CodLocationEndleaf.FrontEndleaf, loc!.Endleaf);
        Assert.Equal("x", loc.S);
        Assert.True(loc.Rmn);
        Assert.Equal(12, loc.N);
        // all other properties are default
        Assert.Null(loc.Sfx);
        Assert.Null(loc.V);
        Assert.Null(loc.C);
        Assert.Equal(0, loc.L);
        Assert.Null(loc.Word);
    }

    [Fact]
    public void ParseSysRmnNInBracketsSlash()
    {
        CodLocation? loc = CodLocation.Parse("(/x:^12)");
        Assert.NotNull(loc);
        Assert.Equal(CodLocationEndleaf.BackEndleaf, loc!.Endleaf);
        Assert.Equal("x", loc.S);
        Assert.True(loc.Rmn);
        Assert.Equal(12, loc.N);
        // all other properties are default
        Assert.Null(loc.Sfx);
        Assert.Null(loc.V);
        Assert.Null(loc.C);
        Assert.Equal(0, loc.L);
        Assert.Null(loc.Word);
    }

    [Fact]
    public void ParseSysRmnNSfxRColLine()
    {
        CodLocation? loc = CodLocation.Parse("x:^12\"bis\"ra.3");
        Assert.NotNull(loc);
        Assert.Equal("x", loc.S);
        Assert.True(loc.Rmn);
        Assert.Equal(12, loc.N);
        Assert.Equal("bis", loc.Sfx);
        Assert.Equal(false, loc.V);
        Assert.Equal("a", loc.C);
        Assert.Equal(3, loc.L);
        // all other properties are default
        Assert.Equal(CodLocationEndleaf.None, loc!.Endleaf);
        Assert.Null(loc.Word);
    }

    [Fact]
    public void ParseSysRmnNSfxRColLineWord()
    {
        CodLocation? loc = CodLocation.Parse("x:^12\"bis\"ra.3@exemplum");
        Assert.NotNull(loc);
        Assert.Equal("x", loc.S);
        Assert.True(loc.Rmn);
        Assert.Equal(12, loc.N);
        Assert.Equal("bis", loc.Sfx);
        Assert.Equal(false, loc.V);
        Assert.Equal("a", loc.C);
        Assert.Equal(3, loc.L);
        Assert.Equal("exemplum", loc.Word);
        // all other properties are default
        Assert.Equal(CodLocationEndleaf.None, loc!.Endleaf);
    }

    // TODO other tests see cod-location-parser-spec.ts in bricks shell
}
