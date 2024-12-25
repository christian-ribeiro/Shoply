using Shoply.CodeGenerator.Argument;

namespace Shoply.CodeGenerator.Service;

public static class CodeGenerateService
{
    public static void Generate(InputGenerate inputGenerate)
    {
        var listSameProperty = (from i in inputGenerate.ListPropertyExternal
                                join j in inputGenerate.ListPropertyInternal on i.Name equals j.Name
                                select i.Name).ToList();

        var listSamePropertyExternal = (from i in inputGenerate.ListPropertyExternal
                                        join j in inputGenerate.ListPropertyExternal on i.Name equals j.Name
                                        where i.Name == j.Name && inputGenerate.ListPropertyExternal.IndexOf(i) != inputGenerate.ListPropertyExternal.IndexOf(j)
                                        select i).ToList();

        var listSamePropertyInternal = (from i in inputGenerate.ListPropertyInternal
                                        join j in inputGenerate.ListPropertyInternal on i.Name equals j.Name
                                        where i.Name == j.Name && inputGenerate.ListPropertyInternal.IndexOf(i) != inputGenerate.ListPropertyInternal.IndexOf(j)
                                        select i).ToList();

        if (listSameProperty.Count > 0 || listSamePropertyExternal.Count > 0 || listSamePropertyInternal.Count > 0)
            throw new Exception("Carai borracha, tá indeciso?");

        PathService.SetFullPath(inputGenerate);

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