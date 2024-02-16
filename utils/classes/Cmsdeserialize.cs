using qodev_content_management_services.implementation;

namespace qodev_content_management_services.utils.classes;

public class SubMenuItems
{
    
}
public class MenuItems
{
    public string link { get; set; }
    public string name { get; set; }
    public string relatedLinks { get; set; }
    public List<SubMenuItems> subMenuItems { get; set; }
}
public class Elements
{
    public string headerTitle { get; set; }
    public bool stickyHeader { get; set; }
    public List<MenuItems> menus { get; set; }
    public bool useRawLogoUrl { get; set; }
    public string imageSrc { get; set; }
    public List<Buttons> floatRightButtons {get;set;}
}

public class Header
{
    public Elements elements { get; set; }
}

public class ButtonElement
{
    public string key { get; set; }
    public string value { get; set; }
}
public class Buttons
{
    public string buttonType { get; set; }
    public ButtonElement buttonElement { get; set; }
    public string pageUrl { get; set; }
    public string variant { get; set; }
    public string size { get; set; }
    public bool loading { get; set; }
    public string actionKey { get; set; }
    public bool shouldNavigate { get; set; }
    public string href { get; set; }
}

public class ServicesElements {
    public string name {get;set;}
    public string description {get;set;}
    public string icon {get;set;}
}

public class ImageSourceElements {
    public string key { get;set;}
    public string value {get;set;}
}

public class DataElements {
    public List<ServicesElements> services {get;set;}
    public List<ImageSourceElements> imageSource {get;set;}
}

public class Element
{
    public List<Buttons> buttons { get; set; }
    public AlertMessageKey alertMessageKey { get; set; }
    public List<LabelValues> labels { get; set; }
    public CustomLabel customLabel { get; set; }
    public DataElements data {get;set;}
}


public class LabelValues
{
    public string key { get; set; }
    public string value { get; set; }
}



public class CustomLabel
{
}

public class AlertMessageKey
{
}

public class Content
{
    public string contentKey { get; set; }
    public bool hasAuthorizedBlock { get; set; }
    public bool hasForm { get; set; }
    public bool hasContainer { get; set; }
    public bool hasStepper { get; set; }
    public bool hasMultiForm { get; set; }
    public bool hasSidebar { get; set; }
    public Header header { get; set; }
    public Element elements { get; set; }
    public List<object> stepperForm { get; set; }
    public List<object> multiForm { get; set; }
}
public class Cmsdeserialize
{
    public string pageKey { get; set; }
    public int access { get; set; }
    public string path { get; set; }
    public List<Content> content { get; set; }
    public int isDisabled { get; set; }
}

public class Root
{
    public List<Cmsdeserialize> cmsdeserialize { get; set; }
}