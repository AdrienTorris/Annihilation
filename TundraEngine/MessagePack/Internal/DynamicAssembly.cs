using System.Reflection;
using System.Reflection.Emit;

namespace TundraEngine.MessagePack.Internal
{
    internal class DynamicAssembly
    {
        readonly AssemblyBuilder assemblyBuilder;
        readonly ModuleBuilder moduleBuilder;

        public ModuleBuilder ModuleBuilder { get { return moduleBuilder; } }

        public DynamicAssembly(string moduleName)
        {
            this.assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(moduleName), AssemblyBuilderAccess.Run);
            this.moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);
        }
    }
}