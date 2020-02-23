/**
 * @version: 1.0.0
 * @author: Coolite Inc. http://www.coolite.com/
 * @date: 2008-05-26
 * @copyright: Copyright (c) 2006-2008, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license: See license.txt and http://www.coolite.com/license/. 
 * @website: http://www.coolite.com/
 */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Coolite.Utilities
{
    public static class ControlUtils
    {
        /*  FindControl
            -----------------------------------------------------------------------------------------------*/

        public static bool HasControls(Control control)
        {
            return control != null && control.Visible && control.Controls.Count > 0;
        }

        public static Control FindControl(Control seed, string id)
        {
            return ControlUtils.FindControl(seed, id, true, null);
        }

        public static Control FindControl(Control seed, string id, bool traverse)
        {
            return ControlUtils.FindControl(seed, id, traverse, null);
        }

        private static Control FindControl(Control seed, string id, bool traverse, Control branch)
        {
            if (seed == null || string.IsNullOrEmpty(id))
            {
                return null;
            }

            Control found = null;
            try
            {
                found = seed.FindControl(id);

                if (found != null)
                {
                    return found;
                }
            }
            catch(HttpException)
            {
                /// TODO: Notes regarding the FindControl Method.
                
                // We need to call the native .FindControl because .EnsureChildControls() 
                // is method and can only be called internally by a Control.

                // If protected .FindControl actually finds the control, we just return the found control.
                
                // There is a bug in Visual Studio Design-Mode which causes protected/native 
                // .FindControl to think it's found two controls with the same ID, although only 
                // one exists. The conflict appears to be coming from a cached version of the assembly.
                // Might be related to the following Microsoft KB834608 article, see
                // http://support.microsoft.com/default.aspx/kb/834608

                // start checking .ID property
            }

            Control root = (seed is INamingContainer) ? seed : seed.NamingContainer;

            string exclude = (branch != null) ? branch.ID ?? "" : "";

            foreach (Control control in root.Controls)
            {
                if (!exclude.Equals(control.ID) && ControlUtils.HasControls(control))
                {
                    found = ControlUtils.FindChildControl(control, id);
                }

                if (found != null)
                {
                    break;
                }
            }

            if (traverse && found == null)
            {
                found = ControlUtils.FindControl(root.NamingContainer, id, traverse, root);
            }

            return found;
        }

        public static T FindControl<T>(Control seed, string id) where T : Control
        {
            return ControlUtils.FindControl<T>(seed, id, true, null);
        }

        public static T FindControl<T>(Control seed, string id, bool traverse) where T : Control
        {
            return ControlUtils.FindControl<T>(seed, id, traverse, null);
        }

        public static T FindControl<T>(Control seed, string id, bool traverse, Control branch) where T : Control
        {
            Control c = ControlUtils.FindControl(seed, id, traverse, branch);

            if (c != null && !ReflectionUtils.IsTypeOf(c, typeof(T)))
            {
                throw new InvalidCastException(string.Format("The Control ID ('{0}') was found, but it was not a type of {1}. The found Control was a type of {2}.", id, typeof(T).ToString(), c.GetType().ToString()));
            }

            return c as T;
        }

        public static T FindControl<T>(Control seed) where T : Control
        {
            return ControlUtils.FindControl(seed, typeof(T)) as T;
        }

        public static T FindControl<T>(Control seed, bool shallow) where T : Control
        {
            return ControlUtils.FindControl(seed, typeof(T), shallow) as T;
        }

        public static Control FindControl(Control seed, Type type)
        {
            return FindControl(seed, type, false);
        }

        public static Control FindControl(Control seed, Type type, bool shallow)
        {
            return FindControlByTypeName(seed, type.FullName, shallow, true, null);
        }

        public static Control FindControlByTypeName(Control seed, string typeFullName)
        {
            return FindControlByTypeName(seed, typeFullName, false, true, null);
        }

        private static Control FindControlByTypeName(Control seed, string typeFullName, bool shallow, bool traverse, Control branch)
        {
            if (seed == null || string.IsNullOrEmpty(typeFullName))
            {
                return null;
            }

            Control root = (seed is INamingContainer) ? seed : seed.NamingContainer;

            if (ReflectionUtils.IsTypeOf(root, typeFullName, shallow))
            {
                return root;
            }

            Control found = null;
            string exclude = (branch != null) ? branch.ID ?? "" : "";

            foreach (Control control in root.Controls)
            {
                if (!exclude.Equals(control.ID))
                {
                    if (ReflectionUtils.IsTypeOf(control, typeFullName, shallow))
                    {
                        found = control;
                    }
                    else if (ControlUtils.HasControls(control))
                    {
                        found = ControlUtils.FindChildControl(control, typeFullName, shallow);
                    }
                    
                    if (found != null)
                    {
                        break;
                    }
                }
            }

            if (traverse && found == null)
            {
                found = ControlUtils.FindControlByTypeName(root.NamingContainer, typeFullName, shallow, traverse, root);
            }

            return found;
        }


        /*  FindChildControl
            -----------------------------------------------------------------------------------------------*/

        public static Control FindChildControl(Control seed, string id) 
        {
            if (seed == null || string.IsNullOrEmpty(id))
            {
                return null;
            }

            Control found = null;

            try
            {
                found = seed.FindControl(id);

                if (found != null)
                {
                    return found;
                }
            }
            catch (HttpException)
            {

            }

            //Control root = (seed is INamingContainer) ? seed : seed.NamingContainer;

            foreach (Control control in seed.Controls)
            {
                if (ControlUtils.HasControls(control))
                {
                    found = ControlUtils.FindChildControl(control, id);
                }

                if (found != null)
                {
                    break;
                }
            }

            return found;
        }

        public static Control FindChildControl(Control seed, string typeFullName, bool shallow)
        {
            if (seed == null || string.IsNullOrEmpty(typeFullName))
            {
                return null;
            }

            Control found = null;

            foreach (Control control in seed.Controls)
            {
                if (ReflectionUtils.IsTypeOf(control, typeFullName, shallow))
                {
                    found = control;
                }
                else if (ControlUtils.HasControls(control))
                {
                    found = ControlUtils.FindChildControl(control, typeFullName, shallow);
                }

                if (found != null)
                {
                    break;
                }
            }

            return found;
        }


        /*  FindControls
            -----------------------------------------------------------------------------------------------*/

        public static List<T> FindControls<T>(Control seed) where T : Control
        {
            return FindControls<T>(seed, false);
        }

        public static List<T> FindControls<T>(Control seed, bool shallow) where T : Control
        {
            if (seed == null)
            {
                return null;
            }

            List<T> foundControls = new List<T>();

            foreach (Control control in seed.Controls)
            {
                if (ReflectionUtils.IsTypeOf(control, typeof(T), shallow))
                {
                    foundControls.Add(control as T);
                }

                if (ControlUtils.HasControls(control))
                {
                    foundControls.AddRange(ControlUtils.FindControls<T>(control, shallow));
                }
            }

            return foundControls;
        }

        public static List<T> FindControls<T>(Control seed, string typeFullName, bool shallow) where T : Control
        {
            if (seed == null || string.IsNullOrEmpty(typeFullName))
            {
                return null;
            }

            List<T> foundControls = new List<T>();

            foreach (Control control in seed.Controls)
            {
                if (ReflectionUtils.IsTypeOf(control, typeFullName, shallow))
                {
                    foundControls.Add(control as T);
                }

                if (ControlUtils.HasControls(control))
                {
                    foundControls.AddRange(ControlUtils.FindControls<T>(control, typeFullName, shallow));
                }
            }

            return foundControls;
        }


        /*  Misc
            -----------------------------------------------------------------------------------------------*/

        public static bool IsChildOfParent(Control parent, Control child)
        {
            if (parent != null && child != null && !parent.UniqueID.Equals(child.UniqueID))
            {
                for (Control p = child.Parent; p != null; p = p.Parent)
                {
                    if (p.UniqueID == parent.UniqueID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }  
        

        /*  Misc
            -----------------------------------------------------------------------------------------------*/

        public static Control FindControlByClientID(Control seed, string clientID, bool traverse, Control branch)
        {
            if (seed == null || string.IsNullOrEmpty(clientID))
            {
                return null;
            }

            Control parent = (seed is INamingContainer) ? seed : seed.NamingContainer;

            if (clientID.Equals(parent.ClientID ?? ""))
            {
                return parent;
            }

            Control found = null;
            string exclude = (branch != null) ? branch.ClientID ?? "" : "";
            string tempID = "";
            string tempClientID = "";

            List<Control> waiting = new List<Control>();

            foreach (Control c in parent.Controls)
            {
                tempID = c.ID ?? "";
                tempClientID = c.ClientID ?? "";

                if (clientID.Equals(tempID) || clientID.Equals(tempClientID))
                {
                    found = c;
                }
                else if (ControlUtils.HasControls(c) && !exclude.Equals(tempClientID))
                {
                    found = ControlUtils.FindChildControlByClientID(c, clientID);
                }

                if (found != null)
                {
                    break;
                }
            }

            if (traverse && found == null)
            {
                found = ControlUtils.FindControlByClientID(parent.NamingContainer, clientID, true, parent);
            }

            return found;
        }

        public static Control FindChildControlByClientID(Control seed, string clientID)
        {
            if (seed == null || string.IsNullOrEmpty(clientID))
            {
                return null;
            }

            Control found = null;
            string tempID = "";
            string tempClientID = "";

            foreach (Control control in seed.Controls)
            {
                tempID = control.ID ?? "";
                tempClientID = control.ClientID ?? "";

                if (clientID.Equals(tempID) || clientID.Equals(tempClientID))
                {
                    found = control;
                }
                else if (ControlUtils.HasControls(control))
                {
                    found = ControlUtils.FindChildControlByClientID(control, clientID);
                }

                if (found != null)
                {
                    break;
                }
            }

            return found;
        }
    }
}