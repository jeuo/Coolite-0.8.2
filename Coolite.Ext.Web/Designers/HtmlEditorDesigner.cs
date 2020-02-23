/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class HtmlEditorDesigner : WebControlDesigner
    {
        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }

        public override string GetDesignTimeHtml()
        {
            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            HtmlEditor c = (HtmlEditor)this.Control;

            string width = string.Format(" width: {0};", (c.Width != Unit.Empty) ? c.Width.ToString() : "504px");
            string height = string.Format(" height: {0};", (c.Height != Unit.Empty) ? c.Height.ToString() : "275px");

            /*
            * 0 - ClientID
            * 1 - Text
            * 2 - Width
            * 3 - Height
            * 4 - Font
            * 5 - Format
            * 6 - FontSize
            * 7 - Colors
            * 8 - Alignments
            * 9 - Links
            * 10 - Lists
            * 11- SourceEdit
            */

            object[] args = new object[12];
            args[0] = c.ClientID;
            args[1] = c.Text;
            args[2] = width;
            args[3] = height;
            args[4] = (c.EnableFont) ? this.Font : string.Empty;
            args[5] = (c.EnableFormat) ? this.Format : string.Empty;
            args[6] = (c.EnableFontSize) ? this.FontSize : string.Empty;
            args[7] = (c.EnableColors) ? this.Colors : string.Empty;
            args[8] = (c.EnableAlignments) ? this.Alignments : string.Empty;
            args[9] = (c.EnableLinks) ? this.Links : string.Empty;
            args[10] = (c.EnableLists) ? this.Lists : string.Empty;
            args[11] = (c.EnableSourceEdit) ? this.SourceEdit : string.Empty;

            LiteralControl ctrl = new LiteralControl(string.Format(this.Html, args));
            ctrl.RenderControl(htmlWriter);

            return writer.ToString();
        }



        public virtual string Font
        {
            get
            {
                return @"<td class=""""><select class=""x-font-select"">
                  <option value=""arial"" style=""font-family: Arial;"">Arial</option>
                </select>
              </td>
              <td><span class=""ytb-sep""></span></td>";
            }
        }

        public virtual string Format
        {
            get
            {
                return @"<td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-bold"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-italic"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-underline"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><span class=""ytb-sep""></span></td>";
            }
        }

        public virtual string FontSize
        {
            get
            {
                return @"<td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-increasefontsize"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-decreasefontsize"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><span class=""ytb-sep""></span></td>";
            }
        }        

        public virtual string Colors
        {
            get
            {
                return @"<td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-forecolor"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr class=""x-btn-with-menu"">
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-backcolor"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr class=""x-btn-with-menu"">
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><span class=""ytb-sep""></span></td>";
            }
        }

        public virtual string Alignments
        {
            get
            {
                return @"<td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-justifyleft x-btn-pressed"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-justifycenter"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-justifyright"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><span class=""ytb-sep""></span></td>";
            }
        }

        public virtual string Links
        {
            get
            {
                return @"<td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-createlink"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><span class=""ytb-sep""></span></td>";
            }
        }

        public virtual string Lists
        {
            get
            {
                return @"<td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-insertorderedlist"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-insertunorderedlist"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>
              <td><span class=""ytb-sep""></span></td>";
            }
        }


        public virtual string SourceEdit
        {
            get
            {
                return @"<td ><table style=""width: auto;"" class=""x-btn-wrap x-btn x-btn-icon x-edit-sourceedit"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                  <tbody>
                    <tr>
                      <td class=""x-btn-left""><i>&nbsp;</i></td>
                      <td class=""x-btn-center""><em unselectable=""on"">
                        <button tabindex=""-1"" class=""x-btn-text"" type=""button"">&nbsp;</button>
                        </em></td>
                      <td class=""x-btn-right""><i>&nbsp;</i></td>
                    </tr>
                  </tbody>
                </table>
              </td>";
            }
        }

        public virtual string Html
        {
            get
            {
                return @"
  <div style=""{2}"" class=""x-html-editor-wrap"">
    <div class=""x-html-editor-tb"">
      <div class=""x-toolbar x-small-editor"">
        <table cellspacing=""0"">
          <tbody>
            <tr>
            {4}
            {5}
            {6}
            {7}
            {8}
            {9}
            {10}
            {11}
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <textarea tabindex=""-1"" class=""x-form-textarea x-form-field"" style=""border: 0pt none; {2}{3}"" autocomplete=""off"">{1}</textarea>
  </div>
";
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
                    actionLists.Add(new HtmlEditorActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}