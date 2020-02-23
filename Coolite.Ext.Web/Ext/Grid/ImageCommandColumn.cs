/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class ImageCommandColumn : Column
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

        [ClientConfig(JsonMode.Ignore)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override bool RightCommandAlign
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
        
        private GroupImageCommandCollection groupCommands;

        [ClientConfig("groupCommands", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual GroupImageCommandCollection GroupCommands
        {
            get
            {
                if (this.groupCommands == null)
                {
                    this.groupCommands = new GroupImageCommandCollection();
                }
                return this.groupCommands;
            }
        }

        [DefaultValue(false)]
        [ClientConfig(JsonMode.Ignore)]
        protected override bool IsCellCommand
        {
            get
            {
                return false;
            }
        }

        public override string Serialize(Control owner)
        {
            base.Serialize(owner);
            if (this.ScriptManager != null)
            {
                foreach (GroupImageCommand command in this.GroupCommands)
                {
                    if (command.Icon != Icon.None)
                    {
                        this.ScriptManager.RegisterIcon(command.Icon);
                    }
                }
            }

            return string.Concat("new Coolite.Ext.ImageCommandColumn(", new ClientConfig().Serialize(this, true), ")");
        }

        private JFunction prepareGroupCommand;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction PrepareGroupCommand
        {
            get
            {
                if (this.prepareGroupCommand == null)
                {
                    this.prepareGroupCommand = new JFunction();
                    this.prepareGroupCommand.Args = new string[] { "grid", "command", "groupId", "group"};
                }
                return this.prepareGroupCommand;
            }
        }

        private JFunction prepareGroupCommands;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction PrepareGroupCommands
        {
            get
            {
                if (this.prepareGroupCommands == null)
                {
                    this.prepareGroupCommands = new JFunction();
                    this.prepareGroupCommands.Args = new string[] { "grid", "commands", "groupId", "group" };
                }
                return this.prepareGroupCommands;
            }
        }

        private JFunction prepareCommand;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override JFunction PrepareCommand
        {
            get
            {
                if (this.prepareCommand == null)
                {
                    this.prepareCommand = new JFunction();
                    this.prepareCommand.Args = new string[] { "grid", "command", "record", "row" };
                }
                return this.prepareCommand;
            }
        }

        private JFunction prepareCommands;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override JFunction PrepareCommands
        {
            get
            {
                if (this.prepareCommands == null)
                {
                    this.prepareCommands = new JFunction();
                    this.prepareCommands.Args = new string[] { "grid", "commands", "record", "row" };
                }
                return this.prepareCommands;
            }
        }
    }
}