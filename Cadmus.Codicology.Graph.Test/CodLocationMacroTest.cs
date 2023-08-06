using Xunit;

namespace Cadmus.Codicology.Graph.Test;

public class CodLocationMacroTest
{
    [Fact]
    public void Run_Null_Empty()
    {
        CodLocationMacro m = new();
        string? s = m.Run(null, null);
        Assert.Null(s);
    }
}