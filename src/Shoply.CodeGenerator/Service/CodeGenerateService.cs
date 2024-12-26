using Shoply.CodeGenerator.Argument;

namespace Shoply.CodeGenerator.Service;

public static class CodeGenerateService
{
    public static void Generate(InputGenerate inputGenerate)
    {
        var listRepeatedProperty = (from i in inputGenerate.ListPropertyExternal
                                    join j in inputGenerate.ListPropertyInternal on i.Name equals j.Name
                                    select i.Name).Any();

        var listRepeatedPropertyExternal = (from i in inputGenerate.ListPropertyExternal
                                            join j in inputGenerate.ListPropertyExternal on i.Name equals j.Name
                                            where i.Name == j.Name && inputGenerate.ListPropertyExternal.IndexOf(i) != inputGenerate.ListPropertyExternal.IndexOf(j)
                                            select i).Any();

        var listRepeatedPropertyInternal = (from i in inputGenerate.ListPropertyInternal
                                            join j in inputGenerate.ListPropertyInternal on i.Name equals j.Name
                                            where i.Name == j.Name && inputGenerate.ListPropertyInternal.IndexOf(i) != inputGenerate.ListPropertyInternal.IndexOf(j)
                                            select i).Any();

        if (listRepeatedProperty || listRepeatedPropertyExternal || listRepeatedPropertyInternal)
            throw new Exception("Carai borracha, tá indeciso?");

        PathService.SetFullPath(inputGenerate);

        MappingGenerator.Generate(inputGenerate);

        ControllerGenerator.Generate(inputGenerate);
        ArgumentGenerator.Generate(inputGenerate);
        DTOGenerator.Generate(inputGenerate);
        InterfaceGenerator.Generate(inputGenerate);
        ServiceGenerator.Generate(inputGenerate);
        InfrastructureGenerator.Generate(inputGenerate);
        ContextGenerator.Generate(inputGenerate);
        MapperGenerator.Generate(inputGenerate);
    }
}