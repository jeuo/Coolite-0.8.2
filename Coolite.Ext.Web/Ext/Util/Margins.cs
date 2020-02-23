/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;

namespace Coolite.Ext.Web
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Margins : IEquatable<Margins>
    {
        private int bottom;
        private int left;
        private int right;
        private int top;

        public Margins(int top, int right, int bottom, int left)
        {
            this.top = top;
            this.left = left;
            this.right = right;
            this.bottom = bottom;
        }

        [NotifyParentProperty(true)]
        [DefaultValue(-1)]
        public int Top
        {
            get { return this.top; }
            set { this.top = value; }
        }

        [NotifyParentProperty(true)]
        [DefaultValue(-1)]
        public int Left
        {
            get { return this.left; }
            set { this.left = value; }
        }

        [NotifyParentProperty(true)]
        [DefaultValue(-1)]
        public int Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        [NotifyParentProperty(true)]
        [DefaultValue(-1)]
        public int Bottom
        {
            get { return this.bottom; }
            set { this.bottom = value; }
        }

        /// <summary>
        /// Does this object currently represent it's default state.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Does this object currently represent it's default state.")]
        public virtual bool IsDefault
        {
            get
            {
                return this.ToString().Equals("-1 -1 -1 -1");
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", this.Top, this.Right, this.Bottom, this.Left);
        }

        public virtual bool Equals(Margins margins)
        {
            return this.ToString().Equals(margins.ToString());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Margins))
            {
                return false;
            }
            return this.Equals((Margins) obj);
        }

        public virtual void Clear()
        {
            this.Top = -1;
            this.Right = -1;
            this.Bottom = -1;
            this.Left = -1;
        }

        public override int GetHashCode()
        {
            int result = Convert.ToInt32(this.Bottom);
            result = 29 * result + Convert.ToInt32(this.Left);
            result = 29 * result + Convert.ToInt32(this.Right);
            result = 29 * result + Convert.ToInt32(this.Top);
            return result;
        }
    }
}