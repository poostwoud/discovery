/// <summary>
/// <para>Describes the possible UBER action values</para>
/// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_reserved_strings">Format documentation</a></para>
/// </summary>
public enum UberActions
{
    NotSet,
    Append,
    Partial,
    Read,
    Remove,
    Replace
}

/// <summary>
/// <para>Describes the possible UBER transclude values</para>
/// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_reserved_strings">Format documentation</a></para>
/// </summary>
public enum UberTransclusion
{
    NotSet,
    False,
    True
}
    
/// <summary>
/// <para>Describes the UBER data element and supports conversion to XML and JSON strings.</para>
/// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_the_tt_lt_data_gt_tt_element">Format documentation</a></para>
/// </summary>
public sealed class UberData
{
    #region Backing Fields

    private object _value;

    #endregion //***** Backing Fields

    #region Properties

    /// <summary>
    /// <para>The document-wide unique identifier for this element.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// <para>A document-wide non-unique identifer for this element.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// <para>Contains one or more link relation values.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public string Rel { get; set; }

    /// <summary>
    /// <para>A resolvable URL associated with this element.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public Uri Url { get; set; }

    /// <summary>
    /// <para>The network request verb associated with this element.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public UberActions Action { get; set; }

    /// <summary>
    /// <para>Indicates whether the content that is returned from the URL should be embedded within the currently loaded document.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public UberTransclusion Transclude { get; set; }

    /// <summary>
    /// <para>Contains a template to be used to construct URL query strings or request bodies depending on the value in the action property.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// <para>Contains one or more media type identifiers for use when sending request bodies.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public string Sending { get; set; }

    /// <summary>
    /// <para>Contains one or more media type identifiers to expect when receiving request bodies.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public string Accepting { get; set; }

    /// <summary>
    /// <para>In the XML variant of the UBER mesage format, inner text of the &lt;data&gt; element contains the value associated with that element. In the JSON variant there is a value property that contains the associated value. </para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_properties">Format documentation</a></para>
    /// </summary>
    public object Value
    {
        get { return _value; }
        set
        {
            //***** Test for null;
            if (value == null)
            {
                _value = value;
                return;
            }

            //*****
            var valueAsString = value.ToString();

            //***** Test for number;
            double testNumber;
            if (double.TryParse(valueAsString, out testNumber))
            {
                _value = value;
                return;
            }

            //***** Test for bool (false/true);
            bool testBool;
            if (bool.TryParse(valueAsString, out testBool))
            {
                _value = value;
                return;
            }

            //***** Test for string;
            if (value is string)
            {
                _value = value;
                return;
            }

            //***** Unknown type (TODO:Throw exception or use validation?)
            _value = null;
        }
    }

    public UberList Data;

    #endregion //***** Properties

    #region Constructors

    public UberData()
    {
        Action = UberActions.NotSet;
        Transclude = UberTransclusion.NotSet;
        Data = new UberList();
    }

    #endregion //***** Constructors

    #region Methods

