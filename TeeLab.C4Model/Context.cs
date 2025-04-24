using Structurizr;

namespace TeeLab.C4Model;

public class ContextDiagram
{
    private C4 Project { get; set; }
    public Person User { get; set; }
    public Person Designer { get; set; }
    public Person Manufacturer { get; set; }


    public SoftwareSystem TeeLab { get; set; }
    public SoftwareSystem Cloudinary { get; set; }
    public SoftwareSystem Stripe { get; set; }

    public ContextDiagram(C4 project)
    {
        Project = project ?? throw new ArgumentNullException(nameof(project));
        
        User = Project.Model.AddPerson("User", "Use TeeLab to buy their clothes.");
        User.AddTags(nameof(User));
        Designer = Project.Model.AddPerson("Designer", "Designs the clothes.");
        Designer.AddTags(nameof(Designer));
        Manufacturer = Project.Model.AddPerson("Manufacturer", "Manufactures the clothes.");
        Manufacturer.AddTags(nameof(Manufacturer));
        
        TeeLab = Project.Model.AddSoftwareSystem("TeeLab", "A platform to buy, sell and designs clothes.");
        TeeLab.AddTags(nameof(TeeLab));
        Cloudinary = Project.Model.AddSoftwareSystem("Cloudinary", "A cloud service to manage images.");
        Cloudinary.AddTags(nameof(Cloudinary));
        Stripe = Project.Model.AddSoftwareSystem("Stripe", "A cloud service to manage payments.");
        Stripe.AddTags(nameof(Stripe));
    }

    public void Generate()
    {
        User.Uses(TeeLab, "Use TeeLab to buy their clothes.");
        Designer.Uses(TeeLab, "Use Designer to buy their clothes.");
        Manufacturer.Uses(TeeLab, "Use Manufacturer to buy their clothes.");
        
        TeeLab.Uses(Cloudinary, "Use Cloudinary to buy their clothes.");
        TeeLab.Uses(Stripe, "Use Stripe to buy their clothes.");
        
        ApplyStyles();
        Publish();
    }

    private void ApplyStyles()
    {
        var styles = Project.ViewSet.Configuration.Styles;
        
        styles.Add(new ElementStyle(nameof(TeeLab)) {Background = "#FF1420", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(Cloudinary)) {Background = "#0D2A4B", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(Stripe)) {Background = "#4145BC", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(User)) {Background = "#514861", Shape = Shape.Person, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(Designer)) {Background = "#3A679F", Shape = Shape.Person, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(Manufacturer)) { Background = "#86B3EA", Shape = Shape.Person, Color = "#FFFFFF" });
    }
    private void Publish()
    {
        SystemContextView view = Project.ViewSet.CreateSystemContextView(TeeLab, "TeeLab Context View", "");
        view.AddAllElements();
    }
}