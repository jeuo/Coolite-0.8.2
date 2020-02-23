/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

namespace Coolite.Ext.Web
{
    public enum HtmlEvent
    {
       //Loading of an image is interrupted
        Abort,
        
        //An element loses focus
        Blur,

        //The user changes the content of a field
        Change,

        //Mouse clicks an object
        Click,

        //Mouse double-clicks an object
        DoubleClick,

        //An error occurs when loading a document or an image
        Error,

        //An element gets focus
        Focus,

        //A keyboard key is pressed
        KeyDown,

        //A keyboard key is pressed or held down
        KeyPress,

        //A keyboard key is released
        KeyUp,

        //A page or an image is finished loading
        Load,

        //A mouse button is pressed
        MouseDown,

        //The mouse is moved
        MouseMove,

        //The mouse is moved off an element
        MouseOut,

        //The mouse is moved over an element
        MouseOver,

        //A mouse button is released
        MouseUp,

        //The reset button is clicked
        Reset,

        //A window or frame is resized
        Resize,

        //Text is selected
        Select,

        //The submit button is clicked
        Submit,

        //The user exits the page
        Unload
    }
}