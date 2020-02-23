/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Coolite.Utilities;
using System;

namespace Coolite.Ext.Web
{
    public class TokenUtils
    {
        public const string ID_PATTERN = @"#{[^\}]+}";
        public const string SELECT_PATTERN = @"\${[^{}]+}";
        public const string AJAXMETHODS_PATTERN = "#{AjaxMethods}";

        public static bool IsAjaxMethodsToken(string script)
        {
            return script.IndexOf(TokenUtils.AJAXMETHODS_PATTERN) >= 0;
        }

        public static bool IsAlertToken(string script)
        {
            return (!string.IsNullOrEmpty(script)) ? (script.StartsWith("!{") && script.EndsWith("}")) : false;
        }

        public static bool IsRawToken(string script)
        {
            return (!string.IsNullOrEmpty(script)) ? ((script.StartsWith("={") && script.EndsWith("}")) || script.StartsWith("<raw>")) : false;
        }

        public static bool IsFuncToken(string script)
        {
            return !string.IsNullOrEmpty(script) ? script.StartsWith("#F{") && script.EndsWith("}") : false;
        }

        public static bool IsFunction(string script)
        {
            return (!string.IsNullOrEmpty(script)) ? (script.StartsWith("function(") || script.StartsWith("Ext.get") || script.StartsWith("Ext.select")) : false;
        }

        public static bool IsIDToken(string script)
        {
            return (!string.IsNullOrEmpty(script)) ? (new Regex(string.Concat("^", TokenUtils.ID_PATTERN)).Matches(script).Count == 1) : false;
        }

        public static bool IsSelectToken(string script)
        {
            return (!string.IsNullOrEmpty(script)) ? (new Regex(string.Concat("^", TokenUtils.SELECT_PATTERN)).Matches(script).Count == 1) : false;
        }

        public static bool IsFunctionToken(string script)
        {
            return (!string.IsNullOrEmpty(script)) ? (script.StartsWith("_{") && script.EndsWith("}")) : false;
        }

        public static string ParseTokens(string script)
        {
            return TokenUtils.ParseTokens(script, TokenUtils.Page);
        }

        public static string ParseTokens(string script, Control seed)
        {
            if (seed == null)
            {
                seed = TokenUtils.Page;
            }

            bool isRaw = (
                TokenUtils.IsAlertToken(script) ||
                TokenUtils.IsRawToken(script) ||
                TokenUtils.IsSelectToken(script));
            
            script = TokenUtils.ReplaceIDTokens(script, seed);
            script = TokenUtils.ReplaceSelectTokens(script);

            script = TokenUtils.ReplaceAlertToken(script);
            script = TokenUtils.ReplaceRawToken(script);
            script = TokenUtils.ReplaceFunctionToken(script);

            return (isRaw || TokenUtils.IsFunction(script)) ? string.Concat("<raw>", script) : script;
        }

        public static string ReplaceAjaxMethods(string script, Control seed)
        {
            if (TokenUtils.IsAjaxMethodsToken(script))
            {
                UserControl parent = seed as UserControl;

                if (parent == null)
                {
                    parent = ReflectionUtils.GetTypeOfParent(seed, typeof(System.Web.UI.UserControl)) as UserControl;
                }

                ScriptManager sm = null;
                string ns = "Coolite.AjaxMethods";

                if(parent != null)
                {
                    string id = ScriptManager.GetControlIdentification(parent);

                    if (!string.IsNullOrEmpty(id))
                    {
                        id = string.Concat(".", id);
                    }

                    sm = ScriptManager.GetInstance(HttpContext.Current);

                    if (sm != null)
                    {
                        ns = sm.AjaxMethodNamespace;
                    }

                    return script.Replace(TokenUtils.AJAXMETHODS_PATTERN, string.Concat(ns, id));
                }
                else
                {
                    Page parentPage = seed as Page;

                    if (parentPage == null)
                    {
                        parentPage = ReflectionUtils.GetTypeOfParent(seed, typeof(System.Web.UI.Page)) as System.Web.UI.Page;
                    }

                    if (parentPage != null)
                    {
                        sm = ScriptManager.GetInstance(parentPage);

                        if(sm != null)
                        {
                            ns = sm.AjaxMethodNamespace;
                        }

                        return script.Replace(TokenUtils.AJAXMETHODS_PATTERN, ns);
                    }
                }
            }

            return script;
        }

