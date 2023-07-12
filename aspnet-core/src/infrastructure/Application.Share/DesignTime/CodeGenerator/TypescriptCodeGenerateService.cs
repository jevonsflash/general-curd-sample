using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using NSwag.Commands;
using NSwag;
using NSwag.Commands.CodeGeneration;
using NSwag.Commands.Generation;
using NJsonSchema;
using NJsonSchema.Yaml;
using NJsonSchema.Generation;
using NSwag.AssemblyLoader.Utilities;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Application.Share.DesignTime.CodeGenerator
{
    public class TypescriptCodeGenerateService : ITransientDependency
    {
        private OpenApiToTypeScriptClientCommand codeGenerator;
        public TypescriptCodeGenerateService(TypescriptCodeGenerateOptions options)
        {
            Options = options;
            codeGenerator = new OpenApiToTypeScriptClientCommand();
        }

        public TypescriptCodeGenerateOptions Options { get; }

        public async Task<string> Run()
        {
            var assemblies = Options.Assemblies;
            var classNames = new List<string>();
            foreach (var currentAssembly in Options.Assemblies)
            {
                if (currentAssembly != null)
                {
                    var types = currentAssembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (Options.Affixes.Length == 0)
                        {
                            classNames.Add(type.FullName);
                        }
                        else
                        {
                            foreach (var affix in Options.Affixes)
                            {
                                if (type.Name.EndsWith(affix))
                                {
                                    classNames.Add(type.FullName);
                                }
                            }

                        }

                    }
                }
            }
            var Settings = new JsonSchemaGeneratorSettings();
            var rawDocument = new OpenApiDocument();
            var generator = new JsonSchemaGenerator(Settings);
            var schemaResolver = new OpenApiSchemaResolver(rawDocument, Settings);


            var allExportedClassNames = assemblies.SelectMany(a => a.ExportedTypes).Select(t => t.FullName).ToList();
            var matchedClassNames = classNames
                .SelectMany(n => PathUtilities.FindWildcardMatches(n, allExportedClassNames, '.'))
                .Distinct();

            foreach (var className in matchedClassNames)
            {
                var type = assemblies.Select(a => a.GetType(className)).FirstOrDefault(t => t != null);
                generator.Generate(type, schemaResolver);
            }
            OpenApiDocument document = rawDocument;
            codeGenerator.Input = document;
            codeGenerator.Settings.TypeScriptGeneratorSettings.ConvertConstructorInterfaceData = true;
            codeGenerator.MarkOptionalProperties = false;
            codeGenerator.DateTimeType = NJsonSchema.CodeGeneration.TypeScript.TypeScriptDateTimeType.String;
            codeGenerator.NullValue = NJsonSchema.CodeGeneration.TypeScript.TypeScriptNullValue.Null;
            codeGenerator.OutputFilePath = Options.OutputFilePath;
            var result = await codeGenerator.RunAsync(null, null);
            codeGenerator.Input = null;
            return result.ToString();

        }
    }
}
