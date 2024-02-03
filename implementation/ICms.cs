using System.ComponentModel.DataAnnotations;

namespace qodev_content_management_services.implementation;
 public interface ICms
    {
        public Guid id { get; set; }
        public string pageKey { get; set; }
        public string path { get; set; }
        [Required]
        public string content { get; set; }
        public int access { get; set; }
        public int isDisabled { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }

    public class HeaderElements
    {
        public string headerTitle { get; set; }
        public bool stickyHeader { get; set; }
        public List<Menu> menus { get; set; }
    }

    public class ContentItem
    {
        [Required]
        public string contentKey { get; set; }
        public bool hasAuthorizedBlock { get; set; }
        public bool hasForm { get; set; }
        public bool hasStepper { get; set; }
        public bool hasMultiForm { get; set; }
        public bool hasSidebar { get; set; }
        public Header header { get; set; }
        public Elements elements { get; set; }
        public NonStepperForm nonStepperForm { get; set; }
        public List<StepperForm> stepperForm { get; set; }
        public List<MultiForm> multiForm { get; set; }
    }

    public class Header
    {
        public HeaderElements elements { get; set; }
    }

    public class Elements
    {
        public Buttons buttons { get; set; }
        public AlertMessage alertMessageKey { get; set; }
        public Typography typography { get; set; }
        public CustomLabel customLabel { get; set; }
    }

    public class Buttons
    {
        public List<ButtonsValues> values { get; set; }
    }

    public class ButtonsValues
    {
        public string buttonType { get; set; }
        public string buttonKey { get; set; }
        public string pageUrl { get; set; }
        public string variant { get; set; }
        public string size { get; set; }
        public bool loading { get; set; }
    }

    public class AlertMessage
    {
        // Add properties if needed
    }

    public class Typography
    {
        public List<TypographyValues> values { get; set; }
    }

    public class TypographyValues
    {
        // Add properties if needed
    }

    public class CustomLabel
    {
        // Add properties if needed
    }

    public class NonStepperForm
    {
        public string formKey { get; set; }
        public NonStepperFormElements elements { get; set; }
    }

    public class NonStepperFormElements
    {
        public List<NonStepperFormValues> values { get; set; }
    }

    public class NonStepperFormValues
    {
        // Add properties if needed
    }

    public class Menu
    {
        // Add properties if needed
    }

    public class StepperForm
    {
        // Add properties if needed
    }

    public class MultiForm
    {
        // Add properties if needed
    }