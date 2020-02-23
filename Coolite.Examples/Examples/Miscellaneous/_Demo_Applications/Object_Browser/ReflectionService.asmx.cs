using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.Services;
using Coolite.Ext.Web;

namespace Coolite.Examples.Examples.Miscellaneous.Demo_Applications.Object_Browser
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ReflectionService : System.Web.Services.WebService
    {
        [WebMethod]
        public TreeNodeCollection GetNamespaces(string node, string marker)
        {
            node = marker;
            TreeNodeCollection nodes = new TreeNodeCollection(false);
            
            if(!string.IsNullOrEmpty(node))
            {
                string requestedNS = null;
                string requestedType = null;

                if(node == "_root")
                {
                    requestedNS = node;
                }
                
                if(node.StartsWith("ns:"))
                {
                    requestedNS = node.Substring(3);
                }

                if (node.StartsWith("type:"))
                {
                    requestedType = node.Substring(5);
                }

                if (!string.IsNullOrEmpty(requestedNS))
                {
                    BuildNamespace(requestedNS, nodes);
                }
                else if (!string.IsNullOrEmpty(requestedType))
                {
                    BuildBaseTypes(requestedType, nodes);
                }
            }
            
            return nodes;
        }

        [WebMethod]
        public TreeNodeCollection GetMembers(string node, string marker)
        {
            TreeNodeCollection list = new TreeNodeCollection(false);

            if(!string.IsNullOrEmpty(marker) && marker.StartsWith("type:"))
            {
                string requestedType = marker.Substring(5);
                TypeInfo typeInfo = TypeInfo.GetTypeInfo(requestedType);
                int index = 0;

                TreeNodeCollection ctorsList = new TreeNodeCollection(false);
                foreach (CooliteMethodInfo ctor in typeInfo.Constructors)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Leaf = true;
                    treeNode.Text = this.Context.Server.HtmlEncode(ctor.Signature);
                    treeNode.CustomAttributes.Add(new ConfigItem("marker", "type:" + typeInfo.Type.GUID, ParameterMode.Value));
                    treeNode.CustomAttributes.Add(new ConfigItem("member", "ctor:" + index++, ParameterMode.Value));
                    treeNode.IconCls = "method-icon";
                    ctorsList.Add(treeNode);
                }
                ctorsList.Sort((x, y) => x.Text.CompareTo(y.Text));
                list.AddRange(ctorsList);

                index = 0;
                TreeNodeCollection methodsList = new TreeNodeCollection(false);
                foreach (CooliteMethodInfo method in typeInfo.Methods)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Leaf = true;
                    treeNode.Text = this.Context.Server.HtmlEncode(method.Signature);
                    treeNode.CustomAttributes.Add(new ConfigItem("marker", "type:" + typeInfo.Type.GUID, ParameterMode.Value));
                    treeNode.CustomAttributes.Add(new ConfigItem("member", "method:" + index++, ParameterMode.Value));
                    treeNode.IconCls = "method-icon";
                    methodsList.Add(treeNode);
                }
                methodsList.Sort((x, y) => x.Text.CompareTo(y.Text));
                list.AddRange(methodsList);
            }
            

            return list;
        }

        [WebMethod]
        public AjaxResponse GetMemberDescription(string marker, string member)
        {
            AjaxResponse response = new AjaxResponse();

            if (!string.IsNullOrEmpty(marker) && marker.StartsWith("type:") &&
                !string.IsNullOrEmpty(member))
            {
                string requestedType = marker.Substring(5);
                TypeInfo typeInfo = TypeInfo.GetTypeInfo(requestedType);

                int index = -1;
                string prototype = "";
                if(member.StartsWith("ctor:"))
                {
                    index = int.Parse(member.Substring(5));
                    prototype = typeInfo.Constructors[index].Prototype;
                }
                else if (member.StartsWith("method:"))
                {
                    index = int.Parse(member.Substring(7));

                    prototype = typeInfo.Methods[index].Prototype;
                }
                else if (member.StartsWith("property:"))
                {
                    index = int.Parse(member.Substring(9));
                }
                else if (member.StartsWith("field:"))
                {
                    index = int.Parse(member.Substring(6));
                }
                else if (member.StartsWith("event:"))
                {
                    index = int.Parse(member.Substring(6));
                }

                ParameterCollection prms = new ParameterCollection();
                prms["mPrototype"] = prototype;
                
                //TODO: add XML docs information and Description attribute
                //prms["xml docs"] = "value2";

                response.ExtraParamsResponse = prms.ToJson();
            }

            return response;
        }

        private void BuildBaseTypes(string requestedType, TreeNodeCollection nodes)
        {
            TypeInfo typeInfo = TypeInfo.GetTypeInfo(requestedType);

            Type baseType = typeInfo.BaseType;
            if (baseType != null)
            {
                AddType(baseType, nodes, false);
            }

            Type[] interfaces = typeInfo.Interfaces;
            TreeNodeCollection interfacesNodes = new TreeNodeCollection(false);
            foreach (Type interfaceType in interfaces)
            {
                AddType(interfaceType, interfacesNodes, false);
            }

            interfacesNodes.Sort((x, y) => x.Text.CompareTo(y.Text));
            nodes.AddRange(interfacesNodes);
        }

        private void BuildNamespace(string requestedNS, TreeNodeCollection nodes)
        {
            ICollection<NamespaceInfo> list;
            NamespaceInfo ns;
            if (requestedNS == "_root")
            {
                list = ReflectionHelper.Namespaces;
                foreach (NamespaceInfo info in list)
                {
                    TreeNodeBase treeNode;
                    if (info.NamespaceTypes.Count == 0)
                    {
                        treeNode = new TreeNode();
                        treeNode.Leaf = true;
                    }
                    else
                    {
                        treeNode = new AsyncTreeNode();
                        treeNode.SingleClickExpand = true;
                    }

                    treeNode.Text = this.Context.Server.HtmlEncode(info.NamespaceName);
                    //treeNode.NodeID = "ns:"+info.FullName;
                    treeNode.CustomAttributes.Add(new ConfigItem("marker", "ns:" + info.NamespaceName, ParameterMode.Value));
                    treeNode.IconCls = "namespace-icon";
                    nodes.Add(treeNode);
                }
                nodes.Sort((x, y) => x.Text.CompareTo(y.Text));
            }
            else
            {
                ns = NamespaceInfo.GetByName(requestedNS);
                if (ns != null)
                {
                    foreach (Type type in ns.NamespaceTypes)
                    {
                        this.AddType(type, nodes, true);
                    }
                }
                nodes.Sort((x, y) => x.Text.CompareTo(y.Text));
            }
        }

        private void AddType(Type type, TreeNodeCollection typesNodes, bool addBaseTypeNode)
        {
            TypeInfo typeInfo = TypeInfo.AddToCache(type);

            if(addBaseTypeNode)
            {
                TreeNode typeNode = new TreeNode();
                typeNode.Leaf = false;
                //typeNode.SingleClickExpand = true;
                typeNode.Text = this.Context.Server.HtmlEncode(typeInfo.TypeName);
                typeNode.CustomAttributes.Add(new ConfigItem("marker", "type:" + typeInfo.Type.GUID, ParameterMode.Value));
                typeNode.CustomAttributes.Add(new ConfigItem("isType", "true", ParameterMode.Raw));
                typeNode.IconCls = typeInfo.IconCls;
                typesNodes.Add(typeNode);

                AsyncTreeNode baseTypeNode = new AsyncTreeNode();
                baseTypeNode.SingleClickExpand = true;
                baseTypeNode.Text = "Base types";
                baseTypeNode.CustomAttributes.Add(new ConfigItem("marker", "type:" + typeInfo.Type.GUID, ParameterMode.Value));
                typeNode.Nodes.Add(baseTypeNode);
                return;
            }
            
            TreeNodeBase treeNode;
            if (typeInfo.BaseType != null || typeInfo.Interfaces.Length > 0)
            {
                treeNode = new AsyncTreeNode();
                //treeNode.SingleClickExpand = true;
            }
            else
            {
                treeNode = new TreeNode();
                treeNode.Leaf = true;
            }

            treeNode.Text = this.Context.Server.HtmlEncode(typeInfo.TypeName);
            //treeNode.NodeID = "type:" + typeInfo.Type.GUID;
            treeNode.CustomAttributes.Add(new ConfigItem("marker", "type:" + typeInfo.Type.GUID, ParameterMode.Value));
            treeNode.CustomAttributes.Add(new ConfigItem("isType", "true", ParameterMode.Raw));
            treeNode.IconCls = typeInfo.IconCls;
            typesNodes.Add(treeNode);
        }
    }
}