        public static string ReplaceAlertToken(string script)
        {
            if (TokenUtils.IsAlertToken(script))
            {
                script = string.Format(ScriptManager.FunctionTemplate, string.Concat("Ext.Msg.alert(", script.Substring(0, script.Length - 1).Substring(2), ");"));
            }
            return script;
        }

        public static string ReplaceRawToken(string script)
        {
            if (TokenUtils.IsRawToken(script))
            {
                script = (script.StartsWith("<raw>")) ? script.Substring(5) : script.Substring(0, script.Length - 1).Substring(2);
            }
            return script;
        }

        public static string ReplaceFuncToken(string script)
        {
            if (TokenUtils.IsFuncToken(script))
            {
                script = string.Concat("function(){return ", script.Substring(0, script.Length - 1).Substring(3), ";}");
            }
            return script;
        }
        
        public static List<string> ExtractIDs(string script)
        {

            List<string> result = new List<string>();

            if (!string.IsNullOrEmpty(script))
            {
                Regex regex = new Regex(TokenUtils.ID_PATTERN);
                MatchCollection matches = regex.Matches(script);
                string id = "";
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        id = match.Value.Remove(match.Value.Length - 1).Remove(0, 2);

                        if (!string.IsNullOrEmpty(id))
                        {
                            result.Add(id);
                        }
                    }
                }
                else if (!script.EndsWith("}"))
                {
                    result.Add(script);
                }
            }

            return result;
        }

        public static string ReplaceIDTokens(string script, Control seed)
        {
            script = TokenUtils.ReplaceAjaxMethods(script, seed);
            script = TokenUtils.ReplaceFuncToken(script);

            Regex regex = new Regex(TokenUtils.ID_PATTERN);
            MatchCollection matches = regex.Matches(script);
            Control control = null;
            string id = "";
            foreach (Match match in matches)
            {
                id = match.Value.Remove(match.Value.Length - 1).Remove(0, 2);

                control = ControlUtils.FindControl(seed, id);

                if (control != null)
                {
                    if(control is Layout)
                    {
                        Component component = ((Layout)control).ParentComponent;
                        script = script.Replace(match.Value, component != null ? string.Concat(component.ClientID, ".getLayout()") : control.ClientID);
                    }
                    else if (control is Observable || control is UserControl)
                    {
                        script = script.Replace(match.Value, control.ClientID);
                    }
                    else
                    {
                        script = script.Replace(match.Value, string.Concat("Ext.get(\"", control.ClientID, "\")"));
                    }
                }
                else
                {
                    script = script.Replace(match.Value, string.Concat("Ext.get(\"", match.Value.Remove(match.Value.Length - 1).Remove(0, 2), "\")"));
                }
            }

            return script;
        }

        public static string ReplaceSelectTokens(string script)
        {
            return new Regex(TokenUtils.SELECT_PATTERN).Replace(script, new MatchEvaluator(TokenUtils.MatchSelectTokens));
        }

        static string MatchSelectTokens(Match match)
        {
            return string.Concat("Ext.select(\"", match.Value.Remove(match.Value.Length - 1).Remove(0, 2), "\")");
        }

        public static string ReplaceFunctionToken(string script)
        {
            if (TokenUtils.IsFunctionToken(script))
            {
                script = string.Concat("function()", script.Remove(0, 1));
            }
            return script;
        }

        public static string ParseAndNormalize(string script)
        {
            return TokenUtils.ParseAndNormalize(script, null);
        }

        public static string ParseAndNormalize(string script, Control seed)
        {
            return TokenUtils.NormalizeRawToken(TokenUtils.ParseTokens(script, seed));
        }

        public static string NormalizeRawToken(string script)
        {
            if (TokenUtils.IsRawToken(script))
            {
                return TokenUtils.ReplaceRawToken(script);
            }
            return string.Concat("\"", script, "\"");
        }

        private static Page page;

        public static Page Page
        {
            get
            {
                if (TokenUtils.page == null)
                {
                    if (HttpContext.Current != null && HttpContext.Current.CurrentHandler is Page)
                    {
                        TokenUtils.page = (Page)HttpContext.Current.CurrentHandler;
                    }
                }

                return TokenUtils.page;
            }
        }
    }
}