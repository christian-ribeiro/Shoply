using Shoply.CodeGenerator.Argument;

namespace Shoply.CodeGenerator.Service;

public static class CodeGenerateService
{
    public static void Generate(InputGenerate inputGenerate)
    {
        var listSameProperty = (from listExternal in inputGenerate.ListPropertyExternal
                                join listInternal in inputGenerate.ListPropertyInternal on listExternal.Name equals listInternal.Name
                                select listExternal.Name).ToList();

        if (listSameProperty.Count > 0)
            throw new Exception("Carai borracha, tá indeciso?");

        PathService.SetFullPath(inputGenerate);

        ContextGenerator.Generate(inputGenerate);
        ControllerGenerator.Generate(inputGenerate);
        ArgumentGenerator.Generate(inputGenerate);
        DTOGenerator.Generate(inputGenerate);
        InterfaceGenerator.Generate(inputGenerate);
        ServiceGenerator.Generate(inputGenerate);
        InfrastructureGenerator.Generate(inputGenerate);
        MapperGenerator.Generate(inputGenerate);
    }
}