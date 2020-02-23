/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;
using System.Web.UI;
using System;

namespace Coolite.Ext.Web
{
    public class ScriptManagerActionList : WebControlActionList
    {
        public ScriptManagerActionList(IComponent component) : base(component) { }

        public bool HideInDesign
        {
            get
            {
                return ((ScriptManager)this.Control).HideInDesign;

            }
            set
            {
                this.GetPropertyByName("HideInDesign").SetValue(this.Control, value);
            }
        }

        public bool CleanResourceUrl
        {
            get
            {
                return ((ScriptManager)this.Control).CleanResourceUrl;

            }
            set
            {
                this.GetPropertyByName("CleanResourceUrl").SetValue(this.Control, value);
            }
        }

        public ResourceLocationType RenderScripts
        {
            get
            {
                return ((ScriptManager)this.Control).RenderScripts;

            }
            set
            {
                this.GetPropertyByName("RenderScripts").SetValue(this.Control, value);
            }
        }

        public ResourceLocationType RenderStyles
        {
            get
            {
                return ((ScriptManager)this.Control).RenderStyles;

            }
            set
            {
                this.GetPropertyByName("RenderStyles").SetValue(this.Control, value);
            }
        }

        public string ResourcePath
        {
            get
            {
                return ((ScriptManager)this.Control).ResourcePath;

            }
            set
            {
                this.GetPropertyByName("ResourcePath").SetValue(this.Control, value);
            }
        }

        public ScriptMode ScriptMode
        {
            get
            {
                return ((ScriptManager)this.Control).ScriptMode;

            }
            set
            {
                this.GetPropertyByName("ScriptMode").SetValue(this.Control, value);
            }
        }

        public bool SourceFormatting
        {
            get
            {
                return ((ScriptManager)this.Control).SourceFormatting;

            }
            set
            {
                this.GetPropertyByName("SourceFormatting").SetValue(this.Control, value);
            }
        }

        public Theme Theme
        {
            get
            {
                return ((ScriptManager)this.Control).Theme;

            }
            set
            {
                this.GetPropertyByName("Theme").SetValue(this.Control, value);
            }
        }

        public ScriptAdapter ScriptAdapter
        {
            get
            {
                return ((ScriptManager)this.Control).ScriptAdapter;

            }
            set
            {
                this.GetPropertyByName("ScriptAdapter").SetValue(this.Control, value);
            }
        }

        public StateProvider StateProvider
        {
            get
            {
                return ((ScriptManager)this.Control).StateProvider;

            }
            set
            {
                this.GetPropertyByName("StateProvider").SetValue(this.Control, value);
            }
        }

        public bool QuickTips
        {
            get
            {
                return ((ScriptManager)this.Control).QuickTips;

            }
            set
            {
                this.GetPropertyByName("QuickTips").SetValue(this.Control, value);
            }
        }

        public bool RemoveViewState
        {
            get
            {
                return ((ScriptManager)this.Control).RemoveViewState;

            }
            set
            {
                this.GetPropertyByName("RemoveViewState").SetValue(this.Control, value);
            }
        }

        public InitScriptMode InitScriptMode
        {
            get
            {
                return ((ScriptManager)this.Control).InitScriptMode;

            }
            set
            {
                this.GetPropertyByName("InitScriptMode").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("HideInDesign", "Hide", string.Empty, "Hide the Coolite ScriptManager during Design-Time editing"));

            this.AddPropertyItem(new DesignerActionPropertyItem("Theme", "Theme", "500", "Sets the current Theme"));

            this.AddPropertyItem(new DesignerActionPropertyItem("CleanResourceUrl", "CleanResourceUrl", "500", "Specifies whether the Coolite ScriptManager will output 'clean' Url's when linking to Embedded Resources"));
            this.AddPropertyItem(new DesignerActionPropertyItem("QuickTips", "QuickTips", "500", "Enable QuickTips"));
            this.AddPropertyItem(new DesignerActionPropertyItem("RemoveViewState", "RemoveViewState", "500", "Remove ViewState from the page rendering"));
            //this.AddPropertyItem(new DesignerActionPropertyItem("SourceFormatting", "SourceFormatting", "500", "Specifies whether the scripts rendered to the page should be formatted"));

            this.AddPropertyItem(new DesignerActionPropertyItem("ScriptMode", "ScriptMode", "500", "Specifies whether the Scripts should be rendered in Release or Debug mode"));

            this.AddPropertyItem(new DesignerActionPropertyItem("ScriptAdapter", "ScriptAdapter", "500", "Gets the current script Adapter"));
            this.AddPropertyItem(new DesignerActionPropertyItem("InitScriptMode", "InitScriptMode", "500", "Render config script into the page or as separate JavaScript file"));
            //this.AddPropertyItem(new DesignerActionPropertyItem("StateProvider", "StateProvider", "500", "Specifies the state provider"));

            this.AddPropertyItem(new DesignerActionPropertyItem("RenderStyles", "RenderStyles", "500", "Determines how or if the required Styles should be rendered to the Page"));
            this.AddPropertyItem(new DesignerActionPropertyItem("RenderScripts", "RenderScripts", "500", "Determines how or if the required Scripts should be rendered to the Page"));

            DesignerActionPropertyItem resourcePath = new DesignerActionPropertyItem("ResourcePath", "ResourcePath", "500", "Gets the prefix of the Url path to the base ~/Coolite/ folder containing the resources files for this project");

            if (this.RenderScripts == ResourceLocationType.File || this.RenderStyles == ResourceLocationType.File)
            {
                this.AddPropertyItem(resourcePath);
            }
            else
            {
                this.RemovePropertyItem(resourcePath);
            }

            return base.GetSortedActionItems();
        }
    }
}