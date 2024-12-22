using Shoply.CodeGenerator;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Service;

EnumModule module = EnumModule.Registration;
string subPath = "";
string entityName = "";

List<InputGenerateProperty> listPropertyExternal =
[
];

List<InputGenerateProperty> listPropertyInternal =
[
];

CodeGenerateService.Generate(new InputGenerate(module, subPath, entityName, listPropertyExternal, listPropertyInternal));