/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class DatePickerDesigner : WebControlDesigner
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

            DatePicker c = (DatePicker)this.Control;

            string width = (c.Width != Unit.Empty) ? string.Format(" width: {0};", c.Width.ToString()) : string.Empty;
            string height = (c.Height != Unit.Empty) ? string.Format(" height: {0};", c.Height.ToString()) : string.Empty;

            object[] args = new object[3];
            args[0] = c.ClientID;
            args[1] = DateTime.Today.ToString("MMMM yyyy");
            args[2] = c.TodayText;

            LiteralControl ctrl = new LiteralControl(string.Format(this.Html, args));
            ctrl.RenderControl(htmlWriter);

            return writer.ToString();
        }

        public virtual string Html
        {
            get
            {
                /*
                 * 0 - ClientID
                 * 1 - Month/Year "January 2008"
                 * 2 - TodayText
                 */
                return @"<div style=""-moz-user-select: none; width: 175px;"" class=""x-date-picker x-unselectable"">
    <table style=""width: 175px;"" cellspacing=""0"">
      <tbody>
        <tr>
          <td class=""x-date-left""><a class=""x-unselectable"" style=""-moz-user-select: none;"" href=""#"" title=""Previous Month (Control+Left)"">&nbsp;</a></td>
          <td class=""x-date-middle"" align=""center""><table style=""width: auto;"" class=""x-btn-wrap x-btn"" border=""0"" cellpadding=""0"" cellspacing=""0"">
              <tbody>
                <tr class=""x-btn-with-menu"">
                  <td class=""x-btn-left""><i>&nbsp;</i></td>
                  <td class=""x-btn-center""><em unselectable=""on"">
                    <button class=""x-btn-text"" type=""button"">{1}</button>
                    </em></td>
                  <td class=""x-btn-right""><i>&nbsp;</i></td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class=""x-date-right""><a class=""x-unselectable"" style=""-moz-user-select: none;"" href=""#"" title=""Next Month (Control+Right)"">&nbsp;</a></td>
        </tr>
        <tr>
          <td colspan=""3"">
            <table class=""x-date-inner"" cellspacing=""0"">
              <thead>
                <tr>
                  <th><span>S</span></th>
                  <th><span>M</span></th>
                  <th><span>T</span></th>
                  <th><span>W</span></th>
                  <th><span>T</span></th>
                  <th><span>F</span></th>
                  <th><span>S</span></th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td title="""" class=""x-date-prevday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>30</span></em></a></td>
                  <td title="""" class=""x-date-prevday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>31</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>1</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>2</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>3</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>4</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>5</span></em></a></td>
                </tr>
                <tr>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>6</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>7</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>8</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>9</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>10</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>11</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>12</span></em></a></td>
                </tr>
                <tr>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>13</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>14</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>15</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>16</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>17</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>18</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>19</span></em></a></td>
                </tr>
                <tr>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>20</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>21</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>22</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>23</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>24</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>25</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>26</span></em></a></td>
                </tr>
                <tr>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>27</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>28</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>29</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>30</span></em></a></td>
                  <td title="""" class=""x-date-active""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>31</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>1</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>2</span></em></a></td>
                </tr>
                <tr>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>3</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>4</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>5</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>6</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>7</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>8</span></em></a></td>
                  <td title="""" class=""x-date-nextday""><a href=""#"" hidefocus=""on"" class=""x-date-date"" tabindex=""1""><em><span>9</span></em></a></td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
        <tr>
          <td colspan=""3"" class=""x-date-bottom"" align=""center""><table style=""width: auto;"" class=""x-btn-wrap x-btn"" border=""0"" cellpadding=""0"" cellspacing=""0"">
              <tbody>
                <tr>
                  <td class=""x-btn-left""><i>&nbsp;</i></td>
                  <td class=""x-btn-center""><em unselectable=""on"">
                    <button class=""x-btn-text"" type=""button"">{2}</button>
                    </em></td>
                  <td class=""x-btn-right""><i>&nbsp;</i></td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
      </tbody>
    </table>
    <div class=""x-date-mp""></div>
  </div>";
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
                    actionLists.Add(new DatePickerActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}