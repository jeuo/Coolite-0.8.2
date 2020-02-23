/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;

namespace Coolite.Ext.Web
{
    public class AfterStoreChangedEventArgs : EventArgs
    {
        private bool success;
        private Exception exception;
        private ConfirmationList confirmationList;
        private bool exceptionHandled;

        public AfterStoreChangedEventArgs(bool success, Exception exception, ConfirmationList confirmationList)
        {
            this.exception = exception;
            this.confirmationList = confirmationList;
            this.success = success;
        }

        public bool Success
        {
            get { return success; }
        }

        public Exception Exception
        {
            get { return exception; }
        }

        public bool ExceptionHandled
        {
            get { return exceptionHandled; }
            set { exceptionHandled = value; }
        }

        public ConfirmationList ConfirmationList
        {
            get
            {
                return confirmationList;
            }
            internal set
            {
                confirmationList = value;
            }
        }
    }
}