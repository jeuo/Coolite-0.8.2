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
    /// <summary>
    /// An individual column's config object defines the header string, the Record field
    /// the column draws its data from, an optional rendering function to provide customized
    /// data formatting, and the ability to apply a CSS class to all cells in a column
    /// through its id config option.
    /// </summary>
    public class Column : ColumnBase, ICustomConfigSerialization
    {
        [ClientConfig]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        public virtual bool RightCommandAlign
        {
            get
            {
                object obj = this.ViewState["RightCommandAlign"];
                return obj == null ? true : (bool)obj;
            }
            set
            {
                this.ViewState["RightCommandAlign"] = value;
            }
        }
        
        private ImageCommandCollection commands;

        [ClientConfig("commands", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual ImageCommandCollection Commands
        {
            get
            {
                if (this.commands == null)
                {
                    this.commands = new ImageCommandCollection();
                }
                return this.commands;
            }
        }

        [DefaultValue(false)]
        [ClientConfig]
        protected virtual bool IsCellCommand
        {
            get
            {
                return this.Commands.Count > 0;
            }
        }

        private JFunction prepareCommand;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction PrepareCommand
        {
            get
            {
                if (this.prepareCommand == null)
                {
                    this.prepareCommand = new JFunction();
                    this.prepareCommand.Args = new string[] { "grid", "command", "record", "row", "col", "value" };
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
        public virtual JFunction PrepareCommands
        {
            get
            {
                if (this.prepareCommands == null)
                {
                    this.prepareCommands = new JFunction();
                    this.prepareCommands.Args = new string[] { "grid", "commands", "record", "row", "col", "value" };
                }
                return this.prepareCommands;
            }
        }

        public virtual string Serialize(Control owner)
        {
            if (this.ScriptManager != null)
            {
                foreach (ImageCommand command in this.Commands)
                {
                    if (command.Icon != Icon.None)
                    {
                        this.ScriptManager.RegisterIcon(command.Icon);
                    }
                }
            }

            return new ClientConfig().Serialize(this, true);
        }
    }
}