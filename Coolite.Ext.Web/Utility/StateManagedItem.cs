/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public abstract class StateManagedItem : IStateManager
    {
        protected StateManagedItem(Control owner) : this()
        {
            this.owner = owner;
        }

        protected StateManagedItem()
        {
            Page page = null;
            if (HttpContext.Current != null && HttpContext.Current.CurrentHandler is Page)
            {
                page = (Page)HttpContext.Current.CurrentHandler;
            }

            if (page != null)
            {
                page.PreRenderComplete += DataBindPoint;
            }
        }

        void DataBindPoint(object sender, EventArgs e)
        {
            WebControl owner = this.Owner as WebControl;
            if (this.AutoDataBind || (owner != null && owner.AutoDataBind))
            {
                this.DataBind();
            }
        }

        private bool autoDataBind;

        [DefaultValue(false)]
        public bool AutoDataBind
        {
            get
            {
                return this.autoDataBind;
            }
            set
            {
                this.autoDataBind = value;
            }
        }

        ScriptManager sm;
        public ScriptManager ScriptManager
        {
            get
            {
                if (this.sm == null)
                {
                    if (HttpContext.Current != null)
                    {
                        this.sm = ScriptManager.GetInstance(HttpContext.Current);
                    }
                }

                return this.sm;
            }
        }

        private Control owner;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The Owner Control for this Listener.")]
        public Control Owner
        {
            get
            {
                if (this.owner == null)
                {
                    if (HttpContext.Current != null && HttpContext.Current.CurrentHandler is Page)
                    {
                        this.owner = (Page)HttpContext.Current.CurrentHandler;
                    }
                }

                return this.owner;
            }
            internal set
            {
                this.owner = value;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Does this object currently represent it's default state.")]
        public virtual bool IsDefault
        {
            get { return false; }
        }


        /*  ViewState
            -----------------------------------------------------------------------------------------------*/

        private StateBag viewstate;

        protected StateBag ViewState
        {
            get
            {
                if (this.viewstate == null)
                {
                    this.viewstate = new StateBag();
                    this.TrackViewState();
                }

                return this.viewstate;
            }
            set
            {
                this.viewstate = value;
            }
        }

        private bool trackingViewState = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTrackingViewState
        {
            get { return this.trackingViewState; }
        }

        public virtual void LoadViewState(object state)
        {
            object[] states = state as object[];
            if (states != null)
            {
                foreach (Pair pair in states)
                {
                    switch((string)pair.First)
                    {
                        case "base":
                            ((IStateManager)this.ViewState).LoadViewState(pair.Second);
                            break;
                        case "vsMembers":
                            ViewStateProcessor.LoadViewState(this, pair.Second);
                            break;
                    }
                }
            }
            else
            {
                ((IStateManager)this.ViewState).LoadViewState(state);
            }
        }

        public virtual object SaveViewState()
        {
            List<Pair> state = new List<Pair>();
            object baseState = ((IStateManager)this.ViewState).SaveViewState();
            if(baseState != null)
            {
                state.Add(new Pair("base", baseState));
            }

            object vsMembers = ViewStateProcessor.SaveViewState(this);
            if (vsMembers != null)
            {
                state.Add(new Pair("vsMembers", vsMembers));
            }

            return state.Count == 0 ? null : state.ToArray();
        }

        public virtual void TrackViewState()
        {
            this.trackingViewState = true;
            ((IStateManager)this.ViewState).TrackViewState();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        bool IStateManager.IsTrackingViewState
        {
            get { return this.IsTrackingViewState; }
        }

        void IStateManager.LoadViewState(object state)
        {
            this.LoadViewState(state);
        }

        object IStateManager.SaveViewState()
        {
            return this.SaveViewState();
        }

        void IStateManager.TrackViewState()
        {
            this.TrackViewState();
        }

        public void SetDirty()
        {
            this.ViewState.SetDirty(true);
        }


        static readonly object DataBindingEvent = new object();
        EventHandlerList events;

        protected EventHandlerList Events
        {
            get
            {
                if (this.events == null)
                    this.events = new EventHandlerList();
                return this.events;
            }
        }


        public event EventHandler DataBinding
        {
            add
            {
                Events.AddHandler(DataBindingEvent, value);
            }
            remove
            {
                Events.RemoveHandler(DataBindingEvent, value);
            }
        }

        protected virtual void OnDataBinding(EventArgs e)
        {
            EventHandler handler = (EventHandler)(this.Events[DataBindingEvent]);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Control BindingContainer
        {
            get
            {
                Control container = this.Owner.NamingContainer;
                if (container != null)
                    container = container.BindingContainer;
                return container;
            }
        }

        public virtual void DataBind()
        {
            OnDataBinding(EventArgs.Empty);
        }
    }
}