    private static string ToXmlAttribute(string name, object value)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString())) return string.Empty;
        return string.Format(" {0}=\"{1}\"", name, value);
    }

    private static string ToJsonAttribute(string name, object value, bool first, bool array = false)
    {
        return string.Format("{2} \"{0}\" : {1}", name, (array ? value : string.Format("\"{0}\"", value)), first ? string.Empty : ",");
    }

    public string ToXmlString()
    {
        //*****
        if (Value != null && Data.Count > 0)
            throw new Exception("Value and sub-Data can't both be set for a Data element?");

        //***** Begin element with attributes;
        var xml = new StringBuilder();
        xml.Append("<data");
        if (!string.IsNullOrWhiteSpace(Id)) xml.Append(ToXmlAttribute("id", Id));
        if (!string.IsNullOrWhiteSpace(Name)) xml.Append(ToXmlAttribute("name", Name));
        if (!string.IsNullOrWhiteSpace(Rel)) xml.Append(ToXmlAttribute("rel", Rel));
        if (Url != null && !string.IsNullOrWhiteSpace(Url.ToString())) xml.Append(ToXmlAttribute("url", Url.ToString()));
        if (Action != UberActions.NotSet) xml.Append(ToXmlAttribute("action", Action.ToString().ToLower()));
        if (Transclude != UberTransclusion.NotSet) xml.Append(ToXmlAttribute("transclude", Transclude.ToString().ToLower()));
        if (!string.IsNullOrWhiteSpace(Model)) xml.Append(ToXmlAttribute("model", Model));
        if (!string.IsNullOrWhiteSpace(Sending)) xml.Append(ToXmlAttribute("sending", Sending));
        if (!string.IsNullOrWhiteSpace(Accepting)) xml.Append(ToXmlAttribute("accepting", Accepting));

        //***** No Value and Data;
        if (Value == null && Data.Count == 0)
        {
            //***** Close element;
            xml.Append(" />");
            return xml.ToString();
        }

        //***** Value;
        if (Value != null)
        {
            //***** Close element with value;
            xml.AppendFormat(">{0}</data>", Value);
            return xml.ToString();
        }

        //***** Close element with data elements;
        xml.AppendFormat(">{0}</data>", Data.ToXmlString());
        return xml.ToString();
    }

    public string ToJsonString()
    {
        //*****
        if (Value != null && Data.Count > 0)
            throw new Exception("Value and sub-Data can't both be set for a Data element?");

        //***** Begin object;
        var json = new StringBuilder();
        json.Append("{");

        //***** Add properties;
        var first = true;
        if (!string.IsNullOrWhiteSpace(Id)) { json.Append(ToJsonAttribute("id", Id, first)); first = false; }
        if (!string.IsNullOrWhiteSpace(Name)) { json.Append(ToJsonAttribute("name", Name, first)); first = false; }
        if (!string.IsNullOrWhiteSpace(Rel)) { json.Append(ToJsonAttribute("rel", string.Format("\"[{0}]\"", string.Join("\", \"", Rel.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries))), first, true)); first = false; }
        if (Url != null && !string.IsNullOrWhiteSpace(Url.ToString())) { json.Append(ToJsonAttribute("url", Url.ToString(), first)); first = false; }
        if (Action != UberActions.NotSet) { json.Append(ToJsonAttribute("action", Action.ToString().ToLower(), first)); first = false; }
        if (Transclude != UberTransclusion.NotSet) { json.Append(ToJsonAttribute("transclude", Transclude.ToString().ToLower(), first)); first = false; }
        if (!string.IsNullOrWhiteSpace(Model)) { json.Append(ToJsonAttribute("model", Model, first)); first = false; }
        if (!string.IsNullOrWhiteSpace(Sending)) { json.Append(ToJsonAttribute("sending", Sending, first)); first = false; }
        if (!string.IsNullOrWhiteSpace(Accepting)) { json.Append(ToJsonAttribute("accepting", Accepting, first)); first = false; }

        //***** Close object, no Value and Data;
        if (Value == null && Data.Count == 0)
        {
            //***** Close object;
            json.Append("}");
            return json.ToString();
        }

        //***** Close object with value attribute;
        if (Value != null)
        {
            //***** Add value attribute;
            json.Append(ToJsonAttribute("value", Value, first));

            //***** Close object;
            json.Append("}");
            return json.ToString();
        }

        //***** Close object with data objects;
        json.Append(ToJsonAttribute("data", Data.ToJsonString(), first, true));
            
        //***** Close object;
        json.Append("}");
        return json.ToString();
    }

    #endregion //***** Methods
}

/// <summary>
/// UberData list as a specialized list for converting to XML and JSON strings.
/// </summary>
public sealed class UberList : List<UberData>
{
    #region Methods

    public string ToXmlString()
    {
        var xml = new StringBuilder();
        foreach (var item in this)
            xml.Append(item.ToXmlString());
        return xml.ToString();
    }

    public string ToJsonString()
    {
        var json = new StringBuilder();
        json.Append("[");
        var first = true;
        foreach (var item in this)
        {
            json.AppendFormat("{1} {0}", item.ToJsonString(), first ? string.Empty : ",");
            first = false;
        }
        json.Append("]");
        return json.ToString();
    }

    #endregion //***** Methods
}    

/// <summary>
/// <para>Describes an UBER message.</para>
/// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html">Format documentation</a></para>
/// </summary>
public sealed class UberBuilder
{
    #region Properties

    /// <summary>
    /// <para>The main element in UBER messages.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_the_tt_lt_data_gt_tt_element">Format documentation</a></para>
    /// </summary>
    public UberList Data;

    /// <summary>
    /// <para>The element that carries error details from the previous request.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_the_tt_lt_error_gt_tt_element">Format documentation</a></para>
    /// </summary>
    public UberList Error;

    #endregion //***** Properties

    #region Constructors

    public UberBuilder()
    {
        Data = new UberList();
        Error = new UberList();
    }

    #endregion //***** Constructors

    #region Methods

    /// <summary>
    /// <para>Outputs a XML representation of the UBER message.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_xml_example">Example</a></para>
    /// </summary>
    /// <returns>XML as string</returns>
    public string ToXmlString()
    {
        //***** Begin UBER element;
        var xml = new StringBuilder();
        xml.Append("<uber version=\"1.0\">");
            
        //***** Append data;
        xml.Append(Data.ToXmlString());

        //***** If available, add error data;
        if (Error.Count > 0)
            xml.AppendFormat("<error>{0}</error>", Error.ToXmlString());

        //***** Close UBER element;
        xml.Append("</uber>");
        return xml.ToString();
    }

    /// <summary>
    /// <para>Outputs a JSON representation of the UBER message.</para>
    /// <para><a href="https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_json_example">Example</a></para>
    /// </summary>
    /// <returns>JSON as string</returns>
    public string ToJsonString()
    {
        //***** Open UBER object;
        var json = new StringBuilder();
        json.Append("{ \"uber\" : {");

        //***** Add version attribute;
        json.Append(" \"version\" : \"1.0\"");

        //***** If available, add data;
        if (Data.Count > 0)
            json.AppendFormat(", \"data\" : {0}", Data.ToJsonString());

        //***** If available, add error data;
        if (Error.Count > 0)
            json.AppendFormat(", \"error\" : {0}", Error.ToJsonString());

        //***** Close UBER object;
        json.Append("} }");
        return json.ToString();
    }

    #endregion //***** Methods
}
