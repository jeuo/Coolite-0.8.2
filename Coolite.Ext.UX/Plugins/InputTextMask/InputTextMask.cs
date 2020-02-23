/******** 
 * This file is part of the Coolite UX Toolkit.

 * The Coolite UX Toolkit is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * The Coolite UX Toolkit is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public License
 * along with the Coolite Toolkit.  If not, see <http://www.gnu.org/licenses/>.
 */

/*
* @version:		0.6.0 Preview-1
* @author:		Coolite Inc. http://www.coolite.com/
* @date:		2008-08-05
* @copyright:	Copyright (c) 2006-2008, Coolite Inc, or as noted within each 
* 				applicable file LICENSE.txt file
* @license:		LGPL 3.0 License
* @website:		http://www.coolite.com/
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Coolite.Ext.Web;

[assembly: WebResource("Coolite.Ext.UX.Plugins.InputTextMask.resources.InputTextMask.js", "text/javascript")]

namespace Coolite.Ext.UX
{
    /** It creates a new input text mask.
     * @class InputTextMask is a plugin for Ext.form.TextField, used to add an input mask to the field.
     * Mask Individual Character Usage:
     * 
     * 9 - designates only numeric values
     * L - designates only uppercase letter values
     * l - designates only lowercase letter values
     * A - designates only alphanumeric values
     * X - denotes that a custom client script regular expression is specified
     *
     * All other characters are assumed to be "special" characters used to mask the input component.
     * Example 1:
     *    (999)999-9999 only numeric values can be entered where the the character
     *    position value is 9. Parenthesis and dash are non-editable/mask characters.
     * Example 2:
     *    99L-ll-X[^A-C]X only numeric values for the first two characters,
     *    uppercase values for the third character, lowercase letters for the
     *    fifth/sixth characters, and the last character X[^A-C]X together counts
     *    as the eighth character regular expression that would allow all characters
     *    but "A", "B", and "C". Dashes outside the regular expression are non-editable/mask characters.
    */

    [InstanceOf(ClassName = "Ext.ux.netbox.InputTextMask")]
    [ClientScript(Type = typeof(InputTextMask), WebResource = "Coolite.Ext.UX.Plugins.InputTextMask.resources.InputTextMask.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.UX.InputTextMask), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:InputTextMask runat=\"server\" />")]
    [Description("InputTextMask plugin used for mask/regexp operations")]
    public class InputTextMask : Plugin
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The InputTextMask")]
        public string Mask
        {
            get
            {
                object obj = this.ViewState["Mask"];
                return obj == null ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Mask"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to clear the mask when the field blurs and the text is invalid. Optional, default is true")]
        public bool ClearWhenInvalid
        {
            get
            {
                object obj = this.ViewState["ClearWhenInvalid"];
                return obj == null ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ClearWhenInvalid"] = value;
            }
        }
    }
}
