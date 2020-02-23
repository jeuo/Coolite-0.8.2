/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;

namespace Coolite.Ext.Web
{
    public class ContentPanelDesigner : PanelBaseDesigner
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            this.SetViewFlags(ViewFlags.TemplateEditing, true);
        }

        public override TemplateGroupCollection TemplateGroups
        {
            get
            {
                TemplateGroupCollection templateGroups = new TemplateGroupCollection();
                TemplateGroup group = new TemplateGroup("Body");
                TemplateDefinition template = new TemplateDefinition(this, "Body", this.Control, "Body", false);
                group.AddTemplateDefinition(template);
                templateGroups.Add(group);
                return templateGroups;
            }
        }

        public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
        {
            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ITemplate template = ((ContentPanel)this.Control).Body;

                if (template != null)
                {
                    return ControlPersister.PersistTemplate(template, host);
                }
            }
            return string.Empty;
        }

        public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
        {
            if (content == null)
                return;

            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ITemplate template = ControlParser.ParseTemplate(host, content);
                ((ContentPanel)this.Control).Body = template;
            }
        }

        protected string ContentRegionName
        {
            get
            {
                if (Layout.HasValue)
                {
                    switch (Layout.Value)
                    {
                        case LayoutType.Accordion:
                            break;
                        case LayoutType.Anchor:
                            break;
                        case LayoutType.Border:
                            return string.Format("Body_{0}_{1}", BorderRegion.Region, 0);
                        case LayoutType.Card:
                            break;
                        case LayoutType.Column:
                            break;
                        case LayoutType.Container:
                            break;
                        case LayoutType.Fit:
                            return string.Format("Body_{0}", 0);
                        case LayoutType.Form:
                            break;
                        case LayoutType.Table:
                            break;
                        case LayoutType.FormAnchor:
                            break;
                    }
                }

                return "Body";
            }
        }
        
    }
}