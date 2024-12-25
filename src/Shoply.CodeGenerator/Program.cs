﻿using Shoply.CodeGenerator;
using Shoply.CodeGenerator.Argument;
using Shoply.CodeGenerator.Service;

EnumModule module = EnumModule.Registration;
EnumDbContext context = EnumDbContext.ShoplyDbContext;
string subPath = "";
string entityName = "";
string databaseName = "";

List<InputGenerateProperty> listPropertyExternal =
[
];

List<InputGenerateProperty> listPropertyInternal =
[
];

CodeGenerateService.Generate(new InputGenerate(context, module, subPath, entityName, databaseName, listPropertyExternal, listPropertyInternal));