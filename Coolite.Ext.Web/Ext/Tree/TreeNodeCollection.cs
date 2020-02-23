/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Coolite.Ext.Web
{
    [Serializable]
    [XmlSchemaProvider("MySchema")]
    public class TreeNodeCollection : StateManagedCollection<TreeNodeBase>, IXmlSerializable, ISerializable
    {
        public TreeNodeBase Primary
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[0];
                }

                return null;
            }
        }

        public TreeNodeCollection() : this(false) { }

        public TreeNodeCollection(bool single)
        {
            this.SingleItemMode = single;
        }

        private void AddChildes(TreeNode parent, StringBuilder sb, Control owner)
        {
            string cfg = new ClientConfig().Serialize(parent);

            if (parent.Nodes.Count > 0)
            {
                int index = cfg.LastIndexOf("}");
                sb.Append(cfg.Substring(0, index));

                sb.Append(",children:[");
                foreach (TreeNodeBase node in parent.Nodes)
                {
                    node.Owner = owner;

                    if (node is TreeNode)
                    {
                        TreeNode treeNode = (TreeNode)node;
                        this.AddChildes(treeNode, sb, owner);
                    }
                    else
                    {
                        sb.Append(new ClientConfig().Serialize(node));
                    }

                    sb.Append(",");
                }

                if (sb[sb.Length - 1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("");
                sb.Append("]}");
            }
            else
            {
                sb.Append(cfg);
            }
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder(256);
            bool comma = false;
            sb.Append("[");
            foreach (TreeNodeBase node in this)
            {
                if (comma)
                {
                    sb.Append(",");
                }

                TreeNode treeNode = node as TreeNode;
                if(treeNode == null)
                {
                    sb.Append(new ClientConfig().SerializeInternal(node, this.Owner));     
                }
                else
                {
                    this.AddChildes(treeNode, sb, this.Owner);
                }

                comma = true;
            }
            sb.Append("]");

            return sb.ToString();
        }

        #region Implementation of IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            //no operation
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("json");
            writer.WriteCData(this.ToJson());
            writer.WriteEndElement();
        }

        #endregion

        #region Implementation of ISerializable

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("json", this.ToJson());
        }

        #endregion

        private const string ns = "http://tempuri.org/";
        public static XmlQualifiedName MySchema(XmlSchemaSet xs)
        {
            // This method is called by the framework to get the schema for this type.
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var xw = new XmlTextWriter(sw);

            // generate the schema for type T
            xw.WriteStartDocument();
            xw.WriteStartElement("schema");
            xw.WriteAttributeString("targetNamespace", "http://tempuri.org/");
            xw.WriteAttributeString("xmlns", "http://www.w3.org/2001/XMLSchema");
            xw.WriteStartElement("complexType");
            xw.WriteAttributeString("name", "TreeNodeCollection");
            xw.WriteStartElement("sequence");

            xw.WriteStartElement("element");
            xw.WriteAttributeString("name", "json");
            xw.WriteAttributeString("type", "string");
            xw.WriteEndElement();

            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Close();

            var schemaSerializer = new XmlSerializer(typeof(XmlSchema));
            var sr = new StringReader(sb.ToString());
            var s = (XmlSchema)schemaSerializer.Deserialize(sr);
            xs.XmlResolver = new XmlUrlResolver();
            xs.Add(s);

            return new XmlQualifiedName("TreeNodeCollection", ns);
        }
    }
}