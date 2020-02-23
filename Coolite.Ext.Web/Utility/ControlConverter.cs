/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Coolite.Ext.Web
{
    public class ControlConverter : StringConverter
    {
        public ControlConverter() { }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if ((context == null) || (context.Container == null))
            {
                return null;
            }
            object[] controls = this.GetControls(context.Container);
            if (controls != null)
            {
                return new TypeConverter.StandardValuesCollection(controls);
            }
            return null;
        }

        private object[] GetControls(IContainer container)
        {
            ComponentCollection components = container.Components;
            ArrayList controls = new ArrayList();
            foreach (IComponent component in components)
            {
                if (component is System.Web.UI.Control)
                {
                    Control c = (Control)component;

                    if (!string.IsNullOrEmpty(c.ID) && this.CheckType(c))
                    {
                        controls.Add(string.Copy(c.ID));
                    }
                }
            }
            controls.Sort(Comparer.Default);
            return controls.ToArray();
        }

        private bool CheckType(Control c)
        {
            return (!this.Types.Contains(c.GetType()));
        }

        private List<Type> types;
        private List<Type> Types
        {
            get
            {
                if (this.types == null)
                {
                    this.types = new List<Type>();
                    this.types.Add(typeof(HtmlForm));
                    this.types.Add(typeof(ScriptManager));
                    this.types.Add(typeof(HtmlInputHidden));
                    this.types.Add(typeof(Hidden));
                    this.types.Add(typeof(Page));
                }
                return this.types;
            }
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}