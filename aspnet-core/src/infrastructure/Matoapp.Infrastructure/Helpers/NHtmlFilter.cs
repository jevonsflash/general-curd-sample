using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Matoapp.Infrastructure.Helpers
{

    /// <summary>
    /// Html 脚本过滤
    /// </summary>
    public class NHtmlFilter
    {
        protected static readonly RegexOptions REGEX_FLAGS_SI = RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled;

        private static string P_COMMENTS = "<!--(.*?)-->";
        private static Regex P_COMMENT = new Regex("^!--(.*)--$", REGEX_FLAGS_SI);
        private static string P_TAGS = "<(.*?)>";
        private static Regex P_END_TAG = new Regex("^/([a-z0-9]+)", REGEX_FLAGS_SI);
        private static Regex P_START_TAG = new Regex("^([a-z0-9]+)(.*?)(/?)$", REGEX_FLAGS_SI);
        private static Regex P_QUOTED_ATTRIBUTES = new Regex("([a-z0-9|(a-z0-9\\-a-z0-9)]+)=([\"'])(.*?)\\2", REGEX_FLAGS_SI);
        private static Regex P_UNQUOTED_ATTRIBUTES = new Regex("([a-z0-9]+)(=)([^\"\\s']+)", REGEX_FLAGS_SI);
        private static Regex P_PROTOCOL = new Regex("^([^:]+):", REGEX_FLAGS_SI);
        private static Regex P_ENTITY = new Regex("&#(\\d+);?");
        private static Regex P_ENTITY_UNICODE = new Regex("&#x([0-9a-f]+);?");
        private static Regex P_ENCODE = new Regex("%([0-9a-f]{2});?");
        private static Regex P_VALID_ENTITIES = new Regex("&([^&;]*)(?=(;|&|$))");
        private static Regex P_VALID_QUOTES = new Regex("(>|^)([^<]+?)(<|$)", RegexOptions.Singleline | RegexOptions.Compiled);
        private static string P_END_ARROW = "^>";
        private static string P_BODY_TO_END = "<([^>]*?)(?=<|$)";
        private static string P_XML_CONTENT = "(^|>)([^<]*?)(?=>)";
        private static string P_STRAY_LEFT_ARROW = "<([^>]*?)(?=<|$)";
        private static string P_STRAY_RIGHT_ARROW = "(^|>)([^<]*?)(?=>)";
        private static string P_AMP = "&";
        private static string P_QUOTE = "\"";
        private static string P_LEFT_ARROW = "<";
        private static string P_RIGHT_ARROW = ">";
        private static string P_BOTH_ARROWS = "<>";

        // @xxx could grow large... maybe use sesat's ReferenceMap
        private static Dictionary<string, string> P_REMOVE_PAIR_BLANKS = new Dictionary<string, string>();
        private static Dictionary<string, string> P_REMOVE_SELF_BLANKS = new Dictionary<string, string>();
        /** 
         * flag determining whether to try to make tags when presented with "unbalanced"
         * angle brackets (e.g. "<b text </b>" becomes "<b> text </b>").  If set to false,
         * unbalanced angle brackets will be html escaped.
         */
        protected static bool alwaysMakeTags = true;

        /**
         * flag determing whether comments are allowed in input String.
         */
        protected static bool stripComment = true;


        /// <summary>
        /// 不允许
        /// </summary>
        private string[] vDisallowed { get; set; }
        /// <summary>
        /// 允许
        /// </summary>
        protected Dictionary<string, List<string>> vAllowed { get; set; }

        /** counts of open tags for each (allowable) html element **/
        protected Dictionary<string, int> vTagCounts;

        /** html elements which must always be self-closing (e.g. "<img />") **/
        protected string[] vSelfClosingTags;

        /** html elements which must always have separate opening and closing tags (e.g. "<b></b>") **/
        protected string[] vNeedClosingTags;

        /** attributes which should be checked for valid protocols **/
        protected string[] vProtocolAtts;

        /** allowed protocols **/
        protected string[] vAllowedProtocols;

        /** tags which should be removed if they contain no content (e.g. "<b></b>" or "<b />") **/
        protected string[] vRemoveBlanks;

        /** entities allowed within html markup **/
        protected string[] vAllowedEntities;


        /// <summary>
        /// 是否为调试
        /// </summary>
        protected bool vDebug;

        public NHtmlFilter() : this(false) { }

        public NHtmlFilter(bool debug)
        {
            //List<Item> vAllowed = new List<Item>();
            vAllowed = new Dictionary<string, List<string>>();
            #region 允许通过数组

            vAllowed.Add("a", new List<string>() { "target", "href", "title", "class", "style" });
            vAllowed.Add("addr", new List<string>() { "title", "class", "style" });
            vAllowed.Add("address", new List<string>() { "class", "style" });
            vAllowed.Add("area", new List<string>() { "shape", "coords", "href", "alt" });
            vAllowed.Add("article", new List<string>() { });
            vAllowed.Add("aside", new List<string>() { });
            vAllowed.Add("audio", new List<string>() { "autoplay", "controls", "loop", "preload", "src", "class", "style" });
            vAllowed.Add("b", new List<string>() { "class", "style" });
            vAllowed.Add("bdi", new List<string>() { "dir" });
            vAllowed.Add("bdo", new List<string>() { "dir" });
            vAllowed.Add("big", new List<string>() { });
            vAllowed.Add("blockquote", new List<string>() { "cite", "class", "style" });
            vAllowed.Add("br", new List<string>() { });
            vAllowed.Add("caption", new List<string>() { "class", "style" });
            vAllowed.Add("center", new List<string>() { });
            vAllowed.Add("cite", new List<string>() { });
            vAllowed.Add("code", new List<string>() { "class", "style" });
            vAllowed.Add("col", new List<string>() { "align", "valign", "span", "width", "class", "style" });
            vAllowed.Add("colgroup", new List<string>() { "align", "valign", "span", "width", "class", "style" });
            vAllowed.Add("dd", new List<string>() { "class", "style" });
            vAllowed.Add("del", new List<string>() { "datetime" });
            vAllowed.Add("details", new List<string>() { "open" });
            vAllowed.Add("div", new List<string>() { "class", "style" });
            vAllowed.Add("dl", new List<string>() { "class", "style" });
            vAllowed.Add("dt", new List<string>() { "class", "style" });
            vAllowed.Add("em", new List<string>() { "class", "style" });
            vAllowed.Add("font", new List<string>() { "color", "size", "face" });
            vAllowed.Add("footer", new List<string>() { });
            vAllowed.Add("h1", new List<string>() { "class", "style" });
            vAllowed.Add("h2", new List<string>() { "class", "style" });
            vAllowed.Add("h3", new List<string>() { "class", "style" });
            vAllowed.Add("h4", new List<string>() { "class", "style" });
            vAllowed.Add("h5", new List<string>() { "class", "style" });
            vAllowed.Add("h6", new List<string>() { "class", "style" });
            vAllowed.Add("header", new List<string>() { });
            vAllowed.Add("hr", new List<string>() { });
            vAllowed.Add("i", new List<string>() { "class", "style" });
            vAllowed.Add("img", new List<string>() { "src", "alt", "title", "style", "width", "height", "id", "_src", "loadingclass", "class", "data-latex", "data-id", "data-type", "data-s" });
            vAllowed.Add("ins", new List<string>() { "datetime" });
            vAllowed.Add("li", new List<string>() { "class", "style" });
            vAllowed.Add("mark", new List<string>() { });
            vAllowed.Add("nav", new List<string>() { });
            vAllowed.Add("ol", new List<string>() { "class", "style" });
            vAllowed.Add("p", new List<string>() { "class", "style" });
            vAllowed.Add("pre", new List<string>() { "class", "style" });
            vAllowed.Add("s", new List<string>() { });
            vAllowed.Add("section", new List<string>() { });
            vAllowed.Add("small", new List<string>() { });
            vAllowed.Add("span", new List<string>() { "class", "style" });
            vAllowed.Add("sub", new List<string>() { "class", "style" });
            vAllowed.Add("sup", new List<string>() { "class", "style" });
            vAllowed.Add("strong", new List<string>() { "class", "style" });
            vAllowed.Add("table", new List<string>() { "width", "border", "align", "valign", "class", "style" });
            vAllowed.Add("tbody", new List<string>() { "align", "valign", "class", "style" });
            vAllowed.Add("td", new List<string>() { "width", "rowspan", "colspan", "align", "valign", "class", "style" });
            vAllowed.Add("tfoot", new List<string>() { "align", "valign", "class", "style" });
            vAllowed.Add("th", new List<string>() { "width", "rowspan", "colspan", "align", "valign", "class", "style" });
            vAllowed.Add("thead", new List<string>() { "align", "valign", "class", "style" });
            vAllowed.Add("tr", new List<string>() { "rowspan", "align", "valign", "class", "style" });
            vAllowed.Add("tt", new List<string>() { });
            vAllowed.Add("u", new List<string>() { });
            vAllowed.Add("ul", new List<string>() { "class", "style" });
            vAllowed.Add("video", new List<string>() { "autoplay", "controls", "loop", "preload", "src", "height", "width", "class", "style" });
            #endregion


            vDebug = debug;
            vTagCounts = new Dictionary<string, int>();

            vSelfClosingTags = new string[] { "img" };
            vNeedClosingTags = new string[] { "a", "b", "strong", "i", "em" };
            vDisallowed = new string[] { "script" };
            vAllowedProtocols = new string[] { "http", "mailto" }; // no ftp.
            vProtocolAtts = new string[] { "src", "href" };
            vRemoveBlanks = new string[] { "a", "b", "strong", "i", "em" };
            vAllowedEntities = new string[] { "amp", "gt", "lt", "quot" };
            stripComment = true;
            alwaysMakeTags = true;
        }


        protected void reset()
        {
            vTagCounts = new Dictionary<string, int>();
        }

        protected void debug(string msg)
        {
            if (vDebug)
                System.Diagnostics.Debug.WriteLine(msg);
        }

        //---------------------------------------------------------------
        // my versions of some PHP library functions

        public static string chr(int dec)
        {
            return "" + (char)dec;
        }

        /// <summary>
        /// 转换成实体字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string htmlSpecialChars(string str)
        {
            str = str.Replace(P_QUOTE, "\"");

            str = str.Replace(P_LEFT_ARROW, "<");
            str = str.Replace(P_RIGHT_ARROW, ">");
            str = str.Replace("\n", "<br>");
            return str;
        }

        //---------------------------------------------------------------

        /**
         * given a user submitted input String, filter out any invalid or restricted
         * html.
         * 
         * @param input text (i.e. submitted by a user) than may contain html
         * @return "clean" version of input, with only valid, whitelisted html elements allowed
         */
        public string filter(string input)
        {
            reset();
            string s = input;

            debug("************************************************");
            debug("              INPUT: " + input);

            s = escapeComments(s);
            debug("     escapeComments: " + s);

            s = balanceHTML(s);
            debug("        balanceHTML: " + s);

            s = checkTags(s);
            debug("          checkTags: " + s);

            s = processRemoveBlanks(s);
            debug("processRemoveBlanks: " + s);

            s = validateEntities(s);
            debug("    validateEntites: " + s);

            debug("************************************************\n\n");
            return s;
        }

        protected string escapeComments(string s)
        {
            return Regex.Replace(s, P_COMMENTS, new MatchEvaluator(ConverMatchComments), RegexOptions.Singleline);
        }

        protected string regexReplace(string regex_pattern, string replacement, string s)
        {
            return Regex.Replace(s, regex_pattern, replacement);
        }

        protected string balanceHTML(string s)
        {
            if (alwaysMakeTags)
            {
                //
                // try and form html
                //
                s = regexReplace(P_END_ARROW, "", s);
                s = regexReplace(P_BODY_TO_END, "<$1>", s);
                s = regexReplace(P_XML_CONTENT, "$1<$2", s);

            }
            else
            {
                //
                // escape stray brackets
                //
                s = regexReplace(P_STRAY_LEFT_ARROW, "<$1", s);
                s = regexReplace(P_STRAY_RIGHT_ARROW, "$1$2><", s);

                //
                // the last regexp causes '<>' entities to appear
                // (we need to do a lookahead assertion so that the last bracket can
                // be used in the next pass of the regexp)
                //
                s = s.Replace(P_BOTH_ARROWS, "");
            }
            return s;
        }

        protected string checkTags(string s)
        {
            //替换不允许标签
            foreach (var item in vDisallowed)
            {
                s = Regex.Replace(s, string.Format(@"<{0}\b(.)*?>(.)+?</{0}>", item), "");
            }
            s = Regex.Replace(s, P_TAGS, new MatchEvaluator(ConverMatchTags), RegexOptions.Singleline);

            // these get tallied in processTag
            // (remember to reset before subsequent calls to filter method)
            foreach (string key in vTagCounts.Keys)
            {
                for (int ii = 0; ii < vTagCounts[key]; ii++)
                {
                    s += "</" + key + ">";
                }
            }

            return s;
        }

        protected string processRemoveBlanks(string s)
        {
            foreach (string tag in vRemoveBlanks)
            {
                s = regexReplace("<" + tag + "(\\s[^>]*)?></" + tag + ">", "", s);
                s = regexReplace("<" + tag + "(\\s[^>]*)?/>", "", s);
            }
            return s;
        }

        private string processTag(string s)
        {
            // ending tags
            Match m = P_END_TAG.Match(s);
            if (m.Success)
            {
                string name = m.Groups[1].Value.ToLower();
                if (allowed(name))
                {
                    if (!inArray(name, vSelfClosingTags))
                    {
                        if (vTagCounts.ContainsKey(name))
                        {
                            vTagCounts[name] = vTagCounts[name] - 1;
                            return "</" + name + ">";
                        }
                    }
                }
            }


            // starting tags
            m = P_START_TAG.Match(s);
            if (m.Success)
            {
                string name = m.Groups[1].Value.ToLower();
                string body = m.Groups[2].Value;
                string ending = m.Groups[3].Value;

                //debug( "in a starting tag, name='" + name + "'; body='" + body + "'; ending='" + ending + "'" );
                if (allowed(name))
                {
                    string params1 = "";

                    MatchCollection m2 = P_QUOTED_ATTRIBUTES.Matches(body);
                    MatchCollection m3 = P_UNQUOTED_ATTRIBUTES.Matches(body);
                    List<string> paramNames = new List<string>();
                    List<string> paramValues = new List<string>();
                    foreach (Match match in m2)
                    {
                        paramNames.Add(match.Groups[1].Value); //([a-z0-9]+)
                        paramValues.Add(match.Groups[3].Value); //(.*?)
                    }
                    foreach (Match match in m3)
                    {
                        paramNames.Add(match.Groups[1].Value); //([a-z0-9]+)
                        paramValues.Add(match.Groups[3].Value); //([^\"\\s']+)
                    }

                    string paramName, paramValue;
                    for (int ii = 0; ii < paramNames.Count; ii++)
                    {
                        paramName = paramNames[ii].ToLower();
                        paramValue = paramValues[ii];

                        if (allowedAttribute(name, paramName))
                        {
                            if (inArray(paramName, vProtocolAtts))
                            {
                                paramValue = processParamProtocol(paramValue);
                            }
                            params1 += " " + paramName + "=\"" + paramValue + "\"";
                        }
                    }

                    if (inArray(name, vSelfClosingTags))
                    {
                        ending = " /";
                    }

                    if (inArray(name, vNeedClosingTags))
                    {
                        ending = "";
                    }

                    if (ending == null || ending.Length < 1)
                    {
                        if (vTagCounts.ContainsKey(name))
                        {
                            vTagCounts[name] = vTagCounts[name] + 1;
                        }
                        else
                        {
                            vTagCounts.Add(name, 1);
                        }
                    }
                    else
                    {
                        ending = " /";
                    }
                    return "<" + name + params1 + ending + ">";
                }
                else
                {
                    return "";
                }
            }

            // comments
            m = P_COMMENT.Match(s);
            if (!stripComment && m.Success)
            {
                return "<" + m.Value + ">";
            }

            return "";
        }

        private string processParamProtocol(string s)
        {
            s = decodeEntities(s);
            Match m = P_PROTOCOL.Match(s);
            if (m.Success)
            {
                string protocol = m.Groups[1].Value;
                if (!inArray(protocol, vAllowedProtocols))
                {
                    // bad protocol, turn into local anchor link instead
                    s = "#" + s.Substring(protocol.Length + 1, s.Length - protocol.Length - 1);
                    if (s.StartsWith("#//"))
                    {
                        s = "#" + s.Substring(3, s.Length - 3);
                    }
                }
            }
            return s;
        }

        private string decodeEntities(string s)
        {

            s = P_ENTITY.Replace(s, new MatchEvaluator(ConverMatchEntity));

            s = P_ENTITY_UNICODE.Replace(s, new MatchEvaluator(ConverMatchEntityUnicode));

            s = P_ENCODE.Replace(s, new MatchEvaluator(ConverMatchEntityUnicode));

            s = validateEntities(s);
            return s;
        }

        private string validateEntities(string s)
        {
            s = P_VALID_ENTITIES.Replace(s, new MatchEvaluator(ConverMatchValidEntities));
            s = P_VALID_QUOTES.Replace(s, new MatchEvaluator(ConverMatchValidQuotes));
            return s;
        }

        private static bool inArray(string s, string[] array)
        {
            foreach (string item in array)
            {
                if (item != null && item.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }

        private bool allowed(string name)
        {
            return (vAllowed.Count == 0 || vAllowed.ContainsKey(name)) && !inArray(name, vDisallowed);
        }

        private bool allowedAttribute(string name, string paramName)
        {
            return allowed(name) && (vAllowed.Count == 0 || vAllowed[name].Contains(paramName));
        }

        private string checkEntity(string preamble, string term)
        {

            return ";".Equals(term) && isValidEntity(preamble)
                    ? '&' + preamble
                    : "&" + preamble;
        }
        private bool isValidEntity(string entity)
        {
            return inArray(entity, vAllowedEntities);
        }
        private static string ConverMatchComments(Match match)
        {
            string matchValue = "<!--" + htmlSpecialChars(match.Groups[1].Value) + "-->";
            return matchValue;
        }

        private string ConverMatchTags(Match match)
        {
            string matchValue = processTag(match.Groups[1].Value);
            return matchValue;
        }

        private string ConverMatchEntity(Match match)
        {
            string v = match.Groups[1].Value;
            int decimal1 = int.Parse(v);
            return chr(decimal1);
        }

        private string ConverMatchEntityUnicode(Match match)
        {
            string v = match.Groups[1].Value;
            int decimal1 = Convert.ToInt32("0x" + v, 16);
            return chr(decimal1);
        }

        private string ConverMatchValidEntities(Match match)
        {
            string one = match.Groups[1].Value; //([^&;]*)
            string two = match.Groups[2].Value; //(?=(;|&|$))
            return checkEntity(one, two);
        }
        private string ConverMatchValidQuotes(Match match)
        {
            string one = match.Groups[1].Value; //(>|^)
            string two = match.Groups[2].Value; //([^<]+?)
            string three = match.Groups[3].Value;//(<|$)
            return one + regexReplace(P_QUOTE, "\"", two) + three;
        }

        public bool isAlwaysMakeTags()
        {
            return alwaysMakeTags;
        }

        public bool isStripComments()
        {
            return stripComment;
        }

        class Item
        {
            public string name { get; set; }
            public List<string> parameter { get; set; }
        }

    }

}
