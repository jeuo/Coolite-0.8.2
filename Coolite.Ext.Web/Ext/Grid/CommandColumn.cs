/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class CommandColumn : ColumnBase, ICustomConfigSerialization
    {
        /// <summary>
        /// (optional) Specify as false to prevent the user from hiding this column. Defaults to false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("(optional) Specify as false to prevent the user from hiding this column. Defaults to true.")]
        public override bool Hideable
        {
            get
            {
                object obj = this.ViewState["Hideable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Hideable"] = value;
            }
        }
        
        private GridCommandCollection commands;

        [ClientConfig("commands", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual GridCommandCollection Commands
        {
            get
            {
                if (this.commands == null)
                {
                    this.commands = new GridCommandCollection();
                }
                return this.commands;
            }
        }

        private GridCommandCollection groupCommands;

        [ClientConfig("groupCommands", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual GridCommandCollection GroupCommands
        {
            get
            {
                if (this.groupCommands == null)
                {
                    this.groupCommands = new GridCommandCollection();
                }
                return this.groupCommands;
            }
        }

        public string Serialize(Control owner)
        {
            if (this.ScriptManager != null)
            {
                RegisterIcons(this.Commands);
                RegisterIcons(this.GroupCommands);
            }

            return string.Concat("new Coolite.Ext.CommandColumn(", new ClientConfig().Serialize(this, true), ")");
        }

        private void RegisterIcons(GridCommandCollection commands)
        {
            foreach (GridCommandBase command in commands)
            {
                GridCommand cmd = command as GridCommand;
                if (cmd != null)
                {
                    if (cmd.Icon != Icon.None)
                    {
                        this.ScriptManager.RegisterIcon(cmd.Icon);
                    }
                    if (cmd.Menu.Items.Count > 0)
                    {
                        this.RegisterMenuIcons(cmd.Menu);
                    }
                }
            }
        }

        private void RegisterMenuIcons(CommandMenu menu)
        {
            foreach (MenuCommand menuCommand in menu.Items)
            {
                if (menuCommand.Icon != Icon.None)
                {
                    this.ScriptManager.RegisterIcon(menuCommand.Icon);
                }

                if (menuCommand.Menu.Items.Count > 0)
                {
                    this.RegisterMenuIcons(menuCommand.Menu);
                }
            }
        }

        private JFunction prepareToolbar;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction PrepareToolbar
        {
            get
            {
                if (this.prepareToolbar == null)
                {
                    this.prepareToolbar = new JFunction();
                    this.prepareToolbar.Args = new string[] { "grid", "toolbar", "rowIndex", "record" };
                }
                return this.prepareToolbar;
            }
        }

        private JFunction prepareGroupToolbar;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction PrepareGroupToolbar
        {
            get
            {
                if (this.prepareGroupToolbar == null)
                {
                    this.prepareGroupToolbar = new JFunction();
                    this.prepareGroupToolbar.Args = new string[] { "grid", "toolbar", "groupId", "records" };
                }
                return this.prepareGroupToolbar;
            }
        }
    }

    public abstract class GridCommandBase: StateManagedItem
    {
    }

    public class GridCommandCollection: StateManagedCollection<GridCommandBase>
    {
    }

    public class CommandSeparator : GridCommandBase
    {
        [ClientConfig("xtype")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        internal virtual string XType
        {
            get
            {
                return "tbseparator";
            }
        }
    }

    public class CommandFill : GridCommandBase
    {
        [ClientConfig("xtype")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        internal virtual string XType
        {
            get
            {
                return "tbfill";
            }
        }
    }

    public class CommandSpacer : GridCommandBase
    {
        [ClientConfig("xtype")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        internal virtual string XType
        {
            get
            {
                return "coolitetbspacer";
            }
        }

        public CommandSpacer()
        {
        }

        public CommandSpacer(Unit width)
        {
            this.Width = width;
        }

        public CommandSpacer(int width)
        {
            this.Width = Unit.Pixel(width);
        }

        [ClientConfig]
        [DefaultValue(typeof(Unit), "")]
        [NotifyParentProperty(true)]
        public Unit Width
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["Width"], Unit.Empty, "Width");
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }

        internal Unit UnitPixelTypeCheck(object obj, Unit defaultValue, string propertyName)
        {
            Unit temp = (obj == null) ? defaultValue : (Unit)obj;

            if (temp.Type != UnitType.Pixel)
            {
                throw new InvalidCastException(string.Format("The Unit Type for the toolbar spacer {0} property must be of Type 'Pixel'. Example: Unit.Pixel(150) or '150px'.", propertyName));
            }
            return temp;
        }
    }

    public class CommandText : GridCommandBase
    {
        [ClientConfig("xtype")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        internal virtual string XType
        {
            get
            {
                return "tbtext";
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }
    }

    public class GridCommand : GridCommandBase
    {
        [ClientConfig("xtype")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        internal virtual string XType
        {
            get
            {
                return "tbbutton";
            }
        }

        [ClientConfig("command")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string CommandName
        {
            get
            {
                return (string)this.ViewState["CommandName"] ?? "";
            }
            set
            {
                this.ViewState["CommandName"] = value;
            }
        }

        private SimpleToolTip toolTip;

        [ClientConfig("tooltip", JsonMode.Object)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SimpleToolTip ToolTip
        {
            get
            {
                if(this.toolTip == null)
                {
                    this.toolTip = new SimpleToolTip();
                }

                return this.toolTip;
            }
        }

        //[DefaultValue("")]
        //[NotifyParentProperty(true)]
        //public string QTipTitle
        //{
        //    get
        //    {
        //        return (string)this.ViewState["QTipTitle"] ?? "";
        //    }
        //    set
        //    {
        //        this.ViewState["QTipTitle"] = value;
        //    }
        //}

        //[DefaultValue("")]
        //[NotifyParentProperty(true)]
        //public string QTipText
        //{
        //    get
        //    {
        //        return (string)this.ViewState["QTipText"] ?? "";
        //    }
        //    set
        //    {
        //        this.ViewState["QTipText"] = value;
        //    }
        //}

        //[ClientConfig("tooltip", JsonMode.Raw)]
        //[DefaultValue("")]
        //internal string QTipProxy
        //{
        //    get
        //    {
        //        if(string.IsNullOrEmpty(this.QTipTitle) && string.IsNullOrEmpty(this.QTipText))
        //        {
        //            return "";
        //        }

        //        if (string.IsNullOrEmpty(this.QTipTitle))
        //        {
        //            return JSON.Serialize(this.QTipText);
        //        }

        //        if (string.IsNullOrEmpty(this.QTipText))
        //        {
        //            return JSON.Serialize(this.QTipTitle);
        //        }

        //        return string.Concat("{title:", JSON.Serialize(this.QTipTitle), ",text:", JSON.Serialize(this.QTipText),"}");
        //    }
        //}

        [DefaultValue(Icon.None)]
        [NotifyParentProperty(true)]
        public virtual Icon Icon
        {
            get
            {
                object obj = this.ViewState["Icon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["Icon"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string IconCls
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return string.Format("icon-{0}", this.Icon.ToString().ToLower());
                }
                return (string)this.ViewState["IconCls"] ?? "";
            }
            set
            {
                this.ViewState["IconCls"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable stand out by default (defaults to false).")]
        [ClientConfig]
        public virtual bool StandOut
        {
            get
            {
                object obj = this.ViewState["StandOut"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["StandOut"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Cls
        {
            get
            {
                if (this.StandOut)
                {
                    if (this.Icon != Icon.None || !string.IsNullOrEmpty(this.IconCls))
                    {
                        return (string.IsNullOrEmpty(this.Text) ? "x-btn-icon" : "x-btn-text-icon") + " x-btn-over";
                    }
                    else
                    {
                        return "x-btn-over";
                    }
                }

                return (string)this.ViewState["Cls"] ?? "";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string CtCls
        {
            get
            {
                return (string)this.ViewState["CtCls"] ?? "";
            }
            set
            {
                this.ViewState["CtCls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool Disabled
        {
            get
            {
                object obj = this.ViewState["Disabled"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Disabled"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string DisabledClass
        {
            get
            {
                return (string)this.ViewState["DisabledClass"] ?? "";
            }
            set
            {
                this.ViewState["DisabledClass"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool Hidden
        {
            get
            {
                object obj = this.ViewState["Hidden"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Hidden"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string OverCls
        {
            get
            {
                return (string)this.ViewState["OverCls"] ?? "";
            }
            set
            {
                this.ViewState["OverCls"] = value;
            }
        }

        private CommandMenu menu;

        [ClientConfig("menu", JsonMode.Object)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual CommandMenu Menu
        {
            get
            {
                if (this.menu == null)
                {
                    this.menu = new CommandMenu();
                }
                return this.menu;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(HideMode.Display)]
        [NotifyParentProperty(true)]
        [Description("How this component should be hidden. Supported values are 'visibility' (css visibility), 'offsets' (negative offset position) and 'display' (css display) - defaults to 'display'.")]
        public virtual HideMode HideMode
        {
            get
            {
                object obj = this.ViewState["HideMode"];
                return (obj == null) ? HideMode.Display : (HideMode)obj;
            }
            set
            {
                this.ViewState["HideMode"] = value;
            }
        }

        /// <summary>
        /// The minimum width for this button (used to give a set of buttons a common width).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "16")]
        [Description("The minimum width for this button (used to give a set of buttons a common width).")]
        public virtual Unit MinWidth
        {
            get
            {
                return UnitPixelTypeCheck(ViewState["MinWidth"], Unit.Pixel(16), "MinWidth");
            }
            set
            {
                this.ViewState["MinWidth"] = value;
            }
        }

        internal static Unit UnitPixelTypeCheck(object obj, Unit defaultValue, string propertyName)
        {
            Unit temp = (obj == null) ? defaultValue : (Unit)obj;

            if (temp.Type != UnitType.Pixel)
            {
                throw new InvalidCastException(string.Format("The Unit Type for the GridCommand {0} property must be of Type 'Pixel'. Example: Unit.Pixel(150) or '150px'.", propertyName));
            }

            return temp;
        }

    }

    public class SplitCommand : GridCommand
    {
        [ClientConfig("xtype")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        internal override string XType
        {
            get
            {
                return "tbsplit";
            }
        }
    }

    public class MenuCommand : StateManagedItem
    {
        [ClientConfig("command")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string CommandName
        {
            get
            {
                return (string)this.ViewState["CommandName"] ?? "";
            }
            set
            {
                this.ViewState["CommandName"] = value;
            }
        }
        
        [DefaultValue(Icon.None)]
        [NotifyParentProperty(true)]
        public virtual Icon Icon
        {
            get
            {
                object obj = this.ViewState["Icon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["Icon"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string IconCls
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return string.Format("icon-{0}", this.Icon.ToString().ToLower());
                }
                return (string)this.ViewState["IconCls"] ?? "";
            }
            set
            {
                this.ViewState["IconCls"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Cls
        {
            get
            {
                return (string)this.ViewState["Cls"] ?? "";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string CtCls
        {
            get
            {
                return (string)this.ViewState["CtCls"] ?? "";
            }
            set
            {
                this.ViewState["CtCls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool Disabled
        {
            get
            {
                object obj = this.ViewState["Disabled"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Disabled"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string DisabledClass
        {
            get
            {
                return (string)this.ViewState["DisabledClass"] ?? "";
            }
            set
            {
                this.ViewState["DisabledClass"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool Hidden
        {
            get
            {
                object obj = this.ViewState["Hidden"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Hidden"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string OverCls
        {
            get
            {
                return (string)this.ViewState["OverCls"] ?? "";
            }
            set
            {
                this.ViewState["OverCls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string ItemCls
        {
            get
            {
                return (string)this.ViewState["ItemCls"] ?? "";
            }
            set
            {
                this.ViewState["ItemCls"] = value;
            }
        }

        private CommandMenu menu;

        [ClientConfig("menu", JsonMode.Object)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual CommandMenu Menu
        {
            get
            {
                if (this.menu == null)
                {
                    this.menu = new CommandMenu();
                }
                return this.menu;
            }
        }
    }

    public class CommandMenu : StateManagedItem
    {
        private MenuCommandCollection items;

        [ClientConfig("items", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual MenuCommandCollection Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new MenuCommandCollection();
                }
                return this.items;
            }
        }
    }

    public class MenuCommandCollection : StateManagedCollection<MenuCommand>
    {
    }
}