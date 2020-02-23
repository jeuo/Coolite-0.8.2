/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class PluginsCollection<T> : List<T> where T : Plugin
    {
        new public void Add(T plugin)
        {
            base.Add(plugin);

            if (this.AfterPluginAdd != null)
            {
                this.AfterPluginAdd(plugin);
            }
        }

        new public void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);

            foreach (T plugin in collection)
            {
                if (this.AfterPluginAdd != null)
                {
                    this.AfterPluginAdd(plugin);
                }
            }
        }

        new public void Insert(int index, T plugin)
        {
            base.Insert(index, plugin);

            if (this.AfterPluginAdd != null)
            {
                this.AfterPluginAdd(plugin);
            }
        }

        new public void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);

            foreach (T plugin in collection)
            {
                if (this.AfterPluginAdd != null)
                {
                    this.AfterPluginAdd(plugin);
                }
            }
        }

        new public void Clear()
        {
            foreach (T plugin in this)
            {
                if (this.AfterPluginRemove != null)
                {
                    this.AfterPluginRemove(plugin);
                }
            }
            base.Clear();
        }

        new public void Remove(T plugin)
        {
            base.Remove(plugin);

            if (this.AfterPluginRemove!= null)
            {
                this.AfterPluginRemove(plugin);
            }
        }

        internal delegate void AfterPluginAddHandler(T plugin);
        internal event AfterPluginAddHandler AfterPluginAdd;

        internal delegate void AfterPluginRemoveHandler(T plugin);
        internal event AfterPluginRemoveHandler AfterPluginRemove;
    }
}