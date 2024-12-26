using Shoply.CodeGenerator;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Service;

EnumModule module = EnumModule.Registration;
EnumDbContext context = EnumDbContext.ShoplyDbContext;
string subPath = "Product";
string entityName = "ProductImage";
string databaseName = "imagem_produto";

List<InputGenerateProperty> listPropertyExternal =
[
   new InputGenerateProperty("FileName", "nome_arquivo", EnumPropertyType.String, false, false, true, null),
   new InputGenerateProperty("FileLength", "tamanho_arquivo", EnumPropertyType.Decimal, false, false, false, null),
   new InputGenerateProperty("ImageUrl", "link_imagem", EnumPropertyType.String, false, false, false, null),
   new InputGenerateProperty("ProductId", "id_produto", EnumPropertyType.Long, false, false, false, null),
];

List<InputGenerateProperty> listPropertyInternal =
[
];

CodeGenerateService.Generate(new InputGenerate(context, module, subPath, entityName, databaseName, listPropertyExternal, listPropertyInternal));