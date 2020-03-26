/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class RecordFieldCollection : StateManagedCollection<RecordField> 
    {
        public virtual void Add(params string[] names)
        {
            string[] parts;
            string name;

            foreach (string s in names)
            {
                name = s.Trim();

                if (name.Contains(":"))
                {
                    parts = name.Split(':');

                    try
                    {
                        base.Add(new RecordField(parts[0], (RecordFieldType)Enum.Parse(typeof(RecordFieldType), StringUtils.ToTitleCase(parts[1]))));
                        return;
                    }
                    catch (ArgumentException) 
                    {
                        throw new ArgumentException("The RecordFieldType of \"" + parts[1] + "\" was not found");
                    }
                }
                
                base.Add(new RecordField(name));
            }
        }

        public virtual void Add(string name)
        {
            if (name.Contains(","))
            {
                this.Add(name.Split(','));
            }
            else
            {
                base.Add(new RecordField(name));
            }
        }

        public virtual void Add(string name, RecordFieldType type)
        {
            base.Add(new RecordField(name, type));
        }
        
        public virtual void Add(string name, RecordFieldType type, string dateFormat)
        {
            base.Add(new RecordField(name, type, dateFormat));
        }
    }
}
