/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class CheckboxConverter : StringConverter
    {
        public CheckboxConverter() { }

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
                if (component is System.Web.UI.WebControls.CheckBox && !component.GetType().Equals(typeof(Coolite.Ext.Web.Checkbox)))
                {
                    Control c = (Control)component;
                    if (!string.IsNullOrEmpty(c.ID))
                    {
                        controls.Add(string.Copy(c.ID));
                    }
                }
            }
            controls.Sort(Comparer.Default);
            controls.Insert(0, "Default");
            return controls.ToArray();
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