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
    public class HtmlEditorAjaxEvents : FieldAjaxEvents
    {
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ComponentAjaxEvent Blur
        {
            get
            {
                return base.Blur;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ComponentAjaxEvent Focus
        {
            get
            {
                return base.Focus;
            }
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ComponentAjaxEvent Change
        {
            get
            {
                return base.Change;
            }
        }

        private ComponentAjaxEvent activate;

        /// <summary>
        /// Fires when the editor is first receives the focus. Any insertion must wait until after this event.
        /// </summary>
        [ListenerArgument(0, "el", typeof(HtmlEditor), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("activate", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the editor is first receives the focus. Any insertion must wait until after this event.")]
        public virtual ComponentAjaxEvent Activate
        {
            get
            {
                if (this.activate == null)
                {
                    this.activate = new ComponentAjaxEvent();
                }
                return this.activate;
            }
        }

        private ComponentAjaxEvent beforePush;

        /// <summary>
        /// Fires before the iframe editor is updated with content from the textarea. Return false to cancel the push.
        /// </summary>
        [ListenerArgument(0, "el", typeof(HtmlEditor), "this")]
        [ListenerArgument(1, "html", typeof(string), "Html")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforepush", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the iframe editor is updated with content from the textarea. Return false to cancel the push.")]
        public virtual ComponentAjaxEvent BeforePush
        {
            get
            {
                if (this.beforePush == null)
                {
                    this.beforePush = new ComponentAjaxEvent();
                }
                return this.beforePush;
            }
        }

        private ComponentAjaxEvent beforeSync;

        /// <summary>
        /// Fires before the textarea is updated with content from the editor iframe. Return false to cancel the sync.
        /// </summary>
        [ListenerArgument(0, "el", typeof(HtmlEditor), "this")]
        [ListenerArgument(1, "html", typeof(string), "Html")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforesync", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the textarea is updated with content from the editor iframe. Return false to cancel the sync.")]
        public virtual ComponentAjaxEvent BeforeSync
        {
            get
            {
                if (this.beforeSync == null)
                {
                    this.beforeSync = new ComponentAjaxEvent();
                }
                return this.beforeSync;
            }
        }

        private ComponentAjaxEvent editModeChange;

        /// <summary>
        /// Fires when the editor switches edit modes.
        /// </summary>
        [ListenerArgument(0, "el", typeof(HtmlEditor), "this")]
        [ListenerArgument(1, "sourceEdit", typeof(bool), "True if source edit, false if standard editing.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("editmodechange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the editor switches edit modes.")]
        public virtual ComponentAjaxEvent EditModeChange
        {
            get
            {
                if (this.editModeChange == null)
                {
                    this.editModeChange = new ComponentAjaxEvent();
                }
                return this.editModeChange;
            }
        }

        private ComponentAjaxEvent initialize;

        /// <summary>
        /// Fires when the editor is fully initialized (including the iframe).
        /// </summary>
        [ListenerArgument(0, "el", typeof(HtmlEditor), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("initialize", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the editor is fully initialized (including the iframe).")]
        public virtual ComponentAjaxEvent Initialize
        {
            get
            {
                if (this.initialize == null)
                {
                    this.initialize = new ComponentAjaxEvent();
                }
                return this.initialize;
            }
        }

        private ComponentAjaxEvent push;

        /// <summary>
        /// Fires when the iframe editor is updated with content from the textarea.
        /// </summary>
        [ListenerArgument(0, "el", typeof(HtmlEditor), "this")]
        [ListenerArgument(1, "html", typeof(string), "Html")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("push", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the iframe editor is updated with content from the textarea.")]
        public virtual ComponentAjaxEvent Push
        {
            get
            {
                if (this.push == null)
                {
                    this.push = new ComponentAjaxEvent();
                }
                return this.push;
            }
        }

        private ComponentAjaxEvent sync;

        /// <summary>
        /// Fires when the textarea is updated with content from the editor iframe.
        /// </summary>
        [ListenerArgument(0, "el", typeof(HtmlEditor), "this")]
        [ListenerArgument(1, "html", typeof(string), "Html")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("sync", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the textarea is updated with content from the editor iframe.")]
        public virtual ComponentAjaxEvent Sync
        {
            get
            {
                if (this.sync == null)
                {
                    this.sync = new ComponentAjaxEvent();
                }
                return this.sync;
            }
        }
    }
}