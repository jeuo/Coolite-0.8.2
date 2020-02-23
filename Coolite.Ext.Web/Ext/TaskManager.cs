/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Provides the ability to execute one or more arbitrary tasks in a multithreaded manner.
    /// </summary>
    [InstanceOf(ClassName = "Coolite.Ext.TaskManager")]
    [ToolboxData("<{0}:TaskManager runat=\"server\"></{0}:TaskManager>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TaskManager), "Build.Resources.ToolboxIcons.TaskManager.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("Provides the ability to execute one or more arbitrary tasks in a multithreaded manner.")]
    public class TaskManager : Observable, IAjaxPostBackEventHandler
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(10)]
        [Description("The minimum precision in milliseconds supported by this TaskRunner instance (defaults to 10)")]
        public virtual int Interval
        {
            get
            {
                object obj = this.ViewState["Interval"];
                return (obj == null) ? 10 : (int)obj;
            }
            set
            {
                this.ViewState["Interval"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(50)]
        [Description("The start delay in milliseconds for autorun tasks")]
        public virtual int AutoRunDelay
        {
            get
            {
                object obj = this.ViewState["AutoRunDelay"];
                return (obj == null) ? 50 : (int)obj;
            }
            set
            {
                this.ViewState["AutoRunDelay"] = value;
            }
        }

        private TaskCollection tasks;

        [ClientConfig("tasksConfig", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Tasks")]
        public virtual TaskCollection Tasks
        {
            get
            {
                if (this.tasks == null)
                {
                    this.tasks = new TaskCollection();
                    this.tasks.AfterItemAdd += this.AfterItemAdd;
                }

                return this.tasks;
            }
        }

        protected virtual void AfterItemAdd(Task task)
        {
            task.Owner = this;
            task.Listeners.Update.Owner = this;
            task.AjaxEvents.Update.Owner = this;
        }

        void IAjaxPostBackEventHandler.RaiseAjaxPostBackEvent(string eventArgument, ParameterCollection extraParams)
        {
            string action = eventArgument;

            foreach (Task task in this.Tasks)
            {
                if (!task.AjaxEvents.Update.IsDefault && task.AjaxEvents.Update.HandlerName == action)
                {
                    task.AjaxEvents.Update.OnEvent(new AjaxEventArgs(extraParams));
                }
            }
        }

        public void StartAll()
        {
            this.AddScript("{0}.startAll();", this.ClientID);
        }

        public void StopAll()
        {
            this.AddScript("{0}.stopAll();", this.ClientID);
        }

        public void StartTask(int index)
        {
            this.AddScript("{0}.startTask({1});", this.ClientID, index);
        }

        public void StopTask(int index)
        {
            this.AddScript("{0}.stopTask({1});", this.ClientID, index);
        }

        public void StartTask(string name)
        {
            this.AddScript("{0}.startTask({1});", this.ClientID, JSON.Serialize(name));
        }

        public void StopTask(string name)
        {
            this.AddScript("{0}.stopTask({1});", this.ClientID, JSON.Serialize(name));
        }
    }

    public class  TaskCollection : StateManagedCollection<Task> { }

    public class Task : StateManagedItem
    {
        [ClientConfig("id")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("(optional) The TaskID.")]
        public virtual string TaskID
        {
            get
            {
                return (string)this.ViewState["TaskID"] ?? "";
            }
            set
            {
                this.ViewState["TaskID"] = value;
            }
        }
        
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to auto run task (defaults to false).")]
        public virtual bool AutoRun
        {
            get
            {
                object obj = this.ViewState["AutoRun"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoRun"] = value;
            }
        }
        
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(1000)]
        [Description("The frequency in milliseconds with which the task should be executed (defaults to 1000)")]
        public virtual int Interval
        {
            get
            {
                object obj = this.ViewState["Interval"];
                return (obj == null) ? 1000 : (int)obj;
            }
            set
            {
                this.ViewState["Interval"] = value;
            }
        }

        [ClientConfig(typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [DefaultValue(null)]
        [Description("(optional) An array of arguments to be passed to the function specified by run")]
        public virtual string[] Args
        {
            get
            {
                object obj = this.ViewState["Args"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["Args"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue("this")]
        [NotifyParentProperty(true)]
        [Description("(optional) The scope in which to execute the run function.")]
        public virtual string Scope
        {
            get
            {
                return (string)this.ViewState["Scope"] ?? "this";
            }
            set
            {
                this.ViewState["Scope"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("(optional) The length of time in milliseconds to execute the task before stopping automatically (defaults to indefinite).")]
        public virtual int Duration
        {
            get
            {
                object obj = this.ViewState["Duration"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Duration"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("(optional) The number of times to execute the task before stopping automatically (defaults to indefinite).")]
        public virtual int Repeat
        {
            get
            {
                object obj = this.ViewState["Repeat"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Repeat"] = value;
            }
        }

        [DefaultValue("")]
        [ClientConfig("serverRun", JsonMode.Raw)]
        internal string AjaxEventProxy
        {
            get
            {
                if (!this.AjaxEvents.Update.IsDefault)
                {
                    string configObject = new ClientConfig().SerializeInternal(this.AjaxEvents.Update, this.AjaxEvents.Update.Owner);

                    StringBuilder cfgObj = new StringBuilder(configObject.Length + 64);

                    cfgObj.Append(configObject);
                    cfgObj.Remove(cfgObj.Length - 1, 1);
                    cfgObj.AppendFormat("{0}eventType: \"{1}\"", configObject.Length > 2 ? "," : "", AjaxRequestType.PostBack.ToString().ToLower());

                    cfgObj.AppendFormat(",action:\"{0}\"", this.AjaxEvents.Update.HandlerName);

                    cfgObj.Append("}");

                    return new JFunction(string.Concat("return ", cfgObj.ToString(),";")).ToString();
                }
                
                return "";
            }
        }

        [DefaultValue("")]
        [ClientConfig("clientRun", JsonMode.Raw)]
        internal string ListenerProxy
        {
            get
            {
                if (!this.Listeners.Update.IsDefault)
                {
                    return this.Listeners.Update.ToString();
                }

                return "";
            }
        }


        [DefaultValue("")]
        [ClientConfig("onstart", typeof(FunctionJsonConverter))]
        [NotifyParentProperty(true)]
        public string OnStart
        {
            get
            {
                return (string)this.ViewState["OnStart"] ?? "";
            }
            set
            {
                this.ViewState["OnStart"] = value;
            }
        }

        [DefaultValue("")]
        [ClientConfig("onstop", typeof(FunctionJsonConverter))]
        [NotifyParentProperty(true)]
        public string OnStop
        {
            get
            {
                return (string)this.ViewState["OnStop"] ?? "";
            }
            set
            {
                this.ViewState["OnStop"] = value;
            }
        }

        private TaskListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        [DefaultValue("")]
        public TaskListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TaskListeners();

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private TaskAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side AjaxEvent Handlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side AjaxEventHandlers")]
        [ViewStateMember]
        public TaskAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new TaskAjaxEvents();

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }
    }

    public class TaskListeners: StateManagedItem
    {
        private SimpleListener update;

        [ClientConfig("clientRun", JsonMode.Raw)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The function to execute each time the task is run. The function will be called at each interval.")]
        public virtual SimpleListener Update
        {
            get
            {
                if (this.update == null)
                {
                    this.update = new SimpleListener();
                }
                return this.update;
            }
        }

        public override object SaveViewState()
        {
            return this.Update.SaveViewState();
        }

        public override void LoadViewState(object state)
        {
            this.Update.LoadViewState(state);
        }
    }

    public class TaskAjaxEvents: StateManagedItem
    {
        private ComponentAjaxEvent update;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The function to execute each time the task is run. The function will be called at each interval.")]
        public virtual ComponentAjaxEvent Update
        {
            get
            {
                if (this.update == null)
                {
                    this.update = new ComponentAjaxEvent();
                }
                return this.update;
            }
        }

        public override object SaveViewState()
        {
            return this.Update.SaveViewState();
        }

        public override void LoadViewState(object state)
        {
            this.Update.LoadViewState(state);
        }
    }
}