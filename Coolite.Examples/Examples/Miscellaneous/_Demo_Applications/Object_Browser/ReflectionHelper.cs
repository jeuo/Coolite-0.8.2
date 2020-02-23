using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Coolite.Examples.Examples.Miscellaneous.Demo_Applications.Object_Browser
{
    public class ReflectionHelper
    {
        private static List<NamespaceInfo> namespacesList;
        private static readonly object syncNS = new object();
        public static List<NamespaceInfo> Namespaces
        {
            get
            {
                lock (syncNS)
                {
                    if (namespacesList == null)
                    {
                        namespacesList = new List<NamespaceInfo>();

                        foreach (Type type in Types)
                        {
                            if (!string.IsNullOrEmpty(type.Namespace))
                            {
                                NamespaceInfo _namespace = NamespaceInfo.AddType(type);
                                if (!namespacesList.Contains(_namespace))
                                {
                                    namespacesList.Add(_namespace);
                                }
                            }
                        }
                    }
                    return namespacesList;
                }
            }
        }

        private static List<Type> typesList;
        private static readonly object syncTypes = new object();
        public static List<Type> Types
        {
            get
            {
                lock (syncTypes)
                {
                    if(typesList == null)
                    {
                        typesList = new List<Type>();
                        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                        foreach (Assembly assembly in assemblies)
                        {
                            Type[] types = assembly.GetTypes();
                            foreach (Type type in types)
                            {
                                if (type.IsPublic || (type.IsNested && !type.IsNestedPrivate))
                                {
                                    typesList.Add(type);
                                }
                            }
                        }
                    }

                    return typesList;
                }
            }
        }
    }

    public class NamespaceInfo
    {
        private readonly string namespaceName;
        private readonly List<Type> namespaceTypes = new List<Type>();
        private static readonly Dictionary<string, NamespaceInfo> namespaces = new Dictionary<string, NamespaceInfo>();

        public NamespaceInfo(string namespaceName)
        {
            this.namespaceName = namespaceName;
        }

        public string NamespaceName
        {
            get { return namespaceName; }
        }

        public List<Type> NamespaceTypes
        {
            get { return namespaceTypes; }
        }

        public static NamespaceInfo AddType(Type type)
        {
            lock (namespaces)
            {
                NamespaceInfo _namespace;
                if (!namespaces.TryGetValue(type.Namespace, out _namespace))
                {
                    _namespace = new NamespaceInfo(type.Namespace);
                    namespaces.Add(type.Namespace, _namespace);
                }

                if (!_namespace.namespaceTypes.Contains(type))
                {
                    _namespace.namespaceTypes.Add(type);    
                }

                return _namespace;
            }
        }

        public static NamespaceInfo GetByName(string name)
        {
            NamespaceInfo _namespace;
            if (namespaces.TryGetValue(name, out _namespace))
            {
                return _namespace;
            }

            return null;
        }

    }

    public class TypeInfo
    {
        private static readonly Dictionary<Guid, TypeInfo> handledTypes = new Dictionary<Guid, TypeInfo>();
        public static TypeInfo GetTypeInfo(string guid)
        {
            TypeInfo typeInfo;
            if(handledTypes.TryGetValue(new Guid(guid), out typeInfo))
            {
                return typeInfo;
            }

            return null;
        }

        public static TypeInfo AddToCache(Type type)
        {
            lock (handledTypes)
            {
                TypeInfo typeInfo;
                if (!handledTypes.TryGetValue(type.GUID, out typeInfo))
                {
                    typeInfo = new TypeInfo(type);
                    handledTypes.Add(type.GUID, typeInfo);
                }

                return typeInfo;
            }
        }
        
        private string typeName;
        private string fullTypeName;
        private Type type;
        private Type baseType;
        private Type[] interfaces;
        private string iconCls;
        private CooliteMethodInfo[] methods;
        private CooliteMethodInfo[] ctors;

        public TypeInfo(Type type)
        {
            this.type = type;
        }

        public string IconCls
        {
            get
            {
                if(string.IsNullOrEmpty(this.iconCls))
                {
                    if (type.IsSubclassOf(typeof(System.Delegate)))
                    {
                        this.iconCls = "delegate-icon";
                    }
                    else if (type.IsClass || type.Equals(typeof(System.Enum)))
                    {
                        this.iconCls = "class-icon";
                    }
                    else if (type.IsInterface)
                    {
                        this.iconCls = "interface-icon";
                    }
                    else if (type.IsEnum)
                    {
                        this.iconCls = "enum-icon";
                    }
                    else if (type.IsValueType)
                    {
                        this.iconCls = "struct-icon";
                    }
                    else
                    {
                        this.iconCls = "";
                    }
                }

                return this.iconCls;
            }
        }

        public string FullTypeName
        {
            get
            {
                if (string.IsNullOrEmpty(this.fullTypeName))
                {
                    this.fullTypeName = this.BuildTypeName(true);
                }

                return this.fullTypeName;
            }
        }
        
        public string TypeName
        {
            get
            {
                if (string.IsNullOrEmpty(this.typeName))
                {
                    this.typeName = this.BuildTypeName(false);
                }
                return this.typeName;
            }
        }

        private string BuildTypeName(bool full)
        {
            string result;
            if (this.type.IsGenericType)
            {
                string name = full ? this.type.FullName : this.type.Name;
                if(string.IsNullOrEmpty(name))
                {
                    name = this.type.Name;
                }
                int index = name.LastIndexOf("`");
                if (index < 0)
                {
                    index = name.Length;
                }
                name = name.Substring(0, index).Replace("&","");
                
                StringBuilder sb = new StringBuilder(name);
                sb.Append("<");
                foreach (Type genericArgument in this.type.GetGenericArguments())
                {
                    sb.Append(genericArgument.Name).Append(",");
                }
                if (sb[sb.Length - 1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append(">");

                result = sb.ToString();
            }
            else
            {
                result = (full ? this.type.FullName : this.type.Name).Replace("&", "");
            }

            if (this.type.IsNested)
            {
                result = string.Concat(this.type.DeclaringType.Name, ".", result);
            }

            return result;
        }

        public Type Type
        {
            get { return type; }
        }

        public Type BaseType
        {
            get
            {
                if(this.baseType == null)
                {
                    this.baseType = this.type.BaseType;
                }
                return this.baseType;
            }
        }

        public Type[] Interfaces
        {
            get
            {
                if (this.interfaces == null)
                {
                    this.interfaces = this.type.GetInterfaces();
                    Type[] baseInterfaces;
                    if(this.BaseType != null)
                    {
                        baseInterfaces = this.BaseType.GetInterfaces();
                    }
                    else
                    {
                        baseInterfaces = new Type[0];
                    }

                    List<Type> declaredIfaces = new List<Type>();
                    foreach (Type iface in this.interfaces)
                    {
                        int index = Array.IndexOf(baseInterfaces, iface);
                        if(index < 0)
                        {
                            declaredIfaces.Add(iface);
                        }
                    }

                    this.interfaces = declaredIfaces.ToArray();
                }
                return this.interfaces;
            }
        }

        public CooliteMethodInfo[] Methods
        {
            get
            {
                if (this.methods == null)
                {
                    MethodInfo[] methodInfos = this.Type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    List<CooliteMethodInfo> methodsList = new List<CooliteMethodInfo>(methodInfos.Length);
                    foreach (MethodInfo methodInfo in methodInfos)
                    {
                        if (methodInfo.IsSpecialName)
                        {
                            continue;
                        }
                        methodsList.Add(new CooliteMethodInfo(methodInfo, this.Type));
                    }

                    this.methods = methodsList.ToArray();
                }

                return this.methods;
            }
        }

        public CooliteMethodInfo[] Constructors
        {
            get
            {
                if (this.ctors == null)
                {
                    ConstructorInfo[] ctrosInfo = this.Type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

                    if(ctrosInfo.Length == 1 && ctrosInfo[0].GetParameters().Length == 0)
                    {
                        this.ctors = new CooliteMethodInfo[0];
                        return this.ctors;
                    }

                    List<CooliteMethodInfo> ctorsList = new List<CooliteMethodInfo>(ctrosInfo.Length);
                    foreach (ConstructorInfo ctorInfo in ctrosInfo)
                    {
                        ctorsList.Add(new CooliteMethodInfo(ctorInfo, this.Type));
                    }

                    this.ctors = ctorsList.ToArray();
                }

                return this.ctors;
            }
        }
    }

    public class CooliteMethodInfo
    {
        private MethodInfo method;
        private ConstructorInfo ctor;
        private Type parentType;

        public CooliteMethodInfo(MethodInfo method, Type parentType)
        {
            this.method = method;
            this.parentType = parentType;
        }

        public CooliteMethodInfo(ConstructorInfo ctor, Type parentType)
        {
            this.ctor = ctor;
            this.parentType = parentType;
        }

        public MethodInfo Method
        {
            get { return method; }
        }

        public ConstructorInfo Constructor
        {
            get { return ctor; }
        }

        private string signature;
        public string Signature
        {
            get
            {
                if(this.signature == null)
                {
                    MethodBase meth = (MethodBase)this.method ?? this.ctor;
                    StringBuilder sb = new StringBuilder();

                    string name = this.method != null ? this.method.Name : parentType.Name;
                    if (meth.IsGenericMethod)
                    {
                        int index = name.LastIndexOf("`");
                        if (index < 0)
                        {
                            index = name.Length;
                        }

                        sb.Append(name.Substring(0, index));
                        sb.Append("<");
                        foreach (Type genericArgument in meth.GetGenericArguments())
                        {
                            sb.Append(genericArgument.Name).Append(",");
                        }
                        if (sb[sb.Length - 1] == ',')
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append(">");
                    }
                    else
                    {
                        sb.Append(name);
                    }

                    sb.Append("(");

                    bool needComma = false;
                    foreach (ParameterInfo parameter in meth.GetParameters())
                    {
                        if(needComma)
                        {
                            sb.Append(", ");
                        }
                        needComma = true;
                        if (parameter.IsOut)
                        {
                            sb.Append("out ");
                        }
                        else if(parameter.ParameterType.IsByRef)
                        {
                            sb.Append("ref ");
                        }
                        
                        TypeInfo typeInfo = TypeInfo.AddToCache(parameter.ParameterType);
                        sb.Append(typeInfo.FullTypeName);
                    }

                    sb.Append(")");

                    this.signature = sb.ToString();
                }
                return this.signature;
            }
        }

        private string prototype;
        public string Prototype
        {
            get
            {

                if (this.prototype == null)
                {
                    MethodBase meth = (MethodBase)this.method ?? this.ctor;
                    StringBuilder sb = new StringBuilder();

                    if(meth.IsPublic)
                    {
                        sb.Append("public ");
                    }
                    if (meth.IsFamilyOrAssembly)
                    {
                        sb.Append("internal protected ");
                    }
                    if (meth.IsAssembly)
                    {
                        sb.Append("internal ");
                    }
                    else if (meth.IsFamily)
                    {
                        sb.Append("protected ");
                    }
                    else if (meth.IsPrivate)
                    {
                        sb.Append("private ");
                    }

                    if(meth.IsAbstract)
                    {
                        sb.Append("abstract ");
                    }

                    if (meth.IsStatic)
                    {
                        sb.Append("static ");
                    }

                    if (meth.IsFinal)
                    {
                        sb.Append("sealed ");
                    }

                    if (this.method != null && meth.IsVirtual)
                    {
                        if (this.method.GetBaseDefinition() != this.method)
                        {
                            sb.Append("override ");
                        }
                        else
                        {
                            sb.Append("virtual ");    
                        }
                    }

                    if (this.method != null)
                    {
                        TypeInfo returnType = TypeInfo.AddToCache(this.method.ReturnType);
                        sb.Append(returnType.FullTypeName).Append(" "); 
                    }

                    string name = this.method != null ? this.method.Name : parentType.Name;
                    if (meth.IsGenericMethod)
                    {
                        
                        int index = name.LastIndexOf("`");
                        if (index < 0)
                        {
                            index = name.Length;
                        }

                        sb.Append(name.Substring(0, index));
                        sb.Append("<");
                        foreach (Type genericArgument in meth.GetGenericArguments())
                        {
                            sb.Append(genericArgument.Name).Append(",");
                        }
                        if (sb[sb.Length - 1] == ',')
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append(">");
                    }
                    else
                    {
                        sb.Append(name);
                    }

                    sb.Append("(");

                    bool needComma = false;
                    foreach (ParameterInfo parameter in meth.GetParameters())
                    {
                        if (needComma)
                        {
                            sb.Append(", ");
                        }
                        needComma = true;
                        if (parameter.IsOut)
                        {
                            sb.Append("out ");
                        }
                        else if (parameter.ParameterType.IsByRef)
                        {
                            sb.Append("ref ");
                        }

                        TypeInfo typeInfo = TypeInfo.AddToCache(parameter.ParameterType);
                        sb.Append(typeInfo.FullTypeName).Append(" ");
                        sb.Append(parameter.Name);
                    }

                    sb.Append(")");

                    this.prototype = sb.ToString();
                }

                return this.prototype;
            }
        }
    }

    public class CoolitePropertyInfo
    {
        private PropertyInfo property;
        private Type parentType;

        public CoolitePropertyInfo(PropertyInfo property, Type parentType)
        {
            this.property = property;
            this.parentType = parentType;
        }

        public PropertyInfo Property
        {
            get { return property; }
        }
    }
}
