/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class ScriptManagerDesigner : WebControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (this.Control is ScriptManager)
            {
                ResourceManager.CheckConfiguration(component.Site);
            }
        }

        public override string GetDesignTimeHtml()
        {
            if (((ScriptManager)this.Control).HideInDesign)
            {
                return base.GetDesignTimeHtml();
            }

            return base.GetDesignTimeHtml() + base.CreatePlaceHolderDesignTimeHtml(this.Message);
        }

        private string Message
        {
            get
            {
                ScriptManager sm = (ScriptManager)this.Control;
                string url = this.GetWebResourceUrl("Coolite.Ext.Web.Build.Resources.Images.Coolite_Logo.gif");
                string template = 
                @"<table style=""margin: 8px;"">
                    <tr>
                        <th style=""font-weight:bold;"" width=""125"">Theme</th>
                        <td width=""100"">{0}</td>
                    </tr>
                    <tr>
                        <th style=""font-weight:bold;"">Adapter</th>
                        <td>{1}</td>
                    </tr>
                    <tr>
                        <th style=""font-weight:bold;"">Script Mode</th>
                        <td>{2}</td>
                    </tr>
                    <tr>
                        <td style=""text-alight:right;"" colspan=""2""><img src=""{3}"" /></td>
                    </tr>
                </table>";
                return string.Format(template, sm.Theme.ToString(), sm.ScriptAdapter.ToString(), sm.ScriptMode.ToString(), url);
            }
        }

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new ScriptManagerActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}