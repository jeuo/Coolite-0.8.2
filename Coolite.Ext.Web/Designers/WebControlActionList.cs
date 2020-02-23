/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace Coolite.Ext.Web
{
    
    public class WebControlActionList : System.ComponentModel.Design.DesignerActionList
    {
        public DesignerActionItemCollection Items;

        public WebControlActionList(IComponent component) : base(component) 
        {
            this.Control = component as WebControl;
        }

        public override bool AutoShow
        {
            get
            {
                return false;
            }
            set
            {
                base.AutoShow = false;
            }
        }

        private WebControl control;
        public WebControl Control
        {
            get
            {
                return this.control;
            }
            set
            {
                this.control = value;
            }
        }

        //public bool Trace
        //{
        //    get
        //    {
        //        return this.Control.Trace;
        //    }
        //    set
        //    {
        //        GetPropertyByName("Trace").SetValue(this.Control, value);
        //    }
        //}

        public string ID
        {
            get
            {
                return this.Control.ID;
            }
            set
            {
                GetPropertyByName("ID").SetValue(this.Control, value);
            }
        }

        public void LaunchSupportHome()
        {
            System.Diagnostics.Process.Start("http://www.coolite.com/support/");
        }

        public void LaunchForums()
        {
            System.Diagnostics.Process.Start("http://www.coolite.com/forums/");
        }

        public void LaunchDocumentation()
        {
            System.Diagnostics.Process.Start("http://www.coolite.com/docs/");
        }

        public void LaunchExamples()
        {
            System.Diagnostics.Process.Start("http://www.coolite.com/examples/");
        }

        public PropertyDescriptor GetPropertyByName(string name)
        {
            PropertyDescriptor property;
            property = TypeDescriptor.GetProperties(this.Control)[name];
            if (null == property)
                throw new ArgumentException("Matching WebControl property not found.", name);
            else
                return property;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddHeaderItem(new DesignerActionHeaderItem("Properties", "500"));
            this.AddHeaderItem(new DesignerActionHeaderItem("Support [Version " + this.Control.Version + "]", "1000"));

            this.AddMethodItem(new DesignerActionMethodItem(this, "LaunchExamples", "Examples Explorer", "1000", "View the Coolite examples online"));
            this.AddMethodItem(new DesignerActionMethodItem(this, "LaunchForums", "Online Forums", "1000", "Visit the Coolite Forums"));
            this.AddMethodItem(new DesignerActionMethodItem(this, "LaunchSupportHome", "Coolite Support Home", "1000", "Visit the Coolite website for more support options", true));
            this.AddMethodItem(new DesignerActionMethodItem(this, "LaunchDocumentation", "Online Documentation", "1000", "View online documentation"));

            this.Items = new DesignerActionItemCollection();
            //this.Items.Add(new DesignerActionPropertyItem("Trace", "Trace", string.Empty, "Trace the Design-Time HTML"));
            this.Items.Add(new DesignerActionPropertyItem("ID", "ID", string.Empty, "Change the ID of the control"));

            foreach (DesignerActionHeaderItem item in this.Headers)
            {
                this.Items.Add(item);
            }

            foreach (DesignerActionPropertyItem item in this.Properties)
            {
                this.Items.Add(item);
            }

            foreach (DesignerActionMethodItem item in this.Methods)
            {
                this.Items.Add(item);
            }

            return this.Items;
        }

        public void AddHeaderItem(DesignerActionHeaderItem item)
        {
            foreach (DesignerActionHeaderItem header in this.Headers)
            {
                if (item.DisplayName == header.DisplayName)
                {
                    return;
                }
            }
            this.Headers.Add(item);
        }

        public void RemoveHeaderItem(DesignerActionHeaderItem item)
        {
            foreach (DesignerActionHeaderItem header in this.Headers)
            {
                if (item.DisplayName == header.DisplayName)
                {
                    this.Headers.Remove(header);
                    return;
                }
            }
        }

        public void AddPropertyItem(DesignerActionPropertyItem item)
        {
            foreach (DesignerActionPropertyItem property in this.Properties)
            {
                if (item.MemberName == property.MemberName && item.Category == property.Category)
                {
                    return;
                }
            }
            this.Properties.Add(item);
        }

        public void RemovePropertyItem(DesignerActionPropertyItem item)
        {
            foreach (DesignerActionPropertyItem property in this.Properties)
            {
                if (item.MemberName == property.MemberName && item.Category == property.Category)
                {
                    this.Properties.Remove(property);
                    return;
                }
            }
        }

        public void AddMethodItem(DesignerActionMethodItem item)
        {
            foreach (DesignerActionMethodItem method in this.Methods)
            {
                if (item.MemberName == method.MemberName && item.Category == method.Category)
                {
                    return;
                }
            }
            this.Methods.Add(item);
        }

        public void RemoveMethodItem(DesignerActionMethodItem item)
        {
            foreach (DesignerActionMethodItem method in this.Methods)
            {
                if (item.MemberName == method.MemberName && item.Category == method.Category)
                {
                    this.Methods.Remove(method);
                    return;
                }
            }
        }

        private List<DesignerActionHeaderItem> headers = new List<DesignerActionHeaderItem>();
        private List<DesignerActionHeaderItem> Headers
        {
            get
            {
                this.headers.Sort(new DesignerActionHeaderItemComparer());
                return this.headers;
            }
        }

        private List<DesignerActionPropertyItem> properties = new List<DesignerActionPropertyItem>();
        private List<DesignerActionPropertyItem> Properties
        {
            get
            {
                return this.properties;
            }
        }

        private List<DesignerActionMethodItem> methods = new List<DesignerActionMethodItem>();
        private List<DesignerActionMethodItem> Methods
        {
            get
            {
                return this.methods;
            }
        }
    }

    public class DesignerActionHeaderItemComparer : IComparer<DesignerActionHeaderItem>
    {
        public int Compare(DesignerActionHeaderItem att1, DesignerActionHeaderItem att2)
        {
            Int32 cat1 = Convert.ToInt32(att1.Category);
            Int32 cat2 = Convert.ToInt32(att2.Category);

            return cat1.CompareTo(cat2);
        }
    }
}