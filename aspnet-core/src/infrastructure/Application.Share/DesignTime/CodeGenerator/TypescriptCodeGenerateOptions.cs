using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using NSwag.AssemblyLoader;
using NSwag.Commands.CodeGeneration;
using NSwag.Commands.Generation;
using Volo.Abp.DependencyInjection;

namespace Application.Share.DesignTime.CodeGenerator
{
    public class TypescriptCodeGenerateOptions : ISingletonDependency
    {
        public TypescriptCodeGenerateOptions()
        {

        }

        public string[] Affixes { get; set; }
        public Assembly[] Assemblies { get; set; }
        public string OutputFilePath { get; set; }
    }
}
