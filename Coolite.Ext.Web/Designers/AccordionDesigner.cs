/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;

namespace Coolite.Ext.Web
{
    public class AccordionDesigner : WebControlDesigner
    {
        private Accordion layout;
        private DesignerRegionCollection designerRegions;

        public override bool AllowResize
        {
            get { return false; }
        }

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            this.SetViewFlags(ViewFlags.TemplateEditing, true);
            this.layout = (Accordion)component;
        }

        public override TemplateGroupCollection TemplateGroups
        {
            get
            {
                TemplateGroupCollection templateGroups = new TemplateGroupCollection();
                TemplateGroup group = new TemplateGroup("Body");

                int i = 0;
                foreach (ContentPanel item in this.layout.Items)
                {
                    string itemID = string.IsNullOrEmpty(item.ID) ? "No ID" : item.ID;
                    TemplateDefinition template = new TemplateDefinition(this, string.Format("Body{0}({1})", i, itemID), item, "Body", false);
                    group.AddTemplateDefinition(template);
                    i++;
                }

                templateGroups.Add(group);
                return templateGroups;
            }
        }

        public override string GetDesignTimeHtml(DesignerRegionCollection regions)
        {
            StringBuilder sb = new StringBuilder(256);
            designerRegions = regions;

            EditableDesignerRegion editableDesignerRegion = new EditableDesignerRegion(CurrentDesigner, "Body_" + this.layout.ExpandedPanelIndex);
            designerRegions.Add(editableDesignerRegion);

            sb.AppendFormat("<div style='width:100%;height:100%;margins:0px;padding-addToStart:0px;padding-right:0px;padding-left:0px;padding-bottom:{0}px;'>",23*this.layout.Items.Count);

            bool wasExpaned = false;
            
            for (int i = 0; i < this.layout.Items.Count; i++)
            {
                PanelBase item = this.layout.Items[i] as PanelBase;

                this.AddIcon(item.Icon);

                sb.Append("<div class='x-panel ");

                if(!item.Border)
                {
                    sb.Append("x-panel-noborder ");
                }

                if(item.Collapsed || wasExpaned)
                {
                    SetCollapsed(item, true);
                    sb.Append("x-panel-collapsed");
                }
                else
                {
                    wasExpaned = true;
                }

                sb.Append("' style='width:auto;'>");

                sb.AppendFormat("<a href='#' {0}><div class='x-panel-header ", GetDesignerRegionAttribute(i));

                if (!item.Border)
                {
                    sb.Append("x-panel-header-noborder ");
                }

                sb.Append("x-accordion-hd'>");

                sb.Append("<div class='x-tool x-tool-toggle'></div>");

                string iconCls = item.IconCls;
                if (!string.IsNullOrEmpty(iconCls))
                {
                    string s =
                        this.GetWebResourceUrl(
                            "Coolite.Ext.Web.Build.Resources.Coolite.extjs.resources.images.default.s.gif");
                    sb.AppendFormat("<img class='x-panel-inline-icon {0}'src='{1}'/>", iconCls, s);
                }

                sb.AppendFormat("<span class='x-panel-header-text' style='text-decoration:none;'>{0}</span>", string.IsNullOrEmpty(item.Title) ? "&nbsp;" : item.Title);

                sb.Append("</div></a>");

                if (!item.Collapsed)
                {
                    sb.AppendFormat(@"<div class='x-panel-bwrap' style='DISPLAY: block; LEFT: auto; VISIBILITY: visible; POSITION: static; TOP: auto'>
                                    <div class='x-panel-body {0}' style='width:auto;height:100%;padding:0px;margins:0px;' {1}></div>
                                    </div>", item.Border ? "" : "x-panel-body-noborder", GetEditableDesignerRegionAttribute());
                }

                sb.Append("</div>");
            }

            sb.Append("</div>");

            return sb.ToString() + this.GetIconStyleBlock();
        }

        private static string GetEditableDesignerRegionAttribute()
        {
            return string.Format("{0}=\"{1}\"", DesignerRegion.DesignerRegionAttributeName, 0);
        }

        public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
        {
            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
            if (host != null && region != null)
            {
                string[] parameters = region.Name.Split('_');

                if (parameters.Length == 2 && parameters[0] == "Body")
                {
                    int activeIndex = int.Parse(parameters[1]);

                    if (activeIndex >= 0)
                    {
                        ContentPanel panel = this.layout.Items[activeIndex] as ContentPanel;
                        if (panel != null)
                        {
                            ITemplate contentTemplate = panel.Body;

                            if (contentTemplate != null)
                            {
                                return ControlPersister.PersistTemplate(contentTemplate, host);
                            }
                        }
                    }
                }
            }
            return String.Empty;
        }

        public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
        {
            if (content == null)
                return;
            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));
            if (host != null)
            {

                string[] parameters = region.Name.Split('_');

                if (parameters.Length == 2 && parameters[0] == "Body")
                {
                    int index = int.Parse(parameters[1]);
                    if (this.layout.Items.Count > 0)
                    {
                        ContentPanel panel = this.layout.Items[index] as ContentPanel;

                        if (panel != null)
                        {
                            ITemplate template = ControlParser.ParseTemplate(host, content);
                            panel.Body = template;
                        }
                        Tag.SetDirty(true);
                    }
                }
            }
        }

        private static void SetCollapsed(PanelBase item, bool collapse)
        {
            PropertyDescriptor collapsed = TypeDescriptor.GetProperties(item)["Collapsed"];
            collapsed.SetValue(item, collapse);
            item.Collapsed = collapse;
        }

        private string GetDesignerRegionAttribute(int index)
        {
            string name = string.Format("Toggle_{0}", index);
            designerRegions.Add(new DesignerRegion(this, name, false));
            return string.Format("{0}=\"{1}\"", DesignerRegion.DesignerRegionAttributeName, designerRegions.Count - 1);
        }

        protected override void OnClick(DesignerRegionMouseEventArgs e)
        {
            if (e.Region == null)
            {
                return;
            }

            string[] parameters = e.Region.Name.Split('_');

            if (parameters.Length < 2)
            {
                return;
            }

            switch (parameters[0])
            {
                case "Toggle":
                    Toggle(int.Parse(parameters[1]));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            this.Tag.SetDirty(true);
            this.UpdateDesignTimeHtml();
        }

        private void Toggle(int index)
        {
            if(index < this.layout.Items.Count)
            {
                PanelBase item = this.layout.Items[index] as PanelBase;

                IComponentChangeService changeService =
                        (IComponentChangeService)GetService(typeof(IComponentChangeService));

                try
                {
                    changeService.OnComponentChanging(this.layout,
                                                      TypeDescriptor.GetProperties(this.layout)["Items"]);
                    if (item.Collapsed)
                    {
                        this.ExpandItem(item);
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(item)["Collapsed"].SetValue(item, true);
                        item.Collapsed = true;
                    }

                }
                finally
                {
                    changeService.OnComponentChanged(this.layout,
                                                     TypeDescriptor.GetProperties(this.layout)["Items"],
                                                     null, null);
                }
            }
        }

        public void AddPanel()
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

            if (host != null)
            {
                Panel item = (Panel)host.CreateComponent(typeof(Panel));
                
                if (item != null)
                {
                    IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                    try
                    {
                        changeService.OnComponentChanging(layout, TypeDescriptor.GetProperties(layout)["Items"]);
                        item.Title = "Item";
                        item.Border = false;
                        layout.Items.Add(item);

                        this.ExpandItem(item);
                    }
                    finally
                    {
                        changeService.OnComponentChanged(layout, TypeDescriptor.GetProperties(layout)["Items"], null, null);
                    }
                }
                this.UpdateDesignTimeHtml();

                this.Tag.SetDirty(true);
            }
        }

        private void ExpandItem(PanelBase item)
        {
            foreach (PanelBase panel in this.layout.Items)
            {
                if (!panel.Collapsed)
                {
                    TypeDescriptor.GetProperties(panel)["Collapsed"].SetValue(panel, true);
                    panel.Collapsed = true;
                }
            }

            TypeDescriptor.GetProperties(item)["Collapsed"].SetValue(item, false);
            item.Collapsed = false;
        }

        public void RemovePanel()
        {
            int oldIndex = this.layout.ExpandedPanelIndex;
            if (this.layout.ExpandedPanelIndex > -1)
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                if (host != null)
                {
                    using (DesignerTransaction dt = host.CreateTransaction("Remove Panel"))
                    {
                        IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                        PanelBase panel = this.layout.Items[oldIndex] as PanelBase;
                        
                        try
                        {
                            changeService.OnComponentChanging(this.layout, TypeDescriptor.GetProperties(this.layout)["Items"]);
                            this.layout.Items.Remove(panel);

                            if (this.layout.Items.Count > 0)
                            {
                                PanelBase activeItem =
                                    this.layout.Items[Math.Min(oldIndex, this.layout.Items.Count - 1)] as PanelBase;

                                this.ExpandItem(activeItem);
                            }
                        }
                        finally
                        {
                            changeService.OnComponentChanged(this.layout, TypeDescriptor.GetProperties(this.layout)["Items"], null, null);
                        }

                        panel.Dispose();

                        this.UpdateDesignTimeHtml();
                        dt.Commit();
                    }
                    this.Tag.SetDirty(true);
                }
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
                    actionLists.Add(new AccordionActionList(this));
                }
                return actionLists;
            }
        }

    }
}